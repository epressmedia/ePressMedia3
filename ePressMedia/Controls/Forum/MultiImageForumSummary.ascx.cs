using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Forum
{
    [Description("Forum Contents Summary w/ Images)")]
    public partial class MultiImageForumSummary : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ForumLoad(ContentIDs);
                lbl_header.Text = HeaderName;
            }
        }


        void ForumLoad(string CategoryIDs)
        {
            var contIDs = ContentIDs.Split(',').ToList();

            IQueryable<ForumModel.ForumThread> forums;
            forums = (from c in context.ForumThreads
                            where contIDs.Contains(c.ForumId.ToString())&& c.Suspended == false && c.PostDate < DateTime.Now && c.Thumb.Trim().Length > 0
                            select c);



            if (ContentItemOrder == ContentSortBy.ViewInDays)
                forums = forums.Where(c => c.PostDate >= DateTime.Now.AddDays(numOfDaysForItemOrder*-1)).OrderByDescending(c => c.ViewCount).Take(NumOfItems);
            else // Default Post Date
                forums = forums.OrderByDescending(c => c.PostDate).Take(NumOfItems);


            if (forums.Count() == 0)
                MultiImageForumSummary_Container.Visible = false;
            else
            {
                ReportSetRep.DataSource = forums;
                ReportSetRep.DataBind();
            }


        }


        protected void ReportSetRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ForumModel.ForumThread forum = e.Item.DataItem as ForumModel.ForumThread;

                var lnk = e.Item.FindControl("ViewLink") as HyperLink;
                lnk.Text = forum.Subject;
                lnk.NavigateUrl = "~/Forum/view.aspx?p=" + forum.ForumId.ToString() + "&tid=" + forum.ThreadId.ToString();

                (e.Item.FindControl("Thumb") as Image).ImageUrl = forum.Thumb;

            }
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

        private int numOfItems = 10;
        [Category("EPMProperty"), Description("Number of items to be listed in each section"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Number of items is required")]
        public int NumOfItems
        {
            get;
            set;
        }

        private string contentIDs;
        [Category("EPMProperty"), Description("Content IDs to be linked to the control"), Required(ErrorMessage = "At least one content ID is required.")]
        public string ContentIDs
        {
            get;
            set;
        }


        private ContentSortBy contentItemOrder = ContentSortBy.PostDate;
        [Category("EPMProperty"), Description("PostDate = By Post Date / ViewInDays = By Number of Views in the defined number of days."), Required(ErrorMessage = "ContentItemOrder is required.")]
        public ContentSortBy ContentItemOrder
        {
            get;
            set;
        }

        private int numOfDaysForItemOrder = 7;
        [Category("EPMProperty"), Description("Number of days that will be used to sort the content items."), DefaultValue(typeof(System.Int32), "7"), Required(ErrorMessage = "ContentItemOrder is required.")]
        public int NumOfDaysForItemOrder
        {
            get;
            set;
        }

        public enum ContentSortBy
        {
            PostDate = 0,
            ViewInDays = 1
        }

    }
}