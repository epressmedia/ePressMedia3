using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Profile;

using EPM.Core;
using EPM.Core.Users;

namespace ePressMedia.Controls.Logins
{
    public partial class DefaultLoginPanel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoginPanel.Visible = Page.User.Identity.Name.Equals(string.Empty);
            AfterLoginPanel.Visible = !Page.User.Identity.Name.Equals(string.Empty);
            if (LoginPanel.Visible)
            {
                if (Request.Cookies["username"] != null)
                {
                    txt_UserName.Text = Request.Cookies["username"].Value;
                }
            }
            if (AfterLoginPanel.Visible)
            {
                lbl_username2.Text = Page.User.Identity.Name.ToString();
            }
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!EPM.Core.Users.Security.ValidateLogin(this.Parent.Page,txt_UserName.Text, txt_Password.Text, ChkRememberMe.Checked))
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, GetGlobalResourceObject("Resources", "Login.FailMassage").ToString());

        }
        protected void btn_myAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Account/ManageAccount.aspx");
        }
        

        protected void btn_LogOut_Click(object sender, EventArgs e)
        {
            LogOut();
        }
        
        void LogOut()
        {
            FormsAuthentication.SignOut();

            //Session["Name"].Abandon();
            Response.Redirect("/");
        }
    }
}