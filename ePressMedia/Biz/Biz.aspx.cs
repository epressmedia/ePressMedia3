
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

using Knn.Data;
using Knn.Common;



public partial class Biz_Biz : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            
            //Master.SetSideMenu("~/Biz/Biz.aspx", null);
            //Master.SetSidePlaceholder("~/Biz/BizCatSearch.ascx");
            //Master.SetSidePlaceholder("~/Biz/BizRankBox.ascx");
        }
    }



}
