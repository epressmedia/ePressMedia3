using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class Mobile_Biz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string pageURL="";
                placeholder1.Controls.Clear();

                if (Request.QueryString["q"] != null)
                    pageURL = "Common/MobileBizSearch.ascx";
                else if ((Request.QueryString["char"] != null) || (Request.QueryString["fav"] != null))
                    pageURL = "Common/MobileBizSub1.ascx";
                else if (Request.QueryString["catId"] != null)
                    pageURL = "Common/MobileBizList1.ascx";
                else if (Request.QueryString["bizId"] != null)
                    pageURL = "Common/MobileBizDetail1.ascx";
                else
                    pageURL = "Common/MobileBiz1.ascx";

                
                placeholder1.Controls.Add(LoadControl(pageURL));
            }

        }
    }
