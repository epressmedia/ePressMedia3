using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Legacy.Security;
using EPM.Business.Model.Calendar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Description("Calendar Monthly View Control")]

    public partial class FullCalendarView : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Calendar1.VisibleDate = DateTime.Today;

                    // Override the calendar category ID entered from the page configuration
                    if (CategoryID == 0)
                    {
                        if (Request.QueryString["p"] == null)
                            CategoryID = 0;
                        else
                        {
                            CategoryID = int.Parse(Request.QueryString["p"].ToString());
                        }
                    }

                    

                    AccessControl ac = AccessControl.SelectAccessControlByUserName(
                                    Page.User.Identity.Name, ResourceType.Calendar, CategoryID);
                    if (ac == null)
                        ac = new AccessControl(Permission.None);

                    if (!ac.CanList)
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    PostLink.NavigateUrl = "/Calendar/PostEvent.aspx?p=" + CategoryID;

                    PostLink.Visible = ac.CanWrite;
                    ModLink.Visible = ac.CanModify;
                    DelButton.Visible = ac.CanDelete;

                

                    int ContentTypeId = context.ContentTypes.Single(c => c.ContentTypeName == "Calendar").ContentTypeId;

                    //Master.SetSideMenu(ContentTypeId, CategoryID, null);

                    if (Request.QueryString["eid"] != null)
                    {


                        CalendarModel.Event evt = context.Events.Single(c => c.EventId == int.Parse(Request.QueryString["eid"]));

                        bindEventInfo(evt);
                    }

                }
                catch
                { }
            }
            else
            {
                if (Request.Form["__EVENTTARGET"].ToString().Equals("Lnk"))
                {
                    int eid = int.Parse(Request.Form["__EVENTARGUMENT"]);


                    CalendarModel.Event evt = context.Events.Single(c => c.EventId == eid);

                    bindEventInfo(evt);
                }
            }
        }

        void bindEventInfo(CalendarModel.Event evt)
        {
            EvtTitle.Text = evt.Title;
            EvtDate.Text = evt.StartDate.ToShortDateString() + " ~ " + evt.EndDate.ToShortDateString();
            EvtTime.Text = evt.EventHour.ToString("00") + ":" + evt.EventMinute.ToString("00");
            HeltAt.Text = evt.HeldAt;
            if (!string.IsNullOrEmpty(evt.Address))
            {
                MapLink.Text = "(주소:" + evt.Address + ")";
                MapLink.NavigateUrl = "http://maps.google.com/?q=loc:" + Server.UrlEncode(evt.Address);
            }

            EvtHost.Text = evt.Host;
            Inquiry.Text = evt.Contact;
            if (!string.IsNullOrEmpty(evt.Url))
                SiteLink.Text = SiteLink.NavigateUrl = "http://" + evt.Url;
            Desc.Text = evt.Descr;

            ModLink.NavigateUrl = string.Format("/Calendar/EditEvent.aspx?p={0}&eid={1}",
                                evt.Calendar1.CalId, evt.EventId);

            CurEvent.Text = evt.EventId.ToString();
            Desc.Text = evt.EventId.ToString();

            eventView.Visible = true;


            if (DelButton.Visible)
            {
                string msg = "행사 등록시 설정한 비밀번호를 입력하세요.";
                string path = "/Page/DataEntry.aspx?cid=" + evt.EventId.ToString() + "&area=calendar&mode=Delete&p=" + evt.Calendar1.CalId.ToString() + "&passwordinput=true&returnURL=" + Server.UrlEncode("/page/calendar.aspx") + "&msg=" + msg;
                DeletePopup.Title = "Delete Event";// +Ad.Subject;
                DeletePopup.Width = 300;
                DeletePopup.Height = 200;
                DelButton.OnClientClick = DeletePopup.GetOpenPath(path);
            }

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            e.Cell.Controls.Clear();
            HyperLink h = new HyperLink();
            h.NavigateUrl = "/Calendar/PostEvent.aspx?p=" + CategoryID.ToString() + "&Day=" + Calendar1.VisibleDate.Year.ToString() + "-" + Calendar1.VisibleDate.Month.ToString() + "-" + e.Day.DayNumberText;
            h.Text = e.Day.DayNumberText;

            e.Cell.Controls.Add(h);

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


                var col = CalController.SelectEventTitles(CategoryID, e.Day.Date).ToList();
                if (col.Count > 0 && e.Day.Date.Month == Calendar1.VisibleDate.Month)
                {
                    //e.Cell.Controls.Add(getEventLinks(col));
                    e.Cell.Controls.Add(getEventLinks(col));
                }
                else
                    e.Cell.Controls.Add(new LiteralControl("<br /><br />&nbsp;"));
            }
            catch { }
        }

        static string navFormat =
            "<li><a href='javascript:__doPostBack(\"Lnk\",\"{0}\")' title='{1}'>{2}</a></li>";
        //LiteralControl getEventLinks(NameValueCollection col)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("<ul>");

        //    foreach (string key in col.AllKeys)
        //        sb.AppendFormat(navFormat, key, col[key], toShortTitle(col[key], 10));

        //    sb.Append("</ul>");

        //    return new LiteralControl(sb.ToString());
        //}

        LiteralControl getEventLinks(List<CalendarModel.Event> events)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<ul>");

            foreach (CalendarModel.Event eve in events)
                sb.AppendFormat(navFormat, eve.EventId, eve.Title, toShortTitle(eve.Title, 10));

            sb.Append("</ul>");

            return new LiteralControl(sb.ToString());
        }

        static string toShortTitle(string title, int maxLength)
        {
            if (title.Length <= maxLength)
                return title;

            return title.Substring(0, maxLength) + "...";
        }


        protected void ListLink_Click(object sender, EventArgs e)
        {
            eventView.Visible = false;
        }

        //protected void DelButton_Click(object sender, EventArgs e)
        //{
        //    ConfirmMpe.Show();
        //}

        //protected void ConfirmButton_Click(object sender, EventArgs e)
        //{
        //    string aa = Desc.Text;
        //    CalendarModel.Event ev = CalController.GetEventByID(int.Parse(CurEvent.Text));
            

        //    if (DelPassword.Text.Equals(ev.Password))
        //    {
        //        CalController.DeleteEvent(ev.EventId);
                
        //    }
        //    else if (AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Calendar,
        //        CategoryID, Permission.FullControl))
        //    {
        //        CalController.DeleteEvent(ev.EventId);
        //    }
        //    else
        //    {
        //        ConfirmMpe.Show();
        //        return;
        //    }

        //    eventView.Visible = false;
        //}

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {

        }

        private static int categoryId =0;
        [Category("EPMProperty"), Description("(Optional) Override Calendar Category ID")]
        public int CategoryID
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
    }
