using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPM.Core;
using EPM.Core.Admin;
using EPM.Core.Classified;
using EPM.Legacy.Data;
using EPM.Legacy.Security;
using EPM.Data.Model;
using EPM.Business.Model.Classified;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;

namespace ePressMedia.Classified.Controls
{
    [Description("Classified Listing Control")]
    public partial class AdList : System.Web.UI.UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AdList));
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                try
                {

                    bool override_CatId = false;
                    int cat;
                    if (Request.QueryString["p"] != null)
                    {
                        cat = int.Parse(Request.QueryString["p"]);
                    }
                    else
                    {
                        cat = CategoryID;
                    }


                    if (CategoryID > 0)
                    {
                        cat = CategoryID;
                        override_CatId = true;
                    }
                    CatId.Value = cat.ToString();

                    if (!ShowPager)
                        FitPager1.Visible = false;

                    if (!ShowSearch)
                        classified_search_panel.Visible = false;


                    int i = Request.Url.Query.IndexOf("&aid");
                    Params.Value = string.Format("~/Classified/view.aspx?p={0}&q={1}",
                        CatId.Value.ToString(), Request.QueryString["q"]);


                    FitPager1.RowsPerPage = (RowPerPage > 0) ? RowPerPage : 10;// catSettings.RowsPerPage;// cfg.RowsPerPage;

                    bindData();


                    btn_post2.Visible= btn_post.Visible = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                        ResourceType.Classified, cat, Permission.Write);
                    if (override_CatId)
                        btn_post2.Visible = btn_post.Visible = false;

                    
                    string path = "/Page/DataEntry.aspx?p=" + cat + "&area=classified&mode=Add";

                    string catName = context.ClassifiedCategories.SingleOrDefault(c => c.CatId == cat).CatName;
                    EntryPopup.Title = "New Ad - " + catName;
                    btn_post.OnClientClick = EntryPopup.GetOpenPath(path);
                    btn_post2.OnClientClick = EntryPopup.GetOpenPath(path);

                }
                catch (Exception ex)
                {
                    btn_post2.Visible = btn_post.Visible = false;
                    log.Error(ex.Message);
                }


            }
        }

        public void HideSearchPanel()
        {
            classified_search_panel.Visible = false;
        }

        void bindCondition()
        {
            int cat = int.Parse(Request.QueryString["p"]);

            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                SearchValue.Text = Request.QueryString["q"];

            if (!string.IsNullOrEmpty(Request.QueryString["aid"]))
                CurAdId.Value = Request.QueryString["aid"];
        }

        void bindData()
        {

            log.Info("Load Classified List");
            try
            {
                bindCondition();

                int pageNum;
                int cat = int.Parse(CatId.Value);


                if (FitPager1.CurrentPage > 0)
                {
                    pageNum = FitPager1.CurrentPage;
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.QueryString["page"]))
                        pageNum = 1;
                    else
                        pageNum = int.Parse(Request.QueryString["page"]);
                }

                EPMEntityModel context = new EPMEntityModel();



                IQueryable<ClassifiedModel.ClassifiedAd> classifiedQuery = ClassifiedController.GetClassifiedAdsByCategoryId(cat,true);

                // Search filtering when querystring is filled.
                bool search_query = false;
                if (Request.QueryString["q"] != null)
                    search_query = true;

                if (search_query)
                {
                    string query_str = Request.QueryString["q"].ToString();
                    if (query_str.Length > 0)
                        classifiedQuery = classifiedQuery.Where(c => c.Subject.Contains(query_str) || c.Description.Contains(query_str) || c.Phone.Contains(query_str));
                }

                // Sorting for Ads
                if (ShowAdRows)
                {
                    classifiedQuery = classifiedQuery.OrderByDescending(c => c.Announce).ThenByDescending(c => c.Premium).ThenByDescending(c => c.LastUpdate);
                }
                else
                {
                    classifiedQuery = classifiedQuery.OrderByDescending(c => c.Announce).ThenByDescending(c => c.LastUpdate);
                }



                FitPager1.TotalRows = classifiedQuery.Count();
                FitPager1.CurrentPage = pageNum;

                if (ShowPager)
                    FitPager1.Visible = (FitPager1.TotalRows > 0);

                var newresult = classifiedQuery.Skip((pageNum - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);

                if (ViewType == ViewTypes.Table)
                {
                    AdRepeater.DataSource = newresult.ToList();
                    AdRepeater.DataBind();
                }
                else
                {
                    AdRepeater_Simple.DataSource = newresult.ToList();
                    AdRepeater_Simple.DataBind();
                }

               
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }




        protected void SearchButton_Click(object sender, EventArgs e)
        {


            int catId = int.Parse(Request.QueryString["p"]);
            if (CategoryID > 0)
            {
                catId = CategoryID;
            }
            Response.Redirect(string.Format("~/Classified/list.aspx?p={0}&q={1}", catId,Server.UrlEncode(SearchValue.Text.Trim())));


        }

        protected void PageNumber_Changed(object sender, EventArgs e)
        {
            //bindPage(getFilter());

            bindData();
        }

        protected void AdRepeater_Simple_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
        e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ClassifiedModel.ClassifiedAd a = e.Item.DataItem as ClassifiedModel.ClassifiedAd;

                HyperLink s = e.Item.FindControl("ViewLink") as HyperLink;
                s.NavigateUrl = Params.Value + "&page=" + FitPager1.CurrentPage + "&aid=" + a.AdId;
                s.Text = a.Subject;
                if (CurAdId.Value == a.AdId.ToString())
                    s.CssClass = "sel";

                HyperLink d = e.Item.FindControl("ViewLink2") as HyperLink;
                d.NavigateUrl = Params.Value + "&page=" + FitPager1.CurrentPage + "&aid=" + a.AdId;

                string description_str = EPM.Core.WebHelper.StripTagsCharArray(a.Description);
                d.Text = EPM.Legacy.Common.Utility.TruncateStringByWord(description_str, NumOfDescriptionChar);

                Image img = e.Item.FindControl("Thumb") as Image;
                if (ShowImage)
                {
                    if (!string.IsNullOrEmpty(a.Thumb))
                        img.ImageUrl = a.Thumb;
                    else
                        img.Visible = false;
                }
                else
                {
                    img.Visible = false;
                }
            }
        }
        protected void AdRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            

            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ClassifiedModel.ClassifiedAd a = e.Item.DataItem as ClassifiedModel.ClassifiedAd;

                HyperLink l = e.Item.FindControl("ViewLink") as HyperLink;
                l.NavigateUrl = Params.Value + "&page=" + FitPager1.CurrentPage + "&aid=" + a.AdId;

                if (a.AdId == int.Parse(CurAdId.Value))
                    l.CssClass = "sel";

                Image img = e.Item.FindControl("Thumb") as Image;
                if (ShowImage)
                {
                    if(!string.IsNullOrEmpty(a.Thumb))
                        img.ImageUrl =  a.Thumb;
                    else
                        img.Visible = false;
                }
                else
                    img.Visible = false;

                Label lbl = e.Item.FindControl("CommCnt") as Label;

                int comment_counter = a.ClassifiedComments.Count(c => c.Blocked == false);
                lbl.Text = "[" + comment_counter.ToString() + "]";
                lbl.Visible = comment_counter > 0;


                Label SubTitle = e.Item.FindControl("SubTitle") as Label;
                string description_str = EPM.Core.WebHelper.StripTagsCharArray(a.Description);
                SubTitle.Text = EPM.Legacy.Common.Utility.TruncateStringByWord(description_str, NumOfDescriptionChar); //description_str.Length > 90 ? description_str.Substring(0, 90) : description_str;

                HtmlImage imgnew = e.Item.FindControl("imgnew") as HtmlImage;
                int hours = int.Parse(EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Number of Hours to show New in Classified"));
                if (a.LastUpdate >= DateTime.Now.AddHours(hours*-1))
                    imgnew.Visible = true;

                Label RegDate = e.Item.FindControl("RegDate") as Label;
                // XXX Hours/Minutes Ago format is no longer displayed
                //if (a.LastUpdate >= DateTime.Now.AddHours(-23))
                //{
                //    int hour = DateTime.Now.Hour - a.LastUpdate.Hour;
                //    if (hour < 1)
                //    {
                //        int minute = DateTime.Now.Minute - a.LastUpdate.Minute;
                //        if (minute == 0)
                //            minute = 1;
                //        RegDate.Text = " < " + (minute).ToString() + ((minute == 1) ? " Minute" : " Minutes") + " Ago";
                //    }
                //    else
                //    {

                //        RegDate.Text = " < " + hour.ToString() + ((hour == 1) ? " Hour" : " Hours") + " Ago";
                //    }
                //}
                //else
                    RegDate.Text = a.LastUpdate.ToShortDateString();


            }
        }



        private bool showAdRows = false;
        [Category("EPMProperty"), Description("Show Ad rows at the beginning of listing"), DefaultValue(typeof(System.Boolean), "false"), Required()]
        public bool ShowAdRows
        {
            get { return showAdRows; }
            set { showAdRows = value; }
        }


        private int rowPerPage = 10;
        [Category("EPMProperty"), Description("Define number of articles per page"), DefaultValue(typeof(System.Int32), "10"), Required(ErrorMessage = "Row Per Page is required.")]
        public int RowPerPage
        {
            get { return rowPerPage; }
            set { rowPerPage = value; }
        }

        private int categoryId = 0;
        [Category("EPMProperty"), Description("(Optional) Override Category ID")]
        public int CategoryID
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        private Boolean showPager = true;
        [Category("EPMProperty"), Description("Show Pager"), DefaultValue(typeof(System.Boolean), "true"), Required()]
        public Boolean ShowPager
        {
            get { return showPager; }
            set { showPager = value; }
        }

        private Boolean showSearch = true;
        [Category("EPMProperty"), Description("Show Search"), DefaultValue(typeof(System.Boolean), "true"), Required()]
        public Boolean ShowSearch
        {
            get { return showSearch; }
            set { showSearch = value; }
        }

        private Boolean showImage = false;
        [Category("EPMProperty"), Description("Show Search"), DefaultValue(typeof(System.Boolean), "false"), Required()]
        public Boolean ShowImage
        {
            get { return showImage; }
            set { showImage = value; }
        }

        private int numOfDescriptionChar = 90;
        [Category("EPMProperty"), Description("Number of characters to show in description"), DefaultValue(typeof(System.Int32), "90"), Required()]
        public int NumOfDescriptionChar
        {
            get { return numOfDescriptionChar; }
            set { numOfDescriptionChar = value; }
        }

        private ViewTypes viewType = ViewTypes.Table;
        [Category("EPMProperty"), Description("Specify View Type to be displayed"), DefaultValue(typeof(ViewTypes), "Table"), Required()]
        public ViewTypes ViewType
        {
            get { return viewType; }
            set { viewType = value; }
        }


        public enum ViewTypes
        {
            Table,
            Simple
        }

    }
}