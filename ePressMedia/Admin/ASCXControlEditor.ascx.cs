using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using EPM.Core;
using EPM.Core.Admin;
using EPM.Core.Pages;
using System.Reflection;
using ePressMedia.Admin;

namespace ePressMedia.Pages
{
    public partial class ASCXControlEditor : ControlEditorBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ControlEditorInitializer();
                if (Mode == DataEntryPage.EntryViewMode.Edit)
                {
                    LoadASCXData();
                    lbl_title.Text = "Edit Widget";
                    btn_Delete.Visible = true;
                }
                else if (Mode == DataEntryPage.EntryViewMode.Add)
                    lbl_title.Text = "Add a New Widget";
                LoadASCXEditParameters();
            }
            catch
            {
                CloseWindow();
            }
        }




        void LoadASCXData()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ePressMedia.Admin.PageConfig.ContentXML);
            string placeholder = Request.QueryString["placeholder"].ToString();
            SelectedNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + placeholder + "']/Control[@ID='" + Request.QueryString["id"].ToString() + "']");
            //lbl_title.Text = Request.QueryString["id"].ToString();
        }

        private void LoadASCXEditParameters()
        {
            string src = "";
            if (Mode == DataEntryPage.EntryViewMode.Edit)
            {
                
                src = XmlHelper.GetAttributeValue(SelectedNode, "src"); // src is the ascx path
                 txt_control_container_style.Text = XmlHelper.GetAttributeValue(SelectedNode, "C_Style");
                 txt_control_css_class.Text = XmlHelper.GetAttributeValue(SelectedNode, "Class");
            }
            else if (Mode == DataEntryPage.EntryViewMode.Add)
            {
                src = Request.QueryString["src"].ToString(); // in case of Add, src is WidgetID
            }


            ctrl_params.DataSource = ASCXBuilder.LoadASCXParameters(Mode, src, SelectedNode);
            ctrl_params.DataBind();



        }
        protected void ctrl_params_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item ||
              e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ASCXBuilder.ASCXParameterBinding(e);
                }
            }
            catch
            {
                CloseWindow();
            }

        }

        void AddASCX()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
           List<string> placeholderList = EPM.Core.Admin.PageBuilder.GetPlaceholdersByMasterPagePath(context.MasterPageConfigs.Single(c=>c.MasterPageId == PageConfig.MasterPageID).MasterPagePath);
            int widget_id = int.Parse( Request.QueryString["src"].ToString());
            ASCXBuilder.AddASCX(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, PageConfig.MasterPageID, placeholderList, ePressMedia.Admin.PageConfig.CurrentPlaceholder, widget_id, txt_control_container_style.Text, txt_control_css_class.Text, ctrl_params, ePressMedia.Admin.PageConfig.CategoryID);

        }
        void UpdateASCX()
        {
            ASCXBuilder.UpdateASCX(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, Control_Id, txt_control_container_style.Text, txt_control_css_class.Text, ctrl_params, PageConfig.CategoryID);
        }
        void DeleteASCX()
        {
            DeleteControl(PageConfig.ContentXML, PageConfig.ContentType, PageConfig.UseForType, Placeholder, Control_Id, PageConfig.CategoryID);
        }

        
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (Mode == DataEntryPage.EntryViewMode.Edit)
                UpdateASCX();
            else if (Mode == DataEntryPage.EntryViewMode.Add)
                AddASCX();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
            
        }


        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            DeleteASCX();
            CloseWindow();
        }


    }
}