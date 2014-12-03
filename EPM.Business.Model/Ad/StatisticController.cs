using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Ad
{
    public static class StatisticController
    {

        public static void CountExpose(int BannerId, int ZoneId, string PageURL, Char ExposeType)
    {
        EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
        AdModel.AdExpose expose = new AdModel.AdExpose();
        expose.AdBannerId = BannerId;
        expose.AdZoneId = ZoneId;
        expose.ExposeDate = DateTime.Now;
        expose.PageURL = PageURL;
        expose.ExposeType = ExposeType;
        context.Add(expose);
        context.SaveChanges();
    }

        public static IQueryable<AdModel.AdExpose> GetExposeByBannerId(int BannerId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return (from c in context.AdExposes
                    where c.AdBannerId == BannerId
                    select c);
        }

        public static IQueryable<AdModel.AdExpose> GetExposeByBannerZoneId(int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return (from c in context.AdExposes
                    where c.AdZoneId == ZoneId
                    select c);
        }
    }
}
