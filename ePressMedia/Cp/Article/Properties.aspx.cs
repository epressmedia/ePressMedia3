using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core.Admin;
using EPM.Business.Model.Admin;

namespace ePressMedia.Cp.Article
{
    public partial class ArticleProperties : System.Web.UI.Page
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadThumbnailMode();
                LoadThumbnail();
                LoadUploadRoot();
                LoadGeneralSetting();
                LoadRelatedArticleWeight();
            }
        }

        private void LoadThumbnailMode()
        {
            ddl_thumbmode.DataSource = Enum.GetNames(typeof(EPM.ImageLibrary.ThumbnailMethod));
            ddl_thumbmode.DataBind();
        }

        private void LoadThumbnail()
        {
            thumb_width.Text = SiteSettingController.GetSiteSettingValueByName("Article Thumbnail Width");
            thumb_height.Text = SiteSettingController.GetSiteSettingValueByName("Article Thumbnail Height");
            thumb_bg_color.Text = SiteSettingController.GetSiteSettingValueByName("Article Thumbnail Background Color");
            string mode = string.IsNullOrEmpty(SiteSettingController.GetSiteSettingValueByName("Article Thumbnail Mode")) ? "" : SiteSettingController.GetSiteSettingValueByName("Article Thumbnail Mode");
            if (mode.Length > 0)
                ddl_thumbmode.SelectedIndex = ddl_thumbmode.Items.IndexOf(ddl_thumbmode.Items.FindByText(mode));
        }


        private void LoadGeneralSetting()
        {
            txt_max_art.Text = SiteSettingController.GetSiteSettingValueByName("Max Number of Articles Per Category");

            txt_max_filesize.Text = SiteSettingController.GetSiteSettingValueByName("Maximum article image upload size");
            txt_hours_new.Text = SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Article");
            
        }
        private void LoadRelatedArticleWeight()
        {

            sl_title.Value = Convert.ToDecimal(SiteSettingController.GetSiteSettingValueByName("Related Article Title Weight"));
            sl_body.Value = Convert.ToDecimal(SiteSettingController.GetSiteSettingValueByName("Related Article Body Weight"));
            sl_tag.Value = Convert.ToDecimal(SiteSettingController.GetSiteSettingValueByName("Related Article Tag Weight"));
            sl_count.Value = Convert.ToDecimal(SiteSettingController.GetSiteSettingValueByName("Related Article Count Weight"));

        }
        private void LoadUploadRoot()
        {
            txt_articleUploadeRoot.Text = EPM.Core.Admin.SiteSettings.ArticleUploadRoot;
        }

        protected void btn_uploadRoot_Click(object sender, EventArgs e)
        {
           SiteSettingController.UpdateSiteSetting("Article Upload Root", txt_articleUploadeRoot.Text);
           
        }

        protected void btn_thumbnail_Click(object sender, EventArgs e)
        {

            SiteSettingController.UpdateSiteSetting("Article Thumbnail Width", thumb_width.Text);
            SiteSettingController.UpdateSiteSetting("Article Thumbnail Height", thumb_height.Text);
            SiteSettingController.UpdateSiteSetting("Article Thumbnail Mode", ddl_thumbmode.SelectedItem.Text);
            SiteSettingController.UpdateSiteSetting("Article Thumbnail Background Color", thumb_bg_color.Text);

         
        }


        protected void btn_general_setting_save_Click(object sender, EventArgs e)
        {
            SiteSettingController.UpdateSiteSetting("Max Number of Articles Per Category", txt_max_art.Text);
            SiteSettingController.UpdateSiteSetting("Maximum article image upload size", txt_max_filesize.Text);
            SiteSettingController.UpdateSiteSetting("Number of Hours to show New in Article", txt_hours_new.Text);

                
        }

        protected void btn_relatedArticle_Click(object sender, EventArgs e)
        {
            SiteSettingController.UpdateSiteSetting("Related Article Title Weight", sl_title.Value.ToString());
            SiteSettingController.UpdateSiteSetting("Related Article Body Weight", sl_body.Value.ToString());
            SiteSettingController.UpdateSiteSetting("Related Article Tag Weight", sl_tag.Value.ToString());
            SiteSettingController.UpdateSiteSetting("Related Article Count Weight", sl_count.Value.ToString());
        }
    }
}