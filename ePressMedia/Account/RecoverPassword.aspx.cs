using System;
using System.Web.Security;
using System.Web.Profile;
using EPM.Core;

public partial class Account_RecoverPassword : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
            //Master.SetBreadCrumb("<span> &gt; 계정관리 &gt; 비밀번호찾기</span>");
    }

    protected void SetPassword_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(UserName.Text);

        if (user == null)
        {
            EPM.Legacy.Common.Utility.RegisterJsAlert(this, GetGlobalResourceObject("Resources", "Account.msg_UserNotRegistered").ToString());
        }
        else
        {
            if (user.Email.Equals(Email.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                if (user.IsLockedOut)
                    user.UnlockUser();

                string pwd = user.ResetPassword();

                // profile base should be recreated because the current context doesn't have the login user info
                 var profile = ProfileBase.Create(user.UserName);
                profile.SetPropertyValue("PwdExpired", true);

                profile.Save();

                if (EPM.Email.EmailSender.SendTemplateEmail(user.Email, "Recover Password", pwd, Request.Url.Host.ToString()))
                    EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, GetGlobalResourceObject("Resources", "Account.msg_PasswordSent").ToString(), string.Empty, "/Account/Signin.aspx");
                else
                    EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, GetGlobalResourceObject("Resources", "Account.msg_ProblemOccured").ToString(), string.Empty, "/");
            }
            else
            {
                EPM.Legacy.Common.Utility.RegisterJsAlert(this, GetGlobalResourceObject("Resources", "Account.msg_UserNotRegistered").ToString());
            }
        }
    }
}