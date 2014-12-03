using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Xml;

using EPM.Core;

public partial class Controls_NewsPanel : System.Web.UI.UserControl
{

    private string title;
    public string Title
    {
        set { title = value; }
        get { return title; }
    }

    private string articleCatID1;
    public string ArticleCatID1
    {
        set { articleCatID1 = value; }
        get { return articleCatID1; }
    }

    private string articleCatName1;
    public string ArticleCatName1
    {
        set { articleCatName1 = value; }
        get { return articleCatName1; }
    }

    private int noArticles1;
    public int NoArticles1
    {
        set { noArticles1 = value; }
        get { return noArticles1; }
    }

    private string articleCatID2;
    public string ArticleCatID2
    {
        set { articleCatID2 = value; }
        get { return articleCatID2; }
    }

    private string articleCatName2;
    public string ArticleCatName2
    {
        set { articleCatName2 = value; }
        get { return articleCatName2; }
    }

    private int noArticles2;
    public int NoArticles2
    {
        set { noArticles2 = value; }
        get { return noArticles2; }
    }

    private string rss1;
    public string RSS1
    {
        set { rss1 = value; }
        get { return rss1; }
    }
    private string rss2;
    public string RSS2
    {
        set { rss2 = value; }
        get { return rss2; }
    }




    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IQueryable<ArticleModel.Article> articles;
            IQueryable<ArticleModel.Article> articles2;

            if (!string.IsNullOrEmpty(ArticleCatID1))
            {
                var CatIDs = ArticleCatID1.Split(',').ToList();


                articles = (from c in context.Articles
                            where CatIDs.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate < DateTime.Now
                            orderby c.PostDate descending
                            select c).Take(NoArticles1);

                //if (ArticleCatID1.IndexOf(',') > 0)

                //    articles = Article.SelectPreviewArticles(ArticleCatID1, NoArticles1);
                //else
                //    articles = Article.SelectPreviewArticles(int.Parse(ArticleCatID1), NoArticles1);

                //var topArticle = from c in articles
                //                 orderby c.ArticleId descending
                //                 select c;

                if (articles.Count() > 0)
                {

                    HtmlAnchor imageLink = (HtmlAnchor)FindControl("NewsPanel_ImgContainerLink1");
                    imageLink.HRef = "~/Article/ViewArticle.aspx?p=" + (articles.First()).CategoryId.ToString() + "&aid=" + (articles.First()).ArticleId.ToString();

                    NewsPanel_ImgContainer1.ImageUrl = EPM.Core.Admin.SiteSettings.ArticleThumbnailPath + (articles.First()).Thumb_Path;

                    //NewsPanel_ImgContainer1.ImageUrl = "~/" + (topArticle.First()).FirstImageURL;
                    lbl_mainImage1.Text = (articles.First()).Title;


                    Repeater1.DataSource = articles.Skip(1);
                    Repeater1.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(RSS1))
            {
                DataTable dt1 = new DataTable();
                dt1 = ReadRssFeed(RSS1);

                HtmlAnchor imageLink = (HtmlAnchor)FindControl("NewsPanel_ImgContainerLink1");
                imageLink.HRef = dt1.Rows[0]["link"].ToString();
                imageLink.Target = "_blank";

                NewsPanel_ImgContainer2.ImageUrl = dt1.Rows[0]["comments"].ToString();
                lbl_mainImage2.Text = dt1.Rows[0]["title"].ToString();

                dt1.Rows.RemoveAt(0);

                Repeater1.DataSource = dt1;
                Repeater1.DataBind();
            }



            if (!string.IsNullOrEmpty(ArticleCatID2))
            {

               var CatIDs = ArticleCatID2.Split(',').ToList();


                articles2 = (from c in context.Articles
                             where CatIDs.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate < DateTime.Now
                            orderby c.PostDate descending
                            select c).Take(NoArticles2);

                //if (ArticleCatID2.IndexOf(',') > 0)
                //    articles2 = Article.SelectPreviewArticles(ArticleCatID2, NoArticles2);
                //else
                //    articles2 = Article.SelectPreviewArticles(int.Parse(ArticleCatID2), NoArticles2);

                //var topArticle = from c in articles2
                //                 orderby c.ArticleId descending
                //                 select c;

                HtmlAnchor imageLink = (HtmlAnchor)FindControl("NewsPanel_ImgContainerLink2");
                imageLink.HRef = "~/Article/ViewArticle.aspx?p=" + (articles2.First()).CategoryId.ToString() + "&aid=" + (articles2.First()).ArticleId.ToString();

                NewsPanel_ImgContainer2.ImageUrl = "~/" + EPM.Core.Admin.SiteSettings.ArticleThumbnailPath + (articles2.First()).Thumb_Path;

                //NewsPanel_ImgContainer2.ImageUrl = "~/" + (topArticle.First()).FirstImageURL;
                lbl_mainImage2.Text = (articles2.First()).Title;


                Repeater2.DataSource = articles2.Skip(1);
                Repeater2.DataBind();
            }
            else if (!string.IsNullOrEmpty(RSS2))
            {
                DataTable dt2 = new DataTable();
                dt2 = ReadRssFeed(RSS2);

                HtmlAnchor imageLink = (HtmlAnchor)FindControl("NewsPanel_ImgContainerLink2");
                imageLink.HRef = dt2.Rows[0]["link"].ToString();
                imageLink.Target = "_blank";

                NewsPanel_ImgContainer2.ImageUrl = dt2.Rows[0]["comments"].ToString();
                lbl_mainImage2.Text = dt2.Rows[0]["title"].ToString();

                dt2.Rows.RemoveAt(0);

                Repeater2.DataSource = dt2;
                Repeater2.DataBind();
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

                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                    HtmlAnchor anchor = (HtmlAnchor)e.Item.FindControl("a_link");
                    Label title = (Label)e.Item.FindControl("lbl_title");
                if (!string.IsNullOrEmpty(ArticleCatID1))
                {

                    anchor.HRef = "~/Article/ViewArticle.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                    title.Text = a.Title;
                }
                else if (!string.IsNullOrEmpty(RSS1))
                {
                    DataRowView row = e.Item.DataItem as DataRowView;
                    anchor.HRef = row["link"].ToString();
                    anchor.Target = "_blank";
                    title.Text = row["title"].ToString();

                }

            }
        }
        catch
        {
        }
    }
    protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {

                
                HtmlAnchor anchor = (HtmlAnchor)e.Item.FindControl("a_link2");
                Label title = (Label)e.Item.FindControl("lbl_title2");
                if (!string.IsNullOrEmpty(ArticleCatID2))
                {
                    ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                    anchor.HRef = "~/Article/ViewArticle.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                    title.Text = a.Title;
                }
                else if (!string.IsNullOrEmpty(RSS2))
                {
                    DataRowView row = e.Item.DataItem as DataRowView;
                    anchor.HRef = row["link"].ToString();
                    anchor.Target = "_blank";
                    title.Text = row["title"].ToString();

                }

            }
        }
        catch
        {
        }
    }
    public static DataTable ReadRssFeed(string URL)
    {
        XmlTextReader reader = new XmlTextReader(URL);
        // creates a new instance of DataSet 
        DataSet ds = new DataSet();
        // Reads the xml into the dataset
        ds.ReadXml(reader);

        DataTable returnTable;

        //check RSS version
        if (double.Parse(ds.Tables[0].Rows[0]["version"].ToString()) == 2)
            returnTable = ds.Tables[2];
        else
            returnTable = ds.Tables[2];


        DataTable dt = new DataTable();
        DataColumn dc;
        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "title";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "link";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "description";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "comments";
        dt.Columns.Add(dc);

        for (int i = 0; i < returnTable.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["title"] = returnTable.Rows[i]["title"];
            dr["link"] = returnTable.Rows[i]["link"];
            dr["description"] = returnTable.Rows[i]["description"];
            if (returnTable.Columns["comments"] != null) 
                dr["comments"] = returnTable.Rows[i]["comments"];
            else
                dr["comments"] = "";
            dt.Rows.Add(dr);
        }


        return dt;
        

    }
}
