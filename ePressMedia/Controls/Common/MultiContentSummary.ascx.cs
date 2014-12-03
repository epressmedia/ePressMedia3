using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPM.Core.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Description("Article/Forum/Classified/Biz Contents Summary (Text Only)")]
public partial class Controls_Common_MultiContentSummary : System.Web.UI.UserControl
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


         


        }
        LoadContentHeader();
        lbl_header.Text = HeaderName;
        HeadMoreLink.HRef = MoreLink;
    }



    void LoadContentHeader()
    {
        Dictionary<int, string> contDic = new Dictionary<int, string>();
        String[] contIDs = ContentIDs.Split(',');


        String[] contNames = ContentNames.Split(',');
        for (int i = 0; i < contIDs.Count(); i++)
        {
            contDic.Add(int.Parse(contIDs[i].ToString().Trim()), contNames[i].ToString().Trim());
        }


        ContentRepeater.DataSource = contDic;
        ContentRepeater.DataBind();

        ContentItemRepeater.DataSource = contDic;
        ContentItemRepeater.DataBind();

        if (contIDs.Count() == 1)
            ContentRepeater.Visible = false;

    }

    public static Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }


        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }


        return null;
    }

    void ArticleLoad(int CategoryID, Repeater ContentItemSubRepeater)
    {

        var articles = (from c in context.Articles
                        where c.CategoryId == CategoryID && c.Suspended == false && c.IssueDate < DateTime.Now
                        select c);



        if (ContentItemOrder == ContentSortBy.ViewInDays)
            articles = articles.Where(c => c.IssueDate >= DateTime.Now.AddDays(NumOfDaysForItemOrder * -1)).OrderBy(c => c.ViewCount).Take(NumOfItems);
        else // Default Post Date
            articles = articles.OrderByDescending(c => c.IssueDate).Take(NumOfItems);


        ContentItemSubRepeater.DataSource = articles;
        ContentItemSubRepeater.DataBind();


    }

    void ForumLoad(int CategoryID, Repeater ContentItemSubRepeater)
    {
        var forums = (from c in context.ForumThreads
                      where c.ForumId == CategoryID && c.Suspended == false && c.PostDate < DateTime.Now
                      select c);



        if (ContentItemOrder == ContentSortBy.ViewInDays)
            forums = forums.Where(c => c.PostDate <= DateTime.Now.AddDays(NumOfDaysForItemOrder * -1)).OrderBy(c => c.ViewCount).Take(NumOfItems);
        else // Default Post Date
            forums = forums.OrderByDescending(c => c.PostDate).Take(NumOfItems);


        ContentItemSubRepeater.DataSource = forums;
        ContentItemSubRepeater.DataBind();
    }


    void ClassifiedLoad(int CategoryID, Repeater ContentItemSubRepeater)
    {
        var classifieds = (from c in context.ClassifiedAds
                           where c.Category == CategoryID && c.Suspended == false && c.RegDate < DateTime.Now
                           select c);



        if (ContentItemOrder == ContentSortBy.ViewInDays)
            classifieds = classifieds.Where(c => c.RegDate <= DateTime.Now.AddDays(NumOfDaysForItemOrder * -1)).OrderByDescending(c => c.ViewCount).Take(NumOfItems);
        else // Default Post Date
            classifieds = classifieds.OrderByDescending(c => c.RegDate).Take(NumOfItems);


        ContentItemSubRepeater.DataSource = classifieds;
        ContentItemSubRepeater.DataBind();
    }

    void BizLoad(int CategoryID, Repeater ContentItemSubRepeater)
    {
        var biz = EPM.Business.Model.Biz.BEController.GetALLActiveBEs().Where(c => c.ApprovedFg == true);

        biz = biz.OrderByDescending(c => c.CreatedDate).Take(NumOfItems);


        ContentItemSubRepeater.DataSource = biz;
        ContentItemSubRepeater.DataBind();
    }



    private string headerName;
    [Category("EPMProperty"), Description("Name displayed at the top of the control"), Required(ErrorMessage = "HeaderName is required")]
    public string HeaderName
    {
        get { return headerName; }
        set { headerName = value; }
    }

    private string moreLink;
    [Category("EPMProperty"), Description("(Optional)Redirect URL of the header text."), Display(Name="Header More Link")]
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

    private string moreLinkText;
    [Category("EPMProperty"), Description("Text or Image to be display in the \"More\" link")]
    public string MoreLinkText
    {
        get { return moreLinkText; }
        set { moreLinkText = value; }
    }



    private int numOfItems = 10;
    [Category("EPMProperty"), Description("Number of itmes to be listed in each section"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Number of items is required")]
    public int NumOfItems
    {
        get { return numOfItems; }
        set { numOfItems = value; }
    }

    private string contentIDs;
    [Category("EPMProperty"), Description("Content IDs to be linked to the control"), Required(ErrorMessage = "At least one content ID is required."), PropertyGroup("ContentList")]
    public string ContentIDs
    {
        get { return contentIDs; }
        set { contentIDs = value; }
    }

    private ContentTypes contentType;
    [Category("EPMProperty"), Description("Content Type to be linked"), Required(ErrorMessage = "Content Type is required.")]
    public ContentTypes ContentType
    {
        get { return contentType; }
        set { contentType = value; }
    }

    private string contentNames;
    [Category("EPMProperty"), Description("Name of content(s) to be displayed."), Required(ErrorMessage = "ContnetNames is required."), PropertyGroup("ContentList")]
    public string ContentNames
    {
        get { return contentNames; }
        set { contentNames = value; }
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
        Classified = 2,
        Biz = 3
    }



    protected void ContentItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
        {
            Repeater ContentItemSubRepeater = (Repeater)item.FindControl("ContentItemSubRepeater");

            string CatID = ((System.Collections.Generic.KeyValuePair<int, string>)(item.DataItem)).Key.ToString();
            if (ContentTypes.Article == ContentType)
                ArticleLoad(int.Parse(CatID), ContentItemSubRepeater);
            else if (ContentTypes.Forum == ContentType)
                ForumLoad(int.Parse(CatID), ContentItemSubRepeater);
            else if (ContentTypes.Classified == ContentType)
                ClassifiedLoad(int.Parse(CatID), ContentItemSubRepeater);
            else if (ContentTypes.Biz == ContentType)
                BizLoad(int.Parse(CatID), ContentItemSubRepeater);

            // process the more link
            // check if it is image type
            bool ImageType = false;
            if (!string.IsNullOrEmpty(MoreLinkText))
            {
                HtmlGenericControl MultiContentSummary_More = e.Item.FindControl("MultiContentSummary_More") as HtmlGenericControl;
                MultiContentSummary_More.Visible = true;
                string[] imageArray = { ".png", ".jpg", ".gif", ".bmp" };
                foreach (string x in imageArray)
                {
                    if (MoreLinkText.Contains(x))
                        ImageType = true;
                }

                Literal lt_more = e.Item.FindControl("lt_more") as Literal;
                if (ImageType)
                    lt_more.Text = "<img src=\"" + moreLinkText + "\" target=\"" + MoreLinkType + "\" />";
                else
                    lt_more.Text = MoreLinkText;

                HyperLink hc = e.Item.FindControl("MultiContentSummary_MoreLink") as HyperLink;

                if (ContentTypes.Article == ContentType)
                    hc.NavigateUrl = "/Article/list.aspx?p=" + CatID.ToString();
                else if (ContentTypes.Forum == ContentType)
                    hc.NavigateUrl = "/Forum/list.aspx?p=" + CatID.ToString();
                else if (ContentTypes.Classified == ContentType)
                    hc.NavigateUrl = "~/Classified/list.aspx?p=" + CatID.ToString();
                else if (ContentTypes.Biz == ContentType)
                    hc.NavigateUrl = "~/Biz/list.aspx";

            }
        

        }

    }

    protected void ContentItemSubRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hc = e.Item.FindControl("itemLink") as HyperLink;
            Label itemtitle = e.Item.FindControl("itemTitle") as Label;

            if (ContentTypes.Article == ContentType)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                itemtitle.Text = a.Title;
                hc.NavigateUrl = "/Article/view.aspx?p=" + a.CategoryId + "&aid=" + a.ArticleId;
            }
            else if (ContentTypes.Forum == ContentType)
            {
                ForumModel.ForumThread f = e.Item.DataItem as ForumModel.ForumThread;
                itemtitle.Text = f.Subject;
                hc.NavigateUrl = "/Forum/view.aspx?p=" + f.ForumId + "&tid=" + f.ThreadId;
            }
            else if (ContentTypes.Classified == ContentType)
            {
                ClassifiedModel.ClassifiedAd c = e.Item.DataItem as ClassifiedModel.ClassifiedAd;
                itemtitle.Text = c.Subject;
                hc.NavigateUrl = "~/Classified/view.aspx?p=" + c.Category.ToString() + "&aid=" + c.AdId.ToString();
            }
            else if (ContentTypes.Biz == ContentType)
            {
                BizModel.BusinessEntity c = e.Item.DataItem as BizModel.BusinessEntity;
                itemtitle.Text = c.PrimaryName;
                hc.NavigateUrl = "~/Biz/viewbiz.aspx?id=" + c.BusinessEntityId.ToString();
            }

        }
       


        
    }
}

