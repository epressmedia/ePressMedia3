using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Calendar
{
    public static class CalController
    {
        public static IEnumerable<CalendarModel.Calendar>  GetCalendarCategory()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            return context.Calendars.Where(c => 1 == 1);
        }
        public static string GetCalendarName(int CalId)
        {
            if (GetCalendarByID(CalId) != null)
                return GetCalendarByID(CalId).CalName;
            else
                return "";
        }
        public static void AddCategory(string CategoryName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            CalendarModel.Calendar cal = new CalendarModel.Calendar();
            cal.CalName = CategoryName;
            context.Add(cal);
            context.SaveChanges();
        }

        public static void UpdateCategory(int CatID, string CategoryName)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            CalendarModel.Calendar cal = context.Calendars.Single(c => c.CalId == CatID);
            cal.CalName = CategoryName;
            context.SaveChanges();
        }
        public static CalendarModel.Calendar GetCalendarByID(int CalId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var calendars = context.Calendars.Where(c => c.CalId == CalId).ToList();
            if (calendars.Count() > 0)
                return calendars[0];
            else
                return null;
        }

        public static CalendarModel.Event GetEventByID(int eventId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var events = context.Events.Where(c => c.EventId == eventId).ToList();
            if (events.Count() > 0)
                return events[0];
            else
                return null;
        }



        public static void DeleteEvent(int eventId)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            CalendarModel.Event eve = context.Events.Where(x => x.EventId == eventId).SingleOrDefault();
            if (eve != null)
            {
                context.Delete(eve);
                context.SaveChanges();
            }
        }

        public static void UpdateEvent(int eventId, string address, int catId, string contact, string description, DateTime endDate, DateTime startDate, string title, string url, string heldat, 
            byte eventHour, byte eventMinute, string host, string imageUrl, string ipAddress)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            CalendarModel.Event eve = context.Events.Single(c => c.EventId == eventId);
            eve.Address = address;
            eve.Calendar = catId;
            eve.Contact = contact;
            eve.Descr = description;
            eve.EndDate = endDate;
            eve.StartDate = startDate;
            eve.Title = title;
            eve.Url = url;
            eve.HeldAt = heldat;
            eve.EventHour = eventHour;
            eve.EventMinute = eventMinute;
            eve.Host = host;
            eve.ImageUrl = "";
            eve.IpAddr = ipAddress;
            eve.PostDate = DateTime.Now;
            context.SaveChanges();

        }

        public static int AddEvent(string address, int catId, string contact, string description, DateTime endDate, DateTime startDate, string title, string url, string heldat,
    byte eventHour, byte eventMinute, string host, string imageUrl, string ipAddress, string password)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            CalendarModel.Event eve = new CalendarModel.Event();
            eve.Address = address;
            eve.Calendar = catId;
            eve.Contact = contact;
            eve.Descr = description;
            eve.EndDate = endDate;
            eve.StartDate = startDate;
            eve.Title = title;
            eve.Url = url;
            eve.HeldAt = heldat;
            eve.EventHour = eventHour;
            eve.EventMinute = eventMinute;
            eve.Host = host;
            eve.ImageUrl = "";
            eve.IpAddr = ipAddress;
            eve.Password = password;
            eve.PostDate = DateTime.Now;
            context.Add(eve);
            context.SaveChanges();
            return eve.EventId;

        }

        public static IQueryable<CalendarModel.Event> SelectEventTitles(int calendarId, DateTime eventDate)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            var eve = context.Events.Where(c => c.Calendar1.CalId == calendarId && c.StartDate == eventDate).OrderBy(c => c.EventHour);
            if (eve.Count() > 0)
                return eve;
            else
                return null;
                    
        }
    }
}
