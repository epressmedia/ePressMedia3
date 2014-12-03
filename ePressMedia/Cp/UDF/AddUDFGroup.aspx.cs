using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Cp.UDF
{
    public partial class AddUDFGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txt_noofcolumn.Text = "1";
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
             int groupid = EPM.Business.Model.UDF.UDFGroupController.AddUDFGroup(txt_description.Text, txt_groupname.Text,Guid.Empty, int.Parse(txt_noofcolumn.Text));
             closeWindow();
        }
        private void closeWindow()
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "mykey", "CloseDataEntry();", true);
        }


    }
}