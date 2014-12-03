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
    public partial class PostEvent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int calId = int.Parse(Request.QueryString["p"]);

                    if (Request.QueryString["Day"] != null)
                    {
                        DateTime dt;
                        if (DateTime.TryParse(Request.QueryString["Day"].ToString(), out dt))
                        {
                            StartDate.SelectedDate = dt;
                            EndDate.SelectedDate = dt;
                        }

                    }

                    if (!AccessControl.AuthorizeUser(Page.User.Identity.Name,
                        ResourceType.Calendar, calId, Permission.Write))
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    //Master.SetSideMenu(4, calId, "행사등록");

                    ListLink.NavigateUrl = (Request.UrlReferrer == null) ? "#" : Request.UrlReferrer.ToString();

                }
                catch
                {
                    PostLink.Visible = false;
                }
            }
        }

        protected void PostLink_Click(object sender, EventArgs e)
        {
            int event_id = CalController.AddEvent(Address.Text, int.Parse(Request.QueryString["p"]), Contact.Text, EPM.Core.ForumUtility.GetCleanText(CkEditor.Text), EndDate.SelectedDate.Value, StartDate.SelectedDate.Value,
                 EventTitle.Text, SiteUrl.Text, HeldAt.Text, byte.Parse(HourList.SelectedValue), byte.Parse(MinuteList.SelectedValue), Host.Text, "", Request.UserHostAddress, Password.Text);




            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this.Page, event_id > 0, "등록되었습니다", "등록중 오류가 발생했습니다.",
                                ListLink.NavigateUrl);
        }
    }
}