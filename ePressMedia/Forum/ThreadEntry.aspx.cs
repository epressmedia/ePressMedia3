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

using Brettle.Web.NeatUpload;

namespace ePressMedia.Forum
{
    public partial class ThreadEntry : System.Web.UI.Page
    {
        private Random random = new Random();
        //string captchaChars = "1234567890";
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        private string generateRandomCode()
        {
            string s = "";
            for (int i = 0; i < 4; i++)
                //s = String.Concat(s, captchaChars[random.Next(26)]);
                s = String.Concat(s, this.random.Next(10).ToString());
            return s;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    string cap = generateRandomCode();
                    this.Session["CaptchaImageText"] = cap;
                    CapCompValidator.ValueToCompare = cap;

                    int forumId = int.Parse(Request.QueryString["p"]);
                    AccessControl ac = AccessControl.SelectAccessControlByUserName(
                        Page.User.Identity.Name, ResourceType.Forum, forumId);
                    if (ac == null)
                        ac = new AccessControl(Permission.None);

                    if (!ac.CanWrite)
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    PostBy.Text = Page.User.Identity.Name;

                    if (PostBy.Text != string.Empty)    // logged in user
                    {
                        Password.Attributes["value"] = EPM.Core.ForumUtility.GetRandomPassword(8);
                        pwdRow.Visible = false;
                    }

                    if (ac.HasFullControl)
                    {
                        annRow.Visible = true;
                        //Captcha.Text = cap;

                        System.Web.Security.MembershipUser user = System.Web.Security.Membership.GetUser(User.Identity.Name);
                        PostBy.Text = user.UserName;
                    }


                    var cfg = (from c in context.ForumConfigs
                               where c.ForumId == forumId
                               select c).Single();

                    //ForumConfig cfg = ForumConfig.SelectById(forumId);


                    if (cfg.PrivateOnly)    // 비밀글만 가능한 경우
                    {
                        ChkPrivate.Checked = true;
                        ChkPrivate.Enabled = false;
                    }

                    prvRow.Visible = cfg.AllowPrivacy;

                    attRow.Visible = ac.HasFullControl || cfg.AllowAttach;

                    ListLink.NavigateUrl = Request.UrlReferrer.ToString();

                    //Master.SetSideMenu(2, forumId, "글쓰기");
                }
                catch
                {
                    PostLink.Enabled = false;
                }
            }
        }

        protected void PostLink_Click(object sender, EventArgs e)
        {
            int forumId = int.Parse(Request.QueryString["p"]);

            ForumModel.ForumThread forum_t = new ForumModel.ForumThread();

            forum_t.ForumId = forumId;
            forum_t.Subject = Subject.Text;
            forum_t.Body = EPM.Core.ForumUtility.GetCleanText(CkEditor.Text);
            forum_t.PostBy = PostBy.Text;
            forum_t.IPAddress = Request.UserHostAddress;
            forum_t.Announce = ChkAnnounce.Checked;
            forum_t.Password = Password.Text;
            forum_t.Private = ChkAnnounce.Checked ? false : ChkPrivate.Checked;
            forum_t.UserName = Page.User.Identity.Name;
            forum_t.Suspended = false;
            forum_t.PostDate = DateTime.Now;
            forum_t.Thumb = "";


            if (context.ForumThreads.Where(c => c.ForumId == forumId).Count() > 0)
            {
                forum_t.ThreadNum = ((from c in context.ForumThreads
                                      where c.ForumId == forumId
                                      orderby c.ThreadNum descending
                                      select c).First()).ThreadNum + 1;
            }
            else
                forum_t.ThreadNum = 1;



            //int threadId = ForumThread.InsertThread(forumId, 
            //            Subject.Text, 
            //            ForumUtility.GetCleanText(Server, CkEditor.Text),
            //            PostBy.Text, Request.UserHostAddress, 
            //            ChkAnnounce.Checked, Password.Text, 
            //            ChkAnnounce.Checked ? false : ChkPrivate.Checked, //공지사항은 비밀글이 될 수 없음
            //            Page.User.Identity.Name);

            context.Add(forum_t);
            context.SaveChanges();
            int threadId = forum_t.ThreadId;

            if (threadId > 0)
                uploadFiles(threadId);



            var cfg = (from c in context.ForumConfigs
                       where c.ForumId == forumId
                       select c).Single();
            //ForumConfig cfg = ForumConfig.SelectById(forumId);

            bool res = true;
            if (cfg.NotifyPost)
                res = sendPostNotification(cfg.MailList);

            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, (threadId > 0),
                        res ? "Successfully Saved" : "Successfully Saved...",
                        "등록중에 오류가 발생했습니다",
                        ListLink.NavigateUrl);
        }

        void uploadFiles(int tid)
        {
            string uploadPath = SiteSettings.ForumUploadRoot;
            string thumbPath = SiteSettings.ForumThumbnailPath;
            bool firstImage = true;

            for (int i = 0; i < MultiFile1.Files.Length; i++)
            {
                UploadedFile file = MultiFile1.Files[i];

                if (file.FileName.ToLower().EndsWith(".pdf") ||
                    file.FileName.ToLower().EndsWith(".doc") ||
                    file.FileName.ToLower().EndsWith(".xls"))
                {
                    string saveName = EPM.Core.FileHelper.GetSafeFileName(Server.MapPath(uploadPath),
                        file.FileName);


                    ForumModel.ForumAttachments f_attachment = new ForumModel.ForumAttachments();

                    f_attachment.FileName = saveName;
                    f_attachment.ThreadId = tid;

                    context.Add(f_attachment);
                    context.SaveChanges();

                    //ForumThread.InsertAttachedFile(tid, saveName);

                    file.SaveAs(Server.MapPath(uploadPath) + "\\" + saveName);
                }
                else
                {
                    string savedName = ImageUtilityLegacy.ResizeAndSaveImageByWidth(file,
                        Server.MapPath(uploadPath), file.FileName, int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Max Upload Image Width").ToString()));


                    ForumModel.ForumAttachments f_attachment = new ForumModel.ForumAttachments();

                    f_attachment.FileName = savedName;
                    f_attachment.ThreadId = tid;

                    context.Add(f_attachment);
                    context.SaveChanges();

                    //ForumThread.InsertAttachedFile(tid, savedName);

                    if (firstImage)
                    {
                        string thumbName = ImageUtilityLegacy.SqueezeThumbnail(
                            Server.MapPath(uploadPath) + "\\" + savedName,
                            Server.MapPath(thumbPath), 120, 90);

                        var Thread = (from c in context.ForumThreads
                                      where c.ThreadId == tid
                                      select c).Single();

                        Thread.Thumb = thumbPath + thumbName;
                        context.SaveChanges();

                        //ForumThread.SetThumbnail(tid, thumbName);

                        firstImage = false;
                    }
                }
            }
        }

        bool sendPostNotification(string mailingAddresses)
        {
            if (string.IsNullOrEmpty(mailingAddresses))
                return true;

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Forum Email Sender"), "Forum");
                mail.To.Add(mailingAddresses);
                mail.Subject = "[Forum Thread Post Notification] " + Subject.Text;
                mail.IsBodyHtml = true;
                mail.Body = CkEditor.Text;

                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}