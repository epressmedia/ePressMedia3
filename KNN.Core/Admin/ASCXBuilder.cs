using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EPM.Core;
using EPM.Core.Pages;
using EPM.Data.Model;
using System.Reflection;

namespace EPM.Core.Admin
{
    public class ASCXBuilder : PageBuilder
    {
        public static void ASCXParameterBinding(RepeaterItemEventArgs e)
        {
            SiteModel.Widget_detail detail = e.Item.DataItem as SiteModel.Widget_detail;

            // Set the param display name
            Label param_display_name = e.Item.FindControl("param_display_name") as Label;
            // Label p_type = e.Item.FindControl("type") as Label;

            param_display_name.Text = (string.IsNullOrEmpty(detail.Field_Display_Name) ? detail.Field_name : detail.Field_Display_Name) + (detail.Required_fg ? "*" : "");

            if (detail.Field_data_type == "System.Boolean")
            {
                RadioButton trueButton = e.Item.FindControl("param_value_true") as RadioButton;
                trueButton.Visible = true;

                RadioButton falseButton = e.Item.FindControl("param_value_false") as RadioButton;
                falseButton.Visible = true;

                if (detail.Default_value.ToLower() == "true")
                    trueButton.Checked = true;
                else
                    falseButton.Checked = true;
            }
            else if (detail.Field_data_type.Contains("System.Int"))
            {
                TextBox param_value_int = e.Item.FindControl("param_value_int") as TextBox;
                RegularExpressionValidator param_value_int_datatype = e.Item.FindControl("param_value_int_datatype") as RegularExpressionValidator;
                RequiredFieldValidator param_value_int_required = e.Item.FindControl("param_value_int_required") as RequiredFieldValidator;

                param_value_int.Visible = true;


                param_value_int_datatype.Visible = true;
                param_value_int_required.Visible = true;
            }
            else if (detail.Field_data_type.Contains("+") || detail.Field_data_type.Contains("Telerik.Web.UI."))
            {

                Type t = null;
                if (detail.Field_data_type.Contains("Telerik.Web.UI."))
                {
                    var assembly = System.Reflection.Assembly.Load("Telerik.Web.UI");
                    t = assembly.GetType(detail.Field_data_type);
                }
                else
                {
                    var assembly = System.Reflection.Assembly.Load("ePressMedia");
                    t = assembly.GetType(detail.Field_data_type);
                }
                string[] items = t.GetEnumNames();

                DropDownList param_value_list = e.Item.FindControl("param_value_list") as DropDownList;
                param_value_list.Visible = true;
                foreach (string item in items)
                {
                    param_value_list.Items.Add(new ListItem(item, item));
                }

                param_value_list.SelectedIndex = param_value_list.Items.IndexOf(param_value_list.Items.FindByText(detail.Default_value));
            }
            else if (!string.IsNullOrEmpty(detail.AssemblyName))
            {
                object result = null;
                Assembly assembly = Assembly.Load(detail.AssemblyName);

                Type type = assembly.GetType(detail.ClassName);
                if (type != null)
                {
                    MethodInfo methodInfo = type.GetMethod(detail.MethodName);
                    if (methodInfo != null)
                    {

                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        result = methodInfo.Invoke(null, null);

                    }
                }

                DropDownList param_value_list = e.Item.FindControl("param_value_list") as DropDownList;
                param_value_list.Visible = true;

                param_value_list.DataSource = result;
                param_value_list.DataTextField = "Key";
                param_value_list.DataValueField = "Key";
                param_value_list.DataBind();
                param_value_list.Items.Insert(0, new ListItem("-- select --", ""));
                if (detail.Required_fg)
                {
                    RequiredFieldValidator param_value_ddl_required = e.Item.FindControl("param_value_ddl_required") as RequiredFieldValidator;
                    param_value_ddl_required.Enabled = true;

                }


                param_value_list.SelectedIndex = param_value_list.Items.IndexOf(param_value_list.Items.FindByText(detail.Default_value));
            }
            else
            {
                RequiredFieldValidator param_required = e.Item.FindControl("param_required") as RequiredFieldValidator;

                TextBox param_value = e.Item.FindControl("param_value") as TextBox;
                param_required.Visible = true;
                param_value.Visible = true;
            }

        }

        public static List<SiteModel.Widget_detail> LoadASCXParameters(DataEntryPage.EntryViewMode Mode, string src, XmlNode SelectedNode)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            List<SiteModel.Widget_detail> paramlist = null;
            if (Mode == DataEntryPage.EntryViewMode.Edit)
            {
                //                src = XmlHelper.GetAttributeValue(SelectedNode, "src");
                paramlist = (from c in context.Widget_details
                             where c.Widget.File_path == src
                             select c).ToList();
            }
            else if (Mode == DataEntryPage.EntryViewMode.Add)
            {
                // if add, src is the widget id
                //              src = Request.QueryString["src"].ToString();
                paramlist = (from c in context.Widget_details
                             where c.Widget.Widget_id == int.Parse(src)
                             select c).ToList();
            }

            // If Edit, Load the value from the XML
            if (Mode == DataEntryPage.EntryViewMode.Edit)
            {

                //          txt_control_container_style.Text = XmlHelper.GetAttributeValue(SelectedNode, "C_Style");
                //         txt_control_css_class.Text = XmlHelper.GetAttributeValue(SelectedNode, "Class");
                XmlNodeList NodeList = SelectedNode.ChildNodes;

                XmlElement[] xmlarray = NodeList.Cast<XmlElement>().ToArray<XmlElement>();

                foreach (SiteModel.Widget_detail param in paramlist)
                {
                    if (xmlarray.Count(c => c.Attributes["Name"].Value.ToString() == param.Field_name) > 0)
                    {

                        param.Default_value = xmlarray.Single(c => c.Attributes["Name"].Value.ToString() == param.Field_name).Attributes["Value"].Value.ToString();
                    }
                }
            }

            return paramlist;


        }

        public static void AddASCX(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, int MasterPageID, List<string> placeholderList,
            string CurrentPlaceholder, int WidgetIDToAdd, string Style, string Class, Repeater ControlRepeater, int ContentID)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            //if content XML is empty, poplate the base xml from the class method.
            if (String.IsNullOrEmpty(ContentXML))
                xmlMaindoc = GetDefaultPageSetting(ContentType, MasterPageID, placeholderList);
            else
                xmlMaindoc.LoadXml(ContentXML);

            string placeholder = CurrentPlaceholder;

            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + placeholder + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var param = (from c in context.Widgets
                         where c.Widget_id == WidgetIDToAdd
                         select new
                         {
                             ID = c.Widget_id,
                             src = c.File_path,
                             name = c.Widget_name
                         }).Single();
            string control_type = context.WidgetTypes.Single(c => c.Widget_Type_Name == "ASCX").Widget_Type_Id.ToString();

            // need the nodelist for get the information from existing controls
            XmlNodeList nodelist = GetControlNodeListByPlaceholder(ContentXML, placeholder);

            XmlHelper.CreateAttribute(newControl, "ID", CurrentPlaceholder + "_ascx_" + (GetMaxControlD(nodelist) + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", control_type);
            XmlHelper.CreateAttribute(newControl, "src", param.src.ToString());
            XmlHelper.CreateAttribute(newControl, "Name", param.name.ToString());
            XmlHelper.CreateAttribute(newControl, "Seq", GetNextControlSequence(nodelist).ToString());
            XmlHelper.CreateAttribute(newControl, "C_Style", Style);
            XmlHelper.CreateAttribute(newControl, "Class", Class);

            foreach (RepeaterItem item in ControlRepeater.Items)
            {
                string paramValue_text = "";
                XmlNode newParam = xmlMaindoc.CreateElement("Param");
                Label paramName = (Label)item.FindControl("param_name");
                Label paramType = (Label)item.FindControl("param_type");

                Label param_assembly = (Label)item.FindControl("param_assembly");
                if (paramType.Text == "System.Boolean")
                {
                    RadioButton param_value_true = (RadioButton)item.FindControl("param_value_true");
                    paramValue_text = param_value_true.Checked ? "True" : "False";
                }
                else if (paramType.Text.Contains("System.Int"))
                {
                    TextBox param_value_int = (TextBox)item.FindControl("param_value_int");
                    paramValue_text = param_value_int.Text;
                }
                else if (((paramType.Text.Contains("+")) || (paramType.Text.Contains("Telerik.Web.UI"))) || (!string.IsNullOrEmpty(param_assembly.Text)))
                {
                    DropDownList param_value_list = item.FindControl("param_value_list") as DropDownList;
                    paramValue_text = param_value_list.SelectedItem.Text;
                }
                else
                {
                    TextBox paramValue = (TextBox)item.FindControl("param_value");
                    paramValue_text = paramValue.Text;
                }
                RequiredFieldValidator required = (RequiredFieldValidator)item.FindControl("param_required");
                XmlHelper.CreateAttribute(newParam, "Name", paramName.Text);
                XmlHelper.CreateAttribute(newParam, "Type", paramType.Text);
                XmlHelper.CreateAttribute(newParam, "Value", paramValue_text);
                XmlHelper.CreateAttribute(newParam, "Required", required.Enabled.ToString());
                newControl.AppendChild(newParam);
            }

            insertPointNode.AppendChild(newControl);


            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);

        }

        public static void UpdateASCX(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, string CurrentPlaceholder,
            string ControlID, string Style, string Class, Repeater ControlRepeater, int ContentID)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ContentXML);
            string selectedText = ControlID;

            foreach (RepeaterItem item in ControlRepeater.Items)
            {
                string paramValue_text = "";



                XmlNode newParam = xmlMaindoc.CreateElement("Param");
                Label paramName = (Label)item.FindControl("param_name");
                Label paramType = (Label)item.FindControl("param_type");
                Label param_assembly = (Label)item.FindControl("param_assembly");

                if (paramType.Text == "System.Boolean")
                {
                    RadioButton param_value_true = (RadioButton)item.FindControl("param_value_true");
                    paramValue_text = param_value_true.Checked ? "True" : "False";
                }
                else if (paramType.Text.Contains("System.Int"))
                {
                    TextBox param_value_int = (TextBox)item.FindControl("param_value_int");
                    paramValue_text = param_value_int.Text;
                }
                else if (((paramType.Text.Contains("+")) || (paramType.Text.Contains("Telerik.Web.UI"))) || (!string.IsNullOrEmpty(param_assembly.Text)))
                {
                    DropDownList param_value_list = item.FindControl("param_value_list") as DropDownList;
                    paramValue_text = param_value_list.SelectedItem.Text;
                }
                else
                {
                    TextBox paramValue = (TextBox)item.FindControl("param_value");
                    paramValue_text = paramValue.Text;
                }
                string apath = "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + selectedText + "']/Param[@Name='" + paramName.Text + "']";
                XmlNode test = xmlMaindoc.SelectSingleNode(apath);

                if ((xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + selectedText + "']/Param[@Name='" + paramName.Text + "']") == null))
                {
                    RequiredFieldValidator required = (RequiredFieldValidator)item.FindControl("param_required");
                    XmlHelper.CreateAttribute(newParam, "Name", paramName.Text);
                    XmlHelper.CreateAttribute(newParam, "Type", paramType.Text);
                    XmlHelper.CreateAttribute(newParam, "Value", paramValue_text);
                    XmlHelper.CreateAttribute(newParam, "Required", required.Enabled.ToString());
                    xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + selectedText + "']").AppendChild(newParam);
                }
                else
                    // Update Control Params
                    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + selectedText + "']/Param[@Name='" + paramName.Text + "']", "Value", paramValue_text);



            }


            // Update Style
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + selectedText + "']", "C_Style", Style, true);
            // Update Class
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + selectedText + "']", "Class", Class, true);


            // Update XML
            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);



        }

        
    }
}
