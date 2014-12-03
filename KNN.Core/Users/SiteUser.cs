using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

using EPM.Legacy.Data;


namespace EPM.Core.Users
{

    /// <summary>
    /// ObjectDataSource 에서 CRUD 메소드를 Mapping 하기 위해 만든 클래스
    /// 단순히 ASP.NET 의 멤버쉽프레임웍의 메소드를 호출하는 역할만 한다.
    /// </summary>
    public class SiteUser
    {
        public string UserName { get; private set; }
        public string UserId { get; private set; }
        public string RoleName { get; private set; }
        public DateTime LastLoginDate { get; private set; }
        public string Email { get; private set; }

        static string selectPageQuery =
        @"
    SELECT * FROM
    (
    SELECT u.UserId, u.UserName, r.RoleName, m.Email, m.LastLoginDate, m.IsApproved,
    ROW_NUMBER() OVER (ORDER BY {0}) AS RowNum
    FROM aspnet_users u 
    JOIN aspnet_usersinroles ur ON u.UserId=ur.UserId 
    JOIN aspnet_roles r ON ur.RoleId=r.RoleId
    JOIN aspnet_membership m ON u.UserId=m.UserId
    {1}
    ) Tbl
    WHERE RowNum BETWEEN {2} AND {3}";

        static string selectCountQuery =
        @"SELECT COUNT(*) FROM aspnet_users u JOIN aspnet_Membership m ON u.UserId=m.UserId {0}";

        public static int GetUserCount(string filterExpr)
        {
            return DataAccess.ExecuteScalar(string.Format(selectCountQuery, filterExpr));
        }

        public static List<SiteUser> SelectUsers(string filterExpr, string sortExpr, int pageNum, int rowsPerPage)
        {
            int startRowIndex = (pageNum - 1) * rowsPerPage + 1;

            string query = string.Format(selectPageQuery, sortExpr, filterExpr,
                startRowIndex, startRowIndex + rowsPerPage - 1);

            return DataAccess.SelectCollection<SiteUser>(query, getFromReader);
        }

        public static MembershipUserCollection GetAllUsers()
        {
            
            return Membership.GetAllUsers();
        }

        public static void UpdateUserInfo(string userName, string email, bool isapproved)
        {
            MembershipUser user = Membership.GetUser(userName);
            user.Email = email;
            user.IsApproved = isapproved;

            Membership.UpdateUser(user);
        }

        public static void DeleteUser(string userName)
        {
            Membership.DeleteUser(userName, true);
        }

        public static MembershipUserCollection GetUsersByName(string searchName, int pageNum, int rowsPerPage, out int totalRows)
        {
            return Membership.FindUsersByName("%" + searchName + "%", pageNum, rowsPerPage, out totalRows);
        }

        public static MembershipUserCollection GetUsersByEmail(string searchAddr, int pageNum, int rowsPerPage, out int totalRows)
        {
            return Membership.FindUsersByEmail("%" + searchAddr + "%", pageNum, rowsPerPage, out totalRows);
        }

        static SiteUser getFromReader(SqlDataReader r)
        {
            SiteUser u = new SiteUser();
            u.UserId = r["UserId"].ToString();
            u.UserName = r["UserName"].ToString();
            u.Email = r["Email"].ToString();
            u.RoleName = r["RoleName"].ToString();
            u.LastLoginDate = (DateTime)r["LastLoginDate"];

            return u;
        }
    }
}