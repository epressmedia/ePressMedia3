using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.UDF
{
    public static class UDFController
    {

        public static IQueryable<UDFModel.UDFInfo> GetAllUDFs()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFInfos.OrderBy(x=>x.UDFID);
        }


        public static UDFModel.UDFInfo GetUDFInfoByUDFID(int UDFId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFInfos.Where(x => x.UDFID == UDFId).SingleOrDefault();
        }

        public static IQueryable<UDFModel.UDFGroup> GetUDFGroupsByUDFID(int UDFId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return (from a in context.UDFAssignments
                    join u in context.UDFGroups on a.UDFGroupId equals u.UDFGroupId
                    where a.UDFId == UDFId
                    select u).AsQueryable();

        }


        public static int AddUDF(int DataTypeId, string UDFName, string UDFDescription, bool ReferenceFg, Guid CreatedBy, string Prefix, string PostFix, string Label)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();

            UDFModel.UDFInfo udfinfo = new UDFModel.UDFInfo();
            udfinfo.DataTypeID = DataTypeId;
            udfinfo.UDFName = UDFName;
            udfinfo.UDFDescription = UDFDescription;
            udfinfo.ReferenceFg = ReferenceFg;
            udfinfo.CreatedBy = CreatedBy;
            udfinfo.CreatedDate = DateTime.Now;
            udfinfo.PrefixLabel = Prefix;
            udfinfo.PostfixLabel = PostFix;
            udfinfo.Label = Label;
            context.Add(udfinfo);
            context.SaveChanges();

            return udfinfo.UDFID;      
        }

        public static void DeleteUDF(int UDFID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            UDFModel.UDFInfo udfinfo = context.UDFInfos.Where(x => x.UDFID == UDFID).SingleOrDefault();
            if (udfinfo != null)
            {
                context.Delete(udfinfo);
                context.SaveChanges();
            }
        }


        public static void UpdateUDF(int UDFID,  string UDFName, string UDFDescription, bool ReferenceFg, Guid ModifiedBy, string Prefix, string PostFix, string Label)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            UDFModel.UDFInfo udfinfo = context.UDFInfos.Where(x => x.UDFID == UDFID).SingleOrDefault();
            if (udfinfo != null)
            {
                udfinfo.UDFName = UDFName;
                udfinfo.UDFDescription = UDFDescription;
                udfinfo.ReferenceFg = ReferenceFg;
                udfinfo.ModifiedBy = ModifiedBy;
                udfinfo.ModifiedDate = DateTime.Now;
                udfinfo.PrefixLabel = Prefix;
                udfinfo.PostfixLabel = PostFix;
                udfinfo.Label = Label;
                context.SaveChanges();
            }
        }

        public static bool IsLineBreak(int UDFId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFInfos.Any(c => c.UDFDataType.DataTypeName == "LineBreak" && c.UDFID == UDFId);
        }

        public static UDFModel.UDFInfo GetUDFByUDFID(int UDFId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFInfos.SingleOrDefault(c => c.UDFID == UDFId);
        }
        public static string GetUDFValue(int UDFId, int ContentId, int ContentTypeId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (context.UDFValues.Any(c => c.UDFID == UDFId && c.SrcID == ContentId && c.ContentTypeID == ContentTypeId))
                return context.UDFValues.SingleOrDefault(c => c.UDFID == UDFId && c.SrcID == ContentId && c.ContentTypeID == ContentTypeId).Value;
            else
                return null;
        }

        public static IQueryable<UDFModel.UDFInfo> GetUDFsByGroupID(int GroupId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return from c in GetUDFAssignmentsByGroupID(GroupId)
                   join a in context.UDFInfos on c.UDFId equals a.UDFID
                   select a;
        }


        public static string GetFieldTypeByUDFId(int UDFId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (context.UDFInfos.Any(c => c.UDFID == UDFId))
                return context.UDFInfos.SingleOrDefault(c => c.UDFID == UDFId).UDFDataType.DataTypeDescr;
            else
                return "None";
        }


        public static IQueryable<UDFModel.UDFAssignment> GetUDFAssignmentsByGroupID(int groupid)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFAssignments.Where(c=>c.UDFGroupId == groupid);
        }

        public static void ProcessUDFs(List<UDFModel.UDFValue> UDFValues)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            foreach (UDFModel.UDFValue value in UDFValues)
            {
                if (context.UDFValues.Any(c => c.UDFID == value.UDFID && c.ContentTypeID == value.ContentTypeID && c.SrcID == value.SrcID))
                {
                    UpdateUDFValue(context, value.UDFID, value.SrcID, value.ContentTypeID, value.Value);
                }
                else
                {
                    InsertUDFValue(context, value.UDFID, value.SrcID, value.ContentTypeID, value.Value);
                }
            }
        }

        private static void UpdateUDFValue(EPM.Data.Model.EPMEntityModel context, int UDFId, int SrcId, int ContentTypeId, string value)
        {
            UDFModel.UDFValue UDF_value = context.UDFValues.Single(c => c.UDFID == UDFId && c.ContentTypeID == ContentTypeId && c.SrcID == SrcId);
            UDF_value.Value = value;
            context.SaveChanges();

        }

        private static void InsertUDFValue(EPM.Data.Model.EPMEntityModel context,int UDFId, int SrcId, int ContentTypeId, string value )
        {
            UDFModel.UDFValue udf_value = new UDFModel.UDFValue();
            udf_value.UDFID = UDFId;
            udf_value.SrcID = SrcId;
            udf_value.Value = value;
            udf_value.ContentTypeID = ContentTypeId;
            context.Add(udf_value);
            context.SaveChanges();
        }

 

        public static int GetNoOfColumns(int UDFGroupID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            int return_value = context.UDFGroups.Single(c => c.UDFGroupId == UDFGroupID).NoOfColumns;
            if (return_value == 0)
                return_value = 1;
            return return_value;
        }

        public static UDFModel.UDFAssignment GetUDFAssignmentInfoByID(int UDFAssignmentID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFAssignments.Where(x => x.UDFAssignmentId == UDFAssignmentID).SingleOrDefault();
        }


        public static void AddUDFAssignment(int UDFGroupId, int UDFId, string DefaultValue, bool Required, bool Active, bool Searchable, int Sequence, Guid AssignedBy)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            UDFModel.UDFAssignment udf = new UDFModel.UDFAssignment();
            udf.UDFGroupId = UDFGroupId;
            udf.UDFId = UDFId;
            udf.DefaultValue = DefaultValue;
            udf.RequiredFg = Required;
            udf.ActiveFg = Active;
            udf.SearchFg = Searchable;
            udf.SequenceNo = Sequence;
            udf.AssignedBy = AssignedBy;
            udf.AssignedDate = DateTime.Now;
            context.Add(udf);
            context.SaveChanges();
        }

        public static void DeleteUDFAssignment(int UDFAssignmentId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            UDFModel.UDFAssignment udf = context.UDFAssignments.Where(x => x.UDFAssignmentId == UDFAssignmentId).SingleOrDefault();
            if (udf != null)
            {
                context.Delete(udf);
                context.SaveChanges();
            }
        }

        public static void UpdateUDFAssignment(int UDFAssignmentId, string DefaultValue, bool Required, bool Active, bool Searchable, int Sequence, Guid Modifiedby)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            UDFModel.UDFAssignment udf = context.UDFAssignments.Where(x => x.UDFAssignmentId == UDFAssignmentId).SingleOrDefault();
            if (udf != null)
            {

                udf.DefaultValue = DefaultValue;
                udf.RequiredFg = Required;
                udf.ActiveFg = Active;
                udf.SearchFg = Searchable;
                udf.SequenceNo = Sequence;
                udf.ModifiedBy = Modifiedby;
                udf.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }
        }


        public static IQueryable<UDFModel.UDFDataType> GetAllDataTypes()
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.UDFDataTypes;
        }




    }
}
