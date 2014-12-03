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
using System.Net.Configuration;



namespace EPM.Email
{
    public class EmailHelper
    {



        public static void AddLoginHistory(string userName)
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            UserModel.DailyLogin loginEvent = new UserModel.DailyLogin();
            loginEvent.LoginDate = DateTime.Now;
            loginEvent.UserName = userName;
            context.Add(loginEvent);
            context.SaveChanges();
        }



        public static void SubscribeKcr(string userName, bool subscribe)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<UserModel.Subscription> subs = (from c in context.Subscriptions
                                                 where c.UserName == userName
                                                 select c).ToList();

            if (subs.Count() > 0)
            {
                UserModel.Subscription sub = subs[0];
                sub.SubsDate = DateTime.Now;
                sub.Subscribe = subscribe;
            }
            else
            {
                UserModel.Subscription sub = new UserModel.Subscription();
                sub.UserName = userName;
                sub.SubsDate = DateTime.Now;
                sub.Subscribe = true;
                context.Add(sub);
            }

            context.SaveChanges();


        }

        public bool SubscribeChecked(string userName)
        {
            bool Subscribe = false;
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<UserModel.Subscription> subs = (from c in context.Subscriptions
                                                 where c.UserName == userName
                                                 select c).Take(1).ToList();

            if (subs.Count() > 0)
            {
                Subscribe = subs[0].Subscribe;

            }
            return Subscribe;


        }



        public static SmtpSection GetSMTPSettings()
        {
            return ((SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp"));
        }

    }
}
