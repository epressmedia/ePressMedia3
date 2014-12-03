using System;
using System.Web.Security;
using System.Web.Profile;

using EPM.Core;
using EPM.Core.Users;

namespace ePressMedia.Cp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["admin_username"] != null)
            {
                UserName.Text = Request.Cookies["admin_username"].Value;
                ChkRememberMe.Checked = true;
            }
            LoadVersion();
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (Membership.ValidateUser(UserName.Text, Password.Text))
            {

                MembershipUser user = Membership.GetUser(UserName.Text);


                var profile = ProfileBase.Create(user.UserName);



                if (bool.Parse(profile["PwdExpired"].ToString())) // User must change password
                {
                    FormsAuthentication.SetAuthCookie(UserName.Text, false);
                    Session["REDIR"] = Request.Url.ToString();
                    Response.Redirect("ChangePassword.aspx");

                }
                else if (profile["Verified"].ToString() == "false") // the given credential matches, but not verified yet
                {
                    Response.Redirect("VerifyAccount.aspx");
                }
                else // User Logged in
                {
                    if (Request.Cookies["admin_username"] == null && ChkRememberMe.Checked)
                    {
                        Response.Cookies["admin_username"].Value = UserName.Text;
                        Response.Cookies["admin_username"].Expires = DateTime.Now.AddDays(7);
                    }
                    else if (Request.Cookies["admin_username"] != null && ChkRememberMe.Checked == false)
                    {
                        Response.Cookies["admin_username"].Value = UserName.Text;
                        Response.Cookies["admin_username"].Expires = DateTime.Now.AddDays(-1);
                    }

                    AccountHelper.AddLoginHistory(UserName.Text);
                    //Response.Redirect("Default.aspx");
                    FormsAuthentication.RedirectFromLoginPage(UserName.Text, false);
                }
            }
            else
            {
                // Invalid Name or Password
                Message.Visible = true;
            }

            
        }

        public void LoadVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(MapPath("~/bin/ePressMedia.dll"));
            string ver = assembly.GetName().Version.Major.ToString() + "." + assembly.GetName().Version.Minor.ToString() + "." + assembly.GetName().Version.Build.ToString();
            version.Text = "v" + ver.ToString();
        }
    }
}