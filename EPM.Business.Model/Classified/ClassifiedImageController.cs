using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EPM.Data.Model;

namespace EPM.Business.Model.Classified
{
    public class ClassifiedImageController
    {
        public static IQueryable<ClassifiedModel.ClassifiedImage> GetImageByClassifiedId(int ClassifiedId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.ClassifiedImages
                   where c.ClassifiedAd.AdId == ClassifiedId
                   select c;
        }

    }
}
