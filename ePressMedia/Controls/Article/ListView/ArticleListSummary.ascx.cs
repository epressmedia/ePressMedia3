using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using log4net;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EPM.Data.Model;


using EPM.Legacy.Data;
using EPM.Legacy.Security;

[Description("Article Listing Summary Control")]
public partial class Article_ArticleListSummary : System.Web.UI.UserControl
{
    private int rowPerPage;
    [Category("EPMProperty"), Description("Define number of articles per page"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Row Per Page is required.")]
    public int RowPerPage
    {
        get { return rowPerPage; }
        set { rowPerPage = value; }
    }

    private int categoryId = 0;
    [Category("EPMProperty"), Description("(Optional) Override Category IDs")]
    public int CategoryID
    {
        get { return categoryId; }
        set { categoryId = value; }
    }

    private bool showHeadline = false;
    [Category("EPMProperty"), Description("Show headline rows"), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public bool ShowHeadline
    {
        get { return showHeadline; }
        set { showHeadline = value; }
    }

    private Boolean showPager = true;
    [Category("EPMProperty"), Description("Show Pager"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowPager
    {
        get { return showPager; }
        set { showPager = value; }
    }

    private Boolean showSearch = true;
    [Category("EPMProperty"), Description("Show Search"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowSearch
    {
        get { return showSearch; }
        set { showSearch = value; }
    }


    private static readonly ILog log = LogManager.GetLogger(typeof(Article_ArticleListSummary));
    EPMEntityModel context = new EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bool override_CatId = false;
                int cat;
                if (Request.QueryString["p"] != null)
                {
                    cat = int.Parse(Request.QueryString["p"]);
                }
                else
                {
                    cat = CategoryID;
                    
                }


                if (CategoryID > 0)
                {
                    cat = CategoryID;
                    override_CatId = true;
                }
                CatId.Value = cat.ToString();

                if (!ShowPager)
                    FitPager1.Visible = false;

                if (!ShowSearch)
                    article_search_panel.Visible = false;

                int i = Request.Url.Query.IndexOf("&aid");
                Params.Value = string.Format("~/Article/view.aspx?p={0}&q={1}",
                    CatId.Value.ToString(), Request.QueryString["q"]);


                PostLink_popup.Visible = AccessControl.AuthorizeUser(Page.User.Identity.Name,
ResourceType.Article, cat, Permission.Write);


                if (override_CatId)
                {
                    //PostLink.Visible = false;
                    PostLink_popup.Visible = false;

                    WindowManager.Visible = ArticleEditor.Visible = false;
                    WindowManager.Enabled = ArticleEditor.Enabled = false;
                }

                //PostLink.NavigateUrl = "~/Article/AddArticle.aspx?p=" + cat + "&mode=Add";



                //ArticleEditor.NavigateUrl = "
                string path = "/Article/ArticleEntry.aspx?p=" + cat + "&mode=Add";

                string catName = context.ArticleCategories.SingleOrDefault(c => c.ArtCatId == cat).CatName;
                WindowManager.Title = "New Article Entry - " + catName;
                PostLink_popup.OnClientClick = "showArticleEditor('" + path + "','" + catName + "'); return false;";


                FitPager1.RowsPerPage = RowPerPage == 0 ? 10 : RowPerPage;// catSettings.RowsPerPage;// cfg.RowsPerPage;

                


                //PostLink.NavigateUrl = "~/Article/PostArticle.aspx?p=" + cat;


                bindData();

            }
            catch (Exception ex) { log.Error(ex.Message); }
        }
    }

    void bindData()
    {
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

            //EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();



            IQueryable<ArticleModel.Article> articleQuery;
            articleQuery = EPM.Business.Model.Article.ArticleContoller.GetArticlesByCatIDs(cat.ToString(), admin,false);
            //articleQuery = from c in context.Articles
            //               where c.Suspended == false
            //               orderby c.IssueDate descending
            //               select c;


            //articleQuery = articleQuery.Where(c => c.CategoryId == cat);
            //if (!admin)
            //    articleQuery = articleQuery.Where(c => c.IssueDate < DateTime.Now && c.Suspended == false);
            

            
            if (search_query)
            {
                string query_str = Request.QueryString["q"].ToString();
                articleQuery = articleQuery.Where(c => c.Title.Contains(query_str) || c.Body.Contains(query_str) || c.Reporter.Contains(query_str));
            }



            FitPager1.TotalRows = articleQuery.Count();
            FitPager1.CurrentPage = pageNum;

            if (ShowPager)
                FitPager1.Visible = (FitPager1.TotalRows > 0);

            var newresult = articleQuery.Skip((pageNum - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);



            if ((FitPager1.CurrentPage == 1) && (ShowHeadline))
            {


                ArticleListSummary_HeadlineTitle.Text = newresult.First().Title;
                ArticleListSummary_HeadlineSubTitle.Text = newresult.First().SubTitle;
                ArticleListSummary_HeadlineAbstract.Text = newresult.First().Abstract;
                ArticleListSummary_HeadlineReporter.Text = newresult.First().Reporter + " / " + newresult.First().IssueDate.ToString("yyyy.MM.dd");
                ArticleListSummary_HeadlineLink.NavigateUrl = ("~/Article/view.aspx?p=" + newresult.First().CategoryId + "&page=" + FitPager1.CurrentPage + "&aid=" + newresult.First().ArticleId);



                ArticleListSummary_Repeater.DataSource = newresult.Skip(1);
                ArticleListSummary_Repeater.DataBind();
            }

            else
            {
                ArticleListSummary_Headline.Visible = false;
                ArticleListSummary_Repeater.DataSource = newresult;
                ArticleListSummary_Repeater.DataBind();
            }
        }
        catch
        {
        }
    }


    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        bindData();
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        int catId = int.Parse(Request.QueryString["p"]);
        Response.Redirect(string.Format("list.aspx?p={0}&page={1}&q={2}", catId,
            FitPager1.CurrentPage, Server.UrlEncode(SearchValue.Text.Trim())));
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
                Label catName = e.Item.FindControl("ArticleListSummary_CatName") as Label;
                
                l.HRef = ResolveClientUrl("/Article/view.aspx?p=" + a.CategoryId + "&page=" + FitPager1.CurrentPage +
                                "&aid=" + a.ArticleId);

                catName.Text = "[" + a.ArticleCategory1.CatName + "]";


            }
        }
        catch(Exception ex)
        {
            log.Error(ex.Message);
        }
    }
}