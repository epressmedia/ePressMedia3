using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Linq;


namespace EPM.Core.Classified
{
    /// <summary>
    /// Summary description for ClassifiedHelper
    /// </summary>
    /// 
    
    public static class ClassifiedHelper
    {
        public const string LabelFormat = "<span class='lbl'>{0}:</span> ";
        public const string DataFormat = "<span class='data'>{0}</span>";
        public const string ShortLabelFormat = "<span class='slbl'>{0}:</span> ";
        public const string ShortDataFormat = "<span class='sdata'>{0}</span>";
        public const string ShortPriceFormat = "<span class='sdata'>$ {0:#,##0}</span>";
        public const string LineBreak = "<br />";





        /// <summary>
        /// Get query part of the given request excluding page parameter
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetPagelessParamString(HttpRequest req)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string key in req.QueryString)
            {
                if (key.Equals("page") || key.Equals("aid"))
                    continue;

                sb.Append((sb.Length > 0) ? "&" : "?");

                sb.AppendFormat("{0}={1}", key, req.QueryString[key]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 광고의 비밀번호가 맞는지 확인한다.
        /// </summary>
        /// <param name="id">광고 아이디</param>
        /// <param name="pwd">original password와 비교할 password</param>
        /// <returns>맞으면 true, 틀리면 false를 리턴한다.</returns>
        public static bool VerifyPassword(int id, string pwd)
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            

            return (1 == context.ClassifiedAds.Count(c => c.AdId == id && c.Password == pwd));
        }
 

        /// <summary>
        /// 광고 조회수를 1 증가 시킨다. 쿠키를 이용하여 한시간 이내에 다시 증가시키는 경우는 조회수가 
        /// 올라가지 않는다.
        /// </summary>
        public static bool IncreaseViewCount(int id, HttpRequest req, HttpResponse res)
        {
            if (req.Cookies["cls" + id.ToString()] != null)
                return false;

            HttpCookie cookie = new HttpCookie("cls" + id.ToString(), "1");
            cookie.Expires = DateTime.Now.AddHours(1);
            res.Cookies.Add(cookie);

            IncreaseViewCount(id);

            return true;
        }

        /// <summary>
        /// 광고 조회수를 무조건 1 증가 시킨다.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void IncreaseViewCount(int id)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ClassifiedModel.ClassifiedAd ad = context.ClassifiedAds.Single(c => c.AdId == id);
            ad.ViewCount = ad.ViewCount + 1;
            context.SaveChanges();
        }



        /// <summary>
        /// Delete Classified Ad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeleteClassifiedAd(int id)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            ClassifiedModel.ClassifiedAd ad = context.ClassifiedAds.Single(c => c.AdId == id);
            ad.Suspended = true;
            context.SaveChanges();
        }

    }
}