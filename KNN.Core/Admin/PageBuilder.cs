using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPM.Core;
using EPM.Core.Pages;
using EPM.Data.Model;
using System.Reflection;

namespace EPM.Core.Admin
{
    public class PageBuilder
    {

        public static XmlDocument GetDefaultPageSetting(EPMBasePage.ContentTypes ContentType, int MasterPageId, List<string> placeholderList)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            XmlDocument doc;
            doc = XmlHelper.CreateXmlDocument();

            XmlNode myroot = doc.CreateElement("PageRoot");
            XmlNode rootNode = doc.AppendChild(myroot);

            // Build Config
            XmlNode myconfig = doc.CreateElement("Configs");
            // Master Setting

            XmlNode mySubConfig;
            mySubConfig = doc.CreateElement("MasterPage");


            SiteModel.MasterPageConfig masterPageConfig = context.MasterPageConfigs.Single(c => c.MasterPageId == MasterPageId);
            XmlHelper.CreateAttribute(mySubConfig, "Value", masterPageConfig.MasterPagePath.Replace('\\', '/'));
            XmlHelper.CreateAttribute(mySubConfig, "Name", masterPageConfig.Description);
            myconfig.AppendChild(mySubConfig);

            rootNode.AppendChild(myconfig);

            // Build Contents

            XmlNode myContents = doc.CreateElement("Contents");

            XmlHelper.CreateChildNodes(myContents, "Content", "ID", placeholderList.ToArray());

            rootNode.AppendChild(myContents);

            return doc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MasterPagePath">e.g. ~\Master\MasterPage.master</param>
        /// <returns></returns>
        public static List<string> GetPlaceholdersByMasterPagePath(string MasterPagePath)
        {
         
            string master_vPath = MasterPagePath.Replace("~/Master/", "~\\Master\\");
            System.Web.UI.Page page = System.Web.HttpContext.Current.Handler as System.Web.UI.Page;
            var siteMaster = page.LoadControl(master_vPath);

            // Find the list of ContentPlaceHolder controls
            var controls = WebHelper.FindControlsByType<ContentPlaceHolder>(siteMaster);

            // Do something with each control that was found

            List<string> PlaceholderList = new List<string>();
            if (controls.Count > 0)
            {
                //IDictionary<string, string> controlListDic = new Dictionary<string, string>();
                foreach (var control in controls)
                {
                    PlaceholderList.Add(control.ClientID.ToString());

                }
             
            }
            return PlaceholderList;
        }
        

        public static XmlNodeList GetControlNodeListByPlaceholder(string ContentXML, string PlaceholderName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ContentXML);

           return xmlDoc.SelectNodes("/PageRoot/Contents/Content[@ID='" + PlaceholderName + "']/Control");
        }

        protected static int GetMaxControlD(XmlNodeList nodelist)
        {
            // Get the max control id
            List<int> max_control_id_list = new List<int>();

            foreach (XmlNode node in nodelist)
            {
                string cont_id = node.Attributes["ID"].Value.ToString();
                string seq = node.Attributes["Seq"].Value.ToString();
                max_control_id_list.Add(int.Parse(cont_id.Substring(cont_id.LastIndexOf("_") + 1, cont_id.Length - cont_id.LastIndexOf("_") - 1)));
            }

            if (max_control_id_list.Count == 0)
                return 0;
            else
                return max_control_id_list.Max();
        }

        protected static int GetNextControlSequence(XmlNodeList nodelist)
        {
            // get the next sequence
            List<int> next_sequence_list = new List<int>();

            foreach (XmlNode node in nodelist)
            {
                string cont_id = node.Attributes["ID"].Value.ToString();
                string seq = node.Attributes["Seq"].Value.ToString();
                next_sequence_list.Add(int.Parse(seq));
            }

            if (next_sequence_list.Count == 0)
                return 1;
            else
                return next_sequence_list.Max() + 1;

        }

        public static string OrderControl(string ContentXML, string placeholder_name, int control_index, int destination_index)
        {
            
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(ContentXML);

            int detinationItemSeq = destination_index + 1;
            int movingItemSeq = control_index + 1;

            // get the node of moving item
            XmlNode nodeMoving = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + movingItemSeq.ToString() + "']");
            // get the original seq of the moved item
            string MovingNodeID = nodeMoving.Attributes["ID"].Value.ToString();


            // Check if the item moved 
            if (movingItemSeq < detinationItemSeq) //Moving Below
            {
                //find all nodes having lower sequence than movingItemSeq and give it -1 to its seq
                int counter = movingItemSeq + 1;
                while (counter <= detinationItemSeq)
                {
                    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + counter.ToString() + "']", "Seq", (counter - 1).ToString());
                    counter++;
                }
            }
            else // Moving Above
            {
                //find all nodes having higher sequence than movingItemSeq and give it +1 to its seq
                int counter = movingItemSeq - 1;
                while (counter >= detinationItemSeq)
                {
                    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + counter.ToString() + "']", "Seq", (counter + 1).ToString());
                    counter--;
                }
            }

            // update the moving item seq
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@ID='" + MovingNodeID + "']", "Seq", detinationItemSeq.ToString());



            return System.Web.HttpUtility.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));

            
        }

  

    }
}
