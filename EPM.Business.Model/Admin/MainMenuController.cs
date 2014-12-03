using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Admin
{
    public static class MainMenuController
    {
        /// <summary>
        /// generate the list of classified category linked to the menu only
        /// </summary>
        /// <param name="ModuleName"></param>
        /// <returns></returns>
        public static List<String> GetCategoriesInUseByModule(string ModuleName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var category = (from c in context.SiteMenus
                            where c.ContentView.ContentType.ContentTypeName == ModuleName && c.Visible == true
                            select c.Param).ToList();

            return category;
        }

        public static List<string> GetArticleCategoriesInUse()
        {
            return GetCategoriesInUseByModule("Article/News");
        }

        public static List<string> GetClassifiedCategoriesInUse()
        {
            return GetCategoriesInUseByModule("Classified");
        }

        public static List<string> GetForumCategoriesInUse()
        {
            return GetCategoriesInUseByModule("Forum");
        }

        public static bool ContentCategoryUsed(string content_name, int category_id)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.SiteMenus.Any(c => c.ContentView.ContentType.ContentTypeName == content_name && c.Param == category_id.ToString());
        }
    }
}
