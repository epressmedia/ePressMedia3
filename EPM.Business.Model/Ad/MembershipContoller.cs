using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Ad
{
    public static class MembershipContoller
    {

        public static void AddMembership(int BannerId, int ZoneId, int Sequence)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdZoneMember member = new AdModel.AdZoneMember();
            member.AdBannerId = BannerId;
            member.AdZoneId = ZoneId;
            member.Sequence = Sequence;
            context.Add(member);
            context.SaveChanges();
        }

        public static void AddMembership(IList<AdModel.AdBanner> banners, int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            int count = 1;
            foreach (AdModel.AdBanner banner in banners)
            {
                AddMembership(banner.AdBannerId, ZoneId, count);
                count++;
            }
        }

        public static void AddMembership(List<int> BannerIDList, int ZoneId)
        {   
            int count=1;
            foreach (int BannerId in BannerIDList)
            {
                AddMembership(BannerId, ZoneId, count);
                count++;
            }
        }

        public static void DeleteMembership(int BannerId, int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdZoneMember member = context.AdZoneMembers.Single(c => c.AdZoneId == ZoneId && c.AdBannerId == BannerId);
            context.Delete(member);
            context.SaveChanges();
        }


        public static void DeleteMembership(IList<AdModel.AdBanner> banners, int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            foreach (AdModel.AdBanner banner in banners)
            {
                DeleteMembership(banner.AdBannerId, ZoneId);
            }
        }

        public static void InitiateMembership(int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (context.AdZoneMembers.Any(c => c.AdZoneId == ZoneId))
            {
                context.Delete(context.AdZoneMembers.Where(c => c.AdZoneId == ZoneId));
                context.SaveChanges();
            }
        }

        public static IQueryable<AdModel.AdBanner> GetMembersByZoneId(int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            IQueryable<AdModel.AdBanner> banners =  (from b in context.AdBanners
             join m in context.AdZoneMembers on b.AdBannerId equals m.AdBannerId
             where b.StartDate <= DateTime.Now && (b.EndDate == null || b.EndDate >= DateTime.Now)
             && b.ActiveFg == true && m.AdZoneId == ZoneId
             orderby m.Sequence
             select b);

            int count = 1;
            foreach (AdModel.AdBanner banner in banners)
            {
                banner.BannerSequence = count;
                count++;
            }

            return banners;
        }
        public static IQueryable<AdModel.AdBanner> GetMemberByZoneIdWithWeightApply(int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            IQueryable<AdModel.AdBanner> banners = GetMembersByZoneId(ZoneId);

            List<string> stringlist = new List<string>();

            foreach (AdModel.AdBanner banner in banners)
            {

                int weight_count =banner.Weight;
                while (weight_count > 0)
                {
                    stringlist.Add(banner.AdBannerId.ToString());
                    weight_count--;
                }
                
            }



            Random rd = new Random();
            int rd_number = rd.Next(stringlist.Count());

            int bannerId = int.Parse(stringlist[rd_number]);

            return banners.Where(c=>c.AdBannerId == bannerId).Take(1);
        }


        public static IQueryable<AdModel.AdBanner> GetRandomOrderMembersByZoneId(int ZoneId)
        {
            IQueryable<AdModel.AdBanner> banners = GetMembersByZoneId(ZoneId);
            Random rd = new Random();
            
            foreach (AdModel.AdBanner banner in banners)
            {
                int sequence = rd.Next(banners.Count() * 2);
                banner.BannerSequence = sequence;
            }
            return banners;



        }


    }
}
