using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using EPM.Core;
using log4net;
using Brettle.Web.NeatUpload;
using EPM.Core.Admin;
using EPM.Legacy.Security;
using EPM.Core.Classified;
using EPM.Business.Model.Classified;




public partial class Classified_ClassifiedPost : System.Web.UI.UserControl
{

    private static readonly ILog log = LogManager.GetLogger(typeof(Classified_ClassifiedPost));

    private static ClassifiedViewModes viewModes;
    public static ClassifiedViewModes ViewMode
    {
        get { return viewModes; }
        set { viewModes = value; }
    }

    private static int adID = -1;
    public static int AdID
    {
        get { return adID; }
        set { adID = value; }
    }

    private static int categoryID = -1;
    public static int CategoryID
    {
        get { return categoryID; }
        set { categoryID = value; }
    }

    public enum ClassifiedViewModes
    {
        Add,
        Edit,
        View
    }

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                CategoryID = int.Parse(Request.QueryString["p"]);

                // Check Access Control
                AccessControl ac = AccessControl.SelectAccessControlByUserName(
      Page.User.Identity.Name, EPM.Legacy.Security.ResourceType.Classified, CategoryID);

                if (ac == null)
                    ac = new AccessControl(Permission.None);

                if (!ac.CanWrite)
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                if (ac.HasFullControl)
                    annRow.Visible = true;

                if (Request.QueryString["Mode"] == null)
                    ViewMode = ClassifiedViewModes.View;
                else
                {
                    switch (Request.QueryString["Mode"].ToLower())
                    {
                        case "add":
                            ViewMode = ClassifiedViewModes.Add;
                            break;
                        case "edit":
                            ViewMode = ClassifiedViewModes.Edit;
                            break;
                        default:
                            ViewMode = ClassifiedViewModes.View;
                            break;
                    }
                }

                // Check if Ad ID has been provieded in Edit Mode
                if ((ViewMode == ClassifiedViewModes.Edit) && (Request.QueryString["aid"] != null))
                {
                    AdID = int.Parse(Request.QueryString["aid"]);
                    chk_del_current_image.Visible = ClassifiedImageController.GetImageByClassifiedId(adID).ToList().Count() > 0;

                    //Hide Tag field as it is  used in Add mode only
                    TypeList.Visible = false;

                }


                ListLink.NavigateUrl = (Request.UrlReferrer == null) ? "list.aspx?p=" + CategoryID.ToString() : Request.UrlReferrer.ToString();




                if (ViewMode == ClassifiedViewModes.View)
                    PostLink.Visible = false;
                else if (ViewMode == ClassifiedViewModes.Edit)
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
                else if (ViewMode == ClassifiedViewModes.Add)
                {
                    // Get Post by user
                    PostBy.Text = Page.User.Identity.Name.ToString();
                }


            }
            catch(Exception ex)
            {
                PostLink.Visible = false;
                log.Error(ex.Message);
            }

        }
        LoadUDFPanel();
    }

    // Bring the exsiting Ad data for editing
    private void LoadClassifiedData()
    {

        ClassifiedModel.ClassifiedAd a = ClassifiedController.GetClassifiedAdByAdId(AdID);

        if (a != null)
        {
            PostBy.Text = a.PostBy;
            PostBy.Enabled = false; // Poested By Cannot be edited in Edit Mode
            Email.Text = a.Email;
            Phone.Text = a.Phone;
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
        AccessControl ac = AccessControl.SelectAccessControlByUserName(
Page.User.Identity.Name, EPM.Legacy.Security.ResourceType.Classified, int.Parse(Request.QueryString["p"]));

        if (Password1.Text.Trim().Length > 0 || ac.HasFullControl)
        {
            if (ClassifiedHelper.VerifyPassword(AdID, Password1.Text))
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

    public void UploadImages(int id)
    {
        // if delete current image is checked, delete image and clear the thumbnail info
        if (chk_del_current_image.Checked)
        {
            IEnumerable<ClassifiedModel.ClassifiedImage> old_images = context.ClassifiedImages.Where(c => c.AdId == id);
            context.Delete(old_images);
            context.SaveChanges();

            UpdateThumbnail(id, "");
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

            InsertImage(id, SiteSettings.ClassifiedUploadRoot+"/"+savedName);

            if (i == 0) // first image. to be saved as thumbnail
            {
                string thumbName = ImageUtilityLegacy.SqueezeThumbnail(
                    Server.MapPath(SiteSettings.ClassifiedUploadRoot) + "\\" + savedName,
                    Server.MapPath(SiteSettings.ClassifiedThumbnailPath),
                    SiteSettings.ClassifiedThumbnailSize.Width,
                    SiteSettings.ClassifiedThumbnailSize.Height);

                UpdateThumbnail(id, SiteSettings.ClassifiedThumbnailPath + thumbName);
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
        uc.ValidationGroup = PostLink.ValidationGroup;
        UDFPanel.Controls.Add(uc);
    }




    protected void PostLink_Click(object sender, EventArgs e)
    {
        try
        {
            // Need to reset the Category ID to avoid an issue from entering data from different session(browser tab)
            CategoryID = int.Parse(Request.QueryString["p"]);
            if (Request.QueryString["Mode"] == null)
                ViewMode = ClassifiedViewModes.View;
            else
            {
                switch (Request.QueryString["Mode"].ToLower())
                {
                    case "add":
                        ViewMode = ClassifiedViewModes.Add;
                        break;
                    case "edit":
                        ViewMode = ClassifiedViewModes.Edit;
                        break;
                    default:
                        ViewMode = ClassifiedViewModes.View;
                        break;
                }
            }

            ClassifiedModel.ClassifiedAd ad;

            if (ViewMode == ClassifiedViewModes.Add)
            {
                ad = new ClassifiedModel.ClassifiedAd();
                // tag will be added only when an option is selected
                ad.Subject =  (int.Parse(TypeList.SelectedItem.Value.ToString()) > 0 ? "["+TypeList.SelectedItem.Text+"] ":"")+Subject.Text;
                ad.RegDate = DateTime.Now;
                ad.Announce = ChkAnnounce.Checked;
                ad.Category = CategoryID;
                ad.Description = ForumUtility.GetCleanText(ContentEditor.Text);
                ad.Phone = Phone.Text;
                ad.Email = Email.Text;
                ad.Password = Password1.Text;
                ad.IpAddr = Request.UserHostAddress;
                ad.PostBy = PostBy.Text;
                ad.Suspended = false;
                ad.LastUpdate = DateTime.Now;
                ad.Premium = false;
                ad.UserName = Page.User.Identity.Name;
                ad.Completed = false;

                context.Add(ad);
                context.SaveChanges();
                AdID = ad.AdId;

                UploadImages(AdID);

            }
            else if (ViewMode == ClassifiedViewModes.Edit)
            {

                if (VerifyPassword())
                {
                    ad = context.ClassifiedAds.Single(c => c.AdId == AdID);
                    ad.Subject = Subject.Text;
                    ad.LastUpdate = DateTime.Now;
                    ad.Announce = ChkAnnounce.Checked;
                    ad.Description = ForumUtility.GetCleanText(ContentEditor.Text);
                    ad.Phone = Phone.Text;
                    ad.Email = Email.Text;
                    ad.IpAddr = Request.UserHostAddress;
                    context.SaveChanges();

                    UploadImages(AdID);
                }
                else
                {
                    return;
                }

            }

            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this.Page, (CategoryID > 0), "Succesfully submitted.", "An error was occured in saving process. Please contract Administrator for further assistance.",
            ListLink.NavigateUrl);

        }

        catch (Exception ex)
        {
            log.Error(ex.Message);
            EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "An error occured. Please contact Administrator for further assisntace");
        }

    }
}