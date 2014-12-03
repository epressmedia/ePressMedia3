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

[Description("Article Listing Control")]
public partial class Controls_Article_ArticleSummaryList : System.Web.UI.UserControl
{

    private static readonly ILog log = LogManager.GetLogger(typeof(Controls_Article_ArticleSummaryList));

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (1==1)//(!IsPostBack)
        {


            try
            {


                bindData();


            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
    
    void bindData()
    {

        try
        {
            //var CatId = CategoryIDs.Split(',').ToList();

            //IQueryable<ArticleModel.Article> articleQuery;
            ////articleQuery = (from c in context.Articles
            ////                where CatId.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate < DateTime.Now
            ////                select c).Union(from a in context.Articles
            ////            join vc in context.VirtualCategoryLinks on a.ArticleId equals vc.ArticleId
            ////                                where CatId.Contains(vc.CatId.ToString()) && a.Suspended == false && a.IssueDate < DateTime.Now
            ////                                select a).OrderByDescending(c => c.IssueDate).Take(NumberOfItems);


            //var query1 = (from c in context.Articles
            //              where CatId.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate < DateTime.Now
            //              orderby c.IssueDate descending
            //              select c).Take(NumberOfItems);
            //var query2 = (from a in context.Articles
            //              join vc in context.VirtualCategoryLinks on a.ArticleId equals vc.ArticleId
            //              where CatId.Contains(vc.CatId.ToString()) && a.Suspended == false && a.IssueDate < DateTime.Now
            //              orderby a.IssueDate descending
            //              select a).Take(NumberOfItems);


            //articleQuery = (from n in query1
            //                select n).Union(from n2 in query2 select n2).OrderByDescending(c => c.IssueDate).Take(NumberOfItems);


            IQueryable<ArticleModel.Article>  articleQuery = EPM.Business.Model.Article.ArticleContoller.GetArticlesByCatIDs(CategoryIDs,numberOfItems);


            if (ShowHeadline)
            {

                ArtRepeater_Headline.Visible = true;

                ArtRepeater_Headline.DataSource = articleQuery.Take(NumberOfHeadLines);
                ArtRepeater_Headline.DataBind();


                ArtRepeater.DataSource = articleQuery.Skip(NumberOfHeadLines).Take(NumberOfItems-NumberOfHeadLines);
                ArtRepeater.DataBind();
            }
            else
            {
                ArtRepeater_Headline.Visible = false;
                ArtRepeater.DataSource = articleQuery.Take(NumberOfItems);
                ArtRepeater.DataBind();
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }

    }


    protected void ArtRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                

                HyperLink ViewLink = e.Item.FindControl("ViewLink") as HyperLink;
                
                ViewLink.NavigateUrl = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                


                System.Web.UI.HtmlControls.HtmlImage img_new = e.Item.FindControl("img_new") as System.Web.UI.HtmlControls.HtmlImage;
                int hours = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Article"));
                if (a.PostDate >= DateTime.Now.AddHours(hours*-1))
                    img_new.Visible = true;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }
    protected void ArtRepeater_Headline_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;

                Literal ltr_abstract = e.Item.FindControl("ltr_abstract") as Literal;
                string abstract_str = EPM.Legacy.Common.Utility.TruncateStringByWord(a.Abstract, NumberOfCharacters);
                // if abstract has no string, ... won't be displayed even.
                ltr_abstract.Text = abstract_str.Replace("...","").Length == 0 ? "": EPM.Legacy.Common.Utility.TruncateStringByWord(a.Abstract, NumberOfCharacters);

                Label lbl_issuedate = e.Item.FindControl("lbl_issuedate") as Label;
                lbl_issuedate.Text = " ["+a.IssueDate.ToShortDateString()+"]";

                HyperLink ViewImageLink = e.Item.FindControl("ViewImageLink") as HyperLink;
                ViewImageLink.NavigateUrl = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();

                HyperLink ViewLink = e.Item.FindControl("ViewLink") as HyperLink;
                ViewLink.NavigateUrl = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                HyperLink ViewLink2 = e.Item.FindControl("ViewLink2") as HyperLink;
                ViewLink2.NavigateUrl = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();

                EPM.Core.Common.BindHelper.ImageBinder(e,  "Thumb", a.ArticleId, ThumbnailTypeString);

               // Image img = e.Item.FindControl("Thumb") as Image;
               //HtmlControl div = e.Item.FindControl("ArticleSummaryList_thumb") as HtmlControl;
               // if (string.IsNullOrEmpty(a.Thumb_Path))
               // {
               //     img.Visible = false;// ImageUrl = "~/img/noThumb.png";
               //     div.Visible = false;
               // }
               // else
               //     EPM.Core.Common.BindHelper.ImageBinder(e, "Thumb", "", a.ArticleId, ThumbnailTypeString);
               //     //img.ImageUrl = a.Thumb_Path;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }
    

        private bool showHeadline = false;
        [Category("EPMProperty"),Description("Show headline rows"),DefaultValue(typeof(System.Boolean), "false"),Required()]
        public bool ShowHeadline
        {
            get { return showHeadline; }
            set { showHeadline = value; }
        }

        private int numberOfHeadLines;
        [Category("EPMProperty"),Description("Define number of articles shows in headline")]
        public int NumberOfHeadLines
        {
            get { return numberOfHeadLines; }
            set { numberOfHeadLines = value; }
        }

        private int numberOfItems;
        [Category("EPMProperty"), Description("Define number of articles to show"), DefaultValue(typeof(System.Int32),"10"), Required(ErrorMessage="Number Of Item is required.")]
        public int NumberOfItems
        {
            get { return numberOfItems; }
            set { numberOfItems = value; }
        }

        private string categoryIds;
        [Category("EPMProperty"),  Description("Category IDs")]
        public string CategoryIDs
        {
            get { return categoryIds; }
            set { categoryIds = value; }
        }

        private int numberOfCharacters = 120;
        [Category("EPMProperty"),DisplayName("Number of Characters"), Description("Define number of characters to show in description"), DefaultValue(typeof(System.Int32), "120"), Required(ErrorMessage = "Number Of Characters is required.")]
        public int NumberOfCharacters
        {
            get { return numberOfCharacters; }
            set { numberOfCharacters = value; }
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