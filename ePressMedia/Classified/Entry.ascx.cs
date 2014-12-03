using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Web.UI;
using EPM.Core.Pages;
using EPM.Legacy.Security;
using EPM.Business.Model.Classified;
using EPM.Core.Admin;
using EPM.Core.Classified;
using log4net;
using Brettle.Web.NeatUpload;

namespace ePressMedia.Classified
{
    public partial class Entry : DataEntryPage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Entry));
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    // Check Access Control
                    AccessControl ac = GetAccessControl(ResourceType.Classified);

                    if (ac == null)
                        ac = new AccessControl(Permission.None);

                    if (!ac.CanWrite)
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    if (ac.HasFullControl)
                        annRow.Visible = true;

                    // Check if Ad ID has been provieded in Edit Mode
                    if ((ViewMode == EntryViewMode.Edit) && (ContentID > 0))
                    {

                        chk_del_current_image.Visible = ClassifiedImageController.GetImageByClassifiedId(ContentID).ToList().Count() > 0;

                        //Hide Tag field as it is  used in Add mode only
                        TypeList.Visible = false;

                    }
                    




                    if (ViewMode == EntryViewMode.View)
                        btn_Save.Visible = false;
                    else if (ViewMode == EntryViewMode.Edit)
                    {
                        // Only one password field is needed
                        CompPass.Enabled = false;
                        Password2.Visible = false;
                        ReqPass2.Enabled = false;


                        //If in Edit mode, user has full permission, then enter a value to bypass password validation
                        if (ac.HasFullControl)
                        {
                            pwdRow.Visible = false;
                            Password1.Text = "1";
                        }

                        // Load up the existing classified ad data
                        LoadClassifiedData();
                    }
                    else if (ViewMode == EntryViewMode.Add)
                    {
                        // Get Post by user
                        PostBy.Text = Page.User.Identity.Name.ToString();
                        LoadClassifiedTag();
                    }
                }
                catch (Exception ex)
                {
                    btn_Save.Visible = false;
                    log.Error(ex.Message);
                }
            }

        }

        #region Methods

        private void LoadClassifiedTag()
        {
            TypeList.DataSource= ClassifiedTagController.GetTagsByCategoryId(CategoryID);
            TypeList.DataTextField = "TagName";
            TypeList.DataValueField = "TagId";
            TypeList.DataBind();


            if (TypeList.Items.Count == 0)
            {
                TypeList.Visible = false;
            }
            else
            {
                TypeList.Items.Insert(0, new ListItem("", "-1"));
            }
        }
        private void LoadClassifiedData()
        {

            ClassifiedModel.ClassifiedAd a = ClassifiedController.GetClassifiedAdByAdId(ContentID);

            if (a != null)
            {
                PostBy.Text = a.PostBy;
                PostBy.Enabled = false; // Poested By Cannot be edited in Edit Mode
                Email.Text = a.Email;
                Phone.Text = a.Phone.Trim();
                Subject.Text = a.Subject;
                ContentEditor.Content = a.Description;
                ChkAnnounce.Checked = a.Announce;
            }

        }

        public bool VerifyPassword()
        {
            // user name might be the same for guest user name so this is not a good method to validate the password.
            bool return_value = false;

            // Check Access Control
            AccessControl ac = GetAccessControl(ResourceType.Classified);
            if (Password1.Text.Trim().Length > 0 || ac.HasFullControl)
            {
                if (ClassifiedController.ValidatePassword(ContentID, Password1.Text))
                    return_value = true;

                // Admin user and users having the full permission can delete the ads with no password validation
                if (ac.HasFullControl)
                    return_value = true;
            }

            // Popup error message when the password is not correct
            if (!return_value)
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Invalid Password. Please re-enter your password.");
            return return_value;
        }

        public void UploadImages()
        {
            // if delete current image is checked, delete image and clear the thumbnail info
            if (chk_del_current_image.Checked)
            {
                IEnumerable<ClassifiedModel.ClassifiedImage> old_images = context.ClassifiedImages.Where(c => c.AdId == ContentID);
                context.Delete(old_images);
                context.SaveChanges();

                ClassifiedController.RemoveClassifiedThumbnail(ContentID);
            }

            // get the cls upload path
            string uploadPath = SiteSettings.ClassifiedUploadRoot;
            int maxNoOfFiles = 5;
            for (int i = 0; i < MultiFile1.Files.Length && i < maxNoOfFiles; i++)
            {
                UploadedFile file = MultiFile1.Files[i];

                string savedName = ImageUtilityLegacy.ResizeAndSaveImageByWidth(file,
                    Server.MapPath(SiteSettings.ClassifiedUploadRoot), file.FileName,
                    int.Parse(MaxWidth.Value));

                InsertImage(ContentID, SiteSettings.ClassifiedUploadRoot + "/" + savedName);

                if (i == 0) // first image. to be saved as thumbnail
                {
                    string thumbName = ImageUtilityLegacy.SqueezeThumbnail(
                        Server.MapPath(SiteSettings.ClassifiedUploadRoot) + "\\" + savedName,
                        Server.MapPath(SiteSettings.ClassifiedThumbnailPath),
                        SiteSettings.ClassifiedThumbnailSize.Width,
                        SiteSettings.ClassifiedThumbnailSize.Height);

                    UpdateThumbnail(ContentID, SiteSettings.ClassifiedThumbnailPath + thumbName);
                }
            }
        }

        private void InsertImage(int id, string imageName)
        {

            ClassifiedController.AddClassifiedImage(id, imageName);

        }

        private void UpdateThumbnail(int id, string thumbName)
        {
            ClassifiedController.UpdateClassifiedThumbnail(id, thumbName);
        }

        void LoadUDFPanel()
        {
            var c = LoadControl("/Page/UDFEntryPanel.ascx");
            ePressMedia.Pages.UDFEntryPanel uc = c as ePressMedia.Pages.UDFEntryPanel;
            uc.ID = "udfs_panel";
            uc.ContentTypeId = 3; // Classified
            uc.ContentId = CategoryID;
            uc.ValidationGroup = btn_Save.ValidationGroup;
            UDFPanel.Controls.Add(uc);
        }

        private void  AddClassifiedAd()
        {
            string tag = "";
            if (TypeList.Items.Count > 0)
            {
                if (int.Parse(TypeList.SelectedItem.Value.ToString()) > 0)
                    tag = "[" + TypeList.SelectedItem.Text + "] ";
            }
            ContentID = ClassifiedController.AddClassifiedAd(tag + Subject.Text,
                ChkAnnounce.Checked, CategoryID, EPM.Core.ForumUtility.GetCleanText(ContentEditor.Content), Phone.TextWithPromptAndLiterals, Email.Text,
                Password1.Text, Request.UserHostAddress, PostBy.Text, false, Page.User.Identity.Name);

        }
        private void UpdateClassifiedAd()
        {
            ClassifiedController.UpdateClassifiedAd(ContentID, Subject.Text, ChkAnnounce.Checked,
                EPM.Core.ForumUtility.GetCleanText(ContentEditor.Content), Phone.TextWithPromptAndLiterals, Email.Text, Request.UserHostAddress);

        }

        #endregion


        #region Event Handler
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewMode == EntryViewMode.Add)
                {
                    AddClassifiedAd();
                    if (ContentID > 0)
                        UploadImages();

                }
                else if (ViewMode == EntryViewMode.Edit)
                {

                    if (VerifyPassword())
                    {
                        UpdateClassifiedAd();
                        UploadImages();
                    }
                    else
                    {
                        return;
                    }

                }
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Succesfully submitted.");
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
                
            }

            catch (Exception ex)
            {
                log.Error(ex.Message);
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "An error occured. Please contact Administrator for further assisntace.");
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
            }

        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
        }
        #endregion

    }
}