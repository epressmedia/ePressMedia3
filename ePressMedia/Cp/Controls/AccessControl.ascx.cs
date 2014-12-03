using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using EPM.Legacy.Security;

public partial class Cp_Controls_AccessControl : System.Web.UI.UserControl
{
    public int ResourceType
    {
        //get { return int.Parse(ResType.Value); }
        set { ResType.Value = value.ToString(); }
    }

    public int ResourceId
    {
        //get { return int.Parse(ResType.Value); }
        set { ResId.Value = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            LoadUserGroupData();



            //if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            //    ResId.Value = Request.QueryString["id"];

            //ResType.Value = ((int)ResourceType.Article).ToString();

            bindData();
        }
    }

    void LoadUserGroupData()
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        var userroles = (from ur in context.UserRoles
                         select new
                         {
                             RoleName = ur.RoleName,
                             RoleValue = ur.RoleName
                         })
                        .Except(from u in context.AccessControlLists
                                where u.ResType == int.Parse(ResType.Value) && u.ResId == int.Parse(ResId.Value)
                                select new
                                {
                                    RoleName = u.PrincipalName,
                                    RoleValue = u.PrincipalName
                                });



        UserGroupList.DataValueField = "RoleName";
        UserGroupList.DataTextField = "RoleValue";
        UserGroupList.DataSource = userroles;
        UserGroupList.DataBind();

        UserGroupList.Items.Insert(0, new ListItem("-- Select User Role --", "00000"));
    }

    void bindData()
    {
        DataRepeater.DataSource =
            AccessControl.SelectAccessControlList((ResourceType)(int.Parse(ResType.Value)),
                    int.Parse(ResId.Value));
        DataRepeater.DataBind();
    }


    protected void AddButton_Click(object sender, EventArgs e)
    {

        if (UserGroupList.SelectedIndex > 0)
        {
            AccessControl.InsertAccessControl(UserGroupList.SelectedItem.Text,
                (ResourceType)(int.Parse(ResType.Value)), int.Parse(ResId.Value));

            bindData();
            LoadUserGroupData();
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in DataRepeater.Items)
        {
            int id = int.Parse((item.FindControl("AcId") as HiddenField).Value);

            AccessControl.UpdatePermissions(id,
                (item.FindControl("HasFullControl") as CheckBox).Checked,
                (item.FindControl("CanList") as CheckBox).Checked,
                (item.FindControl("CanRead") as CheckBox).Checked,
                (item.FindControl("CanWrite") as CheckBox).Checked,
                (item.FindControl("CanComment") as CheckBox).Checked,
                (item.FindControl("CanModify") as CheckBox).Checked,
                (item.FindControl("CanDelete") as CheckBox).Checked, false);
        }

        EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "설정이 저장되었습니다.");
    }

    protected void DelButton_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in DataRepeater.Items)
        {
            if ((item.FindControl("ChkSelected") as CheckBox).Checked)
                AccessControl.DeletePermission(
                    int.Parse((item.FindControl("AcId") as HiddenField).Value));
        }

        bindData();
        LoadUserGroupData();
    }

}