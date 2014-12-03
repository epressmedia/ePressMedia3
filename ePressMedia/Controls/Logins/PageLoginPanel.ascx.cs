using System;
using System.Web.Security;
using System.Web.Profile;

using EPM.Core;
using EPM.Core.Users;


namespace ePressMedia.Controls.Logins
{
    public partial class PageLoginPanel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Master.SetBreadCrumb("<span> &gt; 로그인</span>");

                if (Request.Cookies["username"] != null)
                {
                    UserName.Text = Request.Cookies["username"].Value;
                    ChkRememberMe.Checked = true;
                }
            }
            //UserName.Focus();
            //this.Form.DefaultButton = BtnLogin.UniqueID;
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            Message.Visible = !EPM.Core.Users.Security.ValidateLogin(this.Page, UserName.Text, Password.Text, ChkRememberMe.Checked);
        }
    }
}