using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;



public partial class Cp_Biz_BizModUnapproved : System.Web.UI.Page
{
    static string[] letterLabels = { "가", "나", "다", "라", "마", "바", "사", "아", "자", "차", "카", "타", "파", "하" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindData();
        }
    }

    void bindData()
    {
     //   List<BizModRequest> req = BizModRequestDAL.SelectBizModRequest(String.Format("{0} {1}", SortValue.Value, SortOrder.Value));

        List<BizModel.BusinessEntityRequest> req = BERequestController.GetAllBusinessEntityRequests().ToList();// BizModRequestDAL.SelectBizModRequest(String.Format("{0} {1}", SortValue.Value, SortOrder.Value));
        DataView.DataSource = req;
        DataView.DataBind();
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
                int reqId = Int32.Parse((item.FindControl("ReqId") as Label).Text);

                //BizDAL.UpdateBizWithModRequest(reqId);
                //BizModRequestDAL.DeleteModRequest(reqId);
            }
        }

        bindData();
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in DataView.Items)
        {
            if ((item.FindControl("ItemCheck") as CheckBox).Checked)
            {
                int id = Int32.Parse((item.FindControl("ReqId") as Label).Text);
                //BizModRequestDAL.DeleteModRequest(id);
            }
        }

        bindData();

    }

    protected void DataView_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        SortValue.Value = e.CommandName;
        SortOrder.Value = e.CommandArgument.ToString();

        bindData();
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