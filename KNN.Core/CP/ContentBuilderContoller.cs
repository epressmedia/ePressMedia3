using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace EPM.Core.CP
{
    public class ContentBuilderContoller
    {
        public static void UpdateContentXML(int CategoryID, XmlDocument xmlMaindoc, EPMBasePage.ContentTypes ContentType, EPMBasePage.UseForTypes UseForType)
        {


            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string UpdatedXML = HttpUtility.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));

            if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Article)
            {
                ArticleModel.ArticleCategory artCat = context.ArticleCategories.Single(c => c.ArtCatId == CategoryID);
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    artCat.metadataStr = UpdatedXML;
                else
                    artCat.DetailMetadataStr = UpdatedXML;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Forum)
            {
                ForumModel.ForumConfig forCat = context.ForumConfigs.Single(c => c.ForumId == CategoryID);

                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    forCat.MetadataStr = UpdatedXML;
                else
                    forCat.DetailMetadataStr = UpdatedXML;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Template)
            {
                SiteModel.PageTemplate pageCat = context.PageTemplates.Single(c => c.TemplateId == CategoryID);
                pageCat.MetadataStr = UpdatedXML;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
            {
                SiteModel.MasterPageConfig pageCat = context.MasterPageConfigs.Single(c => c.MasterPageId == CategoryID);
                pageCat.MetadataStr = UpdatedXML;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Page)
            {
                SiteModel.CustomPage pageCat = context.CustomPages.Single(c => c.CustomPageId == CategoryID);
                pageCat.MetadataStr = UpdatedXML;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Classified)
            {

                ClassifiedModel.ClassifiedCategory claCat = context.ClassifiedCategories.Single(c => c.CatId == CategoryID);
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    claCat.MetadataStr = UpdatedXML;
                else
                    claCat.DetailMetadataStr = UpdatedXML;
            }

            context.SaveChanges();
        }

        public static Dictionary<string,string> GetMembersFromControlEnum(System.Reflection.PropertyInfo property)
        {
            Dictionary<string, string> members = new Dictionary<string, string>();
            if (property.PropertyType.IsEnum)
            {
                foreach (System.Reflection.FieldInfo info in property.PropertyType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
                    members.Add(info.Name, info.GetRawConstantValue().ToString());
                return members;
            }
            else
            {
                return null;
            }
        }
    }
}
