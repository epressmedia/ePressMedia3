using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Knn.Data;



public partial class Search_ArticleSearch : System.Web.UI.Page
{
    static string bcFormat = 
    @"<span> &gt; </span><a href='Search.aspx?q={0}'>통합검색</a>
      <span> &gt; 기사검색 '{1}'</span>";

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["q"] == null)
                return;

            Master.SetBreadCrumb(string.Format(bcFormat,
                    Server.UrlEncode(Request.QueryString["q"]), Request.QueryString["q"]));
            //Master.SetHomePage(false);

            bindData();
        }
    }

    void bindData()
    {
        if (Request.QueryString["q"].Trim() == string.Empty)
            return;

        string query = Request.QueryString["q"].Trim();
        var results = from c in context.Articles
                      where c.Suspended == false && c.IssueDate <= DateTime.Now && (c.Title.Contains(query) || c.SubTitle.Contains(query) || c.Body.Contains(query) || c.Abstract.Contains(query))
                      orderby c.PostDate descending
                      select c;

        
        FitPager1.TotalRows = results.Count();
        FitPager1.CurrentPage = 1;
        FitPager1.Visible = (FitPager1.TotalRows > FitPager1.RowsPerPage);

        ArtCount.Text = FitPager1.TotalRows.ToString();


        AdRepeater.DataSource = results.Skip((FitPager1.CurrentPage - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);
        AdRepeater.DataBind();
    }

    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        bindData();
    }

    protected void AdRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem ||
            e.Item.ItemType == ListItemType.Item)
        {
            if (string.IsNullOrEmpty((e.Item.DataItem as ArticleModel.Article).Thumb_Path))
            {
                (e.Item.FindControl("ThumbImg") as Image).Visible = false;
            }
        }
    }

}