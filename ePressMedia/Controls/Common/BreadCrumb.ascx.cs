using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;

namespace ePressMedia.Controls.Common
{
    [Description("Page Bread Crumb")]
    public partial class BreadCrumb : System.Web.UI.UserControl
    {
        static string bcSeperator = "<span> &gt; </span>";
        static string linkFormat = "<a href='{0}' target='{1}'>{2}</a>";
        private static readonly ILog log = LogManager.GetLogger(typeof(BreadCrumb));
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (CatId == 0)
                {
                    if (Request.QueryString["p"] != null)
                        CatId = int.Parse(Request.QueryString["p"].ToString());
                    else
                        CatId = -1;
                }

                String Baseurl = "~/" + (HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToList())[1].ToString() + "/";
                if (Baseurl.ToLower() == "~/page/")
                {
                    string fullOrigionalpath = Request.Url.ToString();

                    string pageid = (Request.Url.Query.Split('&'))[0].Replace("?pg=", "");


                    CatId = int.Parse(pageid);
                }

                PopulateSideMenu(Baseurl, CatId);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " \r\n " + ex.StackTrace + "\r\n" + ex.Source);
            }


        }

        public void PopulateSideMenu(string Baseurl, int contentId)
        {

            if ((context.ContentTypes.Count(c => c.BaseUrl == Baseurl) > 0) || (Baseurl == "~/cp/"))
            {

                if ((Baseurl == "~/cp/") && (Request.QueryString["content"] != null))
                {
                    Baseurl = "~/" + Request.QueryString["content"].ToString() + "/";
                }
                int contentTypeID = context.ContentTypes.Single(cc => cc.BaseUrl == Baseurl).ContentTypeId;
                IQueryable<SiteModel.SiteMenu> menu = from c in context.SiteMenus
                                                      where c.ContentView.ContentTypeId == contentTypeID && c.Visible == true && c.Param == contentId.ToString()
                                                      orderby c.MenuId descending
                                                      select c;

                if (menu.Count() > 0)
                {
                    setSideMenu(menu.ToList()[0], null);

                }
                else
                {
                    BreadCrumb_Panel.Visible = false;
                }


            }

            else
            {
                BreadCrumb_Panel.Visible = false;
            }

        }

        private void setSideMenu(SiteModel.SiteMenu menu, string curPageName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!ShowLastLevelOnly)
                sb.Append(bcSeperator);
            sb.AppendFormat(linkFormat, menu.Url.StartsWith("~") ? menu.Url.Substring(1) : menu.Url,
                    menu.Target, menu.Label);
            if (ShowLastLevelOnly)
                A1.Visible = false;

            if (!string.IsNullOrEmpty(curPageName))
            {
                if (!ShowLastLevelOnly)
                    sb.Append(bcSeperator);
                sb.Append(curPageName);
            }

            if (!ShowLastLevelOnly)
            {
                while (menu.ParentId > 0)
                {


                    menu = context.SiteMenus.Single(c => c.MenuId == menu.ParentId);
                    sb.Insert(0, string.Format(linkFormat,
                            menu.Url.StartsWith("~") ? menu.Url.Substring(1) : menu.Url,
                            menu.Target, menu.Label));
                    sb.Insert(0, bcSeperator);
                }
            }
            else
            {

            }

            BcPath.Text = sb.ToString();
        }
        private ContentTypes contentType;
        [Category("EPMProperty"), Description("Define Content Type to overwite")]
        public ContentTypes ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        public enum ContentTypes
        {
            Article = 1,
            Forum = 2,
            Classified = 3,
            Calendar = 4,
            Biz = 5,
            Search = 6,
            Page = 7
        }

        private int catId = 0;
        [Category("EPMProperty"), Description("Define Category ID to overwrite")]
        public int CatId
        {
            get { return catId; }
            set { catId = value; }
        }


        private Boolean showLastLevelOnly = false;
        [Category("EPMProperty"), Description("Display the name of last breadcrumb level only"), DefaultValue(typeof(System.Boolean), "false"), Required()]
        public Boolean ShowLastLevelOnly
        {
            get { return showLastLevelOnly; }
            set { showLastLevelOnly = value; }
        }

    }
}