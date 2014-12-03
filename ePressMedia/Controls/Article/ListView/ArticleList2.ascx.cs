using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.HtmlControls;

using log4net;

using Knn.Data;
using Knn.Security;

[Description("Article Listing Type2")]
public partial class Article_ArticleList2 : System.Web.UI.UserControl
{

    private bool showHeadline = false;

    [Category("KNNProperty"), Description("Show headline rows"), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public bool ShowHeadline
    {
        get { return showHeadline; }
        set { showHeadline = value; }
    }

    private int numberOfHeadLines;
    [Category("KNNProperty"), Description("Define number of articles shows in headline")]
    public int NumberOfHeadLines
    {
        get { return numberOfHeadLines; }
        set { numberOfHeadLines = value; }
    }

    private int rowPerPage;
    [Category("KNNProperty"), Description("Define number of articles per page"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Row Per Page is required.")]
    public int RowPerPage
    {
        get { return rowPerPage; }
        set { rowPerPage = value; }
    }

    private int categoryId = 0;
    [Category("KNNProperty"), Description("(Optional) Override Category ID")]
    public int CategoryID
    {
        get { return categoryId; }
        set { categoryId = value; }
    }

    private Boolean showPager = true;
    [Category("KNNProperty"), Description("Show Pager"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowPager
    {
        get { return showPager; }
        set { showPager = value; }
    }

    private Boolean showSearch = true;
    [Category("KNNProperty"), Description("Show Search"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowSearch
    {
        get { return showSearch; }
        set { showSearch = value; }
    }

            private static readonly ILog log = LogManager.GetLogger(typeof(Article_ArticleList2));
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int cat = int.Parse(Request.QueryString["p"]);
                if (CategoryID > 0)
                    cat = CategoryID;
                CatId.Value = cat.ToString();

                int i = Request.Url.Query.IndexOf("&aid");
                //Params.Value = string.Format("~/Article/ViewArticle.aspx?p={0}&q={1}",
                //    Request.QueryString["p"], Request.QueryString["q"]);

                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                var catSettings = (context.ArticleCategories.Single(c => c.ArtCatId == cat));


                article_search_panel.Visible = ShowSearch;
                FitPager1.Visible = ShowPager;

                FitPager1.RowsPerPage = RowPerPage == null ? 10 : RowPerPage;// catSettings.RowsPerPage;// cfg.RowsPerPage;

                bindData();

                PostLink.Visible = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                    ResourceType.Article, cat, Permission.Write);

                PostLink.NavigateUrl = "PostArticle.aspx?p=" + cat;




            }
            catch(Exception ex) {
                log.Error(ex.Message);
            }
        }
    }

    void bindData()
    {
         log.Info("Load Article List");
         try
         {
             int pageNum;
             int cat = int.Parse(CatId.Value);

             if (FitPager1.CurrentPage > 1)
             {
                 pageNum = FitPager1.CurrentPage;
             }
             else
             {
                 if (string.IsNullOrEmpty(Request.QueryString["page"]))
                     pageNum = 1;
                 else
                     pageNum = int.Parse(Request.QueryString["page"]);
             }

             bool admin = AccessControl.AuthorizeUser(Page.User.Identity.Name,
 ResourceType.Article, cat, Permission.FullControl);

             bool search_query = false;
             if (Request.QueryString["q"] != null)
                 search_query = true;

             EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();



             IQueryable<ArticleModel.Article> articleQuery;
             articleQuery = from c in context.Articles
                            orderby c.IssueDate descending
                            select c;

             articleQuery = articleQuery.Where(c => c.CategoryId == cat);
             if (!admin)
                 articleQuery = articleQuery.Where(c => c.IssueDate < DateTime.Now && c.Suspended == false);
             if (search_query)
             {
                 string query_str = Request.QueryString["q"].ToString();
                 articleQuery = articleQuery.Where(c => c.Title.Contains(query_str) || c.Body.Contains(query_str) || c.Reporter.Contains(query_str));
             }

             FitPager1.TotalRows = articleQuery.Count();
             FitPager1.CurrentPage = pageNum;
             if (ShowPager)
                FitPager1.Visible = (FitPager1.TotalRows > 0);

             var finalResult = articleQuery.Skip((pageNum - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);


             if ((FitPager1.CurrentPage == 1) && (ShowHeadline))
             {

                 ArticleListSummary_Headline.Visible = true;
                 ArticleListSummary_HeadlineTitle.Text = finalResult.First().Title;
                 ArticleListSummary_HeadlineSubTitle.Text = finalResult.First().SubTitle;
                 ArticleListSummary_HeadlineAbstract.Text = finalResult.First().Abstract;
                 ArticleListSummary_HeadlineReporter.Text = finalResult.First().Reporter;
                 //ArticleListSummary_HeadlinePostDate.Text = finalResult.First().IssueDate.ToString("yyyy.MM.dd");
                 //ArticleListSummary_HeadlineHit.Text = finalResult.First().ViewCount.ToString();
                 ArticleListSummary_HeadlineLink.NavigateUrl = ("~/Article/ViewArticle.aspx?p=" + finalResult.First().CategoryId + "&page=" + FitPager1.CurrentPage + "&aid=" + finalResult.First().ArticleId);
                 //ArticleListSummary_HeadlineLinkImage.NavigateUrl = ("~/Article/ViewArticle.aspx?p=" + finalResult.First().CategoryId + "&page=" + FitPager1.CurrentPage + "&aid=" + finalResult.First().ArticleId);
                 //ArticleListSummary_TitleThumb.ImageUrl = ResolveClientUrl(finalResult.First().Thumb_Path);


                 ArticleListSummary_Repeater.DataSource = finalResult.Skip(NumberOfHeadLines);
                 ArticleListSummary_Repeater.DataBind();
             }
             else
             {
                 ArticleListSummary_Headline.Visible = false;
                 ArticleListSummary_Repeater.DataSource = finalResult.ToList();
                 ArticleListSummary_Repeater.DataBind();
             }

         }
         catch (Exception ex)
         {
             log.Error(ex.Message);
         }
    }

    

    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        bindData();
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        int catId = int.Parse(Request.QueryString["p"]);
        if (CategoryID > 0)
            catId = CategoryID;
        Response.Redirect(string.Format("Articles.aspx?p={0}&q={1}", catId, Server.UrlEncode(SearchValue.Text.Trim())));
    }

    protected void ArticleListSummary_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;

                HtmlAnchor l = e.Item.FindControl("ArticleListSummary") as HtmlAnchor;
                //Label catName = e.Item.FindControl("ArticleListSummary_CatName") as Label;

                l.HRef = ResolveClientUrl("~/ViewArticle.aspx?p="+a.CategoryId + "&page=" + FitPager1.CurrentPage +
                                "&aid=" + a.ArticleId);              

            }
        }
        catch(Exception ex)
        {
            log.Error(ex.Message);
        }
    }
}