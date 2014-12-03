using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using EPM.Business.Model.Calendar;
using Telerik.Web.UI;


public partial class Cp_Calendar_Calendars : System.Web.UI.Page
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
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
        txt_calendar.Text   = "";

        toolbox1.EnableButtons("Add", true);
    }


    protected void AddButton_Click(object sender, EventArgs e)
    {

        if (txt_calendar.Text.Trim().Length > 0)
        {
            CalController.AddCategory(txt_calendar.Text);
            RadGrid1.Rebind();
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

            int catid = int.Parse(e.CommandArgument.ToString());
            if (EPM.Business.Model.Admin.MainMenuController.ContentCategoryUsed("Calendar", catid))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete calendar because it is being used in the main menu.');", true);
            else
            {
                CalendarModel.Calendar cal = context.Calendars.Single(c => c.CalId == catid);
                context.Delete(cal);
                context.SaveChanges();
                RadGrid1.Rebind();
            }
        }
        else if (e.CommandName.Equals("permission"))
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/permissions.aspx?type=calendar&id=" + e.CommandArgument.ToString(), listing_div, null);
        }
    }

    protected void OpenAccessLinqDataSource1_Updating(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceUpdateEventArgs e)
    {
// no other field needs to be updated

    }

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            if (!(e.Item is GridEditFormInsertItem))
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridEditManager manager = item.EditManager;
                GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("CalId") as GridTextBoxColumnEditor;
                SettingName.TextBoxControl.Visible = false;
                SettingName.TextBoxControl.Width = Unit.Pixel(300);


                GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("CalName") as GridTextBoxColumnEditor;
                SettingDescr.TextBoxControl.Width = Unit.Pixel(500);





            }
        }
    }
    protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        GridEditableItem updateitem = e.Item as GridEditableItem;
        string CatID = updateitem.GetDataKeyValue("CalId").ToString();
        CalController.UpdateCategory(int.Parse(CatID), (updateitem["CalName"].Controls[0] as TextBox).Text);
        RadGrid1.Rebind();
    }


}