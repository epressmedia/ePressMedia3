using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EPM.Core.Admin
{
    public class ImageBuilder : PageBuilder
    {
        public static void AddImage(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, int MasterPageID, List<string> placeholderList,
            string CurrentPlaceholder, string ControlName, string SrcPath, string LinkHref, String LinkTarget, string ContainerStyle, string ControlStyle, string Class, int ContentID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            XmlDocument xmlMaindoc = new XmlDocument();
            //if content XML is empty, poplate the base xml from the class method.
            if (String.IsNullOrEmpty(ContentXML))
                xmlMaindoc = GetDefaultPageSetting(ContentType, MasterPageID, placeholderList);
            else
                xmlMaindoc.LoadXml(ContentXML);

            string control_type = context.WidgetTypes.Single(c => c.Widget_Type_Name.ToLower() == "image").Widget_Type_Id.ToString();
            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");
            // need the nodelist for get the information from existing controls
            XmlNodeList nodelist = GetControlNodeListByPlaceholder(ContentXML, CurrentPlaceholder);

            XmlHelper.CreateAttribute(newControl, "ID", CurrentPlaceholder + "_Img_" + (GetMaxControlD(nodelist) + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", control_type);
            XmlHelper.CreateAttribute(newControl, "src", SrcPath);
            XmlHelper.CreateAttribute(newControl, "Style", ControlStyle);
            XmlHelper.CreateAttribute(newControl, "C_Style", ContainerStyle);
            XmlHelper.CreateAttribute(newControl, "Name", ControlName);
            XmlHelper.CreateAttribute(newControl, "Seq", GetNextControlSequence(nodelist).ToString());


            XmlHelper.CreateAttribute(newControl, "class", Class);
            XmlHelper.CreateAttribute(newControl, "href", LinkHref);
            XmlHelper.CreateAttribute(newControl, "target", LinkTarget);

            insertPointNode.AppendChild(newControl);

            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);
        }

        public static void UpdateImage(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, string CurrentPlaceholder,
            string ControlID, string ControlName, string SrcPath, string LinkHref, String LinkTarget, string ContainerStyle, string ControlStyle, string Class, int ContentID)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ContentXML);


            // Update Control Params
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']", "Name", ControlName);
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']", "src", SrcPath);
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']", "Style", ControlStyle);
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']", "C_Style", ContainerStyle);
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']", "href", LinkHref, true);
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']", "target", LinkTarget, true);
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + CurrentPlaceholder + "']/Control[@ID='" + ControlID + "']", "class", Class, true);


            // Update Content XML
            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);
        }

    }
}
