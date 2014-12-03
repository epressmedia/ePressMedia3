using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;
using EPM.Core;


namespace ePressMedia.Cp.Pages
{
    public partial class Preview : EPM.Core.EPMBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            EPMPageRender.PageRender(Session["Preview_XML"].ToString(), true, false);
            //RenderXML(Session["Preview_XML"].ToString(),-1,true);
        }


        protected override void OnPreInit(EventArgs e)
        {
            try
            {

                string xml_setting = Session["Preview_XML"].ToString();

                // check if the xml setting is set
                if (xml_setting.Length > 0)
                {
                    // Load settings in xml format
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(xml_setting);

                    //Get Master Page setting and set the master page
                    this.MasterPageFile = (xml.SelectSingleNode("/PageRoot/Configs/MasterPage")).Attributes["Value"].Value;

                }
            }
            catch (Exception ex)
            {
                Logger.Equals(ex.Message);
            }
        }
    }
}