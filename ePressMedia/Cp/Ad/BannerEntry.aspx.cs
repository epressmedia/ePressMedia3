using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Ad;
using EPM.ImageLibrary;
using EPM.Core.Pages;

namespace ePressMedia.Cp.Ad
{
    public partial class BannerEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMode();
            if (!IsPostBack)
            {
                LoadMediaType();
                LoadTargetLinkType();
                InitiateControl();
                if (ViewMode == DataEntryPage.EntryViewMode.Edit)
                    LoadData();

                val_end_today_date.ValueToCompare = DateTime.Today.ToShortDateString();

                
            }
        }

        private void LoadTargetLinkType()
        {
            ddl_target_link.Items.Add(new Telerik.Web.UI.RadComboBoxItem("_blank", "_blank"));
            ddl_target_link.Items.Add(new Telerik.Web.UI.RadComboBoxItem("_self", "_self"));
        }
        private void InitiateControl()
        {
            if (ViewMode == DataEntryPage.EntryViewMode.Edit)
            {
                div_active.Visible = true;
                cbo_businessEntity.Visible = false;
                div_advertiser_add.Visible = false;
                div_advertiser_edit.Visible = true;
                rdo_start_Immediate.Enabled = rdo_start_specific.Enabled = false;
                rdo_start_specific.Checked = true;
                val_start_date.Enabled = true;
                txt_start_date.Attributes["Style"] = "";
                start_js_active.Text = "Disable";

                
            }
        }
        private void SetMode()
        {
            if (Request.QueryString["Mode"] == null)
                ViewMode = DataEntryPage.EntryViewMode.View;
            else
            {
                switch (Request.QueryString["Mode"].ToLower())
                {
                    case "add":
                        ViewMode = DataEntryPage.EntryViewMode.Add;
                        break;
                    case "edit":
                        ViewMode = DataEntryPage.EntryViewMode.Edit;
                        break;
                    default:
                        ViewMode = DataEntryPage.EntryViewMode.View;
                        break;
                }
            }

        }

        private void LoadData()
        {
            if (Request.QueryString["Id"] != null)
            {
                BannerId = int.Parse(Request.QueryString["Id"].ToString());

                AdModel.AdBanner banner = BannerContoller.GetBannnerById(BannerId);
                //cbo_businessEntity.DataBind();
                //cbo_businessEntity.SelectedIndex = cbo_businessEntity.FindItemIndexByText(banner.BusinessEntity.PrimaryName);
                txt_advertiser_name.Text = banner.BusinessEntity.PrimaryName;
                txt_description.Text = banner.Description;
                txt_start_date.SelectedDate = banner.StartDate;
                if (banner.EndDate != null)
                {
                    rdo_end_specific.Checked = true;
                    txt_end_date.SelectedDate = banner.EndDate;
                    txt_end_date.Attributes["Style"] = "";
                    val_start_end_date.Enabled = true;
                }
                sl_weight.Value = banner.Weight;
                txt_width.Text = banner.Width.ToString();
                txt_height.Text = banner.Height.ToString();
                txt_destination_url.Text = banner.LinkURL;
                txt_alt_text.Text = banner.LinkAltString;
                banner_image.ImageUrl = banner.SourcePath;
                ddl_target_link.SelectedIndex = ddl_target_link.FindItemIndexByValue(banner.LinkTarkget.ToString().ToLower());
                ddl_meida_type.SelectedIndex = ddl_meida_type.FindItemIndexByValue(banner.MediaTypeId.ToString().ToLower());
                chk_active.Checked = banner.ActiveFg;
                lbl_path.Text = banner.SourcePath;
            }
        }

        private static DataEntryPage.EntryViewMode viewModes;
        public static DataEntryPage.EntryViewMode ViewMode
        {
            get { return viewModes; }
            set { viewModes = value; }
        }

        private static int bannerId;
        public static int BannerId
        {
            get;
            set;
        }

        private void LoadMediaType()
        {
            ddl_meida_type.DataSource = AdContoller.GetMediaTypes();
            ddl_meida_type.DataTextField = "MediaTypeName";
            ddl_meida_type.DataValueField = "AdMediaTypeId";
            ddl_meida_type.DataBind();
        }

        protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            string targetPath = "/Pics/Ads";

            // check if the folder exists
            EPM.Core.FileHelper.FolderExists(Server.MapPath(targetPath), true);
            string filename = EPM.Core.FileHelper.GetSafeFileName(Server.MapPath(targetPath), e.File.FileName);


            e.File.SaveAs(Server.MapPath(targetPath) + "\\" + filename);

            //string test = targetPath.Replace("~", "");
            //string test1 = filename;4

            //RadEditor1.Content = RadEditor1.Content.Replace("/Article/StreamImage.ashx?path=/RadUploadTemp/" + replaceFilename, targetPath.Replace("~", "") + "/" + filename);
            lbl_path.Text = targetPath.Replace("~", "") + "/" + filename;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
  
      
                    Guid current_user = (Guid)Membership.GetUser().ProviderUserKey;
                    DateTime start_date = ((rdo_start_Immediate.Checked) ? DateTime.Now : txt_start_date.SelectedDate.Value);
                    DateTime end_date = (rdo_no_expire.Checked ? DateTime.MinValue : txt_end_date.SelectedDate.Value);

                    if (ViewMode == DataEntryPage.EntryViewMode.Add)
                    {
                        BannerContoller.AddBanner(int.Parse(cbo_businessEntity.SelectedValue), txt_description.Text, start_date,
                            end_date, current_user, int.Parse(ddl_meida_type.SelectedItem.Value.ToString()), int.Parse(sl_weight.Value.ToString()), int.Parse(string.IsNullOrEmpty(txt_width.Text) ? "0" : txt_width.Text),
                            int.Parse(string.IsNullOrEmpty(txt_height.Text) ? "0" : txt_height.Text), txt_destination_url.Text,
                            ddl_target_link.Text, txt_alt_text.Text, lbl_path.Text, "");
                        closeWindow();
                    }
                    else if (ViewMode == DataEntryPage.EntryViewMode.Edit)
                    {
                        BannerContoller.UpdateBanner(BannerId, txt_description.Text, txt_start_date.SelectedDate.Value, end_date, current_user, int.Parse(ddl_meida_type.SelectedItem.Value.ToString()),
                            int.Parse(sl_weight.Value.ToString()), int.Parse(string.IsNullOrEmpty(txt_width.Text) ? "0" : txt_width.Text),
                            int.Parse(string.IsNullOrEmpty(txt_height.Text) ? "0" : txt_height.Text),
                            chk_active.Checked, txt_destination_url.Text, ddl_target_link.Text, txt_alt_text.Text, lbl_path.Text, "");
                    }

                    closeWindow();



        }

        private string DateValidation()
        {
            string message = "";
            if (rdo_start_specific.Checked)
            {
                if (txt_start_date.SelectedDate == null)
                {
                    message = "Please enter start date.";
                }
                
            }
            else if (rdo_end_specific.Checked)
            {
                if (txt_end_date.SelectedDate == null)
                {
                    message = "Please enter end date.";
                }
            }

            if ((rdo_end_specific.Checked) && (rdo_start_specific.Checked) && (txt_start_date.SelectedDate >= txt_end_date.SelectedDate))
            {
                message = "End Date must be greater than Start Date.";
            }

            return message;
        }



        private void closeWindow()
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
        }
    }
}
