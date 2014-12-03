using System;
using System.Web;
using System.Web.Security;
using System.Web.Profile;

namespace Cp.User
{
    public partial class CpUserUserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        void BindData()
        {
            Guid guid = new Guid(Request.QueryString["id"]);

            MembershipUser user = Membership.GetUser(guid);

            UserName.Text = user.UserName;
            Email.Text = user.Email;
            //UserRole.Text = Roles.GetRolesForUser(user.UserName)[0];

            RegDate.Text = user.CreationDate.ToString();
            LastLogin.Text = user.LastLoginDate.ToString();

            AccStat.Text = user.IsApproved ? "정상" : "사용정지";
            EnableAcc.Text = user.IsApproved ? "계정 사용정지 " : "계정 사용 ";

            RoleList.DataSource = Roles.GetAllRoles();
            RoleList.DataBind();
            RoleList.SelectedValue = Roles.GetRolesForUser(user.UserName)[0];

            BindProfile(user.UserName);
        }

        void BindProfile(string userName)
        {


            MembershipUser user = Membership.GetUser(userName);

            var profile = ProfileBase.Create(userName);

            
            

            EmailAddress.Text = user.Email.ToLower().Trim();
            FirstName.Text = profile.GetPropertyValue("FirstName").ToString();
            LastName.Text = profile.GetPropertyValue("LastName").ToString();

            Phone.Text = profile.GetPropertyValue("Phone").ToString();
            PostalCode.Text = profile.GetPropertyValue("Zip").ToString().ToUpper();
            Address1.Text = profile.GetPropertyValue("Address1").ToString();
            Address2.Text = profile.GetPropertyValue("Address2").ToString();

            Province.Text = profile.GetPropertyValue("Province").ToString();

            City.Text = profile.GetPropertyValue("City").ToString();
            

            if (!string.IsNullOrEmpty(profile.GetPropertyValue("UserComment").ToString()))
                UserComment.Text = profile.GetPropertyValue("UserComment").ToString();

        }
     
        protected void SetRole_Click(object sender, EventArgs e)
        {
            Roles.RemoveUserFromRole(UserName.Text, Roles.GetRolesForUser(UserName.Text)[0]);
            Roles.AddUserToRole(UserName.Text, RoleList.SelectedValue);

            EPM.Legacy.Common.Utility.RegisterJsAlert(this, "사용자 그룹이 변경되었습니다.");
        }
     
        protected void EnableAcc_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser(UserName.Text);
            user.IsApproved = !user.IsApproved;
         
            Membership.UpdateUser(user);

            BindData();
        }

        protected void ResetPwd_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser(UserName.Text);
            if (user.IsLockedOut)
                user.UnlockUser();

            string pwd = user.ResetPassword();

            //var profile = HttpContext.Current.Profile;


            var profile = ProfileBase.Create(UserName.Text);
            profile.SetPropertyValue("PwdExpired", true);
            //ProfileCommon profile = Profile.GetProfile(user.UserName);
            //profile.PwdExpired = true;
            profile.Save();

            NewPass.Text = string.Format("새 임시 비밀번호는 {0} 입니다.", pwd);
            NewPass.Visible = true;
        }
    }
}