using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;

using EPM.Legacy.Data;


    public partial class Mobile_Common_MobileBizList1 : System.Web.UI.UserControl
    {
        int row_per_page;
        protected void Page_Load(object sender, EventArgs e)
        {
            row_per_page = 10;
            int catId = int.Parse(Request.QueryString["catId"].ToString());
            CatId.Value = catId.ToString();
            if (Request.QueryString["page"] == null)
                PageNumber.Value = "1";
            else
                PageNumber.Value = Request.QueryString["page"].ToString();


            bindBiz(int.Parse(PageNumber.Value.ToString()), catId);
        }

        void bindBiz(int page, int catId)
        {
            
            setMaxPage(BEController.GetActiveBEsByCatID(catId).Count());
            

            bindBizPage(page, catId);
        }

        void bindBizPage(int page, int catId)
        {
            List<BizModel.BusinessEntity> biz = BEController.GetActiveBEsByCatID(catId).Skip((page - 1) * row_per_page).Take(row_per_page).ToList();// BizDAL.SelectSimpleBiz(getFilter(catId), page, row_per_page);


            BizRepeater.DataSource = biz;
            BizRepeater.DataBind();
        }

        //DataFilterCollection getFilter(int catId)
        //{
            

        //    DataFilterCollection filters = new DataFilterCollection();
        //    filters.AddFilter(new SuspendedFilter(false));
        //    filters.AddFilter(new CatagoryFilter(catId.ToString()));


        //    return filters;
        //}


        //Missing in KNN DLL -- This should be added to KNN.BizAd.BizFilter when versioning happens
        public class SuspendedFilter : DataBooleanFilter
        {
            public SuspendedFilter(bool isTrue) : base(isTrue) { }

            public override string ToFilterString()
            {
                return "Suspended=" + (IsTrue ? "1" : "0");
            }
        }

        //Missing in KNN DLL -- This should be added to KNN.BizAd.BizFilter when versioning happens
        public class CatagoryFilter : DataValueFilter
        {
            public CatagoryFilter(string value) : base(value) { }

            public override string ToFilterString()
            {
                if (SelectedValue.Trim().Equals(string.Empty))
                    return string.Empty;
                else
                    return "Biz.CatId=" + SelectedValue;

            }
        }

        void setMaxPage(int cnt)
        {
            MaxPageNumber.Value = ((cnt / row_per_page) + 1).ToString();
        }

    }
