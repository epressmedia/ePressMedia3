using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Linq;
using Telerik.Web.UI;

using System.Web.Profile;
using System.Configuration;


namespace Cp.User
{
    public partial class CpUserUsersEx : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserRoleList();

      
            }
            LoadUsers();
            
        }


        void LoadUserRoleList()
        {
            var userroles = from u in context.UserRoles
                            select new
                            {
                                RoleId = u.RoleId,
                                RoleName = u.RoleName
                            };
            ddl_userroles.DataTextField = "RoleName";
            ddl_userroles.DataValueField = "RoleName";
            ddl_userroles.DataSource = userroles;
            ddl_userroles.DataBind();

            ddl_userroles.Items.Insert(0, new ListItem("-- Select User Role --", ""));

        }


        void LoadUsers()
        {
            
            
                if (ddl_userroles.SelectedIndex <= 0)
            {
                var users = (from c in context.EPM_vw_user_infos
                             select new
                             {
                                 c.UserId,
                                 FullName = c.FullName,
                                 LoginName = c.LoginName,
                                 Email = c.Email,
                                 IsValidated = c.IsValidated,
                                 Status = c.Status,
                                 IsLockedOut = c.IsLockedOut,
                                 LastLoginDate = c.LastLoginDate.ToLocalTime(),
                                 RegisterDate = c.CreateDate.ToLocalTime()
                             }).ToList()
                             .OrderByDescending(c=>c.RegisterDate);
                UserGrid.DataSource = users;
                UserGrid.DataBind();
            }
            else
            {
                var users = (from c in context.EPM_vw_user_infos
                             join role in context.UserRoleMemberships on c.UserId equals role.UserId
                             where role.UserRoles.RoleName == ddl_userroles.SelectedItem.Text
                             select new
                             {
                                 c.UserId,
                                 FullName = c.FullName,
                                 LoginName = c.LoginName,
                                 Email = c.Email,
                                 IsValidated = c.IsValidated,
                                 Status = c.Status,
                                 IsLockedOut = c.IsLockedOut,
                                 LastLoginDate = c.LastLoginDate.ToLocalTime(),
                                 RegisterDate = c.CreateDate.ToLocalTime()
                             }).ToList();
                UserGrid.DataSource = users;

                UserGrid.DataBind();
            }

            
            
        }


        protected void UserGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
          if (e.CommandName.Equals("config"))
        {
            MembershipUser user = Membership.GetUser(e.CommandArgument.ToString());

            OpenEditWindow(user.ProviderUserKey.ToString());
        }

            else if (e.CommandName.Equals("Deactivate"))
            {
                MembershipUser user = Membership.GetUser(e.CommandArgument.ToString());
                user.IsApproved = false;
                Membership.UpdateUser(user);
                e.Item.OwnerTableView.DataBind();
                LoadUsers();

            }
            else if (e.CommandName.Equals("Activate"))
            {

                MembershipUser user = Membership.GetUser(e.CommandArgument.ToString());
                user.IsApproved = true;
                Membership.UpdateUser(user);
                e.Item.OwnerTableView.DataBind();
                LoadUsers();
            }
            else if (e.CommandName.Equals("Approve"))
            {

                var profile = ProfileBase.Create(e.CommandArgument.ToString());
                profile.SetPropertyValue("Verified", true);
                profile.Save();
                e.Item.OwnerTableView.DataBind();
                LoadUsers();

                
            }
            else if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
            {
                ConfigureExport();
            }
        }
        public void ConfigureExport()
        {
            UserGrid.ExportSettings.ExportOnlyData = true;
            UserGrid.ExportSettings.IgnorePaging = true;
            UserGrid.ExportSettings.OpenInNewWindow = false;
            UserGrid.ExportSettings.FileName = "UserList_" + DateTime.Now.ToShortDateString();
            
        }

        protected void OpenEditWindow(string uid)
        {
            RadWindow windowpreview = new RadWindow();
            windowpreview.NavigateUrl = "/CP/User/userdetail.aspx?uid=" + uid.ToString();// +e.CommandArgument.ToString();
            windowpreview.VisibleOnPageLoad = true;
            windowpreview.Modal = true;
            windowpreview.Behaviors = Telerik.Web.UI.WindowBehaviors.Close;
            windowpreview.OpenerElementID = listing_div.ClientID;
            windowpreview.AutoSize = true;
            windowpreview.ShowContentDuringLoad = true;
            windowpreview.Animation = WindowAnimation.Fade;
            windowpreview.VisibleStatusbar = false;
            windowpreview.MinWidth = 780;
            //windowpreview.RestrictionZoneID = "content-outer";
            windowpreview.OnClientClose = "ReloadOnClientClose";

            listing_div.Controls.Add(windowpreview);
        }





        protected void btn_profile_Click(object sender, EventArgs e)
        {
            GridDataItem di = (GridDataItem)UserGrid.SelectedItems[0];
            string user_id = di.GetDataKeyValue("UserId").ToString();
            Response.Redirect("~/CP/User/UserInfo.aspx?ID=" + user_id);
        }
  

 

        protected void ddl_userroles_SelectedIndexChanged(object sender, EventArgs e)
        {

                LoadUsers();
               // div_userrole.Visible = false;
        }





        protected void UserGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UserGrid.CurrentPageIndex = e.NewPageIndex;
            LoadUsers();
            UserGrid.DataBind();

            //div_userrole.Visible = false;
        }

        protected void UserGrid_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            //div_userrole.Visible = false;
        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {

            RadGrid allusers = new RadGrid();
            Page.Controls.Add(allusers);
            allusers.AutoGenerateColumns = true;
            allusers.ExportSettings.ExportOnlyData = true;
            allusers.ExportSettings.IgnorePaging = true;
            allusers.ExportSettings.OpenInNewWindow = false;

            allusers.MasterTableView.AutoGenerateColumns = true;
            allusers.MasterTableView.DataSource =  EPM.Legacy.Data.DataAccess.GetDataTable("exec EPM_sp_user_info_udf");
            allusers.MasterTableView.DataBind();

            allusers.ExportSettings.FileName = "FullUserList_" + DateTime.Now.ToShortDateString();
            allusers.MasterTableView.ExportToExcel();
        }



        protected void UserGrid_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            LoadUsers();
           // div_userrole.Visible = false;
        }

 


}
}