using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Core
{
       public class  ListRedirect:System.Web.UI.Page
    {
           protected  void Page_Load(object sender, EventArgs e)
           {
               Response.Redirect("list.aspx" + CommonUtility.GetFullQueryString());

           }
    }

       public class DetailRedirect : System.Web.UI.Page
       {
           protected  void Page_Load(object sender, EventArgs e)
           {
               Response.Redirect("view.aspx" + CommonUtility.GetFullQueryString());

           }
       }

    
}
