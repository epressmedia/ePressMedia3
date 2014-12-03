using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Pages
{
    public partial class PageControlEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Request.QueryString["id"] != null) && (Request.QueryString["type"] != null))
            {
                //lbl_control_name.Text = Request.QueryString["id"].ToString() + Request.QueryString["type"].ToString();


                    UserControl uc = (UserControl)(Page.LoadControl("/Admin/"+Request.QueryString["type"].ToString().ToLower()+"controlEditor.ascx"));
                    ContentPlaceholder.Controls.Add(uc);


            }
            
        }
    }
}