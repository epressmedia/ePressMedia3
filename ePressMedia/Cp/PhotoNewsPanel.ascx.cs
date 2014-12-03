using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPM.Core;
using EPM.Core.Admin;



public partial class Controls_PhotoNewsPanel : System.Web.UI.UserControl
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

    private bool articlesVideoType1 = false;
    public bool ArticlesVideoType1
    {
        set { articlesVideoType1 = value; }
        get { return articlesVideoType1; }
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

    private bool articlesVideoType2 = false;
    public bool ArticlesVideoType2
    {
        set { articlesVideoType2 = value; }
        get { return articlesVideoType2; }
    }


    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
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

            //if (articles.Count() > 0)
            //{
            //    var topArticle = from c in articles
            //                     orderby c.ArticleId descending
            //                     select c;

                if (articles.Count() > 0)
                {

                    HtmlAnchor imageLink = (HtmlAnchor)FindControl("PhotoNewsPanel_ImgContainerLink1");
                    imageLink.HRef = "~/Article/ViewArticle.aspx?p=" + (articles.First()).CategoryId.ToString() + "&aid=" + (articles.First()).ArticleId.ToString();


                    //if (ArticlesVideoType1)
                    //    PhotoNewsPanel_ImgContainer1.ImageUrl = "http://img.youtube.com/vi/" + topArticle.First().VideoId + "/0.jpg";
                    //else
                    //    PhotoNewsPanel_ImgContainer1.ImageUrl = "~/" + SiteSettings.ArticleThumbnailPath + (topArticle.First()).Thumbnail;


                    if (string.IsNullOrEmpty(articles.First().Thumb_Path))
                        PhotoNewsPanel_ImgContainer1.ImageUrl = "~/img/noThumb.png";
                    else
                        PhotoNewsPanel_ImgContainer1.ImageUrl = "~/" + SiteSettings.ArticleThumbnailPath + articles.First().Thumb_Path;

                    //PhotoNewsPanel_ImgContainer1.ImageUrl = "~/" + (topArticle.First()).FirstImageURL;
                    lbl_mainImage1.Text = (articles.First()).Title;
                    lbl_mainImage1.Width = 200;
                    lbl_mainImage1.CssClass = "PhotoNewsPanel_MainTitle";

                    lbl_mainsubtitle1.Text = (articles.First()).Abstract.Length > 40 ? (articles.First()).Abstract.Substring(0, 40) : (articles.First()).Abstract;// GetShortAbstract(40);
                    lbl_mainsubtitle1.Width = 200;
                    lbl_mainsubtitle1.CssClass = "PhotoNewsPanel_Abstract";

                    if (articles.Count() > 1)
                    {
                        PhotoNewsPanel_Repeater1.DataSource = articles.Skip(1);
                        PhotoNewsPanel_Repeater1.DataBind();
                    }
            //    }
            }
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

            if (articles2.Count() > 0)
            {
                //var topArticle = from c in articles2
                //                 orderby c.ArticleId descending
                //                 select c;

                HtmlAnchor imageLink = (HtmlAnchor)FindControl("PhotoNewsPanel_ImgContainerLink2");
                imageLink.HRef = "~/Article/ViewArticle.aspx?p=" + (articles2.First()).CategoryId.ToString() + "&aid=" + (articles2.First()).ArticleId.ToString();

                if (string.IsNullOrEmpty(articles2.First().Thumb_Path))
                    PhotoNewsPanel_ImgContainer2.ImageUrl = "~/img/noThumb.png";
                else
                    PhotoNewsPanel_ImgContainer2.ImageUrl = "~/" + SiteSettings.ArticleThumbnailPath + articles2.First().Thumb_Path;


                //PhotoNewsPanel_ImgContainer2.ImageUrl = "~/" + (topArticle.First()).FirstImageURL;
                lbl_mainImage2.Text = (articles2.First()).Title;
                lbl_mainImage2.Width = 200;
                lbl_mainImage2.CssClass = "PhotoNewsPanel_MainTitle";

                lbl_mainsubtitle2.Text = (articles2.First()).Abstract.Length > 0 ? (articles2.First()).Abstract.Substring(0, 40) : (articles2.First()).Abstract;
                lbl_mainsubtitle2.Width = 200;
                lbl_mainsubtitle2.CssClass = "PhotoNewsPanel_Abstract";

                if (articles2.Count() > 1)
                {
                    PhotoNewsPanel_Repeater2.DataSource = articles2.Skip(1);
                    PhotoNewsPanel_Repeater2.DataBind();
                }
            }
        }
    }
    protected void PhotoNewsPanel_Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                HtmlAnchor anchor = (HtmlAnchor)e.Item.FindControl("PhotoNewsPanel_ImgItemLink1");
                HtmlImage image = (HtmlImage)e.Item.FindControl("PhotoNewsPanel_ImgItemSrc1");
                Label title = (Label)e.Item.FindControl("lbl_title1");
                if (!string.IsNullOrEmpty(ArticleCatID1))
                {

                    anchor.HRef = "~/Article/ViewArticle.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                    title.Text = a.Title;


                    if (string.IsNullOrEmpty(a.Thumb_Path))
                        image.Src =  "~/img/noThumb.png";
                    else
                        image.Src = "~/" + SiteSettings.ArticleThumbnailPath + a.Thumb_Path;



                    
                }

            }
        }
        catch
        {
        }
    }
    protected void PhotoNewsPanel_Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;
                HtmlAnchor anchor = (HtmlAnchor)e.Item.FindControl("PhotoNewsPanel_ImgItemLink2");
                HtmlImage image = (HtmlImage)e.Item.FindControl("PhotoNewsPanel_ImgItemSrc2");
                Label title = (Label)e.Item.FindControl("lbl_title2");
                if (!string.IsNullOrEmpty(ArticleCatID2))
                {

                    anchor.HRef = "~/Article/ViewArticle.aspx?p=" + a.CategoryId.ToString() + "&aid=" + a.ArticleId.ToString();
                    title.Text = a.Title;
                    //if (ArticlesVideoType2)
                    //    image.Src = "http://img.youtube.com/vi/" + a.VideoId + "/default.jpg";
                    //else
                    //    image.Src = "~/" + SiteSettings.ArticleThumbnailPath + a.Thumbnail;

                    if (string.IsNullOrEmpty(a.Thumb_Path))
                        image.Src = "~/img/noThumb.png";
                    else
                        image.Src = "~/" + SiteSettings.ArticleThumbnailPath + a.Thumb_Path;
                }

            }
        }
        catch
        {
        }

    }
}