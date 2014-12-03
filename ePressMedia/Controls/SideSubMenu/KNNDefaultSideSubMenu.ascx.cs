using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using log4net;


//using Knn.Website;
[Description("SideSubMenu")]
public partial class Controls_SideSubMenu_KNNDefaultSideSubMenu : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger(typeof(Controls_SideSubMenu_KNNDefaultSideSubMenu));

    string menuColor = null;
    int curMenuId = 0;


    private ContentTypes contentType;
    [Category("EPMProperty"), Description("Define Content Type ID: Article(1),Forum(2),Classified(3),Calendar(4),Biz(5),Search(6)"), Required()]
    public ContentTypes ContentType
    {
        get {return contentType;}
        set{contentType=value;}
    }

    public enum ContentTypes
    {
        Article = 1,
        Forum = 2,
        Classified = 3,
        Calendar = 4,
        Biz = 5,
        Search = 6,
        Page = 7
    }

    private int catId = 0;
    [Category("EPMProperty"), Description("Define Category ID")]
    public int CatId
    {
        get { return catId; }
        set { catId = value; }
    }

    private bool showHeader = true;
    [Category("EPMProperty"), Description("Determine if the header is displayed."), DefaultValue(typeof(System.Boolean),"false")]
    public bool ShowHeader
    {
        get { return showHeader; }
        set { showHeader = value; }
    }

    EPM.Data.Model.EPMEntityModel context;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
        context = new EPM.Data.Model.EPMEntityModel();
        if (CatId == 0)
        {
            if (Request.QueryString["p"] != null)
            {
                CatId = int.Parse(Request.QueryString["p"].ToString());
            }
            else if (Page.RouteData.Values["category"] != null)
            {
                if (ContentType == ContentTypes.Article)
                    CatId = EPM.Business.Model.Article.ArticleCategoryContoller.GetArticleCatIdByURLSlug(Page.RouteData.Values["category"].ToString());
            }
        }
           // SetSideMenu((int)ContentType, CatId, null);
            if (CatId > 0)
            PopulateSideMenu("~/"+(HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToList())[1].ToString()+"/", CatId);
        }
        catch(Exception ex)
            {
                log.Error(ex.Message+" \r\n "+ex.StackTrace +"\r\n"+ ex.Source);
            }
    }

    /// <summary>
    /// Config the side menu bar
    /// </summary>
    /// <param name="url">Current Page URL</param>
    /// <param name="curPageName">BreadCrumb Name displaying at the end</param>
    public void SetSideMenu(string url, string curPageName)
    {

        SiteModel.SiteMenu menu = context.SiteMenus.Single(c => c.Url == url && c.Visible == true);
        setSideMenu(menu, curPageName);
    }


    public void PopulateSideMenu(string Baseurl,int contentId)
    {

        if ((context.ContentTypes.Count(c => c.BaseUrl == Baseurl) > 0) || (Baseurl == "~/cp/"))
        {

            if ((Baseurl == "~/cp/") && (Request.QueryString["content"] != null))
            {
                Baseurl = "~/" + Request.QueryString["content"].ToString() + "/";
            }

            SetSideMenu(context.ContentTypes.Single(c => c.BaseUrl == Baseurl).ContentTypeId, contentId, null);


        }
        else
        {
            string contentTypeName = contentType.ToString();
            SiteModel.ContentType cont = context.ContentTypes.Single(c => c.ContentTypeName == contentTypeName);
            if (cont != null)
                SetSideMenu(cont.ContentTypeId, contentId, null);
            
        }

        
    }

    public void SetSideMenu(int contentType, int contentId, string curPageName)
    {


        IQueryable<SiteModel.SiteMenu> menu = from c in context.SiteMenus
                                              where c.ContentView.ContentTypeId == contentType && c.Visible == true && c.Param == contentId.ToString()
                                              orderby c.MenuId descending
                                              select c;

        if (menu.Count() > 0)
        {
            setSideMenu(menu.ToList()[0], curPageName);
            
        }
    }


    private void setSideMenu(SiteModel.SiteMenu menu, string curPageName)
    {
        leftMenu.Visible = true;
        curMenuId = menu.MenuId;

       
        while (menu.ParentId > 0)
        {

            
            menu = context.SiteMenus.Single(c => c.MenuId == menu.ParentId);

        }

        if (string.IsNullOrEmpty(menu.ExParam1))
            MenuImage.Visible = false;
        else
            MenuImage.ImageUrl =  menu.ExParam1; // This needs to be full path 

        menuColor = menu.ExParam2;


        List<SiteModel.SiteMenu> result = (from c in context.SiteMenus
                     where c.ParentId == menu.MenuId && c.Visible == true
                     orderby c.DispOrder
                     select c).ToList();


        SideRepeater.DataSource = result;
        SideRepeater.DataBind();

        if ((ShowHeader) && (context.SiteMenus.Count(c => c.MenuId == result[0].ParentId) > 0))
        {
            KNNDefaultSideSubMenu_Header.Visible = ShowHeader;
            lbl_header.Text = context.SiteMenus.Single(c => c.MenuId == result[0].ParentId).Label;
        }
    }

    protected void SideChildMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
            (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            SiteModel.SiteMenu menu = e.Item.DataItem as SiteModel.SiteMenu;
            if (menu.MenuId == curMenuId)
            {
                HyperLink l = e.Item.FindControl("MenuLink") as HyperLink;
                l.Text = "▶ " + l.Text;
                l.ForeColor = System.Drawing.ColorTranslator.FromHtml(menu.ExParam2);
            }
        }
    }
    protected void SideMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
            (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            SiteModel.SiteMenu menu = e.Item.DataItem as SiteModel.SiteMenu;
            HyperLink l = e.Item.FindControl("MenuLink") as HyperLink;
            if (menu.MenuId == curMenuId)
            {
                l.Text = "▶ " + l.Text;
                l.ForeColor = System.Drawing.ColorTranslator.FromHtml(menuColor);
            }
            
            var sub = from c in context.SiteMenus
                         where c.ParentId == menu.MenuId && c.Visible  == true
                         orderby c.DispOrder
                         select c;

            if (sub.Count() > 0)
            {
                l.ForeColor = System.Drawing.ColorTranslator.FromHtml(menuColor);
                Repeater r = e.Item.FindControl("ChildRepeater") as Repeater;
                r.DataSource = sub;
                r.DataBind();
            }
        }
    }
}