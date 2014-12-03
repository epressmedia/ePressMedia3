using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Knn;
using Knn.Data;

namespace Knn.Article
{
    public class ArticleCategoryConfig
    {
        string catName;
        public string CategoryName { get { return catName; } }

        string hdrImage;
        public string HeaderImage { get { return hdrImage; } }

        int rowsPerPage;
        public int RowsPerPage { get { return rowsPerPage; } }

        static string selectByIdQuery =
        "SELECT c.*, a.CatName FROM ArticleCatConfigs c JOIN ArticleCategories a ON c.CatId=a.CatId WHERE c.CatId=";
        public static ArticleCategoryConfig SelectById(int catId)
        {
            return DataAccess.SelectInstanceById<ArticleCategoryConfig>
                        (selectByIdQuery + catId, getFromReader);
        }

        static string insertCommand =
        "INSERT INTO ArticleCatConfigs (CatId) VALUES(@CatId)";

        public static bool Insert(int catId)
        {
            SqlCommand cmd = new SqlCommand(insertCommand);
            cmd.Parameters.Add(new SqlParameter("@CatId", catId));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }


        static string updateCommand =
        @"UPDATE ArticleCatConfigs SET HdrImg=@HdrImg, RowsPerPage=@RowsPerPage 
        WHERE CatId=@CatId";

        public static bool Update(int catId, string hdrImage, int rowsPerPage)
        {
            SqlCommand cmd = new SqlCommand(updateCommand);

            cmd.Parameters.Add(new SqlParameter("@CatId", catId));
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@HdrImg", hdrImage));
            cmd.Parameters.Add(new SqlParameter("@RowsPerPage", rowsPerPage));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }

        static ArticleCategoryConfig getFromReader(SqlDataReader reader)
        {
            ArticleCategoryConfig c = new ArticleCategoryConfig();

            c.catName = reader["CatName"].ToString();
            c.hdrImage = reader["HdrImg"].ToString();
            c.rowsPerPage = (int)reader["RowsPerPage"];

            return c;
        }
    }
}
