using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace EPM.Web.UI
{
    public class InformationBox : Panel
    {
        public InformationBox()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string title;

        public string CssClass
        {
            get
            {
                return this.CssClass;
            }
            set
            {
                this.CssClass = String.Format("{0} {1}", "qsf-infopanel", value);
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    title = String.Format(@"<h3>{0}</h3>", value);
                else
                    title = "";
            }
        }
        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            string finalTitle = Title;
            if (!String.IsNullOrEmpty(Title) && Controls.Count == 1 && Controls[0] is LiteralControl && String.IsNullOrEmpty((Controls[0] as LiteralControl).Text.Trim()))
            {
                finalTitle = Title.Replace("class=\"title\"", "class=\"title\" style=\"margin-bottom:0\"");
            }
            writer.Write(string.Format(@"<div class=""qsf-infopanel""><span class=""qsf-ib qsf-infopanel-icon""></span><div class=""qsf-ib qsf-infopanel-content"">{0}", finalTitle));
            base.RenderContents(writer);
            writer.Write(string.Format("</div></div>"));
        }


    }
}
