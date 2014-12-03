using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using EPM.Legacy.Security;


public partial class Calendar_EditEvent : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserControl uc =
            (UserControl)Page.LoadControl("~/Calendar/Controls/EditEvent.ascx");
        ContentPlaceHolder cph = this.Master.FindControl("Content") as ContentPlaceHolder;
        cph.Controls.Add(uc);
    }



}