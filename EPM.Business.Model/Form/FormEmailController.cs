using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPM.Data.Model;

namespace EPM.Business.Model.Form
{
    public static class FormEmailController
    {
        public static IQueryable<FormModel.FormEmail> GetAllFormEmailsByFormId(int FormId)
        {
            EPMEntityModel _context = new EPMEntityModel();
            return _context.FormEmails.Where(x => x.FormId == FormId).AsQueryable();
        }

        public static int AddFormEmail(int EmailEventId, string Receipients, int UDFInfoId, int FormId)
        {
            EPMEntityModel _context = new EPMEntityModel();
            FormModel.FormEmail email = new FormModel.FormEmail();
            email.EmailEventId = EmailEventId;
            if (!string.IsNullOrEmpty(Receipients))
                email.Receipients = Receipients;
            if (UDFInfoId > 0) 
                email.EmailUDFInfoId = UDFInfoId;
            email.FormId = FormId;
            _context.Add(email);
            _context.SaveChanges();


            return email.FormEmailId;
        }

        public static void EditFormEmail(int FormEmailId, int EmailEventId, string Receipients, int UDFInfoId, int FormId)
        {
            EPMEntityModel _context = new EPMEntityModel();
            FormModel.FormEmail email = _context.FormEmails.Where(x => x.FormEmailId == FormEmailId).SingleOrDefault();

            if (email != null)
            {
                email.EmailEventId = EmailEventId;
                    email.Receipients = Receipients;
                if (UDFInfoId > 0)
                    email.EmailUDFInfoId = UDFInfoId;
                else
                    email.EmailUDFInfoId = null;
                email.FormId = FormId;
                _context.SaveChanges();
            }
        }

        public static void DeleteFormEmail(int FormEmailId)
        {
            EPMEntityModel _context = new EPMEntityModel();
            FormModel.FormEmail email = _context.FormEmails.Where(x => x.FormEmailId == FormEmailId).SingleOrDefault();
            if (email != null)
            {
                _context.Delete(email);
                _context.SaveChanges();
            }
        }

    }
}
