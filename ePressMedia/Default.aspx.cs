using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using EPM.Core.Admin;
using EPM.Data.Model;



public partial class _Default : EPM.Core.EPMBasePage
{


    protected void Page_Load(object sender, EventArgs e)
    {

        EPMEntityModel context = new EPMEntityModel();
        if (1 == 1)// (!IsPostBack)
        {

            int cat = int.Parse( EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Default Page ID").ToString().Trim());

            LoadPageControls(cat, ContentTypes.Page, UseForTypes.ListView);
        }
    }
    //protected override void OnPreInit(EventArgs e)
    //{
    //    try
    //    {
    //        if (SiteSettings.GetSiteSettingValueByName("Default Page ID").Length > 0)
    //        {
    //            int cat = int.Parse(SiteSettings.GetSiteSettingValueByName("Default Page ID").ToString().Trim());
    //            // Get the page setting
    //            EPMEntityModel context = new EPMEntityModel();
    //            string xml_setting = (context.CustomPages.Single(a => a.CustomPageId == cat)).MetadataStr;

    //            // check if the xml setting is set
    //            if (xml_setting.Length > 0)
    //            {
    //                // Load settings in xml format
    //                XmlDocument xml = new XmlDocument();
    //                xml.LoadXml(xml_setting);

    //                //Get Master Page setting and set the master page
    //                this.MasterPageFile = (xml.SelectSingleNode("/PageRoot/Configs/MasterPage")).Attributes["Value"].Value;

    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Cp");
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.Equals(ex.Message);
    //    }
    //}





}