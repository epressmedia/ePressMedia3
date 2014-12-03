using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using log4net;

namespace ePressMedia.Pages
{
    
    public partial class DataEntry : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataEntry));
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
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.QueryString["area"] != null && Request.QueryString["mode"] != null)
            {

                try
                {
                    string pageLoad = "";
                    if (Request.QueryString["mode"].ToString().ToLower() == "delete")
                    {
                        pageLoad = "/Page/delete.ascx";
                    }
                    else
                        pageLoad = "/" + Request.QueryString["area"].ToString() + "/entry.ascx";
                    Control uc = Page.LoadControl(pageLoad);
                    if (uc != null)
                        DataEntry_Content.Controls.Add(uc);
                }
                catch(Exception ex) {
                    log.Error(ex.Message);
                }
            }
        }
    }
}