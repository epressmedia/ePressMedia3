using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;

using Telerik.Web.UI;

public partial class Cp_Biz_BizList : System.Web.UI.Page
{
    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {

            LoadData();
            ResetToolbar();
                
        }
        toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
    }


    void LoadData()
    {
        LoadFilteredData(true);
    }

    void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        string action = e.Item.Text.ToLower();

        if (action == "add")
            AddButtonEvent();
        else if (action == "move")
            MoveButtonEvent();
        else if (action == "edit")
            EditButtonEvent();
        else if (action == "delete")
            DeleteButtonEvent();
        else if (action == "cancel")
            CancelButtonEvent();
        else if (action == "approve")
        {
            int counter = RadGrid1.SelectedItems.Count;

            for (int i = 0; i < counter; i++)
            {
                GridDataItem item = RadGrid1.SelectedItems[i] as GridDataItem;
                BEController.ApproveBE(int.Parse(item["BusinessEntityId"].Text));
            }
            RadGrid1.Rebind();
        }


        //ResetToolbar();
    }

    protected void btn_Move_Click(object sender, EventArgs e)
    {

        ResetToolbar();
    }

    void ResetToolbar()
    {
        toolbox1.EnableButtons("add", true);
    }
    void AddButtonEvent()
    {
        Response.Redirect("AddBiz.aspx");
    }
    void MoveButtonEvent()
    {
        ddl_CategoryNames.DataSource = BECategoryController.GetAllBusinessCatgories();
        ddl_CategoryNames.DataBind();
        ddl_CategoryNames.Items.Add(new ListItem("-- Select --", ""));
        toolbox1.EnableButtons("cancel", true);
        MovePanel.Visible = true;
        
    }
    void EditButtonEvent()
    {
        if (RadGrid1.SelectedItems.Count == 1)
        {
            GridDataItem item = RadGrid1.SelectedItems[0] as GridDataItem;
            Response.Redirect("BizInfo.aspx?id=" + item["BusinessEntityId"].Text);
        }
    }
    void DeleteButtonEvent()
    {
        int counter = RadGrid1.SelectedItems.Count;

            for (int i = 0; i < counter; i++)
            {
                GridDataItem item = RadGrid1.SelectedItems[i] as GridDataItem;
                BEController.DeleteBE(int.Parse(item["BusinessEntityId"].Text));
            }
            RadGrid1.Rebind();
            ResetToolbar();
    }
    void CancelButtonEvent()
    {
        ResetToolbar();
        MovePanel.Visible = false;
    }


    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        if (RadGrid1.SelectedItems.Count > 0)
        {
            if (RadGrid1.SelectedItems.Count == 1)
            {
                toolbox1.EnableButtons("edit", false);
                
            }
            toolbox1.EnableButtons("move,delete,approve", false);

        }
        else
        {
            toolbox1.DisableButton("move");
            toolbox1.DisableButton("edit");
            toolbox1.DisableButton("approve");
            toolbox1.DisableButton("delete");
        }
    }
    protected void ToggleSelectedState(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (sender as CheckBox);
        foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        {
            (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
            dataItem.Selected = headerCheckBox.Checked;
        }
    }

    protected void FilterCombo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        Session["BizCatName"] = e.Value;
        LoadFilteredData(true);
    }

    void LoadFilteredData(bool databind)
    {
        IQueryable<BizModel.BusinessEntity> bes = BEController.GetALLActiveBEs();
        
        if (Session["BizCatName"] != null)
        {
            if ((Session["BizCatName"].ToString().Length > 0) && (Session["BizCatName"].ToString() != "0"))
                bes = bes.Where(c => c.BusienssCategory.CategoryName == Session["BizCatName"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["ApprovedFg"] != null)
            {

                string value = Request.QueryString["ApprovedFg"].ToString();
                GridColumn rowStateColumn = RadGrid1.MasterTableView.GetColumnSafe("ApprovedFg");
                rowStateColumn.CurrentFilterFunction = GridKnownFunction.EqualTo;
                rowStateColumn.CurrentFilterValue = value.Length == 0 ? "false" : value;
                bes = bes.Where(c => c.ApprovedFg == bool.Parse(value));
            }
            if (Request.QueryString["AdOwner"] != null)
            {

                string value = Request.QueryString["AdOwner"].ToString();
                GridColumn rowStateColumn = RadGrid1.MasterTableView.GetColumnSafe("AdOwner");
                rowStateColumn.CurrentFilterFunction = GridKnownFunction.EqualTo;
                rowStateColumn.CurrentFilterValue = value.Length == 0 ? "false" : value;
                bes = bes.Where(c => c.AdOwner == bool.Parse(value));
            }
            if (Request.QueryString["PandingChange"] != null)
            {

                string value = Request.QueryString["PandingChange"].ToString();
                GridColumn rowStateColumn = RadGrid1.MasterTableView.GetColumnSafe("PendingChange");
                rowStateColumn.CurrentFilterFunction = GridKnownFunction.EqualTo;
                rowStateColumn.CurrentFilterValue = value.Length == 0 ? "false" : value;
                bes = bes.Where(c => c.BusinessEntityRequests.Count(x=>x.ReviewDate == null && x.Status == 'N')> 0);
            }
        }      

        Label1.Text = RadGrid1.MasterTableView.FilterExpression;
        RadGrid1.DataSource = bes.OrderByDescending(c => c.BusinessEntityId);
        if (databind)
            RadGrid1.DataBind();

        
    }

    protected void FilterCombo_DataBound(object sender, EventArgs e)
    {
        RadComboBox combo = ((RadComboBox)sender);
        combo.Items.Insert(0, new RadComboBoxItem("-- All --", "0"));
        if ((Session["BizCatName"] == null) ||  (Session["BizCatName"].ToString().Length == 0))
        {
            Session["BizCatName"] = "0";
        }
        combo.SelectedIndex = combo.Items.FindItemIndexByValue(Session["BizCatName"].ToString());

    }



    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        LoadFilteredData(false);
    }

    protected void btn_preset_Click(object sender, EventArgs e)
    {
        Button item = (Button)sender;
        string type = item.Text;
        switch (type)
        {
            case "Show Un-Approved":
                Response.Redirect("/cp/Biz/?ApprovedFg=false");
                break;
            case "Show Advertiser":
                Response.Redirect("/cp/Biz/?AdOwner=true");
                break;
            case "Show Pending Changes":
                Response.Redirect("/cp/Biz/?PandingChange=true");
                break;
            default:
                Response.Redirect("/cp/Biz");
                break;
        }
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox chk = (CheckBox)item["PendingChange"].Controls[0];
            string value = item.GetDataKeyValue("BusinessEntityId").ToString();

            if (BERequestController.GetPendingBusinessEntityRequestsByBEID(int.Parse(value)).Count() > 0)
                chk.Checked = true;
            else
                chk.Checked = false;
        }
    }



  

     
}