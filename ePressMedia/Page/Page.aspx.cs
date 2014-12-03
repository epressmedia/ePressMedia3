using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using log4net;
using EPM.Legacy.Security;
namespace ePressMedia.Pages
{

    public partial class _Page : EPM.Core.EPMBasePage
    {


        protected void Page_Load(object sender, EventArgs e)
        {


            //Page.ClientScript.RegisterStartupScript(typeof(_Page), "postback", "document.forms[0].action='';", true);
            
            //for logging to file
            try
            {
                int cat = int.Parse(Request.QueryString["pg"]);

                LoadPageControls(cat, ContentTypes.Page, UseForTypes.ListView);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        //protected override void OnPreInit(EventArgs e)
        //{
        //    try
        //    {
        //        int cat = int.Parse(Request.QueryString["pg"]);
        //        // Get the page setting
        //        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        //        string xml_setting = (context.CustomPages.Single(a => a.CustomPageId == cat)).MetadataStr;

        //        // check if the xml setting is set
        //        if (xml_setting.Length > 0)
        //        {
        //            // Load settings in xml format
        //            XmlDocument xml = new XmlDocument();
        //            xml.LoadXml(xml_setting);

        //            //Get Master Page setting and set the master page
        //            this.MasterPageFile = (xml.SelectSingleNode("/PageRoot/Configs/MasterPage")).Attributes["Value"].Value;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Equals(ex.Message);
        //    }
        //}

  

    }
}