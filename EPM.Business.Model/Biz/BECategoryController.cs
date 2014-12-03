using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPM.Business.Model.Biz
{
    public static class BECategoryController
    {
        public static int  AddBusinessCategory(string CategoryName, int ParentCategoryId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessCategory category = new BizModel.BusinessCategory();
            category.CategoryName = CategoryName;
            if (ParentCategoryId > 0)
                category.ParentCategoryId = ParentCategoryId;
            context.Add(category);
            context.SaveChanges();
            return category.CategoryId;

        }
        public static int AddBusinessCategory(string CategoryName)
        {
            return AddBusinessCategory(CategoryName, -1);
        }

        public static void UpdateBusinessCategory(int CategoryId, string Name, int ParentId)
        {
            

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessCategory category = (from c in context.BusinessCategories
                                                 where c.CategoryId == CategoryId
                                                      select c).Single();

            category.CategoryName = Name;
            category.ParentCategoryId = ParentId <= 0? (int?)null : ParentId;
            

            context.SaveChanges();
        }

        public static IQueryable<BizModel.BusinessCategory> GetAllBusinessCatgories()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.BusinessCategories
                   orderby c.CategoryName
                   select c;
        }
        public static BizModel.BusinessCategory GetBusinessCatgoryByID(int CatId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessCategories.Any(c => c.CategoryId == CatId))
                return (from c in context.BusinessCategories
                        where c.CategoryId == CatId
                        select c).Single();
            else
                return null;
        }
        public static BizModel.BusinessCategory GetBusinessCatgoryByName(string CatName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessCategories.Any(c => c.CategoryName == CatName && c.ParentCategoryId == null))
                return (from c in context.BusinessCategories
                        where c.CategoryName == CatName && c.ParentCategoryId == null
                        select c).Single();
            else
                return null;
        }
        public static IQueryable<BizModel.BusinessCategory> GetBusinessCatgoryByNames(string CatNames)
        {
            var categories = CatNames.Split(',').ToList();
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessCategories.Any(c => categories.Contains(c.CategoryName) && c.ParentCategoryId == null))
                return (from c in context.BusinessCategories
                        where categories.Contains(c.CategoryName) && c.ParentCategoryId == null
                        select c);
            else
                return null;
        }

        public static IQueryable<BizModel.BusinessCategory> GetBusinessCategoriesByParentID(int parentID)
        {
            return GetAllBusinessCatgories().Where(c => c.ParentCategoryId == parentID);
        }

        public static IQueryable<BizModel.BusinessCategory> GetBusinessCategoriesByParentIDs(int[] parentIDs)
        {
            return GetAllBusinessCatgories().Where(c => parentIDs.Contains((int)c.ParentCategoryId) || parentIDs.Contains((int)c.CategoryId));
        }

        public static void DeleteBusinessCategory(int CategoryId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            if (!ValidateChildExists(CategoryId))
            {
                BizModel.BusinessCategory category = (from c in context.BusinessCategories
                                                      where c.CategoryId == CategoryId
                                                      select c).Single();

                context.Delete(category);

                context.SaveChanges();
            }
        }
        public static bool ValidateChildExists(int CategoryId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.BusinessCategories.Any(c => c.ParentCategoryId == CategoryId);
        }

        public static bool ValidateEntityExists(int CategoryId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.BusinessEntities.Any(c => c.BusienssCategory.CategoryId == CategoryId);
        }


    }
}
