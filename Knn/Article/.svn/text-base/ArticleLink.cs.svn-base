using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Knn;
using Knn.Data;

namespace Knn.Article
{
    public class ArticleLink
    {
        int linkId;
        public int LinkId { get { return linkId; } }
        
        string linkUrl;
        public string LinkUrl { get { return linkUrl; } }

        string title;
        public string Title { get { return title; } }

        DateTime regDate;
        public DateTime RegDate { get { return regDate; } }

        public string ShortTitle(int maxLen)
        {
            if (title.Length <= maxLen)
                return title;
            else
                return title.Substring(0, maxLen) + "...";
        }

        public static int GetCount()
        {
            return DataAccess.ExecuteScalar("SELECT COUNT(*) FROM ArticleLinks");
        }

        static string selectPageQuery =
        @"SELECT * FROM (SELECT LinkId, Title, LinkUrl, RegDate,
        ROW_NUMBER() OVER (ORDER BY LinkId DESC) AS RowNum FROM ArticleLinks) Tbl
        WHERE RowNum BETWEEN {0} AND {1}";

        public static List<ArticleLink> SelectArticleLinks(int pageNum, int rowsPerPage)
        {
            int startRowIndex = (pageNum - 1) * rowsPerPage + 1;
            string query = string.Format(selectPageQuery, 
                                startRowIndex, startRowIndex + rowsPerPage - 1);

            return DataAccess.SelectCollection<ArticleLink>(query, getFromReader);
        }

        static string insertCommand =
        "INSERT INTO ArticleLinks (Title, LinkUrl) VALUES(@Title, @LinkUrl)";

        public static bool InsertArticleLink(string title, string url)
        {
            SqlCommand cmd = new SqlCommand(insertCommand);
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@Title", title));
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@LinkUrl", url));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }

        public static bool DeleteArticleLinks(int id)
        {
            return (1 == DataAccess.ExecuteCommand("DELETE ArticleLinks WHERE LinkId=" + id));
        }

        static ArticleLink getFromReader(SqlDataReader reader)
        {
            ArticleLink a = new ArticleLink();

            a.linkId = (int)reader["LinkId"];
            a.linkUrl = reader["LinkUrl"].ToString();
            a.title = reader["Title"].ToString();
            a.regDate = (DateTime)reader["RegDate"];

            return a;
        }
    }
}
