using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class Mobile_NewsDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int aid = int.Parse(Request.QueryString["aid"]);
            placeholder1.Controls.Add(LoadControl("Common/MobileNewsDetail1.ascx"));

        }
    }
