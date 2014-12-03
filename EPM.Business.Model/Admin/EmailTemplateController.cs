using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EPM.Business.Model.Admin
{
    public static class EmailTemplateController
    {

        public static EmailModel.EmailEvent GetEmailTemplateById(int EmailEventID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.EmailEvents.Count(c => c.EmailEventId == EmailEventID) > 0)
                return context.EmailEvents.Single(c => c.EmailEventId == EmailEventID);
            else
                return null;
        }

        public static void SaveEmailTemplate(int EmailEventID, string subject, string body, string updated_by)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

           EmailModel.EmailEvent ee = context.EmailEvents.Single(c => c.EmailEventId == EmailEventID);
           ee.Body = body;
           ee.Subject = subject;
           ee.ModifiedBy = updated_by;
           //.ModifiedDt = DateTime.Today;
           context.SaveChanges();
        }

        public static void AddEmailTemplate(string EmailEventName, string EmailEventDescr)
        {
            EPM.Data.Model.EPMEntityModel _context = new Data.Model.EPMEntityModel();
            EmailModel.EmailEvent email = new EmailModel.EmailEvent();
            email.EmailEventName = EmailEventName;
            email.Description = EmailEventDescr;
            email.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name.ToString();
            email.CreatedDt = DateTime.Parse(DateTime.Now.ToString("u"));
            email.ModifiedBy = System.Web.HttpContext.Current.User.Identity.Name.ToString();
            email.ModifiedDt = DateTime.Parse(DateTime.Now.ToString("u"));
            email.Body= "";
            email.Subject = "";
            _context.Add(email);
            _context.SaveChanges();
        }

        public static IQueryable<EmailModel.EmailEvent> GetAllEmailEvents()
        {
            EPM.Data.Model.EPMEntityModel _context = new Data.Model.EPMEntityModel();
            return _context.EmailEvents;
        }
    }
}
