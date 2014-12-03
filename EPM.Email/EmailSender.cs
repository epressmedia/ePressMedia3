using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.Linq;
using log4net;

namespace EPM.Email
{
    public class EmailSender
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmailHelper));

        public static bool SendEmail(string mailTo, string mailCc, string subject, string body, bool isHtml)
        {

            bool result = true;
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    string to = mailTo != null ? string.Join(",", mailTo) : null;
                    string cc = mailCc != null ? string.Join(",", mailCc) : null;

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    //mail.From = new MailAddress(mailFrom, mailFromDisplayName);
                    mail.To.Add(to);

                    if (cc != null)
                    {
                        mail.Bcc.Add(cc);
                    }

                    mail.Subject = subject;
                    mail.Body = body.Replace(Environment.NewLine, "<BR>");
                    mail.IsBodyHtml = isHtml;

                    client.Send(mail);
                    return result;
                }
            }

            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public static void SendInformationMail(string toAddr, string subject, string body)
        {

            try
            {
                MailMessage mail = new MailMessage();
                //mail.From = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString());
                mail.To.Add(toAddr);
                mail.Subject = subject;

                mail.IsBodyHtml = true;
                mail.Body = body;
                //mail.ReplyToList.Add(ConfigurationManager.AppSettings["SenderEmail"].ToString());

                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public static void SendInformationMail(string toAddr, string subject, string templateFile, string paramValue, string hostname)
        {
            string path = HttpContext.Current.Server.MapPath(templateFile);

            TextReader tr = new StreamReader(path);
            string body = tr.ReadToEnd().Replace("{###}", paramValue);
            body = body.Replace("[[hostname]]", hostname);
            tr.Close();

            SendInformationMail(toAddr, subject, body);
        }



        public static bool SendTemplateEmail(string toAddr, string templateName, string paramValue, string hostname)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var email_tr = (from c in context.EmailEvents
                            where c.EmailEventName == templateName
                            select c).ToList();
            string tr = "";
            string subject = "";
            int emaileventid = -1;
            bool success = true;
            if (email_tr.Count() > 0)
            {
                tr = email_tr[0].Body;
                emaileventid = email_tr[0].EmailEventId;
                subject = email_tr[0].Subject;

                string body = tr.Replace("[[param]]", paramValue);
                body = body.Replace("[[hostname]]", hostname);





                // Placeholder portaion will be added to here
                // Update body with the actual placeholder value


                // Log the email event before sending out an email and get the email event history id to pass into the email send function.
                int email_history_id = EmailLogger.LogEmailHistory(toAddr, emaileventid, subject, body);

                // If sender is null, the sender email will be picked up from web.config
                SendEmail( toAddr, subject, body, email_history_id);
            }
            else
            {
                log.Error("Email Template Name(" + templateName + ") does not exist in the system. P:" + paramValue + "/H:" + hostname + "/R:" + toAddr);
                success = false;
            }

            return success;
            


        }



        public static void SendEmail(string toAddr, string subject, string body, int email_history_id)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            
            try
            {
                MailMessage mail = new MailMessage();
                List<string> toAddresses = toAddr.Split(';').ToList<string>();
                foreach (string toAddress in toAddresses)
                {
                    if (!string.IsNullOrEmpty(toAddress))
                        mail.To.Add(toAddress);
                }
                mail.Subject = subject;

                mail.IsBodyHtml = true;
                mail.Body = body;


                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);


                if (email_history_id > 0)
                {
                    // Update the email sent timestamp 
                    EmailModel.EmailHistory history = context.EmailHistories.Single(c => c.EmailHistoryId == email_history_id);
                    history.SentDt = DateTime.Now;
                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                log.Error(ex.Message);
                if (email_history_id > 0)
                {
                    EmailModel.EmailHistory history = context.EmailHistories.Single(c => c.EmailHistoryId == email_history_id);
                    // Update email history log with the error message
                    history.Error = ex.Message;
                    context.SaveChanges();
                }
            }
        }
    }
}
