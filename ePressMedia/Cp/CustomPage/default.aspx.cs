using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Pages
{
    public partial class CustomPages : System.Web.UI.Page
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            txt_TempName.Text = txt_TempDescription.Text = "";

            toolbox1.EnableButtons("Add", true);
        }


        protected void AddButton_Click(object sender, EventArgs e)
        {
            SiteModel.CustomPage cp = new SiteModel.CustomPage();
            cp.Name = txt_TempName.Text;
            cp.Description = txt_TempDescription.Text;
            context.Add(cp);
            context.SaveChanges();

            reset_AddPanel();

            RadGrid1.DataBind();


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
                if (EPM.Business.Model.Admin.MainMenuController.ContentCategoryUsed("Page", catid))
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete this custom page because it is being used in the main menu.');", true);
                else
                {
                    SiteModel.CustomPage page = context.CustomPages.Single(c => c.CustomPageId == catid);
                    page.DeletedFg = true;
                    context.SaveChanges();
                    RadGrid1.Rebind();
                }
            }
            else if (e.CommandName.Equals("config"))
            {

                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/PageConfig.aspx?type=page&cat=" + e.CommandArgument.ToString(), listing_div, null, 1048, 900);
            }
            else if (e.CommandName.Equals("properties"))
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/CustomPage/CatProperties.aspx?id=" + e.CommandArgument.ToString(), listing_div, null);
            }
        }

        protected void OpenAccessLinqDataSource1_Updating(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            SiteModel.CustomPage pt = e.NewObject as SiteModel.CustomPage;
            //pt.Modified_dt = DateTime.Now;
            
        }

        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    GridEditManager manager = item.EditManager;
                    GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("CustomPageId") as GridTextBoxColumnEditor;
                    SettingName.TextBoxControl.Enabled = false;
                    SettingName.TextBoxControl.Width = Unit.Pixel(300);


                    GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("Name") as GridTextBoxColumnEditor;
                    SettingDescr.TextBoxControl.Width = Unit.Pixel(500);


                    GridTextBoxColumnEditor SettingValue = manager.GetColumnEditor("Description") as GridTextBoxColumnEditor;
                    SettingValue.TextBoxControl.Width = Unit.Pixel(800);



                }
            }

        }
    }
}