using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Core.Pages;
using System.Xml;
using System.Web.UI.WebControls;

namespace EPM.Core.Admin
{
    public class ControlEditorBase : System.Web.UI.UserControl
    {

        protected void ControlEditorInitializer()
        {
            try
            {
                Mode = DataEntryPage.EntryViewMode.Edit;
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"].ToString().ToLower() == "add")
                    {
                        Mode = DataEntryPage.EntryViewMode.Add;
                    }

                }

                // Set properties
                if (Mode == DataEntryPage.EntryViewMode.Edit)
                {
                    Control_Id = Request.QueryString["id"].ToString();
                    Placeholder = Request.QueryString["placeholder"].ToString();
                }
                else if (Mode == DataEntryPage.EntryViewMode.Add)
                {

                }
            }
            catch
            {
                CloseWindow();
            }
        }

        
        protected void CloseWindow()
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", true);
        }

        protected  void DeleteControl(string ContentXML, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType, string PlaceholerName, string ControlID, int ContentID)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ContentXML);

            XmlNode currentNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + PlaceholerName + "']/Control[@ID='" + ControlID + "']");
            int Sequence = int.Parse(currentNode.Attributes["Seq"].Value.ToString());
            XmlHelper.Delete(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + PlaceholerName + "']/Control[@ID='" + ControlID + "']");

            XmlNodeList nodes = xmlMaindoc.SelectNodes("/PageRoot/Contents/Content[@ID='" + PlaceholerName + "']/Control[@Seq > " + Sequence + "]");


            foreach (XmlNode node in nodes)
            {
                int current_seq = int.Parse(node.Attributes["Seq"].Value.ToString());
                XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + PlaceholerName + "']/Control[@Seq='" + current_seq.ToString() + "']", "Seq", (current_seq - 1).ToString());
            }

            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(ContentID, xmlMaindoc, ContentType, UseForType);

        }


        private string control_Id;
        public string Control_Id
        { get; set; }

        private string placeholder;
        public string Placeholder
        { get; set; }

        private DataEntryPage.EntryViewMode mode;
        public DataEntryPage.EntryViewMode Mode
        { get; set; }

        private XmlNode selectedNode;
        public XmlNode SelectedNode
        { get; set; }

    }
}
