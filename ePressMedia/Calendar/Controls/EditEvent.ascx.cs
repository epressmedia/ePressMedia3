using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Calendar;


using EPM.Legacy.Security;


namespace ePressMedia.Calendar.Controls
{
    public partial class EditEvent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int evtId = int.Parse(Request.QueryString["eid"]);
                    int calId = int.Parse(Request.QueryString["p"]);

                    if (!AccessControl.AuthorizeUser(Page.User.Identity.Name,
                        ResourceType.Calendar, calId, Permission.Modify))
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    bindData(evtId);

                    //Master.SetSideMenu(4, calId, "행사수정");

                    ListLink.NavigateUrl = (Request.UrlReferrer == null) ? "#" :
                                        Request.UrlReferrer.ToString();
                }
                catch
                {
                    PostLink.Visible = false;

                }
            }
        }

        void bindData(int id)
        {

            

            CalendarModel.Event ev = CalController.GetEventByID(id);

            

            if (ev == null)
            {
                PostLink.Visible = false;
                return;
            }

            EventTitle.Text = ev.Title;
            StartDate.SelectedDate = ev.StartDate;
            EndDate.SelectedDate = ev.EndDate;
            HourList.SelectedValue = ev.EventHour.ToString("00");
            MinuteList.SelectedValue = ev.EventMinute.ToString("00");

            HeldAt.Text = ev.HeldAt;
            Address.Text = ev.Address;
            Host.Text = ev.Host;
            Contact.Text = ev.Contact;
            SiteUrl.Text = ev.Url;
            CkEditor.Text = ev.Descr;
        }

        protected void PostLink_Click(object sender, EventArgs e)
        {

            try
            {
                int eid = int.Parse(Request.QueryString["eid"]);
                if (CalController.GetEventByID(eid).Password.Trim() == Password.Text.Trim())
                {
                    CalController.UpdateEvent(eid, Address.Text, int.Parse(Request.QueryString["p"]), Contact.Text, EPM.Core.ForumUtility.GetCleanText(CkEditor.Text),
                        EndDate.SelectedDate.Value, StartDate.SelectedDate.Value, EventTitle.Text, SiteUrl.Text, HeldAt.Text, byte.Parse(HourList.SelectedValue),
                        byte.Parse(MinuteList.SelectedValue), Host.Text, "", Request.UserHostAddress);

                    EPM.Legacy.Common.Utility.RegisterJsResultAlert(this.Page, true, "저장되었습니다", "", ListLink.NavigateUrl);
                }
                else
                    EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "비밀번호가 맞지 않습니다");

                
            }
            catch
            {
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "비밀번호가 맞지 않습니다");
            }

        }
    }
}