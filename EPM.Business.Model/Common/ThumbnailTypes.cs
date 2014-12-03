using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Common
{
    public static class ThumbnailTypes
    {


         public static List<string> GetThumbnailTypes()
        {
             List<String> thumbTypes = new List<string>();
             EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
             List<SiteModel.ThumbnailType> types = (from c in context.ThumbnailTypes
                                                   select c).ToList();
             foreach(SiteModel.ThumbnailType type in types)
             {
                 thumbTypes.Add(type.ThumbnailTypeName);
             }
             return thumbTypes;
        }


         public static string GetDefaultThumbnailTypeString()
         {
             
             EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
             string thumbType = ((from c in context.ThumbnailTypes
                                  where c.DefaultFg == true
                                  select c).Single()).ThumbnailTypeName;
             return thumbType;
         }

         public static SiteModel.ThumbnailType GetDefaultThumbnailTypeObject()
         {

             EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
             SiteModel.ThumbnailType thumbType = ((from c in context.ThumbnailTypes
                                                   where c.DefaultFg == true
                                                   select c).Single());

             return thumbType;
         }


         public static Dictionary<string,bool> GetThumbnailTypesDictionary()
         {

             EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
             List<SiteModel.ThumbnailType> thumbTypes = ((from c in context.ThumbnailTypes
                                                   select c).ToList());




             Dictionary<string, bool> iType =  new Dictionary<string,bool>();
             foreach (SiteModel.ThumbnailType thumbType in thumbTypes)
             {
                 iType.Add(thumbType.ThumbnailTypeName, thumbType.DefaultFg);
             }



             return iType;
         }

          public static SiteModel.ThumbnailType GetThumbnailTypeObjectByName(string name)
         {

             EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
             SiteModel.ThumbnailType thumbType = ((from c in context.ThumbnailTypes
                                                   where c.ThumbnailTypeName == name
                                                   select c).Single());

             return thumbType;
         }

    }


    
}
