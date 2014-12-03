using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace EPM.Legacy.Data
{
    public static class DataAccess
    {
        public delegate T InstanceFromReader<T>(SqlDataReader reader);
        //public delegate List<T> CollectionFromReader<T>(SqlDataReader reader);

        public static T SelectInstanceById<T>(string query, InstanceFromReader<T> readMethod)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader(CommandBehavior.SingleRow);

                T t = default(T);

                if (r.Read())
                    t = readMethod(r);

                r.Close();
                conn.Close();

                return t;
            }
        }

        public static List<T> SelectCollection<T>(string query, InstanceFromReader<T> readMethod)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader r = cmd.ExecuteReader();

                List<T> list = new List<T>();

                while (r.Read())
                    list.Add(readMethod(r));

                r.Close();
                conn.Close();

                return list;
            }
        }

        public static List<T> SelectCollection<T>(SqlCommand cmd, InstanceFromReader<T> readMethod)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;

                SqlDataReader r = cmd.ExecuteReader();

                List<T> list = new List<T>();

                while (r.Read())
                    list.Add(readMethod(r));

                r.Close();
                conn.Close();

                return list;
            }
        }

        public static bool ProcessTransaction(SqlCommand cmd1, SqlCommand cmd2)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                cmd1.Connection = cmd2.Connection = conn;
                SqlTransaction trans = conn.BeginTransaction();
                cmd1.Transaction = cmd2.Transaction = trans;

                try
                {
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                    trans.Commit();

                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// Executes query command with ExecuteScalar() method call.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <returns>Returns the count of the affected row(s)</returns>
        public static int ExecuteScalar(string query)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                int cnt = (int)cmd.ExecuteScalar();

                conn.Close();
                return cnt;
            }
        }

        public static int ExecuteScalar(SqlCommand command)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                command.Connection = conn;
                int cnt = (int)command.ExecuteScalar();

                conn.Close();
                return cnt;
            }
        }

        public static DataTable GetDataTable(string query)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable myDataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                adapter.SelectCommand = new SqlCommand(query, conn);
                adapter.Fill(myDataTable);
            }
            return myDataTable;
        }



        public static SqlParameter GetNVarCharParam(string name, string val)
        {
            SqlParameter p = new SqlParameter(name, val);
            p.SqlDbType = SqlDbType.NVarChar;

            return p;
        }

        public static SqlParameter GetNCharParam(string name, string val)
        {
            SqlParameter p = new SqlParameter(name, val);
            p.SqlDbType = SqlDbType.NChar;

            return p;
        }

        public static SqlParameter GetNTextParam(string name, string val)
        {
            SqlParameter p = new SqlParameter(name, val);
            p.SqlDbType = SqlDbType.NText;

            return p;
        }

        public static int ExecuteCommand(string query)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                int cnt = (int)cmd.ExecuteNonQuery();

                conn.Close();
                return cnt;
            }
        }

        public static int ExecuteCommand(SqlCommand command)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                command.Connection = conn;

                int cnt = (int)command.ExecuteNonQuery();

                conn.Close();
                return cnt;
            }
        }

        static string deleteCommand = "DELETE {0} WHERE {1}={2}";
        public static bool DeleteRowById(string tableName, string idColName, int id)
        {
            return (1 == DataAccess.ExecuteCommand(string.Format(deleteCommand,
                tableName, idColName, id)));
        }


        static string selectQuery = "SELECT {0}, {1} FROM {2}";
        static string selectQuery3 = "SELECT {0}, {1} FROM {2} WHERE {3}";
        static string selectQuery4 = "SELECT {0}, {1} FROM {2} WHERE {3} ORDER BY {4}";
        static string selectQuery5 = "SELECT Distinct {0}, {1} FROM {2} WHERE {3} ORDER BY {4}";

        public static NameValueCollection SelectKeyValuePairs(
                    string tableName, string keyColName, string valColName)
        {
            return selectKeyValuePairs(string.Format(selectQuery, keyColName, valColName, tableName));
        }

        public static NameValueCollection SelectKeyValuePairs(
                    string tableName, string keyColName, string valColName, string filterExpr)
        {
            return selectKeyValuePairs(string.Format(selectQuery3, keyColName,
                valColName, tableName, filterExpr));
        }

        public static NameValueCollection SelectKeyValuePairs(string tableName, string keyColName,
                string valColName, string filterExpr, string sortExpr)
        {
            return selectKeyValuePairs(string.Format(selectQuery4, keyColName,
                valColName, tableName, filterExpr, sortExpr));
        }

        public static NameValueCollection SelectDistinctKeyValuePairs(string tableName, string keyColName,
        string valColName, string filterExpr, string sortExpr)
        {
            return selectKeyValuePairs(string.Format(selectQuery5, keyColName,
                valColName, tableName, filterExpr, sortExpr));
        }

        static NameValueCollection selectKeyValuePairs(string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            return SelectKeyValuePairs(cmd);

            //using (SqlConnection conn = new SqlConnection(
            //    ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            //{
            //    var col = new NameValueCollection();

            //    conn.Open();
            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    SqlDataReader r = cmd.ExecuteReader();

            //    while (r.Read())
            //        col.Add(r[0].ToString(), r[1].ToString());

            //    r.Close();
            //    conn.Close();

            //    return col;
            //}
        }

        public static NameValueCollection SelectKeyValuePairs(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                var col = new NameValueCollection();

                conn.Open();
                cmd.Connection = conn;
                //SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                    col.Add(r[0].ToString(), r[1].ToString());

                r.Close();
                conn.Close();

                return col;
            }
        }

        static string selectByKeyQuery = "SELECT {0} FROM {1} WHERE {2}={3}";
        public static string SelectValueByKey(string tableName, string keyColName,
                                              string valColName, int key)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    string.Format(selectByKeyQuery, valColName, tableName, keyColName, key), conn);
                SqlDataReader r = cmd.ExecuteReader(CommandBehavior.SingleRow);

                string res = "";
                if (r.Read())
                    res = r[0].ToString();

                r.Close();
                conn.Close();

                return res;
            }
        }
    }

}
