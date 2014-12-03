using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            char[] chs = { '/' };
            if (Request["ReturnUrl"] != null)
            {
                string[] strs = Request["ReturnUrl"].Split(chs);

                if (strs[1].ToLower() == "cp")
                {
                    Response.Redirect(@"\CP\Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.QueryString["ReturnURL"].ToString()));
                }
                else
                {
                    Response.Redirect(@"\Account\SignIn.aspx?ReturnUrl=" + Server.UrlEncode(Request.QueryString["ReturnURL"].ToString()));
                }
            }
        }
    }
}