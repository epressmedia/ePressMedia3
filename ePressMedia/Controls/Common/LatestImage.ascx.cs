using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Description("Recent Images from (Text Only)")]
public partial class Controls_LatestImage : System.Web.UI.UserControl
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {

        }
        if (string.IsNullOrEmpty(ContentID.ToString()))
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
        
        if (ContentType == ContentTypes.Article)
        {
            var article = (from c in context.Articles
                                                  where c.CategoryId == ContentID && c.Suspended == false && c.IssueDate < DateTime.Now && c.Thumb_Path.Length > 0
                                                  orderby c.IssueDate descending
                                                  select c).Take(1);


            if (article.Count() == 1)
            {
                LatestImage_Img.ImageUrl = article.ToList()[0].Thumb_Path;
                LatestImage_Img.NavigateUrl =  "~/Article/view.aspx?p=" + article.ToList()[0].CategoryId.ToString() + "&aid=" + article.ToList()[0].ArticleId.ToString();
                LatestImage_Img.ToolTip = article.ToList()[0].Title;
            }
            else
            {
                lbl_empty.Visible = true;
            }

        }
        else if (ContentType == ContentTypes.Forum)
        {
            var forum = (from c in context.ForumThreads
                                            where c.ForumId == ContentID && c.Suspended == false && c.PostDate < DateTime.Now && c.Thumb.Length > 0
                                            orderby c.PostDate descending
                                            select c).Take(1);


            if (forum.Count() == 1)
            {
                LatestImage_Img.ImageUrl = forum.ToList()[0].Thumb;
                LatestImage_Img.NavigateUrl = "~/Forum/view.aspx?p=" + forum.ToList()[0].ForumId.ToString() + "&tid=" + forum.ToList()[0].ThreadId.ToString();
                LatestImage_Img.ToolTip = forum.ToList()[0].Subject;
            }
            else
            {
                lbl_empty.Visible = true;
            }

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


    private int contentID;
    [Category("EPMProperty"), Description("A single Content ID to be linked to the control"), Required(ErrorMessage = "content ID is required.")]
    public int ContentID
    {
        get { return contentID; }
        set { contentID = value; }
    }



    private ContentTypes contentType;
    [Category("EPMProperty"), Description("Content Type to be linked"), Required(ErrorMessage = "Content Type is required.")]
    public ContentTypes ContentType
    {
        get { return contentType; }
        set { contentType = value; }
    }



    public enum ContentTypes
    {
        Article = 0,
        Forum = 1
    }


}