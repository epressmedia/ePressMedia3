using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Business.Model.UDF
{
    public static class UDFAttachmentContoller
    {

        public static IQueryable<UDFModel.UDFAssignment> GetUDFAssignmentsByContentType(int ContentTypeId, int categoryid)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            List<UDFModel.UDFAttachment> attachments = null;
            if (categoryid > 0)
                attachments = context.UDFAttachments.Where(c => c.ContentTypeID == ContentTypeId && c.ContentCategoryID == categoryid).ToList();
            else
                attachments = context.UDFAttachments.Where(c => c.ContentTypeID == ContentTypeId).ToList();
            IQueryable<UDFModel.UDFAssignment> udf_assignment = null;
            foreach (UDFModel.UDFAttachment attachment in attachments)
            {
                var UDFAssignements = EPM.Business.Model.UDF.UDFController.GetUDFAssignmentsByGroupID(attachment.UDFGroupID);
                if (UDFAssignements != null)
                {
                    if (udf_assignment == null)
                    {
                        udf_assignment = UDFAssignements;
                    }
                    else
                    {
                        udf_assignment = udf_assignment.Union(UDFAssignements);
                    }
                }
            }
            return udf_assignment;
        }

        public static void AttachUDFGroup(int ContentTypeId, int CategoryId, int UDFGroupId)
        {
            EPM.Data.Model.EPMEntityModel _context = new Data.Model.EPMEntityModel();
            var udf_attachment = _context.UDFAttachments.Where(x => x.ContentTypeID == ContentTypeId);
            if (CategoryId > 0)
                udf_attachment = udf_attachment.Where(x => x.ContentCategoryID == CategoryId);

           
            if (udf_attachment.Count() > 0) // then update
            {

                if (UDFGroupId == 0) // delete
                {
                    _context.Delete(udf_attachment);
                    _context.SaveChanges();
                }
                else
                {

                    var attachment = udf_attachment.Single();
                    if (CategoryId > 0)
                        attachment.ContentCategoryID = CategoryId;
                    attachment.ContentTypeID = ContentTypeId;
                    attachment.UDFGroupID = UDFGroupId;
                    _context.SaveChanges();
                }
            }
            else // Add
            {
                UDFModel.UDFAttachment attachment = new UDFModel.UDFAttachment();
                attachment.ContentTypeID = ContentTypeId;
                if (CategoryId > 0)
                    attachment.ContentCategoryID = CategoryId;
                attachment.UDFGroupID = UDFGroupId;
                _context.Add(attachment);
                _context.SaveChanges();
            }
       
        }



      
    }
}
