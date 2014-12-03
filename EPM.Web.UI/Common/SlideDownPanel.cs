using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace EPM.Web.UI
{
    public enum ConfiguratorPanelOrientation
    {
        Horizontal,
        Vertical
    }

    public class SlideDownPanel : Panel, IPostBackDataHandler
    {
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(string.Format(
                @"<div class=""qsfConfig{4}"">
				<a class=""cfgHead qsfClear"" href=""javascript:slideConfig('{0}', '{1}');"" onfocus=""blur()"">
                    <span class=""cfgTitle"">{2}</span>
                    <span class=""cfgDescription"">{5}</span>
					<span class=""cfgButton cfg{1}""></span>
					<span class=""cfgBorder""></span>
                </a>
                <div class=""cfgContent qsfClear"" style=""{3}"">",
                ClientID,
                Expanded ? "Up" : "Down",
                Title,
                Expanded ? "" : "display:none",
                Orientation == ConfiguratorPanelOrientation.Vertical ? " cfgVertical" : "",
                Description)
            );

            base.RenderContents(writer);

            writer.Write(string.Format(@"</div><div class='hiddenFieldContainer'><input type=""hidden"" name=""{0}"" value=""{1}"" /></div></div>",
                UniqueID,
                Expanded)
                );
        }

        [DefaultValue("")]
        public string Title
        {
            get { return (string)(ViewState["Title"] ?? ""); }
            set { ViewState["Title"] = value; }
        }

        [DefaultValue("")]
        public string Description
        {
            get { return (string)(ViewState["Description"] ?? ""); }
            set { ViewState["Description"] = value; }
        }

        [DefaultValue(false)]
        public bool Expanded
        {
            get { return (bool)(ViewState["Expanded"] ?? false); }
            set { ViewState["Expanded"] = value; }
        }

        [DefaultValue(ConfiguratorPanelOrientation.Horizontal)]
        public ConfiguratorPanelOrientation Orientation
        {
            get { return (ConfiguratorPanelOrientation)(ViewState["Orientation"] ?? ConfiguratorPanelOrientation.Horizontal); }
            set { ViewState["Orientation"] = value; }
        }

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            if (!string.IsNullOrEmpty(postCollection[postDataKey]))
            {
                Expanded = Convert.ToBoolean(postCollection[postDataKey]);
            }
            return true;
        }

        public void RaisePostDataChangedEvent()
        {
        }



        protected override void OnInit(EventArgs e)
        {
            string cssUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "EPM.Web.UI.EPMStyle.css");
            HtmlLink cssLink = new HtmlLink();
            cssLink.Href = cssUrl;
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            this.Page.Header.Controls.Add(cssLink);


            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InfoBox", Page.ClientScript.GetWebResourceUrl(this.GetType(), "EPM.Web.UI.EPMScripts.js"));
        } 
    }
}
