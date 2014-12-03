using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Ad
{
    public static class BannerContoller
    {
        public static IQueryable<AdModel.AdBanner> GetAllAdBanners()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return (from c in context.AdBanners
                    select c);
        }

        public static IQueryable<AdModel.AdBanner> GetAllActiveAdBanenrs()
        {
            return GetAllAdBanners().Where(c => c.ActiveFg == true);
        }

        public static IQueryable<AdModel.AdBanner> GetBannersByZoneId(int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return GetAllAdBanners().Where(c => c.AdZoneMembers.Any(a=>a.AdZoneId == ZoneId));
        }

        public static IQueryable<AdModel.AdBanner> GetBannersByBusinessEntityId(int BusinessEntityId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return GetAllAdBanners().Where(c => c.BusinessEntityId == BusinessEntityId);
        }

        public static AdModel.AdBanner GetBannnerById(int BannerId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (context.AdBanners.Any(c => c.AdBannerId == BannerId))
            {
                return context.AdBanners.Single(c => c.AdBannerId == BannerId);
            }
            else
                return null;
        }

        public static int  AddBanner(int BusinessEntityId, string Description, DateTime StartDate, DateTime EndDate, Guid CreatedBy, int MediaTypeId,int Weight, int? Width, int? Height, string LinkURL, string LinkTarget, string LinkAltString, string SourcePath, string SourceContent)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdBanner banner = new AdModel.AdBanner();
            banner.BusinessEntityId = BusinessEntityId;
            banner.StartDate = StartDate;
            if (EndDate != DateTime.MinValue) 
                banner.EndDate = EndDate;
            banner.Description = Description;
            banner.CreatedBy = CreatedBy;
            banner.CreatedDate = DateTime.Now;
            banner.MediaTypeId = MediaTypeId;
            banner.ActiveFg = true;
            banner.ModifiedBy = null;
            banner.ModifiedDate = null;
            banner.LinkURL = LinkURL;
            banner.LinkTarkget = LinkTarget;
            banner.LinkAltString = LinkAltString;
            banner.Width = Width==0?null :Width;
            banner.Height = Height==0?null:Height;
            banner.SourcePath = SourcePath;
            banner.SourceContent = SourceContent;
            banner.Weight = Weight;
            context.Add(banner);
            context.SaveChanges();
            return banner.AdBannerId;
        }

        public static void UpdateBanner(int BannerId, string Description, DateTime StartDate, DateTime EndDate, Guid ModifiedBy, int MediaTypeId,int Weight, int? Width, int? Height, bool ActiveFg, string LinkURL, string LinkTarget, string LinkAltString, string SourcePath, string SourceContent)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdBanner banner = context.AdBanners.Single(c => c.AdBannerId == BannerId);
            banner.Description = Description;
            banner.StartDate = StartDate;
            if (EndDate != DateTime.MinValue)
                banner.EndDate = EndDate;
            else
                banner.EndDate = null;
            banner.ModifiedBy = ModifiedBy;
            banner.ModifiedDate = DateTime.Now;
            banner.MediaTypeId = MediaTypeId;
            banner.LinkURL = LinkURL;
            banner.LinkTarkget = LinkTarget;
            banner.LinkAltString = LinkAltString;
            banner.Width = Width == 0 ? null : Width;
            banner.Height = Height == 0 ? null : Height;
            banner.SourcePath = SourcePath;
            banner.SourceContent = SourceContent;
            banner.Weight = Weight;
            banner.ActiveFg = ActiveFg;
            context.SaveChanges();
        }

        public static void DeleteBanner(int BannerID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            var expose = context.AdExposes.Where(x => x.AdBannerId == BannerID);
            context.Delete(expose);
            var zonemember = context.AdZoneMembers.Where(x => x.AdBannerId == BannerID);
            context.Delete(zonemember);
            var banner = context.AdBanners.Where(c => c.AdBannerId == BannerID);
            context.Delete(banner);

            context.SaveChanges();
        }
    }
}
