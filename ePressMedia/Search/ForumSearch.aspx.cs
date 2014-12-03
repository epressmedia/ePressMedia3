using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using Knn.Data;


public partial class Search_ForumSearch : System.Web.UI.Page
{
    static string bcFormat =
    @"<span> &gt; </span><a href='Search.aspx?q={0}'>통합검색</a>
      <span> &gt; 게시판검색 '{1}'</span>";

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

        var results = from c in context.ForumThreads
                      where c.Suspended == false && (c.Subject.Contains(query) || c.Body.Contains(query))
                      orderby c.PostDate descending
                      select c;

        
        FitPager1.TotalRows = results.Count();
        FitPager1.CurrentPage = 1;
        FitPager1.Visible = (FitPager1.TotalRows > FitPager1.RowsPerPage);

        ThreadCount.Text = FitPager1.TotalRows.ToString();


        ForumRepeater.DataSource = results.Skip((FitPager1.CurrentPage - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);

        ForumRepeater.DataBind();
    }

    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        bindData();
    }


}