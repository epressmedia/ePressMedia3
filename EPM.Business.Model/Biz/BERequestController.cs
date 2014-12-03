using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Biz
{
    public static class BERequestController
    {
        public static void AddBusinessEntityRequest(int BusinessEntityID, string FieldName, string oldValue, string newvalue)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessEntityRequest request = new BizModel.BusinessEntityRequest();
            request.BusinessEntityId = BusinessEntityID;
            request.RequestDate = DateTime.Now;
            request.FieldName = FieldName;
            request.OldValue = oldValue;
            request.NewValue = newvalue;
            request.Status = 'N';

            context.Add(request);
            context.SaveChanges();
            
        }

        public static void ResponseAllFieldsByRequestID(int BEID, Guid reviewed_by, ResponseTypes responseType)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

                List<BizModel.BusinessEntityRequest> requests = context.BusinessEntityRequests.Where(c => c.BusinessEntityId == BEID && c.Status == 'N' && c.ReviewedBy == null ).ToList();
                foreach(BizModel.BusinessEntityRequest request in requests)
                {
                    ReviewSingleField(request.RequestId,reviewed_by, responseType);
                }
        }


        public enum ResponseTypes
        {
            Accept,
            Reject
        }

        public static void ReviewSingleField(int requestID,Guid reviewed_by, ResponseTypes responseType)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntityRequests.Any(c=>c.RequestId == requestID))
            {
                char responseCode = 'R';
                switch (responseType)
                {
                    case ResponseTypes.Accept:
                        responseCode = 'A';
                        break;
                    case ResponseTypes.Reject:
                        responseCode = 'R';
                        break;
                    default:
                        responseCode = 'R';
                        break;
                }

                BizModel.BusinessEntityRequest request = context.BusinessEntityRequests.Single(c => c.RequestId == requestID);
                // check if the same field and same BEID request exists. If so, cancel the previous requests
                if(context.BusinessEntityRequests.Any(c=> c.RequestId != requestID && c.FieldName == request.FieldName && c.BusinessEntityId == request.BusinessEntityId))
                {
                    foreach (BizModel.BusinessEntityRequest oldrequest in context.BusinessEntityRequests.Where(c => c.RequestId != requestID && c.FieldName == request.FieldName && c.BusinessEntityId == request.BusinessEntityId).ToList())
                    {
                        oldrequest.ReviewDate = DateTime.Now;
                        oldrequest.ReviewedBy = reviewed_by;
                        oldrequest.Status = 'R';
                        context.SaveChanges();
                    }
                }
                try
                {
                    ProcessFieldUpdate(request);
                    request.ReviewDate = DateTime.Now;
                    request.ReviewedBy = reviewed_by;
                    request.Status = responseCode;
                    
                }
                catch(Exception ex)
                {
                    request.ReviewDate = DateTime.Now;
                    request.ReviewedBy = reviewed_by;
                    request.Status = 'E';
                    request.Error=ex.Message;
                }
                context.SaveChanges();
            }
        }


        public static IQueryable<BizModel.BusinessEntityRequest> GetAllBusinessEntityRequests()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.BusinessEntityRequests
                   orderby c.RequestDate descending
                   select c;
        }

        public static IQueryable<BizModel.BusinessEntityRequest> GetPendingBusinessEntityRequestsByBEID(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.BusinessEntityRequests
                   where c.BusinessEntityId == BEID && c.ReviewDate == null && c.Status == 'N'
                   orderby c.RequestDate descending
                   select c;
        }


        private static void ProcessFieldUpdate(BizModel.BusinessEntityRequest request)
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessEntity BE = context.BusinessEntities.Single(c => c.BusinessEntityId == request.BusinessEntityId);


            switch (request.FieldName)
            {
                case "PrimaryName":
                    BE.PrimaryName = request.NewValue;
                    break;
                case "SecondaryName":
                    BE.SecondaryName = request.NewValue;
                    break;
                case "CategoryID":
                    BE.CategoryID = int.Parse(request.NewValue);
                    break;
                case "Address":
                    BE.Address = request.NewValue;
                    break;
                case "City":
                    BE.City = request.NewValue;
                    break;
                case "State":
                    BE.State = request.NewValue;
                    break;
                case "ZipCode":
                    BE.ZipCode = request.NewValue;
                    break;
                case "VideoURL":
                    BE.VideoURL = request.NewValue;
                    break;
                case "ShortDesc":
                    BE.ShortDesc = request.NewValue;
                    break;
                case "Description":
                    BE.Description = request.NewValue;
                    break;
                case "Website":
                    BE.Website = request.NewValue;
                    break;
                case "Phone1":
                    BE.Phone1 = request.NewValue;
                    break;
                case "Phone2":
                    BE.Phone2 = request.NewValue;
                    break;
                case "Fax":
                    BE.Fax = request.NewValue;
                    break;
                case "Email":
                    BE.Email = request.NewValue;
                    break;

            }

            context.SaveChanges();
            

        }
    }
}
