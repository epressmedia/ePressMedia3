using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Legacy.Security;
using EPM.Business.Model.Calendar;

public partial class Cp_Calendar_CalendarAccessControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id;
            try { id = int.Parse(Request.QueryString["id"]); }
            catch { return; }

            AccessControl1.ResourceType = (int)ResourceType.Calendar;
            AccessControl1.ResourceId = id;

            CalName.Text = CalController.GetCalendarName(id);// KCalendar.Calendar.GetCalendarName(id);
        }
    }
}