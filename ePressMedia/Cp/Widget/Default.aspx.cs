using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace Cp.Widget
{
    public partial class CpWidgetDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadArea();
                LoadControlTypes();
            }
        }

        private void LoadControlTypes()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var controlTypes = from c in context.WidgetTypes
                               where c.Widget_Type_Name.ToLower() != "image"
                               select c;

            ddl_control_type.DataValueField = "widget_type_id";
            ddl_control_type.DataTextField = "widget_type_descr";
            ddl_control_type.DataSource = controlTypes;
            ddl_control_type.DataBind();

            ddl_control_type.Items.Insert(0, new ListItem("--- Select Control Type ---", "0"));
        }

        private void LoadArea()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            var result = from c in context.ContentTypes
                         where c.Enabled == true
                         orderby c.ContentTypeName
                         select c;

            ddl_area.DataTextField = "ContentTypeName";
            ddl_area.DataValueField = "ContentTypeId";
            ddl_area.DataSource = result;
            ddl_area.DataBind();

            ListItem item = new ListItem("-- Select Module -- ","-1");
            ddl_area.Items.Insert(0, item);
        }

        protected void DdlAreaSelectedIndexChanged(object sender, EventArgs e)
        {
            ascx_add_container.Visible = false;
            html_add_container.Visible = false;
            ddl_control_type.SelectedIndex = -1;
        }

        protected void BtnAddUserControlClick(object sender, EventArgs e)
        {
            string err = ValidateControlAdd();
            if (err.Length == 0)
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

                SiteModel.Widget widjet = new SiteModel.Widget();
                widjet.ContentTypeId = int.Parse(ddl_area.SelectedValue.ToString());
                widjet.Widget_name = txt_control_name.Text;
                widjet.created_dt = DateTime.Now;
                widjet.modified_dt = DateTime.Now;
                widjet.active_fg = true;
                widjet.Widget_type_id = context.WidgetTypes.Single(c => c.Widget_Type_Name == "ASCX").Widget_Type_Id;

                String phPath = txt_control_path.Text;
                widjet.File_path = phPath;

                UserControl uc = (UserControl)(Page.LoadControl(phPath));
                Type type = uc.GetType();
                DescriptionAttribute contdescrattri = Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (contdescrattri != null)
                    widjet.Widget_descr = contdescrattri.Description.ToString();
                widjet.FrontEditable = true;

                context.Add(widjet);
                context.SaveChanges();
                int widgetID = widjet.Widget_id;
                System.Reflection.PropertyInfo[] properties = type.GetProperties();

                

                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    CategoryAttribute attribute =
                        Attribute.GetCustomAttribute(property, typeof(System.ComponentModel.CategoryAttribute)) as CategoryAttribute;

                    DescriptionAttribute descAttribute = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    DefaultValueAttribute DefAttribute = Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                    DisplayNameAttribute DisNameAttribute  = Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                    EditorBrowsableAttribute BrowAttribute = Attribute.GetCustomAttribute(property, typeof(EditorBrowsableAttribute)) as EditorBrowsableAttribute;


                    EPM.Core.Attributes.AssemblyAttribute AssemblyAttribute = Attribute.GetCustomAttribute(property, typeof(EPM.Core.Attributes.AssemblyAttribute)) as EPM.Core.Attributes.AssemblyAttribute;

                    RequiredAttribute ReqAttribute = Attribute.GetCustomAttribute(property, typeof(RequiredAttribute)) as RequiredAttribute;

                    if (attribute != null)
                    {
                        if (attribute.Category.ToString() == "EPMProperty") // This property has a StoredDataValueAttribute
                        {
                            SiteModel.Widget_detail detail = new SiteModel.Widget_detail();
                            detail.Widget_id = widgetID;
                            detail.Field_name = property.Name;// DipNameAttribute != null ? DipNameAttribute.DisplayName : property.Name;
                            detail.Field_data_type = property.PropertyType.ToString();
                            detail.Field_Display_Name = DisNameAttribute != null ? DisNameAttribute.DisplayName.ToString(): "";

                            if (descAttribute != null)
                            {
                                detail.Field_descr = descAttribute.Description.ToString();
                            }

                            if (ReqAttribute != null)
                            {
                                detail.Required_fg = true;
                            }

                            if (DefAttribute != null)
                            {
                                detail.Default_fg = true;
                                detail.Default_value = DefAttribute.Value.ToString();
                            }
                            if (AssemblyAttribute != null)
                            {
                                detail.AssemblyName = AssemblyAttribute.AssemblyName;
                                detail.ClassName = AssemblyAttribute.ClassName;
                                detail.MethodName = AssemblyAttribute.MethodName;
                            }

                            detail.Read_only_fg = !property.CanWrite;

                            context.Add(detail);
                            context.SaveChanges();
                        }
                    }
                }

                this.gv_widjet_list.Rebind();

                txt_control_name.Text = "";
                txt_control_path.Text = "";

            }
            else
            {
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, err);
            }
        }

        private string ValidateControlAdd()
        {
            string err_message = "";
            if (!FileHelper.FileExists(Server.MapPath(txt_control_path.Text)))
            {
                err_message = "Control does not exist. Please varify the control path.";
            }
            else
            {
                if (ddl_area.SelectedIndex > 0)
                {
                    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                    var result = from c in context.Widgets
                                 where (c.Widget_name == txt_control_name.Text || c.File_path == txt_control_path.Text.Trim())// && c.ContentTypeId == int.Parse(ddl_area.SelectedValue.ToString()) 
                                 select c;
                    if (result.Count() > 0)
                    {
                            err_message = "Already registared control.";
                    }
                }
                else
                {
                    err_message = "Module is not selected.";
                }
            }

            return err_message;
        }

        protected void gv_widjet_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void hideAllAddContainer()
        {
            html_add_container.Visible = false;
            ascx_add_container.Visible = false;
        }

        protected void gv_widjet_list_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            btn_deactivate.Visible = false;
            btn_activate.Visible = false;
            btn_edit.Visible = false;
            btn_delete.Visible = false;
            btn_update.Visible = false;

            hideAllAddContainer();

            if (e.CommandName == "RowClick" && e.Item is Telerik.Web.UI.GridDataItem)
            {
                if (e.Item.CanExpand)
                {
                    e.Item.Selected = true;
                    int id = int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Widget_id"].ToString());
                    ActivateButtonUpdate(id);


                }
            }
        }

        private void ActivateButtonUpdate(int id)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var result = (from c in context.Widgets
                          where c.Widget_id == id
                          select c).Single();

            if (result.active_fg)
            {
                btn_deactivate.Visible = true;
                btn_activate.Visible = false;
            }
            else
            {
                btn_deactivate.Visible = false;
                btn_activate.Visible = true;
            }


            if (result.WidgetType.Widget_Type_Name.ToLower() == "html")
                btn_edit.Visible = true;
            if (result.WidgetType.Widget_Type_Name.ToLower() == "ascx")
            {
                btn_update.Visible = true;
            }
        }

        protected void gv_widjet_list_DataBound(object sender, EventArgs e)
        {
            btn_deactivate.Visible = false;
            btn_activate.Visible = false;
            btn_edit.Visible = false;
            btn_delete.Visible = false;


        }

        protected void btn_deactivate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(gv_widjet_list.SelectedValue.ToString());
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var result = (from c in context.Widgets
                          where c.Widget_id == id
                          select c).Single();
            result.active_fg = false;
            context.SaveChanges();
            this.gv_widjet_list.Rebind();
        }

        protected void btn_activate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(gv_widjet_list.SelectedValue.ToString());
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var result = (from c in context.Widgets
                          where c.Widget_id == id
                          select c).Single();
            result.active_fg = true;
            context.SaveChanges();
            this.gv_widjet_list.Rebind();
        }

        protected void ddl_control_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_add_html_control_Click(object sender, EventArgs e)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            SiteModel.Widget widget = new SiteModel.Widget();
            widget.ContentTypeId = int.Parse(ddl_area.SelectedValue.ToString());
            widget.Widget_name = txt_html_name.Text;
            widget.Widget_descr = txt_html_descr.Text;
            widget.created_dt = DateTime.Now;
            widget.modified_dt = DateTime.Now;
            widget.active_fg = true;
            widget.Widget_type_id = context.WidgetTypes.Single(c => c.Widget_Type_Name == "HTML").Widget_Type_Id;
            widget.FrontEditable = !chk_html_hide_edit.Checked;

            widget.Widget_Data = html_editor_text.Content;
            context.Add(widget);
            context.SaveChanges();

            this.gv_widjet_list.Rebind();


            txt_html_name.Text = "";
            txt_html_descr.Text = "";
            html_editor_text.Content = "";
            hideAllAddContainer();
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int id = int.Parse(gv_widjet_list.SelectedValue.ToString());
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            SiteModel.Widget widget = (from c in context.Widgets
                                       where c.Widget_id == id
                                       select c).Single();
            if (widget.WidgetType.Widget_Type_Name.ToLower() == "html")
            {
                html_add_container.Visible = true;
                txt_html_name.Text = widget.Widget_name;
                txt_html_descr.Text = widget.Widget_descr;
                html_editor_text.Content = widget.Widget_Data;
                btn_add_html_control.Visible = false;
                btn_update_html_control.Visible = true;
                ascx_add_container.Visible = false;
                chk_html_hide_edit.Checked = !widget.FrontEditable;

            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            int id = int.Parse(gv_widjet_list.SelectedValue.ToString());
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            SiteModel.Widget widget = (from c in context.Widgets
                                       where c.Widget_id == id
                                       select c).Single();


            UpdateUserControlAttributes(id, widget.File_path);
        }

        void UpdateUserControlAttributes(int id, string phPath)
        {
            UserControl uc = (UserControl)(Page.LoadControl(phPath));
            Type type = uc.GetType();
            System.Reflection.PropertyInfo[] properties = type.GetProperties();

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<SiteModel.Widget_detail> details = (from c in context.Widget_details
                                                     where c.Widget_id == id
                                                         select c).ToList();

            context.Delete(details);
            context.SaveChanges();
            

            //IDictionary<string, string> prop_param = new Dictionary<string, string>();

            foreach (System.Reflection.PropertyInfo property in properties)
            {
                CategoryAttribute attribute =
                    Attribute.GetCustomAttribute(property, typeof(System.ComponentModel.CategoryAttribute)) as CategoryAttribute;

                DescriptionAttribute descAttribute = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute;
                DefaultValueAttribute DefAttribute = Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
                DisplayNameAttribute DisNameAttribute = Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                RequiredAttribute ReqAttribute = Attribute.GetCustomAttribute(property, typeof(RequiredAttribute)) as RequiredAttribute;

                EPM.Core.Attributes.AssemblyAttribute AssemblyAttribute = Attribute.GetCustomAttribute(property, typeof(EPM.Core.Attributes.AssemblyAttribute)) as EPM.Core.Attributes.AssemblyAttribute;

                if (attribute != null)
                {
                    if (attribute.Category.ToString() == "EPMProperty") // This property has a StoredDataValueAttribute
                    {
                        SiteModel.Widget_detail detail = new SiteModel.Widget_detail();
                        detail.Widget_id = id;
                        detail.Field_name = property.Name;
                        detail.Field_data_type = property.PropertyType.ToString();
                        detail.Field_Display_Name = DisNameAttribute != null ? DisNameAttribute.DisplayName.ToString() : "";

                        if (descAttribute != null)
                        {
                            detail.Field_descr = descAttribute.Description.ToString();
                        }

                        if (ReqAttribute != null)
                        {
                            detail.Required_fg = true;
                        }

                        if (DefAttribute != null)
                        {
                            detail.Default_fg = true;
                            detail.Default_value = DefAttribute.Value.ToString();
                        }

                        if (AssemblyAttribute != null)
                        {
                            detail.AssemblyName = AssemblyAttribute.AssemblyName;
                            detail.ClassName = AssemblyAttribute.ClassName;
                            detail.MethodName = AssemblyAttribute.MethodName;
                        }

                        detail.Read_only_fg = !property.CanWrite;

                        context.Add(detail);
                        context.SaveChanges();
                    }
                }
            }

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            ascx_add_container.Visible = false;
            html_add_container.Visible = false;
            if (ddl_control_type.SelectedIndex > 0)
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

                string control_item = context.WidgetTypes.Single(c => c.Widget_Type_Descr.ToLower() == ddl_control_type.SelectedItem.ToString().ToLower()).Widget_Type_Name.ToLower();

                if (control_item == "ascx")
                {
                    ascx_add_container.Visible = true;
                }
                else if (control_item == "html")
                    html_add_container.Visible = true;
            }
        }

        protected void btn_update_html_control_Click(object sender, EventArgs e)
        {
            int id = int.Parse(gv_widjet_list.SelectedValue.ToString());
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            SiteModel.Widget widget = (from c in context.Widgets
                                       where c.Widget_id == id
                                       select c).Single();

            widget.Widget_name = txt_html_name.Text;
            widget.Widget_descr = txt_html_descr.Text;
            widget.Widget_Data = html_editor_text.Content;
            widget.modified_dt = DateTime.Now;
            widget.FrontEditable = !chk_html_hide_edit.Checked;

            context.SaveChanges();

            gv_widjet_list.SelectedIndexes.Clear();


            txt_html_name.Text = "";
            txt_html_descr.Text = "";
            html_editor_text.Content = "";
            hideAllAddContainer();
            EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Control has been successfully updated.");



        }

        protected void gv_widjet_list_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            
            if (e.Item is Telerik.Web.UI.GridDataItem  && e.Item.OwnerTableView.Name== "ParentGrid")
            {
                HtmlImage image = e.Item.FindControl("control_img") as HtmlImage;

                if (((SiteModel.Widget)(e.Item.DataItem)).WidgetType.Widget_Type_Name.ToLower() == "html")
                {
                    image.Src = "/img/WidgetHTML.png";
                }
                else
                {
                    if (((SiteModel.Widget)(e.Item.DataItem)).File_path != null)
                    {
                        string image_url = ((SiteModel.Widget)(e.Item.DataItem)).File_path.ToString().Replace("ascx", "png").Substring(1);


                        if (System.IO.File.Exists(Server.MapPath(image_url)))
                        {
                            image.Src = image_url;
                        }
                        else
                        {
                            image.Src = "/img/WidgetNoImage.png";
                        }
                    }
                    else
                    {
                        image.Src = "/img/WidgetNoImage.png";
                    }
                }




                image.Attributes["style"] = "width:70px";
            }
            else if (e.Item is Telerik.Web.UI.GridDataItem && e.Item.OwnerTableView.Name == "ChildGrid")
            {

                if (e.Item.Controls.Count == 0)
                {
                    GridTableView detailTable = (GridTableView)e.Item.OwnerTableView;
                    detailTable.Visible = false;
                }
            }
        }
    }
}
