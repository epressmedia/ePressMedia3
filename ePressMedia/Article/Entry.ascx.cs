using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using EPM.Legacy.Security;
using EPM.Core;
using EPM.Core.Admin;
using EPM.ImageLibrary;
using EPM.ImageUtil;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;
using EPM.Business.Model;
using Telerik.Web.UI;
using Telerik.OpenAccess;
using EPM.Core.Pages;

namespace ePressMedia.Article
{
    public partial class ArticleEntryUC : DataEntryPage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ArticleEntryUC));
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        #region Properties



        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                try
                {

                    AccessControl ac = GetAccessControl(EPM.Legacy.Security.ResourceType.Article);
                    if (ac == null)
                        ac = new AccessControl(Permission.None);

                    if (!ac.CanWrite)
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    if (ac.HasFullControl)
                        Reporter.Text = "";


                    if ((ViewMode == EntryViewMode.Edit) && (ContentID > 0))
                        thumbnail_update.Visible = true;

                    IssueDatePicker.SelectedDate = DateTime.Now;




                    LoadVirtualCatList();
                    LoadArticleCategory();

                    if (ViewMode == EntryViewMode.View)
                        btn_Save.Visible = false;// PostLink.Visible = false;
                    if (ViewMode == EntryViewMode.Edit)
                    {
                        LoadArticleData();
                        LoadTagData();
                        LoadRelatedArticle();

                    }
                    
                    // Load Article Module General Settings
                    ArticleGeneralSettings();
                }
                catch (Exception ex)
                {
                    btn_Save.Visible = false;// PostLink.Visible = false;
                    log.Error(ex.Message);
                }
            }
        }


        #region Methods

        void ArticleGeneralSettings()
        {
            int maxsize = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Maximum article image upload size"));
            RadAsyncUpload1.MaxFileSize = maxsize * 1024;
            ltl_upload_note.Text = GetGlobalResourceObject("Resources", "Article.lbl_MaxImageSize").ToString() + maxsize.ToString() + " KB";
        }

        private void LoadArticleCategory()
        {
            // get the list of article category which user has write access
            List<ArticleModel.ArticleCategory> artcats =  EPM.Business.Model.Article.ArticleCategoryContoller.ActicleCategoriesWithPermission(Permission.Write);
            ddl_art_category.DataSource = artcats;
            ddl_art_category.DataTextField = "CatName";
            ddl_art_category.DataValueField = "ArtCatId";
            ddl_art_category.DataBind();

            //Not required becauase it is a requied field.
            //ddl_art_category.Items.Insert(0, new ListItem(GetGlobalResourceObject("Resources", "Gen.lbl_OptionSelect").ToString(), "-1"));

            // select the current category
            ddl_art_category.SelectedIndex = ddl_art_category.Items.IndexOf(ddl_art_category.Items.FindByValue(CategoryID.ToString()));


        }


        void LoadArticleData()
        {
            ArticleModel.Article a = context.Articles.Single(c => c.ArticleId == ContentID);

            Subject.Text = a.Title;
            SubTitle.Text = a.SubTitle;
            
            Reporter.Text = a.Reporter;
            IssueDatePicker.SelectedDate = a.IssueDate;
            
            //default the dropdown list

            ddl_art_category.SelectedIndex = ddl_art_category.Items.IndexOf(ddl_art_category.Items.FindByValue(a.CategoryId.ToString()));
            
            

            RadEditor1.Content = a.Body;

            if ((ViewMode == EntryViewMode.Edit) && (ddl_vc.Items.Count > 0))
            {
                List<ArticleModel.VirtualCategoryLink> vcl = (from c in context.VirtualCategoryLinks
                                                              where c.ArticleId == ContentID
                                                              select c).ToList();
                if (vcl.Count() > 0)
                {
                    ddl_vc.SelectedIndex = ddl_vc.Items.IndexOf(ddl_vc.Items.FindByValue(vcl[0].CatId.ToString()));
                }
            }
        }

        void LoadTagData()
        {


            List<ArticleModel.ArticleTag> tag = (from t in context.ArticleTags
                                                 where t.ArticleId == ContentID
                                                 select t).ToList();

            if (tag.Count > 0)
                myTagField.Value = tag[0].TagString;

        }

        void LoadRelatedArticle()
        {
            List<ArticleModel.Article> relatedarticles = (from r in context.RelatedArticles
                                                          join c in context.Articles on r.RelatedArticleId equals c.ArticleId
                                                          where r.ArticleId == ContentID
                                                          select c).ToList();
            lb_selectedResult.DataSource = relatedarticles;
            lb_selectedResult.DataTextField = "Title";
            lb_selectedResult.DataValueField = "ArticleId";
            lb_selectedResult.DataBind();
        }

        void LoadVirtualCatList()
        {

            List<ArticleModel.ArticleCategory> vc = (from c in context.ArticleCategories
                                                     where c.VirtualCat_fg == true
                                                     select c).ToList();

            ddl_vc.DataValueField = "ArtCatId";
            ddl_vc.DataTextField = "CatName";

            ddl_vc.DataSource = vc;
            ddl_vc.DataBind();

            if (vc.Count() > 0)
                ddl_vc.Items.Insert(0, new ListItem("-- " + GetGlobalResourceObject("Resources", "Article.lbl_SelectVirCat").ToString() + " -- ", "0"));
            else
                ddl_vc.Items.Insert(0, new ListItem("-- " + GetGlobalResourceObject("Resources", "Article.lbl_NoVirCatAvail").ToString() + " --", "0"));



        }

        void SaveVirtualDirectory()
        {

            ArticleModel.VirtualCategoryLink vc_link;
            // Virtual Directory
            if (ddl_vc.SelectedIndex > 0)
            {

                if (context.VirtualCategoryLinks.Count(c => c.ArticleId == ContentID) == 0)
                {
                    vc_link = new ArticleModel.VirtualCategoryLink();
                    vc_link.ArticleId = ContentID;
                    vc_link.CatId = int.Parse(ddl_vc.SelectedItem.Value);
                    context.Add(vc_link);
                }
                else
                {
                    vc_link = context.VirtualCategoryLinks.Single(c => c.ArticleId == ContentID);
                    vc_link.CatId = int.Parse(ddl_vc.SelectedItem.Value);

                }
                context.SaveChanges();

            }
            else
            {
                if (context.VirtualCategoryLinks.Count(c => c.ArticleId == ContentID) > 0)
                {
                    vc_link = context.VirtualCategoryLinks.Single(c => c.ArticleId == ContentID);
                    context.Delete(vc_link);
                    context.SaveChanges();
                }

            }
        }

        void SaveThumbnail()
        {
            ArticleModel.Article article_saved = context.Articles.Single(c => c.ArticleId == ContentID);

            List<string> imagestring = EPMImageExtractUtil.GetImagesFromArticleBody(article_saved.Body, true);

            if (imagestring.Count > 0)
            {

                if (imagestring.Count == 1)
                {

                    EPM.Core.Article.Thumbnail.GenerateThumbnails(ContentID, imagestring[0]);

                }
                else
                {
                    Session["ThumbnailImages"] = imagestring;
                    Session["ThumbnailArticleID"] = ContentID;
                    Response.Redirect("/Article/ArticleThumbnail.aspx");
                }
            }
        }


        void SaveTags()
        {

            // check if article tag record exists
            ArticleModel.ArticleTag tag = new ArticleModel.ArticleTag();

            if (context.ArticleTags.Count(c => c.ArticleId == ContentID) == 0)
            {
                // create a new record
                tag.ArticleId = ContentID;
                tag.TagString = myTagField.Value;
                context.Add(tag);
            }
            else
            {
                // update the existing record with the new tag strings
                tag = context.ArticleTags.Single(c => c.ArticleId == ContentID);
                tag.TagString = myTagField.Value;

            }

            context.SaveChanges();

        }

        void SaveRelatedArticles()
        {
            // if edit mode, delete existing related article (if exists)


            if (ViewMode == EntryViewMode.Edit)
            {
                if (context.RelatedArticles.Count(c => c.ArticleId == ContentID) > 0)
                {
                    IEnumerable<ArticleModel.RelatedArticle> articles = from c in context.RelatedArticles
                                                                        where c.ArticleId == ContentID
                                                                        select c;
                    context.Delete(articles);
                    context.SaveChanges();
                }
            }


            //Now add Relative Article no matter what.
            if (lb_selectedResult.Items.Count > 0)
            {

                foreach (RadListBoxItem item in lb_selectedResult.Items)
                {
                    ArticleModel.RelatedArticle relatedArticle = new ArticleModel.RelatedArticle();
                    relatedArticle.ArticleId = ContentID;
                    relatedArticle.RelatedArticleId = int.Parse(item.Value);
                    context.Add(relatedArticle);
                    context.SaveChanges();
                }

            }
        }
        #endregion



        #region Event Handler
        protected void btn_Save_Click(object sender, EventArgs e)
        {

            try
            {
                // Set the CategoryID from the categort dropdown
                CategoryID = int.Parse(ddl_art_category.SelectedItem.Value.ToString());

                //// (OLD)Need to reset the CategoryID property to avoid an issue from entering from different tab.
                //// CategoryID =    int.Parse(Request.QueryString["p"]);

                DateTime issueDate;
                try { issueDate = DateTime.Parse(IssueDatePicker.SelectedDate.Value.ToShortDateString() + " " + IssueDatePicker.SelectedDate.Value.ToShortTimeString()); }// IssueDatePicker.DbSelectedDate; }
                catch { issueDate = DateTime.Now; } // DateTime.Parse(DateTime.Now.ToShortDateString()); }

                //Abstract column is now storing all plain text of the content. And the number of characters will be controled in each widget retrieving the abstract
                //string abs = Abstract.Text.Trim().Length > 0 ? Abstract.Text : RadEditor1.Text; // commented out as the abstrat will be the conetent without html tags


                ArticleModel.Article article;

                if (ViewMode == EntryViewMode.Add)
                {
                    // Add Article
                    article = new ArticleModel.Article();
                    article.Title = Subject.Text;
                    article.SubTitle = SubTitle.Text;
                    article.Abstract = RadEditor1.Text;
                    article.Body = RadEditor1.Content;
                    article.IssueDate = issueDate;
                    article.PostDate = DateTime.Now;
                    article.CategoryId = CategoryID;
                    article.Reporter = Reporter.Text;
                    article.PostBy = "";
                    article.Suspended = false;
                    article.Hilighted = false;
                    article.Thumb_Path = "";
                    article.VideoId = "";
                    article.AllowComment = false;

                    context.Add(article);

                    context.SaveChanges();
                    ContentID = article.ArticleId;

                }
                else if (ViewMode == EntryViewMode.Edit)
                {

                    article = context.Articles.Single(c => c.ArticleId == ContentID);
                    article.CategoryId = CategoryID; // As the dropdown list is available in the edit button. the category can be updated
                    article.Title = Subject.Text;
                    article.SubTitle = SubTitle.Text;
                    article.Abstract = RadEditor1.Text;
                    article.Body = RadEditor1.Content;
                    article.IssueDate = issueDate;
                    article.PostDate = DateTime.Now;
                    article.Reporter = Reporter.Text;
                    article.PostBy = "";
                    article.Suspended = false;
                    article.VideoId = "";
                    article.AllowComment = false;

                    context.SaveChanges();

                }

                SaveVirtualDirectory();
                SaveTags();
                SaveRelatedArticles();


                // Call Thumbnail Process
                if (((ViewMode == EntryViewMode.Edit) && (chk_update_thumbnail.Checked)) || (ViewMode == EntryViewMode.Add))
                    SaveThumbnail();

                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, GetGlobalResourceObject("Resources", "Article.msg_PostSuccess").ToString());
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, GetGlobalResourceObject("Resources", "Gen.ErrorOccurContactAdmin").ToString());
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            lb_searchResult.Items.Clear();

            if (!string.IsNullOrEmpty(txt_Searchword.Text))
            {

                List<ArticleModel.Article> articles = EPM.Business.Model.Search.SearchController.SearchArticle(txt_Searchword.Text).OrderByDescending(c => c.PostDate).Take(60).ToList();
                lb_searchResult.DataSource = articles;
                lb_searchResult.DataTextField = "Title";
                lb_searchResult.DataValueField = "ArticleId";
                lb_searchResult.DataBind();
            }
        }

        protected void lb_searchResult_ItemDataBound(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {

        }
        protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            string targetPath = SiteSettings.ArticleUploadRoot.ToString();
            string filename = EPM.Core.FileHelper.GetSafeFileName(Server.MapPath(targetPath), e.File.FileName);

            ImageAsyncUploadResult result = e.UploadResult as ImageAsyncUploadResult;
            string tempFileLocation = result.TempFileLocation;
            int index = tempFileLocation.LastIndexOf("\\\\");

            string replaceFilename = tempFileLocation.Substring(index + 2, tempFileLocation.Length - index - 2);

            e.File.SaveAs(Server.MapPath(targetPath) + "\\" + filename);

            string test = targetPath.Replace("~", "");
            string test1 = filename;

            RadEditor1.Content = RadEditor1.Content.Replace("/Article/StreamImage.ashx?path=/RadUploadTemp/" + replaceFilename, targetPath.Replace("~", "") + "/" + filename);

        }
        protected void lb_searchResult_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            foreach (RadListBoxItem item in e.Items)
            {
                //Update the image
                if (e.SourceListBox == lb_searchResult)
                {
                    item.Value = item.Value;
                    item.Text = item.Text;
                }

                //Databind the item in order to evaluate the databinding expressions from the template
                item.DataBind();
            }
        }

        #endregion
    }
}