using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using log4net;

namespace EPM.Core.Web
{
    public static class EPMBaseXML
    {

        public static string GetBaseXML()
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            Page page = HttpContext.Current.Handler as Page;
            string xml_setting = "";
            string pageName = (HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToList())[1].ToString();
            if ((HttpContext.Current.Request.QueryString["p"] != null) || (page.RouteData.Values["category"] != null))
            {

                int cat = -1;
                if (HttpContext.Current.Request.QueryString["p"] != null)
                {
                    cat = int.Parse(HttpContext.Current.Request.QueryString["p"]);
                }
                else if ((page.RouteData.Values["category"] != null) && (pageName.ToLower() == "article"))
                {
                    cat = EPM.Business.Model.Article.ArticleCategoryContoller.GetArticleCatIdByURLSlug(page.RouteData.Values["category"].ToString());
                }
                // Get the page setting
                if (pageName.ToLower() == "article")
                    xml_setting = (context.ArticleCategories.Single(a => a.ArtCatId == cat)).metadataStr;
                else if (pageName.ToLower() == "forum")
                    xml_setting = (context.ForumConfigs.Single(a => a.ForumId == cat)).MetadataStr;
                else if (pageName.ToLower() == "classified")
                    xml_setting = (context.ClassifiedCategories.Single(a => a.CatId == cat)).MetadataStr;

            }
            else if (HttpContext.Current.Request.QueryString["pg"] != null)
            {
                int cat = int.Parse(HttpContext.Current.Request.QueryString["pg"]);
                if (pageName.ToLower() == "page")
                    xml_setting = (context.CustomPages.Single(a => a.CustomPageId == cat)).MetadataStr;
            }
            else
            {
                if (pageName.ToLower() == "biz")
                    xml_setting = (context.BusinessEntityConfigs.Single(c => 1 == 1)).MetadataStr;
            }

            if (pageName.ToLower() == "default.aspx")
            {
                if (EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Default Page ID").Length > 0)
                {
                    int cat = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Default Page ID").ToString().Trim());
                    xml_setting = (context.CustomPages.Single(a => a.CustomPageId == cat)).MetadataStr;
                }
                else
                    HttpContext.Current.Response.Redirect("~/Cp");

            }
            return xml_setting;
        }

        public static void SetMasterPageSession(string xml_setting)
        {

            // Load settings in xml format
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xml_setting);

            if ((xml.SelectSingleNode("/PageRoot/Configs/MasterPage")).Attributes["Name"] != null)
                 HttpContext.Current.Session["MasterName"] = (xml.SelectSingleNode("/PageRoot/Configs/MasterPage")).Attributes["Name"].Value;
            else
                HttpContext.Current.Session["MasterName"] = "";

        }
        

    }
}
