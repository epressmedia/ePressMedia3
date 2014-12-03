using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using EPM.Business.Model.UDF;

namespace ePressMedia.Cp.UDF
{
    public partial class UDFReference : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                toolbox1.EnableButtons("Add", true);

                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

                


            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
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
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["UDFID"] != null)
            {
                UDFReferenceController.AddUDFReference(txt_displayvalue.Text, txt_internalvalue.Text, int.Parse(Request.QueryString["UDFID"].ToString()), dp_start_date.SelectedDate, dp_end_date.SelectedDate);
                RadGrid1.DataBind();
                reset_AddPanel();
            }
        }

        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                //if (!(e.Item is GridEditFormInsertItem))
                //{
                //    GridEditableItem item = e.Item as GridEditableItem;
                //    GridEditManager manager = item.EditManager;
                //    GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("ArtCatID") as GridTextBoxColumnEditor;
                //    SettingName.TextBoxControl.Enabled = false;
                //    SettingName.TextBoxControl.Width = Unit.Pixel(800);


                //    GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("CatName") as GridTextBoxColumnEditor;
                //    SettingDescr.TextBoxControl.Width = Unit.Pixel(800);

                //    GridCheckBoxColumnEditor SettingLink = manager.GetColumnEditor("LinkArticle_fg") as GridCheckBoxColumnEditor;
                //    SettingLink.CheckBoxControl.Enabled = false;

                //    GridCheckBoxColumnEditor SettingVirtual = manager.GetColumnEditor("VirtualCat_fg") as GridCheckBoxColumnEditor;
                //    SettingVirtual.CheckBoxControl.Enabled = false;

                //}
            }
        }

        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updateitem = e.Item as GridEditableItem;
            string ReferenceID = updateitem.GetDataKeyValue("ReferenceID").ToString();

            UDFReferenceController.UpdateUDFReference(int.Parse(ReferenceID), 
                (updateitem["DisplayValue"].Controls[0] as TextBox).Text,
                (updateitem["InternalValue"].Controls[0] as TextBox).Text, 
                (updateitem["EffrectiveDate"].Controls[0] as RadDatePicker).SelectedDate, 
                (updateitem["TerminateDate"].Controls[0] as RadDatePicker).SelectedDate);

        }

    }
}