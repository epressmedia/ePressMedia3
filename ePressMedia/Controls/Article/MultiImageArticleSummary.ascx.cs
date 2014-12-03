using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Article
{
    [Description("Article Contents Summary w/ Images)")]
    public partial class MultiImageArticleSummary : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArticleLoad(ContentIDs);
                lbl_header.Text = HeaderName;
            }
        }



        void ArticleLoad(string CategoryIDs)
        {
            var contIDs = ContentIDs.Split(',').ToList();

            //IQueryable<ArticleModel.Article> articles;
            // articles = (from c in context.Articles
            //                where contIDs.Contains(c.CategoryId.ToString())&& c.Suspended == false && c.IssueDate < DateTime.Now && c.Thumb_Path.Trim().Length > 0
            //                select c);

            IQueryable<ArticleModel.Article> articles = EPM.Business.Model.Article.ArticleContoller.GetArticlesByCatIDs(CategoryIDs, NumOfItems, true);



            if (ContentItemOrder == ContentSortBy.ViewInDays)
                articles = articles.Where(c => c.PostDate >= DateTime.Now.AddDays(-7)).OrderByDescending(c => c.ViewCount);//.Take(NumOfItems);
            else // Default Post Date
                articles = articles.OrderByDescending(c => c.PostDate);//.Take(NumOfItems);


            if (articles.Count() == 0)
                MultiImageArticleSummary_Container.Visible = false;
            else
            {
                ReportSetRep.DataSource = articles;
                ReportSetRep.DataBind();
            }


        }


        protected void ReportSetRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article article = e.Item.DataItem as ArticleModel.Article;

                var lnk = e.Item.FindControl("ViewLink") as HyperLink;
                var ImageLink = e.Item.FindControl("ImageLink") as HyperLink;
                lnk.Text = article.Title;
                lnk.NavigateUrl = "~/Article/view.aspx?p="+article.CategoryId.ToString()+"&aid=" + article.ArticleId.ToString();
                ImageLink.NavigateUrl = "~/Article/view.aspx?p=" + article.CategoryId.ToString() + "&aid=" + article.ArticleId.ToString(); 

                EPM.Core.Common.BindHelper.ImageBinder(e, "Thumb", article.ArticleId, ThumbnailTypeString);

                //(e.Item.FindControl("Thumb") as Image).ImageUrl = article.Thumb_Path;

                //var lit = e.Item.FindControl("Preview") as Literal;
                //lit.Text = (article.Abstract.Length > 44) ?
                //    article.Abstract.Substring(0, 44) + "..." : article.Abstract;


            }
        }


        private string headerName;
        [Category("EPMProperty"), Description("Name displayed at the top of the control"), Required(ErrorMessage = "HeaderName is required")]
        public string HeaderName
        {
            get;
            set;
        }

        private string moreLink;
        [Category("EPMProperty"), Description("(Optional)URL of the page which will be open when the More button is clicked.")]
        public string MoreLink
        {
            get;
            set;
        }

        private string moreLinkType = "_self";
        [Category("EPMProperty"), Description("(Optional)More link open type"), DefaultValue(typeof(System.String), "_self")]
        public string MoreLinkType
        {
            get;
            set;
        }

        private int numOfItems = 10;
        [Category("EPMProperty"), Description("Number of items to be listed in each section"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Number of items is required")]
        public int NumOfItems
        {
            get;
            set;
        }

        private string contentIDs;
        [Category("EPMProperty"), Description("Content IDs to be linked to the control"), Required(ErrorMessage = "At least one content ID is required.")]
        public string ContentIDs
        {
            get;
            set;
        }



        private string contentNames;
        [Category("EPMProperty"), Description("Name of content(s) to be displayed."), Required(ErrorMessage = "ContnetNames is required.")]
        public string ContentNames
        {
            get;
            set;
        }

        private ContentSortBy contentItemOrder = ContentSortBy.PostDate;
        [Category("EPMProperty"), Description("PostDate = By Post Date / ViewInDays = By Number of Views in the defined number of days."), Required(ErrorMessage = "ContentItemOrder is required.")]
        public ContentSortBy ContentItemOrder
        {
            get;
            set;
        }

        private int numOfDaysForItemOrder = 7;
        [Category("EPMProperty"), Description("Number of days that will be used to sort the content items."), DefaultValue(typeof(System.Int32), "7"), Required(ErrorMessage = "ContentItemOrder is required.")]
        public int NumOfDaysForItemOrder
        {
            get;
            set;
        }

        public enum ContentSortBy
        {
            PostDate = 0,
            ViewInDays = 1
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
}