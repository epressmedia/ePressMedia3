using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using EPM.Business.Model.Calendar;

namespace ePressMedia.Controls.Calendar
{
    public partial class MonthViewCalendar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            bindEventsToDayCell(e);

            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday && e.Day.Date.Month == Calendar1.VisibleDate.Month)
                e.Cell.ForeColor = System.Drawing.Color.Red;
            else if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday && e.Day.Date.Month == Calendar1.VisibleDate.Month)
                e.Cell.ForeColor = System.Drawing.Color.Blue;
        }
        void bindEventsToDayCell(DayRenderEventArgs e)
        {
            try
            {
                var col = CalController.SelectEventTitles(int.Parse(Request.QueryString["p"]), e.Day.Date).ToList();
                //NameValueCollection col = Knn.Calendar.Event.SelectEventTitles(
                //                  int.Parse(Request.QueryString["p"]), e.Day.Date);
                if (col.Count > 0 && e.Day.Date.Month == Calendar1.VisibleDate.Month)
                {
                    e.Cell.BackColor = System.Drawing.Color.Gray;
                }
                 //   e.Cell.Controls.Add(getEventLinks(col));
                else
                    e.Cell.Controls.Add(new LiteralControl("<br /><br />&nbsp;"));
            }
            catch { }
        }
    }
}