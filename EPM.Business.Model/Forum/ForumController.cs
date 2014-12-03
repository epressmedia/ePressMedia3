using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Forum
{
    public static class ForumController
    {
        public static void DeleteForumCategory(int CatId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            var forum_threads = from c in context.ForumThreads
                                where c.Forum.ForumId == CatId 
                                select c;
            context.Delete(forum_threads);
            context.SaveChanges();

            var forum_config = from c in context.ForumConfigs
                               where c.ForumId == CatId 
                               select c;
            context.Delete(forum_config);
            context.SaveChanges();


            ForumModel.Forum forum = (from c in context.Forums
                                      where c.ForumId == CatId 
                                      select c).Single();
            context.Delete(forum);
            context.SaveChanges();
        }

        public static void DeleteForumThread(int ThreadId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            ForumModel.ForumThread t = context.ForumThreads.Where(x => x.ThreadId == ThreadId).SingleOrDefault();
            if (t != null)
            {
                t.Suspended = true;
                context.SaveChanges();
            }
        }

        public static IQueryable<ForumModel.ForumThread> GetThreadsByForumId(int ForumId, bool showAnounce = false)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var query = (from c in context.ForumThreads
                    where c.ForumId == ForumId
                    && c.Suspended == false
                    orderby c.Announce descending, c.PostDate descending
                    select c);
            if (!showAnounce)
                return query.Where(c => c.Announce == showAnounce);
            else
                return query;
        }

        public static IQueryable<ForumModel.ForumThread> GetThreadsByForumIds(string ForumIds, bool showAnounce = false)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var CatIds = ForumIds.Split(',').ToList();
            var query = (from c in context.ForumThreads
                     where CatIds.Contains(c.ForumId.ToString())
                     && c.Suspended == false
                     orderby c.Announce descending, c.PostDate descending
                     select c);
           if (!showAnounce)
                return query.Where(c => c.Announce == showAnounce);
            else
                return query;
        }


        public static ForumModel.ForumThread GetThreadByThreadID(int ThreadID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.ForumThreads.Where(c => c.ThreadId == ThreadID).Any())
            {
                ForumModel.ForumThread query = (from c in context.ForumThreads
                                                where c.ThreadId == ThreadID
                                                && c.Suspended == false
                                                select c).Single();
                return query;
            }
            else
                return null;
        }

        public static int GetImageCountByThreadID(int ThreadID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.ForumAttachments.Where(c => c.ThreadId == ThreadID && (c.FileName.EndsWith("png") || c.FileName.EndsWith("gif") || c.FileName.EndsWith("jpg"))).Count();
            
        }

        public static bool ValidateThreadPassword(int ThreadID, string password)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.ForumThreads.Any(c => c.ThreadId == ThreadID && c.Password == password);
        }

        public static IQueryable<ForumModel.ForumAttachments> GetAttachmentsByThreadID(int ThreadID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.ForumAttachments.Where(c => c.ThreadId == ThreadID);

        }

        public static void AddThreadAttachment(int ThreadID, string FilePath)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ForumModel.ForumAttachments f_attachment = new ForumModel.ForumAttachments();

            f_attachment.FileName = FilePath;
            f_attachment.ThreadId = ThreadID;

            context.Add(f_attachment);
            context.SaveChanges();
        }

        public static void DeleteAllThreadAttachments(int ThreadID)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var attachments = from c in context.ForumAttachments
                              where c.ThreadId == ThreadID
                              select c;
            context.Delete(attachments);
            context.SaveChanges();
        }


        public static int AddForumThread(int CategoryID, string Subject, String Body, String PostedBy,String IPAddress, Boolean Announce,
            String Password, Boolean Private, String UserName, Boolean Suspended, String Thumb)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ForumModel.ForumThread forum_t = new ForumModel.ForumThread();

            forum_t.ForumId = CategoryID;
            forum_t.Subject = Subject;
            forum_t.Body = Body;
            forum_t.PostBy = PostedBy;
            forum_t.IPAddress = IPAddress;
            forum_t.Announce = Announce;
            forum_t.Password = Password;
            forum_t.Private = Announce ? false : Private;
            forum_t.UserName = UserName;
            forum_t.Suspended = Suspended;
            forum_t.PostDate = DateTime.Now;
            forum_t.Thumb = Thumb;

            // Get the last thread number in given category
            if (context.ForumThreads.Where(c => c.ForumId == CategoryID).Count() > 0)
            {
                forum_t.ThreadNum = ((from c in context.ForumThreads
                                      where c.ForumId == CategoryID
                                      orderby c.ThreadNum descending
                                      select c).First()).ThreadNum + 1;
            }
            else
                forum_t.ThreadNum = 1;


            context.Add(forum_t);
            context.SaveChanges();
            return forum_t.ThreadId;
        }
        public static void UpdateForumThread(int ThreadID, string Subject, String Body, String PostedBy, String IPAddress, Boolean Announce, Boolean Private, String UserName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.ForumThreads.Where(c => c.ThreadId == ThreadID).Any())
            {


                ForumModel.ForumThread forum_t = (from c in context.ForumThreads
                                                where c.ThreadId == ThreadID
                                                && c.Suspended == false
                                                select c).Single();

                forum_t.Subject = Subject;
                forum_t.Body = Body;
                forum_t.IPAddress = IPAddress;
                forum_t.Announce = Announce;
                forum_t.Private = Announce ? false : Private;
                forum_t.PostBy = PostedBy;
                forum_t.UserName = UserName;

                context.SaveChanges();
            }
        }
        public static void UpdateForumThumb(int ThreadID, string ThumbPath)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.ForumThreads.Where(c => c.ThreadId == ThreadID).Any())
            {
                ForumModel.ForumThread query = (from c in context.ForumThreads
                                                where c.ThreadId == ThreadID
                                                && c.Suspended == false
                                                select c).Single();
                query.Thumb = ThumbPath;
                context.SaveChanges();

            }
        }

        public static ForumModel.Forum GetForumByThreadId(int ThreadId)
        {
            EPM.Data.Model.EPMEntityModel context = new Data.Model.EPMEntityModel();
            return context.ForumThreads.Where(x => x.ThreadId == ThreadId).SingleOrDefault().Forum;
        }

    }
}
