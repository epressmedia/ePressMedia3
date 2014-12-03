using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using EPM.Business.Model.Admin;

namespace ePressMedia.Cp.Pages
{
    public partial class MasterPages : System.Web.UI.Page
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMasterFiles();
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
            ddl_master.SelectedIndex = -1;
               txt_TempDescription.Text = "";

            toolbox1.EnableButtons("Add", true);
        }
        private void LoadMasterFiles()
        {
            IDictionary<string, string> masterList = new Dictionary<string, string>();
            masterList =  EPM.Core.FileHelper.GetFiles(Server, "~\\Master", "master", true);

            ddl_master.DataTextField = "key";
            ddl_master.DataValueField = "value";
            ddl_master.DataSource = masterList;
            ddl_master.DataBind();

            ddl_master.Items.Insert(0, new ListItem("--- Select Master Page ---", "0"));

        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (ddl_master.SelectedIndex > 0)
            {
                var master_exists = context.MasterPageConfigs.Where(c => c.Name == ddl_master.SelectedItem.Text && c.Description == txt_TempDescription.Text && c.DeletedFg == false);

                if (master_exists.Count() == 0)
                {
                    SiteModel.MasterPageConfig master = new SiteModel.MasterPageConfig();
                    master.Name = ddl_master.SelectedItem.Text;
                    master.Description = txt_TempDescription.Text;
                    master.MasterPagePath = "~/Master/" + ddl_master.SelectedItem.Text;
                    context.Add(master);
                    context.SaveChanges();

                    RadGrid1.DataBind();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Master Page already exists.');", true);
                }



                txt_TempDescription.Text = "";
                ddl_master.SelectedIndex = -1;
            }

        }
        void DeleteMasterPage(int catid)
        {
            SiteModel.MasterPageConfig page = context.MasterPageConfigs.Single(c => c.MasterPageId == catid);
            if (MasterPageContoller.CheckBeingUsed(page.MasterPagePath.Replace("\\","/"), page.Description))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('This master page cannot be deleted because it is being used.');", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('delete committed');", true);
                MasterPageContoller.DeleteMasterPage(catid);
            }
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
               
                DeleteMasterPage(int.Parse(e.CommandArgument.ToString()));
                RadGrid1.Rebind();
            }
            else if (e.CommandName.Equals("config"))
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/PageConfig.aspx?type=master&cat=" + e.CommandArgument.ToString(), listing_div, null, 1048, 900);
            }
        }

        protected void OpenAccessLinqDataSource1_Updating(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            //SiteModel.PageTemplate pt = e.NewObject as SiteModel.PageTemplate;
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
                    GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("MasterPageId") as GridTextBoxColumnEditor;
                    SettingName.TextBoxControl.Enabled = false;
                    SettingName.TextBoxControl.Width = Unit.Pixel(300);


                    GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("Name") as GridTextBoxColumnEditor;
                    SettingDescr.TextBoxControl.Width = Unit.Pixel(500);
                    SettingDescr.TextBoxControl.Enabled = false;
                    


                    GridTextBoxColumnEditor SettingValue = manager.GetColumnEditor("Description") as GridTextBoxColumnEditor;
                    SettingValue.TextBoxControl.Width = Unit.Pixel(800);



                }
            }
        }
    }
}