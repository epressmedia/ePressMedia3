using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EPM.Business.Model.Biz
{
    public static class BENoteController
    {
        public static int AddBusinessEntityNote(int BusinessEntityID, string NoteContent, Guid created_by)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            BizModel.BusinessEntityNote note = new BizModel.BusinessEntityNote();
            note.BusinessEntityId = BusinessEntityID;
            note.CreatedBy = created_by;
            note.CreatedDate = DateTime.Now;
            note.Deleted = false;
            note.Note = NoteContent;
            

            context.Add(note);
            context.SaveChanges();
            return note.NoteId;
        }

        public static void DeletBusinessEntityNote(int NoteId, Guid modified_by)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntityNotes.Any(c=> c.NoteId == NoteId))
            {
                BizModel.BusinessEntityNote note = context.BusinessEntityNotes.Single(c => c.NoteId == NoteId);
                note.Deleted = true;
                note.ModifiedBy = modified_by;
                note.ModifiedDate  = DateTime.Today;
                context.SaveChanges();

             }
        }

        public static void UpdateBusinessEntityNote(int NoteId, string NoteContent, Guid Modified_by)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.BusinessEntityNotes.Any(c => c.NoteId == NoteId))
            {
                BizModel.BusinessEntityNote note = context.BusinessEntityNotes.Single(c => c.NoteId == NoteId);
                note.Note = NoteContent;
                note.ModifiedBy = Modified_by;
                note.ModifiedDate = DateTime.Today;
                context.SaveChanges();

            }
        }

        public static IQueryable<BizModel.BusinessEntityNote> GetNotes(int BusinessEntityId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return from c in context.BusinessEntityNotes
                   where c.BusinessEntityId == BusinessEntityId && c.Deleted == false
                   orderby c.CreatedDate descending
                   select c;
        }
    }
}
