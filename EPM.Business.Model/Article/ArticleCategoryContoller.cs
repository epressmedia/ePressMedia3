using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Legacy.Security;

namespace EPM.Business.Model.Article
{
    public static class ArticleCategoryContoller
    {

        public static List<ArticleModel.ArticleCategory> ActicleCategoriesWithPermission(Permission permission)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            // The move to article cat list Needs to be filetered by the access.
            List<ArticleModel.ArticleCategory> ArtCats = GetAllActiveCategories().ToList();

            // Loop through the article category to see if the user has write access to the category.
            // for each cannot be used because collection cannot be modified while it is being looped within for each. 
            for (int i = ArtCats.Count - 1; i >= 0; i--)
            {
                if (!AccessControl.AuthorizeUser(System.Web.HttpContext.Current.User.Identity.Name,
              ResourceType.Article, ArtCats[i].ArtCatId, permission))
                {
                    ArtCats.RemoveAt(i);
                }
            }

            return ArtCats;
        }

        public static IQueryable<ArticleModel.ArticleCategory> GetAllActiveCategories()
        {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                return (from c in context.ArticleCategories
                        where c.Deleted_fg == false
                        select c);
        }

        public static int GetArticleCatIdByURLSlug(string UrlSlug)
        {
                 EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                return context.ArticleCategories.Single(c=>c.UrlSlug == UrlSlug).ArtCatId;

            
        }

        public static ArticleModel.ArticleCategory GetArticleCategoryByArticleId(int ArticleId)
        {

            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.Articles.Where(x => x.ArticleId == ArticleId).SingleOrDefault().ArticleCategory1;
        }
    }
}
