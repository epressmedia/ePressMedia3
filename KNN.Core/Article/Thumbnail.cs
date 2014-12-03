using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EPM.Core.Admin;
using EPM.ImageLibrary;
using System.IO;
using System.Drawing;
using EPM.Business.Model.Article;

namespace EPM.Core.Article
{
    public static class Thumbnail
    {
        public static void GenerateThumbnails(int ArticleID, string imgURL, string ThumbnailType)
        {
            ProcessThumbnail(ArticleID, imgURL, ThumbnailType);
        }

        public static void GenerateThumbnails(int ArticleID, string imgURL)
        {
            ProcessThumbnail(ArticleID, imgURL, "");
        }

        private static void ProcessThumbnail(int ArticleID, string imgURL, string ThumbnailType)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ArticleModel.Article article_saved = context.Articles.Single(c => c.ArticleId == ArticleID);

            string Thumbnail_path = HttpContext.Current.Server.MapPath(SiteSettings.ArticleThumbnailPath);
            //string thumb_bgcolor = SiteSettings.GetSiteSettingValueByName("Article Thumbnail Background Color");


            // Save the original image URL
            ArticleThumbnailController.AddArticleThumbnailPath(ArticleID, context.ThumbnailTypes.Single(c => c.OriginalFg == true).ThumbnailTypeId, imgURL);

            string prefix = imgURL.Length > 8 ? imgURL.Substring(0, 8).ToLower() : "";
            if (prefix.StartsWith("http://") || prefix.StartsWith("https://"))
            {
                imgURL = imgURL;
            }
            else
            {
                imgURL = HttpContext.Current.Server.MapPath(imgURL);
            }

            EPMImage thumbImage = new EPMImage(imgURL);


            
            List<SiteModel.ThumbnailType> types = (from c in context.ThumbnailTypes
                                                   where c.OriginalFg == false
                                                   select c).ToList();
            if (!string.IsNullOrEmpty(ThumbnailType))
                types = types.Where(c => c.ThumbnailTypeName == ThumbnailType).ToList();

            foreach (SiteModel.ThumbnailType thumbType in types)
            {

                int thumb_width = thumbType.Width;// SiteSettings.GetSiteSettingValueByName("Article Thumbnail Width");
                int thumb_height = thumbType.Height;// SiteSettings.GetSiteSettingValueByName("Article Thumbnail Height");
                string thumb_mode = thumbType.ProcessType;// SiteSettings.GetSiteSettingValueByName("Article Thumbnail Mode");

                if ((thumb_width != 0) && (thumb_height != 0))
                {

                    string safeFileName = "";

                    if (prefix.StartsWith("http://") || prefix.StartsWith("https://"))
                    {

                        safeFileName = EPM.Core.FileHelper.GetSafeFileName(Thumbnail_path + "\\" + "external_" + DateTime.Now.ToString("ddMMyyyy") + "_" + ArticleID + "_" + thumb_width.ToString() + "x" + thumb_height.ToString() + ".jpg");
                    }
                    else
                    {
                        safeFileName = EPM.Core.FileHelper.GetSafeFileName(Thumbnail_path + "\\" + Path.GetFileNameWithoutExtension(imgURL) + "_" + thumb_width.ToString() + "x" + thumb_height.ToString() + ".jpg");

                    }


                    //thumbImage.BackgroundColor = string.IsNullOrEmpty(thumb_bgcolor) ? Color.Transparent : ColorTranslator.FromHtml(thumb_bgcolor);
                    thumbImage.GetThumbnailImage(thumb_width, thumb_height, (ThumbnailMethod)Enum.Parse(typeof(ThumbnailMethod), thumb_mode)).Image.Save(safeFileName);

                    if (thumbType.DefaultFg)
                    {
                        article_saved.Thumb_Path = SiteSettings.ArticleThumbnailPath + Path.GetFileName(safeFileName);// Path.GetFileNameWithoutExtension(imgUrl) + "_t.jpg";
                        context.SaveChanges();
                    }

                    ArticleThumbnailController.AddArticleThumbnailPath(ArticleID, thumbType.ThumbnailTypeId, SiteSettings.ArticleThumbnailPath + Path.GetFileName(safeFileName));
                }

            }
        }
    }
}
