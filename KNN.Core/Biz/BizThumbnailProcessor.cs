using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EPM.Core.Admin;
using EPM.ImageLibrary;
using System.IO;
using System.Drawing;
using EPM.Business.Model.Biz;
using Brettle.Web.NeatUpload;


namespace EPM.Core.Biz
{
    public static class BizThumbnailProcessor
    {
        public static void GenerateThumbnails(int BEID, string imgURL, bool PrimaryFg)
        {
            ProcessThumbnail(BEID, imgURL, "", PrimaryFg);
        }

        public static string UploadImages(int id, Brettle.Web.NeatUpload.UploadedFile[] uploadFiles)
        {
            string uploadPath = EPM.Core.Admin.SiteSettings.BizUploadRoot;
            EPM.Core.FileHelper.FolderExists(HttpContext.Current.Server.MapPath(uploadPath), true);
            string firstImgUrl = null;

            int i = 0;
            for (; i < uploadFiles.Length; i++)
            {
                UploadedFile file = uploadFiles[i];
                string saveName = HttpContext.Current.Server.MapPath(uploadPath) + "\\" + id.ToString() + "_" + file.FileName;

                string savedPath = ImageUtilityLegacy.ResizeAndSaveImage(file, saveName, 2000);

                EPM.Core.Biz.BizThumbnailProcessor.GenerateThumbnails(id, uploadPath + "/" + Path.GetFileName(saveName), (i == 0));
                if (i == 0) // first image. to be cropped in the next page
                    firstImgUrl = uploadPath + "/" + Path.GetFileName(saveName);
            }

            return firstImgUrl;
        }

        private static void ProcessThumbnail(int BEID, string imgURL, string ThumbnailType, bool PrimaryFg)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();


            string Thumbnail_path = HttpContext.Current.Server.MapPath(SiteSettings.BizThumbnailPath);
            EPM.Core.FileHelper.FolderExists(HttpContext.Current.Server.MapPath(SiteSettings.BizThumbnailPath), true);
            //string thumb_bgcolor = SiteSettings.GetSiteSettingValueByName("Article Thumbnail Background Color");

            int BEImageID = BEImageController.AddBEImage(BEID, PrimaryFg);
            // Save the original image URL
            BEThumbnailController.AddBizThumbnailPath(BEImageID, context.ThumbnailTypes.Single(c => c.OriginalFg == true).ThumbnailTypeId, imgURL);
            

            imgURL = HttpContext.Current.Server.MapPath(imgURL);


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


                    safeFileName = EPM.Core.FileHelper.GetSafeFileName(Thumbnail_path + "\\" + Path.GetFileNameWithoutExtension(imgURL) + "_" + BEID.ToString() +"_" + thumb_width.ToString() + "x" + thumb_height.ToString() + ".jpg");

           


                    //thumbImage.BackgroundColor = string.IsNullOrEmpty(thumb_bgcolor) ? Color.Transparent : ColorTranslator.FromHtml(thumb_bgcolor);
                    thumbImage.GetThumbnailImage(thumb_width, thumb_height, (ThumbnailMethod)Enum.Parse(typeof(ThumbnailMethod), thumb_mode)).Image.Save(safeFileName);

                    //BizModel.BusinessEntityImage be = context.BusinessEntityImages.Single(c => c.BusinessEntityId == BEID);
                    //if (thumbType.DefaultFg)
                    //{
                    //    be.PrimaryFg = true;
                    //    //article_saved.Thumb_Path = SiteSettings.ArticleThumbnailPath + Path.GetFileName(safeFileName);// Path.GetFileNameWithoutExtension(imgUrl) + "_t.jpg";
                    //    context.SaveChanges();
                    //}

                    BEThumbnailController.AddBizThumbnailPath(BEImageID, thumbType.ThumbnailTypeId, SiteSettings.BizThumbnailPath + Path.GetFileName(safeFileName));
                    //ArticleThumbnailController.AddArticleThumbnailPath(BEID, thumbType.ThumbnailTypeId, SiteSettings.BizThumbnailPath + Path.GetFileName(safeFileName));
                }

            }
        }
    }
}
