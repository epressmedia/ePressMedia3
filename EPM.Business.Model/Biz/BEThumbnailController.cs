using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace EPM.Business.Model.Biz
{
    public static class BEThumbnailController
    {
        private static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static IQueryable<BizModel.BusinessEntityThumbnail> GetDefaultBizThumbnailsByBEID(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.BusinessEntityThumbnails.Where(c => c.BusinessEntityImage.BusinessEntityId == BEID && c.ThumbnailType.DefaultFg == true );
        }

        public static void AddBizThumbnailPath(int BEImageID, int ThumbnailTypeID, string Path)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            //if the thumbnail ever exists, then just update
            if (context.BusinessEntityThumbnails.Count(c => c.BusinessEntityImageId == BEImageID && c.ThumbnailTypeId == ThumbnailTypeID) > 0)
            {
                // the current image path and delete it if exists
                string old_image_path = context.BusinessEntityThumbnails.Single(c => c.BusinessEntityImageId == BEImageID && c.ThumbnailTypeId == ThumbnailTypeID).ThumbnailPath;
                if (!string.IsNullOrEmpty(old_image_path) && old_image_path != Path)
                    DeleteFile(HttpContext.Current.Server.MapPath(old_image_path));

                BizModel.BusinessEntityThumbnail thumbnail = context.BusinessEntityThumbnails.Single(c => c.BusinessEntityImageId == BEImageID && c.ThumbnailTypeId == ThumbnailTypeID);
                thumbnail.ThumbnailPath = Path;
                context.SaveChanges();
            }
            else
            {
                //if doesn't exists, create one

                

                BizModel.BusinessEntityThumbnail thumbnail = new BizModel.BusinessEntityThumbnail();
                thumbnail.BusinessEntityImageId = BEImageID;
                thumbnail.ThumbnailTypeId = ThumbnailTypeID;
                thumbnail.ThumbnailPath = Path;
                context.Add(thumbnail);
                context.SaveChanges();
            }
        }

        public static bool BizThumbnailExists(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.BusinessEntityThumbnails.Any(x => x.BusinessEntityImage.BusinessEntityId == BEID && x.ThumbnailType.OriginalFg == false);
        }

        public static string GetBizThumbnailPath(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return GetBizThumbnailPath(BEID, context.ThumbnailTypes.Single(c => c.DefaultFg == true).ThumbnailTypeId);
        }

        public static string GetBizThumbnailPath(int BEID, int ThumbnailTypeID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntityThumbnails.Count(c => c.BusinessEntityImage.BusinessEntityId == BEID && c.ThumbnailTypeId == ThumbnailTypeID && c.BusinessEntityImage.PrimaryFg == true) > 0)
                return context.BusinessEntityThumbnails.Single(c => c.BusinessEntityImage.BusinessEntityId == BEID && c.ThumbnailTypeId == ThumbnailTypeID && c.BusinessEntityImage.PrimaryFg == true).ThumbnailPath;
            else
                return "";
        }
        public static string GetBizThumbnailPath(int BEID, string ThumbnailTypeName)
        {
            if (string.IsNullOrEmpty(ThumbnailTypeName))
            {
                return GetBizThumbnailPath(BEID);
            }
            else
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                if (context.BusinessEntityThumbnails.Count(c => c.BusinessEntityImage.BusinessEntityId == BEID && c.ThumbnailType.ThumbnailTypeName == ThumbnailTypeName && c.BusinessEntityImage.PrimaryFg == true) > 0)
                    return context.BusinessEntityThumbnails.Single(c => c.BusinessEntityImage.BusinessEntityId == BEID && c.ThumbnailType.ThumbnailTypeName == ThumbnailTypeName && c.BusinessEntityImage.PrimaryFg == true).ThumbnailPath;
                else
                    return GetBizThumbnailPath(BEID);
            }
        }
        public static string GetBizThumbSourceImagePath(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            SiteModel.ThumbnailType thumb = context.ThumbnailTypes.Single(c => c.OriginalFg == true);

            return GetBizThumbnailPath(BEID, thumb.ThumbnailTypeId);
        }

    }
}
