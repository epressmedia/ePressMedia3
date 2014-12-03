using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Common
{
    public partial class DisplayHtml : System.Web.UI.UserControl
    {

        private int htmlID;
        [Category("KNNProperty"), Description("ID of HTML Content"), Required(ErrorMessage = "Please enter a HTML ID")]
        public int HtmlID
        {
            get;
            set;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            List<SiteModel.Widget> htmlControl = (from c in context.Widgets
                                            where c.WidgetType.Widget_Type_Name.ToLower() == "html" && c.Widget_id == HtmlID && c.active_fg == true
                                            select c).ToList();

            if (htmlControl.Count > 0)
                lbl_html.Text = htmlControl[0].Widget_Data;
        }
    }
}