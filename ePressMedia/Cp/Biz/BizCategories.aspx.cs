using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;
//

public partial class Cp_Biz_BizCategories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategories();
            BindParentCategories();

            toolbox1.EnableButtons("Add", true);
        }
        toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
    }

    void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        string action = e.Item.Text.ToLower();


        if (action == "add")
        {
            reset_AddPanel();
            toolbox1.EnableButtons("Cancel", true);
            toolbox1.EnableButtons("Save", false);
            CatEditPanel.Visible = true;
            txt_CatName.Enabled = true;
            ddl_ParentCat.Enabled = true;
            ddl_ParentCat.SelectedIndex = ddl_ParentCat.Items.IndexOf(ddl_ParentCat.Items.FindByText("Root"));

        }
        else if (action == "edit")
        {
            txt_CatName.Enabled = true;
            ddl_ParentCat.Enabled = true;
            toolbox1.EnableButtons("Save", true);
            toolbox1.EnableButtons("Cancel", false);
            // Remove itself from the list
            ddl_ParentCat.Items.Remove(ddl_ParentCat.Items.FindByValue(lbl_CatId.Value));
            // Get the child categories and remove them as well
            List<BizModel.BusinessCategory> lists = BECategoryController.GetBusinessCategoriesByParentID(int.Parse(lbl_CatId.Value)).ToList();
            foreach (BizModel.BusinessCategory biz in lists)
            {
                ddl_ParentCat.Items.Remove(ddl_ParentCat.Items.FindByValue(biz.CategoryId.ToString()));
            }

        }
        else if (action == "cancel")
        {
            reset_AddPanel();

        }
        else if (action == "save")
        {
            if (string.IsNullOrEmpty(lbl_CatId.Value))
            {
                AddNewCategorry();
            }
            else
            {
                EditCategory();
            }
            BindCategories();
            BindParentCategories();
            reset_AddPanel();
        }
        else if (action == "delete")
        {
            DeleteCategory();
            BindCategories();
            BindParentCategories();
            reset_AddPanel();
        }

    }
    void AddNewCategorry()
    {
        string ParentCatId = ddl_ParentCat.SelectedValue;
        if (int.Parse(ParentCatId) > 0)
            BECategoryController.AddBusinessCategory(txt_CatName.Text, int.Parse(ParentCatId));
        else
            BECategoryController.AddBusinessCategory(txt_CatName.Text);
    }
    void EditCategory()
    {
        BECategoryController.UpdateBusinessCategory(int.Parse(lbl_CatId.Value), txt_CatName.Text, int.Parse(ddl_ParentCat.SelectedValue));
    }
    void DeleteCategory()
    {
        if ((!BECategoryController.ValidateChildExists(int.Parse(lbl_CatId.Value))) && (!BECategoryController.ValidateEntityExists(int.Parse(lbl_CatId.Value))))
        {
            BECategoryController.DeleteBusinessCategory(int.Parse(lbl_CatId.Value));
        }
    }
    void reset_AddPanel()
    {
        txt_CatName.Text = "";
        lbl_CatId.Value = "";
        ddl_ParentCat.SelectedIndex = ddl_ParentCat.Items.IndexOf(ddl_ParentCat.Items.FindByValue("0"));
        CatEditPanel.Visible = false;
        txt_CatName.Enabled = false;
        ddl_ParentCat.Enabled = false;
        toolbox1.EnableButtons("Add", true);
        BindParentCategories();


    }

    void BindCategories()
    {
        CatTreeView.DataSource = BECategoryController.GetAllBusinessCatgories();
        CatTreeView.DataBind();
    }

    void BindParentCategories()
    {

        ddl_ParentCat.DataSource = BECategoryController.GetAllBusinessCatgories();
        ddl_ParentCat.DataTextField = "CategoryName";
        ddl_ParentCat.DataValueField = "CategoryId";
        ddl_ParentCat.DataBind();
        ddl_ParentCat.Items.Insert(0, new ListItem("Root", "0"));

    }


    protected void CatTreeView_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        BizModel.BusinessCategory category =  BECategoryController.GetBusinessCatgoryByID(int.Parse(e.Node.Value));
        lbl_CatId.Value = category.CategoryId.ToString();
        txt_CatName.Text = category.CategoryName;
        
        string parentCatId = string.IsNullOrEmpty(category.ParentCategoryId.ToString()) ? "0" : category.ParentCategoryId.ToString();

        ddl_ParentCat.SelectedIndex = ddl_ParentCat.Items.IndexOf(ddl_ParentCat.Items.FindByValue(parentCatId));

        CatEditPanel.Visible = true;
        txt_CatName.Enabled = false;
        ddl_ParentCat.Enabled = false;

        toolbox1.EnableButtons("Add", true);
        toolbox1.EnableButtons("Edit", false);
        if( (category.BusinessEntities.Count(c=>c.CategoryID == category.CategoryId) == 0) && (!BECategoryController.ValidateChildExists(category.CategoryId)))
            toolbox1.EnableButtons("Delete", false);
    }

    protected void btn_BEs_Click(object sender, EventArgs e)
    {
        Session["BizCatName"] = txt_CatName.Text;
        Response.Redirect("/CP/Biz/");
    }
}