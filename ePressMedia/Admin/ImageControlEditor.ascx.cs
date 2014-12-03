using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ePressMedia.Admin;
using EPM.Core;
using EPM.Core.Admin;
using EPM.Core.Pages;
using System.Xml;
using EPM.Business.Model.Admin;


namespace ePressMedia.Page
{
    public partial class ImageControlEditor : ControlEditorBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ControlEditorInitializer();
            if (!IsPostBack)
            {
          

                if (Mode == DataEntryPage.EntryViewMode.Edit)
                {
                    btn_Delete.Visible = true;
                    lbl_title.Text = "Edit Image";
                    LoadImageData();
                }
                else if (Mode == DataEntryPage.EntryViewMode.Add)
                {
                    lbl_title.Text = "Add a New Image";
                }
            }

        }


        void LoadImageData()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ePressMedia.Admin.PageConfig.ContentXML);

            XmlNode selectedNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + Placeholder + "']/Control[@ID='" + Control_Id + "']");
            txt_image_name.Text = XmlHelper.GetAttributeValue(selectedNode, "Name");
            txt_image_path.Text = XmlHelper.GetAttributeValue(selectedNode, "src");
            txt_control_container_style.Text = XmlHelper.GetAttributeValue(selectedNode, "C_Style");
            txt_image_style.Text = XmlHelper.GetAttributeValue(selectedNode, "Style");
            txt_link_href.Text = XmlHelper.GetAttributeValue(selectedNode, "href");
            txt_link_target.Text = XmlHelper.GetAttributeValue(selectedNode, "target");
            lbl_control_id.Text = XmlHelper.GetAttributeValue(selectedNode, "ID");
            txt_control_container_class.Text = XmlHelper.GetAttributeValue(selectedNode, "class");
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

            SaveImage();
            CloseWindow();
        }

        void SaveImage()
        {
            if (Mode == DataEntryPage.EntryViewMode.Edit)
                UpdateImage();
            else if (Mode == DataEntryPage.EntryViewMode.Add)
                AddImage();

        }
        void UpdateImage()
        {

            EPM.Core.Admin.ImageBuilder.UpdateImage(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, Control_Id, txt_image_name.Text, txt_image_path.Text,
                txt_link_href.Text, txt_link_target.Text, txt_control_container_style.Text, txt_image_style.Text, txt_control_container_class.Text, PageConfig.CategoryID);
        }
        void AddImage()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<string> placeholderList = PageBuilder.GetPlaceholdersByMasterPagePath(context.MasterPageConfigs.Single(c => c.MasterPageId == PageConfig.MasterPageID).MasterPagePath);
            EPM.Core.Admin.ImageBuilder.AddImage(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, PageConfig.MasterPageID, placeholderList,PageConfig.CurrentPlaceholder,txt_image_name.Text,
            txt_image_path.Text,txt_link_href.Text,txt_link_target.Text,txt_control_container_style.Text,txt_image_style.Text,txt_control_container_class.Text,PageConfig.CategoryID);
        }

        
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {

            CloseWindow();
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            DeleteControl(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, Control_Id, PageConfig.CategoryID);
            CloseWindow();
        }
    }
}