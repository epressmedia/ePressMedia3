using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EPM.Business.Model.Biz;

public partial class Cp_Biz_BizUnapproved : System.Web.UI.Page
{
    static string filterFormat = " Name LIKE N'%{0}%' ";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = SearchButton.UniqueID;
        SearchValue.Focus();
        if (!IsPostBack)
        {
            int pn = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["pn"]))
            {
                try { pn = int.Parse(Request.QueryString["pn"]); }
                catch { }
            }

            bindData(pn);
        }
    }

    void bindData(int pageNum)
    {
        int cnt = BEController.GetALLActiveBEs().Count();// BizDAL.GetBizCount(getFilterExpr());
        FitPager1.TotalRows = cnt;
        FitPager1.CurrentPage = pageNum;
        FitPager1.Visible = true;

        bindPage();
    }

    void bindPage()
    {
        string sortExpr = String.Format("{0} {1}", SortValue.Value, SortOrder.Value);

        List<BizModel.BusinessEntity> biz = null;// BizDAL.SelectBiz(getFilterExpr(), sortExpr, FitPager1.CurrentPage, FitPager1.RowsPerPage);

        DataView.DataSource = biz;
        DataView.DataBind();
    }

    string getFilterExpr()
    {
        string filterExpr = "Approved=0";
        if (SearchValue.Text.Trim() != string.Empty)
            filterExpr = string.Format(filterFormat, SearchValue.Text.Trim());

        return filterExpr;
    }

    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = DataView.Controls[0].FindControl("CheckAll") as CheckBox;
        bool isChecked = chk.Checked;

        foreach (RepeaterItem item in DataView.Items)
        {
            (item.FindControl("ItemCheck") as CheckBox).Checked = isChecked;
        }
    }

    protected void ApproveButton_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in DataView.Items)
        {
            if ((item.FindControl("ItemCheck") as CheckBox).Checked)
            {
                int id = Int32.Parse((item.FindControl("BizId") as Label).Text);

                BERequestController.ApproveBusinessEntityRequest(id, Guid.Empty);
                //BizDAL.ApprovePost(id);
            }
        }

        bindData(FitPager1.CurrentPage);
    }

    protected void PageNumberChanged(object sender, EventArgs e)
    {
        bindPage();
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        bindData(1);
    }
    protected void DataView_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        SortValue.Value = e.CommandName;
        SortOrder.Value = e.CommandArgument.ToString();

        bindPage();
    }
    protected void DataView_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            LinkButton l = e.Item.FindControl("Sort" + SortValue.Value) as LinkButton;
            l.CommandArgument = SortOrder.Value.Equals("DESC") ? "ASC" : "DESC";
            l.Text += SortOrder.Value.Equals("DESC") ? " ▲" : " ▼";
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
        }
    }
}