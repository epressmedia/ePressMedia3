using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using EPM.Legacy.Security;

public partial class Cp_Classified_ClassifiedAccessControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id;
            try { id = int.Parse(Request.QueryString["id"]); }
            catch { return; }

            AccessControl1.ResourceType = (int)ResourceType.Classified;
            AccessControl1.ResourceId = id;
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            CatName.Text = context.ClassifiedCategories.SingleOrDefault(c => c.CatId == id).CatName;
        }
    }
}