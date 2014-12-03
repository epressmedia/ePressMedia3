using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPM.Business.Model.UDF;
using EPM.Web.UI;

namespace ePressMedia.Pages
{
    public partial class UDFEntryPanel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    LoadUDFs();
        }

        override protected void OnInit(EventArgs e)
        {
            LoadUDFs();
            base.OnInit(e);
        }

        

        public List<UDFModel.UDFValue> UDFValue
        {
            get;
            set;
        }

        public string ValidationGroup
        {
            get;
            set;
        }

        public int ContentTypeId
        {
            get;
            set;
        }

        public bool ReadOnly
        {
            get;
            set;
        }
        public int NumOfCol
        { get; set; }
        
        public int CategoryId
        { get; set; }

        public int ContentId
        { get; set; }

        private void LoadUDFs()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var assignments = UDFAttachmentContoller.GetUDFAssignmentsByContentType(ContentTypeId, CategoryId);      
            
            if (assignments != null)
            {
                int groupid = assignments.ToList()[0].UDFGroupId;
                udf_group.Attributes.Add("GroupId", groupid.ToString());
                NumOfCol = UDFController.GetNoOfColumns(groupid);
                udf_repeater.DataSource = assignments.OrderBy(c => c.SequenceNo); //info;
                udf_repeater.DataBind();
            }
        }

        protected void udf_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                UDFModel.UDFAssignment assignment = (UDFModel.UDFAssignment)e.Item.DataItem;

                EPM.Web.UI.UDF udf_control = e.Item.FindControl("UDF") as EPM.Web.UI.UDF;
                if (NumOfCol > 0)
                    udf_control.Width = Unit.Percentage((100/NumOfCol)-2);
                string stored_value = UDFController.GetUDFValue(assignment.UDFId, ContentId, ContentTypeId);
                if (UDFController.IsLineBreak(assignment.UDFId))
                {
                    HtmlControl div_linebreak = e.Item.FindControl("div_linebreak") as HtmlControl;
                    div_linebreak.Visible = true;
                    udf_control.Visible = false;
                }
                else
                {
                    if (udf_control != null)
                    {
                        udf_control.Label = (assignment.UDFInfo.Label) ?? "";
                        udf_control.UDFID = assignment.UDFId;
                        udf_control.Required = assignment.RequiredFg;
                        udf_control.ReadOnly = ReadOnly;
                        udf_control.Value = string.IsNullOrEmpty(stored_value) ? (assignment.DefaultValue ?? "") : stored_value;
                        udf_control.ValidationGroup = ValidationGroup;
                        udf_control.PostLabel = UDFController.GetUDFByUDFID(assignment.UDFId).PostfixLabel;
                        udf_control.PreLabel = UDFController.GetUDFByUDFID(assignment.UDFId).PrefixLabel;
                    }
                }
            }
        }

        public List<UDFModel.UDFValue> GetValues()
        {
            List<UDFModel.UDFValue> values = new List<UDFModel.UDFValue>();

            Repeater repeater = this.FindControl("udf_repeater") as Repeater;
            if (repeater != null)
            {
                for (int iCounter = 0; iCounter < repeater.Items.Count; iCounter++)
                {
                    EPM.Web.UI.UDF udf = (EPM.Web.UI.UDF)repeater.Items[iCounter].FindControl("UDF");
                    if (udf.Visible)
                    {
                        UDFModel.UDFValue value = new UDFModel.UDFValue();
                        value.ContentTypeID = ContentTypeId;
                        // SrcID = ContentId
                        value.SrcID = ContentId;
                        value.UDFID = udf.UDFID;
                        value.Value = udf.Text;
                        values.Add(value);
                    }
                }
            }
            return values;
            
        }
        /// <summary>
        /// Created to process the udf records with overridding the content id
        /// </summary>
        /// <param name="content_id"></param>
        /// <returns></returns>
        public List<UDFModel.UDFValue> GetValues(int content_id)
        {
            List<UDFModel.UDFValue> values = new List<UDFModel.UDFValue>();

            Repeater repeater = this.FindControl("udf_repeater") as Repeater;
            if (repeater != null)
            {
                for (int iCounter = 0; iCounter < repeater.Items.Count; iCounter++)
                {
                    EPM.Web.UI.UDF udf = (EPM.Web.UI.UDF)repeater.Items[iCounter].FindControl("UDF");
                    if (udf.Visible)
                    {
                        UDFModel.UDFValue value = new UDFModel.UDFValue();
                        value.ContentTypeID = ContentTypeId;
                        value.SrcID = content_id;
                        value.UDFID = udf.UDFID;
                        value.Value = udf.Text;
                        values.Add(value);
                    }
                }
            }
            return values;

        }

    }
}