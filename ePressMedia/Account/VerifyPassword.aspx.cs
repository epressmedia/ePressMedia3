using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_VerifyPassword : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //    Master.SetBreadCrumb("<span> &gt; 계정관리 &gt; 비밀번호확인</span>");
    }

    protected void VerifyButton_Click(object sender, EventArgs e)
    {
        if (Membership.ValidateUser(Page.User.Identity.Name, Password.Text))
        {
            Session["PW_CHK"] = "1";
            Response.Redirect("ManageAccount.aspx");
        }

        Message.Visible = true;
        Password.Focus();
    }
}