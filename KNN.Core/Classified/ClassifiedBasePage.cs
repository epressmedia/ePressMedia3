using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using System.Text;

using EPM.Legacy.Security;

namespace EPM.Core.Classified
{
    public class ClassifiedPostPage: StaticPageRender
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                try
                {
                    int catId = int.Parse(Request.QueryString["p"]);

                    if (!AccessControl.AuthorizeUser(Page.User.Identity.Name,
                        ResourceType.Classified, catId, Permission.Write))
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                }
                catch { }
            }
        }

        public void ShowResult(bool res, string redirUrl)
        {
            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, res, "등록되었습니다", "등록중 오류가 발생했습니다",
                        redirUrl);
        }


    }

    public class ClassifiedEditPage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                try
                {
                    int catId = int.Parse(Request.QueryString["p"]);

                    if (!AccessControl.AuthorizeUser(Page.User.Identity.Name,
                        ResourceType.Classified, catId, Permission.Modify))
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                }
                catch { }
            }
        }

        protected void ShowResult(bool res, string redirUrl)
        {
            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, res, "수정되었습니다", "저장중 오류가 발생했습니다",
                        redirUrl);
        }

        protected void ShowWrongPwdMessage()
        {
            EPM.Legacy.Common.Utility.RegisterJsAlert(this, "비밀번호가 맞지 않습니다.");
        }
    }

    public class ClassifiedListPage : System.Web.UI.Page
    {
        protected void AuthorizeUser(System.Web.UI.Control postLink)
        {
            int catId = int.Parse(Request.QueryString["p"]);
            AccessControl ac = AccessControl.SelectAccessControlByUserName(
                Page.User.Identity.Name, ResourceType.Classified, catId);
            if (ac == null)
                ac = new AccessControl(Permission.None);

            if (!ac.CanRead)
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();

            postLink.Visible = ac.CanWrite;
        }
    }

    public class ClassifiedViewPage : System.Web.UI.Page
    {
        protected void AuthorizeUser(Control editLink, Control delLink)
        {
            int catId = int.Parse(Request.QueryString["p"]);
            AccessControl ac = AccessControl.SelectAccessControlByUserName(
                Page.User.Identity.Name, ResourceType.Classified, catId);
            if (ac == null)
                ac = new AccessControl(Permission.None);

            if (!ac.CanRead)
                System.Web.Security.FormsAuthentication.RedirectToLoginPage();

            editLink.Visible = ac.CanModify;
            delLink.Visible = ac.CanDelete;
        }
    }
}
