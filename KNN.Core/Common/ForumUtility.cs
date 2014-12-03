using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using EPM.Business.Model.Admin;

namespace EPM.Core
{

    /// <summary>
    /// Summary description for ForumUtility
    /// </summary>
    public static class ForumUtility
    {
        static bool needRefresh = true;
        static string bannedWords;
        static Random random = new Random();
        static string passChars = "ACEFGHJKLMNPQRSTXYZ2345679!@#$%^&*()~";

        public static void RefreshBannedWords()
        {
            needRefresh = true;
        }

        public static string GetCleanText(string text)
        {
            if (needRefresh)
                loadBannedWords();


            string[] listwords = bannedWords.Split(',');

            for (int i = 0; i < listwords.Length; i++)
            {
                if (listwords[i].Length > 0)
                    text = text.Replace(listwords[i], "XXX");
            }

            return text; //Regex.Replace(text,  bannedWords , "XXX");
        }


        static void loadBannedWords()//(HttpServerUtility server)
        {
            

            bannedWords = SiteSettingController.GetSiteSettingValueByName("Banned Words");


        }

        public static string ToMaskedIpAddress(string ipAddr)
        {
            int i = ipAddr.LastIndexOf('.');
            if (i <= 0)
                return ipAddr;

            return (ipAddr.Substring(0, i) + ".XXX");
        }

        public static string GetRandomPassword(int length)
        {
            string s = "";
            for (int i = 0; i < length; i++)
                s = String.Concat(s, passChars[random.Next(36)]);
            return s;
        }
    }
}