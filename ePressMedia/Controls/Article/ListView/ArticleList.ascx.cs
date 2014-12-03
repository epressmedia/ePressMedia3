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
using EPM.Business.Model.Article;
using EPM.Core.Attributes;
using EPM.Data.Model;

[Description("Article Listing Control")]
public partial class Article_ArticleList : System.Web.UI.UserControl
{

    private static readonly ILog log = LogManager.GetLogger(typeof(Article_ArticleList));
    EPMEntityModel context = new EPMEntityModel();
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            try
            {

                bool override_CatId = false;
                string cat = "";

                if (Request.QueryString["p"] != null)
                    cat = Request.QueryString["p"].ToString();
                else if (Page.RouteData.Values["category"] != null)
                    cat = ArticleCategoryContoller.GetArticleCatIdByURLSlug(Page.RouteData.Values["category"].ToString()).ToString();


                if (!String.IsNullOrEmpty(CategoryIDs))
                {
                    cat = CategoryIDs;
                    override_CatId = true;
                }
                CatId.Value = cat.ToString();

                if (!ShowPager)
                    FitPager1.Visible = false;

                if (!ShowSearch)
                    article_search_panel.Visible = false;


                if (override_CatId)
                {
                    PostLink_popup.Visible = PostLink_popup2.Visible = false;

                    EntryPopup.Visible = EntryPopup.Enabled = false;
                    //WindowManager.Visible = ArticleEditor.Visible = false;
                    //WindowManager.Enabled = ArticleEditor.Enabled = false;
                }
                else
                {
                    var contIDs = cat.Split(',').ToList();

                    PostLink_popup.Visible = PostLink_popup2.Visible = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                    ResourceType.Article,int.Parse(contIDs[0]), Permission.Write);

                    //ArticleEditor.NavigateUrl = "
                    //string path = "/Article/ArticleEntry.aspx?p=" + cat + "&mode=Add";

                    string path = "/Page/DataEntry.aspx?p=" + cat + "&area=article&mode=Add";

                    string catName = context.ArticleCategories.SingleOrDefault(c => c.ArtCatId == int.Parse(cat)).CatName;
                    EntryPopup.Title = "New Article Entry - " + catName;
                    PostLink_popup.OnClientClick = PostLink_popup2.OnClientClick= EntryPopup.GetOpenPath(path);// "ShowDataEntry('" + path + "','" + catName + "'); return false;";
                }

                

                FitPager1.RowsPerPage = (RowPerPage > 0) ? RowPerPage : 10;// catSettings.RowsPerPage;// cfg.RowsPerPage;

                bindData();




            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        if (LightBoxType)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"        $(document).ready(function(){
    $(" + "\"" + "a[rel^='prettyPhoto']" + "\"" + ").prettyPhoto({allow_resize: true,default_width:500, default_height:344, horizontal_padding: 20});  });");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "lightbox", sb.ToString(), true);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {

    }

    void bindData()
    {
        try
        {
            int pageNum;
            //int cat = int.Parse(CatId.Value);

            var contIDs = CatId.Value.Split(',').ToList();

            if (FitPager1.CurrentPage > 0)
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





            bool admin = false;
            if (contIDs.Count == 1)
            {
               admin = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                 ResourceType.Article, int.Parse(contIDs[0]), Permission.FullControl);
            }

            bool search_query = false;
            if (Request.QueryString["q"] != null)
                search_query = true;

            IQueryable<ArticleModel.Article> articleQuery;

            articleQuery = ArticleContoller.GetArticlesByCatIDs(CatId.Value, admin,false);

            if (search_query)
            {
                string query_str = Request.QueryString["q"].ToString();
                articleQuery = articleQuery.Where(c => c.Title.Contains(query_str) || c.Body.Contains(query_str) || c.Reporter.Contains(query_str));
            }

            if (LightBoxType || PhotoListing)
            {
                articleQuery = articleQuery.Where(c => !string.IsNullOrEmpty(c.Thumb_Path) || !string.IsNullOrEmpty(c.VideoId));
            }

            FitPager1.TotalRows = articleQuery.Count();
            FitPager1.CurrentPage = pageNum;

            if (ShowPager)
                FitPager1.Visible = (FitPager1.TotalRows > 0);

            var newresult = articleQuery.Skip((pageNum - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);

            if (!PhotoListing)
            {
                if ((FitPager1.CurrentPage == 1) && (ShowHeadline))
                {

                    ArticleList_Headline.Visible = true;

                    ArtRepeater_Headline.DataSource = newresult.Take(NumberOfHeadLines);
                    ArtRepeater_Headline.DataBind();


                    ArtRepeater.DataSource = newresult.Skip(NumberOfHeadLines);
                    ArtRepeater.DataBind();
                }
                else
                {
                    ArticleList_Headline.Visible = false;
                    ArtRepeater.DataSource = newresult.ToList();
                    ArtRepeater.DataBind();
                }
            }
            else
            {
                ArticleList_Headline.Visible = false;
                ArtRepeater.Visible = false;
                DataImageRepeater.Visible = true;
                DataImageRepeater.DataSource = newresult;
                DataImageRepeater.DataBind();
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }

    }


    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        bindData();
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        string catId;
        if (Request.QueryString["p"] != null)
        {
            catId = Request.QueryString["p"].ToString();
        }
        else
        {
            catId = CategoryIDs;

        }


        Response.Redirect(string.Format("list.aspx?p={0}&q={1}", catId, Server.UrlEncode(SearchValue.Text.Trim())));
    }




    private bool showHeadline = false;
    [Category("EPMProperty"),DisplayName("Show Headline"), Description("Show headline rows"), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public bool ShowHeadline
    {
        get { return showHeadline; }
        set { showHeadline = value; }
    }

    private int numberOfHeadLines;
    [Category("EPMProperty"), DisplayName("Number of Headline Articles"), Description("Define number of articles shows in headline")]
    public int NumberOfHeadLines
    {
        get { return numberOfHeadLines; }
        set { numberOfHeadLines = value; }
    }

    private int rowPerPage = 10;
    [Category("EPMProperty"), DisplayName("Articles Per Page"), Description("Define number of articles per page"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Row Per Page is required.")]
    public int RowPerPage
    {
        get { return rowPerPage; }
        set { rowPerPage = value; }
    }

    private string categoryIds;
    [Category("EPMProperty"), DisplayName("Article Category IDs"), Description("(Optional) Override Category IDs")]
    public string CategoryIDs
    {
        get { return categoryIds; }
        set { categoryIds = value; }
    }

    private Boolean showPager = true;
    [Category("EPMProperty"), Description("Show Pager"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowPager
    {
        get { return showPager; }
        set { showPager = value; }
    }

    private Boolean showSearch = true;
    [Category("EPMProperty"), DisplayName("Show Search"), Description("Show Search"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowSearch
    {
        get { return showSearch; }
        set { showSearch = value; }
    }

    private int numOfDescriptionChar = 100;
    [Category("EPMProperty"), DisplayName("Number Characters in Description "), Description("Number of characters to show in description"), DefaultValue(typeof(System.Int32), "100"), Required()]
    public int NumOfDescriptionChar
    {
        get { return numOfDescriptionChar; }
        set { numOfDescriptionChar = value; }
    }


    private Boolean photoListing = false;
    [Category("EPMProperty"), DisplayName("Photo Listing"), Description("Show in Image View"), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public Boolean PhotoListing
    {
        get { return photoListing; }
        set { photoListing = value; }
    }

    private Boolean lightBoxType = false;
    [Category("EPMProperty"), DisplayName("Show in Lightbox"), Description("Show as Light Box Type"), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public Boolean LightBoxType
    {
        get { return lightBoxType; }
        set { lightBoxType = value; }
    }

    [Category("EPMProperty"), DisplayName("Show lightbox images as album gallery"), Description("Show all images in an article by clicking on the article"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean LightBoxByAlbumGallery
    {
        get;
        set;
    }

    private Dictionary<string, bool> thumbnailTypeHeadline = EPM.Business.Model.Common.ThumbnailTypes.GetThumbnailTypesDictionary();
    [Category("EPMProperty"), DisplayName("Thumbnail Type For Headline"), Description("Specify Thumbnail Type for headline articles"), DataType("ThumbnailType"), Assembly("EPM.Business.Model", "EPM.Business.Model.Common.ThumbnailTypes", "GetThumbnailTypesDictionary")]
    public Dictionary<string, bool> ThumbnailTypeHeadline
    {
        get { return thumbnailTypeHeadline; }
        set { thumbnailTypeHeadline = value; }
    }

    public string ThumbnailTypeHeadlineString
    {
        get { return ThumbnailTypeHeadline.Single(c => c.Value == true).Key.ToString(); }
    }

    private Dictionary<string, bool> thumbnailType = EPM.Business.Model.Common.ThumbnailTypes.GetThumbnailTypesDictionary();
    [Category("EPMProperty"), DisplayName("Thumbnail Type"), Description("Specify Thumbnail Type"), DataType("ThumbnailType"), Assembly("EPM.Business.Model", "EPM.Business.Model.Common.ThumbnailTypes", "GetThumbnailTypesDictionary"), Required()]
    public Dictionary<string, bool> ThumbnailType
    {
        get { return thumbnailType; }
        set { thumbnailType = value; }
    }

    public string ThumbnailTypeString
    {
        get { return ThumbnailType.Single(c => c.Value == true).Key.ToString(); }
    }

    private Boolean hideThumbnail = false;
    [Category("EPMProperty"), DisplayName("Hide Thumbnail"), Description("Hide thumbnail (This applies to listing view only."), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public Boolean HideThumbnail
    {
        get { return hideThumbnail; }
        set { hideThumbnail = value; }
    }


    private Boolean hightlightCurrentItem = false;
    [Category("EPMProperty"), DisplayName("Hightlight Current Item"), Description("Highlight the current item. Work with the detail control via CSS."), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public Boolean HightlightCurrentItem
    {
        get { return hightlightCurrentItem; }
        set { hightlightCurrentItem = value; }
    }

    







    protected void DataImageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {

            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article t = e.Item.DataItem as ArticleModel.Article;


                EPM.Core.Common.BindHelper.ImageBinder(e, "Img1", t.ArticleId, ThumbnailTypeString);



                HyperLink l1 = e.Item.FindControl("ViewLink1") as HyperLink;
                HyperLink l2 = e.Item.FindControl("ViewLink2") as HyperLink;
                //HtmlImage thumbimage = e.Item.FindControl("Img1") as HtmlImage;

                if (!LightBoxType)
                {
                    l1.NavigateUrl = l2.NavigateUrl =
                        string.Format("/Article/view.aspx?p={0}&page={1}&aid={2}&mode=p",
                                    t.CategoryId, FitPager1.CurrentPage, t.ArticleId);
                }
                else
                {
                    string VidioID = "";
                    List<String> Images = EPM.ImageUtil.EPMImageExtractUtil.GetImagesFromArticleBody(t.Body, true);
                    if (LightBoxByAlbumGallery)
                    {   string click_javascript = "";
                        string api_images="";
                        string api_title = "";
                    foreach (string image in Images)
                    {
                        if (image.Contains("http://img.youtube.com/vi/"))
                        {
                            VidioID = image.Replace("http://img.youtube.com/vi/", "").Replace("/0.jpg", "");
                            api_images = api_images + "'" + "http://www.youtube.com/watch?v=" + VidioID +"',"; 
                        }
                        else
                            api_images = api_images + "'" + image + "',";
                        
                        api_title = api_title+"'"+t.Title+"',";
  
                    }
                    api_images = api_images.Remove(api_images.Length - 1);
                    api_title = api_title.Remove(api_title.Length - 1);
                    click_javascript = "api_images = [" + api_images + "]; api_title =[" + api_title + "]; $.prettyPhoto.open(api_images,'',api_title);";
                    l1.Attributes.Add("onclick", click_javascript);
                    }
                    else
                    {
                        foreach (string image in Images)
                        {
                            if (image.Contains("http://img.youtube.com/vi/"))
                            {
                                VidioID = image.Replace("http://img.youtube.com/vi/", "").Replace("/0.jpg", "");
                                break;

                            }
                        }
                        if (!String.IsNullOrEmpty(VidioID))
                        {
                            l1.NavigateUrl = "http://www.youtube.com/watch?v=" + VidioID;
                        }
                        else
                        {
                            l1.NavigateUrl = EPM.ImageUtil.EPMImageExtractUtil.GetFirstImageUrlFromArticleBody(t.Body);
                        }

                        l1.Attributes.Add("rel", "prettyPhoto[pp_gal]");
                        l1.ToolTip = t.Title;
                    }
                    // Hyperlink to the article title to increase the view count by clicking on the title.
                        l2.NavigateUrl = string.Format("/Article/view.aspx?p={0}&page={1}&aid={2}&mode=p",
                                    t.CategoryId, FitPager1.CurrentPage, t.ArticleId);
                        //l2.Text = "<img src='/img/edit.png'>" + t.Title; // no longer need to display the pencil icon as it is visible for everyone



                }

                if (t.ArticleComments.Count > 0)
                {
                    Literal lb = e.Item.FindControl("CommCount") as Literal;
                    lb.Text = "[" + t.ArticleComments.Count + "]";
                }

                Image img_new = e.Item.FindControl("ImgNew") as Image;
                int hours = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Article"));
                if (t.IssueDate >= DateTime.Now.AddHours(hours*-1))
                    img_new.Visible = true;

                Label lbl_issuedate = e.Item.FindControl("lbl_issuedate") as Label;
                lbl_issuedate.Text = t.IssueDate.ToShortDateString();
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

                string base_link = string.Format("~/Article/view.aspx?p={0}&q={1}",a.ArticleCategory1.ArtCatId.ToString(), Request.QueryString["q"]);

                HyperLink l = e.Item.FindControl("ViewLink") as HyperLink;
                HyperLink l1 = e.Item.FindControl("Article_Abstract") as HyperLink;
                l.NavigateUrl = base_link + "&page=" + FitPager1.CurrentPage +
                                "&aid=" + a.ArticleId;
                l1.NavigateUrl = base_link + "&page=" + FitPager1.CurrentPage +
                                "&aid=" + a.ArticleId;
                l1.Text = EPM.Legacy.Common.Utility.TruncateStringByWord(a.Abstract, NumOfDescriptionChar);

                HyperLink li = e.Item.FindControl("ViewImageLink") as HyperLink;
                li.NavigateUrl = base_link + "&page=" + FitPager1.CurrentPage +
                                "&aid=" + a.ArticleId;




                HtmlControl div = e.Item.FindControl("thumb_div") as HtmlControl;
                HtmlControl ArticleList_art = e.Item.FindControl("ArticleList_art") as HtmlControl;

                // Control Article Highlight
                if ((HightlightCurrentItem) && (Request.QueryString["aid"] != null))
                {
                    if (int.Parse(Request.QueryString["aid"].ToString()) == a.ArticleId)
                    {
                        string current_class = ArticleList_art.Attributes["class"];
                        ArticleList_art.Attributes["class"] = current_class + " ArticleList_Selected";
                    }
                }

                if (!HideThumbnail)
                    EPM.Core.Common.BindHelper.ImageBinder(e, "Thumb", a.ArticleId, ThumbnailTypeString);
                else
                    ((Image)e.Item.FindControl("Thumb")).Visible = false;

                if (a.ArticleComments.Where(c => c.Blocked == false).Count() > 0)
                {
                    Label lb = e.Item.FindControl("CommCount") as Label;
                    lb.Text = "[" + a.ArticleComments.Count + "]";
                }

                if (a.IssueDate > DateTime.Now)
                {
                    Literal PendingMsg = new Literal();
                    PendingMsg.Text = "<div class='ArticleList_PostPending'><p>Post Pending till " + a.IssueDate + "</p></div>";
                    ArticleList_art.Controls.Add(PendingMsg);
                }


                System.Web.UI.HtmlControls.HtmlImage img_new = e.Item.FindControl("img_new") as System.Web.UI.HtmlControls.HtmlImage;
                if (a.IssueDate >= DateTime.Now.AddDays(-1))
                    img_new.Visible = true;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }
    /// <summary>
    /// Headline repeater and bind data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ArtRepeater_Headline_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;

                string base_link = string.Format("~/Article/view.aspx?p={0}&q={1}", a.ArticleCategory1.ArtCatId.ToString(), Request.QueryString["q"]);

                HyperLink l = e.Item.FindControl("ArticleList_HeadlineLinkImage") as HyperLink;
                l.NavigateUrl = base_link + "&page=" + FitPager1.CurrentPage +
                                "&aid=" + a.ArticleId;

                HyperLink li = e.Item.FindControl("ArticleList_HeadlineLink") as HyperLink;
                li.NavigateUrl = base_link + "&page=" + FitPager1.CurrentPage +
                                "&aid=" + a.ArticleId;


                Label abstract_label = e.Item.FindControl("ArticleList_HeadlineAbstract") as Label;
                abstract_label.Text = EPM.Legacy.Common.Utility.TruncateStringByWord(a.Abstract, NumOfDescriptionChar);



                // If HideThumbnail == True, all thubmnails shouldn't be displayed
                if (!HideThumbnail)
                    EPM.Core.Common.BindHelper.ImageBinder(e, "ArticleList_TitleThumb", a.ArticleId, ThumbnailTypeHeadlineString);
                else
                    ((Image)e.Item.FindControl("ArticleList_TitleThumb")).Visible = false;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }


}