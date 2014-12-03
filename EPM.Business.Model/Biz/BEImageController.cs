using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EPM.Data.Model;
using System.Web;

namespace EPM.Business.Model.Biz
{
    public class BEImageController
    {
        public static IQueryable<BizModel.BusinessEntityImage> GetBEImagesByBEID(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.BusinessEntityImages
                   where c.BusinessEntityId == BEID
                   select c;
        }
        public static int AddBEImage(int BEID, bool primaryFg)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessEntityImage image = new BizModel.BusinessEntityImage();
            image.BusinessEntityId = BEID;
            image.PrimaryFg = primaryFg;
            context.Add(image);
            context.SaveChanges();
            return image.BusinessEntityImageId;

        }

        public static void DeleteAllImages(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<BizModel.BusinessEntityThumbnail> thumbnails = context.BusinessEntityThumbnails.Where(c => c.BusinessEntityImage.BusinessEntityId == BEID).ToList();
            foreach (BizModel.BusinessEntityThumbnail thunbmail in thumbnails)
            {
                context.Delete(thunbmail);
            }

            List<BizModel.BusinessEntityImage> images = context.BusinessEntityImages.Where(c => c.BusinessEntityId == BEID).ToList();
            foreach (BizModel.BusinessEntityImage image in images)
            {
                context.Delete(image);
            }

            context.SaveChanges();
            
        }

    }
}
