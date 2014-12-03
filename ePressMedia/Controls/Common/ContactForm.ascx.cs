using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;

namespace ePressMedia.Controls.Common
{
    [Description("General Contact Form")]


    public partial class ContactForm : System.Web.UI.UserControl
    {



        private static readonly ILog log = LogManager.GetLogger(typeof(ContactForm));
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            EnableControls();
            
            if (!IsPostBack)
                lbl_header.Text = FormName;
        }
        void ClearAllFields()
        {
            txt_address.Text = txt_city.Text = txt_firstname.Text = txt_lastname.Text= txt_emailaddress.Text = txt_comments.Text = txt_phonenumber.Text = txt_postalcode.Text = txt_state.Text = "";
             
        }

        void EnableControls()
        {
            if (FirstName)
                ContactForm_FirstName.Visible = true;
            if (LastName)
                ContactForm_LastName.Visible = true;
            if (EmailAddress)
                ContactForm_EmailAddress.Visible = true;
            if (Address)
                ContactForm_Address.Visible = true;
            if (City)
                ContactForm_City.Visible = true;
            if (State)
                ContactForm_State.Visible = true;
            if (Zipcode)
                ContactForm_PostalCode.Visible = true;
            if (comments)
                ContactForm_Comments.Visible = true;
            if (PhoneNumber)
                ContactForm_PhoneNumber.Visible = true;

        }


        private string formName;
        [Category("EPMProperty"), Description("Form Name to be displayed. The name will be emailed as the email subject.")]
        public string FormName
        {
            get { return formName; }
            set { formName = value; }
        }


        private Boolean firstname;
        [Category("EPMProperty"), Description("Show First Name Field"), DefaultValue(typeof(System.Boolean), "true")]
        public Boolean FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private Boolean lastname;
        [Category("EPMProperty"), Description("Show Last Name Field"), DefaultValue(typeof(System.Boolean), "true")]
        public Boolean LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        private Boolean emailAddress;
        [Category("EPMProperty"), Description("Show Email Address Field"), DefaultValue(typeof(System.Boolean), "true")]
        public Boolean EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        private Boolean phoneNumber;
        [Category("EPMProperty"), Description("Show Phone Number Field"), DefaultValue(typeof(System.Boolean), "false")]
        public Boolean PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private Boolean address;
        [Category("EPMProperty"), Description("Show Address Field"), DefaultValue(typeof(System.Boolean), "false")]
        public Boolean Address
        {
            get { return address; }
            set { address = value; }
        }

        private Boolean city;
        [Category("EPMProperty"), Description("Show City Field"), DefaultValue(typeof(System.Boolean), "false")]
        public Boolean City
        {
            get { return city; }
            set { city = value; }
        }

        private Boolean state;
        [Category("EPMProperty"), Description("Show State/Province Field"), DefaultValue(typeof(System.Boolean), "false")]
        public Boolean State
        {
            get { return state; }
            set { state = value; }
        }

        private Boolean zipcode;
        [Category("EPMProperty"), Description("Show Zip Code/Postal Code Field"), DefaultValue(typeof(System.Boolean), "false")]
        public Boolean Zipcode
        {
            get { return zipcode; }
            set { zipcode = value; }
        }


        private Boolean comments;
        [Category("EPMProperty"), Description("Show Coments Field"), DefaultValue(typeof(System.Boolean), "true")]
        public Boolean Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        private Boolean captcha;
        [Category("EPMProperty"), Description("Show Captcha Field"), DefaultValue(typeof(System.Boolean), "false")]
        public Boolean Captcha
        {
            get { return captcha; }
            set { captcha = value; }
        }

        private string recipient;
        [Category("EPMProperty"), Description("Recipient Email Address")]
        public string Recipient
        {
            get { return recipient; }
            set { recipient = value; }
        }


        protected void btn_send_Click(object sender, EventArgs e)
        {
            Page.Validate("SendEmail");

            
            if (((Captcha) && (Page.IsValid)) || (!Captcha))
            {
                sendMessage();

            }
            else
            {
                lbl_errorMessage.Text = RadCaptcha1.ErrorMessage;
            }
        }


        void sendMessage()
        {
            try
            {
                string subject = lbl_header.Text.Length > 0 ? lbl_header.Text : Page.Title.ToString() + "Contact Form";
                string body = BuildUpMessage();
                //int email_history_id = EPM.Email. EmailLogger.LogEmailHistory(Recipient, -1, subject, body);
                EPM.Email.EmailSender.SendEmail(Recipient, subject, body, -1);
                ClearAllFields();
                lbl_errorMessage.Text = "";
                
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('Email has been sent successfully.')", true);
            }
            catch (Exception ex)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('An error has occured. Please contract the administrator.')", true);
                log.Error(ex.Message);
            }
        }
        protected string BuildUpMessage()
        {
            string msg = "";

            if (FirstName)
                msg = msg+ lbl_firstname.Text + ": " + txt_firstname.Text + "<br/>";
            if (LastName)
                msg = msg + lbl_lastname.Text + ": " + txt_lastname.Text + "<br/>";
            if (EmailAddress)
                msg = msg + lbl_emailaddress.Text + ": " + txt_emailaddress.Text + "<br/>";
            if (PhoneNumber)
                msg = msg + lbl_phonenumber.Text + ": " + txt_phonenumber.Text + "<br/>";
            if (Address)
                msg = msg + lbl_address.Text + ": " + txt_address.Text + "<br/>";
            if (City)
                msg = msg + lbl_city.Text + ": " + txt_city.Text + "<br/>";
            if (State)
                msg = msg + lbl_state.Text + ": " + txt_state.Text + "<br/>";
            if (Zipcode)
                msg = msg + lbl_postalcode.Text + ": " + txt_postalcode.Text + "<br/>";
            if (comments)
                msg = msg + lbl_comments.Text + ": " + txt_comments.Text.Replace(System.Environment.NewLine,"<br />") + "<br/>";

            return msg;
        }
    }
}