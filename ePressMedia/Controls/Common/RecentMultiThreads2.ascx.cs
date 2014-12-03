using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Description("Recent Article/Forum Contents Summary (with Image)")]
public partial class Controls_RecentMultiThreads2 : System.Web.UI.UserControl
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

    private int Counter = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
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


            //lbl_title.Text = HeaderName;
            LoadContent();
        }
    }

    void LoadContent()
    {
        var CatIds = ContentIDs.Split(',').ToList();
        if (ContentType == ContentTypes.Article)
        {
            IQueryable<ArticleModel.Article> articleQuery;
            articleQuery = (from c in context.Articles
                         where CatIds.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate < DateTime.Now
                         orderby c.PostDate descending
                         select c);

            if (ImageView)
                articleQuery = articleQuery.Where(c => c.Thumb_Path.Length > 0);
            

            Repeater1.DataSource = articleQuery.Take(NumOfItems);
            Repeater1.DataBind();
        }
        else if (ContentType == ContentTypes.Forum)
        {
            IQueryable<ForumModel.ForumThread> forumQuery;
            forumQuery = (from c in context.ForumThreads
                         where CatIds.Contains(c.ForumId.ToString()) && c.Suspended == false && c.Announce == false
                         orderby c.PostDate descending
                         select c);

            if (ImageView)
                forumQuery = forumQuery.Where(c => c.Thumb.Length > 0);

            Repeater2.DataSource = forumQuery.Take(NumOfItems);
            Repeater2.DataBind();

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
                if (ContentType == ContentTypes.Article)
                {
                    ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                    if (ShowCategoryName)
                    {
                        Label lbl_catname = e.Item.FindControl("lbl_catName") as Label;
                        lbl_catname.Text = "[" + a.ArticleCategory1.CatName + "]";

                    }
                    anchor.HRef = "~/Article/ViewArticle.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();

                    if (ImageView)
                    {
                        Image img_thumb = (Image)e.Item.FindControl("img_thumb");
                        img_thumb.Visible = true;
                        img_thumb.ImageUrl = a.Thumb_Path;
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
                    anchor.HRef = "~/Forum/ViewThread.aspx?p=" + a.ForumId.ToString() + "&tid=" + a.ThreadId.ToString();
                }
                    
                Label lbl_number_count= (Label)e.Item.FindControl("number_count");
                Label lbl_text = (Label)e.Item.FindControl("lbl_contents");
                Label lbl_catName = (Label)e.Item.FindControl("lbl_catName");
                


                if (BulletType == BulletTypes.Bullet && !ImageView)
                {
                    lbl_number_count.Text = " &#8226;";
                }
                else if (BulletType == BulletTypes.Number && !ImageView)
                {
                    lbl_number_count.Text = " " + Counter + "   ";
                }

                Counter++;
            }
        }
        catch
        {
        }
    }


    private string headerName;
    [Category("KNNProperty"), Description("Name displayed at the top of the control"), Required(ErrorMessage = "HeaderName is required")]
    public string HeaderName
    {
        get;
        set;
    }

    private string moreLink;
    [Category("KNNProperty"), Description("(Optional)URL of the page which will be open when the More button is clicked.")]
    public string MoreLink
    {
        get;
        set;
    }

    private string moreLinkType = "_self";
    [Category("KNNProperty"), Description("(Optional)More link open type"), DefaultValue(typeof(System.String), "_self")]
    public string MoreLinkType
    {
        get;
        set;
    }

    private int numOfItems = 10;
    [Category("KNNProperty"), Description("Number of itmes to be listed in each section"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Number of items is required")]
    public int NumOfItems
    {
        get;
        set;
    }

    private string contentIDs;
    [Category("KNNProperty"), Description("Content IDs to be linked to the control"), Required(ErrorMessage = "At least one content ID is required.")]
    public string ContentIDs
    {
        get;
        set;
    }

    private bool showCategoryName = false;
    [Category("KNNProperty"), Description("Category Name to show in each line."), DefaultValue(typeof(System.Boolean), "false")]
    public bool ShowCategoryName
    {
        get;
        set;
    }

    private bool imageView = false;
    [Category("KNNProperty"), Description("Determine to show the thumbnais"), DefaultValue(typeof(System.Boolean), "false")]
    public bool ImageView
    {
        get;
        set;
    }

    private ContentTypes contentType;
    [Category("KNNProperty"), Description("Content Type to be linked"), Required(ErrorMessage = "Content Type is required.")]
    public ContentTypes ContentType
    {
        get;
        set;
    }

    private string contentNames;
    [Category("KNNProperty"), Description("Name of content(s) to be displayed."), Required(ErrorMessage = "ContnetNames is required.")]
    public string ContentNames
    {
        get;
        set;
    }

    private BulletTypes bulletType = BulletTypes.None;
    [Category("KNNProperty"), Description("Bullet Type to show at the front of each line."), DefaultValue(typeof(BulletTypes), "None")]
    public BulletTypes BulletType
    {
        get;
        set;
    }


    public enum ContentTypes
    {
        Article = 0,
        Forum = 1
    }

    public enum BulletTypes
    {
        None,
        Bullet,
        Number
        
    }
}