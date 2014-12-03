using Brettle.Web.NeatUpload;
using Sd = System.Drawing;
using Sd2d = System.Drawing.Drawing2D;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Linq;

using EPM.Legacy.Common;
using EPM.Business.Model.Biz;




public partial class Biz_PostBiz : System.Web.UI.Page
{
 
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            generateCaptcha();


            bindCategoryList();

            bindAreaList();

        }
        Password1.Attributes["value"] = Password1.Text;
        Password2.Attributes["value"] = Password2.Text;
    }

    void generateCaptcha()
    {
        Random r = new Random();

        string s = "";
        for (int i = 0; i < 4; i++)
            s = String.Concat(s, r.Next(10).ToString());

        this.Session["CaptchaImageText"] = s;
        CapCompValidator.ValueToCompare = s;
    }

    void bindAreaList()
    {
        List<GeoModel.Ref_province> provinces = EPM.Business.Model.Common.GeoController.GetAllProvinces();
        StateList.DataTextField = "province_name";
        StateList.DataValueField = "province_cd";
        StateList.DataSource = provinces;
        StateList.DataBind();

        StateList.Items.Insert(0, new ListItem("-- Select --", "-1"));
    }

    void bindCategoryList()
    {
        BizCatList.Items.Clear();

        List<BizModel.BusinessCategory> cats = BECategoryController.GetAllBusinessCatgories().ToList();

        BizCatList.Items.Add(new ListItem("--Select--", "0"));
        foreach (BizModel.BusinessCategory cat in cats)
            BizCatList.Items.Add(new ListItem(cat.CategoryName, cat.CategoryId.ToString()));
    }



    protected void PostButton_Click(object sender, EventArgs e)
    {

        //if (AreaID.Value.Length == 0)
        //    return;
        string LoginInUserName="";
        Guid LoginIdentity = Guid.Empty;
        if (Page.User.Identity.Name.Equals(string.Empty))
        {
            LoginInUserName = Page.User.Identity.Name;
        }
        else
        {
            LoginIdentity = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
        }

        // Extract Youtube Video ID
        string Video = "";
        if (!string.IsNullOrEmpty(VideoId.Text))
            Video = Regex.Match(VideoId.Text, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^""&?\/ ]{11})").Groups[1].Value;

        int BEID = BEController.AddBE(BizNameKr.Text, BizNameEn.Text, int.Parse(BizCatList.SelectedValue),  LoginIdentity, Address.Text,
            (AreaList.SelectedIndex > 0)?AreaList.SelectedItem.Text:"",StateList.SelectedIndex > 0 ? StateList.SelectedItem.Value:"",
            ZipCode.Text, Video, Password1.Text, ShortDesc.Text, BizDescr.Text, Phone1.TextWithPromptAndLiterals, Phone2.TextWithPromptAndLiterals, Fax.TextWithPromptAndLiterals, BizEmail.Text, false, LoginInUserName, HomePage.Text, false);

        if (BEID > 0) // successfully inserted
        {

            string firstImgUrl = EPM.Core.Biz.BizThumbnailProcessor.UploadImages(BEID, MultiFile1.Files);
        }

        EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, (BEID > 0), "업소록 등록 신청이 완료되었습니다. \\n업소록에는 관리자 확인 후 보여지게 됩니다.",
            "등록중 오류가 발생했습니다.\n잠시 후 다시 시도해 주세요.", "/Biz", "/Biz");
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Biz");
    }

    protected void StateList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindArea(StateList.SelectedValue);
    }

    void bindArea(string province)
    {
        AreaList.Items.Clear();

        List<GeoModel.Ref_city> cities = EPM.Business.Model.Common.GeoController.GetCitiesByProvince(province);

        AreaList.DataValueField = "city_name";
        AreaList.DataTextField = "city_name";
        AreaList.DataSource = cities;
        AreaList.DataBind();

        AreaList.Items.Insert(0, new ListItem("-- Select City -- ", "-1"));

    }

    protected void LetterList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCategoryList();
    }




}