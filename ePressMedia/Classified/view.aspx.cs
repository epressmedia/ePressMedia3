using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Legacy.Security;
using System.Linq;
using System.Xml;


public partial class Classified_ViewAd : EPM.Core.EPMBasePage
{


    protected void Page_Load(object sender, EventArgs e)
    {

        //for logging to file
        try
        {
            int cat = int.Parse(Request.QueryString["p"]);

            if (!AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Classified, cat, Permission.List))
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();


            LoadPageControls(cat, ContentTypes.Classified, UseForTypes.DetailView);

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
            int cat = int.Parse(Request.QueryString["p"]);
            // Get the page setting
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            string xml_setting = (context.ClassifiedCategories.Single(a => a.CatId == cat)).DetailMetadataStr;

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