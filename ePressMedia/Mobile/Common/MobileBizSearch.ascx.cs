using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using EPM.Business.Model.Biz;
using EPM.Legacy.Data;




    public partial class Mobile_Common_MobileBizSearch : System.Web.UI.UserControl
    {
        int row_per_page;
        protected void Page_Load(object sender, EventArgs e)
        {
            row_per_page = 10;
            string qs = Request.QueryString["q"].Trim();
            QueryText.Value = qs;
            if (Request.QueryString["page"] == null)
                PageNumber.Value = "1";
            else
                PageNumber.Value = Request.QueryString["page"].ToString();

            bindBiz(qs);
        }
        void bindBiz(string query)
        {
            //DataFilterCollection filters = new DataFilterCollection();
            //filters.AddFilter(new BizCategoryFilter(BizCategoryFilter.CategoryAll));
            //filters.AddFilter(new BizKeywordFilter(query));


            var biz = BEController.GetALLActiveBEs().Where(c => c.PrimaryName.Contains(QueryText.Value) || c.SecondaryName.Contains(QueryText.Value));

           

            int cnt = biz.Count();// BizDAL.GetBizCount(filters);
           // BizLink.Visible = (cnt > 0);
            BizCount.Text = "검색결과(" +cnt.ToString()+") - \""+QueryText.Value+"\"";
            BizRepeater.DataSource = biz.Skip((int.Parse(PageNumber.Value) - 1) * row_per_page).Take(row_per_page);// BizDAL.SelectSimpleBiz(filters, int.Parse(PageNumber.Value), row_per_page);
            BizRepeater.DataBind();

            setMaxPage(cnt);
        }


        void setMaxPage(int cnt)
        {
            MaxPageNumber.Value = ((cnt / row_per_page) + 1).ToString();
        }

        protected void BizRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                BizModel.BusinessEntity be = e.Item.DataItem as BizModel.BusinessEntity;
                Literal CatName = e.Item.FindControl("CatName") as Literal;
                Literal Name = e.Item.FindControl("Name") as Literal;
                Literal ShortDescr = e.Item.FindControl("ShortDesc") as Literal;
                Literal Phone1 = e.Item.FindControl("BizPhone1") as Literal;
                CatName.Text = be.BusienssCategory.CategoryName;
                Name.Text = be.PrimaryName;
                ShortDescr.Text = be.ShortDesc;
                Phone1.Text = be.Phone1;

            }
        }

    }
