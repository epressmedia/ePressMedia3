using Brettle.Web.NeatUpload;
using Sd = System.Drawing;
using Sd2d = System.Drawing.Drawing2D;

using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;

using EPM.Legacy.Common;
using Telerik.Web.UI;
using EPM.Web.UI;

public partial class Cp_Biz_BizInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["id"]);
        if (!IsPostBack)
        {
            
            bindCategoryList();
            bindBizView(id);
            
        }
        
        LoadChangeRequest(id);
        toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
    }

    void bindCategoryList()
    {
        BizCatList.Items.Clear();

        List<BizModel.BusinessCategory> cats = BECategoryController.GetAllBusinessCatgories().ToList();

        BizCatList.Items.Add(new ListItem("--Select--", "0"));
        foreach (BizModel.BusinessCategory cat in cats)
            BizCatList.Items.Add(new ListItem(cat.CategoryName, cat.CategoryId.ToString()));
    }


    void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        string action = e.Item.Text.ToLower();

        if (action == "save")
            SaveButtonEvent();
        else if (action == "cancel")
            CancelButtonEvent();
        else if (action == "approve")
            ApproveButtonEvent();


        Response.Redirect("/CP/Biz/");

    }
    void ApproveButtonEvent()
    {
        BEController.ApproveBE(int.Parse(Request.QueryString["id"].ToString()));
    }

    void SaveButtonEvent()
    {
        saveChanges();
    }
    void CancelButtonEvent()
    {
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

    void bindBizView(int id)
    {
        BizModel.BusinessEntity b = BEController.GetBEbyBEID(id);
        bindstate();
        Password.Text = b.Password;
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
        VideoId.Text = string.IsNullOrEmpty(b.VideoURL) ? "" : "http://www.youtube.com/watch?v=" + b.VideoURL;
        ChkAdOwner.Checked = b.AdOwner;
        Chk_Premium.Checked = b.PremiumListing;
        BizDescr.Text = b.Description;

        txt_status.Text = b.ApprovedFg?"Approved":"Not Approved";
        txt_created_date.Text = b.CreatedDate.ToString("MM/dd/yyyy");
        txt_created_by.Text = string.IsNullOrEmpty(b.CreatedByName) ? GetUserFullNameByUserID(b.CreatedBy) : b.CreatedByName;
        txt_modified_date.Text = (DateTime.MinValue == b.ModifiedDate)?"": b.ModifiedDate.ToString("MM/dd/yyyy");
        txt_modified_by.Text = GetUserFullNameByUserID(b.ModifiedBy);

        if (b.ApprovedFg)
        {
            toolbox1.DisableButton("approve");
        }


        //Load Images if exists
        if (b.BusinessEntityImages.Count() > 0)
        {
            ChkDelFiles.Visible = true;
            listImage.Visible = true;

            imageRepeater.DataSource = BEThumbnailController.GetDefaultBizThumbnailsByBEID(b.BusinessEntityId);
            imageRepeater.DataBind();
        }
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

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        saveChanges();
    }

    void AcceptChanges()
    {
        int id = int.Parse(Request.QueryString["id"]);
        Guid LoginIdentity = Guid.Empty;
        if (!Page.User.Identity.Name.Equals(string.Empty))
        {
            LoginIdentity = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
        }


        //Change Requests Process
        if (BERequestController.GetPendingBusinessEntityRequestsByBEID(id).Count() > 0)
        {
            string[] approve_requestIds = ApproveList.Value.Replace(" ", "").Split(',');
            if (approve_requestIds.Length > 0)
            {
                foreach (string approve_requestId in approve_requestIds)
                {
                    if (approve_requestId.Length > 0)
                        BERequestController.ReviewSingleField(int.Parse(approve_requestId), LoginIdentity, BERequestController.ResponseTypes.Accept);
                }
            }


            string[] reject_requestIds = RejectList.Value.Replace(" ", "").Split(',');
            if (reject_requestIds.Length > 0)
            {
                foreach (string reject_requestId in reject_requestIds)
                {
                    if (reject_requestId.Length > 0)
                        BERequestController.ReviewSingleField(int.Parse(reject_requestId), LoginIdentity, BERequestController.ResponseTypes.Reject);
                }
            }
        }

    }

    void saveChanges()
    {
        int id = int.Parse(Request.QueryString["id"]);

        Guid LoginIdentity = Guid.Empty;
        if (!Page.User.Identity.Name.Equals(string.Empty))
        {
            LoginIdentity = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
        }

        // Extract Youtube Video ID
        string Video = "";
        if (!string.IsNullOrEmpty(VideoId.Text))
            Video = System.Text.RegularExpressions.Regex.Match(VideoId.Text, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^""&?\/ ]{11})").Groups[1].Value;


        BEController.UpdateBE(id, BizName.Text, BizNameEng.Text, int.Parse(BizCatList.SelectedValue.ToString()), LoginIdentity, Address.Text, AreaList.SelectedIndex > 0 ? AreaList.SelectedItem.Text : "", StateList.SelectedIndex > 0 ? StateList.SelectedItem.Value.ToString() : "",
    ZipCode.Text, Video, Password.Text, ShortDesc.Text, BizDescr.Text, Phone1.TextWithLiterals, Phone2.TextWithLiterals, Fax.TextWithLiterals, BizEmail.Text, ChkAdOwner.Checked, Homepage.Text, Chk_Premium.Checked);


        // Change Process
        AcceptChanges();

        // IMAGE UPDATE PROCESS
        if (true)//res) // updated 
        {
            if (ChkDelFiles.Checked) // 기존파일 삭제
                BEImageController.DeleteAllImages(id);

            string firstImgUrl = EPM.Core.Biz.BizThumbnailProcessor.UploadImages(id, MultiFile1.Files);
        }

        EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, "저장되었습니다", "저장에 실패했습니다",
            "/cp/biz");
    }

    protected void StateList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindArea(StateList.SelectedValue);
    }

    string uploadImages(int id)
    {
        string uploadPath = EPM.Core.Admin.SiteSettings.BizUploadRoot;
        string firstImgUrl = null;

        int i = 0;
        for (; i < MultiFile1.Files.Length; i++)
        {
            Brettle.Web.NeatUpload.UploadedFile file = MultiFile1.Files[i];
            string saveName = Server.MapPath(uploadPath) + "\\" + id.ToString() + "_" + file.FileName;

            string savedPath = ImageUtilityLegacy.ResizeAndSaveImage(file, saveName, 2000);

            EPM.Core.Biz.BizThumbnailProcessor.GenerateThumbnails(id, uploadPath + "/" + Path.GetFileName(saveName),((ChkChgThumb.Checked) && (i == 0)));
            if (i == 0) // first image. to be cropped in the next page
                firstImgUrl = uploadPath + "/" + Path.GetFileName(saveName);
        }

        return firstImgUrl;
    }

    protected void PwdButton_Click(object sender, EventArgs e)
    {

        BEController.UpdatePassword(int.Parse(Request.QueryString["id"]), NewPwd.Text.Trim());
        PwdLabel.Text = "Password has been updated.";
        Password.Text = "";
    }



    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_createdby = (Label)item["CreatedByUser"].FindControl("lbl_createdby");
            Label lbl_modifiedby = (Label)item["ModifiedByUser"].FindControl("lbl_modifiedby");

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessEntityNote note = context.BusinessEntityNotes.Single(c => c.NoteId == int.Parse(item.GetDataKeyValue("NoteId").ToString()));

            System.Web.Security.MembershipUser cb = System.Web.Security.Membership.GetUser((object)note.CreatedBy);
            if (cb != null)
            {
                var profile = System.Web.Profile.ProfileBase.Create(cb.UserName);
                string cb_userName = profile != null ? profile.GetPropertyValue("FirstName").ToString() + " " + profile.GetPropertyValue("LastName").ToString() : "";
                lbl_createdby.Text = cb_userName;
            }
            System.Web.Security.MembershipUser mb = System.Web.Security.Membership.GetUser((object)note.ModifiedBy);
            if(mb != null)
            {
            var profile = System.Web.Profile.ProfileBase.Create(mb.UserName);
            string mb_userName = profile != null ? profile.GetPropertyValue("FirstName").ToString() + " " + profile.GetPropertyValue("LastName").ToString() : "";
            lbl_modifiedby.Text = mb_userName;
            }

        }
    }

    private string GetUserFullNameByUserID(Guid id)
    {
        System.Web.Security.MembershipUser mu = System.Web.Security.Membership.GetUser((object)id);
        if (mu != null)
        {
            var profile = System.Web.Profile.ProfileBase.Create(mu.UserName);
            return profile.GetPropertyValue("FirstName").ToString() + " " + profile.GetPropertyValue("LastName").ToString();
        }
        else
        {
            return "";
        }
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {

                GridEditableItem item = e.Item as GridEditableItem;
                GridEditManager manager = item.EditManager;
                GridTextBoxColumnEditor BusinessEntityId = manager.GetColumnEditor("BusinessEntityId") as GridTextBoxColumnEditor;
                BusinessEntityId.TextBoxControl.Enabled = false;



                GridTextBoxColumnEditor Note = manager.GetColumnEditor("Note") as GridTextBoxColumnEditor;
                Note.TextBoxControl.TextMode = TextBoxMode.MultiLine;

                GridTextBoxColumnEditor CreatedDtate = manager.GetColumnEditor("CreatedDate") as GridTextBoxColumnEditor;
                CreatedDtate.TextBoxControl.Visible = false;

                GridTextBoxColumnEditor CreatedBy = manager.GetColumnEditor("CreatedBy") as GridTextBoxColumnEditor;
                CreatedBy.TextBoxControl.Visible = false;

                GridTextBoxColumnEditor ModifiedDate = manager.GetColumnEditor("ModifiedDate") as GridTextBoxColumnEditor;
                ModifiedDate.TextBoxControl.Visible = false;

        }
    }

    protected void OpenAccessLinqDataSource1_Updating(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceUpdateEventArgs e)
    {
        BizModel.BusinessEntityNote newCategory = e.NewObject as BizModel.BusinessEntityNote;
        newCategory.ModifiedBy = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
        newCategory.ModifiedDate = DateTime.Now;       
    }

    protected void OpenAccessLinqDataSource1_Inserting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceInsertEventArgs e)
    {
        BizModel.BusinessEntityNote newCategory = e.NewObject as BizModel.BusinessEntityNote;
        newCategory.BusinessEntityId = int.Parse(Request.QueryString["id"].ToString());
        newCategory.CreatedBy = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
        newCategory.CreatedDate = DateTime.Now;
        newCategory.ModifiedBy = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
        newCategory.ModifiedDate = DateTime.Now;

    }

    protected void OpenAccessLinqDataSource1_Deleting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceDeleteEventArgs e)
    {
        BizModel.BusinessEntityNote newCategory = e.OriginalObject as BizModel.BusinessEntityNote;
        newCategory.ModifiedBy = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;
        newCategory.ModifiedDate = DateTime.Now;

    }

    private void LoadChangeRequest(int BEID)
    {
        List<BizModel.BusinessEntityRequest> requests = BERequestController.GetPendingBusinessEntityRequestsByBEID(BEID).OrderBy(c=>c.RequestId).ToList();
        ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("Content");
        foreach (BizModel.BusinessEntityRequest request in requests)
        {

            BEChangeBox epm = (BEChangeBox)(cph.FindControl("epm_" + request.FieldName));
            if (epm != null)
            {
                string value = request.NewValue;

                // Some cases the value is stored, so the display value in the dropdown should be shown up. 
                if (request.FieldName == "State")
                    value = EPM.Business.Model.Common.GeoController.GetStateFullNameByAbbreviation(value);
                else if (request.FieldName == "CategoryID")
                    value = BECategoryController.GetBusinessCatgoryByID(int.Parse(value)).CategoryName;

                epm.Text = value;
                epm.RequestId = request.RequestId;
            }
            
        }


    }
    public static object GetPropValue(object src, string propName)
    {
        return src.GetType().GetProperty(propName).GetValue(src, null);
    }
}