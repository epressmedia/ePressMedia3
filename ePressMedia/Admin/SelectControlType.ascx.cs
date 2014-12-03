using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Admin
{
    public partial class SelectControlType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "IMAGE")
            {
                string url = "/Admin/AddNewControl.aspx?mode=add&type=ImageControlEditor";
                Response.Redirect(url);
            }
            else if (button.Text == "HTML")
            {
                string url = "/Admin/AddNewControl.aspx?mode=add&type=HtmlControlEditor";
                Response.Redirect(url);
            }
            else if (button.Text == "WIDGET")
            {
                string url = "/Admin/AddNewControl.aspx?type=SelectModuleType";
                Response.Redirect(url);
            }
            else if (button.Text == "AD ZONE")
            {
                string url = "/Admin/AddNewControl.aspx?mode=add&type=adzoneControlEditor";
                Response.Redirect(url);
            }
            else
            {
                CancelProcess();
            }

        }

        void CancelProcess()
        {
        }
    }
}