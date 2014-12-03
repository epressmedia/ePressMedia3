using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Globalization;
using AjaxControlToolkit;

namespace EPM.Web.UI
{
    public class EntryPopup : WebControl
    {
        
        public  string Title
        {
            get { return (String)(ViewState["Title"] ?? ""); }
            set { ViewState["Title"] = value; }
        }

        public string OpenMethod
        {
            get { return ID+"ShowDataEntry"; }
        }

        public string GetOpenPath(string path)
        {
            return OpenMethod+"('" + path + "',''); return false;";
        }

        int width = 980;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        int height = 800;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }



        protected override void CreateChildControls()
        {
            RadWindowManager _entryWindow = new RadWindowManager();
            _entryWindow.ID = ID+ "_WindowManager";
            _entryWindow.ReloadOnShow = true;
            _entryWindow.ShowContentDuringLoad = true;
            _entryWindow.VisibleStatusbar = false;
            _entryWindow.Behaviors = WindowBehaviors.Close;
            _entryWindow.InitialBehaviors = WindowBehaviors.None;
            _entryWindow.VisibleOnPageLoad = false;
            _entryWindow.ShowContentDuringLoad = false;
            _entryWindow.DestroyOnClose = true;
            _entryWindow.Modal = true;
            _entryWindow.OnClientClose = "OnClientclose";




   
                _entryWindow.Height = Unit.Pixel(Height);
                _entryWindow.MinWidth = Unit.Pixel(Width);
            
            //_entryWindow.Width = Unit.Pixel(980);


            
            _entryWindow.Animation = WindowAnimation.Fade;
            //;
            _entryWindow.AutoSize = true;
            _entryWindow.Title = Title;
            _entryWindow.CenterIfModal = true;
 

            RadWindow window = new RadWindow();
            window.ID = ID+"_EditorWindow";
            window.Behaviors = WindowBehaviors.Close;
            window.Modal = true;
            window.AutoSize = true;
            window.CenterIfModal = true;

            window.Width = System.Web.UI.WebControls.Unit.Pixel(Width);
            window.Height = System.Web.UI.WebControls.Unit.Pixel(Height);




            _entryWindow.Windows.Add(window);


            this.Controls.Add(_entryWindow);


            this.Controls.Add(new LiteralControl(@"    <script type=""text/javascript"">
        function "+OpenMethod+@"(url, name) {
            var bodyOverflow = """";
            var htmlOverflow = """";
            bodyOverflow = document.body.style.overflow;
            htmlOverflow = document.documentElement.style.overflow;
            //hide the overflow   
            document.body.style.overflow = ""hidden"";
            document.documentElement.style.overflow = ""hidden"";
            var oWndManager = $find("""+_entryWindow.ClientID+ @""");
            oWndManager.open(url, name);
        }
           
            

    </script>"));
        }
        protected override void OnInit(EventArgs e)
        {

            string cssUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "EPM.Web.UI.EPMStyle.css");

            Boolean cssAlrealyIncluded = false;
            HtmlLink linkAtual;
            foreach (Control ctrl in Page.Header.Controls)
            {
                if (ctrl.GetType() == typeof(HtmlLink))
                {
                    linkAtual = (HtmlLink)ctrl;

                    if (linkAtual.Attributes["href"].Contains(cssUrl))
                    {
                        cssAlrealyIncluded = true;
                    }
                }
            }

            if (!cssAlrealyIncluded)
            {

                HtmlLink cssLink = new HtmlLink();
                cssLink.Href = cssUrl;
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("type", "text/css");
                this.Page.Header.Controls.Add(cssLink);
            }


            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InfoBox", Page.ClientScript.GetWebResourceUrl(this.GetType(), "EPM.Web.UI.EPMScripts.js"));
        } 
    }
}
