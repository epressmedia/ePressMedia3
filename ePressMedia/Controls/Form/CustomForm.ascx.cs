using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPM.Business.Model.UDF;
using EPM.Web.UI;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Form
{
    public partial class CustomForm : System.Web.UI.UserControl
    {
            protected void Page_Load(object sender, EventArgs e)
    {
        LoadDefaultConfiguratoin();
        LoadUDFPanel();
            }
            public void LoadDefaultConfiguratoin()
            {
                btn_submit.Text = ButtonName;
            }

            public void LoadUDFPanel()
            {
                string ContentName = Enum.GetName(typeof(ContentTypes), ContentType);
                EPM.Data.Model.EPMEntityModel _context = new EPM.Data.Model.EPMEntityModel();

                if (_context.ContentTypes.Any(x => x.ContentTypeName == ContentName))
                {
                    SiteModel.ContentType cont = _context.ContentTypes.Where(x => x.ContentTypeName == ContentName).Single();
                    
                    var c = LoadControl("/Page/UDFEntryPanel.ascx");
                    ePressMedia.Pages.UDFEntryPanel uc = c as ePressMedia.Pages.UDFEntryPanel;
                    uc.ID = "udfs_panel";


                    uc.ContentTypeId = cont.ContentTypeId;
                    if (cont.CategoryFg)
                        uc.CategoryId = CategoryId;
                        //uc.ContentId = context.UserLinks.SingleOrDefault(x => x.UserId == (Guid)Membership.GetUser().ProviderUserKey).UserLinkId;


                    
                    uc.ValidationGroup = btn_submit.ValidationGroup;
                    UDF_Panel.Controls.Add(uc);

                    CaptchaFg = EPM.Business.Model.Form.FormController.GetFormById(categoryId).CaptchaFg;
                    if (CaptchaFg)
                        RadCaptcha1.Visible = RadCaptcha1.Enabled = true;
                    else
                        RadCaptcha1.Visible = RadCaptcha1.Enabled = false;
                }
            }


            private ContentTypes contentType;
            [Category("EPMProperty"), Description("Content Type to be linked"), Required(ErrorMessage = "Content Type is required.")]
            public ContentTypes ContentType
            {
                get { return contentType; }
                set { contentType = value; }
            }

            private int categoryId;
            [Category("EPMProperty"), Description("CategoryId(Form ID) to be linked")]
            public int CategoryId
            {
                get { return categoryId; }
                set { categoryId = value; }
            }



            private Boolean captchaFg;
            [Description("Show Captcha (Anti-spam) "), DefaultValue(typeof(System.Boolean), "false")]
            public Boolean CaptchaFg
            {
                get { return captchaFg; }
                set { captchaFg = value; }
            }

            private String buttonName;
            [Category("EPMProperty"), Description("Name of Submit Button"), DefaultValue(typeof(System.String), "Submit")]
            public String ButtonName
            {
                get { return buttonName; }
                set { buttonName = value; }
            }

            private String successMessage = "Email has been sent successfully.";
            [Category("EPMProperty"), Description("Success Message"), DefaultValue(typeof(System.String), "Email has been sent successfully.")]
            public String SuccessMessage
            {
                get { return successMessage; }
                set { successMessage = value; }
            }


            public enum ContentTypes
            {
                //User = 1,
                Form = 2
            }


            protected void btn_submit_Click(object sender, EventArgs e)
            {
                Page.Validate("SendEmail");


                if (((CaptchaFg) && (Page.IsValid)) || (!CaptchaFg))
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
                    //string subject = lbl_header.Text.Length > 0 ? lbl_header.Text : Page.Title.ToString() + "Contact Form";
                    //string body = BuildUpMessage();
                    ////int email_history_id = EPM.Email. EmailLogger.LogEmailHistory(Recipient, -1, subject, body);
                    //EPM.Email.EmailSender.SendEmail(Recipient, subject, body, -1);
                    //ClearAllFields();

                    lbl_errorMessage.Text = "";
                    string message = "";

                    ePressMedia.Pages.UDFEntryPanel uc = (ePressMedia.Pages.UDFEntryPanel)UDF_Panel.FindControl("udfs_panel");

                    foreach (UDFModel.UDFValue uv in uc.GetValues())
                    {

                        UDFModel.UDFInfo udf = EPM.Business.Model.UDF.UDFController.GetUDFByUDFID(uv.UDFID);
                        string value = "";
                        if (udf.ReferenceFg)
                        {
                            var dropdownvalue = EPM.Business.Model.UDF.UDFReferenceController.GetAllReferncesByUDFID(uv.UDFID).Where(x => x.InternalValue == uv.Value).SingleOrDefault();
                            if (dropdownvalue != null)
                                value = dropdownvalue.DisplayValue;
                        }
                        else
                            value = uv.Value;
                        //message = message + "\\n " + udf.Label+": "+ value;
                        message = message + "<br/>" + udf.Label + ": " + value;
                    }

                    message = message + "<br/>";

                   // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('Email has been sent successfully. \n "+message+"');window.location.href= window.location;", true);
                    // Get list of form email list
                    var formemails = EPM.Business.Model.Form.FormEmailController.GetAllFormEmailsByFormId(CategoryId);
                    foreach(FormModel.FormEmail formemail in formemails)
                    {
                        
                        string receipients = "";
                        if (!string.IsNullOrEmpty(formemail.Receipients))
                        {
                            receipients = formemail.Receipients;
                        }
                        else
                        {
                            var udf_email =  uc.GetValues().Where(x=>x.UDFID == formemail.EmailUDFInfoId).SingleOrDefault();
                            if (udf_email != null)
                                receipients = udf_email.Value;
                        }
                        EPM.Email.EmailSender.SendTemplateEmail(receipients, formemail.EmailEvent.EmailEventName, message, "");
                    }

                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('" + SuccessMessage +"');window.location.href= window.location;", true);
                }
                catch (Exception ex)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('An error has occured. Please contract the administrator.')", true);
                    //log.Error(ex.Message);
                }
            }
    }

}