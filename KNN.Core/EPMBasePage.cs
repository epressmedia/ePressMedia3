using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using log4net;


namespace EPM.Core
{
    public class EPMBasePage : System.Web.UI.Page
    {
     public static readonly ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    private static string mode;
    public static string Mode
    {
        set { mode = value; }
        get { return mode; }
    }

    protected override void InitializeCulture()
    {

        string culture_info = "";
        if(HttpContext.Current.Request.QueryString["lang"] != null)
            Session["CultureInfo"] = HttpContext.Current.Request.QueryString["lang"].ToString();
        if (Session["CultureInfo"] != null)
        {
            culture_info = Session["CultureInfo"].ToString();
        }
        else
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            culture_info = context.SiteSettings.Single(c => c.SettingName == "Culture Info").SettingValue;
            Session["CultureInfo"] = culture_info;
        }
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture_info);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture_info);
        base.InitializeCulture();

        /// moved here because oninit doesn't get fired sometimes
        try
        {
            string xml_setting = EPM.Core.Web.EPMBaseXML.GetBaseXML();

            EPM.Core.Web.EPMBaseXML.SetMasterPageSession(xml_setting);

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

    protected override void OnPreInit(EventArgs e)
    {
        try
        {
            string xml_setting = EPM.Core.Web.EPMBaseXML.GetBaseXML();

            EPM.Core.Web.EPMBaseXML.SetMasterPageSession(xml_setting);

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



        public enum ContentTypes
        {
            Article,
            Forum,
            Classified,
            Biz,
            MasterPage,
            Page,
            Template
        }

        public enum UseForTypes
        {
            ListView,
            DetailView
        }
        private void MetaTitleLoad()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            Page.Title = context.SiteSettings.Single(c => c.SettingName == "Default Title").SettingValue;

            HtmlMeta hm1 = new HtmlMeta();
            HtmlMeta hm2 = new HtmlMeta();

            hm1.ID = "mata_description";
            hm1.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            hm1.Name = "description";
            hm1.Content = context.SiteSettings.Single(c => c.SettingName == "Default Meta Descr").SettingValue;

            hm2.ID = "meta_keyword";
            hm2.Name = "keywords";
            hm2.Content = context.SiteSettings.Single(c => c.SettingName == "Default Meta Keywords").SettingValue;


            HtmlHead head = (HtmlHead)Page.Header;

            head.Controls.Add(hm1);
            head.Controls.Add(hm2);

        }
 
	public void LoadPageControls(int cat, ContentTypes contentType, UseForTypes useForType)
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        HttpContext httpContext = System.Web.HttpContext.Current;
            string xml_setting = "";

            httpContext.Session["PageInfo-UseForType"] = useForType;
            httpContext.Session["PageInfo-ContentType"] = contentType;
            httpContext.Session["PageInfo-CategoryID"] = cat;


            MetaTitleLoad();
          
                if (contentType == ContentTypes.Article)
                {
                    ArticleModel.ArticleCategory obj = context.ArticleCategories.Single(a => a.ArtCatId == cat);
                    if (useForType == UseForTypes.ListView)
                        xml_setting = obj.metadataStr;
                    else if (useForType == UseForTypes.DetailView)
                        xml_setting = obj.DetailMetadataStr;
                }
                else if (contentType == ContentTypes.Forum)
                {
                    ForumModel.ForumConfig obj = (context.ForumConfigs.Single(a => a.ForumId == cat));
                    if (useForType == UseForTypes.ListView)
                        xml_setting = obj.MetadataStr;
                    else if (useForType == UseForTypes.DetailView)
                        xml_setting = obj.DetailMetadataStr;
                }
                else if (contentType == ContentTypes.Page)
                {
                    SiteModel.CustomPage obj = (context.CustomPages.Single(a => a.CustomPageId == cat));

                    // Override the meta tag and browser title for custom page
                    if (!string.IsNullOrEmpty(obj.PageTitle))
                    {
                        ((Page)httpContext.Handler).Title = obj.PageTitle;
                    }

                    if (!string.IsNullOrEmpty(obj.MetaDescription))
                    {
                        HtmlMeta metaKeywords = (HtmlMeta)Page.Header.FindControl("mata_description");
                        if (metaKeywords != null)
                            metaKeywords.Content = obj.MetaDescription;
                    }

                    if (useForType == UseForTypes.ListView)
                        xml_setting = obj.MetadataStr;
                    else if (useForType == UseForTypes.DetailView)
                        xml_setting = obj.MetadataStr;
                }
                else if (contentType == ContentTypes.Classified)
                {
                    ClassifiedModel.ClassifiedCategory obj = (context.ClassifiedCategories.Single(a => a.CatId == cat));
                    if (useForType == UseForTypes.ListView)
                        xml_setting = obj.MetadataStr;
                    else if (useForType == UseForTypes.DetailView)
                        xml_setting = obj.DetailMetadataStr;
                }
                else if (contentType == ContentTypes.Biz)
                {
                    BizModel.BusinessEntityConfig obj= (context.BusinessEntityConfigs.Single(a => 1 == 1));
                    if (useForType == UseForTypes.ListView)
                        xml_setting = obj.MetadataStr;
                    else if (useForType == UseForTypes.DetailView)
                        xml_setting = obj.DetailMetadataStr;
                }

              
            EPMPageRender.PageRender(xml_setting, false, ((HttpContext.Current.User.Identity.IsAuthenticated) && (HttpContext.Current.User.IsInRole("Admins"))));
            //RenderXML(xml_setting,-1, false);

    }
     
        /// <summary>
        /// Add a meta tag to the page's header.
        /// </summary>
        /// <param name="name">
        /// The tag name.
        /// </param>
        /// <param name="value">
        /// The tag value.
        /// </param>
        protected virtual void AddMetaTag(string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return;

            const string tag = "\n<meta name=\"{0}\" content=\"{1}\" />";
            Header.Controls.Add(new LiteralControl(string.Format(tag, name, value)));
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.TemplateControl.Error"></see> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="T:System.EventArgs"></see> that contains the event data.
        /// </param>
        protected override void OnError(EventArgs e)
        {
            var ctx = HttpContext.Current;
            var exception = ctx.Server.GetLastError();

            if (exception != null && exception.Message.Contains("callback"))
            {
                // This is a robot spam attack so we send it a 404 status to make it go away.
                ctx.Response.StatusCode = 404;
                ctx.Server.ClearError();
                //Comment.OnSpamAttack();
            }

            base.OnError(e);
        }
	}
}

