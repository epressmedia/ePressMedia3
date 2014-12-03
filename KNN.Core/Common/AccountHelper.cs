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


using EPM.Legacy.Data;

using log4net;


namespace EPM.Core
{

    /// <summary>
    /// Summary description for AccountHelper
    /// </summary>
    public class AccountHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AccountHelper));
        public AccountHelper()
        {
        }


   
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
    }
    public class UserActivity
    {
        public string UserName { get; private set; }
        public int Points { get; private set; }
        public int ForumPosts { get; private set; }
        public int Posts { get { return (ForumPosts + ClassifiedPosts); } }
        public int ClassifiedPosts { get; private set; }
        public int ArticleComments { get; private set; }
        public int ClassifiedComments { get; private set; }
        public int ForumComments { get; private set; }
        public int Comments { get { return (ArticleComments + ClassifiedComments + ForumComments); } }
        public int LoginCount { get; private set; }
        public bool Subscriber { get; private set; }

        public static List<UserActivity> GetActivityRank(int count, DateTime start, DateTime end)
        {
            SqlCommand cmd = new SqlCommand("KnnGetActivityRank");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Count", count));
            cmd.Parameters.Add(new SqlParameter("@Start", start));
            cmd.Parameters.Add(new SqlParameter("@End", end));

            return DataAccess.SelectCollection<UserActivity>(cmd, getUserActivity);
        }

        static UserActivity getUserActivity(SqlDataReader reader)
        {
            UserActivity a = new UserActivity();

            a.UserName = reader["UserName"].ToString();
            a.Points = (int)reader["Point"];
            a.ForumPosts = (int)reader["FrmCnt"];
            a.ClassifiedPosts = (int)reader["ClsCnt"];
            a.ArticleComments = (int)reader["ArtCommCnt"];
            a.ClassifiedComments = (int)reader["FrmCommCnt"];
            a.ForumComments = (int)reader["ClsCommCnt"];
            a.LoginCount = (int)reader["LoginCnt"];
            a.Subscriber = (bool)reader["Subs"];

            return a;
        }

        public static int GetUserActivityPoints(string userName)
        {
            SqlCommand cmd = new SqlCommand("KnnGetActivityPoints");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@UserName", userName));

            return DataAccess.ExecuteScalar(cmd);
        }
    }
}
    