using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Classified
{
    public class ClassifiedController
    {
        public static IQueryable<ClassifiedModel.ClassifiedAd> GetClassifiedAdsByCategoryId(int CatId, bool ShowAnnounce = false)
        {
            // default maxRecords is set to 2000
            int maxRecord = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Max Number of Ads Per Classified Category"));
            return GetClassifiedAdsByCategoryId(CatId, maxRecord, ShowAnnounce);
        }
        public static IQueryable<ClassifiedModel.ClassifiedAd> GetClassifiedAdsByCategoryId(int CatId, int maxRecord, bool ShowAnnounce = false)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            IQueryable<ClassifiedModel.ClassifiedAd> result = (from c in context.ClassifiedAds
                          where c.ClassifiedCategory.CatId == CatId && c.Suspended == false
                          orderby c.RegDate descending
                          select c);
            if (!ShowAnnounce)
                result = result.Where(c => c.Announce == ShowAnnounce);

            if (maxRecord > 0)
                return result.Take(maxRecord);
            else
                return result;

        }
        public static IQueryable<ClassifiedModel.ClassifiedAd> GetClassifiedAdsByCategoryIds(string ContentIDs, bool ShowAnnounce = false, int maxRecord = 0)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var CatIds = ContentIDs.Split(',').ToList();

            IQueryable<ClassifiedModel.ClassifiedAd> result = (from c in context.ClassifiedAds
                          where CatIds.Contains(c.Category.ToString()) && c.Suspended == false
                          orderby c.RegDate descending
                          select c);
            if (!ShowAnnounce)
                result = result.Where(c => c.Announce == ShowAnnounce);

            if (maxRecord > 0)
                return result.Take(maxRecord);
            else
                return result;

        }

        public static ClassifiedModel.ClassifiedCategory GetClassifiedCategoryInfoById(int CategoryId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.ClassifiedCategories.Single(c => c.CatId == CategoryId);
        }

        public static ClassifiedModel.ClassifiedCategory GetClassifiedCategoryByAdId(int AdId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.ClassifiedAds.Where(c => c.AdId == AdId).SingleOrDefault().ClassifiedCategory;
        } 

        public static void AddClassifiedImage(int AdID, string ImageName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ClassifiedModel.ClassifiedImage adImage = new ClassifiedModel.ClassifiedImage();
            adImage.AdId = AdID;
            adImage.FileName = ImageName;
            context.Add(adImage);
            context.SaveChanges();
        }
        public static void UpdateClassifiedThumbnail(int AdID, string ThumbnailName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            if (context.ClassifiedAds.Any(c => c.AdId == AdID && c.Suspended == false))
            {
                ClassifiedModel.ClassifiedAd ad = context.ClassifiedAds.SingleOrDefault(c => c.AdId == AdID);
                if (string.IsNullOrEmpty(ad.Thumb))
                {
                    ad.Thumb = ThumbnailName;
                    context.SaveChanges();
                    
                }
            }
            

        }
        public static void RemoveClassifiedThumbnail(int AdID)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            if (context.ClassifiedAds.Any(c => c.AdId == AdID))
            {
                ClassifiedModel.ClassifiedAd ad = context.ClassifiedAds.SingleOrDefault(c => c.AdId == AdID);
                ad.Thumb = "";
                context.SaveChanges();
            }
        }

        public static ClassifiedModel.ClassifiedAd GetClassifiedAdByAdId(int AdId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.ClassifiedAds.Any(c => c.AdId == AdId && c.Suspended == false))
            {
                return context.ClassifiedAds.SingleOrDefault(c => c.AdId == AdId);
            }
            else
            {
                context.ClearChanges();
                return null;
                
            }
        }

        public static bool ValidatePassword(int AdId, string password)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return (context.ClassifiedAds.Any(c => c.AdId == AdId && c.Password == password));
        }

        public static int AddClassifiedAd(string Subject, bool Announce, int CategoryID, string Descirption, string Phone, string Email, string Password,
        string IPAddress, string PostedBy, Boolean Premium, String UserName )
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            try
            {
                ClassifiedModel.ClassifiedAd ad = new ClassifiedModel.ClassifiedAd();
                // tag will be added only when an option is selected
                ad.Subject = Subject;
                ad.RegDate = DateTime.Now;
                ad.Announce = Announce;
                ad.Category = CategoryID;
                ad.Description = Descirption;
                ad.Phone = Phone;
                ad.Email = Email;
                ad.Password = Password;
                ad.IpAddr = IPAddress;
                ad.PostBy = PostedBy;
                ad.Suspended = false;
                ad.LastUpdate = DateTime.Now;
                ad.Premium = Premium;
                ad.UserName = UserName;
                ad.Completed = false;

                context.Add(ad);
                context.SaveChanges();
                return ad.AdId;
            }
            catch
            {
                return -1;
            }
        }

        public static void UpdateClassifiedAd(int AdID, String Subject, Boolean Announce, String Description, String Phone, String Email, String IPAddress)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ClassifiedModel.ClassifiedAd ad = context.ClassifiedAds.Single(c => c.AdId == AdID);
            ad.Subject = Subject;
            ad.LastUpdate = DateTime.Now;
            ad.Announce = Announce;
            ad.Description = Description;
            ad.Phone = Phone;
            ad.Email = Email;
            ad.IpAddr = IPAddress;
            context.SaveChanges();
        }
    }
}
