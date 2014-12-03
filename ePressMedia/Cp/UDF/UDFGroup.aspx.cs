using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.UDF;
using EPM.Data.Model;

namespace ePressMedia.Cp.UDF
{
    public partial class UDFGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                LoadGroups();

        }

        public void reset()
        {
            rpt_members.DataSource = null;
            rpt_members.DataBind();
            btn_memberadd.Visible = false;
            udf_group_div.Visible = false;
            udf_assignment_div.Visible = false;
            

        }

        public void LoadGroups()
        {
            reset();
            rpt_group.DataSource =  UDFGroupController.GetAllUDFGroups();
            rpt_group.DataBind();

        }
        public void GetMemebers(int UDFGroupId)
        {

            rpt_members.DataSource = UDFController.GetUDFAssignmentsByGroupID(UDFGroupId);
            rpt_members.DataBind();

        }

        protected void rpt_group_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.RadListViewItemType.DataItem || e.Item.ItemType == Telerik.Web.UI.RadListViewItemType.AlternatingItem)
            {
                Telerik.Web.UI.RadListViewDataItem currentItem = (Telerik.Web.UI.RadListViewDataItem)e.Item;
                UDFModel.UDFGroup group = (UDFModel.UDFGroup)currentItem.DataItem;
                Button lbl_groupname = ((Button)e.Item.FindControl("lbl_groupname"));
                lbl_groupname.Text = group.UDFGroupName;
                lbl_groupname.CommandArgument = group.UDFGroupId.ToString();
                btn_update_group.Visible = true;
                
            }
        }

        protected void btn_groupadd_Click(object sender, EventArgs e)
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/UDF/AddUDFGroup.aspx", listing_div, "OnClientclose");

        }



        protected void btn_memberadd_Click(object sender, EventArgs e)
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/UDF/AddUDFMember.aspx?GroupID=" + lbl_GroupID.Value.ToString(), listing_div, "RefreshMemberList");
        }

        protected void lbl_groupname_Click(object sender, EventArgs e)
        {
            lbl_groupname.Text = "UDF Group: " + ((Button)sender).Text;
            udf_group_div.Visible = true;
            btn_memberadd.Visible = true;
            int groupid = int.Parse(((Button)sender).CommandArgument);
            UDFModel.UDFGroup group = UDFGroupController.GetUDFGroupByID(groupid);

            txt_group_description.Text = group.UDFGroupDescription;
            txt_noofcolumns.Text = group.NoOfColumns.ToString();
            lbl_GroupID.Value = groupid.ToString();
            GetMemebers(groupid);

            if (rpt_members.Items.Count == 0)
                btn_delete_group.Visible = true;
            else
                btn_delete_group.Visible = false;
        }

        protected void lbl_member_Click(object sender, EventArgs e)
        {
            lbl_udfname.Text = "UDF Name: " + ((Button)sender).Text;
            udf_assignment_div.Visible = true;
            GetUDFAssignmentInfo(int.Parse(((Button)sender).CommandArgument));
        }

        public void GetUDFAssignmentInfo(int UDFAssignmentID)
        {
            UDFModel.UDFAssignment udf = EPM.Business.Model.UDF.UDFController.GetUDFAssignmentInfoByID(UDFAssignmentID);
            txt_display_order.Text = udf.SequenceNo.ToString();
            txt_default_value.Text = udf.DefaultValue;
            chk_active.Checked = udf.ActiveFg;
            chk_required.Checked = udf.RequiredFg;
            chk_search.Checked = udf.SearchFg;
            lbl_assignment_id.Value = udf.UDFAssignmentId.ToString();
        }

        protected void btn_update_group_Click(object sender, EventArgs e)
        {
            EPM.Business.Model.UDF.UDFGroupController.UpdateUDFGroup(int.Parse(lbl_GroupID.Value), txt_group_description.Text, Guid.Empty, int.Parse(txt_noofcolumns.Text));
        }

        protected void btn_delete_group_Click(object sender, EventArgs e)
        {
            EPM.Business.Model.UDF.UDFGroupController.DeleteUDFGroup(int.Parse(lbl_GroupID.Value));
            LoadGroups();
        }

        protected void btn_update_assignment_Click(object sender, EventArgs e)
        {
            EPM.Business.Model.UDF.UDFController.UpdateUDFAssignment(int.Parse(lbl_assignment_id.Value), txt_default_value.Text, chk_required.Checked, chk_active.Checked, chk_search.Checked, int.Parse(txt_display_order.Text), Guid.Empty);
        }

        protected void btn_remove_assignment_Click(object sender, EventArgs e)
        {
            EPM.Business.Model.UDF.UDFController.DeleteUDFAssignment(int.Parse(lbl_assignment_id.Value.ToString()));
            //udf_assignment_div.Visible = false;
            //rpt_member.DataBind();
        }

        protected void rpt_members_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.RadListViewItemType.DataItem || e.Item.ItemType == Telerik.Web.UI.RadListViewItemType.AlternatingItem)
            {
                Telerik.Web.UI.RadListViewDataItem currentItem = (Telerik.Web.UI.RadListViewDataItem)e.Item;
                UDFModel.UDFAssignment udfs = (UDFModel.UDFAssignment)currentItem.DataItem;
                Button lbl_member = ((Button)e.Item.FindControl("lbl_member"));
                UDFModel.UDFInfo udfinfo = UDFController.GetUDFInfoByUDFID(udfs.UDFId);
                lbl_member.Text = udfinfo.UDFName;
                lbl_member.CommandArgument = udfs.UDFAssignmentId.ToString();
                RadAjaxManager1.AjaxSettings.AddAjaxSetting(lbl_member,Panel2,RadAjaxLoadingPanel1);
            }
        }

        protected void rpt_members_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "RebindListView")
                GetMemebers(int.Parse(lbl_GroupID.Value));
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "close")
            {
                GetMemebers(int.Parse(lbl_GroupID.Value));
            }
        }

        protected void RadAjaxManager1_AjaxSettingCreated(object sender, Telerik.Web.UI.AjaxSettingCreatedEventArgs e)
        {
            //if (e.Updated.ClientID == Panel2.ClientID)
            //    e.UpdatePanel.ChildrenAsTriggers = false;
        }
    }
}