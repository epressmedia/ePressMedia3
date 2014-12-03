using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


using EPM.Legacy.Data;
using EPM.Legacy.Security;



    public partial class Mobile_Common_MobileNewsDetail1 : System.Web.UI.UserControl
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int cat = 1;
                    int aid = int.Parse(Request.QueryString["aid"]);

                    AccessControl ac = AccessControl.SelectAccessControlByUserName(
                                    Page.User.Identity.Name, ResourceType.Article, cat);
                    if (ac == null)
                        ac = new AccessControl(Permission.None);

                    if (!ac.CanRead)
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    bindData(aid);
                }
                catch { }
            }
        }

        void bindData(int articleId)
        {
            ArticleModel.Article a = context.Articles.Single(c => c.ArticleId == articleId);
            if (a == null)
                return;

            MsgTitle.Text = a.Title;
            SubTitle.Text = a.SubTitle;
            ViewCount.Text = a.ViewCount.ToString();
            IssueDate.Text = a.IssueDate.ToShortDateString(); // +" " + t.PostDate.ToShortTimeString();
            ViewCount.Text = a.ViewCount.ToString();
            Message.Text = a.Body.Replace("/Pics/Art/", Request.ApplicationPath + "/Pics/Art/");



            this.Page.Title = context.SiteSettings.Single(c => c.SettingName == "Default Title").SettingValue +" - "+a.Title;


        }


        public string VideoId
        {
            get
            {
                if (ViewState["VideoId"] == null)
                    return "";
                else
                    return ViewState["VideoId"].ToString();
            }
            set
            {
                ViewState["VideoId"] = value;
            }
        }
    }
