using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class Mobile_Classified : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Control a = LoadControl("Common/MobileAdlist1.ascx");
                placeholder1.Controls.Add(a);

            }
        }
    }
