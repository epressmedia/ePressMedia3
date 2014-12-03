using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using EPM.Core;

public partial class Account_ForgotUsername : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //    Master.SetBreadCrumb("<span> &gt; 계정관리 &gt; 아이디찾기</span>");
    }

    protected void SendId_Click(object sender, EventArgs e)
    {
        MembershipUserCollection users = Membership.FindUsersByEmail(Email.Text);

        if (users.Count > 0)
        {
            IEnumerator userEnum = users.GetEnumerator();
            userEnum.MoveNext();
            MembershipUser thisUser = userEnum.Current as MembershipUser;

            //AccountHelper.SendInformationMail(thisUser.Email, GetGlobalResourceObject("Resources", "Account.email_PasswordReset").ToString(),// "요청하신 이용정보를 안내해 드립니다.",
            //    "~/MailTemplate/ForgotUserName.htm", thisUser.UserName, Request.Url.Host);
            if (EPM.Email.EmailSender.SendTemplateEmail(thisUser.Email, "Forgot UserName", thisUser.UserName, Request.Url.Host.ToString()))
            {
                //EPM.Legacy.Common.Utility.RegisterJsAlert(this, GetGlobalResourceObject("Resources", "Account.lbl_EmailConfirmSuccess").ToString());// "회원등록시 설정하신 이메일 주소로 안내 메일이 발송되었습니다.");//Account.lbl_EmailConfirmSuccess
                EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, GetGlobalResourceObject("Resources", "Account.lbl_EmailConfirmSuccess").ToString(), string.Empty, "/Account/SignIn.aspx");
            }
            else
            {
                EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, GetGlobalResourceObject("Resources", "Account.msg_ProblemOccured").ToString(), string.Empty, "/");
            }

            

        }
        else
        {
            EPM.Legacy.Common.Utility.RegisterJsAlert(this, GetGlobalResourceObject("Resources", "Account.lbl_EmailConfirmFail").ToString());//"등록되지 않은 이메일 주소입니다.");//Account.lbl_EmailConfirmFail
        }
    }
}