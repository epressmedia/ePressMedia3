using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Profile;
using System.Configuration;
using Telerik.Web.UI;

namespace ePressMedia.Cp.User
{
    public partial class UserDetail : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            dataload();
        }

        void dataload()
        {
            Guid user_id = Guid.Parse(Request.QueryString["uid"].ToString());// new Guid((e.Item as GridDataItem).GetDataKeyValue("UserId").ToString());
            String userName = context.Users.Where(c => c.UserId == user_id).ToList()[0].UserName;// (e.Item as GridDataItem).GetDataKeyValue("LoginName").ToString();

            var userroles = from u in context.UserRoleMemberships
                            where u.UserId == user_id
                            select new
                            {
                                RoleId = u.UserRoles.RoleId,
                                RoleName = u.UserRoles.RoleName
                            };



            rg_userrole.DataSource = userroles;
            rg_userrole.DataBind();

            div_userrole.Visible = true;
            LoadUserProfile(userName);
            if (!IsPostBack)
                LoadUserRoles(user_id);
            LoadUDFPanel(user_id);
        }
        void LoadUDFPanel(Guid userid)
        {

            EPM.Data.Model.EPM_vw_user_info ep = context.EPM_vw_user_infos.Where(a => a.UserId == userid).Single();

            lbl_emailaddress.Text = ep.Email;
            lbl_fullname.Text = ep.FullName;
            lbl_loginname.Text = ep.LoginName;

            var c = LoadControl("/Page/UDFEntryPanel.ascx");
            ePressMedia.Pages.UDFEntryPanel uc = c as ePressMedia.Pages.UDFEntryPanel;
            uc.ID = "udfs_panel";
            uc.ReadOnly = true;
            uc.ContentTypeId = 8;
            uc.ContentId = context.UserLinks.SingleOrDefault(x => x.UserId == userid).UserLinkId;
            UDF_Panel.Controls.Add(uc);
        }
        private void LoadUserRolesbyUserId(Guid user_id)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var userroles = from u in context.UserRoleMemberships
                            where u.UserId == user_id
                            select new
                            {
                                RoleId = u.UserRoles.RoleId,
                                RoleName = u.UserRoles.RoleName
                            };



            rg_userrole.DataSource = userroles;
            rg_userrole.DataBind();
            LoadUserRoles(user_id);
        }


        private void LoadUserProfile(string userName)
        {

            MembershipUser user = Membership.GetUser(userName);

            var profile = ProfileBase.Create(userName);

            NewPass.Text = "";
            NewPass.ToolTip = userName;
            btn_ResetPw.Visible = true;
            PostalCode.Text = profile.GetPropertyValue("Zip").ToString().ToUpper();
            Address1.Text = profile.GetPropertyValue("Address1").ToString();
            Address2.Text = profile.GetPropertyValue("Address2").ToString();

            Province.Text = profile.GetPropertyValue("Province").ToString();

            City.Text = profile.GetPropertyValue("City").ToString();


            if (!string.IsNullOrEmpty(profile.GetPropertyValue("UserComment").ToString()))
                UserComment.Text = profile.GetPropertyValue("UserComment").ToString();


        }

        private void LoadUserRoles(Guid user_id)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var userroles = (from u in context.UserRoles
                             select new
                             {
                                 RoleId = u.RoleId,
                                 RoleName = u.RoleName
                             }
                            )
                            .Except(from u1 in context.UserRoleMemberships
                                    where u1.UserId == user_id
                                    select new
                                    {
                                        RoleId = u1.UserRoles.RoleId,
                                        RoleName = u1.UserRoles.RoleName
                                    });

            if (userroles.Count() > 0)
            {
                ddl_userrole.DataTextField = "RoleName";
                ddl_userrole.DataValueField = "RoleId";
                ddl_userrole.DataSource = userroles;
                ddl_userrole.DataBind();

                ddl_userrole.Items.Insert(0, new ListItem("-- Select User Role --", "00000"));


            }
            else
            {
                ddl_userrole.Items.Clear();
                ddl_userrole.Items.Insert(0, new ListItem("No User Role Available", "00000"));
            }


        }
        protected void btn_userrole_Click(object sender, EventArgs e)
        {
            if (ddl_userrole.SelectedIndex > 0)
            {


                Guid user_id = Guid.Parse(Request.QueryString["uid"].ToString());// new Guid((e.Item as GridDataItem).GetDataKeyValue("UserId").ToString());
                    String user_name = context.Users.Where(c => c.UserId == user_id).ToList()[0].UserName;// (e.Item as GridDataItem).GetDataKeyValue("LoginName").ToString();
                    //string user_name = di.GetDataKeyValue("LoginName").ToString();
                    //Guid user_id = new Guid(di.GetDataKeyValue("UserId").ToString());

                    Roles.AddUserToRole(user_name, ddl_userrole.SelectedItem.Text.ToString());


                    LoadUserRolesbyUserId(user_id);
                
            }
        }

        protected void btn_ResetPw_Click(object sender, EventArgs e)
        {


            ResetPassword();
        }

        private void ResetPassword()
        {
            MembershipUser user = Membership.GetUser(NewPass.ToolTip);
            if (user.IsLockedOut)
                user.UnlockUser();

            string pwd = user.ResetPassword();

            var profile = ProfileBase.Create(NewPass.ToolTip);
            profile.SetPropertyValue("PwdExpired", true);
            profile.Save();

            NewPass.Text = string.Format("New Password is {0}", pwd);
            NewPass.Visible = true;
            btn_ResetPw.Visible = false;

            //KNN.Core.AccountHelper.SendInformationMail(user.Email, "임시비밀번호를 안내해 드립니다.",
            //             "~/MailTemplate/RecoverPassword.htm", pwd, Request.Url.Host.ToString());

            EPM.Email.EmailSender.SendTemplateEmail(user.Email, "Recover Password", pwd, Request.Url.Host.ToString());

        }
        protected void rg_userrole_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                String RoleName = (e.Item as GridDataItem).GetDataKeyValue("RoleName").ToString();

                //GridDataItem di = (GridDataItem)UserGrid.SelectedItems[0];
                //string user_name = di.GetDataKeyValue("LoginName").ToString();
                //Guid user_id = new Guid(di.GetDataKeyValue("UserId").ToString());


                Guid user_id = Guid.Parse(Request.QueryString["uid"].ToString());// new Guid((e.Item as GridDataItem).GetDataKeyValue("UserId").ToString());
                String user_name = context.Users.Where(c => c.UserId == user_id).ToList()[0].UserName;// (e.Item as GridDataItem).GetDataKeyValue("LoginName").ToString();
                Roles.RemoveUserFromRole(user_name, RoleName);

                LoadUserRolesbyUserId(user_id);
            }
        }
    }
}