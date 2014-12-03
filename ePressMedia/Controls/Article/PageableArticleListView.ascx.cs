using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Article
{
    [Description("Pageable Listview with multiple categories")]
    public partial class PageableArticleListView : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] cat_array = ContentIDs.Split(',');
                List<string> category_list;
                if (RandomRotation)
                {
                    Random rnd = new Random();
                    string[] MyRandomArray = cat_array.OrderBy(x => rnd.Next()).ToArray();


                    category_list = MyRandomArray.ToList();
                }
                else
                {

                    category_list = cat_array.ToList();
                }
        
                Category_Repeater.DataSource = category_list;
                Category_Repeater.DataBind();


                if (HeaderName.EndsWith(".jpg") || HeaderName.EndsWith(".png") || HeaderName.EndsWith(".bmp") || HeaderName.EndsWith(".gif"))
                {
                    img_title.Visible = true;
                    img_title.Src = HeaderName;
                }
                else
                {
                    lbl_header.Visible = true;
                    lbl_header.Text = HeaderName;
                }

             
            }
        }


        void ArticleLoad(int CategoryID, RepeaterItemEventArgs e)
        {

            IQueryable<ArticleModel.Article> articles = EPM.Business.Model.Article.ArticleContoller.GetArticlesByCatIDs(CategoryID.ToString(), NumOfItems, ShowThumbnailArticlesOnly);


            if (ContentItemOrder == ContentSortBy.ViewInDays)
                articles = articles.Where(c => c.PostDate >= DateTime.Now.AddDays(NumOfDaysForItemOrder*-1)).OrderByDescending(c => c.ViewCount);//.Take(NumOfItems);
            else // Default Post Date
                articles = articles.OrderByDescending(c => c.PostDate);//.Take(NumOfItems);

            Repeater rep = e.Item.FindControl("PageableArticleListView_Repeater") as Repeater;
            rep.DataSource = articles;
            rep.DataBind();



        }



        private string headerName;
        [Category("EPMProperty"), Description("Name displayed at the top of the control"), Required(ErrorMessage = "HeaderName is required")]
        public string HeaderName
        {
            get { return headerName; }
            set { headerName = value; }
        }


        private string contentIDs;
        [Category("EPMProperty"), Description("Content IDs to be linked to the control"), Required(ErrorMessage = "At least one content ID is required.")]
        public string ContentIDs
        {
            get { return contentIDs; }
            set { contentIDs = value; }
        }

        private int numOfItems = 1;
        [Category("EPMProperty"), Description("Number of items to be listed in each section"), DefaultValue(typeof(System.Int32), "1"), Required(ErrorMessage = "Number of items is required")]
        public int NumOfItems
        {
            get { return numOfItems; }
            set { numOfItems = value; }
        }



        private ContentSortBy contentItemOrder = ContentSortBy.PostDate;
        [Category("EPMProperty"), Description("PostDate = By Post Date / ViewInDays = By Number of Views in the defined number of days."), Required(ErrorMessage = "ContentItemOrder is required.")]
        public ContentSortBy ContentItemOrder
        {
            get { return contentItemOrder; }
            set { contentItemOrder = value; }
        }

        private int numOfDaysForItemOrder = 7;
        [Category("EPMProperty"), Description("Number of days that will be used to sort the content items."), DefaultValue(typeof(System.Int32), "7"), Required(ErrorMessage = "ContentItemOrder is required.")]
        public int NumOfDaysForItemOrder
        {
            get { return numOfDaysForItemOrder; }
            set { numOfDaysForItemOrder = value; }
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

        private bool randomRotation = false;
        [Category("EPMProperty"), Description("Rotate Content IDs entered"), DefaultValue(typeof(System.Boolean), "false"), Required(ErrorMessage = "Random Rotation is required.")]
        public bool RandomRotation
        {
            get { return randomRotation; }
            set { randomRotation = value; }
        }

        private int numOfDescriptionChar = 120;
        [Category("EPMProperty"), Description("Number of characters to show in description"), DefaultValue(typeof(System.Int32), "120"), Required()]
        public int NumOfDescriptionChar
        {
            get { return numOfDescriptionChar; }
            set { numOfDescriptionChar = value; }
        }

        private bool showThumbnailArticlesOnly = true;
        [Category("EPMProperty"), Description("Show articles that contain a thumbnail image only"), DefaultValue(typeof(System.Boolean), "true"), Required()]
        public bool ShowThumbnailArticlesOnly
        {
            get { return showThumbnailArticlesOnly; }
            set { showThumbnailArticlesOnly = value; }
        }


        protected void PageableArticleListView_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
    e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article article = (ArticleModel.Article)(e.Item.DataItem);

                HyperLink ViewImageLink = e.Item.FindControl("ViewImageLink") as HyperLink;                
                HyperLink ViewContentLink = e.Item.FindControl("ViewContentLink") as HyperLink;
                Literal lbl_abstract = e.Item.FindControl("lbl_abstract") as Literal;

                lbl_abstract.Text = EPM.Legacy.Common.Utility.TruncateStringByWord(article.Abstract, NumOfDescriptionChar);
                EPM.Core.Common.BindHelper.ImageBinder(e, "Thumb", article.ArticleId, ThumbnailTypeString);

                ViewImageLink.NavigateUrl = "~/Article/view.aspx?p="+article.CategoryId.ToString()+"&&aid=" + article.ArticleId.ToString();
                ViewContentLink.NavigateUrl = "~/Article/view.aspx?p=" + article.CategoryId.ToString() + "&&aid=" + article.ArticleId.ToString();
                


            }
        }

        protected void Category_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
e.Item.ItemType == ListItemType.AlternatingItem)
            {

                int CatId = int.Parse((String)(e.Item.DataItem));
                Label ArtCatName = e.Item.FindControl("ArtCatName") as Label;
                HyperLink CatLink = e.Item.FindControl("CatLink") as HyperLink;
                CatLink.NavigateUrl = "~/article/list.aspx?p=" + CatId.ToString();


                ArtCatName.Text = (from c in context.ArticleCategories
                              where c.ArtCatId == CatId
                              select c).Single().CatName.ToString();

                ArticleLoad(CatId, e);

            }
        }



    }
}