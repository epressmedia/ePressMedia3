using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Admin;

namespace ePressMedia.Cp.Classified
{
    public partial class Properties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadGeneralSetting();
            }
        }
        private void LoadGeneralSetting()
        {
            txt_max_cls.Text = SiteSettingController.GetSiteSettingValueByName("Max Number of Ads Per Classified Category");
            txt_hours_new.Text = SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Classified");
        }
        protected void btn_general_setting_save_Click(object sender, EventArgs e)
        {
            SiteSettingController.UpdateSiteSetting("Max Number of Ads Per Classified Category", txt_max_cls.Text);
            SiteSettingController.UpdateSiteSetting("Number of Hours to show New in Classified", txt_hours_new.Text);
        }
    }
}