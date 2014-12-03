using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EPM.Business.Model.Admin
{
    public static class MasterPageContoller
    {
        public static void DeleteMasterPage(int CatId)
        {
               EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
               SiteModel.MasterPageConfig page = context.MasterPageConfigs.Single(c => c.MasterPageId == CatId);
               page.DeletedFg = true;
               context.SaveChanges();
        }

        public static Boolean CheckBeingUsed(string master_path, string master_name)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            int count = 0;

            //check in Article.
            // Listing
            List<ArticleModel.ArticleCategory> artcats = context.ArticleCategories.Where(c => c.Deleted_fg == false  && c.VirtualCat_fg == false && c.LinkArticle_fg == false).ToList();
            foreach (ArticleModel.ArticleCategory artcat in artcats)
            {
                count = count+ InspectXML(artcat.metadataStr, master_path, master_name);
                count = count + InspectXML(artcat.DetailMetadataStr, master_path, master_name);
            }
            // check custom page
            List<SiteModel.CustomPage> pages = context.CustomPages.Where(c => c.DeletedFg == false).ToList();
            foreach (SiteModel.CustomPage page in pages)
            {
                count = count + InspectXML(page.MetadataStr, master_path, master_name);
            }


            // check forum
            List<ForumModel.ForumConfig> forums = context.ForumConfigs.Where(c => 1 == 1).ToList();
            foreach (ForumModel.ForumConfig forum in forums)
            {
                count = count + InspectXML(forum.MetadataStr, master_path, master_name);
                count = count + InspectXML(forum.DetailMetadataStr, master_path, master_name);
            }

            // check forum
            List<ClassifiedModel.ClassifiedCategory> cls = context.ClassifiedCategories.Where(c => 1==1).ToList();
            foreach (ClassifiedModel.ClassifiedCategory cl in cls)
            {
                count = count + InspectXML(cl.MetadataStr, master_path, master_name);
                count = count + InspectXML(cl.DetailMetadataStr, master_path, master_name);
            }
            


            if (count > 0)
                return true;
            else
                return false;
        }
        private static int InspectXML(string xml_string, string path, string name)
        {
            if (!string.IsNullOrEmpty(xml_string))
            {
                int count = 0;
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xml_string);

                //Get Master Page setting and set the master page
                XmlNode node = xml.SelectSingleNode("/PageRoot/Configs/MasterPage");

                string s_name = "";
                if (node.Attributes["Name"] != null)
                    s_name = node.Attributes["Name"].Value.ToString();
                else
                {
                    // get the very first master page name
                    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                    s_name = (context.MasterPageConfigs.Where(c => c.DeletedFg == false).OrderBy(c => c.MasterPageId).ToList())[0].Description;
                }

                if ((node.Attributes["Value"].Value.ToString() == path) && (s_name == name))
                {
                    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                    count++;
                }
                return count;
            }
            else
                return 0;

        }

    }

    
}
