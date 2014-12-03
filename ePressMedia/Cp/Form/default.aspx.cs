using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Form;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Form
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                toolbox1.EnableButtons("Add", true);


            }
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
            txt_formname.Text = "";
            txt_form_description.Text = "";
            chk_captcha.Checked = false;

            toolbox1.EnableButtons("Add", true);
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem updateitem = e.Item as GridEditableItem;
            string FormId = updateitem.GetDataKeyValue("FormId").ToString();
            FormController.EditForm(int.Parse(FormId), (updateitem["FormName"].Controls[0] as TextBox).Text,(updateitem["FormDescription"].Controls[0] as TextBox).Text,(updateitem["CaptchaFg"].Controls[0] as CheckBox).Checked);
            RadGrid1.Rebind();
        }

        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    GridEditManager manager = item.EditManager;
                    GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("FormId") as GridTextBoxColumnEditor;
                    SettingName.TextBoxControl.Visible = false;
                    SettingName.TextBoxControl.Width = Unit.Pixel(300);


                    GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("FormName") as GridTextBoxColumnEditor;
                    SettingDescr.TextBoxControl.Width = Unit.Pixel(500);





                }
            }
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("mod"))
            {
                RadGrid1.Items[int.Parse(e.CommandArgument.ToString())].Edit = true;
                RadGrid1.Rebind();

            }
            else if (e.CommandName.Equals("del"))
            {
                FormController.DeleteForm(int.Parse(e.CommandArgument.ToString()));
                RadGrid1.Rebind();
                //int catid = int.Parse(e.CommandArgument.ToString());
                //if (EPM.Business.Model.Admin.MainMenuController.ContentCategoryUsed("Calendar", catid))
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete calendar because it is being used in the main menu.');", true);
                //else
                //{
                //    CalendarModel.Calendar cal = context.Calendars.Single(c => c.CalId == catid);
                //    context.Delete(cal);
                //    context.SaveChanges();
                //    RadGrid1.Rebind();
                //}
            }
            else if (e.CommandName.Equals("config"))
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Form/FormEmail.aspx?formid=" + e.CommandArgument.ToString(), listing_div, null);
            }
        }

        protected void OpenAccessLinqDataSource1_Updating(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceUpdateEventArgs e)
        {

        }

 

        protected void btn_add_Click(object sender, EventArgs e)
        {
             FormController.AddForm(txt_formname.Text, txt_form_description.Text, chk_captcha.Checked);
             reset_AddPanel();
             RadGrid1.Rebind();
        }
    }
}