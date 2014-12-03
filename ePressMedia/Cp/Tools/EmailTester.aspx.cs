using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Configuration;
using EPM.Email;
using System.Net.Mail;



namespace ePressMedia.Cp.Tools
{
    public partial class EmailTester : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SmtpSection setting = EmailHelper.GetSMTPSettings();
            txt_from.Text = setting.From;


                SmtpClient smtp = new SmtpClient();
                txt_host.Text = smtp.Host;
                txt_port.Text = smtp.Port.ToString();
        

        }

        protected void btn_send_Click(object sender, EventArgs e)
        {
            try{
                EmailSender.SendInformationMail(txt_To.Text, txt_subject.Text, txt_body.Text);
                txt_result.Text = "Email has been sent successfully";
            }
            catch(Exception ex)
            {
                txt_result.Text = ex.StackTrace.ToString();
            }
        }
    }
}