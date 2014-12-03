using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core.Pages;
using EPM.Core.Classified;
using EPM.Legacy.Security;

namespace ePressMedia.Page
{
    public partial class delete : DataEntryPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_msg.Text = Message;

            DelPassword.Visible = PasswordInput;
        }


        private void DeleteContent()
        {

            switch (Module)
            {
                case "classified":
                    DeleteClassified();
                    break;
                case "article":
                    DeleteArticle();
                    break;
                case "forum":
                    DeleteForum();
                    break;
                case "calendar":
                    DeleteCal();
                    break;
                default:
                    break;


            }

     

          //  Response.Redirect(Request.QueryString["returnURL"].ToString());// ListLink.NavigateUrl);
            
        }
        private void DeleteArticle()
        {
            if (AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Article,
    CategoryID, Permission.Delete))
            {
                EPM.Business.Model.Article.ArticleContoller.DeleteArticle(ContentID);
                CloseRedirect();
            }
        }
        private void DeleteForum()
        {
            ForumModel.ForumThread t = EPM.Business.Model.Forum.ForumController.GetThreadByThreadID(ContentID);
            //ForumThread t = ForumThread.SelectThreadById(CommentPanel1.SourceId);
            if (DelPassword.Text.Equals(t.Password) ||
                (AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Forum,CategoryID, Permission.FullControl)))
            {
                EPM.Business.Model.Forum.ForumController.DeleteForumThread(ContentID);
                CloseRedirect();

                //ForumThread.RemoveThread(CommentPanel1.SourceId);
            }

            else
            {
                Close();
            }

            
        }
        private void DeleteCal()
        {
            CalendarModel.Event ev = EPM.Business.Model.Calendar.CalController.GetEventByID(ContentID);

            if ((DelPassword.Text.Equals(ev.Password)) || (AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Calendar,
                CategoryID, Permission.FullControl)))
            {
                EPM.Business.Model.Calendar.CalController.DeleteEvent(ContentID);
                CloseRedirect();

            }
            else
            {
                Close();
            }
        }
        private void DeleteClassified()
        {
            bool abc = AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Classified,
                CategoryID, Permission.FullControl);
            if (ClassifiedHelper.VerifyPassword(ContentID, DelPassword.Text)
                || AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Classified,
                CategoryID, Permission.FullControl))
            //|| (Page.User.Identity.Name != string.Empty && Page.User.Identity.Name.Equals(UserName.Value)))
            {
                ClassifiedHelper.DeleteClassifiedAd(ContentID);
                CloseRedirect();
            }

            else
            {
                //ConfirmMpe.Show();
                Close();
            }
        }
        private void CloseRedirect()
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey2", "CloseDataEntryWithArg('" + ReturnURL + "');return false;", true);
        }
        private void Close()
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseDataEntry();", true);
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
            
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            DeleteContent();
        }
    }
}