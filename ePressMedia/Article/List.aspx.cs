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
using EPM.Business.Model.Article;


public partial class Article_Articles : EPM.Core.EPMBasePage
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //for logging to file
        try
        {
            int cat = -1;
            if (Request.QueryString["p"] != null)
                cat = int.Parse(Request.QueryString["p"]);
            else if (Page.RouteData.Values["category"] != null)
                cat = ArticleCategoryContoller.GetArticleCatIdByURLSlug(Page.RouteData.Values["category"].ToString());

                if (cat > 0)
                {
                    if (!AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Article, cat, Permission.List))
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();


                    LoadPageControls(cat, ContentTypes.Article, UseForTypes.ListView);
                }

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
    //        int cat = int.Parse(Request.QueryString["p"]);
    //        // Get the page setting
    //        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    //        string xml_setting = (context.ArticleCategories.Single(a => a.ArtCatId == cat)).metadataStr;
            
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
    //    catch(Exception ex)
    //    {
    //        Logger.Equals(ex.Message);
    //    }
    //}


    

}