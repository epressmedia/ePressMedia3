using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Account_ChangePassword : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurPwd.Focus();

        //if (!IsPostBack)
        //{
        //    Master.SetBreadCrumb("<span> &gt; 계정관리 &gt; 비밀번호변경</span>");
        //    //UserName.Text = User.Identity.Name;
        //}
    }


    protected void BtnChange_Click(object sender, EventArgs e)
    {
        if (Membership.ValidateUser(UserName.Text, CurPwd.Text) == false)
        {
            Message.Text = GetGlobalResourceObject("Resources", "Account.msg_WrongPassword").ToString();//"현재 패스워드가 정확하지 않습니다.";
            return;
        }

        if (NewPwd.Text.Equals(ConfirmPwd.Text) == false)
        {
            Message.Text = GetGlobalResourceObject("Resources", "Account.msg_PasswordMatch").ToString();// "패스워드 확인 값이 일치하지 않습니다";
            return;
        }

        if (NewPwd.Text.Length < 6)
        {
            Message.Text = GetGlobalResourceObject("Resources", "Account.msg_PasswordValidation").ToString();// "패스워드는 최소 6자리 이상 입력해야 합니다.";
            return;
        }

        MembershipUser user = Membership.GetUser(UserName.Text);
        user.ChangePassword(CurPwd.Text, NewPwd.Text);

        //user.Comment = "";
        //Membership.UpdateUser(user);

        var profile = HttpContext.Current.Profile;
        profile.SetPropertyValue("PwdExpired", false);
        //ProfileBase profile = getpr.GetProfile(user.UserName);
        //profile.PwdExpired = false;
        profile.Save();

        //EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, "패스워드가 변경되었습니다.", string.Empty, "~/");
        Response.Redirect("~/");
    }
}