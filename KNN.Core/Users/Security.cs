using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.Profile;


namespace EPM.Core.Users
{
    public class Security
    {
        public static bool ValidateLogin(System.Web.UI.Page page, string username, string password, bool RememberMe)
        {
            return StandardValidateLogin(page, username, password, RememberMe, false);
        }

        public static bool ValidateLogin(System.Web.UI.Page page, string username, string password, bool RememberMe, bool KeepSignIn)
        {
            if (RememberMe)
                return StandardValidateLogin(page, username, password, RememberMe, true);
            else
                return StandardValidateLogin(page, username, password, RememberMe, false);
        }

        private static bool StandardValidateLogin(System.Web.UI.Page page, string username, string password, bool RememberMe, bool KeepSignIn)
        {
            bool LoginValid = true;

            if (Membership.ValidateUser(username, password))
            {

                MembershipUser user = Membership.GetUser(username);


                var profile = ProfileBase.Create(user.UserName);

                

                if (bool.Parse(profile["PwdExpired"].ToString())) // User must change password
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    page.Session["REDIR"] = page.Request.Url.ToString();
                    page.Response.Redirect("ChangePassword.aspx");

                }
                    


                else if (!bool.Parse(profile["Verified"].ToString())) // the given credential matches, but not verified yet
                {
                    page.Response.Redirect("/Account/VerifyAccount.aspx");
                }
                else // User Logged in
                {

                    if (page.Request.Cookies["username"] == null && RememberMe)
                    {
                        page.Response.Cookies["username"].Value = username;
                        page.Response.Cookies["username"].Expires = DateTime.Now.AddDays(7);
                    }
                    else if (page.Request.Cookies["username"] != null && RememberMe == false)
                    {
                        page.Response.Cookies["username"].Value = username;
                        page.Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                    }

                    if (KeepSignIn)
                    {
                        // Kee in logged in Case code 
                        HttpCookie cookie = new HttpCookie("EPMSignIn");
                        cookie.Values["eu"] = CommonUtility.Encrypt(username, "2Pr2ssM2diA", true);
                        cookie.Values["ep"] = CommonUtility.Encrypt(password, "2Pr2ssM2dia", true);
                        cookie.Expires = DateTime.MaxValue;
                        page.Response.Cookies.Add(cookie);
                    }



                    AccountHelper.AddLoginHistory(username);
                    FormsAuthentication.RedirectFromLoginPage(username, true);
                }
            }
            else
            {
                LoginValid = false;
                // Invalid Name or Password
                //Message.Visible = true;
            }

            return LoginValid;
        }

        public static bool GuestViewLimitValid(System.Web.UI.Page context)
        {
            bool result = true;
            int viewCount = -1;
            string title = "ViewLimit"; (EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Default Title") + "_ViewLimit").Replace(" ", "");
            string viewLimit = EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Guest View Limit");

            if (viewLimit.Length > 0)
            {
                if (int.Parse(viewLimit) > 0)
                    viewCount = int.Parse(viewLimit);
            }

            if (viewCount > 0)
            {
                if (context.Request.Cookies[title] != null)
                {
                    if (int.Parse(context.Request.Cookies[title].Value.ToString()) >= viewCount)
                        result = false;
                    else
                        IncreaseLimitCount(context, title);

                }
                else
                {
                    IncreaseLimitCount(context, title);
                }
            }

            return result;
        }
        private static void IncreaseLimitCount(System.Web.UI.Page context, string title)
        {
            //EPM.Legacy.Common.Utility.RegisterJsAlert(context, "This is a C test message for debugging." + title);
            if (context.Request.Cookies[title] == null)
            {
                // EPM.Legacy.Common.Utility.RegisterJsAlert(context, "Goes to Case 1.");
                context.Response.Cookies[title].Value = "1";

            }
            else
            {

                if (context.Request.Cookies[title].Value.Length > 0)
                {
                    //EPM.Legacy.Common.Utility.RegisterJsAlert(context, "Current Value:" + context.Request.Cookies[title].Value);


                    context.Response.Cookies[title].Value = (int.Parse(context.Request.Cookies[title].Value) + 1).ToString();
                }
                else
                {
                    //EPM.Legacy.Common.Utility.RegisterJsAlert(context, "Goes to Case 2.");
                    context.Response.Cookies[title].Value = "1";
                }



            }

            context.Response.Cookies[title].Expires = DateTime.Now.AddDays(1);
        }
    }
}
