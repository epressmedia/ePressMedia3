using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EPM.Core;
using EPM.Core.Admin;
using EPM.ImageUtil;
using EPM.ImageLibrary;
using System.Threading;


public partial class Cp_Tools_ThumbnailGenerator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_ProcessByCategory_Click(object sender, EventArgs e)
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        if (ddl_content_type.SelectedValue.ToLower() == "article")
        {
           List<ArticleModel.Article> articles = (from c in context.Articles
                                                  where c.CategoryId == int.Parse(ddl_category.SelectedValue.ToString())
                                                  select c).ToList();
            
            ProcessArticle(articles);
        }
    }
    
    protected void btn_process_Click(object sender, EventArgs e)
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

        if (rdb_article.Checked)
        {
            List<ArticleModel.Article> articles = (from c in context.Articles
                                                   orderby c.PostDate descending
                                                   select c).Skip(int.Parse(txt_from.Text) - 1).Take(int.Parse(txt_to.Text) - int.Parse(txt_from.Text) + 1).ToList();



            ProcessArticle(articles);


        }
        else if (rdb_biz.Checked)
        {
            List<BizModel.BusinessEntity> bizs = (from c in context.BusinessEntities
                                                  where c.BusinessEntityImages.Count > 0
                                                  orderby c.CreatedDate descending
                                                  select c).Skip(int.Parse(txt_from.Text) - 1).Take(int.Parse(txt_to.Text) - int.Parse(txt_from.Text) + 1).ToList();
            ProcessBiz(bizs);
        }
        else if (rdb_forum.Checked)
        {
            List<ForumModel.ForumThread> forums = (from c in context.ForumThreads
                                                   where (c.Thumb == null || c.Thumb.Trim().Length < 1)
                                                   orderby c.PostDate descending
                                                   select c).Skip(int.Parse(txt_from.Text) - 1).Take(int.Parse(txt_to.Text) - int.Parse(txt_from.Text) + 1).ToList();
            string log = "";
            log = "Total articles to process: " + forums.Count().ToString() + "<br/>";
            int count = 0;

            foreach (ForumModel.ForumThread forum in forums)
            {
                string virPath = EPMImageExtractUtil.GetFirstImageUrlFromArticleBody(forum.Body).Trim();
                if (!virPath.Contains("http"))
                {
                    try
                    {
                        string imgUrl = Server.MapPath(virPath);

                        if (EPM.Core.FileHelper.FileExists(imgUrl))
                        {


                            if (imgUrl.Trim() != string.Empty)
                            {

                                ForumModel.ForumThread fo = context.ForumThreads.Single(c => c.ThreadId == forum.ThreadId);

                                string Thumbnail_path = Server.MapPath(SiteSettings.ForumThumbnailPath) + "\\" + Path.GetFileNameWithoutExtension(imgUrl) + "_t.jpg";
                                EPMImage thumbImage = new EPMImage(imgUrl);

                                thumbImage.GetThumbnailImage(200, 100, ThumbnailMethod.Fit).Image.Save(Thumbnail_path);
                                //thumbImage.GetThumbnailImage(200, 200, ThumbnailMethod.Crop).SaveImage(Thumbnail_path, System.Drawing.Imaging.ImageFormat.Jpeg);
                                fo.Thumb = SiteSettings.ForumThumbnailPath + Path.GetFileNameWithoutExtension(imgUrl) + "_t.jpg";

                                context.SaveChanges();
                                count++;
                            }

                        }
                        else
                        {

                            if (virPath.Trim().Length > 0)
                            {
                                log = log + "Info(" + forum.ThreadId.ToString() + ") - " + "Image does not exist in the server-" + virPath + "<br/>";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log = log + "Error(" + forum.ThreadId.ToString() + ") - " + ex.Message + "<br/>";
                    }
                }
                else
                {

                    log = log + "Info(" + forum.ThreadId.ToString() + ") - " + "External Image invalid -" + virPath + "<br/>";
                }
            }

            log = log + "Total articles processed: " + count.ToString() + "<br/>";
            lbl_log.Text = log;
        }
    }

    protected void ddl_content_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        if (ddl_content_type.SelectedValue.ToLower() == "article")
        {

            ddl_category.DataSource = from c in context.ArticleCategories
                                      orderby c.ArtCatId
                                      select new
                                      {
                                          Id = c.ArtCatId,
                                          Name = c.ArtCatId.ToString()+" - "+ c.CatName
                                      };
            ddl_category.DataTextField = "Name";
            ddl_category.DataValueField = "Id";
            ddl_category.DataBind();

                
        }
        lbl_log.Text = "";
    }


    private void  ProcessArticle(List<ArticleModel.Article> articles)
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        string log = "";
        log = "Total articles to process: " + articles.Count().ToString() + "<br/>";
        int count = 0;
        foreach (ArticleModel.Article article in articles)
        {

            List<string> imagestring = EPMImageExtractUtil.GetImagesFromArticleBody(article.Body, true);

            if (imagestring.Count > 0)
            {
                try
                {
                    if (context.ArticleThumbnails.Count(c => c.ThumbnailType.ThumbnailTypeName == "Small" && c.ArticleId == article.ArticleId) == 0)
                    {

                        EPM.Core.Article.Thumbnail.GenerateThumbnails(article.ArticleId, imagestring[0].ToString().Replace("../", "~/"));
                        log = log + "Success(" + article.ArticleId.ToString() + ") <br/>";
                    }
                    else
                    {
                        string small_path = context.ArticleThumbnails.Single(c => c.ThumbnailType.ThumbnailTypeName == "Small" && c.ArticleId == article.ArticleId).ThumbnailPath;
                        if (!File.Exists(Server.MapPath(small_path)))
                        {
                            EPM.Core.Article.Thumbnail.GenerateThumbnails(article.ArticleId, imagestring[0].ToString().Replace("../", "~/"));
                            log = log + "Success(" + article.ArticleId.ToString() + ") <br/>";
                        }
                        else
                            log = log + "Skip(" + article.ArticleId.ToString() + ") - Image already exists<br/>";
                    }

                }
                catch (Exception ex)
                {
                    log = log + "Error(" + article.ArticleId.ToString() + ") - " + ex.Message + "<br/>";
                }

            }

        }

        log = log + "Total articles processed: " + count.ToString() + "<br/>";
        lbl_log.Text = log;
        
    }

    private void ProcessBiz(List<BizModel.BusinessEntity> bizs)
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        string log = "";
        log = "Total biz to process: " + bizs.Count().ToString() + "<br/>";
        int count = 0;
        foreach (BizModel.BusinessEntity biz in bizs)
        {

            List<string> imagestring = context.BusinessEntityThumbnails.Where(x => x.BusinessEntityImage.BusinessEntityId == biz.BusinessEntityId && x.ThumbnailType.OriginalFg == true).Select(x => x.ThumbnailPath).ToList();// EPMImageExtractUtil.GetImagesFromArticleBody(article.Body, true);

            if (imagestring.Count > 0)
            {
                try
                {



                    if  (context.BusinessEntityThumbnails.Count(c => c.ThumbnailType.ThumbnailTypeName == "Small" && c.BusinessEntityImage.BusinessEntityId == biz.BusinessEntityId) == 0)
                    {

                                            EPM.Core.Biz.BizThumbnailProcessor.GenerateThumbnails(biz.BusinessEntityId, imagestring[0], true);
                        log = log + "Success(" + biz.BusinessEntityId.ToString() + ") <br/>";
                    }
                    else
                    {
                        string small_path = context.BusinessEntityThumbnails.Single(c => c.ThumbnailType.ThumbnailTypeName == "Small" && c.BusinessEntityImage.BusinessEntityId == biz.BusinessEntityId).ThumbnailPath;
                        if (!File.Exists(Server.MapPath(small_path)))
                        {
                            EPM.Core.Biz.BizThumbnailProcessor.GenerateThumbnails(biz.BusinessEntityId, imagestring[0], true);
                            log = log + "Success(" + biz.BusinessEntityId.ToString() + ") <br/>";
                        }
                        else
                            log = log + "Skip(" + biz.BusinessEntityId.ToString() + ") - Image already exists<br/>";
                    }

                }
                catch (Exception ex)
                {
                    log = log + "Error(" + biz.BusinessEntityId.ToString() + ") - " + ex.Message + "<br/>";
                }

            }

        }

        log = log + "Total biz processed: " + count.ToString() + "<br/>";
        lbl_log.Text = log;

    }

    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        if (ddl_content_type.SelectedValue.ToLower() == "article")
        {
            int totalArticle = context.Articles.Count(c => c.CategoryId == int.Parse(ddl_category.SelectedValue.ToString()));
            int ThumbnailCounter = context.ArticleThumbnails.Count(c => c.Article.ArticleCategory1.ArtCatId == int.Parse(ddl_category.SelectedValue.ToString()) && c.ThumbnailType.ThumbnailTypeName.ToLower() == "small");


            List<ArticleModel.Article> articles = (from c in context.Articles
                                                  where c.CategoryId == int.Parse(ddl_category.SelectedValue.ToString())
                                                  select c).ToList();
            int EleCounter = 0;
            foreach (ArticleModel.Article article in articles)
            {
                List<string> imagestring = EPMImageExtractUtil.GetImagesFromArticleBody(article.Body, true);

                if (imagestring.Count > 0)
                {
                    EleCounter++;
                }
            }

            lbl_catInfo.Text = "Total No Of Articles: " + totalArticle.ToString() + " / Total No Of Articles with Image(s): " + EleCounter.ToString() + " / Total No Of Articles to Process: " + (EleCounter - ThumbnailCounter).ToString();
        }

        lbl_log.Text = "";
    }



}