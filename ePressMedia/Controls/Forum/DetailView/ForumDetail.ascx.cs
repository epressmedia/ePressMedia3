using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using EPM.Legacy.Common;
using EPM.Legacy.Security;
using EPM.Business.Model.Forum;
using log4net;

namespace ePressMedia.Controls.Forum.DetailView
{
    public partial class ForumDetail : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        private static readonly ILog log = LogManager.GetLogger(typeof(ForumDetail));
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

                    if (!ac.CanRead)
                    {
                        if (!EPM.Core.Users.Security.GuestViewLimitValid(this.Page))
                        {
                            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                        }
                    }

                    ListLink.NavigateUrl =
                        string.Format("/Forum/List.aspx?p={0}&page={1}&qi={2}&q={3}",
                                      Request.QueryString["p"], Request.QueryString["page"],
                                      Request.QueryString["qi"], Request.QueryString["q"]);

                    //ModLink.NavigateUrl =
                    //        string.Format("/Forum/EditThread.aspx?p={0}&page={1}&tid={2}&qi={3}&q={4}",
                    //                  Request.QueryString["p"], Request.QueryString["page"],
                    //                  Request.QueryString["tid"],
                    //                  Request.QueryString["qi"], Request.QueryString["q"]);

                    //ModLink.Visible = 
                        btn_update .Visible = ac.CanModify;
                    DelButton.Visible = ac.CanDelete;

    
                    if (ac.HasFullControl)
                    {
                        // The move to article cat list Needs to be filetered by the access.
                        List<ForumModel.Forum> forums = (from c in context.Forums
                                                         where c.ForumId != forumId
                                                         select c).ToList();

                        // Loop through the article category to see if the user has write access to the category.
                        // for each cannot be used because collection cannot be modified while it is being looped within for each. 
                        for (int i = forums.Count - 1; i >= 0; i--)
                        {
                            if (!AccessControl.AuthorizeUser(Page.User.Identity.Name,
                          ResourceType.Forum, forums[i].ForumId, Permission.Write))
                            {
                                forums.RemoveAt(i);
                            }
                        }

                        ForumList.DataTextField = "ForumName";
                        ForumList.DataValueField = "ForumId";
                        ForumList.DataSource = forums;
                        ForumList.DataBind();

                        // count available categories and if it is more than 1...
                        if (forums.Count() > 0)
                        {
                            ForumList.Items.Insert(0, new ListItem("-- Please select category to move --"));

                        }
                        else
                        {
                            ForumList.Items.Insert(0, new ListItem("-- You do not have access to any other categories. --"));
                        }
                        movePnl.Visible = true;

                    }


                    bindData(int.Parse(Request.QueryString["tid"].ToString()), ac);
                }
                catch (Exception ex) { log.Error(ex.Message); }
            }
        }

        void bindData(int threadId, AccessControl ac)
        {



            ForumModel.ForumThread t = ForumController.GetThreadByThreadID(threadId);
            if (t == null)
                return;

            MsgTitle.Text = t.Subject;
            PostBy.Text = t.PostBy;
            ViewCount.Text = t.ViewCount.ToString();
            PostDate.Text = t.PostDate.ToShortDateString() + " " + t.PostDate.ToShortTimeString();
            ViewCount.Text = t.ViewCount.ToString();
            Message.Text = t.Body;

            // configure the edit page
            string path = "/Page/DataEntry.aspx?cid=" + t.ThreadId.ToString() + "&area=forum&mode=Edit&p=" + t.ForumId.ToString();
            EntryPopupEdit.Title = "Edit Forum - " + t.Subject;
            btn_update.OnClientClick = EntryPopupEdit.GetOpenPath(path);


            IQueryable<ForumModel.ForumAttachments> attachments = ForumController.GetAttachmentsByThreadID(threadId);

            var f_pic = attachments.Where(c => (c.FileName.EndsWith("png") || c.FileName.EndsWith("gif") || c.FileName.EndsWith("jpg")));
            PicRepeater.DataSource = f_pic;
            PicRepeater.DataBind();

            var f_file = attachments.Where(c => (c.FileName.EndsWith("pdf") || c.FileName.EndsWith("xls") || c.FileName.EndsWith("doc") || c.FileName.EndsWith("docx")|| c.FileName.EndsWith("xlsx") || c.FileName.EndsWith("zip")));
                
            if (f_file.Count() > 0)
            {
                AttRepeater.DataSource = f_file;
                AttRepeater.DataBind();
            }
            else
            {
                AttRepeater.Visible = false;
            }

            if (t.Private)  // 비밀글 이면
            {
                bool showBody = ac.HasFullControl; //||    // 관리자 또는
                    // Below has been commeted out since the writer is the field user can edit when posting a new thread so it can be anything and might match.
                // Therfore, even though it is a thread posted by the user, user still has to enter the password.

                    //(Page.User.Identity.Name != string.Empty && Page.User.Identity.Name.Equals(t.UserName));  // 작성자 본인

                msgBody.Visible = showBody;
                viewPwdPanel.Visible = !showBody;

                if (showBody)
                {
                    IncreaseCounter(threadId, false);
                    //   ForumThread.IncreaseViewCount(threadId, Request, Response);
                }

                return;
            }


            // Increas counter by the cookie check
            IncreaseCounter(threadId,false);


            if (DelButton.Visible)
            {
                string msg = GetGlobalResourceObject("Resources", "Forum.msg_PasswordEnter").ToString();
                string popuppath = "/Page/DataEntry.aspx?cid=" + t.ThreadId.ToString() + "&area=forum&mode=Delete&p=" + t.Forum.ForumId.ToString() + "&passwordinput=true&returnURL=" + Server.UrlEncode(ListLink.NavigateUrl.Replace("~","")) + "&msg=" + msg;
                DeletePopup.Title = "Delete - "+ t.Subject;
                DeletePopup.Width = 300;
                DeletePopup.Height = 100;
                DelButton.OnClientClick = DeletePopup.GetOpenPath(popuppath);
            }


        }

        private void IncreaseCounter(int threadId, bool checkCookie)
        {
            var t = (from c in context.ForumThreads
                     where c.ThreadId == threadId
                     select c).Single();

            // Increas counter by the cookie check
            if (checkCookie)
            {
                if (Request.Cookies["trd" + threadId.ToString()] == null)
                {
                    t.ViewCount = t.ViewCount + 1;
                    context.SaveChanges();

                    HttpCookie cookie = new HttpCookie("trd" + threadId.ToString(), "1");
                    cookie.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(cookie);
                }
            }
            else
            {
                t.ViewCount = t.ViewCount + 1;
                context.SaveChanges();
            }
        }


 
        protected void ViewButton_Click(object sender, EventArgs e)
        {

            
            if (ForumController.ValidateThreadPassword(int.Parse(Request.QueryString["tid"].ToString()),ViewPwd.Text))
            {
                msgBody.Visible = true;
                viewPwdPanel.Visible = false;

                IncreaseCounter(int.Parse(Request.QueryString["tid"].ToString()), false);

                //ForumThread.IncreaseViewCount(t.ThreadId, Request, Response);
            }
            else
            {
                EPM.Legacy.Common.Utility.RegisterJsResultAlert(this.Page, false, string.Empty, GetGlobalResourceObject("Resources", "Account.msg_PasswordMatch").ToString(),// "패스워드가 맞지 않습니다.",
                    ListLink.NavigateUrl);
            }
        }

 

        protected void MoveLink_Click(object sender, EventArgs e)
        {

            var t = (from c in context.ForumThreads
                     where c.ThreadId == int.Parse(Request.QueryString["tid"].ToString())
                     select c).Single();

            int forumid = int.Parse(ForumList.SelectedValue);

            t.ForumId = forumid;

            if (context.ForumThreads.Where(c => c.ForumId == forumid).Count() > 0)
            {
                t.ThreadNum = ((from c in context.ForumThreads
                                where c.ForumId == forumid
                                orderby c.ThreadNum descending
                                select c).First()).ThreadNum + 1;
            }
            else
                t.ThreadNum = 1;

            context.SaveChanges();

            //ForumThread.MoveThread(CommentPanel1.SourceId, 
            //                        int.Parse(ForumList.SelectedValue));

            Response.Redirect(ListLink.NavigateUrl);
        }
    }
}