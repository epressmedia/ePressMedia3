using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.Profile;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EPM.Core;
using EPM.Core.Users;

namespace ePressMedia.Controls.Logins
{
    public partial class LoginLinkLine : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.User.Identity.Name.Equals(string.Empty))
            {
                login_panel.Visible = true;
                login_info.Visible = false;
                login_link.HRef = "/account/signin.aspx?ReturnURL=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery);
                l_find_login.Visible = find_login.Visible = ShowFindUserName;
                if (find_login.Visible)
                    find_login.HRef = "/account/ForgotUsername.aspx";
                l_find_password.Visible = find_password.Visible = ShowFindPassword;
                if (find_password.Visible)
                    find_password.HRef = "/account/RecoverPassword.aspx";
            }
            else
            {
                login_panel.Visible = false;
                login_info.Visible = true;
                user_name.Text = "Welcome, " + Page.User.Identity.Name;
            }
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            //Session["Name"].Abandon();
            Response.Redirect("/");
        }

        protected void btn_user_info_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Account/ManageAccount.aspx");
        }


        [Category("EPMProperty")]
        [Description("Show the link to find user name")]
        [DefaultValue(false)]
        public bool ShowFindUserName
        {
            get;
            set;
        }

        [Category("EPMProperty")]
        [Description("Show the link to recover password")]
        [DefaultValue(true)]
        public bool ShowFindPassword
        {
            get;
            set;
        }


    }
}