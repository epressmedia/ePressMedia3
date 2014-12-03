using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPM.Data.Model;

namespace EPM.Business.Model.UDF
{
    public static class UDFGroupController
    {
        

        public static IQueryable<UDFModel.UDFGroup> GetAllUDFGroups()
        {
            EPMEntityModel context = new EPMEntityModel();
            return context.UDFGroups.OrderBy(x => x.UDFGroupName);
        }


        public static UDFModel.UDFGroup GetUDFGroupByID(int UDFGroupId)
        {
            EPMEntityModel context = new EPMEntityModel();
            return context.UDFGroups.Where(x => x.UDFGroupId == UDFGroupId).SingleOrDefault();
        }
        public static int AddUDFGroup(string GroupDescription, string GroupName, Guid CreatedBy, int NoOfColums)
        {

            EPMEntityModel context = new EPMEntityModel();
            UDFModel.UDFGroup group = new UDFModel.UDFGroup();
            group.NoOfColumns = NoOfColums;
            group.UDFGroupDescription = GroupDescription;
            group.UDFGroupName = GroupName;
            group.CreatedBy = CreatedBy;
            group.CreatedDate = DateTime.Now;
            context.Add(group);
            context.SaveChanges();
            return group.UDFGroupId;
        }

        public static void UpdateUDFGroup(int UDFGroupId, string GroupName, string GroupDescription, Guid ModifiedBy, int NoOfColums)
        {

            EPMEntityModel context = new EPMEntityModel();
            UDFModel.UDFGroup group = context.UDFGroups.Where(x => x.UDFGroupId == UDFGroupId).SingleOrDefault();
            if (group != null)
            {
                group.NoOfColumns = NoOfColums;
                group.UDFGroupDescription = GroupDescription;
                group.UDFGroupName = GroupName;
                group.ModifiedBy = ModifiedBy  ;
                group.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }
        }

        public static void UpdateUDFGroup(int UDFGroupId,string GroupDescription, Guid ModifiedBy, int NoOfColums)
        {

            EPMEntityModel context = new EPMEntityModel();
            UDFModel.UDFGroup group = context.UDFGroups.Where(x => x.UDFGroupId == UDFGroupId).SingleOrDefault();
            if (group != null)
            {
                group.NoOfColumns = NoOfColums;
                group.UDFGroupDescription = GroupDescription;
                group.ModifiedBy = ModifiedBy;
                group.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }
        }

        public static void DeleteUDFGroup(int UDFGroupId)
        {
            EPMEntityModel context = new EPMEntityModel();
            UDFModel.UDFGroup group = context.UDFGroups.Where(x => x.UDFGroupId == UDFGroupId).SingleOrDefault();
            if (group != null)
            {
                context.Delete(group);
                context.SaveChanges();
            }
        }

        public static IQueryable<UDFModel.UDFGroup> GetUDFGroupsByContentType(int ContentTypeId, int CategoryId)
        {
            EPMEntityModel _context = new EPMEntityModel();

            if (CategoryId > 0)
            return (from x in _context.ContentTypes
                    join c in _context.UDFAttachments on x.ContentTypeId equals c.ContentTypeID
                    join g in _context.UDFGroups on c.UDFGroupID equals g.UDFGroupId
                    where c.ContentTypeID == ContentTypeId &&
                   c.ContentCategoryID == CategoryId
                    select g).AsQueryable();
            else
                return (from x in _context.ContentTypes
                        join c in _context.UDFAttachments on x.ContentTypeId equals c.ContentTypeID
                        join g in _context.UDFGroups on c.UDFGroupID equals g.UDFGroupId
                        where c.ContentTypeID == ContentTypeId 
                        select g).AsQueryable();
                    
        }

        public static IQueryable<UDFModel.UDFInfo> GetUDFsNotInGroup(int GroupID)
        {
            EPMEntityModel _context = new EPMEntityModel();

            var UDFMembers = (from x in _context.UDFAssignments
                              //join i in _context.udf
                              where x.UDFGroupId == GroupID
                              select x.UDFInfo);
            return _context.UDFInfos.Where(x => !UDFMembers.Contains(x)).AsQueryable();
        }

        public static IQueryable<UDFModel.UDFGroup> GetUDFGroupsNotInUseByContractType(int ContentTypeId, int CategoryId)
        {
            var UDFAssignmentInUse = (from c in GetUDFGroupsByContentType(ContentTypeId, CategoryId)
                                         select c.UDFGroupId).ToList();
            return from c in GetAllUDFGroups()
                   where !UDFAssignmentInUse.Contains(c.UDFGroupId)
                   select c;
        }


    }
}
