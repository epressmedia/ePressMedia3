using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class Mobile_ClassifiedDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int aid = int.Parse(Request.QueryString["aid"]);
            headerlable.Text = aid.ToString();
            placeholder1.Controls.Add(LoadControl("Common/MobileAdDetail1.ascx"));

        }
    }
