using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Cp.UDF
{
    public partial class AddUDFMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadUDF();
        }

        private void LoadUDF()
        {
            if (Request.QueryString["GroupID"] != null)
            {
            int GroupID= int.Parse(Request.QueryString["GroupID"].ToString());
            ddl_udf.DataSource = EPM.Business.Model.UDF.UDFGroupController.GetUDFsNotInGroup(GroupID);
            ddl_udf.DataValueField = "UDFID";
            ddl_udf.DataTextField = "UDFName";
            ddl_udf.DataBind();

            if (ddl_udf.Items.Count > 0)
                btn_add.Visible = true;
            
        }}

        protected void btn_add_Click(object sender, EventArgs e)
        {
            int GroupID= int.Parse(Request.QueryString["GroupID"].ToString());
            EPM.Business.Model.UDF.UDFController.AddUDFAssignment(GroupID, int.Parse(ddl_udf.SelectedItem.Value), txt_default_value.Text, chk_required.Checked, chk_active.Checked, chk_search.Checked, int.Parse(txt_display_order.Text), Guid.Empty);
            closeWindow();
        }

        private void closeWindow()
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "mykey", "CloseDataEntry();", true);
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            closeWindow();
        }
    }
}