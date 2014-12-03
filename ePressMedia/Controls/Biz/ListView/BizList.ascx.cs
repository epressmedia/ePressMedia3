using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;
using EPM.Business.Model.Common;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Biz.ListView
{
    public partial class BizList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                bindBizCat();
                bindState();
                FitPager1.CurrentPage = 1;

            }

            if (GetQueryStringData())
            {
                bindBiz();
            }
            else
            {
                bindRecentBiz();
            }


        }

        private bool GetQueryStringData()
        {
            bool filtered = false;
            if (Request.QueryString["st"] != null)
            {
                State = Request.QueryString["st"].ToString();
                cbo_province.SelectedIndex = cbo_province.Items.IndexOf(cbo_province.Items.FindByValue(State.ToUpper()));
                filtered = true;
            }
            if (Request.QueryString["area"] != null)
            {
                City = Request.QueryString["area"].ToString();
                txtCity.Text = City;
                filtered = true;
            }
            if (Request.QueryString["cat"] != null)
            {
                Category = Request.QueryString["cat"].ToString();
                BizCatList.SelectedIndex = BizCatList.Items.IndexOf(BizCatList.Items.FindByValue(Category.ToString()));
                filtered = true;
            }
            if (Request.QueryString["q"] != null)
            {
                BizName = Request.QueryString["q"].ToString();
                SearchName.Text = BizName;
                filtered = true;
            }
            return filtered;

        }

        void bindRecentBiz()
        {
            PageTitle.Text = "* 신규등록된 업소입니다. 업소명을 클릭하시면 자세한 정보를 볼 수 있습니다.";

            List<BizModel.BusinessEntity> biz = BEController.GetALLActiveBEs().Where(c=>c.ApprovedFg == true).OrderByDescending(c => c.BusinessEntityId).Take(5).ToList();// BizDAL.SelectBiz("Suspended=0 AND closed=0 AND Approved=1", "BizId DESC", 1, 5);
            BizRepeater.DataSource = biz;
            BizRepeater.DataBind();

            FitPager1.Visible = false;
        }





        void bindBizCat()
        {
            BizCatList.DataSource = BECategoryController.GetAllBusinessCatgories();// BizCategory.SelectBizCategories("1=1");
            BizCatList.DataValueField = "CategoryId";
            BizCatList.DataTextField = "CategoryName";
            BizCatList.DataBind();

            BizCatList.Items.Insert(0, new ListItem("-- All --", "0"));
                
        }

        void bindState()
        {
            List<GeoModel.Ref_province> provinces = EPM.Business.Model.Common.GeoController.GetAllProvinces();

            cbo_province.DataValueField = "Province_cd";
            cbo_province.DataTextField = "Province_name";
            cbo_province.DataSource = provinces;
            cbo_province.DataBind();

            cbo_province.Items.Insert(0, new ListItem("-- All -- ", "All"));


        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string s = SearchName.Text.Trim();

            StringBuilder sb = new StringBuilder("?q=");
            sb.Append(Server.UrlEncode(s));

            if (BizCatList.SelectedIndex > 0)
                sb.Append("&cat=" + BizCatList.SelectedValue);

            if (cbo_province.SelectedIndex > 0)
                sb.Append("&st=" + cbo_province.SelectedValue);
            if (txtCity.Text.Length > 0)
            {


                //List<GeoModel.Ref_city> cities = KNN.Core.Common.GeoUtility.ValidateProvinceCity(cbo_province.SelectedValue.ToString(), txtCity.Text.Trim());


                if (txtCity.Text.Trim().Length > 0)// (cities.Count > 0)
                {
                    sb.Append("&area=" + txtCity.Text.Trim());//cities[0].City_id.ToString());
                }

                //NameValueCollection nv_areaid = Geo.ValidateProvinceCity(cbo_province.SelectedValue, txtCity.Text);
                //if (nv_areaid.Count > 0)
                //    sb.Append("&areaid=" + nv_areaid[0].ToString());
            }
            //if (AreaList.SelectedIndex > 0)
            //    sb.Append("&areaid=" + AreaList.SelectedValue);

            Response.Redirect(sb.ToString());


            //Response.Redirect(string.Format("Default.aspx?q={0}&st={1}&area={2}&cat={3}", 
            //    Server.UrlEncode(s), StateList.SelectedValue, AreaList.SelectedValue, BizCatList.SelectedValue));
        }





        protected void BizRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BizModel.BusinessEntity b = e.Item.DataItem as BizModel.BusinessEntity;

                HyperLink l = e.Item.FindControl("BizLink1") as HyperLink;
                l.NavigateUrl = "/Biz/ViewBiz.aspx?id=" + b.BusinessEntityId;
                if (!string.IsNullOrEmpty(Category))
                    l.NavigateUrl += "&cat=" + Category;
                if (!string.IsNullOrEmpty(BizName))
                    l.NavigateUrl += "&q=" + Server.UrlEncode(BizName);
                if (!string.IsNullOrEmpty(State))
                    l.NavigateUrl += "&st=" + State;
                if (!string.IsNullOrEmpty(City))
                    l.NavigateUrl += "&area=" + City;
                l.NavigateUrl += "&page=" + FitPager1.CurrentPage.ToString();


                if (b.SecondaryName != string.Empty)
                {
                    HyperLink lnk = e.Item.FindControl("BizLink2") as HyperLink;
                    lnk.NavigateUrl = l.NavigateUrl;
                }

                /*
                if (!string.IsNullOrEmpty(CurCat.Value))
                    l.NavigateUrl = "Default.aspx?id=" + b.BizId + "&cat=" + CurCat.Value;
                else if (!string.IsNullOrEmpty(SearchVal.Value))
                    l.NavigateUrl = string.Format("Default.aspx?id={0}&q={1}&st={2}&area={3}",
                        b.BizId, Server.UrlEncode(SearchVal.Value), CurState.Value, CurArea.Value);
                */

                
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                Image i = e.Item.FindControl("BizImg") as Image;
                string processThumbType = "";
                if (string.IsNullOrEmpty(ThumbnailTypeString))
                    processThumbType = context.ThumbnailTypes.Single(c=>c.DefaultFg == true).ThumbnailTypeName;
                else
                    processThumbType = ThumbnailTypeString;

                i.ImageUrl = BEThumbnailController.GetBizThumbnailPath(b.BusinessEntityId, processThumbType);
                if (i.ImageUrl == "")
                    i.ImageUrl = "~/Img/biz_noThumb.png";

                e.Item.FindControl("MovIcon").Visible = !string.IsNullOrEmpty(b.VideoURL);
                e.Item.FindControl("PicIcon").Visible = BEThumbnailController.BizThumbnailExists(b.BusinessEntityId);// !string.IsNullOrEmpty(b.Thumbnail);


                e.Item.FindControl("premium_ad_box").Visible = b.PremiumListing;

                if (e.Item.ItemType == ListItemType.Item)
                    ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("biz_pnl")).Attributes["class"] = ((b.PremiumListing) ? "biz premium_ad_div" : "biz");
                else if (e.Item.ItemType == ListItemType.AlternatingItem)
                    ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("biz_pnl")).Attributes["class"] = ((b.PremiumListing) ? "bizAlt premium_ad_div" : "bizAlt");
                
            }
        }
        protected void PageNumber_Changed(object sender, EventArgs e)
        {
            //bindBizPage(FitPager1.CurrentPage);
            bindBiz();
        }
        void bindBiz()
        {
            int pageNum;
            if (FitPager1.CurrentPage > 1)
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

            

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var biz = BEController.GetALLActiveBEs().Where(c => c.ApprovedFg == true);

            if (!string.IsNullOrEmpty(Category))
            {                
                biz = biz.Where(c => c.CategoryID == int.Parse(category));
            }
            if (!string.IsNullOrEmpty(State))
                biz = biz.Where(c => c.State.ToLower() == State.ToLower());
            if (!string.IsNullOrEmpty(City))
                biz = biz.Where(c => c.City.ToLower() == City.ToLower());
            if (!string.IsNullOrEmpty(BizName))
                biz = biz.Where(c => c.PrimaryName.Contains(BizName) || c.SecondaryName.Contains(BizName));

            int total = biz.Count();


            biz = biz.OrderBy(c => c.PrimaryName).Skip((pageNum - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);

            FitPager1.Visible = (total > 0);
            FitPager1.TotalRows = total;
            FitPager1.CurrentPage = pageNum;


            if ((!string.IsNullOrEmpty(Category)) && (string.IsNullOrEmpty(BizName)) && (pageNum == 1))
            {
                PremiumRepeater.Visible = true;
                Random random = new Random();
                PremiumRepeater.DataSource = BEController.GetActiveBEsByCatID(int.Parse(Category)).Where(x => x.PremiumListing == true).OrderBy(x => x.PrimaryName);
                PremiumRepeater.DataBind();
            }
            else
            {
                PremiumRepeater.DataSource = null;
                PremiumRepeater.DataBind();
                PremiumRepeater.Visible = false;
            }


            BizRepeater.DataSource = biz;// BizDAL.SelectBiz(getFilter(), "BizName", page, FitPager1.RowsPerPage);
            BizRepeater.DataBind();

            if (pageNum == 1)
            {
                //AdOwnerBizRepeater.DataSource = null;// BizDAL.SelectAdOwnerBiz(getFilter());
                //AdOwnerBizRepeater.DataBind();
                //AdOwnerBizRepeater.Visible = true;
            }
            else
            {
                //AdOwnerBizRepeater.Visible = false;
            }
        }

        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        private string bizName;
        public string BizName
        {
            get { return bizName; }
            set { bizName = value; }
        }

        private Dictionary<string, bool> thumbnailType = EPM.Business.Model.Common.ThumbnailTypes.GetThumbnailTypesDictionary();
        [Category("EPMProperty"), Description("Specify Thumbnail Type"), DataType("ThumbnailType")]
        public Dictionary<string, bool> ThumbnailType
        {
            get { return thumbnailType; }
            set { thumbnailType = value; }
        }

        public string ThumbnailTypeString
        {
            get { return ThumbnailType.Single(c => c.Value == true).Key.ToString(); }
        }

    }
}