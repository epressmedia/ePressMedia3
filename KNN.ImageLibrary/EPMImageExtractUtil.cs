using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace EPM.ImageUtil
{
    public class EPMImageExtractUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string GetFirstImageUrlFromArticleBody(string body)
        {
            string lowerBody = body.ToLower();

            int i = lowerBody.IndexOf("<img");
            if (i < 0)              // couldn't find img tag
                return "";

            int j = lowerBody.IndexOf("src", i);
            if (j < 0)              // no src attr
                return "";

            char attrQuote;
            i = lowerBody.IndexOf("\"", j);
            if (i < 0)
            {
                attrQuote = '\'';
                i = lowerBody.IndexOf("'", j);
            }
            else
            {
                attrQuote = '"';
            }

            if (i < 0)          // coudn't find the value of src attribute
                return "";

            i++;

            j = lowerBody.IndexOf(attrQuote, i);

            if (j < 0)          // no matching value closer
                return "";

            return body.Substring(i, j - i);
        }

        /// <summary>
        /// Extract Image URLs from the article content
        /// </summary>
        /// <param name="htmlString">Article in HTML format</param>
        /// <returns></returns>
        public static List<string> GetImagesFromArticleBody(string htmlString)
        {
            return GetImagesFromArticleBody(htmlString, false);
        }

        /// <summary>
        /// Extract Image URLs from the article content and return the thumbnail of YouTube/Vimeo video included in the article content.
        /// </summary>
        /// <param name="htmlString">Article in HTML format</param>
        /// <param name="includeVideo">Catch YouTube or Vimeo Video Thumbnail</param>
        /// <returns></returns>
        public static List<string> GetImagesFromArticleBody(string htmlString, bool includeVideo)
        {
            List<string> images = new List<string>();
            string pattern = "<img.+?src=[\"'](.+?)[\"'].+?>";

            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(htmlString);

            for (int i = 0, l = matches.Count; i < l; i++)
            {
                images.Add(matches[i].Groups[1].Value);
            }

            if (includeVideo)
            {
                pattern = "<iframe.+?src=[\"'](.+?)[\"'].+?>";
                Regex rgx_video = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection matches_video = rgx_video.Matches(htmlString);

                for (int i = 0, l = matches_video.Count; i < l; i++)
                {
                    Regex regexPattern  = null;
                    if (matches_video[i].Value.Contains("youtube.com/embed/") || matches_video[i].Value.Contains("youtube-nocookie.com/embed/"))
                    {
                        // process youtube thubmnail
                        regexPattern = new Regex(@"\/embed\/(?<videoId>.{11})");// (@"/embed/(?<videoId>\w+)");
                        Match videoIdMatch = regexPattern.Match(matches_video[i].Value);

                        if (videoIdMatch.Success)
                        {
                            images.Add("http://img.youtube.com/vi/" + videoIdMatch.Groups["videoId"].Value + "/0.jpg");
                        }
                    }
                    else if (matches_video[i].Value.Contains("vimeo.com/video/"))
                    {
                        // process Vimeo Thumbnail
                        regexPattern = new Regex(@"/video/(?<videoId>\w+)");
                        Match videoIdMatch = regexPattern.Match(matches_video[i].Value);

                        if (videoIdMatch.Success)
                        {
                            //images.Add(videoIdMatch.Groups["videoId"].Value);
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load("http://vimeo.com/api/v2/video/" + videoIdMatch.Groups["videoId"].Value + ".xml");
                            XmlNode node = xmlDoc.SelectSingleNode("videos/video/thumbnail_large");
                            string value = node.InnerText;
                            images.Add(value);


                        }
                    }
                    
                }
            }


            return images;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string GetImageUrlsFromArticle(string body)
        {
            string lowerBody = body.ToLower();




            int i = lowerBody.IndexOf("<img");
            if (i < 0)              // couldn't find img tag
                return "";

            int j = lowerBody.IndexOf("src", i);
            if (j < 0)              // no src attr
                return "";

            char attrQuote;
            i = lowerBody.IndexOf("\"", j);
            if (i < 0)
            {
                attrQuote = '\'';
                i = lowerBody.IndexOf("'", j);
            }
            else
            {
                attrQuote = '"';
            }

            if (i < 0)          // coudn't find the value of src attribute
                return "";

            i++;

            j = lowerBody.IndexOf(attrQuote, i);

            if (j < 0)          // no matching value closer
                return "";

            return body.Substring(i, j - i);
        }


    }
}
