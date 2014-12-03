using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using EPM.Core;
using log4net;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Site
{
    public partial class EmailTemplates : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            dataLoad();

            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
        }

        void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();


            if (action == "add")
            {
                toolbox1.EnableButtons("Cancel", true);
                AddPanel.Visible = true;
            }
            else if (action == "cancel")
            {
                reset_AddPanel();
            }

        }

        void reset_AddPanel()
        {
            AddPanel.Visible = false;
            txt_email_name.Text = "";
            txt_email_description.Text = "";

            toolbox1.EnableButtons("Add", true);
        }
        private void dataLoad()
        {
            var styles = from c in context.EmailEvents
                         orderby c.EmailEventName
                         select c;

            EmailTemplateGrid.DataSource = styles;
            EmailTemplateGrid.DataBind();
        }

        protected void EmailTemplateGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("modify"))
            {
                int email_event_id = int.Parse(e.CommandArgument.ToString());
                OpenEditWindow(email_event_id.ToString(), ((GridDataItem)(e.Item)).ClientID);
                //dataLoad();
            }
        }

        protected void EmailTemplateGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void OpenEditWindow(string pid, string clientid)
        {
            RadWindow windowpreview = new RadWindow();
            windowpreview.NavigateUrl = "/cp/site/editemailtemplate.aspx?p=" + pid.ToString();// +e.CommandArgument.ToString();
            windowpreview.VisibleOnPageLoad = true;
            windowpreview.Modal = true;
            windowpreview.Behaviors = Telerik.Web.UI.WindowBehaviors.Maximize | Telerik.Web.UI.WindowBehaviors.Close | Telerik.Web.UI.WindowBehaviors.Resize | Telerik.Web.UI.WindowBehaviors.Move;
            windowpreview.OpenerElementID = clientid;// linkbutton.ClientID;
            windowpreview.AutoSize = true;
            windowpreview.ShowContentDuringLoad = true;
            windowpreview.MinWidth = 780;
            windowpreview.OnClientClose = "ReloadOnClientClose";
            //windowpreview.RestrictionZoneID = "content-outer";

            EmailTemplateDiv.Controls.Add(windowpreview);
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_email_name.Text) && !string.IsNullOrEmpty(txt_email_description.Text))
            {
                EPM.Business.Model.Admin.EmailTemplateController.AddEmailTemplate(txt_email_name.Text, txt_email_description.Text);
                reset_AddPanel();
                EmailTemplateGrid.DataBind();
            }
        }
    }
}