using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Data.Model;

namespace ePressMedia.Cp
{
    public partial class Master : System.Web.UI.MasterPage
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_username.Text = Page.User.Identity.Name;

                this.Page.Title = "Administration Panel - " + context.SiteSettings.Single(c => c.SettingName == "Default Title").SettingValue.ToString();
            }

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();

            Response.Redirect("/CP");
        }
    }
}