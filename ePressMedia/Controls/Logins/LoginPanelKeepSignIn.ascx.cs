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
    public partial class LoginPanelKeepSignIn : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginPanel.Visible = Page.User.Identity.Name.Equals(string.Empty);
            if (LoginPanel.Visible)
            {
                if (Request.Cookies["EPMSignIn"] != null)
                {
                    txt_UserName.Text = CommonUtility.Decrypt(Request.Cookies["EPMSignIn"]["eu"], "2Pr2ssM2diA", true);
                    txt_Password.Text = CommonUtility.Decrypt(Request.Cookies["EPMSignIn"]["ep"], "2Pr2ssM2dia", true);

                    LoginFunction();
                }
            }
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            LoginFunction();

        }

        private void LoginFunction()
        {
            if (!EPM.Core.Users.Security.ValidateLogin(this.Parent.Page, txt_UserName.Text, txt_Password.Text, ChkRememberMe.Checked, true))
            {
                if (Request.Cookies["EPMSignIn"] != null)
                {
                    Request.Cookies["EPMSignIn"].Expires = DateTime.Now;
                }
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, GetGlobalResourceObject("Resources", "Login.FailMassage").ToString());
            }
        }
    }
}
