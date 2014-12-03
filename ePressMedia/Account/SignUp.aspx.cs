using System;
using System.Linq;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.Profile;
using Telerik.Web.UI;


using EPM.Core;

public partial class Account_SignUp : EPM.Core.StaticPageRender
{

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Label1.Text = this.Page.AppRelativeVirtualPath;
            //Master.SetBreadCrumb("<span> &gt; 계정관리 &gt; 신규회원등록</span>");

            bindProvinceData();

            


        }
        else
        {
            if (!(String.IsNullOrEmpty(Password1.Text.Trim())))
            {
                Password1.Attributes["value"] = Password1.Text;
            }

            if (!(String.IsNullOrEmpty(Password2.Text.Trim())))
            {
                Password2.Attributes["value"] = Password2.Text;
            }
        }
        LoadUDFPanel();
    }

    void LoadUDFPanel()
    {
        var c = LoadControl("/Page/UDFEntryPanel.ascx");
        ePressMedia.Pages.UDFEntryPanel uc = c as ePressMedia.Pages.UDFEntryPanel;
        uc.ID = "udfs_panel";
        uc.ContentTypeId = 8;
        //uc.ContentId = context.UserLinks.SingleOrDefault(x => x.UserId == (Guid)Membership.GetUser().ProviderUserKey).UserLinkId;
        uc.ValidationGroup = RegButton.ValidationGroup;
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

        //ddl_Province.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("--- ", "-1"));


    }

    protected void RegButton_Click(object sender, EventArgs e)
    {
        
        string msg = string.Empty;

        if (EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Not Allowed Login Name").IndexOf(UserName.Text) >= 0)
        {
            msg = GetGlobalResourceObject("Resources", "Account.msg_UserNameNotAllowed").ToString();// " 허용되지 않는 사용자 아이디입니다.";
            EPM.Legacy.Common.Utility.RegisterJsAlert(this, msg);

            return;
        }

        try
        {
            MembershipUser user = Membership.CreateUser(UserName.Text, Password1.Text, Email.Text);
            if (EPM.Core.Users.UserRoles.GetDefaultUserGroupName().Length > 0)
              Roles.AddUserToRole(user.UserName, EPM.Core.Users.UserRoles.GetDefaultUserGroupName());

            UserModel.UserLink link = new UserModel.UserLink();
            link.UserId = (Guid)user.ProviderUserKey;
            context.Add(link);
            context.SaveChanges();

            ePressMedia.Pages.UDFEntryPanel uc = UDF_Panel.FindControl("udfs_panel") as ePressMedia.Pages.UDFEntryPanel;
            uc.ContentId = link.UserLinkId;
            EPM.Business.Model.UDF.UDFController.ProcessUDFs(uc.GetValues());

            AddProfile(user.UserName);

            if (sendValidationEmail(user))
                msg = GetGlobalResourceObject("Resources", "Account.msg_AccountRegSuccess").ToString();// "등록 신청이 완료되었습니다.\\n신청시 입력하신 이메일 주소로 인증메일이 전송되었습니다.\\n메일 내용에 있는 등록 인증 링크를 클릭하시면 등록이 완료됩니다.";
            else
                msg = GetGlobalResourceObject("Resources", "Account.msg_ProblemOccured").ToString();
            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, msg, string.Empty, "SignIn.aspx");
        }
        catch (MembershipCreateUserException ex)
        {
            if (ex.StatusCode == MembershipCreateStatus.DuplicateEmail)
                msg = GetGlobalResourceObject("Resources", "Account.msg_EmailExists").ToString();//"이미 존재하는 이메일 주소입니다.";
            else if (ex.StatusCode == MembershipCreateStatus.DuplicateUserName)
                msg = GetGlobalResourceObject("Resources", "Account.msg_NotAvailable").ToString();//"이미 존재하는 사용자 아이디입니다.";
            else
                msg = ex.Message;
            
            EPM.Legacy.Common.Utility.RegisterJsAlert(this, msg);
        }
    }


    void AddProfile(string userName)
    {

        var profile = ProfileBase.Create(userName);

        profile.SetPropertyValue("Verified", false);
        profile.SetPropertyValue("Phone", Phone.Text);
        profile.SetPropertyValue("Zip", txt_postal_code.Text.Trim());
        profile.SetPropertyValue("Address1", txt_address1.Text.Trim());
        profile.SetPropertyValue("Address2", txt_address2.Text.Trim());
        if (ddl_city.SelectedItem != null)
            profile.SetPropertyValue("City", ddl_city.SelectedItem.Text);
        if (ddl_Province.SelectedItem != null)
            profile.SetPropertyValue("Province", ddl_Province.SelectedItem.Text);
        profile.SetPropertyValue("UserComment", ToKcr.Text.Length > 300 ? ToKcr.Text.Substring(0, 300) : ToKcr.Text);
        profile.SetPropertyValue("FirstName", FirstName.Text.Trim());
        profile.SetPropertyValue("LastName", LastName.Text.Trim());
        profile.SetPropertyValue("PwdExpired", false);

        
        
        

        if (ChkSubs.Checked)
            AccountHelper.SubscribeKcr(userName, true);

        //if (GenderList.SelectedIndex > 0)
        //    profile.SetPropertyValue("Gender",GenderList.SelectedValue);
        //if (AgeList.SelectedIndex > 0)
        //    profile.SetPropertyValue("Age",AgeList.SelectedValue);

        

        profile.Save();

    }


    protected bool sendValidationEmail(MembershipUser user)
    {
        //AccountHelper.SendInformationMail(user.Email, GetGlobalResourceObject("Resources", "Account.email_AccountRegEmailSubject").ToString(), 
        //    "~/MailTemplate/VerifyAccount.htm", user.ProviderUserKey.ToString(), Request.Url.Host);

        return (EPM.Email.EmailSender.SendTemplateEmail(user.Email, "Verify Account", user.ProviderUserKey.ToString(), Request.Url.Host.ToString()));

    }

    protected void DupCheck_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(UserName.Text);

        if (EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Not Allowed Login Name").IndexOf(UserName.Text) >= 0)
        {
            DupChkResult.Text = GetGlobalResourceObject("Resources", "Account.msg_UserNameNotAllowed").ToString();
            DupChkResult.CssClass = "err";
            return;
        }

        DupChkResult.Text = (user == null) ? GetGlobalResourceObject("Resources", "Account.msg_Available").ToString() : GetGlobalResourceObject("Resources", "Account.msg_NotAvailable").ToString();
        DupChkResult.CssClass = (user == null) ? "noerr" : "err";
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
        ddl_city.SelectedIndex = -1;

        if (ddl_Province.SelectedIndex > 0 || ddl_Province.SelectedItem != null)
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