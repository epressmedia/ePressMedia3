using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.UDF
{
    public static class UDFReferenceController
    {
        public static IQueryable<UDFModel.UDFReference> GetAllUDFReferences()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFReferences.OrderBy(x => x.ReferenceID);
        }
        public static void AddUDFReference(string DisplayValue, string InternalValue, int UDFID, DateTime? EffectiveDate, DateTime? TerminateDate)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            UDFModel.UDFReference udf = new UDFModel.UDFReference();
            udf.DisplayValue = DisplayValue;
            udf.InternalValue = InternalValue;
            udf.UDFID = UDFID;
            if (EffectiveDate.HasValue)
                udf.EffrectiveDate = EffectiveDate.Value;
            else
                udf.EffrectiveDate = DateTime.Now;
            if (TerminateDate.HasValue)
                udf.TerminateDate = TerminateDate.Value;
            context.Add(udf);
            context.SaveChanges();
        }

        public static void UpdateUDFReference(int ReferenceID, string DisplayValue, string InternalValue, DateTime? EffectiveDate, DateTime? TerminateDate)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            UDFModel.UDFReference udf = context.UDFReferences.Where(x => x.ReferenceID == ReferenceID).SingleOrDefault();
            if (udf != null)
            {
                udf.DisplayValue = DisplayValue;
                udf.InternalValue = InternalValue;
                udf.EffrectiveDate = EffectiveDate.Value;
                if (TerminateDate.HasValue)
                    udf.TerminateDate = TerminateDate.Value;
                else
                    udf.TerminateDate = null;
                context.SaveChanges();
            }
        }

        public static IQueryable<UDFModel.UDFReference> GetAllReferncesByUDFID(int UDFID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFReferences.Where(c => c.UDFID == UDFID && c.EffrectiveDate <= DateTime.Now && (c.TerminateDate == null || c.TerminateDate > DateTime.Now)).OrderBy(c=>c.DisplayValue);
        }

        public static string GetDisplayValue(int UDFID, string InternalValue)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (context.UDFReferences.Any(c => c.UDFID == UDFID && c.InternalValue == InternalValue))
                return context.UDFReferences.Where(c => c.UDFID == UDFID && c.InternalValue == InternalValue).ToList()[0].DisplayValue;
            else
                return "";
        }

    }
}
