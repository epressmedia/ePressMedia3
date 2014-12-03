using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Data.Model;

namespace EPM.Business.Model.Search
{
    public static class SearchController
    {

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<ArticleModel.Article> SearchArticle(string text)
        {
            EPMEntityModel context = new EPMEntityModel();

            var categories = Admin.MainMenuController.GetArticleCategoriesInUse();
            List<ArticleModel.Article> articles = (from c in context.SP_Article_FullText(text)
                                            join a in context.Articles on c.ArticleId equals a.ArticleId
                                                   where a.Suspended == false && categories.Contains(c.CategoryId.ToString())
                                                   && a.IssueDate <= DateTime.Now
                                            select a).OrderByDescending(c=>c.IssueDate).ToList();
            return articles;
        }

        public static List<ForumModel.ForumThread> SearchForum(string text)
        {
            EPMEntityModel context = new EPMEntityModel();

            var categories = Admin.MainMenuController.GetForumCategoriesInUse();
            List<ForumModel.ForumThread> forums = (from c in context.SP_Forum_FullText(text)
                                                   join a in context.ForumThreads on c.ThreadId equals a.ThreadId
                                                   where categories.Contains(c.ForumId.ToString()) 
                                                   && a.Suspended == false // Not deleted
                                                   && a.Private == false // Public only
                                                   select a).OrderByDescending(c=>c.PostDate).ToList();
            return forums;
        }

        public static List<ClassifiedModel.ClassifiedAd> SearchClassified(string text)
        {
            EPMEntityModel context = new EPMEntityModel();

            var categories = Admin.MainMenuController.GetClassifiedCategoriesInUse();
            List<ClassifiedModel.ClassifiedAd> classifiedAds = (from c in context.SP_Classified_FullText(text)
                                                   join a in context.ClassifiedAds on c.AdId equals a.AdId
                                                   where a.Completed == false // Not compelted
                                                   && a.Suspended == false // Not deleted
                                                   && categories.Contains(c.Category.ToString())
                                                   select a).OrderByDescending(c=>c.RegDate).ToList();
            return classifiedAds;
        }

        public static List<BizModel.BusinessEntity> SearchBusinessEntity(string text)
        {
            EPMEntityModel context = new EPMEntityModel();

            return (from c in context.SP_BusinessEntity_FullText(text)
                   select c).ToList();
        }
    }
}
