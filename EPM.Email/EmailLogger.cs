using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Email
{
    public static class EmailLogger
    {
        public static int LogEmailHistory( string recipients, int emaileventid, string subject, string body)
        {
            
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            EmailModel.EmailHistory e_history = new EmailModel.EmailHistory();
            e_history.Body = body;
            e_history.Subject = subject;
            e_history.EmailEventId = emaileventid;
            e_history.RecipientEmail = recipients;
            e_history.SenderEmail = "";
            e_history.CreatedDt = DateTime.Now;
            context.Add(e_history);
            context.SaveChanges();
            return e_history.EmailHistoryId;
        }
    }
}
