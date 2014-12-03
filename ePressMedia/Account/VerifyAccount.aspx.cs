using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Profile;

public partial class Account_VerifyAccount : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        //Master.SetBreadCrumb("<span> &gt; 계정관리 &gt; 회원등록확인</span>");

        bool res = false;

        if (!string.IsNullOrEmpty(Request.QueryString["uuid"]))
        {
            try
            {
                Guid guid = new Guid(Request.QueryString["uuid"]);
                MembershipUser user = Membership.GetUser(guid);

                if (user != null)
                {
//                    ProfileCommon profile = Profile.GetProfile(user.UserName);


                    var profile = ProfileBase.Create(user.UserName);
                    profile.SetPropertyValue("Verified", true);

                    //profile.Verified = true;
                    profile.Save();

                    res = true;
                }
            }
            catch
            {
            }
        }

        ErrPnl.Visible = !res;
        SuccPnl.Visible = res;

    }
}