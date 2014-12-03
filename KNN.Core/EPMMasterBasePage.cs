using System;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.ComponentModel;
using log4net;
using Telerik.Web.UI;


namespace EPM.Core
{
    public class EPMMasterBasePage : System.Web.UI.MasterPage
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        private static readonly ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            OtherMetaTags();


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadGACode();
            EPM.Core.Common.StyleSheetLoader.LoadStyleSheets();
            EPM.Core.Common.JSLoader.LoadJS();

            LoadDefaultControls();
            // if user has Admins membership
            if ((HttpContext.Current.User.Identity.IsAuthenticated) && (HttpContext.Current.User.IsInRole("Admins")))
            {
                bool mode = false;
                // if the current URL doesn't not contain CP 
                if ((HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToList())[1].ToString().ToLower() == "cp")
                    mode = true;
                if (!mode)
                {
                    AdminPanelInjection();
                    ControlAddPanelLoad();
                }

            }
            ControlEditPanelLoad();
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void OtherMetaTags()
        {
            HtmlMeta IEEdge = new HtmlMeta();
            IEEdge.HttpEquiv = "X-UA-Compatible";
            IEEdge.Content = "IE=edge";

            ((HtmlHead)Page.Header).Controls.Add(IEEdge);


            HtmlMeta ViewPort = new HtmlMeta();
            ViewPort.Name = "viewport";
            ViewPort.Content = "user-scalable=no, width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0";
            
            ((HtmlHead)Page.Header).Controls.Add(ViewPort);

            

        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadDefaultControls()
        {
            bool mode = false;
            if ((HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToList())[1].ToString().ToLower() == "cp")
                mode = true;
            LoadPageControls(this.Page.MasterPageFile.Replace("/Master/", ""), mode);
        }
        public void SetBreadCrumb(string bcpath)
        {
            if (this.FindControl("BcPath") != null)
            {
                (this.FindControl("BcPath") as Literal).Text = bcpath;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hideBreadCrumb"></param>
        public void HideBreaCrumb(bool hideBreadCrumb)
        {

            Panel panel = this.FindControl("BreadCrumb") as Panel;
            if (panel != null)
                panel.Visible = !hideBreadCrumb;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadGACode()
        {
            if (context.SiteSettings.Count(c => c.SettingName == "GA Web Property ID") > 0)
            {
                string webid = context.SiteSettings.Single(c => c.SettingName == "GA Web Property ID").SettingValue;

                if (webid.Trim().Length > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"var _gaq = _gaq || [];");
                    sb.Append(@"_gaq.push(['_setAccount', '" + webid + @"']);");
                    sb.Append(@"_gaq.push(['_trackPageview']);");
                    sb.Append(@"(function() {");
                    sb.Append(@"var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;");
                    sb.Append(@"ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';");
                    sb.Append(@"var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);");
                    sb.Append(@"})();");
                    sb.Append(@"</script>");


                    if (!Page.ClientScript.IsStartupScriptRegistered("JSScript"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "analyticScript", sb.ToString());
                    }
                }
            }

        }

        /// <summary>
        /// Load Controls in Master Page from page configuraiton XML
        /// </summary>
        /// <param name="masterPageName"></param>
        /// <param name="preview"></param>
        public void LoadPageControls(string masterPageName, bool preview)
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            

            if (context.MasterPageConfigs.Count(a => a.Name == masterPageName && a.DeletedFg == false) > 0)
            {
                string xml_setting = "";
                if (this.Context.Session["MasterName"] == null)
                    xml_setting = (context.MasterPageConfigs.Where(a => a.Name == masterPageName && a.DeletedFg == false).OrderBy(c=>c.MasterPageId).ToList()[0]).MetadataStr;
                else
                {
                    if (this.Context.Session["MasterName"].ToString().Length > 0)
                        xml_setting = (context.MasterPageConfigs.Single(a => a.Description == (this.Context.Session["MasterName"].ToString()) && a.DeletedFg == false)).MetadataStr;
                    else
                        xml_setting = (context.MasterPageConfigs.Where(a => a.Name == masterPageName && a.DeletedFg == false).OrderBy(c => c.MasterPageId).ToList()[0]).MetadataStr;
                }
                //else if (contentType == ContentTypes.Page)

                EPMPageRender.PageRender(xml_setting, preview, ((HttpContext.Current.User.Identity.IsAuthenticated) && (HttpContext.Current.User.IsInRole("Admins"))),true);

            }
        }

        public void AdminPanelInjection()
        {

            // Add slide Panel on the left.
            //UserControl uc = (UserControl)(this.Page.LoadControl("/Page/AdminPanel.ascx"));
            //this.Page.Form.Controls.Add(uc);

            RadSplitter spiltter = new RadSplitter();
            spiltter.ID = "AdminPanel";
            spiltter.CssClass = "AdminPanelSpiltter";
            spiltter.Height = Unit.Percentage(100);
            RadPane adminPane = new RadPane();
            adminPane.ID = "AdminPane";
            adminPane.Width = Unit.Pixel(22);
            
            RadSlidingZone zone = new RadSlidingZone();
            zone.ID = "AdminZone";
            zone.Width = Unit.Pixel(20);

            RadSlidingPane slidingPane = new RadSlidingPane();
            slidingPane.ID = "AdminSlidingPane";
            slidingPane.Width = Unit.Pixel(200);
            slidingPane.Title = "Mode";
            slidingPane.EnableDock = false;
            PlaceHolder ph = new PlaceHolder();
            ph.ID = "ph_control";
            UserControl uc = (UserControl)(this.Page.LoadControl("/Admin/AdminPanel.ascx"));
            ph.Controls.Add(uc);
            slidingPane.Controls.Add(ph);

            RadSlidingPane PageConfigPane = new RadSlidingPane();
            PageConfigPane.ID = "PageConfigPane";
            PageConfigPane.Width = Unit.Pixel(200);
            PageConfigPane.Title = "Page Config";
            PageConfigPane.EnableDock = false;
            PlaceHolder ph2 = new PlaceHolder();
            ph2.ID = "ph_control2";
            UserControl uc2 = (UserControl)(this.Page.LoadControl("/Admin/PageConfig.ascx"));
            ph2.Controls.Add(uc2);
            PageConfigPane.Controls.Add(ph2);
            


            zone.Controls.Add(slidingPane);
            zone.Controls.Add(PageConfigPane);
            adminPane.Controls.Add(zone);
            spiltter.Controls.Add(adminPane);
            this.Page.Form.Controls.Add(spiltter);


        }


        public void ControlEditPanelLoad()
        {
            
            string sb = @"<script language=""javascript"">      
                            button = null;
                            function editControl(editID, type, placeholder) {
                                                var urlEditScreen = ""/Admin/PageControlEditor.aspx?id="" + editID+""&mode=edit&type=""+type+""&placeholder=""+placeholder;
                                                radopen(urlEditScreen, ""EditScreen"");
                                      }

                            function OnClientclose(sender, eventArgs) {
                            if (eventArgs.get_argument() == null)
{
                                 document.location.reload(true);}

                    else {document.location.href = eventArgs.get_argument();}
                            }
                        </script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "EditScreen", sb.ToString());

            Telerik.Web.UI.RadWindowManager windowManager = new Telerik.Web.UI.RadWindowManager();
            windowManager.VisibleStatusbar = false;
            //windowManager.Width = new Unit(980,UnitType.Pixel);
            //windowManager.Height =  new Unit(640, UnitType.Pixel);
            windowManager.ReloadOnShow = true;
            windowManager.EnableShadow = true;
            windowManager.AutoSize = true;
            windowManager.Modal = true;
            windowManager.ID = "ControlEditor";
            windowManager.ShowContentDuringLoad = false;
            windowManager.OnClientClose = "OnClientclose";
            windowManager.DestroyOnClose = true;
            
            windowManager.Behaviors = WindowBehaviors.Close;

            this.Page.Form.Controls.Add(windowManager);
        }
        public void ControlAddPanelLoad()
        {

            string sb = @"<script language=""javascript"">      
                            button = null;
                            function addControl(type) {
                                                var urlAddScreen = ""/Admin/AddNewCOntrol.aspx?type=""+type;
                                                radopen(urlAddScreen, ""ControlAddScreen"");
                                      }

                            function OnClientclose(sender, eventArgs) {
                                 document.location.reload(true);
                            }
                        </script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ControlAddScreen", sb.ToString());

            Telerik.Web.UI.RadWindowManager windowManager = new Telerik.Web.UI.RadWindowManager();
            windowManager.VisibleStatusbar = false;
            //windowManager.Width = new Unit(980,UnitType.Pixel);
            //windowManager.Height =  new Unit(640, UnitType.Pixel);
            windowManager.ReloadOnShow = true;
            windowManager.EnableShadow = true;
            windowManager.AutoSize = true;
            windowManager.Modal = true;
            windowManager.ID = "ControlAddScreen";
            windowManager.ShowContentDuringLoad = false;
            windowManager.OnClientClose = "OnClientclose";
            windowManager.DestroyOnClose = true;

            windowManager.Behaviors = WindowBehaviors.Close;

            this.Page.Form.Controls.Add(windowManager);
        }



    }
}
