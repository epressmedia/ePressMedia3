using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Legacy.Security;
using EPM.Business.Model.Calendar;



public partial class Calendar_WeeklyCalendar : EPM.Core.StaticPageRender
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Calendar1.VisibleDate = DateTime.Today;

                int calId = int.Parse(Request.QueryString["p"]);

                AccessControl ac = AccessControl.SelectAccessControlByUserName(
                                Page.User.Identity.Name, ResourceType.Calendar, calId);
                if (ac == null)
                    ac = new AccessControl(Permission.None);

                if (!ac.CanList)
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                //PostLink.NavigateUrl = "PostEvent.aspx?p=" + calId;
                //PostLink.Visible = ac.CanWrite;

                //ModLink.Visible = ac.CanModify;
                //DelButton.Visible = ac.CanDelete;

                int ContentTypeId = context.ContentTypes.Single(c => c.ContentTypeName == "Calendar").ContentTypeId;

                //Master.SetSideMenu(ContentTypeId, calId, null);

            }
            catch
            { }
        }
    }
}