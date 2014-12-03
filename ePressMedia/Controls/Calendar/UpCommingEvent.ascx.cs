using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.HtmlControls;



namespace ePressMedia.Controls.Calendar
{
    public partial class UpCommingEvent : System.Web.UI.UserControl
    {

        private int numderOfEvent = 2;
        [Category("EPMProperty"), Description("Number of events to display"), DefaultValue(typeof(System.Int32), "2"), Required(ErrorMessage = "Number of Events is required.")]
        public int NumberOfEvent
        {
            get { return numderOfEvent; }
            set { numderOfEvent = value; }
        }

        private string calendarIDs;
        [Category("EPMProperty"), Description("Calendar IDs (use comma as an identifier)"), Required(ErrorMessage = "Calendar ID is required")]
        public string CalendarIDs
        {
            get { return calendarIDs; }
            set { calendarIDs = value; }
        }

        private string headerName;
        [Category("EPMProperty"), Description("Name displayed at the top of the control"), Required(ErrorMessage = "HeaderName is required")]
        public string HeaderName
        {
            get;
            set;
        }

        private string moreLink;
        [Category("EPMProperty"), Description("(Optional)URL of the page which will be open when the More button is clicked.")]
        public string MoreLink
        {
            get;
            set;
        }

        private string moreLinkType = "_self";
        [Category("EPMProperty"), Description("(Optional)More link open type"), DefaultValue(typeof(System.String), "_self")]
        public string MoreLinkType
        {
            get;
            set;
        }

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(CalendarIDs))
                    return;



                if (HeaderName.EndsWith(".jpg") || HeaderName.EndsWith(".png") || HeaderName.EndsWith(".bmp") || HeaderName.EndsWith(".gif"))
                {
                    img_title.Visible = true;
                    img_title.Src = HeaderName;
                }
                else
                {
                    lbl_title.Visible = true;
                    lbl_title.Text = HeaderName;
                }


                LoadCalendar();
            }

        }

        void LoadCalendar()
        {
            var calIDs = calendarIDs.Split(',').ToList();

            var cal = ((from c in context.Events
                       where calIDs.Contains(c.Calendar1.CalId.ToString()) && c.StartDate >= DateTime.Now
                       //orderby c.StartDate descending
                       select new {
                           CalID = c.Calendar1.CalId,
                           EventID = c.EventId,
                           Title = c.Title,
                           Description = EPM.Core.WebHelper.StripTagsCharArray(c.Descr),
                           StartDate = c.StartDate}
                           ).Union(from c in context.Events
                                           where calIDs.Contains(c.Calendar1.CalId.ToString()) && c.StartDate < DateTime.Now && c.EndDate>= DateTime.Now
                      // orderby c.StartDate descending
                                   select new
                                   {
                                       CalID = c.Calendar1.CalId,
                                       EventID = c.EventId,
                                       Title = c.Title,
                                       Description = EPM.Core.WebHelper.StripTagsCharArray(c.Descr),
                                       StartDate = DateTime.Today
                                   }
                           )).OrderBy(c => c.StartDate).Take(numderOfEvent);

            UpCommingEvent_Repeater.DataSource = cal;
            UpCommingEvent_Repeater.DataBind();
        }


        protected void UpCommingEvent_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item ||
                    e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    //CalendarModel.Event cal_event = (CalendarModel.Event)e.Item.DataItem;

                    //HtmlAnchor anchor = (HtmlAnchor)e.Item.FindControl("cal_link");
                    //anchor.HRef = "~/Calendar/MonthlyCalendar.aspx?p=" + cal_event.Calendar1.CalId.ToString() + "&&eid=" + cal_event.EventId.ToString();
                }
            }
            catch
            {
            }
        }
    }
}