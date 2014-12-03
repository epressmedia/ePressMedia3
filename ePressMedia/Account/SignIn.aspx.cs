using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.Security;
using System.Web.Profile;

using EPM.Core;
using EPM.Core.Users;

namespace Account
{
    public partial class AccountSignIn : EPM.Core.StaticPageRender
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSingleContent("Content", "~/Controls/Logins/PageLoginPanel.ascx");
        }
 
    }
}
