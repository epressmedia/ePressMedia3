using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Classified
{
    public class ClassifiedTagController
    {

        public static IQueryable<ClassifiedModel.ClassifiedTag> GetTagsByCategoryId(int CategoryId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.ClassifiedTags.Where(c => c.ClassifiedCatId == CategoryId);
        }

        public static void AddTag(int CategoryId, string TagName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ClassifiedModel.ClassifiedTag tag = new ClassifiedModel.ClassifiedTag();
            tag.ClassifiedCatId = CategoryId;
            tag.TagName = TagName;
            context.Add(tag);
            context.SaveChanges();
        }
        public static void DeleteTag(int TagID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ClassifiedModel.ClassifiedTag tag =  context.ClassifiedTags.Single(c => c.TagId == TagID);
            context.Delete(tag);
            context.SaveChanges();
        }
    }
}
