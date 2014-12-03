using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EPM.Data.Model;
using Telerik.OpenAccess;


namespace EPM.Business.Model.Biz
{
    public static class BEController
    {
        public static IQueryable<BizModel.BusinessEntity> GetALLActiveBEs()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.BusinessEntities
                   where c.DeletedFg == false
                   select c;
        }

        public static IQueryable<BizModel.BusinessEntity> GetMostViewBEsInDays(int Days)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            //return from x in context.BusinessEntityViewHistories
            //       where x.ViewDate >= DateTime.Now.AddDays(Days * -1)
            //       select x.BusinessEntity;

            //IQueryable<BizModel.BusinessEntity> BEs = GetALLActiveBEs();
            //IQueryable<BizModel.BusinessEntityViewHistory> viewCount = context.BusinessEntityViewHistories.Where(x => x.ViewDate >= DateTime.Now.AddDays(Days * -1));
            return from x in context.BusinessEntities
                   where x.DeletedFg == false && x.ApprovedFg == true
                   orderby x.BusinessEntityViewHistories.Where(c=>c.ViewDate  >= DateTime.Now.AddDays(Days * -1)).Count() descending
                   select x;

        }


        public static IQueryable<BizModel.BusinessEntity> GetALLBEsByCatID(int CatID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.BusinessEntities
                   where c.BusienssCategory.CategoryId == CatID
                   select c;
        }
        public static IQueryable<BizModel.BusinessEntity> GetActiveBEsByCatID(int CatID)
        {
            return GetALLBEsByCatID(CatID).Where(c => c.ApprovedFg == true && c.DeletedFg == false);
        }
        public static BizModel.BusinessEntity GetBEbyBEID(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntities.Any(c => c.BusinessEntityId == BEID && c.DeletedFg == false ))
                return context.BusinessEntities.Single(c => c.BusinessEntityId == BEID && c.DeletedFg == false );
            else
                return null;
        }
        public static void UpdateBE(int BEID,string PrimaryName, string SecondaryName, int CatId,  Guid modified_by, string address, string city, 
            string state, string zipcode, string videoURL, string password, string shortDesc, string Description, string phone1, string phone2, string fax, string emailaddress, bool adowner, string website, bool premium)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntities.Any(c => c.BusinessEntityId == BEID && c.DeletedFg == false))
            {
                BizModel.BusinessEntity be = context.BusinessEntities.Single(c=>c.BusinessEntityId == BEID);
                be.PrimaryName = PrimaryName;
                be.SecondaryName = SecondaryName;
                be.CategoryID = CatId;
                //be.ModifiedDate = DateTime.Now;
                //be.ModifiedBy = modified_by;
                be.Address = address;
                be.City = city;
                be.State = state;
                be.ZipCode = zipcode;
                be.VideoURL = videoURL;
                be.Password = password;
                be.ShortDesc = shortDesc;
                be.Description = Description;
                be.Phone1 = phone1;
                be.Phone2 = phone2;
                be.Fax = fax;
                be.Email = emailaddress;
                be.AdOwner = adowner;
                be.Website = website;
                be.PremiumListing = premium;

                context.SaveChanges();
            }
        }
        public static void UpdateBECategory(int BEID, int NewCategoryId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntities.Any(c => c.BusinessEntityId == BEID && c.DeletedFg == false && c.ApprovedFg == true))
            {
                BizModel.BusinessEntity be = context.BusinessEntities.Single(c => c.BusinessEntityId == BEID);
                be.CategoryID = NewCategoryId;

                context.SaveChanges();
            }
        }

        public static void DeleteBE(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntities.Any(c => c.BusinessEntityId == BEID))
            {
                BizModel.BusinessEntity be = context.BusinessEntities.Single(c => c.BusinessEntityId == BEID);
                be.DeletedFg = true;
                context.SaveChanges();
            }
        }
        public static void ApproveBE(int BEID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntities.Any(c => c.BusinessEntityId == BEID))
            {
                BizModel.BusinessEntity be = context.BusinessEntities.Single(c => c.BusinessEntityId == BEID);
                be.ApprovedFg = true;
                context.SaveChanges();
            }
        }
        public static void UpdatePassword(int BEID, string Password)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntities.Any(c => c.BusinessEntityId == BEID))
            {
                BizModel.BusinessEntity be = context.BusinessEntities.Single(c => c.BusinessEntityId == BEID);
                be.Password = Password;
                context.SaveChanges();
            }
        }

        public static int AddBE(string PrimaryName, string SecondaryName, int CatId, Guid created_by, string address, string city,
            string state, string zipcode, string videoURL, string password, string shortDesc, string Description, string phone1, string phone2, string fax, string emailaddress, bool adowner, string createdbyname, string website, bool premium)
        {
            try
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                BizModel.BusinessEntity be = new BizModel.BusinessEntity();
                be.PrimaryName = PrimaryName;
                be.SecondaryName = SecondaryName;
                be.CategoryID = CatId;
                be.CreatedDate = DateTime.Now;
                be.CreatedBy = created_by;
                be.ModifiedDate = DateTime.Now;
                be.Address = address;
                be.City = city;
                be.State = state;
                be.ZipCode = zipcode;
                be.VideoURL = videoURL;
                be.Password = password;
                be.ShortDesc = shortDesc;
                be.Description = Description;
                be.Phone1 = phone1;
                be.Phone2 = phone2;
                be.Fax = fax;
                be.Email = emailaddress;
                be.ApprovedFg = false;
                be.DeletedFg = false;
                be.AdOwner = adowner;
                be.CreatedByName = createdbyname;
                be.Website = website;
                be.PremiumListing = premium;


                context.Add(be);
                context.SaveChanges();

                return be.BusinessEntityId;
            }
            catch
            {
                return -1;
            }
        }
        public static bool ValidatePassword(int BusinessEntityID, string password)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.BusinessEntities.Any(c => c.BusinessEntityId == BusinessEntityID && c.Password == password);
        }

        public static int GetBusinessEntityViewCounter(int BusinessEntityID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.BusinessEntityViewHistories.Count(c => c.BusinessEntityID == BusinessEntityID);
        }

        public static void AddBusinessEntityViewHistory(int BusinessEntityID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessEntityViewHistory history = new BizModel.BusinessEntityViewHistory();
            history.BusinessEntityID = BusinessEntityID;
            history.ViewDate = DateTime.Now;
            context.Add(history);
            context.SaveChanges();
        }

        public static IQueryable<BizModel.BusinessEntity> SearchBiz(string keyword)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string fulltextCondition = "\"*" + keyword + "*\"";
            return  from x in context.BusinessEntities where "CONTAINS(({0},{1},{2},{3}),{4})".SQL<bool>(x.PrimaryName,x.SecondaryName,x.Phone1,x.Phone2, fulltextCondition) select x;
            
        }
    }

    
}
