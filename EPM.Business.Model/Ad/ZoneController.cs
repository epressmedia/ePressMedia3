using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.OpenAccess;

namespace EPM.Business.Model.Ad
{
    public class ZoneController: Repository<AdModel.AdZone>, IZoneContoller
    {
        public ZoneController(IUnitOfWork context)
            : base(context)
        {

        }

        public static IQueryable<AdModel.AdZone> GetAllZones()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return (from c in context.AdZones
                    select c);
        }

        public static AdModel.AdZone GetZoneById(int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (context.AdZones.Any(c => c.AdZoneId == ZoneId))
            {
                return context.AdZones.Single(c => c.AdZoneId == ZoneId);
            }
            else
                return null;
        }

        public static void DeleteZone(int ZoneId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (!context.AdZoneMembers.Any(c => c.AdZoneId == ZoneId))
            {
                AdModel.AdZone zone = context.AdZones.Single(c => c.AdZoneId == ZoneId);
                context.Delete(zone);
            }
        }

        public static int AddZone(string ZoneName, string Description, Guid user_id, int ActionTypeId, bool ActiveFg, bool ApplyWeightFg)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdZone zone = new AdModel.AdZone();
            zone.ZoneName = ZoneName;
            zone.ZoneDescription = Description;
            zone.CreatedBy = user_id;
            zone.CreatedDate = DateTime.Now;
            zone.ZoneActionTypeId = ActionTypeId;
            zone.ModifiedBy = null;
            zone.ModifiedDate = null;
            zone.ActiveFg = ActiveFg;
            // If the zone action type doesn't allow to specify the weight apply, it is always false
            zone.ApplyWeightFg = context.AdZoneActionTypes.Single(c=>c.AdZoneActionTypeId == ActionTypeId).ApplyWeightFg? ApplyWeightFg: false;
            context.Add(zone);
            context.SaveChanges();
            return zone.AdZoneId;
        }

        public static void UpdateZone(int ZoneId, string ZoneName, string Description, Guid user_id,int ActionTypeId, bool ActiveFg, bool ApplyWeightFg )
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            AdModel.AdZone zone = context.AdZones.Single(c => c.AdZoneId == ZoneId);
            zone.ZoneName = ZoneName;
            zone.ZoneDescription = Description;
            zone.ZoneActionTypeId = ActionTypeId;
            zone.ModifiedBy = user_id;
            zone.ModifiedDate = DateTime.Now;
            zone.ActiveFg = ActiveFg;
            zone.ApplyWeightFg = ApplyWeightFg;
            context.SaveChanges();
        }




    }
}
