using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using EPM.Legacy.Security;
using EPM.Core;
using EPM.Core.Admin;
using EPM.ImageLibrary;
using EPM.ImageUtil;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;
using EPM.Business.Model;
using Telerik.Web.UI;
using Telerik.OpenAccess;

namespace ePressMedia.Article
{
    public partial class ArticleEntryTerminated : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Controls.Add(Page.LoadControl("/Article/Add.ascx"));
        }

    }
}