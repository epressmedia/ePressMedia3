using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EPM.Business.Model.Ad
{
    public static class AdContoller
    {

        public static IQueryable<AdModel.AdMediaType> GetMediaTypes()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return (from c in context.AdMediaTypes
                    select c);
        }

        public static AdModel.AdMediaType GetMediaTypeByName(string name)
        {
            return GetMediaTypes().Single(c => c.MediaTypeName == name);
        }


        public static AdModel.AdMediaType GetMediaTypeById(int Id)
        {
            return GetMediaTypes().Single(c => c.AdMediaTypeId == Id);
        }

        public static IQueryable<AdModel.AdRefSize> GetAllSizes()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return (from c in context.AdRefSizes
                    select c);
        }

        public static AdModel.AdRefSize GetSizeById(int id)
        {
            return GetAllSizes().Single(c => c.AdSizeId == id);
        }

        public static void AddAdSize(string SizeName, int Height, int Width)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdRefSize size = new AdModel.AdRefSize();
            size.AdSizeName = SizeName;
            size.Height = Height;
            size.Width = Width;
            context.Add(size);
            context.SaveChanges();
        }

        public static void UpdateAdSize(int SizeId, string SizeName, int Height, int Width)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdRefSize size = context.AdRefSizes.Single(c => c.AdSizeId == SizeId);
            size.AdSizeName = SizeName;
            size.Height = Height;
            size.Width = Width;
            context.SaveChanges();
        }


        public static IQueryable<AdModel.AdZoneActionType> GetAllZoneActionTypes()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.AdZoneActionTypes;
        }

        public static AdModel.AdZoneActionType GetZoneActionTypeById(int Id)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.AdZoneActionTypes.Where(c => c.AdZoneActionTypeId == Id).SingleOrDefault();
        }

        public static IQueryable<BizModel.BusinessEntity> GetBusinessEntitiesWithActiveBanner()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            int[] BusinessEntityIDs = context.AdBanners.Where(c=>c.ActiveFg).Select(c=>c.BusinessEntityId).ToArray();
            return context.BusinessEntities.Where(c => BusinessEntityIDs.Contains(c.BusinessEntityId));
        }

        //public static IQueryable<AdModel.AdBanner> GetBannersByOrder(int ZoneId, bool Random, bool WeightApply)
        //{
        //    EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
        //    IQueryable<AdModel.AdBanner> final_banners = null;

        //    if (!Random)
        //    {
        //        final_banners = (from b in context.AdBanners
        //                         join m in context.AdZoneMembers on b.AdBannerId equals m.AdBannerId
        //                         where m.AdZoneId == ZoneId
        //                         orderby m.Sequence
        //                         select b);
        //    }
        //    else
        //    {
                
        //        if (WeightApply)
        //        {
        //            // get all banners with more than 1 weight add additional item
        //            List<AdModel.AdBanner> weightBanners = context.AdBanners.Where(c => c.Weight > 1).ToList();
        //            foreach (AdModel.AdBanner weightBanner in weightBanners)
        //            {
        //                int count = weightBanner.Weight;
        //                while (count > 1)
        //                {
        //                    AdBanners.ToList().Add(weightBanner);
        //                    count--;
        //                }
        //            }
        //        }

        //        Random rd = new Random();

        //        final_banners = (from b in banners
        //                         join m in context.AdZoneMembers on b.AdBannerId equals m.AdBannerId
        //                         where m.AdZoneId == ZoneId
        //                         orderby rd.Next(100)
        //                         select b);
        //    }
        //    return final_banners;
        //}
    }
}
