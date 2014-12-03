using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPM.Data.Model;

namespace EPM.Business.Model.Form
{
    public static class FormController
    {

        public static IQueryable<FormModel.Form> GetForms()
        {
            EPMEntityModel _context = new EPMEntityModel();
            return _context.Forms.Where(x => x.DeletedFg == false).AsQueryable();
        }

        public static void AddForm(string name, string description, bool captcha_fg)
        {
            EPMEntityModel _context = new EPMEntityModel();
            FormModel.Form form = new FormModel.Form();
            form.FormName = name;
            form.FormDescription = description;
            form.CaptchaFg = captcha_fg;
            form.DeletedFg = false;
            _context.Add(form);
            _context.SaveChanges();
        }

        public static void EditForm(int Id, string name, string description, bool captcha_fg)
        {
            EPMEntityModel _context = new EPMEntityModel();
            FormModel.Form form = _context.Forms.Where(x => x.FormId == Id).SingleOrDefault();
            if (form != null)
            {
                form.FormName = name;
                form.FormDescription = description;
                form.CaptchaFg = captcha_fg;
                _context.SaveChanges();
            }
        }

        public static void DeleteForm(int Id)
        {
             EPMEntityModel _context = new EPMEntityModel();
            FormModel.Form form = _context.Forms.Where(x => x.FormId == Id).SingleOrDefault();
            if ((form != null) && (!form.FormEmails.Any()))
            {
                form.DeletedFg = true;
                _context.SaveChanges();
            }
        }

        public static FormModel.Form GetFormById(int FormId)
        {
            return GetForms().Where(x => x.FormId == FormId).SingleOrDefault();
        }

    }
}
