using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;

using EPM.Legacy.Data;
using EPM.Legacy.Security;

using EPM.Data.Model;

[Description("List related articles")]
public partial class Controls_Article_RelatedArticleList : System.Web.UI.UserControl
{

    private static readonly ILog log = LogManager.GetLogger(typeof(Controls_Article_RelatedArticleList));

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {

            try
            {
                //if (!string.IsNullOrEmpty(Request.QueryString["aid"]))
                //    bindData(int.Parse(Request.QueryString["aid"].ToString()));


            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

    }
    
    void bindData(int articleId)
    {

        try
        {
            // get related articles


            IQueryable<ArticleModel.Article> query = null;
            IEnumerable<ArticleModel.Article> autofind_query = null;

            IQueryable<ArticleModel.Article> final_query = null;
            
            if (ViewType == ViewTypes.TextView)
            {
                query = (from c in context.Articles
                         join r in context.RelatedArticles on c.ArticleId equals r.RelatedArticleId
                         where c.Suspended == false && c.IssueDate < DateTime.Now && r.ArticleId == articleId 
                         orderby c.IssueDate descending
                         select c);//.Take(NumberOfItems);
            }
            else
            {
             query = (from c in context.Articles
                          join r in context.RelatedArticles on c.ArticleId equals r.RelatedArticleId
                          where  c.Suspended == false && c.IssueDate < DateTime.Now && r.ArticleId == articleId && (context.ArticleThumbnails.Any(x=>x.ArticleId == r.ArticleId))// !string.IsNullOrEmpty(c.Thumb_Path) 
                          orderby c.IssueDate descending
                          select c);//.Take(NumberOfItems);
            }
            



            
                if ((query != null) || (NumberOfItems > query.Count()))
                {
                    int need_more_items = NumberOfItems - query.Count();
                    autofind_query = (from c in context.EPM_SP_Get_Related_Articles(articleId, ShowInCategory)
                                      select c);
                    if (autofind_query != null)
                    {
                        final_query = (query).Union(autofind_query);
                    }
                }

                if (query.Count() > 0)
                {
                    if (ViewType == ViewTypes.ListView)
                    {
                        ra_lw_repeater.Visible = true;
                        ra_lw_repeater.DataSource = final_query.Take(NumberOfItems);
                        ra_lw_repeater.DataBind();
                    }
                    else if (ViewType == ViewTypes.GalleryView)
                    {
                        ra_gw_repeater.Visible = true;
                        ra_gw_repeater.DataSource = final_query.Take(NumberOfItems);
                        ra_gw_repeater.DataBind();
                    }

                    else if (ViewType == ViewTypes.TextView)
                    {
                        ra_tw_repeater.Visible = true;
                        ra_tw_repeater.DataSource = final_query.Take(NumberOfItems);
                        ra_tw_repeater.DataBind();
                    }

                    lbl_header.Text = Title;
                    lbl_header.Visible = true;
                }
            
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }

    }



    protected void ra_lw_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;

                HyperLink ViewImageLink = e.Item.FindControl("ViewImageLink") as HyperLink;
                ViewImageLink.NavigateUrl = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();

                HyperLink ViewLink = e.Item.FindControl("ViewLink") as HyperLink;
                ViewLink.NavigateUrl = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();




                Image img = e.Item.FindControl("Thumb") as Image;
                HtmlControl div = e.Item.FindControl("ra_lw_thumb") as HtmlControl;

                 Literal ltr_detail = e.Item.FindControl("ltr_detail") as Literal;
                 ltr_detail.Text = EPM.Legacy.Common.Utility.TruncateStringByWord(a.Abstract, NumofDescCharacters);



                 if (!(EPM.Business.Model.Article.ArticleThumbnailController.ArticleThumbnailExists(a.ArticleId)))
                 {
                     img.Visible = false;// ImageUrl = "~/img/noThumb.png";
                     div.Visible = false;
                 }
                 else
                 {
                     EPM.Core.Common.BindHelper.ImageBinder(e, "Thumb", a.ArticleId, ThumbnailTypeString);
                     //img.ImageUrl = a.Thumb_Path;
                 }
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }
    protected void ra_gw_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;


                HtmlAnchor gw_panel = e.Item.FindControl("gw_panel") as HtmlAnchor;
                gw_panel.HRef = "/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();

                EPM.Core.Common.BindHelper.ImageBinder(e, "gw_img", a.ArticleId, ThumbnailTypeString);

                HtmlImage gw_img = e.Item.FindControl("gw_img") as HtmlImage;
                gw_img.Alt = a.Title;

                Literal ltr_title = e.Item.FindControl("ltr_title") as Literal;
                ltr_title.Text = a.Title;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }

    protected void ra_tw_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;


                HtmlAnchor tw_panel = e.Item.FindControl("tw_panel") as HtmlAnchor;
                tw_panel.HRef = "/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();

                Literal ltr_title = e.Item.FindControl("ltr_title") as Literal;
                ltr_title.Text = a.Title;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }
    
    

    private string title = "Related Articles";
    [Category("EPMProperty"), Description("Title that shows up at the top of control"), DefaultValue(typeof(System.String), "Related Articles"),]
    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    private ViewTypes viewType = ViewTypes.ListView;
    [Category("EPMProperty"), Description("Title that shows up at the top of control"), DefaultValue(typeof(System.String), "Related Articles"),]
    public ViewTypes ViewType
    {
        get { return viewType; }
        set { viewType = value; }
    }



        private int numberOfItems;
        [Category("EPMProperty"), Description("Define number of articles to show"), DefaultValue(typeof(System.Int32),"10"), Required(ErrorMessage="Number Of Item is required.")]
        public int NumberOfItems
        {
            get { return numberOfItems; }
            set { numberOfItems = value; }
        }

        private bool showInCategory;
        [Category("EPMProperty"), Description("If checked, articles in the same category will be populated."), DefaultValue(typeof(System.Boolean), "false")]
        public bool ShowInCategory
        {
            get { return showInCategory; }
            set { showInCategory = value; }
        }




        private int numofDescCharacters = 120;
        [Category("EPMProperty"), Description("Number of characters that shows up in the HeadLineView"), DefaultValue(typeof(System.Int32), "120"),]
        public int NumofDescCharacters
        {
            get { return numofDescCharacters; }
            set { numofDescCharacters = value; }
        }

        public enum ViewTypes
        {
            ListView,
            GalleryView,
            TextView
        }

        private Dictionary<string, bool> thumbnailType = EPM.Business.Model.Common.ThumbnailTypes.GetThumbnailTypesDictionary();
        [Category("EPMProperty"), Description("Specify Thumbnail Type"), DataType("ThumbnailType")]
        public Dictionary<string, bool> ThumbnailType
        {
            get { return thumbnailType; }
            set { thumbnailType = value; }
        }

        public string ThumbnailTypeString
        {
            get { return ThumbnailType.Single(c => c.Value == true).Key.ToString(); }
        }


}