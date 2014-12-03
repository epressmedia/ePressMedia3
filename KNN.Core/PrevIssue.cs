using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using Knn;
using Knn.Data;
namespace EPM.Core
{
    /// <summary>
    /// Summary description for PrevIssue

    /// </summary>
    public class PrevIssue
    {
        int id;
        public int Id { get { return id; } }

        string url;
        public string Url { get { return url; } }

        string title;
        public string Title { get { return title; } }

        string imageUrl;
        public string ImageUrl { get { return imageUrl; } }


        static string updateCommand =
        "UPDATE PrevIssues SET Title=@Title, Url=@Url, ImageUrl=@ImageUrl WHERE Id=@Id";
        public static bool SetIssue(int id, string title, string url, string imageUrl)
        {
            SqlCommand cmd = new SqlCommand(updateCommand);

            cmd.Parameters.Add(new SqlParameter("@Id", id));
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@Title", title));
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@Url", url));
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@ImageUrl", imageUrl));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }

        static string selectByIdQuery = "SELECT * FROM PrevIssues WHERE Id=";
        public static PrevIssue SelectById(int id)
        {
            return DataAccess.SelectInstanceById<PrevIssue>(selectByIdQuery + id,
                getPrevIssueFromReader);
        }

        static string selectQuery = "SELECT * FROM PrevIssues ORDER BY Id";
        public static List<PrevIssue> Select()
        {
            return DataAccess.SelectCollection<PrevIssue>(selectQuery, getPrevIssueFromReader);
        }

        static PrevIssue getPrevIssueFromReader(SqlDataReader reader)
        {
            PrevIssue issue = new PrevIssue();

            issue.id = (int)reader["Id"];
            issue.title = reader["Title"].ToString();
            issue.url = reader["Url"].ToString();
            issue.imageUrl = reader["ImageUrl"].ToString();

            return issue;
        }
    }
}