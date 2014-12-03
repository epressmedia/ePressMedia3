using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.ImageUtil;
using EPM.ImageLibrary;
using EPM.Data.Model;
using EPM.Core.Admin;
using log4net;

namespace ePressMedia.Article
{
    public partial class ArticleThumbnail : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ArticleThumbnail));
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                EPM.Core.Common.StyleSheetLoader.LoadStyleSheets();


                if ((Session["ThumbnailImages"] != null) && (Session["ThumbnailArticleID"] != null))
                {
                    List<string> images = (List<string>)(Session["ThumbnailImages"]);
                    thumnail_repeater.DataSource = images;
                    thumnail_repeater.DataBind();

                }
                else
                {
                    btn_ok.Visible = false;
                }
            }


        }

        protected void thumnail_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Image image = e.Item.FindControl("thumbnail_image") as Image;
            RadioButton radiobutton = e.Item.FindControl("thumbnail_radio") as RadioButton;
            string item = (string)(e.Item.DataItem);
            image.ImageUrl = item;
            if (e.Item.ItemIndex == 0)
            {
                radiobutton.Checked = true;
                txt_url.Text = item;
            }
            radiobutton.GroupName = "Thumbnail_radio";
            radiobutton.Attributes.Add("value", item);
            radiobutton.Attributes.Add("onclick", "SetUniqueRadioButton('Thumbnail_radio',this)");
        }
        protected void btn_OK_Click(object sender, EventArgs e)
        {




            if (String.IsNullOrEmpty(txt_url.Text))
            {
                lbl_msg.Visible = true;
            }
            else
            {
                try
                {
                    string imgURL = txt_url.Text;
                    GenerateThumbnail(imgURL);

                    EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Article has been succesfully submitted.");
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Article has been succesfully submitted. But an error occured during thumbnail genertaion.\n Please contact Administrator for further assisntace");
                }
            }
        }


        private void GenerateThumbnail(string imgUrl)
        {
            //EPM.Data.Model.EPMEntityModel context = new EPMEntityModel();
            int ArticleID = int.Parse(Session["ThumbnailArticleID"].ToString());
            //ArticleModel.Article article_saved = context.Articles.Single(c => c.ArticleId == ArticleID);


            EPM.Core.Article.Thumbnail.GenerateThumbnails(ArticleID, imgUrl);
          
                //string Thumbnail_path = Server.MapPath(SiteSettings.ArticleThumbnailPath);
                //string thumb_width = SiteSettings.GetSiteSettingValueByName("Article Thumbnail Width");
                //string thumb_height = SiteSettings.GetSiteSettingValueByName("Article Thumbnail Height");
                //string thumb_mode = SiteSettings.GetSiteSettingValueByName("Article Thumbnail Mode");
                //string thumb_bgcolor = SiteSettings.GetSiteSettingValueByName("Article Thumbnail Background Color");

                    
                //    string safeFileName = "";

                //    string prefix = imgUrl.Length > 8 ? imgUrl.Substring(0, 8).ToLower() : "";

                //    if (prefix.StartsWith("http://") || prefix.StartsWith("https://"))
                //    {

                //        safeFileName = EPM.Core.FileHelper.GetSafeFileName(Thumbnail_path + "\\" + "external"+DateTime.Now.ToString("ddMMYYYY")+"_"+ArticleID + "_" + thumb_width + "x" + thumb_height + ".jpg");
                //    }
                //    else
                //    {
                //        safeFileName = EPM.Core.FileHelper.GetSafeFileName(Thumbnail_path + "\\" + System.IO.Path.GetFileNameWithoutExtension(imgUrl) + "_" + thumb_width + "x" + thumb_height + ".jpg");
                //        imgUrl = Server.MapPath(imgUrl);
                //    }

                //    EPMImage thumbImage = new EPMImage(imgUrl);
                //    thumbImage.BackgroundColor = string.IsNullOrEmpty(thumb_bgcolor) ? System.Drawing.Color.Transparent : System.Drawing.ColorTranslator.FromHtml(thumb_bgcolor);
                //    thumbImage.GetThumbnailImage(int.Parse(thumb_width), int.Parse(thumb_height), (ThumbnailMethod)Enum.Parse(typeof(ThumbnailMethod), thumb_mode)).Image.Save(safeFileName);

                //    article_saved.Thumb_Path = SiteSettings.ArticleThumbnailPath + System.IO.Path.GetFileName(safeFileName);// Path.GetFileNameWithoutExtension(imgUrl) + "_t.jpg";
                //    context.SaveChanges();
        }
    }
}