using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using log4net;
using System.Linq;

public partial class Cp_Classified_ClassifiedCategories : System.Web.UI.Page
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    private static readonly ILog log = LogManager.GetLogger(typeof(Cp_Classified_ClassifiedCategories));
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            //bindCategory(false);
        }


        if (!IsPostBack)
        {

            toolbox1.EnableButtons("Add", true);


        }
        toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
    }

    void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        string action = e.Item.Text.ToLower();


        if (action == "add")
        {
            toolbox1.EnableButtons("Cancel", true);
            AddPanel.Visible = true;
        }
        else if (action == "cancel")
        {
            reset_AddPanel();
        }

    }

    void reset_AddPanel()
    {
        AddPanel.Visible = false;
        txt_cls_cat_name.Text = "";

        toolbox1.EnableButtons("Add", true);
    }


    protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

        if (e.CommandName.Equals("mod"))
        {
            RadGrid1.Items[int.Parse(e.CommandArgument.ToString())].Edit = true;
            RadGrid1.Rebind();

        }
        else if (e.CommandName.Equals("del"))
        {

            int catid = int.Parse(e.CommandArgument.ToString());
            DeleteClsCat(catid);
        }
        else if (e.CommandName.Equals("properties"))
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/Classified/CatProperties.aspx?id=" + e.CommandArgument.ToString(), listing_div, null);
        }
        else if (e.CommandName.Equals("permission"))
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/Permissions.aspx?type=classified&id=" + e.CommandArgument.ToString(), listing_div, null);
        }
        else if (e.CommandName.Equals("config"))
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/PageConfig.aspx?type=classified&cat=" + e.CommandArgument.ToString(), listing_div, null, 1048, 900);
        }

    }

    void DeleteClsCat(int catid)
    {

         
            int counter = (from c in context.ClassifiedAds
                           where c.ClassifiedCategory.CatId == catid 
                           select c).Count();

            //int cnt = ForumThread.GetThreadCount(forumId, ThreadFilter.None, null, true); // BizDAL.GetBizCount("CatId=" + CatId.Value);
            if (counter > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete clssified category because it has one or more items.');", true);
            }
            else
            {
                if (EPM.Business.Model.Admin.MainMenuController.ContentCategoryUsed("Classified", catid))
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete clssified category because it is being used in the main menu.');", true);
                else
                {
                    ClassifiedModel.ClassifiedCategory classifiedCat = context.ClassifiedCategories.Single(c => c.CatId == catid);
                    context.Delete(classifiedCat);
                    context.SaveChanges();

                    RadGrid1.Rebind();
                }
            }

     
    }


    protected void AddButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txt_cls_cat_name.Text))
        {
            if (!context.ClassifiedCategories.Any(c => c.CatName.ToLower().Trim() == txt_cls_cat_name.Text.ToLower().Trim()))
            {
                ClassifiedModel.ClassifiedCategory cls = new ClassifiedModel.ClassifiedCategory();
                cls.CatName = txt_cls_cat_name.Text.Trim();
                cls.MetadataStr = "";
                cls.DetailMetadataStr = "";


                context.Add(cls);
                context.SaveChanges();


                reset_AddPanel();

                RadGrid1.Rebind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Clssified category name must be unique');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Clssified category name must be provided');", true);
        }
    }
    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
    }
    protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
    {
    }

}