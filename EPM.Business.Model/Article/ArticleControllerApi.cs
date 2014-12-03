using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EPM.Business.Model.Article
{
    public class ArticleControllerApi:ApiController
    {
        private EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();

        public List<ArticleModel.Article> Get()
        {
            var articles = from a in context.Articles
                           select a;
            return articles.ToList();
        }
        
    }
}
