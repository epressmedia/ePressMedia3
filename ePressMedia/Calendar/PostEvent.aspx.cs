using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using EPM.Legacy.Security;

public partial class Calendar_PostEvent : EPM.Core.StaticPageRender
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserControl uc =
      (UserControl)Page.LoadControl("~/Calendar/Controls/PostEvent.ascx");
        ContentPlaceHolder cph = this.Master.FindControl("Content") as ContentPlaceHolder;
        cph.Controls.Add(uc);
    }
}