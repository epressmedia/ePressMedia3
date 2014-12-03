using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Linq;
using Telerik.Web.UI;
using EPM.Core.Admin;

namespace Cp.User
{
    public partial class CpUserUserRoles : System.Web.UI.Page
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {


            defaultUserRoleChecker();
            if (!IsPostBack)
            {
                
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (RoleName.Text.Trim().Equals(string.Empty))
                return;

            if (Roles.RoleExists(RoleName.Text))
            {
                win_manager.RadAlert("User Role " + RoleName.Text + " already exists.", 300, 100, "Error", "");
                //EPM.Legacy.Common.Utility.RegisterJsAlert(this, "User Role " + RoleName.Text + " already exists.");
                return;
            }
             
            Roles.CreateRole(RoleName.Text);

            RoleName.Text = "";

  
            RadGrid1.Rebind();
        }



  

        private void defaultUserRoleChecker()
        {
            List<String> roles = new List<string>(Roles.GetAllRoles());

            foreach (String userrole in EPM.Core.Users.UserRoles.DefaultUserRoles())
            {
                if (!roles.Contains(userrole))
                {
                    Roles.CreateRole(userrole);
                }
            }
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName.Equals("del"))
            {

                string roleid = (e.CommandArgument.ToString());
                DeleteUserRole(roleid);
                RadGrid1.Rebind();
            }
            
        }

        private void DeleteUserRole(string userrole_name)
        {
                        if (Roles.GetUsersInRole(userrole_name).Length > 0)
            {
                win_manager.RadAlert("User Roles in use cannot be deleted.", 300, 100, "Error", "");
                //EPM.Legacy.Common.Utility.RegisterJsAlert(this, "User Roles in use cannot be deleted.");
                return;
            }

            List<String> roles = EPM.Core.Users.UserRoles.DefaultUserRoles();
            if (roles.Contains(userrole_name) || (userrole_name == "System Administrator"))
            {

                win_manager.RadAlert("System default user role cannot be deleted.", 300, 100, "Error", "");
                return;
            }

            Roles.DeleteRole(userrole_name, false);


        }

    }
}