using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core.Admin;
using EPM.Core.Pages;
using System.Xml;
using EPM.Business.Model.Ad;

namespace ePressMedia.Admin
{
    public partial class AdZoneControlEditor : ControlEditorBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
                ControlEditorInitializer();
                if (!IsPostBack)
                {
                    LoadAdZoneControls();
                    if (Mode == DataEntryPage.EntryViewMode.Edit)
                    {
                        btn_Delete.Visible = true;
                        lbl_title.Text = "Edit Ad Zone";


                        if (Request.QueryString["id"] != null)
                        {
                            XmlDocument xmlMaindoc = new XmlDocument();
                            xmlMaindoc.LoadXml(PageConfig.ContentXML);
                            XmlNode currentNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + Placeholder + "']/Control[@ID='" + Control_Id + "']");
                            string src_id = currentNode.Attributes["src"].Value.ToString();
                            ddl_adzonecontrols.SelectedIndex = ddl_adzonecontrols.FindItemIndexByValue(src_id);
                            LoadAdZoneFromXML();
                        }

                        else
                            CloseWindow();
                    }
                    else if (Mode == DataEntryPage.EntryViewMode.Add)
                    {
                        lbl_title.Text = "Add Ad Zone";
                    }
                }
            
        }
        protected void ddl_adzonecontrols_ItemSelected(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
           // LoadAdZoneControls();
        }
        private void LoadAdZoneFromXML()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(PageConfig.ContentXML);

            XmlNode currentNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + Placeholder + "']/Control[@ID='" + Control_Id + "']");
            txt_control_container_style.Text = currentNode.Attributes["C_Style"].Value.ToString();
            txt_control_css_class.Text = currentNode.Attributes["Class"].Value.ToString();
        }

        private void LoadAdZoneControls()
        {
            

            List<AdModel.AdZone> zones = ZoneController.GetAllZones().Where(c => c.ActiveFg == true).ToList();
            ddl_adzonecontrols.DataTextField = "ZoneDescription";
            ddl_adzonecontrols.DataValueField = "AdZoneId";
            ddl_adzonecontrols.DataSource = zones;
            ddl_adzonecontrols.DataBind();

            ddl_adzonecontrols.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("-- Select --", "0"));

        }


        protected void btn_Save_Click(object sender, EventArgs e)
        {
            // if mode is Add, the new HTML control needs to be added to the widget table also the control should be inserted in to the content xml
            // if mode is Edit,  update existing html contract in the table and update the existing xml node in the content xml
            if (ddl_adzonecontrols.SelectedIndex > 0)
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                int adzoneId = int.Parse(ddl_adzonecontrols.SelectedItem.Value.ToString());
                // Update HTML

                if (Mode == DataEntryPage.EntryViewMode.Add)
                {
                    List<string> placeholderList = EPM.Core.Admin.PageBuilder.GetPlaceholdersByMasterPagePath(context.MasterPageConfigs.Single(c => c.MasterPageId == PageConfig.MasterPageID).MasterPagePath);
                    EPM.Core.Admin.AdZoneBuilder.AddAdZone(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, PageConfig.MasterPageID, placeholderList, PageConfig.CurrentPlaceholder, adzoneId.ToString(), adzoneId.ToString(), txt_control_container_style.Text, txt_control_css_class.Text, PageConfig.CategoryID);
                }
                else if (Mode == DataEntryPage.EntryViewMode.Edit)
                    EPM.Core.Admin.AdZoneBuilder.UpdateAdZone(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, Control_Id, adzoneId.ToString(), adzoneId.ToString(), txt_control_container_style.Text, txt_control_css_class.Text, PageConfig.CategoryID);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
            }
            else
            {
                lbl_adzone_error.Visible = true;
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

            XmlNode currentNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + Placeholder + "']/Control[@ID='" + Control_Id + "']");
            string control_text_id = currentNode.Attributes["ID"].Value.ToString();
            DeleteControl(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, control_text_id, PageConfig.CategoryID);
            CloseWindow();
        }



    }
}