using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Globalization;
using AjaxControlToolkit;

namespace EPM.Web.UI
{

    public class UDF : WebControl, INamingContainer
    {
        		#region Private Fields' Region
        private RadTextBox _textbox;
        private Label  _r_textbox;
        private RadTextBox _multiTextbox;
        private Label _r_multiTextbox;
        private RadButton _checkbox;
        private Label _r_checkbox;
        private RadNumericTextBox _intbox;
        private Label _r_intbox;
        private RadNumericTextBox _moneybox;
        private Label _r_moneybox;
        private RadDatePicker _datebox;
        private Label _r_datebox;
        private RadComboBox _listitem;
        private Label _r_listitem;

        		#endregion

        public int SrcID
        {
            get { return (int)(ViewState["SrcID"] ?? 0); }
            set { ViewState["SrcID"] = value; }
        }

        public int UDFID
        {
            get { return (int)(ViewState["UDFID"] ?? 0); }
            set { ViewState["UDFID"] = value; }
        }

        public bool ReadOnly
        {
            get { return (bool)(ViewState["ReadOnly"] ?? false); }
            set { ViewState["ReadOnly"] = value; }
        }


        public string Text
        {
            get
            {
                string value = "";
                switch (FieldType)
                {
                    case FieldTypes.CheckBox:
                        value = this._checkbox.Checked.ToString();
                        break;
                    case FieldTypes.DatePicker:
                        value = this._datebox.SelectedDate.ToString();
                        break;
                    case FieldTypes.ListItem:
                        value = (this._listitem.SelectedIndex > 0) ? this._listitem.SelectedItem.Value.ToString() : "";
                        break;
                    case FieldTypes.Money:
                        value = this._moneybox.Text;
                        break;
                    case FieldTypes.MultilineText:
                        value = this._multiTextbox.Text;
                        break;
                    case FieldTypes.Number:
                        value = this._intbox.Text;
                        break;
                    case FieldTypes.Text:
                        value = this._textbox.Text;
                        break;
                    case FieldTypes.None:
                        value = "";
                        break;

                }
                return value;
            }

        }

        public FieldTypes FieldType
        {

            get { 
                string value =  EPM.Business.Model.UDF.UDFController.GetFieldTypeByUDFId(UDFID);
                return (FieldTypes)Enum.Parse(typeof(FieldTypes), value);
            }
        }

        public string Value
        {
            get {
                string s  = (string)(ViewState["Value"]);
                return (s == null) ? string.Empty : s;
            }
            set { ViewState["Value"] = value; }
        }
        public string Label
        {
            get { return (string)(ViewState["Label"] ?? ""); }
            set { ViewState["Label"] = value; }
        }
        public int MaxLength
        {
            get { return (int)(ViewState["MaxLength"] ?? 0); }
            set { ViewState["MaxLength"] = value; }
        }

        public Boolean Required
        {
            get { return (Boolean)(ViewState["Required"] ?? false); }
            set { ViewState["Required"] = value; }
        }

        public String ValidationGroup
        {

            get { return (String)(ViewState["ValidationGroup"] ?? ""); }
            set { ViewState["ValidationGroup"] = value; }
        }

        public string PostLabel
        {

            get { return (String)(ViewState["PostLabel"] ?? ""); }
            set { ViewState["PostLabel"] = value; }
        }

        public string PreLabel
        {

            get { return (String)(ViewState["PreLabel"] ?? ""); }
            set { ViewState["PreLabel"] = value; }
        }




        public enum FieldTypes
        {
            CheckBox,
            Number,
            Text,
            MultilineText,
            Money,
            DatePicker,
            ListItem,
            LineBreak,
            None
        }
        
   


        protected override void CreateChildControls()
        {
            string ControlID = FieldType.ToString()+"_"+UDFID.ToString();

            if (FieldType != FieldTypes.LineBreak)
            {
                
                if (ReadOnly)
                    this.Controls.Add(new LiteralControl("<div><span>" + Label + ": </span>"));
                else
                    this.Controls.Add(new LiteralControl("<div><span>" + Label + "</span>" + ((Required && !ReadOnly) ? "<span style=\"color:red\">*</span>" : "") + "</div>\r\n<div id=\"" + this.ID + "_" +
                        ControlID + "_Container\" class=\"" + ControlID + "_Container ValidationBox\" "+((FieldType == FieldTypes.MultilineText)?"Style=\"min-width:300px\"":"") +
                       ((FieldType == FieldTypes.CheckBox) ? "Style=\"line-height: 0px\"" : "") + ">"));
            }


            

            if (!string.IsNullOrEmpty(PreLabel))
            {
                this.Controls.Add(new LiteralControl("<span style=\"margin-right:5px\">" + PreLabel + "</span>"));
            }


            switch (FieldType)
            {
                case FieldTypes.CheckBox:
                    Required = false;
                    if (ReadOnly)
                    {
                        _r_checkbox = new Label();
                        _r_checkbox.Text = (Value.ToLower() == "true") ? "Yes" : "No";
                        _r_checkbox.ID = "_r_" + ControlID;
                        _r_checkbox.CssClass = "UDF_ReadValue";
                        this.Controls.Add(_r_checkbox);
                    }
                    else
                    {
                        _checkbox = new RadButton();
                        _checkbox.ToggleType = ButtonToggleType.CheckBox;
                        _checkbox.Checked = (Value.ToLower() == "true");
                        _checkbox.ButtonType = RadButtonType.ToggleButton;
                        _checkbox.ToggleStates.Add(new RadButtonToggleState(Label));
                        _checkbox.AutoPostBack = false;
                        _checkbox.ID = ControlID;
                        this.Controls.Add(_checkbox);
                    }
                    break;
                case FieldTypes.Number:
                    if (ReadOnly)
                    {
                        _r_intbox = new Label();
                        _r_intbox.Text = Value;
                        _r_intbox.ID = "_r_" + ControlID;
                        _r_intbox.CssClass = "UDF_ReadValue";
                        this.Controls.Add(_r_intbox);
                    }
                    else
                    {
                        _intbox = new RadNumericTextBox();
                        _intbox.ShowSpinButtons = false;
                        _intbox.NumberFormat.DecimalDigits = 0;
                        _intbox.Type = NumericType.Number;
                        _intbox.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                        _intbox.ID = ControlID;

                        if (MaxLength > 0)
                            _intbox.MaxLength = MaxLength;
                        if (Value.Length > 0)
                            _intbox.Text = Value;
                        this.Controls.Add(_intbox);
                    }
                    break;
                case FieldTypes.Money:
                    if (ReadOnly)
                    {
                        _r_moneybox = new Label();
                        _r_moneybox.Text = String.IsNullOrEmpty(Value)? String.Empty: Decimal.Parse(Value).ToString("C");
                        _r_moneybox.ID = "_r_" + ControlID;
                        _r_moneybox.CssClass = "UDF_ReadValue";
                        this.Controls.Add(_r_moneybox);
                    }
                    else
                    {
                        _moneybox = new RadNumericTextBox();
                        _moneybox.ShowSpinButtons = false;
                        _moneybox.NumberFormat.DecimalDigits = 2;
                        _moneybox.Type = NumericType.Currency;
                        _moneybox.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                        _moneybox.ID = ControlID;
                        if (Value.Length > 0)
                            _moneybox.Text = Value;
                        this.Controls.Add(_moneybox);
                    }
                    break;
                case FieldTypes.DatePicker:
                    if (ReadOnly)
                    {
                        _r_datebox = new Label();
                        _r_datebox.Text = Value;
                        _r_datebox.ID = "_r_" + ControlID;
                        _r_datebox.CssClass = "UDF_ReadValue";
                        this.Controls.Add(_r_datebox);
                    }
                    else
                    {
                        _datebox = new RadDatePicker();
                        _datebox.DatePopupButton.Visible = false;
                        _datebox.ShowPopupOnFocus = true;
                        _datebox.HideAnimation.Type = CalendarAnimationType.Slide;
                        _datebox.ShowAnimation.Type = CalendarAnimationType.Slide;
                        _datebox.Calendar.FastNavigationSettings.ShowAnimation.Type = CalendarAnimationType.Slide;
                        _datebox.Calendar.FastNavigationSettings.HideAnimation.Type = CalendarAnimationType.Slide;
                        if (Value.Length > 0)
                            _datebox.SelectedDate = Convert.ToDateTime(Value);
                        _datebox.ID = ControlID;
                    }
                    this.Controls.Add(_datebox);

                    break;
                case FieldTypes.MultilineText:
                    if (ReadOnly)
                    {
                        _r_multiTextbox = new Label();
                        _r_multiTextbox.Text = Value;
                        _r_multiTextbox.ID = "_r_" + ControlID;
                        _r_multiTextbox.CssClass = "UDF_ReadValue";
                        _r_multiTextbox.Style.Add("min-width", "300px");
                        this.Controls.Add(_r_multiTextbox);
                    }
                    else
                    {
                        _multiTextbox = new RadTextBox();
                        _multiTextbox.Rows = 5;
                        _multiTextbox.TextMode = InputMode.MultiLine;
                        if (MaxLength > 0)
                            _multiTextbox.MaxLength = MaxLength;
                        if (Value.Length > 0)
                            _multiTextbox.Text = Value;
                        _multiTextbox.ID = ControlID;
                        _multiTextbox.Style.Add("min-width", "300px");
                        this.Controls.Add(_multiTextbox);
                    }
                    break;
                case FieldTypes.Text:
                    if (ReadOnly)
                    {
                        _r_textbox = new Label();
                        _r_textbox.Text = Value;
                        _r_textbox.ID = "_r_" + ControlID;
                        _r_textbox.CssClass = "UDF_ReadValue";
                        this.Controls.Add(_r_textbox);
                    }
                    else
                    {
                        _textbox = new RadTextBox();
                        if (MaxLength > 0)
                            _textbox.MaxLength = MaxLength;
                        if (Value.Length > 0)
                            _textbox.Text = Value;
                        _textbox.ID = ControlID;
                        this.Controls.Add(_textbox);
                    }
                    break;
                case FieldTypes.ListItem:
                    if (ReadOnly)
                    {
                        _r_listitem = new Label();
                        _r_listitem.Text = EPM.Business.Model.UDF.UDFReferenceController.GetDisplayValue(UDFID, Value);
                        _r_listitem.ID = "_r_" + ControlID;
                        _r_listitem.CssClass = "UDF_ReadValue";
                        this.Controls.Add(_r_listitem);
                    }
                    else
                    {
                        _listitem = new RadComboBox();
                        _listitem.ID = ControlID;
                        _listitem.ChangeTextOnKeyBoardNavigation = true;
                        _listitem.EmptyMessage = "-- Select --";
                        _listitem.EnableTextSelection = true;
                        _listitem.MarkFirstMatch = true;
                        _listitem.MaxHeight = Unit.Pixel(200);
                        _listitem.DropDownAutoWidth = RadComboBoxDropDownAutoWidth.Enabled;
                        _listitem.DataSource = EPM.Business.Model.UDF.UDFReferenceController.GetAllReferncesByUDFID(UDFID);
                        _listitem.DataTextField = "DisplayValue";
                        _listitem.DataValueField = "InternalValue";
                        _listitem.DataBind();
                        _listitem.Items.Insert(0, new RadComboBoxItem("-- Select --", ""));
                        if (Value.Length > 0)
                            _listitem.SelectedIndex = _listitem.Items.IndexOf(_listitem.FindItemByValue(Value));
                        if (Value.Length > 0)
                            _listitem.SelectedIndex = _listitem.Items.IndexOf(_listitem.FindItemByValue(Value));
                        this.Controls.Add(_listitem);
                    }
                    break;
                case FieldTypes.LineBreak:
                    Required = false;
                    break;
                case FieldTypes.None:
                    Required = false;
                    break;

            }

            if (Required)
            {
                if (!ReadOnly)
                {
                    CustomValidator customValidator = new CustomValidator();
                    customValidator.Attributes["vid"] = ControlID;
                    customValidator.ControlToValidate = ControlID;
                    customValidator.ID = "cv" + ControlID;
                    customValidator.ClientValidationFunction = "validateValue";
                    customValidator.Display = ValidatorDisplay.Dynamic;
                    customValidator.ValidateEmptyText = true;
                    customValidator.ValidationGroup = ValidationGroup;
                    this.Controls.Add(customValidator);
                }
                
                    
             
            }


            if (!string.IsNullOrEmpty(PostLabel))
            {
                if (FieldType != FieldTypes.CheckBox)
                    this.Controls.Add(new LiteralControl("<span style=\"margin-left:5px\">" + PostLabel + "</span>"));
                else
                    this.Controls.Add(new LiteralControl("<span style=\"margin-left:5px;top:14px;position:relative;\">" + PostLabel + "</span>"));
            }



            if (FieldType != FieldTypes.LineBreak)
            {
                this.Controls.Add(new LiteralControl("\r\n</div><div style=\"clear:both\"></div>"));
            }
            


                    
        }

        protected override void OnInit(EventArgs e)
        {

            

            string cssUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "EPM.Web.UI.EPMStyle.css");

            Boolean cssAlrealyIncluded = false;
            HtmlLink linkAtual;
            foreach (Control ctrl in Page.Header.Controls)
            {
                if (ctrl.GetType() == typeof(HtmlLink))
                {
                    linkAtual = (HtmlLink)ctrl;

                    if (linkAtual.Attributes["href"].Contains(cssUrl))
                    {
                        cssAlrealyIncluded = true;
                    }
                }
            }

            if (!cssAlrealyIncluded)
            {

                HtmlLink cssLink = new HtmlLink();
                cssLink.Href = cssUrl;
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("type", "text/css");
                this.Page.Header.Controls.Add(cssLink);
            }


            this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InfoBox", Page.ClientScript.GetWebResourceUrl(this.GetType(), "EPM.Web.UI.EPMScripts.js"));
        } 

    }



}
