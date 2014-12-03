using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

using System.Security.Cryptography;

namespace EPM.Core
{
    public class CommonUtility
    {

        public static string GetCurrentDate(string stringFormat)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string culture_info = context.SiteSettings.Single(c => c.SettingName == "Culture Info").SettingValue;


            string DateStr = DateTime.Today.ToString(stringFormat, System.Globalization.CultureInfo.CreateSpecificCulture(culture_info));

            return DateStr;

        }


        public static string GetFullQueryString()
        {
            string query = "?";
            foreach (String key in HttpContext.Current.Request.QueryString.AllKeys)
            {
                query = query + key + "=" + HttpContext.Current.Request.QueryString[key] + "&";
            }
            return query;
        }

        public static string FullyQualifiedApplicationPath
        {
            get
            {
                //Return variable declaration
                string appPath = null;

                //Getting the current context of HTTP request
                HttpContext context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format("{0}://{1}{2}{3}",
                      context.Request.Url.Scheme,
                      context.Request.Url.Host,
                      context.Request.Url.Port == 80
                        ? string.Empty : ":" + context.Request.Url.Port,
                      context.Request.ApplicationPath);
                }
                if (!appPath.EndsWith("/"))
                    appPath += "/";
                return appPath;
            }
        }

        
        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        public static string ToUrlSlug(string value)
        {

            //First to lower case
            value = value.ToLowerInvariant();

            ////Remove all accents
            //var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            //value = Encoding.Unicode.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }

        public static string GenerateSlug( string phrase)
        {
            string str = phrase;// RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
        public static string ConvertTextToSlug(string s)
        {
            StringBuilder sb = new StringBuilder();

            bool wasHyphen = true;

            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToLower(c));
                    wasHyphen = false;
                }

                else if (char.IsWhiteSpace(c) && !wasHyphen)
                {
                    sb.Append('-');
                    wasHyphen = true;
                }
            }

            // Avoid trailing hyphens
            if (wasHyphen && sb.Length > 0)
                sb.Length--;

            sb = sb.Replace("?", "");
            sb = sb.Replace(".", "");
            sb = sb.Replace("~", "");
            sb = sb.Replace("/", "");
            sb = sb.Replace("&", "");

            return sb.ToString().Replace("--", "-");
        }




    }
}
