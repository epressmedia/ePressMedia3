using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace EPM.Core.Admin
{
    public class AdZoneBuilder:PageBuilder
    {
        public static void AddAdZone(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, int MasterPageID, List<string> placeholderList,
    string CurrentPlaceholder, string ControlName, string SrcPath,string ContainerStyle,  string Class, int ContentID)
        {

            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            XmlDocument xmlMaindoc = new XmlDocument();
            //if content XML is empty, poplate the base xml from the class method.
            if (String.IsNullOrEmpty(ContentXML))
                xmlMaindoc = GetDefaultPageSetting(ContentType, MasterPageID, placeholderList);
            else
                xmlMaindoc.LoadXml(ContentXML);

            string control_type = context.WidgetTypes.Single(c => c.Widget_Type_Name.ToLower() == "Banner Ad").Widget_Type_Id.ToString();
            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");
            // need the nodelist for get the information from existing controls
            XmlNodeList nodelist = GetControlNodeListByPlaceholder(ContentXML, CurrentPlaceholder);

            XmlHelper.CreateAttribute(newControl, "ID", CurrentPlaceholder + "_adzone_" + (GetMaxControlD(nodelist) + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", control_type);
            XmlHelper.CreateAttribute(newControl, "src", SrcPath);
            XmlHelper.CreateAttribute(newControl, "Seq", GetNextControlSequence(nodelist).ToString());
            XmlHelper.CreateAttribute(newControl, "Name", ControlName);
            XmlHelper.CreateAttribute(newControl, "C_Style", ContainerStyle);
            XmlHelper.CreateAttribute(newControl, "Class", Class);

            insertPointNode.AppendChild(newControl);

            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);
        }

        public static void UpdateAdZone(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, string CurrentPlaceholder,
    string ControlID, string ControlName, string SrcPath, string ContainerStyle, string Class, int ContentID)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ContentXML);


            string EditPointXPath = "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']";
            // Update Control Params
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "src", SrcPath);
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "Name", ControlName);
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "C_Style", ContainerStyle, true);
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "Class", Class, true);


            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);
        }

    }
}
