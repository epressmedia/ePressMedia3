using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Ad;
using EPM.Core.Pages;
using System.Web.Security;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Ad
{
    public partial class ZoneEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMode();
            if (!IsPostBack)
            {
                LoadActionTypes();
                if (ViewMode == DataEntryPage.EntryViewMode.Edit)
                    LoadData();
            }
        }

        private void LoadData()
        {
            if (Request.QueryString["Id"] != null)
            {
                ZoneId = int.Parse(Request.QueryString["Id"].ToString());

                AdModel.AdZone zone = ZoneController.GetZoneById(ZoneId);
                txt_name.Text = zone.ZoneName;
                txt_description.Text = zone.ZoneDescription;
                chk_weight.Checked = zone.ApplyWeightFg;
                chk_active.Checked = zone.ActiveFg;
                div_active.Visible = true;
                cbo_actoin_type.SelectedIndex = cbo_actoin_type.FindItemIndexByValue(zone.ZoneActionTypeId.ToString());

                if (zone.AdZoneActionType.ApplyWeightFg == true)
                    div_banner_weight.Visible = true;
                
                // get membership
                IList<AdModel.AdBanner> banners = BannerContoller.GetBannersByZoneId(zone.AdZoneId).ToList();

                foreach (AdModel.AdBanner banner in banners)
                {
                    Multi_Banner_List.Items.Add(new Telerik.Web.UI.RadListBoxItem(banner.BusinessEntity.PrimaryName + " - " + banner.Description, banner.AdBannerId.ToString()));
                }



            }
        }

        protected void cbo_businessEntity_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<BizModel.BusinessEntity> BEs = GetAdvertisers(e.Text).ToList();

            int ItemsPerRequest = 10;
            int itemOffset = e.NumberOfItems;
            int endOffset = Math.Min(itemOffset + ItemsPerRequest, BEs.Count);
            e.EndOfItems = endOffset == BEs.Count;

            for (int i = itemOffset; i < endOffset; i++)
            {
                cbo_businessEntity.Items.Add(new RadComboBoxItem(BEs[i].PrimaryName, BEs[i].BusinessEntityId.ToString()));
            }

           // e.Message = GetStatusMessage(endOffset, data.Rows.Count);
        }



        private IQueryable<BizModel.BusinessEntity> GetAdvertisers(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                return AdContoller.GetBusinessEntitiesWithActiveBanner();
            }
            else
            {
                return AdContoller.GetBusinessEntitiesWithActiveBanner().Where(c => c.PrimaryName.Contains(name));
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
        private void LoadActionTypes()
        {
            cbo_actoin_type.DataSource = AdContoller.GetAllZoneActionTypes();
            cbo_actoin_type.DataValueField = "AdZoneActionTypeId";
            cbo_actoin_type.DataTextField = "Name";
            cbo_actoin_type.DataBind();

            cbo_actoin_type.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("-- Select --", "0"));
        }

        protected void cbo_businessEntity_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            IList<AdModel.AdBanner> banners = BannerContoller.GetBannersByBusinessEntityId(int.Parse(e.Value)).Where(c=>c.ActiveFg == true).ToList();
            if (banners.Count() > 0)
            {
                cbo_banner_by_be.DataSource = banners;
                cbo_banner_by_be.DataValueField = "AdBannerId";
                cbo_banner_by_be.DataTextField = "Description";
                cbo_banner_by_be.DataBind();
            }
            else
            {
                cbo_banner_by_be.DataSource = null;
                cbo_banner_by_be.DataBind();
            }
        }

        protected void cbo_actoin_type_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cbo_actoin_type.SelectedIndex > 0)
            {
                AdModel.AdZoneActionType actionType = AdContoller.GetZoneActionTypeById(int.Parse(cbo_actoin_type.SelectedItem.Value.ToString()));

                if ((!actionType.AllowMultiBanners) &&(Multi_Banner_List.Items.Count() > 1))
                {
                    
                }
                div_banner_weight.Visible = actionType.ApplyWeightFg;
            }

        }


        private static DataEntryPage.EntryViewMode viewModes;
        public static DataEntryPage.EntryViewMode ViewMode
        {
            get { return viewModes; }
            set { viewModes = value; }
        }

        private static int zoneId;
        public static int ZoneId
        {
            get;
            set;
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (cbo_banner_by_be.SelectedIndex > -1)
            {
                Multi_Banner_List.Items.Add(new Telerik.Web.UI.RadListBoxItem(cbo_businessEntity.Text + " - " + cbo_banner_by_be.Text, cbo_banner_by_be.SelectedItem.Value.ToString()));
                //cbo_businessEntity.Text = "";
                //cbo_banner_by_be.Items.Clear();
            }


        }


        protected void btn_save_Click(object sender, EventArgs e)
        {
     
                Guid current_user = (Guid)Membership.GetUser().ProviderUserKey;
                int ZoneActionTypeId = int.Parse(cbo_actoin_type.SelectedItem.Value.ToString());
                if (ViewMode == DataEntryPage.EntryViewMode.Add)
                {

                    ZoneId = ZoneController.AddZone(txt_name.Text, txt_description.Text, current_user, ZoneActionTypeId, true, chk_weight.Checked);

                }
                else if (ViewMode == DataEntryPage.EntryViewMode.Edit)
                {
                    ZoneController.UpdateZone(ZoneId, txt_name.Text, txt_description.Text, current_user, ZoneActionTypeId, chk_active.Checked, div_banner_weight.Visible ? chk_weight.Checked : false);
                }


                // Drop All Membership
                MembershipContoller.InitiateMembership(ZoneId);

                List<int> BannerIDs = new List<int>();
                foreach (Telerik.Web.UI.RadListBoxItem item in Multi_Banner_List.Items)
                {
                    BannerIDs.Add(int.Parse(item.Value));
                }
                MembershipContoller.AddMembership(BannerIDs, ZoneId);

                closeWindow();

        }


        private void closeWindow()
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
        }

        protected void Multi_Banner_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show image
            int BannerId = int.Parse(Multi_Banner_List.SelectedItem.Value.ToString());

            img_banner.ImageUrl = BannerContoller.GetBannnerById(BannerId).SourcePath;

            

        }

    }

}