using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Admin
{
    public class CustomPageContoller
    {
        public static IQueryable<SiteModel.CustomPage> GetAllCustomPages()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var category = (from c in context.CustomPages
                            select c);

            return category;
        }

        public static SiteModel.CustomPage GetCustomPageById(int Id)
        {
            return GetAllCustomPages().SingleOrDefault(c => c.CustomPageId == Id);
        }

        public static void UpdateCustomPage(int id, string name, string description, string page_title, string meta_description, bool delete_fg)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            SiteModel.CustomPage page = context.CustomPages.SingleOrDefault(c => c.CustomPageId == id);
            page.DeletedFg = delete_fg;
            page.Description = description;
            page.MetaDescription = meta_description;
            page.Name = name;
            page.PageTitle = page_title;

            context.SaveChanges();

        }

       
    }
}
