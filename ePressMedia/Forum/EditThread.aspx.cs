using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core;
using EPM.Core.Admin;
using System.Xml;
using System.Xml.Linq;
using EPM.Legacy.Security;
using Brettle.Web.NeatUpload;


public partial class Forum_EditThread : EPM.Core.StaticPageRender
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int forumId = int.Parse(Request.QueryString["p"]);

                AccessControl ac = AccessControl.SelectAccessControlByUserName(
                    Page.User.Identity.Name, ResourceType.Forum, forumId);

                if (ac == null)
                    ac = new AccessControl(Permission.None);

                if (!ac.CanModify)
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();


                var cfg = (from c in context.ForumConfigs
                           where c.ForumId == forumId
                           select c).Single();
                //ForumConfig cfg = ForumConfig.SelectById(forumId);
                prvRow.Visible = cfg.AllowPrivacy;

                if (cfg.PrivateOnly)    // 비밀글만 가능한 경우
                {
                    ChkPrivate.Checked = true;
                    ChkPrivate.Enabled = false;
                }

                if (ac.HasFullControl)
                {
                    annRow.Visible = true;
                    Password.Attributes["value"] = "1";
                    pwdRow.Visible = false;
                }



                attRow.Visible = ac.HasFullControl || cfg.AllowAttach;

                ListLink.NavigateUrl = Request.UrlReferrer.ToString();

                //Master.SetSideMenu(2, forumId, "글 수정");

                bindData(int.Parse(Request.QueryString["tid"]));
            }
            catch { SaveLink.Enabled = false; }
        }
    }
    protected override void OnPreInit(EventArgs e)
    {
        try
        {
            int cat = int.Parse(Request.QueryString["p"]);
            // Get the page setting
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string xml_setting = (context.ForumConfigs.Single(a => a.ForumId == cat)).MetadataStr;

            // check if the xml setting is set
            if (xml_setting.Length > 0)
            {
                // Load settings in xml format
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xml_setting);

                //Get Master Page setting and set the master page
                this.MasterPageFile = (xml.SelectSingleNode("/PageRoot/Configs/MasterPage")).Attributes["Value"].Value;

            }
        }
        catch
        {

        }
    }
    void bindData(int threadId)
    {

        var t = (from c in context.ForumThreads
                 where c.ThreadId == threadId
                 select c).Single();

        //ForumThread t = ForumThread.SelectThreadById(threadId);
        if (t == null)
            return;

        //if (Page.User.Identity.Name != string.Empty && t.UserName.Equals(Page.User.Identity.Name))
        //Password row should show up all the time no matter what the current user name is..
            pwdRow.Visible = true;

        Subject.Text = t.Subject;
        PostBy.Text = t.PostBy;
        CkEditor.Text = t.Body;
        ChkAnnounce.Checked = t.Announce;
        ChkPrivate.Checked = t.Private;


                var f_pic = from c in context.ForumAttachments
                    where c.ThreadId == threadId && (c.FileName.EndsWith("png") || c.FileName.EndsWith("gif") || c.FileName.EndsWith("jpg"))
                    select c;
                ImgCount.Value = f_pic.Count().ToString();
            //ForumThread.SelectImages(threadId).Count.ToString();
    }

    protected void SaveLink_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["tid"]);
        //ForumThread t = ForumThread.SelectThreadById(id);


        var t = (from c in context.ForumThreads
                 where c.ThreadId == id
                 select c).Single();

        if (Page.User.Identity.Name != string.Empty &&
            Page.User.Identity.Name.Equals(t.UserName))
            saveAndRedirect();
        else if (Password.Text.Equals(t.Password))
            saveAndRedirect();
        else if (AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Forum,
            int.Parse(Request.QueryString["p"]), Permission.FullControl))
            saveAndRedirect();
        else
            EPM.Legacy.Common.Utility.RegisterJsAlert(this, "Password does not match.");
    }

    void saveAndRedirect()
    {
        int threadId = int.Parse(Request.QueryString["tid"]);

        try
        {
            ForumModel.ForumThread t = (from c in context.ForumThreads
                                        where c.ThreadId == threadId
                                        select c).Single();
            t.Announce = ChkAnnounce.Checked;
            t.Subject = Subject.Text;
            t.Body = ForumUtility.GetCleanText(CkEditor.Text);
            t.PostBy = PostBy.Text;
            t.Private = ChkPrivate.Checked;

            context.SaveChanges();

            if (ChkDelFiles.Checked)
            {
                var attachments = from c in context.ForumAttachments
                                                                where c.ThreadId == threadId
                                                                select c;
                context.Delete(attachments);
                context.SaveChanges();



                //ForumThread.DeleteAllAttachedFiles(threadId, SiteSettings.ForumUploadRoot);
            }

            uploadFiles(threadId,
                (ChkDelFiles.Checked || ImgCount.Equals("0")));

            EPM.Legacy.Common.Utility.RegisterJsAlert(this, "Updated Successfully.");

            Response.Redirect(ListLink.NavigateUrl);
        }
        catch
        {
            EPM.Legacy.Common.Utility.RegisterJsAlert(this, "Error Occured");
            Response.Redirect(ListLink.NavigateUrl);
        }




        //bool res = ForumThread.UpdateThread(threadId,
        //    ChkAnnounce.Checked, Subject.Text,
        //    ForumUtility.GetCleanText(Server, CkEditor.Text),
        //    PostBy.Text, ChkPrivate.Checked);

        //if (res)
        //{
        //    if (ChkDelFiles.Checked)
        //        ForumThread.DeleteAllAttachedFiles(threadId, SiteSettings.ForumUploadRoot);

        //    uploadFiles(threadId, 
        //        (ChkDelFiles.Checked || ImgCount.Equals("0")));
        //}

        //EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, res, "저장되었습니다", "저장중에 오류가 발생했습니다",
        //            ListLink.NavigateUrl);
    }

    void uploadFiles(int tid, bool resetThumb)
    {
        string uploadPath = SiteSettings.ForumUploadRoot;
        string thumbPath = SiteSettings.ForumThumbnailPath;
        bool firstImage = true;

        for (int i = 0; i < MultiFile1.Files.Length; i++)
        {
            UploadedFile file = MultiFile1.Files[i];

            if (file.FileName.ToLower().EndsWith(".pdf") ||
                file.FileName.ToLower().EndsWith(".doc") ||
                file.FileName.ToLower().EndsWith(".xls") ||
                file.FileName.ToLower().EndsWith(".xlsx") ||
                file.FileName.ToLower().EndsWith(".docx") ||
                file.FileName.ToLower().EndsWith(".zip")
                )
            {
                string saveName = EPM.Core.FileHelper.GetSafeFileName(Server.MapPath(uploadPath),
                    file.FileName);

                ForumModel.ForumAttachments attachment = new ForumModel.ForumAttachments();
                attachment.FileName = saveName;
                attachment.ThreadId = tid;
                context.Add(attachment);
                context.SaveChanges();
                
                //ForumThread.InsertAttachedFile(tid, saveName);

                file.SaveAs(Server.MapPath(uploadPath) + "\\" + saveName);
            }
            else if (file.FileName.ToLower().EndsWith(".jpg") ||
                file.FileName.ToLower().EndsWith(".gif") ||
                file.FileName.ToLower().EndsWith(".png") ||
                file.FileName.ToLower().EndsWith(".bmp"))
            {
                string savedName = ImageUtilityLegacy.ResizeAndSaveImageByWidth(file,
                    Server.MapPath(uploadPath), file.FileName, 550);


                ForumModel.ForumAttachments attachment = new ForumModel.ForumAttachments();
                attachment.FileName = savedName;
                attachment.ThreadId = tid;
                context.Add(attachment);
                               

                //ForumThread.InsertAttachedFile(tid, savedName);

                if (firstImage && resetThumb)
                {
                    string thumbName = ImageUtilityLegacy.SqueezeThumbnail(
                        Server.MapPath(uploadPath) + "\\" + savedName,
                        Server.MapPath(thumbPath), 120, 90);

                    //ForumThread.SetThumbnail(tid, thumbName);

                    ForumModel.ForumThread forum_thread = (from c in context.ForumThreads
                                                           where c.ThreadId == tid
                                                           select c).Single();
                    forum_thread.Thumb = thumbPath +thumbName;
                    


                    firstImage = false;
                }

                context.SaveChanges();
            }
        }
    }
}