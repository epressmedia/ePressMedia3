using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Article;

namespace ePressMedia.Cp.Tools
{
    public partial class SlugGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_generate_click(object sender, EventArgs e)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<ArticleModel.Article> articles = context.Articles.ToList();


            foreach (ArticleModel.Article article in articles)
            {
                string slug = EPM.Core.CommonUtility.ConvertTextToSlug(article.Title);
                int counter = context.Articles.Where(c=>c.UrlSlug.Contains(slug)).Count();
                if (counter > 0)
                    slug = slug + "-" + counter.ToString();
                article.UrlSlug = slug;
                context.SaveChanges();
            }
        }
    }
}