using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using EPM.Legacy.Security;
using EPM.Core;
using EPM.Core.Admin;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using log4net;
using EPM.Business.Model.Admin;
using Brettle.Web.NeatUpload;
using EPM.Core.Pages;
using EPM.Business.Model.Forum;

namespace ePressMedia.Forum
{
    public partial class Entry : DataEntryPage
    {
        private Random random = new Random();
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                    PostRandomCode();

                ControlLoad();
                if (ViewMode == EntryViewMode.Edit)
                {
                    LoadData();
                }

                //Master.SetSideMenu(2, forumId, "글쓰기");
            }
            catch
            {
                btn_Save.Enabled = false;
            }
        

        }

        #region Methods
        private void ControlLoad()
        {
            AccessControl ac = GetAccessControl(ResourceType.Forum);

            if (ac == null)
                ac = new AccessControl(Permission.None);

            if (!ac.CanWrite)
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();

            PostBy.Text = Page.User.Identity.Name;

            if (ac.HasFullControl)
            {
                annRow.Visible = true;
                System.Web.Security.MembershipUser user = System.Web.Security.Membership.GetUser(Page.User.Identity.Name);
                PostBy.Text = user.UserName;
            }

            var cfg = (from c in context.ForumConfigs
                       where c.ForumId == CategoryID
                       select c).Single();

            //ForumConfig cfg = ForumConfig.SelectById(forumId);


            if (cfg.PrivateOnly)    // 비밀글만 가능한 경우
            {
                ChkPrivate.Checked = true;
                ChkPrivate.Enabled = false;
                pwdRow.Visible = true;
            }

            prvRow.Visible = cfg.AllowPrivacy;

            attRow.Visible = ac.HasFullControl || cfg.AllowAttach;

            if (ViewMode == EntryViewMode.Edit)
            {
                
                if (ac.HasFullControl)
                {
                    Password.Attributes["value"] = "1";
                    pwdRow.Visible = false;
                }
                //Disable Capcha 
                CapReq.Enabled = false;
                CaptchaRow.Visible = false;
                //CapCompValidator.Enabled = false;


                attRow.Visible = ac.HasFullControl || cfg.AllowAttach;
            }

            // Set the file extension allowed to upload
            if (attRow.Visible)
            {
                string genfile = SiteSettingController.GetSiteSettingValueByName("General file extensions allowed in Forum").ToLower().TrimEnd('|');
                string imgfile = SiteSettingController.GetSiteSettingValueByName("Image file extensions allowed in Forum").ToLower().TrimEnd('|');
                string file_list = genfile + "|" + imgfile + "|" + genfile.ToUpper() + "|" + imgfile.ToUpper();
                RegExpExt.ValidationExpression = "(([^.;]*[.])+(" + file_list + "); *)*(([^.;]*[.])+(" + file_list + "))?$";
            }

            
        }
        private void PostRandomCode()
        {
            string cap = generateRandomCode();
            this.Session["CaptchaImageText"] = cap;
         //   CapCompValidator.ValueToCompare = cap;
        }

        private string generateRandomCode()
        {
            //string s = "";
            //for (int i = 0; i < 4; i++)
            //   s = String.Concat(s, captchaChars[random.Next(26)]);
            //   // s = String.Concat(s, this.random.Next(10).ToString());
            //return s;

            var chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 4)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        bool sendPostNotification(string mailingAddresses)
        {
            if (string.IsNullOrEmpty(mailingAddresses))
                return true;

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SiteSettingController.GetSiteSettingValueByName("Forum Email Sender"), "Forum");
                mail.To.Add(mailingAddresses);
                mail.Subject = "[Forum Thread Post Notification] " + Subject.Text;
                mail.IsBodyHtml = true;
                mail.Body = ContentEditor.Content;

                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);

                return true;
            }
            catch
            {
                return false;
            }
        }

        void UploadFiles()
        {
            string uploadPath = SiteSettings.ForumUploadRoot;
            string thumbPath = SiteSettings.ForumThumbnailPath;
            bool firstImage = true;

            for (int i = 0; i < MultiFile1.Files.Length; i++)
            {
                UploadedFile file = MultiFile1.Files[i];

                if (SiteSettingController.GetSiteSettingValueByName("General file extensions allowed in Forum").ToLower().Split('|').Any(c => file.FileName.ToLower().EndsWith(c)))
                // if uploading files which ars not image format
                //if (file.FileName.ToLower().EndsWith(".pdf") ||
                //    file.FileName.ToLower().EndsWith(".doc") ||
                //    file.FileName.ToLower().EndsWith(".xls") ||
                //    file.FileName.ToLower().EndsWith(".xlsx") ||
                //    file.FileName.ToLower().EndsWith(".docx") ||
                //    file.FileName.ToLower().EndsWith(".zip")
                //    )
                {
                    string saveName = EPM.Core.FileHelper.GetSafeFileName(Server.MapPath(uploadPath),
                        file.FileName);


                    ForumController.AddThreadAttachment(ContentID, saveName);
                    file.SaveAs(Server.MapPath(uploadPath) + "\\" + saveName);
                }
                else if (SiteSettingController.GetSiteSettingValueByName("Image file extensions allowed in Forum").ToLower().Split('|').Any(c => file.FileName.ToLower().EndsWith(c)))
                {
                    string savedName = ImageUtilityLegacy.ResizeAndSaveImageByWidth(file,
                        Server.MapPath(uploadPath), file.FileName, int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Max Upload Image Width").ToString()));

                    ForumController.AddThreadAttachment(ContentID, savedName);

                    if (firstImage)
                    {
                        string thumbName = ImageUtilityLegacy.SqueezeThumbnail(
                            Server.MapPath(uploadPath) + "\\" + savedName,
                            Server.MapPath(thumbPath), 120, 90);

                        ForumController.UpdateForumThumb(ContentID, thumbPath + thumbName);

                        // The Thumbnail is genereated from the first image uploaded only
                        firstImage = false;
                    }
                }
            }
        }
        private bool PasswordCheckForEdit()
        {

            bool result = false;
            var t = (from c in context.ForumThreads
                     where c.ThreadId == ContentID
                     select c).Single();

            if (Page.User.Identity.Name != string.Empty &&
                Page.User.Identity.Name.Equals(t.UserName))
                result = true;
            else if (Password.Text.Equals(t.Password))
                result = true;
            else if (GetAccessControl(ResourceType.Forum).HasFullControl)
                result = true;
            else
                result = false;

            return result;
        }

        void LoadData()
        {
            ForumModel.ForumThread t = EPM.Business.Model.Forum.ForumController.GetThreadByThreadID(ContentID);

            if (t == null)
                return;



            Subject.Text = t.Subject;
            PostBy.Text = t.PostBy;
            ContentEditor.Content = t.Body;
            ChkAnnounce.Checked = t.Announce;
            ChkPrivate.Checked = t.Private;
            int imagecounter = ForumController.GetImageCountByThreadID(ContentID);
            ImgCount.Value = imagecounter.ToString();

            // if any attahcment exists
            if (ForumController.GetAttachmentsByThreadID(ContentID).Count() > 0)
                ChkDelFiles.Visible = true;
        }

        void AddForum()
        {

            ContentID = EPM.Business.Model.Forum.ForumController.AddForumThread(CategoryID, Subject.Text, EPM.Core.ForumUtility.GetCleanText(ContentEditor.Content), PostBy.Text, Request.UserHostAddress, ChkAnnounce.Checked,
                Password.Text, ChkPrivate.Checked, Page.User.Identity.Name, false, "");

            if (ContentID > 0)
                UploadFiles();


            // Send Email Notification
            var cfg = (from c in context.ForumConfigs
                       where c.ForumId == CategoryID
                       select c).Single();
            bool res = true;
            if (cfg.NotifyPost)
                res = sendPostNotification(cfg.MailList);

            EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Forum has been succesfully submitted.");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
        }

        void UpdateForum()
        {

            ForumController.UpdateForumThread(ContentID, Subject.Text, ForumUtility.GetCleanText(ContentEditor.Content), PostBy.Text, Request.UserHostAddress, ChkAnnounce.Checked, ChkPrivate.Checked, Page.User.Identity.Name);
            

            if (ChkDelFiles.Checked)
                ForumController.DeleteAllThreadAttachments(ContentID);

            UploadFiles();
                        EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Forum has been succesfully submitted.");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);

        }
        #endregion

        #region Event Handler
        protected void btn_Save_Click(object sender, EventArgs e)
        {


            if (ViewMode == EntryViewMode.Add)
            {
                if (CaptchaRow.Visible)
                {
                    if (this.Session["CaptchaImageText"].ToString() == Captcha.Text)
                    {
                        AddForum();
                    }
                    else
                    {

                        PostRandomCode();
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey2", "alert('" + GetGlobalResourceObject("Resources", "Captcha.ErrorMassage").ToString() + "');", true);
                    }
                }
                
            }
            else if (ViewMode == EntryViewMode.Edit)
            {
                if (PasswordCheckForEdit())
                    UpdateForum();
            }

        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
        }
        #endregion




    }

}