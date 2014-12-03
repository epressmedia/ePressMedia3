using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Description("Recent Article/Forum/Classified Contents Summary (Text Only)")]
public partial class Controls_RecentMultiThreads : System.Web.UI.UserControl
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

    private int Counter = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {

        }
        if (string.IsNullOrEmpty(ContentIDs))
            return;

        if (HeaderName.EndsWith(".jpg") || HeaderName.EndsWith(".png") || HeaderName.EndsWith(".bmp") || HeaderName.EndsWith(".gif"))
        {
            img_title.Visible = true;
            img_title.Src = HeaderName;
        }
        else
        {
            lbl_title.Visible = true;
            lbl_title.Text = HeaderName;
        }

        LoadContent();
    }

    void LoadContent()
    {
        //var CatIds = ContentIDs.Split(',').ToList();
        DateTime targetdate = DateTime.Now.AddDays(NumOfDaysForItemOrder * -1);
        if (ContentType == ContentTypes.Article)
        {
            
            if (ContentItemOrder == ContentSortBy.ViewInDays)
            {


                //var query1 = (from c in context.Articles
                //              where ContentIDs.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate >= targetdate
                //              orderby c.ViewCount descending
                //              select c).Take(NumOfItems);
                //var query2 = (from a in context.Articles
                //              join vc in context.VirtualCategoryLinks on a.ArticleId equals vc.ArticleId
                //              where ContentIDs.Contains(vc.CatId.ToString()) && a.Suspended == false && a.IssueDate >= targetdate
                //              orderby a.ViewCount descending
                //              select a).Take(NumOfItems);
                ////
                //var query = (from n in query1
                //             select n).Union(from n2 in query2 select n2).OrderByDescending(c => c.ViewCount).Take(NumOfItems);
                //var result = query;
                //var result = EPM.Business.Model.Article.ArticleContoller.GetArticlesByCatIDs(ContentIDs, 2000).Where(c => c.IssueDate >= targetdate).OrderByDescending(c => c.ViewCount).Take(NumOfItems);

                var result = EPM.Business.Model.Article.ArticleContoller.GetArticleByCatIDsOrderByViewInDays(ContentIDs, NumOfItems, targetdate);

                if (result != null)
                {
                    Repeater1.DataSource = result;
                    Repeater1.DataBind();
                }
                else
                {
                    lbl_empty.Visible = true;
                }
            }
            else
            {
                //var query1 = (from c in context.Articles
                //              where ContentIDs.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate < DateTime.Now
                //              orderby c.IssueDate descending
                //              select c).Take(NumOfItems);
                //var query2 = (from a in context.Articles
                //              join vc in context.VirtualCategoryLinks on a.ArticleId equals vc.ArticleId
                //              where ContentIDs.Contains(vc.CatId.ToString()) && a.Suspended == false && a.IssueDate < DateTime.Now
                //              orderby a.IssueDate descending
                //              select a).Take(NumOfItems);

                //var query = (from n in query1
                //             select n).Union(from n2 in query2 select n2).OrderByDescending(c => c.IssueDate).Take(NumOfItems);

                //var result = query;
                //var result = EPM.Business.Model.Article.ArticleContoller.GetArticlesByCatIDs(ContentIDs, 2000).OrderByDescending(c => c.IssueDate).Take(NumOfItems);

                var result = EPM.Business.Model.Article.ArticleContoller.GetArticleByCatIDsOrderByPostDate(ContentIDs, NumOfItems);
                

                if (result != null)
                {
                    Repeater1.DataSource = result;
                    Repeater1.DataBind();
                }
                else
                {
                    lbl_empty.Visible = true;
                }
            }

        }
        else if (ContentType == ContentTypes.Forum)
        {

            if (ContentItemOrder == ContentSortBy.PostDate)
            {
                //var query = (from c in context.ForumThreads
                //             where CatIds.Contains(c.ForumId.ToString()) && c.Suspended == false && c.Announce == false
                //             orderby c.PostDate descending
                //             select c).Take(NumOfItems);

                var query = EPM.Business.Model.Forum.ForumController.GetThreadsByForumIds(ContentIDs).OrderByDescending(c => c.PostDate).Take(NumOfItems);

                if (query != null)
                {
                    Repeater2.DataSource = query;
                    Repeater2.DataBind();
                }
                else
                {
                    lbl_empty.Visible = true;
                }
            }
            else
            {
                //var query = (from c in context.ForumThreads
                //             where CatIds.Contains(c.ForumId.ToString()) && c.Suspended == false && c.Announce == false
                //             orderby c.ViewCount descending
                //             select c).Take(NumOfItems);

                var query = EPM.Business.Model.Forum.ForumController.GetThreadsByForumIds(ContentIDs).Where(c => c.PostDate >= targetdate).OrderByDescending(c => c.ViewCount).Take(NumOfItems);

                if (query.Count() > 0)
                {
                    Repeater2.DataSource = query;
                    Repeater2.DataBind();
                }
                else
                {
                    lbl_empty.Visible = true;
                }
            }

        }
        else if (ContentType == ContentTypes.Classified)
        {
            if (ContentItemOrder == ContentSortBy.PostDate)
            {
                //var query = (from c in context.ClassifiedAds
                //             where CatIds.Contains(c.Category.ToString()) && c.Suspended == false && c.Announce == false
                //             orderby c.LastUpdate descending
                //             select c).Take(NumOfItems);
                var query = EPM.Business.Model.Classified.ClassifiedController.GetClassifiedAdsByCategoryIds(ContentIDs).OrderByDescending(c=>c.LastUpdate).Take(NumOfItems);

                if (query.Count() > 0)
                {
                    Repeater2.DataSource = query;
                    Repeater2.DataBind();
                }
                else
                {
                    lbl_empty.Visible = true;
                }
            }
            else
            {
                var query = EPM.Business.Model.Classified.ClassifiedController.GetClassifiedAdsByCategoryIds(ContentIDs).Where(c => c.RegDate >= targetdate).OrderByDescending(c => c.ViewCount).Take(NumOfItems);

                if (query.Count() > 0)
                {
                    Repeater2.DataSource = query;
                    Repeater2.DataBind();
                }
                else
                {
                    lbl_empty.Visible = true;
                }
            }

        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {


                HtmlAnchor anchor = (HtmlAnchor)e.Item.FindControl("art_link");
                string image_thumb = "";
                if (ContentType == ContentTypes.Article)
                {
                    ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                    if (ShowCategoryName)
                    {
                        Label lbl_catname = e.Item.FindControl("lbl_catName") as Label;
                        lbl_catname.Text = "[" + a.ArticleCategory1.CatName + "]";

                    }
                    anchor.HRef = "~/Article/view.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                    if (a.Thumb_Path != null)
                    {
                        if (a.Thumb_Path.Length > 0)
                            image_thumb = a.Thumb_Path;
                    }
                }
                else if (ContentType == ContentTypes.Forum)
                {
                    ForumModel.ForumThread a = e.Item.DataItem as ForumModel.ForumThread;
                    if (ShowCategoryName)
                    {
                        Label lbl_catname = e.Item.FindControl("lbl_catName") as Label;
                        lbl_catname.Text = "[" + a.Forum.ForumName + "]";

                    }
                    anchor.HRef = "~/Forum/view.aspx?p=" + a.ForumId.ToString() + "&tid=" + a.ThreadId.ToString();

                    if (a.Thumb != null)
                    {
                        if (a.Thumb.Length > 0)
                            image_thumb = a.Thumb;
                    }
                }

                else if (ContentType == ContentTypes.Classified)
                {
                    ClassifiedModel.ClassifiedAd a = e.Item.DataItem as ClassifiedModel.ClassifiedAd;
                    
                    if (ShowCategoryName)
                    {
                        Label lbl_catname = e.Item.FindControl("lbl_catName") as Label;
                        lbl_catname.Text = "[" + a.ClassifiedCategory.CatName + "]";

                    }


                    string desc = EPM.Core.WebHelper.StripTagsCharArray(a.Description);
                    anchor.Title = desc.Length > 200 ? desc.Substring(0, 200) + "..." : desc;

                    anchor.HRef = "~/Classified/view.aspx?p=" + a.Category.ToString() + "&aid=" + a.AdId.ToString();

                    if (a.Thumb != null)
                    {
                        if (a.Thumb.Length > 0)
                            image_thumb = a.Thumb;
                    }
                }
                    
                Label lbl_number_count= (Label)e.Item.FindControl("number_count");
                Label lbl_text = (Label)e.Item.FindControl("lbl_contents");
                Label lbl_catName = (Label)e.Item.FindControl("lbl_catName");

                if (BulletType == BulletTypes.Bullet)
                {
                    lbl_number_count.Text = " &#8226;";
                }
                else if (BulletType == BulletTypes.Number)
                {
                    lbl_number_count.Text = " " + Counter + "   ";
                }
                else if (BulletType == BulletTypes.Image)
                {
                    if (image_thumb.Length > 0)
                        lbl_number_count.Text = "<img src='" + image_thumb + "' style='width:25px;height:20px;'/>";
                }

                Counter++;
            }
        }
        catch
        {
        }
    }


    private string headerName;
    [Category("EPMProperty"), Description("Name displayed at the top of the control"), Required(ErrorMessage = "HeaderName is required")]
    public string HeaderName
    {
        get { return headerName; }
        set { headerName = value; }
    }

    private string moreLink;
    [Category("EPMProperty"), Description("(Optional)URL of the page which will be open when the More button is clicked.")]
    public string MoreLink
    {
        get { return moreLink; }
        set { moreLink = value; }
    }

    private string moreLinkType = "_self";
    [Category("EPMProperty"), Description("(Optional)More link open type"), DefaultValue(typeof(System.String), "_self")]
    public string MoreLinkType
    {
        get { return moreLinkType; }
        set { moreLinkType = value; }
    }

    private int numOfItems = 10;
    [Category("EPMProperty"), Description("Number of itmes to be listed in each section"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Number of items is required")]
    public int NumOfItems
    {
        get { return numOfItems; }
        set { numOfItems = value; }
    }

    private string contentIDs;
    [Category("EPMProperty"), Description("Content IDs to be linked to the control"), Required(ErrorMessage = "At least one content ID is required.")]
    public string ContentIDs
    {
        get { return contentIDs; }
        set { contentIDs = value; }
    }

    private bool showCategoryName = false;
    [Category("EPMProperty"), Description("Category Name to show in each line."), DefaultValue(typeof(System.Boolean), "false")]
    public bool ShowCategoryName
    {
        get { return showCategoryName; }
        set { showCategoryName = value; }
    }

    private ContentTypes contentType;
    [Category("EPMProperty"), Description("Content Type to be linked"), Required(ErrorMessage = "Content Type is required.")]
    public ContentTypes ContentType
    {
        get { return contentType; }
        set { contentType = value; }
    }

    private string contentNames;
    [Category("EPMProperty"), Description("Name of content(s) to be displayed."), Required(ErrorMessage = "ContnetNames is required.")]
    public string ContentNames
    {
        get { return contentNames; }
        set { contentNames = value; }
    }

    private BulletTypes bulletType = BulletTypes.None;
    [Category("EPMProperty"), Description("Bullet Type to show at the front of each line."), DefaultValue(typeof(BulletTypes), "None")]
    public BulletTypes BulletType
    {
        get { return bulletType; }
        set { bulletType = value; }
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



    public enum ContentTypes
    {
        Article = 0,
        Forum = 1,
        Classified =2
    }

    public enum BulletTypes
    {
        None,
        Bullet,
        Number,
        Image
        
    }
}