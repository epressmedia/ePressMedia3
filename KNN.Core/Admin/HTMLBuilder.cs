using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EPM.Core.Admin
{
    public  class HTMLBuilder : PageBuilder
    {

        public static void AddHTML(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, int MasterPageID, List<string> placeholderList,
            string CurrentPlaceholder, int WidgetIDToAdd,  string Style, string Class, int ContentID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            XmlDocument xmlMaindoc = new XmlDocument();
            //if content XML is empty, poplate the base xml from the class method.
            if (String.IsNullOrEmpty(ContentXML))
                xmlMaindoc = GetDefaultPageSetting(ContentType, MasterPageID, placeholderList);
            else
                xmlMaindoc.LoadXml(ContentXML);

            string control_type = context.WidgetTypes.Single(c => c.Widget_Type_Name.ToLower() == "html").Widget_Type_Id.ToString();
            string widgetName = context.Widgets.Single(c => c.Widget_id == WidgetIDToAdd).Widget_name.ToString();

            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder  + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");
            // need the nodelist for get the information from existing controls
            XmlNodeList nodelist = GetControlNodeListByPlaceholder(ContentXML, CurrentPlaceholder);

            XmlHelper.CreateAttribute(newControl, "ID", CurrentPlaceholder + "_html_" + (GetMaxControlD(nodelist) + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", control_type);
            XmlHelper.CreateAttribute(newControl, "src", WidgetIDToAdd.ToString());
            XmlHelper.CreateAttribute(newControl, "Seq", GetNextControlSequence(nodelist).ToString());
            XmlHelper.CreateAttribute(newControl, "Name", widgetName);
            XmlHelper.CreateAttribute(newControl, "C_Style", Style);
            XmlHelper.CreateAttribute(newControl, "Class", Class);

            insertPointNode.AppendChild(newControl);

            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);
        }

        public static void EditHtml(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, string CurrentPlaceholder,
            string ControlID, string Style, string Class, int ContentID, int WidgetID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ContentXML);

            string control_type = context.WidgetTypes.Single(c => c.Widget_Type_Name.ToLower() == "html").Widget_Type_Id.ToString();
            string widgetName = context.Widgets.Single(c => c.Widget_id == WidgetID).Widget_name.ToString();

            // Update Control Params
            
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@src='" + ControlID + "' and @Type='" + control_type + "']", "Name", widgetName);


            // Update Style
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@src='" + ControlID + "' and @Type='" + control_type + "']", "C_Style", Style,true);
            // Update Class
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@src='" + ControlID + "' and @Type='" + control_type + "']", "Class", Class,true);

            // src will be updated last
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@src='" + ControlID + "' and @Type='" + control_type + "']", "src", WidgetID.ToString());


            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);

        }
    }
}
