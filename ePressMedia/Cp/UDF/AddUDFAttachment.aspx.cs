using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Cp.UDF
{
    public partial class AddUDFAttachment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadUDFGroups();
        }
        public void LoadUDFGroups()
        {

            grid_UDFGroups.DataSource = EPM.Business.Model.UDF.UDFGroupController.GetUDFGroupsNotInUseByContractType(int.Parse(Request.QueryString["ContentTypeId"].ToString()), int.Parse(Request.QueryString["CategoryId"].ToString()));
            grid_UDFGroups.DataBind();
        }
        protected void btn_add_Click(object sender, EventArgs e)
        {
            //int GroupID = int.Parse(Request.QueryString["GroupID"].ToString());
            //EPM.Business.Model.UDF.UDFController.AddUDFAssignment(GroupID, int.Parse(ddl_udf.SelectedItem.Value), txt_default_value.Text, chk_required.Checked, chk_active.Checked, chk_search.Checked, int.Parse(txt_display_order.Text), Guid.Empty);
            //closeWindow();    

        }

        private void closeWindow()
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "mykey", "CloseDataEntry();", true);
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            closeWindow();
        }


        protected void btn_select_Click(object sender, EventArgs e)
        {
            if (grid_UDFGroups.SelectedItems.Count > 0)
            {
                foreach (Telerik.Web.UI.GridDataItem item in grid_UDFGroups.MasterTableView.Items)
                {
                    if (item.Selected == true)
                    {
                        string ID = item["UDFGroupId"].Text;

                        EPM.Business.Model.UDF.UDFAttachmentContoller.AttachUDFGroup(int.Parse(Request.QueryString["ContentTypeId"].ToString()), int.Parse(Request.QueryString["CategoryId"].ToString()), int.Parse(ID));
                    }
                }
            }
            closeWindow();
        }
    }
}