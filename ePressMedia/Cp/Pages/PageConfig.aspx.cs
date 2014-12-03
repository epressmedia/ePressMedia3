using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Cp.Pages
{
    public partial class PageConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["type"] != null && Request.QueryString["cat"] != null)
            {
                string type = Request.QueryString["type"].ToString().ToLower();
                EPM.Core.EPMBasePage.ContentTypes contentType;
                Boolean ShowListOnly = false;

                switch (type)
                {
                    case "article":
                        contentType = EPM.Core.EPMBasePage.ContentTypes.Article;
                        break;
                    case "forum":
                        contentType = EPM.Core.EPMBasePage.ContentTypes.Forum;
                        break;
                    case "page":
                        contentType = EPM.Core.EPMBasePage.ContentTypes.Page;
                        ShowListOnly = true;
                        break;
                    case "classified":
                        contentType = EPM.Core.EPMBasePage.ContentTypes.Classified;
                        break;
                    case "master":
                        contentType = EPM.Core.EPMBasePage.ContentTypes.MasterPage;
                        ShowListOnly = true;
                        break;
                    case "template":
                        contentType = EPM.Core.EPMBasePage.ContentTypes.Template;
                        ShowListOnly = true;
                        break;
                    default:
                        contentType = EPM.Core.EPMBasePage.ContentTypes.Article;
                        break;
                }

                PageBuilder.ContentType = contentType;
                tab_detail.Enabled = !ShowListOnly;
                tab_detail.Visible = !ShowListOnly;
                if (!ShowListOnly)
                {
                    PageBuilderDetail.ContentType = contentType;
                }    
            }
            else
                PageConfig_Container.Visible = false;
            
        }
    }
}