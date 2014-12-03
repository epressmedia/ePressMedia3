using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model;
using EPM.Legacy.Data;
using EPM.Core.Classified;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Search
{

    [Description("Default Search Control)")]
    public partial class Search : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["q"] != null)
                {
                    try
                    {
                        bindData();
                    }
                    catch
                    { }
                }
                else
                {
                    Response.Redirect("/");
                }
            }

        }
        void bindData()
        {
            string qs = Request.QueryString["q"].Trim();
            
            if (ShowArticleSearch)
            {
                if (ArticleSearchLink.Length > 0)
                    ArtLink.NavigateUrl = ArticleSearchLink+"?q=" + Server.UrlEncode(qs); //.Replace(' ', '+');
                else
                    ArtLinkDiv.Visible = false;

                bindArticle(qs);
                article_srchSec.Visible = true;
            }
            if (ShowClassifiedSearch)
            {
                if (ClassifiedSearchLink.Length > 0)
                    AdLink.NavigateUrl = ClassifiedSearchLink + "?q=" + Server.UrlEncode(qs);
                else
                    AdLinkDiv.Visible = false;
                bindClassified(qs);
                classified_srchSec.Visible = true;
            }
            if (ShowForumSearch)
            {
                if (ForumSearchLink.Length > 0)
                    ForumLink.NavigateUrl = ForumSearchLink+"?q=" + Server.UrlEncode(qs);
                else
                {
                    ForumLinkDiv.Visible = false;
                }
                bindForum(qs);
                forum_srchSec.Visible = true;
            }
            if (ShowBizSearch)
            {
                if (BizSearchLink.Length > 0)
                    BizLink.NavigateUrl = BizSearchLink + "?q=" + Server.UrlEncode(qs);
                else
                    BizLinkDiv.Visible = false;
                bindBiz(qs);
                biz_srchSec.Visible = true;
            }
        }
        void bindArticle(string query)
        {
            var results = EPM.Business.Model.Search.SearchController.SearchArticle(query);
            int cnt = results.Count(); ;
            ArtLink.Visible = (cnt > 0);
            ArticleCount.Text = cnt.ToString();
            ArticleRepeater.DataSource = results.Take(NumberArticleSearch);
            ArticleRepeater.DataBind();
        }

        void bindForum(string query)
        {

            var results = EPM.Business.Model.Search.SearchController.SearchForum(query);
            int cnt = results.Count();
            ForumLink.Visible = (cnt > 0);
            ThreadCount.Text = cnt.ToString();
            ForumRepeater.DataSource = results.Take(NumberForumSearch);
            ForumRepeater.DataBind();
        }

        void bindClassified(string query)
        {
            var results = EPM.Business.Model.Search.SearchController.SearchClassified(query);
            int cnt = results.Count();
            AdLink.Visible = (cnt > 0);
            AdCount.Text = cnt.ToString();
            AdRepeater.DataSource = results.Take(NumberForumSearch);
            AdRepeater.DataBind();
        }

        void bindBiz(string query)
        {
            var results = EPM.Business.Model.Biz.BEController.SearchBiz(query);// EPM.Business.Model.Search.SearchController.SearchBusinessEntity(query);
            int cnt = results.Count();
            BizLink.Visible = (cnt > 0);
            BizCount.Text = cnt.ToString();
            BizRepeater.DataSource = results.Take(NumberBizSearch);
            BizRepeater.DataBind();
        }

        protected void AdRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem ||
                e.Item.ItemType == ListItemType.Item)
            {
                ClassifiedModel.ClassifiedAd ad = e.Item.DataItem as ClassifiedModel.ClassifiedAd;

                var linkset = (from c in context.SiteMenus
                               where c.ContentView.ContentType.ContentTypeName == "Classified" && c.Param == ad.Category.ToString()
                               select c);

                HyperLink h = e.Item.FindControl("ViewLink") as HyperLink;

                h.NavigateUrl = "~/Classified/view.aspx?p=" + ad.Category.ToString() + "&aid=" + ad.AdId.ToString();// linkset.ToList()[0].Url.ToString() + "&aid=" + ad.AdId.ToString();

                Literal Abstract = e.Item.FindControl("Abstract") as Literal;
                string description = EPM.Core.WebHelper.StripTagsCharArray(ad.Description);

                Abstract.Text = (description.Length > 120) ? description.Substring(0, 120) : description;

            }
        }
        protected void BizRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem ||
                e.Item.ItemType == ListItemType.Item)
            {
                BizModel.BusinessEntity biz = e.Item.DataItem as BizModel.BusinessEntity;

                Literal lbl_catname = (Literal)e.Item.FindControl("lbl_catname");
                lbl_catname.Text = "[" + biz.BusienssCategory.CategoryName + "]";

            }
        }


        

        private bool showArticleSearch = false;
        [Category("EPMProperty"), Description("Enable Article Search"), DefaultValue(typeof(System.Boolean), "false")]
        public bool ShowArticleSearch
        {
            get;
            set;
        }

        private bool showForumSearch = false;
        [Category("EPMProperty"), Description("Enable Forum Search"), DefaultValue(typeof(System.Boolean), "false")]
        public bool ShowForumSearch
        {
            get;
            set;
        }

        private bool showClassifiedSearch = false;
        [Category("EPMProperty"), Description("Enable Classified Search"), DefaultValue(typeof(System.Boolean), "false")]
        public bool ShowClassifiedSearch
        {
            get;
            set;
        }

        private bool showBizSearch = false;
        [Category("EPMProperty"), Description("Enable Business Directory Search"), DefaultValue(typeof(System.Boolean), "false")]
        public bool ShowBizSearch
        {
            get;
            set;
        }


        private int numberArticleSearch = 5;
        [Category("EPMProperty"), Description("Number of Article Search Results"), DefaultValue(typeof(System.Int32), "5")]
        public int NumberArticleSearch
        {
            get { return numberArticleSearch; }
            set { numberArticleSearch = value;}
        }

        private int numberForumSearch = 5;
        [Category("EPMProperty"), Description("Number of  Forum Search Results"), DefaultValue(typeof(System.Int32), "5")]
        public int NumberForumSearch
        {
            get { return numberForumSearch; }
            set { numberForumSearch = value; }
        }

        private int numberClassifiedSearch = 5;
        [Category("EPMProperty"), Description("Number of  Classified Search Results"), DefaultValue(typeof(System.Int32), "5")]
        public int NumberClassifiedSearch
        {
            get { return numberClassifiedSearch; }
            set { numberClassifiedSearch = value; }
        }

        private int numberBizSearch = 5;
        [Category("EPMProperty"), Description("Number of  Business Directory Search Results"), DefaultValue(typeof(System.Int32), "5")]
        public int NumberBizSearch
        {
            get { return numberBizSearch; }
            set { numberBizSearch = value; }
        }


        /* Link Properties */
        private string articleSearchLink = "";
        [Category("EPMProperty"), Description("Number of Article Search Results")]
        public string ArticleSearchLink
        {
            get { return articleSearchLink; }
            set { articleSearchLink = value; }
        }

        private string forumSearchLink = "";
        [Category("EPMProperty"), Description("Number of  Forum Search Results")]
        public string ForumSearchLink
        {
            get { return forumSearchLink; }
            set { forumSearchLink = value; }
        }

        private string classifiedSearchLink = "";
        [Category("EPMProperty"), Description("Number of  Classified Search Results") ]
        public string ClassifiedSearchLink
        {
            get { return classifiedSearchLink; }
            set { classifiedSearchLink = value; }
        }

        private string bizSearchLink = "";
        [Category("EPMProperty"), Description("Number of  Business Directory Search Results"),DefaultValue(typeof(System.String), "~/Biz/Biz.aspx")]
        public string BizSearchLink
        {
            get { return bizSearchLink; }
            set { bizSearchLink = value; }
        }

    }


}