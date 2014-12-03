using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search_SearchMenu : System.Web.UI.UserControl
{
    public int SelectedIndex
    {
        set
        {
            if (0 == value)
                liSearch.Attributes.Add("class", "sel");
            else if (1 == value)
                liCls.Attributes.Add("class", "sel");
            else if (2 == value)
                liBiz.Attributes.Add("class", "sel");
            else if (3 == value)
                liArt.Attributes.Add("class", "sel");
            else if (4 == value)
                liForum.Attributes.Add("class", "sel");
            else
                return;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string qs = Server.UrlEncode(Request.QueryString["q"]);
            SearchLink.NavigateUrl = "~/Search/Search.aspx?q=" + qs;
            ClsLink.NavigateUrl = "~/Search/ClassifiedSearch.aspx?q=" + qs;
            BizLink.NavigateUrl = "~/Biz/Biz.aspx?q=" + qs;
            ArtLink.NavigateUrl = "~/Search/ArticleSearch.aspx?q=" + qs;
            ForumLink.NavigateUrl = "~/Search/ForumSearch.aspx?q=" + qs;
        }
    }
}