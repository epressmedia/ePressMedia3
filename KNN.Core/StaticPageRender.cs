using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using System.Globalization;

namespace EPM.Core
{
    public class StaticPageRender : System.Web.UI.Page
    {

        protected override void InitializeCulture()
        {

            string culture_info = "";
            if (Session["CultureInfo"] != null)
            {
                culture_info = Session["CultureInfo"].ToString();
            }
            else
            {
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                culture_info = context.SiteSettings.Single(c => c.SettingName == "Culture Info").SettingValue;
                Session["CultureInfo"] = culture_info;
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture_info);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture_info);
            base.InitializeCulture();
        }

        protected override void OnPreInit(EventArgs e)
        {



            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            if (context.MasterPageLinks.Count(c => c.PagePath == this.Page.AppRelativeVirtualPath) == 1)
            {
                this.MasterPageFile = "~/Master/" + (context.MasterPageLinks.Single(c => c.PagePath == this.Page.AppRelativeVirtualPath)).MasterPageConfig.Name;
            }
            else
            {
                this.MasterPageFile = "~/Master/"+EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Default MasterPage View");
            }

            
        }

        public  void LoadSingleContent(string contentPlaceholderName, string userControlPath)
        {
            UserControl uc = (UserControl)Page.LoadControl(userControlPath);
            ContentPlaceHolder cph = this.Master.FindControl("Content") as ContentPlaceHolder;
            cph.Controls.Add(uc);
        }
    }
}
