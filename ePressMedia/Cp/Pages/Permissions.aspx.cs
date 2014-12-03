using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Legacy.Security;

namespace ePressMedia.Cp.Pages
{
    public partial class Permissions : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id;
            try { id = int.Parse(Request.QueryString["id"]); }
            catch { return; }



            if (Request.QueryString["type"].ToString() != null)
            {
                
                switch (Request.QueryString["type"].ToString().ToLower())
                {
                    case "article":

                AccessControl1.ResourceType = (int)ResourceType.Article;
                AccessControl1.ResourceId = id;
                CatName.Text = context.ArticleCategories.Single(c => c.ArtCatId == id).CatName;
                break;
                    case "forum":
                        AccessControl1.ResourceType = (int)ResourceType.Forum;
                AccessControl1.ResourceId = id;
                CatName.Text = context.Forums.Single(c => c.ForumId == id).ForumName;
                break;
                    case "classified":
                                                AccessControl1.ResourceType = (int)ResourceType.Classified;
                AccessControl1.ResourceId = id;
                CatName.Text = context.ClassifiedCategories.Single(c => c.CatId == id).CatName;
                break;
                    case "calendar":
                                                AccessControl1.ResourceType = (int)ResourceType.Calendar;
                AccessControl1.ResourceId = id;
                CatName.Text = context.Calendars.Single(c => c.CalId == id).CalName;
                break;
                    default:
                break;

                }
            }
        }
    }
}