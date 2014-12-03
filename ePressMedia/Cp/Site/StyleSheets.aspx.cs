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
    public partial class StyleSheets : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataLoad();
                toolbox1.EnableButtons("Add", true);

                
            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked); 
        }

        void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();


            if (action == "edit")
            {
                OpenEditWindow(getSelectItemID());
                add_panel.Visible = false;
                txt_control_path.Text = "";
            }

            else if (action == "add")
            {
                toolbox1.DisableButton("Add");
                add_panel.Visible = true;
            }
   

        }

        private string getSelectItemID()
        {
            string selectedText = "";
            GridItemCollection gridRows = ControlGrid.Items;
            foreach (GridDataItem data in gridRows)
            {
                if (data.Selected)
                {
                    selectedText = data.GetDataKeyValue("StyleSheetID").ToString(); // data["StyleSheetID"].Text;
                    break;
                }
            }

            return selectedText;
        }

        private void dataLoad()
        {
            var styles = from c in context.StyleSheets
                         where c.SystemFg == false
                         orderby c.SequenceNo
                         select c;

            ControlGrid.DataSource = styles;
            ControlGrid.DataBind();
        }
        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            

            if (e.CommandName.Equals("Deactivate"))
            {
                int stylesheetid = int.Parse(e.CommandArgument.ToString());
                SiteModel.StyleSheet selectedSS = context.StyleSheets.Single(c => c.StyleSheetId == stylesheetid);
                selectedSS.Enabled = false;
                context.SaveChanges();
                dataLoad();
                
            }
            else if (e.CommandName.Equals("Activate"))
            {
                int stylesheetid = int.Parse(e.CommandArgument.ToString());
                SiteModel.StyleSheet selectedSS = context.StyleSheets.Single(c => c.StyleSheetId == stylesheetid);
                selectedSS.Enabled = true;
                context.SaveChanges();
                dataLoad();
            }
            else if (e.CommandName.Equals("del"))
            {
                int stylesheetid = int.Parse(e.CommandArgument.ToString());
                
                DeleteStyleSheet(stylesheetid);
                dataLoad();
            }
        }

        protected void DeleteStyleSheet(int stylesheetid)
        {
            SiteModel.StyleSheet selectedSS = context.StyleSheets.Single(c => c.StyleSheetId == stylesheetid);

            int selectedsequence = selectedSS.SequenceNo;

            List<SiteModel.StyleSheet> stylesheets = (from c in context.StyleSheets
                                                    where c.SequenceNo > selectedsequence && c.SystemFg == false
                                                    select c).ToList();

            foreach (SiteModel.StyleSheet stylesheet in stylesheets)
            {
                stylesheet.SequenceNo = stylesheet.SequenceNo-1;
                context.SaveChanges();
            }

            context.Delete(selectedSS);
            context.SaveChanges();

        }

        protected void OpenEditWindow(string pid)
        {
            RadWindow windowpreview = new RadWindow();
            windowpreview.NavigateUrl = "/cp/site/editstylesheet.aspx?p="+pid.ToString();// +e.CommandArgument.ToString();
            windowpreview.VisibleOnPageLoad = true;
            windowpreview.Modal = true;
            windowpreview.Behaviors = Telerik.Web.UI.WindowBehaviors.Maximize | Telerik.Web.UI.WindowBehaviors.Close | Telerik.Web.UI.WindowBehaviors.Resize | Telerik.Web.UI.WindowBehaviors.Move;
            windowpreview.OpenerElementID = toolbox1.ClientID;
            windowpreview.AutoSize = true;
            windowpreview.ShowContentDuringLoad = true;
            windowpreview.MinWidth = 780;
            //windowpreview.RestrictionZoneID = "content-outer";

            StyleSheetDiv.Controls.Add(windowpreview);
        }

        protected void ControlGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolbox1.EnableButtons("add", true);
            if (ControlGrid.SelectedItems[0].ItemIndex > -1)
            {
                toolbox1.EnableButtons("edit", false);
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            AddStyleSheet();
            toolbox1.EnableButtons("Add", true);
            ControlGrid.SelectedIndexes.Clear();
            add_panel.Visible = false;
        }

        void AddStyleSheet()
        {

            if (context.StyleSheets.Count(c => c.Name == txt_control_path.Text && c.SystemFg == false) == 0)
            {
              SiteModel.StyleSheet stylesheet = new SiteModel.StyleSheet();
                stylesheet.SystemFg = false;
                stylesheet.Name = txt_control_path.Text;
                stylesheet.SequenceNo = context.StyleSheets.Count(c => c.SystemFg == false)+1;
                stylesheet.Enabled = true;
                context.Add(stylesheet);
                context.SaveChanges();
                dataLoad();
            }

            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid StyleSheet.')", true);

            txt_control_path.Text = "";
        }

 

        
    }
}