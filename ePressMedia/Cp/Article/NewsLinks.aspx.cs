using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



public partial class Cp_Article_NewsLinks : System.Web.UI.Page
{


    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int catid = int.Parse(Request.QueryString["cat"].ToString());
            bindArticleCat(catid);
            bindData(catid);
            
        }
            
    }

    void bindArticleCat(int catid)
    {
        ArticleModel.ArticleCategory results = (from c in context.ArticleCategories
                      where c.LinkArticle_fg == true && c.ArtCatId == catid
                      select c).Single();
        lbl_catname.Text = results.CatName;

    }

    void bindData(int catid)
    {
        FitPager1.TotalRows = context.Articles.Select(c => c.CategoryId == catid && c.Suspended == false).Count();// ArticleLink.GetCount();
        
        FitPager1.CurrentPage = 1;
        FitPager1.Visible = (FitPager1.TotalRows > 0);

        bindPage(catid);
    }

    void bindPage(int catid)
    {

        var articles = (from c in context.Articles
                                         where c.CategoryId == catid && c.Suspended == false
                                         orderby c.ArticleId descending
                                         select c).Skip((FitPager1.CurrentPage - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);

        Repeater1.DataSource = articles;
        Repeater1.DataBind();
    }


    protected void AddButton_Click(object sender, EventArgs e)
    {
        if (Subject.Text == string.Empty ||
            LinkUrl.Text == string.Empty)
            return;


        ArticleModel.Article article = new ArticleModel.Article();

        article.LinkArticle_URL = LinkUrl.Text;
        article.Title = Subject.Text;
        article.SubTitle = Subject.Text;
        article.Abstract = Subject.Text;
        article.Body = ""; 
        article.IssueDate = DateTime.Now;
        article.PostDate = DateTime.Now;
        article.CategoryId = int.Parse(Request.QueryString["cat"].ToString());
        article.Reporter = User.Identity.Name;
        article.PostBy = User.Identity.Name;
        article.Suspended = false;
        article.Hilighted = false;
        article.Thumb_Path = "";
        article.VideoId = "";
        article.AllowComment = false;


        context.Add(article);
        context.SaveChanges();

        //ArticleLink.InsertArticleLink(Subject.Text, LinkUrl.Text);
        Subject.Text = LinkUrl.Text = string.Empty;
        bindData(int.Parse(Request.QueryString["cat"].ToString()));
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        bindPage(int.Parse(Request.QueryString["cat"].ToString()));
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ArtId.Value = e.CommandArgument.ToString();
        int article_id = int.Parse(ArtId.Value);

        if (e.CommandName.Equals("mod"))
        {

            ConfirmMpe.Show();
            Label Subject = (Label)e.Item.FindControl("lbl_title");
            HyperLink LinkURL = (HyperLink)e.Item.FindControl("ViewLink");

            NewTitle.Text = Subject.Text;
            NewURL.Text = LinkURL.Text;

            NewTitle.Focus();
            
            
        }
        else if (e.CommandName.Equals("del"))
        {
            ArticleModel.Article article = context.Articles.Single(c => c.ArticleId == article_id);
            article.Suspended = true;
            context.SaveChanges();

            bindPage(int.Parse(Request.QueryString["cat"].ToString()));
        }
    }

    protected void ModButton_Click(object sender, EventArgs e)
    {

        if ((NewTitle.Text.Length > 0) && (NewURL.Text.Length > 0) && (ArtId.Value.Length > 0))
        {
            int article_id = int.Parse(ArtId.Value);

            ArticleModel.Article article = context.Articles.Single(c => c.ArticleId == article_id);
            article.Title = NewTitle.Text;
            article.SubTitle = NewTitle.Text;
            article.Abstract = NewTitle.Text;
            article.LinkArticle_URL = NewURL.Text;
            context.SaveChanges();

            NewTitle.Text = "";
            NewURL.Text = "";
            bindPage(int.Parse(Request.QueryString["cat"].ToString()));
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        NewTitle.Text = "";
        NewURL.Text = "";
    }
}