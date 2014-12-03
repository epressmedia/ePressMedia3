using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using Knn.Data;

namespace EPM.Core
{

    /// <summary>
    /// Summary description for Literals
    /// </summary>
    public class Literals
    {
        public Literals()
        {
        }

        static string updateCommand =
            "UPDATE Literals SET Literal=@Literal WHERE Id=@Id";
        public static bool UpdateLiteral(int id, string literal)
        {
            SqlCommand cmd = new SqlCommand(updateCommand);

            cmd.Parameters.Add(new SqlParameter("@Id", id));
            cmd.Parameters.Add(DataAccess.GetNTextParam("@Literal", literal));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }
    }
}