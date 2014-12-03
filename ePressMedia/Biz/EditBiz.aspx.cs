using Brettle.Web.NeatUpload;
using Sd = System.Drawing;
using Sd2d = System.Drawing.Drawing2D;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using EPM.Business.Model.Biz;

using EPM.Legacy.Common;
using System.Reflection;



public partial class Biz_EditBiz : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id;
            try { id = int.Parse(Request.QueryString["id"]); }
            catch { return; }


            bindCategoryList();
            bindBizView(id);

        }
    }
    void bindCategoryList()
    {
        BizCatList.Items.Clear();

        List<BizModel.BusinessCategory> cats = BECategoryController.GetAllBusinessCatgories().ToList();

        BizCatList.Items.Add(new ListItem("--Select--", "0"));
        foreach (BizModel.BusinessCategory cat in cats)
            BizCatList.Items.Add(new ListItem(cat.CategoryName, cat.CategoryId.ToString()));
    }


    void bindstate()
    {
        List<GeoModel.Ref_province> provinces = EPM.Business.Model.Common.GeoController.GetAllProvinces();
        StateList.DataTextField = "province_name";
        StateList.DataValueField = "province_cd";
        StateList.DataSource = provinces;
        StateList.DataBind();

        StateList.Items.Insert(0, new ListItem("-- Select --", "-1"));

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

    void bindBizView(int id)
    {
        BizModel.BusinessEntity b = BEController.GetBEbyBEID(id);

        bindstate();

        BizCatList.SelectedIndex = BizCatList.Items.IndexOf(BizCatList.Items.FindByValue(b.CategoryID.ToString()));
        BizName.Text = b.PrimaryName;
        BizNameEng.Text = b.SecondaryName;
        ShortDesc.Text = b.ShortDesc;
        Phone1.Text = b.Phone1;
        Phone2.Text = b.Phone2;
        Fax.Text = b.Fax;
        BizEmail.Text = b.Email;
        StateList.SelectedValue = b.State;
        bindArea(b.State);
        AreaList.SelectedValue = b.City.ToString();
        Address.Text = b.Address;
        ZipCode.Text = b.ZipCode;
        Homepage.Text = b.Website;
        VideoId.Text = string.IsNullOrEmpty(b.VideoURL)?"":"http://www.youtube.com/watch?v="+b.VideoURL;
        BizDescr.Text = b.Description;


    }
    private void CompareAndLog(string FieldName, string newvalue, string oldvalue)
    {
        int id = int.Parse(Request.QueryString["id"].ToString());
        //Expression<Func<string>> expression = () => b.PrimaryName;
        //MemberExpression body = (MemberExpression)expression.Body;
        //string name = body.Member.Name;
        if (string.IsNullOrEmpty(newvalue))
            newvalue = "";
        if (string.IsNullOrEmpty(oldvalue))
            oldvalue = "";

        if (newvalue != oldvalue)
            BERequestController.AddBusinessEntityRequest(id, FieldName, oldvalue,newvalue);
    }

    protected void PostButton_Click(object sender, EventArgs e)
    {
        int id;
        try { id = int.Parse(Request.QueryString["id"]); }
        catch { return; }

        if ( !BEController.ValidatePassword(id,Password1.Text)) // !BizDAL.VerfyPassword(id, Password1.Text))  // password doesn't match
        {
            ErrorMsg.Text = "비밀 번호가 맞지 않습니다.";
            MsgBoxMpe.Show();
            return;
        }

        // Compare the current field values against the database values
        BizModel.BusinessEntity b = BEController.GetBEbyBEID(id);
        CompareAndLog("PrimaryName", BizName.Text, b.PrimaryName);
        CompareAndLog("SecondaryName", BizNameEng.Text, b.SecondaryName);
        CompareAndLog("CategoryID", BizCatList.SelectedValue.ToString(), b.CategoryID.ToString());
        CompareAndLog("Address", Address.Text, b.Address);
        CompareAndLog("City", AreaList.SelectedIndex > 0 ? AreaList.SelectedItem.Text : "", b.City);
        CompareAndLog("State", StateList.SelectedIndex > 0 ? StateList.SelectedItem.Value.ToString() : "", b.State);
        CompareAndLog("ZipCode", ZipCode.Text, b.ZipCode);
        string Video = "";
        if (!string.IsNullOrEmpty(VideoId.Text))
            Video = Regex.Match(VideoId.Text, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^""&?\/ ]{11})").Groups[1].Value;
        CompareAndLog("VideoURL", Video, b.VideoURL);
        CompareAndLog("ShortDesc", ShortDesc.Text, b.ShortDesc);
        CompareAndLog("Description", BizDescr.Text, b.Description);
        CompareAndLog("Website", Homepage.Text, b.Website);
        CompareAndLog("Email", BizEmail.Text, b.Email);
        CompareAndLog("Phone1", Phone1.Text, b.Phone1);
        CompareAndLog("Phone2", Phone2.Text, b.Phone2);
        CompareAndLog("Fax", Fax.Text, b.Fax);
        
        bool res = true;

        EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, res, "정보 수정 요청이 접수되었습니다\\n관리자 확인 후 반영됩니다",
           "등록중 오류가 발생했습니다.\\n잠시 후 다시 시도해 주세요.", "/biz", "/biz");

        //bool res = BizDAL.UpdateBiz(id,
        //                       txtCity.Text,
        //                       BizNameKr.Text, BizNameEn.Text, ShortDesc.Text,
        //                       Phone1.Text, Phone2.Text, Fax.Text,
        //                       Address.Text, txt_zipcode.Text, HomePage.Text, BizHour.Text,
        //                       VideoId.Text, BizDescr.Text, BizEmail.Text, cbo_province.SelectedValue.ToString());

        //if (res) // updated 
        //{
        //    if (ChkDelFiles.Checked) // 기존파일 삭제
        //        BizDAL.DeleteImages(id, Server);

        //    string firstImgUrl = uploadImages(id);
        //    if (firstImgUrl != null &&
        //        (ChkChgThumb.Checked || ImgCount.Value.Equals("0") || ChkDelFiles.Checked)) // has at least one image
        //    {
        //        string imgFilePath = Server.MapPath(firstImgUrl);
        //        string thumbFileName = Path.GetFileNameWithoutExtension(imgFilePath) + "_t.jpg";
        //        string thumbFileUrl = KNN.Core.Admin.SiteSettings.BizThumbnailPath + thumbFileName;

        //        resizeThumbnail(imgFilePath, Server.MapPath(thumbFileUrl));
        //        BizDAL.SetThumbnail(id, thumbFileUrl);
        //    }
        //}

        //EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, res, "저장되었습니다", "저장에 실패했습니다",
        //                                "biz.aspx");
    }



    protected void StateList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindArea(StateList.SelectedValue);
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("/biz");
    }

 



}
