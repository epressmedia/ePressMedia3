using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using log4net;
using System.Linq;


namespace ePressMedia.Cp.UDF
{
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {

                toolbox1.EnableButtons("Add", true);

                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

                LoadDataType();


            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
		}

        private void LoadDataType()
        {
            ddl_datatype.DataSource = EPM.Business.Model.UDF.UDFController.GetAllDataTypes();
            ddl_datatype.DataValueField = "DataTypeId";
            ddl_datatype.DataTextField = "DataTypeDescr";
            ddl_datatype.DataBind();
        }

        void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();


            if (action == "add")
            {
                toolbox1.EnableButtons("Cancel", true);
                AddPanel.Visible = true;
            }
            else if (action == "cancel")
            {
                reset_AddPanel();
            }

        }

        void reset_AddPanel()
        {
            AddPanel.Visible = false;
           // ArticleName.Text = "";
            //rdb_normal.Checked = true;

            toolbox1.EnableButtons("Add", true);
        }


        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName.Equals("mod"))
            {
                RadGrid1.Items[int.Parse(e.CommandArgument.ToString())].Edit = true;
                RadGrid1.Rebind();

            }
            else if (e.CommandName.Equals("config"))
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/UDF/UDFReference.aspx?UDFID=" + e.CommandArgument.ToString(), listing_div, null,0,400);
            }
            else if (e.CommandArgument.Equals("delete"))
            {
                if (EPM.Business.Model.UDF.UDFController.GetUDFGroupsByUDFID(int.Parse(e.CommandArgument.ToString())).Count() == 0)
                {
                    EPM.Business.Model.UDF.UDFController.DeleteUDF(int.Parse(e.CommandArgument.ToString()));
                    RadGrid1.Rebind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete UDF because it is being used in an UDF group.');", true);
                }
            }

        }


        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    GridEditManager manager = item.EditManager;
                    GridTextBoxColumnEditor UDFID = manager.GetColumnEditor("UDFID") as GridTextBoxColumnEditor;
                    UDFID.TextBoxControl.Enabled = false;
                    //UDFID.TextBoxControl.Width = Unit.Pixel(800);

                    GridTextBoxColumnEditor DataTypeName = manager.GetColumnEditor("DataTypeName") as GridTextBoxColumnEditor;
                    DataTypeName.TextBoxControl.Enabled = false;

                    GridTextBoxColumnEditor UDFName = manager.GetColumnEditor("UDFName") as GridTextBoxColumnEditor;
                    UDFName.TextBoxControl.Enabled = false;

                    GridTextBoxColumnEditor UDFDescription = manager.GetColumnEditor("UDFDescription") as GridTextBoxColumnEditor;
                    UDFDescription.TextBoxControl.Enabled = false;

                    GridCheckBoxColumnEditor ReferenceFg = manager.GetColumnEditor("ReferenceFg") as GridCheckBoxColumnEditor;
                    ReferenceFg.CheckBoxControl.Enabled = false;

                    //GridTextBoxColumnEditor DataTypeName = manager.GetColumnEditor("DataTypeName") as GridTextBoxColumnEditor;
                    //if (EPM.Business.Model.UDF.UDFController.GetUDFGroupsByUDFID(int.Parse(UDFID.Text)).Count() > 0)
                    //    DataTypeName.TextBoxControl.Visible = false;

                   

                    
                }
            }

        }

        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updateitem = e.Item as GridEditableItem;
            string UDFID = updateitem.GetDataKeyValue("UDFID").ToString();



            EPM.Business.Model.UDF.UDFController.UpdateUDF(int.Parse(UDFID),
                (updateitem["UDFName"].Controls[0] as TextBox).Text,
                (updateitem["UDFDescription"].Controls[0] as TextBox).Text,
                (updateitem["ReferenceFg"].Controls[0] as CheckBox).Checked,
                Guid.Empty,
                (updateitem["PrefixLabel"].Controls[0] as TextBox).Text,
                (updateitem["PostfixLabel"].Controls[0] as TextBox).Text,
                (updateitem["Label"].Controls[0] as TextBox).Text);

        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            EPM.Business.Model.UDF.UDFController.AddUDF(int.Parse(ddl_datatype.SelectedItem.Value), txt_udfname.Text, txt_descr.Text,((ddl_datatype.SelectedItem.Text.ToLower() == "listitem")?true:false), Guid.Empty, txt_prefix.Text, txt_postfix.Text, txt_Label.Text);
            reset_AddPanel();

            RadGrid1.Rebind();
        }


	}
}