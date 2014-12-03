using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ePressMedia.Biz
{
    public partial class ViewBiz : EPM.Core.EPMBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPageControls(0, ContentTypes.Biz, UseForTypes.DetailView);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            try
            {

                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                string xml_setting = (context.BusinessEntityConfigs.Single(a => 1 == 1)).DetailMetadataStr;

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