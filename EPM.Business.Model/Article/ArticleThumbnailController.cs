using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Data.Model;
using System.Web;
using System.IO;
using EPM.ImageLibrary;
using System.Drawing;


namespace EPM.Business.Model.Article
{
    public static class ArticleThumbnailController
    {

        private static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void AddArticleThumbnailPath(int ArticleID, int ThumbnailTypeID, string Path)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.ArticleThumbnails.Count(c => c.ArticleId == ArticleID && c.ThumbnailTypeId == ThumbnailTypeID) > 0)
            {
                // the current image path and delete it if exists
                // Process except for the orginal files
                if (context.ArticleThumbnails.Any(c => c.ArticleId == ArticleID && c.ThumbnailTypeId == ThumbnailTypeID && c.ThumbnailType.OriginalFg == false))
                {
                    string old_image_path = context.ArticleThumbnails.Single(c => c.ArticleId == ArticleID && c.ThumbnailTypeId == ThumbnailTypeID && c.ThumbnailType.OriginalFg == false).ThumbnailPath;
                    if (!string.IsNullOrEmpty(old_image_path) && old_image_path != Path)
                    {
                        if (!old_image_path.StartsWith("http://"))
                        {
                            string server_path = HttpContext.Current.Server.MapPath(old_image_path);
                            if (File.Exists(server_path))
                                DeleteFile(server_path);
                        }
                    }
                }

                ArticleModel.ArticleThumbnail thumbnail = context.ArticleThumbnails.Single(c => c.ArticleId == ArticleID && c.ThumbnailTypeId == ThumbnailTypeID);
                thumbnail.ThumbnailPath = Path;
                context.SaveChanges();
            }
            else
            {
                //if doesn't exists, create one
                ArticleModel.ArticleThumbnail thumbnail = new ArticleModel.ArticleThumbnail();
                thumbnail.ArticleId = ArticleID;
                thumbnail.ThumbnailTypeId = ThumbnailTypeID;
                thumbnail.ThumbnailPath = Path;
                context.Add(thumbnail);
                context.SaveChanges();
            }
        }

        public static bool ArticleThumbnailExists(int ArticleID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.ArticleThumbnails.Any(x => x.ArticleId == ArticleID && x.ThumbnailType.OriginalFg == false);
        }

        public static string GetArticleThumbnailPath(int ArticleID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return GetArticleThumbnailPath(ArticleID, context.ThumbnailTypes.Single(c => c.DefaultFg == true).ThumbnailTypeId);
        }

        public static string GetArticleThumbnailPath(int ArticleID, int ThumbnailTypeID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.ArticleThumbnails.Count(c => c.ArticleId == ArticleID && c.ThumbnailTypeId == ThumbnailTypeID) > 0)
                return context.ArticleThumbnails.Single(c => c.ArticleId == ArticleID && c.ThumbnailTypeId == ThumbnailTypeID).ThumbnailPath;
            else
                return "";
        }
        public static string GetArticleThumbnailPath(int ArticleID, string ThumbnailTypeName)
        {
            if (string.IsNullOrEmpty(ThumbnailTypeName))
            {
                return GetArticleThumbnailPath(ArticleID);
            }
            else
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                if (context.ArticleThumbnails.Count(c => c.ArticleId == ArticleID && c.ThumbnailType.ThumbnailTypeName == ThumbnailTypeName) > 0)
                    return context.ArticleThumbnails.Single(c => c.ArticleId == ArticleID && c.ThumbnailType.ThumbnailTypeName == ThumbnailTypeName).ThumbnailPath;
                else
                    return GetArticleThumbnailPath(ArticleID);
            }
        }
        public static string GetArticleThumbSourceImagePath(int ArticleID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            if (ArticleThumbnailExists(ArticleID))
            {
                SiteModel.ThumbnailType thumb = context.ThumbnailTypes.Single(c => c.OriginalFg == true);

                return GetArticleThumbnailPath(ArticleID, thumb.ThumbnailTypeId);
            }
            else
            {
                return "";
            }
        }
    }
}
