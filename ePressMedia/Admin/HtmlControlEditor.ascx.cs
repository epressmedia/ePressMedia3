using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ePressMedia.Admin;
using EPM.Core.Pages;
using EPM.Core.Admin;
using System.Xml;
using EPM.Business.Model.Admin;
using EPM.Core;

namespace ePressMedia.Pages
{
    public partial class HtmlControlEditor : ControlEditorBase
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
       
                ControlEditorInitializer();
                if (!IsPostBack)
                {
                LoadHTMLControls();
                if (Mode == DataEntryPage.EntryViewMode.Edit)
                {
                    btn_Delete.Visible = true;
                    lbl_title.Text = "Edit a HTML";


                    if (Request.QueryString["id"] != null)
                    {
                        SelectHTML(int.Parse(Request.QueryString["id"].ToString()));
                        LoadHTMLFromXML();
                    }

                    else
                        CloseWindow();
                }
                else if (Mode == DataEntryPage.EntryViewMode.Add)
                {
                    lbl_title.Text = "Add a New HTML";
                }
            }
        }
        private void LoadHTMLFromXML()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(PageConfig.ContentXML);

            XmlNode currentNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + Placeholder + "']/Control[@src='" + Control_Id + "']");
            txt_control_container_style.Text = XmlHelper.GetAttributeValue(currentNode, "C_Style");
            txt_control_css_class.Text = XmlHelper.GetAttributeValue(currentNode, "Class");
        }

        private void LoadHTMLControls()
        {
            List<SiteModel.Widget> htmls = ControlEditorController.GetControlsByWidgetType("html").ToList();
            ddl_htmlcontrols.DataTextField = "widget_name";
            ddl_htmlcontrols.DataValueField = "widget_id";
            ddl_htmlcontrols.DataSource = htmls;
            ddl_htmlcontrols.DataBind();

            ddl_htmlcontrols.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("-- Select --", "0"));

        }

        void SelectHTML(int HtmlID)
        {
            ddl_htmlcontrols.SelectedIndex = ddl_htmlcontrols.FindItemIndexByValue(HtmlID.ToString());
            LoadHTMLContent();
        }
        

        protected void ddl_htmlcontrols_ItemSelected(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadHTMLContent();
        }

        private void LoadHTMLContent()
        {
            if (ddl_htmlcontrols.SelectedIndex >= 0)
            {

                SiteModel.Widget widget = ControlEditorController.GetContorlByID(int.Parse(ddl_htmlcontrols.SelectedItem.Value.ToString()));
                html_editor.Content = widget.Widget_Data;
                
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            // if mode is Add, the new HTML control needs to be added to the widget table also the control should be inserted in to the content xml
            // if mode is Edit,  update existing html contract in the table and update the existing xml node in the content xml
            if (ddl_htmlcontrols.SelectedIndex > 0)
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                int widget_id = int.Parse(ddl_htmlcontrols.SelectedItem.Value.ToString());
                // Update HTML
                ControlEditorController.UpdateHTMLContent(widget_id, html_editor.Content);

                if (Mode == DataEntryPage.EntryViewMode.Add)
                {
                    List<string> placeholderList = EPM.Core.Admin.PageBuilder.GetPlaceholdersByMasterPagePath(context.MasterPageConfigs.Single(c => c.MasterPageId == PageConfig.MasterPageID).MasterPagePath);
                    EPM.Core.Admin.HTMLBuilder.AddHTML(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, PageConfig.MasterPageID, placeholderList, PageConfig.CurrentPlaceholder, widget_id, txt_control_container_style.Text, txt_control_css_class.Text, PageConfig.CategoryID);
                }
                else if (Mode == DataEntryPage.EntryViewMode.Edit)
                    EPM.Core.Admin.HTMLBuilder.EditHtml(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, Control_Id, txt_control_container_style.Text, txt_control_css_class.Text, PageConfig.CategoryID, widget_id);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
            }
        }


        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(PageConfig.ContentXML);

            XmlNode currentNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + Placeholder + "']/Control[@src='" + Control_Id + "']");
            string control_text_id = currentNode.Attributes["ID"].Value.ToString();
            DeleteControl(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, control_text_id, PageConfig.CategoryID);
            CloseWindow();
        }






        


    }
}