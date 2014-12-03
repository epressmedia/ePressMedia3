using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cp_User_AddUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList groups = CreateUserWizard1.CreateUserStep.
                ContentTemplateContainer.FindControl("GroupList")
                as DropDownList;

            groups.DataSource = Roles.GetAllRoles();
            groups.DataBind();
            groups.Items.Insert(0, "--그룹을 선택하세요--");
        }
    }

    protected void CreateUserWizard1_CreatedUser1(object sender, EventArgs e)
    {
        TextBox userName = CreateUserWizard1.CreateUserStep.
            ContentTemplateContainer.FindControl("UserName") as TextBox;

        Label tempPass = CreateUserWizard1.CompleteStep.
            ContentTemplateContainer.FindControl("TempPassword") as Label;

        tempPass.Text = CreateUserWizard1.Password;

        DropDownList groups = CreateUserWizard1.CreateUserStep.
            ContentTemplateContainer.FindControl("GroupList")
            as DropDownList;

        MembershipUser user = Membership.GetUser(userName.Text);
        Roles.AddUserToRole(userName.Text, groups.SelectedValue);

        user.Comment = "First";
        Membership.UpdateUser(user);
    }

    protected void CreateUserWizard1_ActiveStepChanged(object sender, EventArgs e)
    {
    }
    protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
    {
    }
    protected void ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("Users.aspx");
    }

}