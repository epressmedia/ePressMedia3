using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

//using Knn.Article;


public partial class Calendar_MonthlyCalendar : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserControl uc =
            (UserControl)Page.LoadControl("~/Controls/Calendar/FullCalendarView.ascx");
        ContentPlaceHolder cph = this.Master.FindControl("Content") as ContentPlaceHolder;
        cph.Controls.Add(uc);
    }

}