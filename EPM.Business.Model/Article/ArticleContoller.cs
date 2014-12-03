using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Business.Model.Admin;
using Telerik.OpenAccess;

namespace EPM.Business.Model.Article
{
    public static class ArticleContoller
    {

        

        public static IQueryable<ArticleModel.Article> GetArticlesByCatIDs(string CatIDs, bool ThumbnailOnlyArticle = false)
        {
            return GetArticlesByCatIDs(CatIDs, int.Parse(SiteSettingController.GetSiteSettingValueByName("Max Number of Articles Per Category")), false, ThumbnailOnlyArticle);
        }

        public static IQueryable<ArticleModel.Article> GetArticlesByCatIDs(string CatIDs, bool Admin, bool ThumbnailOnlyArticle = false)
        {
            return GetArticlesByCatIDs(CatIDs, int.Parse(SiteSettingController.GetSiteSettingValueByName("Max Number of Articles Per Category")), Admin, ThumbnailOnlyArticle);
        }

        public static IQueryable<ArticleModel.Article> GetArticlesByCatIDs(string CatIDs, int NumberOfItems, bool ThumbnailOnlyArticle = false)
        {
            return GetArticlesByCatIDs(CatIDs, NumberOfItems, false, ThumbnailOnlyArticle);
        }
        public static ArticleModel.Article GetArticleIDByURLSlug(string Url_Slug)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.Articles.SingleOrDefault(c => c.UrlSlug == Url_Slug && c.Suspended == false);
        }

        public static void IncreaseCounter(int ArticleID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.Articles.Any(c => c.ArticleId == ArticleID && c.Suspended == false))
            {
                ArticleModel.Article art = context.Articles.Single(c => c.ArticleId == ArticleID && c.Suspended == false);
                art.ViewCount = art.ViewCount + 1;
                context.SaveChanges();
            }
        }

        public static ArticleModel.Article GetArticleByArticleID(int ArticleID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.Articles.Any(c => c.ArticleId == ArticleID && c.Suspended == false))
                return context.Articles.Single(c => c.ArticleId == ArticleID && c.Suspended == false);
            else
                return null;
        }

        public static IQueryable<ArticleModel.Article> GetArticleByCatIDsOrderByViewInDays(string CatIDs, int NumberOfItems, DateTime targetdate)
        {
            var CatIds = CatIDs.Split(',').ToList();

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var query1 = (from c in context.Articles
                          where CatIds.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate >= targetdate
                          orderby c.ViewCount descending
                          select c).Take(NumberOfItems);
            var query2 = (from a in context.Articles
                          join vc in context.VirtualCategoryLinks on a.ArticleId equals vc.ArticleId
                          where CatIds.Contains(vc.CatId.ToString()) && a.Suspended == false && a.IssueDate >= targetdate
                          orderby a.ViewCount descending
                          select a).Take(NumberOfItems);
            //
            return (from n in query1
                    select n).Union(from n2 in query2 select n2).OrderByDescending(c => c.ViewCount).Take(NumberOfItems);

            
        }

        public static IQueryable<ArticleModel.Article> GetArticleByCatIDsOrderByPostDate(string CatIDs, int NumberOfItems)
        {

            var CatIds = CatIDs.Split(',').ToList();
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var query1 = (from c in context.Articles
                          where CatIds.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.IssueDate < DateTime.Now
                          orderby c.IssueDate descending
                          select c).Take(NumberOfItems);

            
            

            var query2 = (from a in context.Articles
                          join vc in context.VirtualCategoryLinks on a.ArticleId equals vc.ArticleId
                          where CatIds.Contains(vc.CatId.ToString()) && a.Suspended == false && a.IssueDate < DateTime.Now
                          orderby a.IssueDate descending
                          select a).Take(NumberOfItems);

            return (from n in query1
                    select n).Union(from n2 in query2 select n2).OrderByDescending(c => c.IssueDate).Take(NumberOfItems);

        }

    
        public static IQueryable<ArticleModel.Article> GetArticlesByCatIDs(string CatIDs, int NumberOfItems, bool Admin, bool ThumbnailOnlyArticle = false)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var CatId = CatIDs.Split(',').ToList();
            IQueryable<ArticleModel.Article> articleQuery;

            IQueryable<ArticleModel.Article> query1 = (from c in context.Articles
                                                       where CatId.Contains(c.CategoryId.ToString()) && c.Suspended == false
                                                       orderby c.IssueDate descending
                                                       select c);


            IQueryable<ArticleModel.Article> query2 = (from a in context.Articles
                                                       join vc in context.VirtualCategoryLinks on a.ArticleId equals vc.ArticleId
                                                       where CatId.Contains(vc.CatId.ToString()) //&& a.Suspended == false
                                                       orderby a.IssueDate descending
                                                       select a);
           


            if (ThumbnailOnlyArticle)
            {
                query1 = query1.Where(c => !string.IsNullOrEmpty(c.Thumb_Path));
                query2 = query2.Where(c => !string.IsNullOrEmpty(c.Thumb_Path));
            }


            if (!Admin)
            {
                query1 = query1.Where(c => c.IssueDate < DateTime.Now);
                query2 = query2.Where(c => c.IssueDate < DateTime.Now);
            }
            // if NumberOfItems = 0, return all records
            if (NumberOfItems > 0)
            {
                query1 = query1.Take(NumberOfItems);
                query2 = query2.Take(NumberOfItems);
            }
               articleQuery = (from n in query1
                                select n).Union(from n2 in query2 select n2).OrderByDescending(c => c.IssueDate);

            if (NumberOfItems > 0)
            {
                articleQuery =articleQuery.Take(NumberOfItems);
            }



            return articleQuery;
        }

        public static void DeleteArticle(int ArticleId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ArticleModel.Article article = context.Articles.Single(s => s.ArticleId == ArticleId);
            article.Suspended = true;
            context.SaveChanges();
        }

        


    }
}
