using System;
using System.Linq;
using System.Web.Security;
using EPM.Core;
using Telerik.Web.UI;

public partial class Account_ManageAccount : EPM.Core.StaticPageRender
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.SetBreadCrumb("<span> &gt; 계정관리 &gt; 개인정보변경</span>");
            try
            {
                string chk = Session["PW_CHK"].ToString();
                if (!chk.Equals("1"))
                    Response.Redirect("VerifyPassword.aspx");

                Session["PW_CHK"] = "0";
            }
            catch
            {
                Response.Redirect("VerifyPassword.aspx");
            }

            bindProvinceData();
            LoadProfileData();

            


            
             
        }
        LoadUDFPanel();
        
    }

    void LoadUDFPanel()
    {
        var c = LoadControl("/Page/UDFEntryPanel.ascx");
        ePressMedia.Pages.UDFEntryPanel uc = c as ePressMedia.Pages.UDFEntryPanel;
        uc.ID="udfs_panel";
        uc.ContentTypeId = 8;
        uc.ContentId = context.UserLinks.SingleOrDefault(x => x.UserId == (Guid)Membership.GetUser().ProviderUserKey).UserLinkId;
        uc.ValidationGroup = SetButton.ValidationGroup;
        UDF_Panel.Controls.Add(uc);
    }


    void bindProvinceData()
    {

        var provinces = from c in context.Ref_provinces
                        orderby c.Country_cd descending, c.Province_name ascending
                        select c;

        ddl_Province.DataTextField = "province_name";
        ddl_Province.DataValueField = "province_cd";

        ddl_Province.DataSource = provinces;
        ddl_Province.DataBind();

    }

    void LoadProfileData()
    {
        

        MembershipUser user = Membership.GetUser(User.Identity.Name);
        var profile = System.Web.HttpContext.Current.Profile;


        Email.Text = user.Email.ToLower().Trim();
        FirstName.Text = profile.GetPropertyValue("FirstName").ToString();
        LastName.Text = profile.GetPropertyValue("LastName").ToString();

        Phone.Text = profile.GetPropertyValue("Phone").ToString();
        txt_postal_code.Text = profile.GetPropertyValue("Zip").ToString().ToUpper();
        txt_address1.Text = profile.GetPropertyValue("Address1").ToString();
        txt_address2.Text = profile.GetPropertyValue("Address2").ToString();

        RadComboBoxItem item = ddl_Province.FindItemByText(profile.GetPropertyValue("Province").ToString());
        if (item != null)
            item.Selected = true;

        LoadCity(ddl_Province.SelectedValue.ToString());

        RadComboBoxItem city_item = ddl_city.FindItemByText(profile.GetPropertyValue("City").ToString());
        if (city_item != null)
            city_item.Selected = true;



        if (!string.IsNullOrEmpty(profile.GetPropertyValue("UserComment").ToString()))
            ToKcr.Text = profile.GetPropertyValue("UserComment").ToString();

        
    }

    protected void SetPassword_Click(object sender, EventArgs e)
    {
        MembershipUser u = Membership.GetUser(Page.User.Identity.Name);
        bool res = u.ChangePassword(OldPassword.Text, Password1.Text);

        EPM.Legacy.Common.Utility.RegisterJsAlert(this, res ? "Password updated" : "Wrong password");
    }

    protected void SetButton_Click(object sender, EventArgs e)
    {
        UpdateProfile(User.Identity.Name);


        ePressMedia.Pages.UDFEntryPanel panel1 = UDF_Panel.FindControl("udfs_panel") as ePressMedia.Pages.UDFEntryPanel;
        if (panel1 != null)
            EPM.Business.Model.UDF.UDFController.ProcessUDFs(panel1.GetValues());   

        EPM.Legacy.Common.Utility.RegisterJsAlert(this, "User Info updated");
    }

    void UpdateProfile(string userName)
    {
        MembershipUser user = Membership.GetUser(userName);
        var profile = System.Web.HttpContext.Current.Profile;

        
        profile.SetPropertyValue("Verified", false);
        profile.SetPropertyValue("Phone", Phone.Text);
        profile.SetPropertyValue("Zip", txt_postal_code.Text.Trim());
        profile.SetPropertyValue("Address1", txt_address1.Text.Trim());
        profile.SetPropertyValue("Address2", txt_address2.Text.Trim());

        if (ddl_Province.SelectedItem != null)
            profile.SetPropertyValue("Province", ddl_Province.SelectedItem.Text);

        if (ddl_city.SelectedItem != null)
            profile.SetPropertyValue("City", ddl_city.SelectedItem.Text);

        profile.SetPropertyValue("UserComment", ToKcr.Text.Length > 300 ? ToKcr.Text.Substring(0, 300) : ToKcr.Text);
        profile.SetPropertyValue("FirstName", FirstName.Text.Trim());
        profile.SetPropertyValue("LastName", LastName.Text.Trim());
        //profile.SetPropertyValue("PwdExpired", true);

        profile.Save();

        AccountHelper.SubscribeKcr(user.UserName, ChkSubs.Checked);

    }


        protected void txt_postal_code_TextChanged(object sender, EventArgs e)
    {
        txt_postal_code.Text = txt_postal_code.Text.Replace(" ", "");

        var postal = (from c in context.Ref_postalcodes
                      where c.Postal_code.Replace(" " ,"") == txt_postal_code.Text
                      select new
                      {
                          province_cd = c.Ref_city.Province_cd,
                          city_id = c.City_id
                      }).Single();

        if (postal != null)
        {
            RadComboBoxItem item = ddl_Province.FindItemByValue(postal.province_cd);
            item.Selected = true;

            LoadCity(ddl_Province.SelectedValue.ToString());

            RadComboBoxItem cityItem = ddl_city.FindItemByValue(postal.city_id.ToString());
            cityItem.Selected = true;

            txt_address1.Focus();
        }



    }

      protected void ddl_Province_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        
        ddl_city.Items.Clear();

        if (ddl_Province.SelectedIndex > 0)
        {

            LoadCity(ddl_Province.SelectedValue.ToString());

            ddl_city.Focus();
        }
    }

      void LoadCity(string province_cd)
      {

          if (ddl_Province.SelectedIndex > 0)
          {

              var cities = from c in context.Ref_cities
                           where c.Province_cd == province_cd
                           orderby c.City_name
                           select c;

              ddl_city.DataTextField = "city_name";
              ddl_city.DataValueField = "city_id";

              ddl_city.DataSource = cities;
              ddl_city.DataBind();


          }
      }
}