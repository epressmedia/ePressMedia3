using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Admin;

namespace ePressMedia.Cp.Forum
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
        protected void btn_general_setting_save_Click(object sender, EventArgs e)
        {
            SiteSettingController.UpdateSiteSetting("Number of Hours to show New in Forum", txt_hours_new.Text);
            SiteSettingController.UpdateSiteSetting("Image file extensions allowed in Forum", txt_image_file_extension.Text);
            SiteSettingController.UpdateSiteSetting("General file extensions allowed in Forum", txt_gen_file_extension.Text);
            SiteSettingController.UpdateSiteSetting("Show attachment indicator(clip icon)", chk_attachment.Checked.ToString());
            
        }
        private void LoadGeneralSetting()
        {
            txt_hours_new.Text = SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Forum");
            txt_image_file_extension.Text = SiteSettingController.GetSiteSettingValueByName("Image file extensions allowed in Forum");
            txt_gen_file_extension.Text = SiteSettingController.GetSiteSettingValueByName("General file extensions allowed in Forum");
            chk_attachment.Checked = Convert.ToBoolean(SiteSettingController.GetSiteSettingValueByName("Show attachment indicator(clip icon)"));
            
        }
    }
}