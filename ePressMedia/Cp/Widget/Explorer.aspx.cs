using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Cp.Widget
{
    public partial class Explorer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the same skin as the one set by the SkinChooser in the Default.aspx page.
            if (!Object.Equals(Session["SkinChooserSkin"], null)) FileExplorer1.Skin = (string)Session["SkinChooserSkin"];
        }
    }
}