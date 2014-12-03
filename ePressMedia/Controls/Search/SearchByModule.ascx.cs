using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core.Classified;
using EPM.Legacy.Data;
using EPM.Business.Model.Search;


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Search
{
    [Description("Extended Search Result Listing by Module")]
    public partial class SearchByModule : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["q"] == null)
                    return;
                FitPager1.CurrentPage = 1;
                if (ContentType == ContentTypes.Article)
                    bindArticleData(true);
                else if (ContentType == ContentTypes.Forum)
                    bindForumData(true);
                else if (ContentType == ContentTypes.Classified)
                    bindClassifiedData(true);
            }
        }

        void bindArticleData(bool newbind)
    {
        if (Request.QueryString["q"].Trim() == string.Empty)
            return;

        List<ArticleModel.Article> results = SearchController.SearchArticle(Request.QueryString["q"].Trim());

        if (newbind)
            FitPager1.TotalRows = results.Count();
        FitPager1.Visible = (FitPager1.TotalRows > FitPager1.RowsPerPage);
        ItemCount.Text = FitPager1.TotalRows.ToString();
        SearchItemRepeater.DataSource = results.Skip((FitPager1.CurrentPage - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);
        SearchItemRepeater.DataBind();
    }

        void bindForumData(bool newbind)
        {
            if (Request.QueryString["q"].Trim() == string.Empty)
                return;

            var results = EPM.Business.Model.Search.SearchController.SearchForum(Request.QueryString["q"].Trim());

            if (newbind)
                FitPager1.TotalRows = results.Count();
            FitPager1.Visible = (FitPager1.TotalRows > FitPager1.RowsPerPage);
            ItemCount.Text = FitPager1.TotalRows.ToString();
            SearchItemRepeater.DataSource = results.Skip((FitPager1.CurrentPage - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);
            SearchItemRepeater.DataBind();
        }


        void bindClassifiedData(bool newbind)
        {
            if (Request.QueryString["q"].Trim() == string.Empty)
                return;

            var results = EPM.Business.Model.Search.SearchController.SearchClassified(Request.QueryString["q"].Trim());

            if (newbind)
                FitPager1.TotalRows = results.Count();
            FitPager1.Visible = (FitPager1.TotalRows > FitPager1.RowsPerPage);
            ItemCount.Text = FitPager1.TotalRows.ToString();
            SearchItemRepeater.DataSource = results.Skip((FitPager1.CurrentPage - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);
            SearchItemRepeater.DataBind();
        }





    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        if (ContentType == ContentTypes.Article)
            bindArticleData(false);
        else if (ContentType == ContentTypes.Forum)
            bindForumData(false);
        else if (ContentType == ContentTypes.Classified)
            bindClassifiedData(false);

    }

    protected void SearchItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem ||
            e.Item.ItemType == ListItemType.Item)
        {
            if (ContentType == ContentTypes.Article)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                if (!EPM.Business.Model.Article.ArticleThumbnailController.ArticleThumbnailExists(a.ArticleId))
                {
                    Image ThumbImg = e.Item.FindControl("ThumbImg") as Image;
                    ThumbImg.Visible = false;
                }
                else
                    EPM.Core.Common.BindHelper.ImageBinder(e, "ThumbImg", a.ArticleId, ThumbnailTypeString,a.Title);
                HyperLink ViewLink = e.Item.FindControl("ViewLink") as HyperLink;
                ViewLink.NavigateUrl = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                ViewLink.Text = a.Title;

                Literal Abstract = e.Item.FindControl("Abstract") as Literal;
                Abstract.Text = EPM.Legacy.Common.Utility.TruncateStringByWord(a.Abstract, NumberChar);
            }
            else if (ContentType == ContentTypes.Forum)
            {
                ForumModel.ForumThread a = e.Item.DataItem as ForumModel.ForumThread;
                Image ThumbImg = e.Item.FindControl("ThumbImg") as Image;
                ThumbImg.Visible = false;                
                HyperLink ViewLink = e.Item.FindControl("ViewLink") as HyperLink;
                ViewLink.NavigateUrl =  "~/Forum/view.aspx?p=" + a.ForumId.ToString() + "&tid=" + a.ThreadId.ToString();
                ViewLink.Text = a.Subject;
                Literal Abstract = e.Item.FindControl("Abstract") as Literal;
                string stripText = EPM.Core.WebHelper.StripTagsCharArray(a.Body);
                Abstract.Text = stripText.Length > NumberChar ? stripText.Substring(0, NumberChar) : stripText;
            }
            if (ContentType == ContentTypes.Classified)
            {
                ClassifiedModel.ClassifiedAd a = e.Item.DataItem as ClassifiedModel.ClassifiedAd;
                Image ThumbImg = e.Item.FindControl("ThumbImg") as Image;
                Literal Abstract = e.Item.FindControl("Abstract") as Literal;
                HyperLink ViewLink = e.Item.FindControl("ViewLink") as HyperLink;

                    if (string.IsNullOrEmpty(a.Thumb))
                        ThumbImg.Visible = false;
                    else
                    {
                        ThumbImg.ImageUrl = a.Thumb;
                        ThumbImg.AlternateText = a.Subject;
                    }

                    ViewLink.NavigateUrl = "~/Classified/view.aspx?p="+a.Category.ToString() + "&aid=" + a.AdId.ToString();
                    ViewLink.Text = a.Subject;
                    string stripText = EPM.Core.WebHelper.StripTagsCharArray(a.Description);
                    Abstract.Text = stripText.Length > NumberChar ? stripText.Substring(0, NumberChar) : stripText;
                
                
            }

        }
    }
    private ContentTypes contentType;
    [Category("EPMProperty"), Description("Content Type to be linked"), Required(ErrorMessage = "Content Type is required.")]
    public ContentTypes ContentType
    {
        get;
        set;
    }

    public enum ContentTypes
    {
        Article = 0,
        Forum = 1,
        Classified = 2
    }


    private int numberChar = 90;
    [Category("EPMProperty"), Description("Number of Characters showing in the description"), Required(ErrorMessage = "Number of Characters is required"), DefaultValue(typeof(System.Int32), "90")]
    public int NumberChar
    {
        get { return numberChar;  }
        set { numberChar = value; }
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