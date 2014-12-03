using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Knn;
using Knn.Data;
using Knn.Security;

namespace Knn.Article
{
    public class ArticleCategory
    {
        public int CatId { get; set; }
        public string CatName { get; set; }

        static string insertCommand =
        "INSERT INTO ArticleCategories (CatName) VALUES(@CatName); SET @NewId=SCOPE_IDENTITY();";

        public static int InsertCategory(string catName, bool createDefaultAcl)
        {
            SqlCommand cmd = new SqlCommand(insertCommand);

            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@CatName", catName));

            SqlParameter outParam = new SqlParameter("@NewId", SqlDbType.Int);
            outParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outParam);

            int id = (1 == DataAccess.ExecuteCommand(cmd)) ? (int)outParam.Value : 0;

            if (id > 0 && createDefaultAcl)
            {
                string priName = ConfigurationManager.AppSettings["AdminGroupName"];
                if (!string.IsNullOrEmpty(priName))
                {
                    int aclId = AccessControl.InsertAccessControl(priName, ResourceType.Article, id);
                    if (aclId > 0)      // success
                        AccessControl.UpdatePermissions(aclId, true, false, false,
                            false, false, false, false, false);
                }

                priName = ConfigurationManager.AppSettings["GuestGroupName"];
                if (!string.IsNullOrEmpty(priName))
                {
                    int aclId = AccessControl.InsertAccessControl(priName, ResourceType.Article, id);
                    if (aclId > 0)      // success
                        AccessControl.UpdatePermissions(aclId, false, true, true,
                            false, true, false, false, false);
                }
            }

            return id;
        }

        static string updateCommand = 
            "UPDATE ArticleCategories SET CatName=@CatName WHERE CatId=@CatId";
        
        public static bool UpdateCategory(int categoryId, string catName)
        {
            SqlCommand cmd = new SqlCommand(updateCommand);

            cmd.Parameters.Add(new SqlParameter("@CatId", categoryId));
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@CatName", catName));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }

        public static bool DeleteCategory(int categoryId)
        {
            bool res = DataAccess.DeleteRowById("ArticleCategories", "CatId", categoryId);

            AccessControl.DeletePermissionByResource(ResourceType.Article, categoryId);

            return res;
        }

        //static string selectQuery = "SELECT * FROM ArticleCategories";
        
        public static NameValueCollection SelectCategories()
        {
            return DataAccess.SelectKeyValuePairs("ArticleCategories", "CatId", "CatName");
        }

        //static string selectByIdQuery = "SELECT CatName FROM ArticleCategories WHERE CatId=";
        public static string GetCategoryName(int catId)
        {
            return DataAccess.SelectValueByKey("ArticleCategories", "CatId", "CatName", catId);
        }
    }
}
