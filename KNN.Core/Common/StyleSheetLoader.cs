using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using EPM.Data.Model;

namespace EPM.Core.Common
{
    public static class StyleSheetLoader
    {
        /// <summary>
        /// 
        /// </summary>
        public static  void LoadStyleSheets()
        {
            //string[] styles = { "../Styles/Default.css", "../Styles/Classified.css", "~/Styles/Biz.css", "~/Styles/Controls_Common.css", "~/Styles/Controls_Article.css", "~/Styles/Controls_Calendar.css", "~/Styles/Controls_SideSubMenu.css", "~/Styles/Custom_Control.css", "~/Styles/Custom_Default.css","~/Styles/Controls_Menu.css" };

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<SiteModel.StyleSheet> stylesheets = (from c in context.StyleSheets
                                                      where c.Enabled == true
                                                      orderby c.SystemFg descending, c.SequenceNo
                                                      select c).ToList();

            foreach (SiteModel.StyleSheet style in stylesheets)
            {
                HtmlHead header = (HtmlHead)((System.Web.HttpContext.Current.Handler as Page).Header);

                HtmlLink css = new HtmlLink();
                css.Href = style.Name;
                css.Attributes["rel"] = "stylesheet";
                css.Attributes["type"] = "text/css";
                header.Controls.Add(css);
            }
        }

    }

    public static class JSLoader
    {
        public static void LoadJS()
        {
        //            <script src='<%= this.ResolveClientUrl("~/Scripts/jquery-1.7.2.min.js") %>' type="text/javascript"></script>
        //<script src='<%= this.ResolveClientUrl("~/Scripts/Common.js") %>' type="text/javascript"></script>
        //<script src='<%= this.ResolveClientUrl("~/Scripts/Custom.js") %>' type="text/javascript"></script>
        //<script src='<%= this.ResolveClientUrl("~/Scripts/jquery.cookie.js") %>' type="text/javascript"></script>
        //<script src='<%= this.ResolveClientUrl("~/Scripts/Controls_Article.js") %>' type="text/javascript"></script>

            string[] JSsrcs = {"/Scripts/jquery-latest.min.js","/Scripts/Common.js","/Scripts/Custom.js","/Scripts/Controls_Article.js"
            ,"/Scripts/jquery.cookie.js",
            "/Scripts/Default.js"};
            foreach (string JSsrc in JSsrcs)
            {
                HtmlHead header = (HtmlHead)((System.Web.HttpContext.Current.Handler as Page).Header);

                HtmlGenericControl js = new HtmlGenericControl("script");
                js.Attributes["type"] = "text/javascript";
                js.Attributes["src"] = JSsrc;
                header.Controls.Add(js);
                
            }
        }
    }
}
