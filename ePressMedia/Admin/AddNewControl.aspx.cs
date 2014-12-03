using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Admin
{
    public partial class AddNewControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null) // type -- ascx name
                form1.Controls.Add(Page.LoadControl("/Admin/" + Request.QueryString["type"].ToString().ToLower() + ".ascx"));             
        }
        

    }
}