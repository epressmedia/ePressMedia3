using Brettle.Web.NeatUpload;
using Sd = System.Drawing;
using Sd2d = System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;
using System.Linq;

using System.Data;
using EPM.ImageLibrary;

public partial class Cp_AddBiz : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindCategoryList();

            bindAreaList();
        }
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

        // Extract Youtube Video ID
        string Video = "";
        if (!string.IsNullOrEmpty(VideoId.Text))
            Video = System.Text.RegularExpressions.Regex.Match(VideoId.Text, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^""&?\/ ]{11})").Groups[1].Value;

        
        int id = BEController.AddBE(BizNameKr.Text, BizNameEn.Text, int.Parse(BizCatList.SelectedValue),(Guid)System.Web.Security.Membership.GetUser().ProviderUserKey, Address.Text,
            (AreaList.SelectedIndex > 0)?AreaList.SelectedItem.Text:"",StateList.SelectedIndex > 0 ? StateList.SelectedItem.Value:"", ZipCode.Text, Video,
            "0000", ShortDesc.Text, BizDescr.Text, Phone1.TextWithPromptAndLiterals, Phone2.TextWithPromptAndLiterals, Fax.TextWithPromptAndLiterals, BizEmail.Text, chk_adowner.Checked, "", HomePage.Text, chk_premium.Checked);
            
        if (id > 0) // successfully inserted
        {
            // 관리자 등록이므로 바로 승인
            BEController.ApproveBE(id);

            // Process Images and Thumbnail
            string firstImgUrl = EPM.Core.Biz.BizThumbnailProcessor.UploadImages(id, MultiFile1.Files);// uploadImages(id);
        }

        EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, (id > 0), "Added Successfully.",
            "An error ocuured. Please contact your site administrator..", "default.aspx", "default.aspx");
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("/CP/Biz/");
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


}