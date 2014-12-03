using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Form
{
    public partial class FormEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FormId"] != null)
                FormId = int.Parse(Request.QueryString["FormId"].ToString());
            LoadTitle();

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
                LoadEmailEvent();
                AddPanel.Visible = true;
            }
            else if (action == "cancel")
            {
                reset_AddPanel();
            }

        }

        public void LoadEmailEvent()
        {
            dll_emailevent.DataSource = EPM.Business.Model.Admin.EmailTemplateController.GetAllEmailEvents();
            dll_emailevent.DataTextField = "EmailEventName";
            dll_emailevent.DataValueField = "EmailEventId";
            dll_emailevent.DataBind();

            dll_emailevent.Items.Insert(0, new ListItem("-- Select -- ", "0"));
            
        }

        void reset_AddPanel()
        {
            AddPanel.Visible = false;
            dll_emailevent.DataSource = null;
            dll_emailevent.DataBind();
            txt_recipients.Text = "";

            toolbox1.EnableButtons("Add", true);
        }


        public static int FormId
        { get; set; }

        public void LoadTitle()
        {

            CatName.Text = EPM.Business.Model.Form.FormController.GetFormById(FormId).FormName;
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if ((dll_emailevent.SelectedIndex > 0) && ((!chk_selectfromudf.Checked && !string.IsNullOrEmpty(txt_recipients.Text) || (chk_selectfromudf.Checked))))
            {
                EPM.Business.Model.Form.FormEmailController.AddFormEmail(int.Parse(dll_emailevent.SelectedItem.Value.ToString()), (manual_email.Visible?txt_recipients.Text:""),(udf_email.Visible?int.Parse(ddl_udfs.SelectedItem.Value):0), FormId);
                Email_List.DataBind();
            }
        }

        protected void Email_List_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                reset_AddPanel();
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;

                    GridEditFormItem itemcontrol = (GridEditFormItem)e.Item;
                    DropDownList ddl = (DropDownList)itemcontrol.FindControl("dll_emailevent");


                    ddl.DataSource = EPM.Business.Model.Admin.EmailTemplateController.GetAllEmailEvents();
                    ddl.DataBind();

                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(item["EmailEvent"].Text));

                    CheckBox chk_edit_selectfromudf = (CheckBox)(itemcontrol.FindControl("chk_edit_selectfromudf"));
                    TextBox txt_edit_recipients = ((TextBox)(itemcontrol.FindControl("txt_edit_recipients")));
                    DropDownList ddl_edit_udfs = (DropDownList)(itemcontrol.FindControl("ddl_edit_udfs"));
                    HtmlControl html_edit_udf_email = (HtmlControl)(itemcontrol.FindControl("edit_udf_email"));
                    HtmlControl html_edit_manual_email = (HtmlControl)(itemcontrol.FindControl("edit_manual_email"));
         

                    if (!string.IsNullOrEmpty(item["Receipients"].Text.Replace("&nbsp;","")))
                    {
                        txt_edit_recipients.Text = item["Receipients"].Text.Replace("&nbsp;","");
                        chk_edit_selectfromudf.Checked = false;

                        ddl_edit_udfs.DataSource = null;
                        ddl_edit_udfs.DataBind();

                        html_edit_udf_email.Visible = false;
                        html_edit_manual_email.Visible = true;

                    }
                    else
                    {
                        chk_edit_selectfromudf.Checked = true;
                        html_edit_udf_email.Visible = true;
                        html_edit_manual_email.Visible = false;
             
                            EPM.Data.Model.EPMEntityModel _context = new EPM.Data.Model.EPMEntityModel();
                            int ContentTypeId = _context.ContentTypes.Where(x => x.ContentTypeName.ToLower() == "form").Single().ContentTypeId;
                            int GroupId = EPM.Business.Model.UDF.UDFGroupController.GetUDFGroupsByContentType(ContentTypeId, FormId).Single().UDFGroupId;


                            ddl_edit_udfs.DataSource = EPM.Business.Model.UDF.UDFController.GetUDFsByGroupID(GroupId).Where(x => x.UDFDataType.DataTypeDescr == "Text");
                            ddl_edit_udfs.DataTextField = "UDFName";
                            ddl_edit_udfs.DataValueField = "UDFId";
                            ddl_edit_udfs.DataBind();

                            ddl_edit_udfs.SelectedIndex = ddl_edit_udfs.Items.IndexOf(ddl_edit_udfs.Items.FindByText(((Label)item["UDFName"].Controls[0].FindControl("lbl_UDFName")).Text));
                            
                        

                    }

                }
            }


        }

        protected void Email_List_UpdateCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                if (e.Item is GridEditableItem)
                {
                    GridEditableItem updateitem = e.Item as GridEditableItem;
                    GridEditFormItem itemcontrol = (GridEditFormItem)e.Item;
                    string FormEmailId = updateitem.GetDataKeyValue("FormEmailId").ToString();

                    if (!string.IsNullOrEmpty(FormEmailId))
                    {
                        CheckBox chk = (CheckBox)(itemcontrol.FindControl("chk_edit_selectfromudf"));

                        if (chk.Checked)
                        {
                            EPM.Business.Model.Form.FormEmailController.EditFormEmail(int.Parse(FormEmailId),
                           int.Parse(((DropDownList)(itemcontrol.FindControl("dll_emailevent"))).SelectedItem.Value),
                           "",
                           int.Parse(((DropDownList)(itemcontrol.FindControl("ddl_edit_udfs"))).SelectedItem.Value),
                           FormId);
                        }
                        else
                        {
                            EPM.Business.Model.Form.FormEmailController.EditFormEmail(int.Parse(FormEmailId),
                                int.Parse(((DropDownList)(itemcontrol.FindControl("dll_emailevent"))).SelectedItem.Value),
                                ((TextBox)(itemcontrol.FindControl("txt_edit_recipients"))).Text,
                                0,
                                FormId);
                        }
                    }
                }
            }

        }

        protected void Email_List_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
               

                LinkButton button = dataItem["DeleteColumn"].Controls[0] as LinkButton;
                button.Attributes["onclick"] = "return confirm('Are you sure you want to delete this record?')";




                Label lbl = (Label)dataItem["UDFName"].Controls[0].FindControl("lbl_UDFName");
                if (lbl != null)
                {
                    int EmailUDFInfoId = ((FormModel.FormEmail)e.Item.DataItem).EmailUDFInfoId.HasValue?((FormModel.FormEmail)e.Item.DataItem).EmailUDFInfoId.Value:0;
                    if (EmailUDFInfoId>0)
                        lbl.Text = EPM.Business.Model.UDF.UDFController.GetUDFInfoByUDFID(EmailUDFInfoId).UDFName;
                }
                
            }
        }

        protected void Email_List_DeleteCommand(object sender, GridCommandEventArgs e)
        {

            string FormEmailId = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FormEmailId"].ToString();
            if (!string.IsNullOrEmpty(FormEmailId))
            {
                EPM.Business.Model.Form.FormEmailController.DeleteFormEmail(int.Parse(FormEmailId));
            }
        }

        protected void chk_selectfromudf_CheckedChanged(object sender, EventArgs e)
        {
            if (udf_email.Visible)
            {
                udf_email.Visible = false;
                manual_email.Visible = true;

            }
            else
            {
                udf_email.Visible = true;
                manual_email.Visible = false;
            }

            if (udf_email.Visible)
            {
                EPM.Data.Model.EPMEntityModel _context = new EPM.Data.Model.EPMEntityModel();
                int ContentTypeId = _context.ContentTypes.Where(x => x.ContentTypeName.ToLower() == "form").Single().ContentTypeId;
                if (EPM.Business.Model.UDF.UDFGroupController.GetUDFGroupsByContentType(ContentTypeId, FormId).Count() > 0)
                {
                    int GroupId = EPM.Business.Model.UDF.UDFGroupController.GetUDFGroupsByContentType(ContentTypeId, FormId).Single().UDFGroupId;
                    ddl_udfs.DataSource = EPM.Business.Model.UDF.UDFController.GetUDFsByGroupID(GroupId).Where(x => x.UDFDataType.DataTypeDescr == "Text");
                    ddl_udfs.DataTextField = "UDFName";
                    ddl_udfs.DataValueField = "UDFId";
                    ddl_udfs.DataBind();
                }
            }
            else
            {
                ddl_udfs.DataSource = null;
                ddl_udfs.DataBind();
            }

            
        }

        protected void chk_edit_selectfromudf_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk_edit_selectfromudf = (CheckBox)sender;
            GridEditableItem item = (GridEditableItem)chk_edit_selectfromudf.NamingContainer;

            HtmlControl html_edit_udf_email = (HtmlControl)(item.FindControl("edit_udf_email"));
            HtmlControl html_edit_manual_email = (HtmlControl)(item.FindControl("edit_manual_email"));
         

            html_edit_udf_email.Visible = chk_edit_selectfromudf.Checked;
            html_edit_manual_email.Visible = !chk_edit_selectfromudf.Checked;


        }


    }
}