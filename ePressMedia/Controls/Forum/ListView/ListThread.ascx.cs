using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;
using EPM.Legacy.Security;
using EPM.Business.Model.Forum;

[Description("Forum Listing Control")]
public partial class Forum_ListThread : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger(typeof(Forum_ListThread));
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                bool override_CatId = false;
                int cat;

                if (Request.QueryString["p"] != null)
                    cat = int.Parse(Request.QueryString["p"]);
                else
                    cat = CategoryID;
                if (CategoryID > 0)
                {
                    cat = CategoryID;
                    override_CatId = true;
                }

                CatId.Value = cat.ToString();

                if (!ShowPager)
                    FitPager1.Visible = false;

                if (!ShowSearch)
                    forum_search_panel.Visible = false;


                btn_post2.Visible = btn_post.Visible = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                    ResourceType.Forum, cat, Permission.Write);

                if (override_CatId)
                    btn_post.Visible =btn_post2.Visible = false;

                if (btn_post.Visible)
                {
                    //PostLink.NavigateUrl =
                    //    string.Format("~/Forum/PostThread.aspx?p={0}&page={1}&tid={2}&qi={3}&q={4}",
                    //    Request.QueryString["p"], Request.QueryString["page"],
                    //    Request.QueryString["tid"],
                    //    Request.QueryString["qi"], Request.QueryString["q"]);


                    string path = "/Page/DataEntry.aspx?p=" + cat + "&area=forum&mode=Add";

                    string catName = context.Forums.SingleOrDefault(c => c.ForumId == cat).ForumName;
                    EntryPopup.Title = "New Forum - " + catName;
                    btn_post.OnClientClick= EntryPopup.GetOpenPath(path);// "ShowDataEntry('" + path + "','" + catName + "'); return false;";
                    btn_post2.OnClientClick= EntryPopup.GetOpenPath(path);


                }
                FitPager1.RowsPerPage = (RowPerPage > 0) ? RowPerPage : 10;
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
            sb.Append(@"         $(function () {
            $('.DataImage_Container .picFrm a').lightBox();
        });");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "lightbox", sb.ToString(), true);
        }
    }

    void bindData()
    {
        try
        {
            int pageNum, forumId;
            string filter;
            string filterValue = null;

            getParams(out forumId, out pageNum, out filter, out filterValue);

            IQueryable<ForumModel.ForumThread> forumQuery = ForumController.GetThreadsByForumId(forumId,true);

            if (filter == "PostedBy")
                forumQuery = forumQuery.Where(c => c.PostBy.Contains(filterValue));
            else if (filter == "Subject")
                forumQuery = forumQuery.Where(c => c.Subject.Contains(filterValue));
            else if (filter == "Content")
                forumQuery = forumQuery.Where(c => c.Body.Contains(filterValue));

            FitPager1.TotalRows = forumQuery.Count();// ForumThread.GetThreadCount(forumId, filter, filterValue, true);
            FitPager1.CurrentPage = pageNum;

            if (showPager)
                FitPager1.Visible = (FitPager1.TotalRows > 0);

            bindPage();
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }

    void bindPage()
    {
        try
        {
            int pageNum, forumId;
            string filter;
            string filterValue = null;

            getParams(out forumId, out pageNum, out filter, out filterValue);

            IQueryable<ForumModel.ForumThread> forumQuery = ForumController.GetThreadsByForumId(forumId,true);
            if (filter == "PostedBy")
                forumQuery = forumQuery.Where(c => c.PostBy.Contains(filterValue));
            else if (filter == "Subject")
                forumQuery = forumQuery.Where(c => c.Subject.Contains(filterValue));
            else if (filter == "Content")
                forumQuery = forumQuery.Where(c => c.Body.Contains(filterValue));

            var newresult = forumQuery.Skip((FitPager1.CurrentPage - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);


            if (!PhotoListing)
            {
                DataRepeater.Visible = true;
                DataRepeater.DataSource = newresult;
                DataRepeater.DataBind();
            }
            else
            {
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

    void getParams(out int forumId, out int pageNum, out string filter, out string filterValue)
    {
        forumId = int.Parse(CatId.Value);// int.Parse(Request.QueryString["p"]);


        if (FitPager1.CurrentPage > 1)
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

        filter = ""; //ThreadFilter.None;
        filterValue = null;
        if (!string.IsNullOrEmpty(Request.QueryString["qi"]) &&
            !string.IsNullOrEmpty(Request.QueryString["q"]))
        {
            filter = Request.QueryString["qi"];//(ThreadFilter)int.Parse(Request.QueryString["qi"]);
            filterValue = Request.QueryString["q"];
        }

        if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
        {
            int tid = int.Parse(Request.QueryString["tid"]);
            CurThread.Value = tid.ToString();
        }
    }

    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        bindPage();
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        int forumId = int.Parse(Request.QueryString["p"]);
        Response.Redirect(string.Format("~/Forum/List.aspx?p={0}&page={1}&qi={2}&q={3}", forumId,
            "1" , SearchList.SelectedValue.ToString(),//.SelectedIndex + 1,
            Server.UrlEncode(SearchValue.Text.Trim())));
    }

    protected void DataRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            int forumId = int.Parse(Request.QueryString["p"]);

            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ForumModel.ForumThread t = e.Item.DataItem as ForumModel.ForumThread;

                HyperLink l = e.Item.FindControl("ViewLink") as HyperLink;
                l.NavigateUrl = string.Format("~/Forum/view.aspx?p={0}&page={1}&tid={2}",
                                t.ForumId, FitPager1.CurrentPage, t.ThreadId);



                if (t.ForumComments.Where(c => c.Blocked == false).Count() > 0)
                {
                    Label lb = e.Item.FindControl("CommCount") as Label;
                    lb.Text = "[" + t.ForumComments.Count(c=>c.Blocked == false) + "]";
                }
                if (t.Announce)
                    l.Font.Bold = true;

                if (t.ThreadId == int.Parse(CurThread.Value))
                    l.CssClass = "sel";

                Literal lbl_PostDate = e.Item.FindControl("lbl_PostDate") as Literal;
                lbl_PostDate.Text = t.PostDate.ToShortDateString();

                // count attachement

                if (Convert.ToBoolean(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Show attachment indicator(clip icon)")))
                {
                    HtmlImage img_attachment = e.Item.FindControl("img_attachment") as HtmlImage;
                    img_attachment.Visible = (t.ForumAttachments.Count() > 0);
                }
                


                System.Web.UI.HtmlControls.HtmlImage img_new = e.Item.FindControl("img_new") as System.Web.UI.HtmlControls.HtmlImage;
                int hours = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Forum"));
                    if (t.PostDate >= DateTime.Now.AddHours(hours*-1))
                        img_new.Visible = true;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }

    private int rowPerPage = 10;
    [Category("EPMProperty"), Description("Define number of forum items per page"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Row Per Page is required."), DisplayName("Row Per Page"), EditorBrowsable(EditorBrowsableState.Advanced) ]
    public int RowPerPage
    {
        get { return rowPerPage; }
        set { rowPerPage = value; }
    }
    private int categoryId = 0;
    [Category("EPMProperty"), Description("(Optional) Override Category ID")]
    public int CategoryID
    {
        get { return categoryId; }
        set { categoryId = value; }
    }

    private Boolean showPager = true;
    [Category("EPMProperty"), Description("Show Pager"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowPager
    {
        get { return showPager; }
        set { showPager = value; }
    }

    private Boolean showSearch = true;
    [Category("EPMProperty"), Description("Show Search"), DefaultValue(typeof(System.Boolean), "true"), Required()]
    public Boolean ShowSearch
    {
        get { return showSearch; }
        set { showSearch = value; }
    }


    private Boolean photoListing = false;
    [Category("EPMProperty"), Description("Show in Image View"), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public Boolean PhotoListing
    {
        get { return photoListing; }
        set { photoListing = value; }
    }

    private Boolean lightBoxType = false;
    [Category("EPMProperty"), Description("Show as Light Box Type"), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public Boolean LightBoxType
    {
        get { return lightBoxType; }
        set { lightBoxType = value; }
    }


    private Boolean hideThumbnail = false;
    [Category("EPMProperty"), Description("Hide thumbnail (This applies to listing view only."), DefaultValue(typeof(System.Boolean), "false"), Required()]
    public Boolean HideThumbnail
    {
        get { return hideThumbnail; }
        set { hideThumbnail = value; }
    }



    protected void DataImageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            int forumId = int.Parse(CatId.Value);

            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ForumModel.ForumThread t = e.Item.DataItem as ForumModel.ForumThread;
                System.Web.UI.HtmlControls.HtmlImage image = e.Item.FindControl("Img1") as System.Web.UI.HtmlControls.HtmlImage;
                image.Attributes.Add("src", t.Thumb);

                HyperLink l1 = e.Item.FindControl("ViewLink1") as HyperLink;
                HyperLink l2 = e.Item.FindControl("ViewLink2") as HyperLink;

                if (!LightBoxType)
                {
                    l1.NavigateUrl = l2.NavigateUrl =
                        string.Format("/Forum/view.aspx?p={0}&page={1}&tid={2}&mode=p",
                                    t.ForumId, FitPager1.CurrentPage, t.ThreadId);
                }
                else
                {
                    var f_pic = from c in context.ForumAttachments
                                where c.ThreadId == t.ThreadId && (c.FileName.EndsWith("png") || c.FileName.EndsWith("gif") || c.FileName.EndsWith("jpg"))
                                orderby c.Id
                                select c;
                    if (f_pic.Count() > 0)
                    {
                        l1.NavigateUrl = "~/" + EPM.Core.Admin.SiteSettings.ForumUploadRoot + "/" + f_pic.ToList()[0].FileName;
                        l1.ToolTip = t.Subject;
                        if (btn_post.Visible)
                        {
                            l2.NavigateUrl = string.Format("/Forum/view.aspx?p={0}&page={1}&tid={2}&mode=p",
                                        t.ForumId, FitPager1.CurrentPage, t.ThreadId);
                            l2.Text = "<img src='/img/edit.png'>"+t.Subject;
                        }
                    }
                  
                }

                if (t.ForumComments.Count > 0)
                {
                    Literal lb = e.Item.FindControl("CommCount") as Literal;
                    lb.Text = "[" + t.ForumComments.Where(c=>c.Blocked== false).Count()  + "]";
                }

                Image img_new = e.Item.FindControl("ImgNew") as Image;
                int hours = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Forum"));
                if (t.PostDate >= DateTime.Now.AddHours(hours*-1))
                    img_new.Visible = true;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }
}