using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using EPM.Core;





namespace ePressMedia.Cp
{
    public partial class Default : System.Web.UI.Page
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Assembly assembly = Assembly.LoadFrom(MapPath("~/bin/ePressMedia.dll"));
            Version ver = assembly.GetName().Version;
            version.Text ="v. "+ ver.ToString();


        }


        void getArticleInfo()
        {
            int Category = context.ArticleCategories.Count();
            int Article = context.Articles.Where(c => c.Suspended == false).Count();

        }

        void getForumInfo()
        {

        }


        


    }

    
}