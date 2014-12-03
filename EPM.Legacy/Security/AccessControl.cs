using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using EPM.Legacy.Data;


namespace EPM.Legacy.Security
{
    public enum ResourceType
    {
        Article = 1,
        Forum = 2,
        Classified = 3,
        Calendar = 4
    }

    public enum Permission
    {
        None = 0x0000,
        FullControl = 0x0001,
        List = 0x0002,
        Read = 0x0004, //View
        Write = 0x0008,
        Comment = 0x0010,
        Modify = 0x0020,
        Delete = 0x0040,
        Reply = 0x0080,
    }

    public class AccessControl
    {
        public int Id { get; set; }
        public string PrincipalName { get; set; }
        public ResourceType ResourceType { get; set; }
        public int ResourceId { get; set; }
        public Permission Permissions { get; set; }

        public bool HasFullControl { get { return HasPermission(Permission.FullControl); } }
        public bool CanList { get { return HasPermission(Permission.List); } }
        public bool CanRead { get { return HasPermission(Permission.Read); } }
        public bool CanWrite { get { return HasPermission(Permission.Write); } }
        public bool CanComment { get { return HasPermission(Permission.Comment); } }
        public bool CanModify { get { return HasPermission(Permission.Modify); } }
        public bool CanDelete { get { return HasPermission(Permission.Delete); } }
        public bool CanReply { get { return HasPermission(Permission.Reply); } }

        public AccessControl(Permission p)
        {
            this.Permissions = p;
        }

        AccessControl(int id, ResourceType resType, int resId, string prName, Permission p)
        {
            Id = id; ResourceType = resType; ResourceId = resId;
            PrincipalName = prName; Permissions = p;
        }

        public static bool AuthorizeUser(string userName, ResourceType resourceType,
                        int resourceId, Permission p)
        {
            //string groupName = "";

            bool group_permission = false;
            string[] groupNames;
            if (!string.IsNullOrEmpty(userName))
            {
                groupNames = System.Web.Security.Roles.GetRolesForUser(userName);

                //Added Looping to support one to many user role relationship
                if (groupNames.Length > 0)
                {
                    foreach (string groupName in groupNames)
                    {
                        group_permission = AuthorizeUserGroup(groupName, resourceType, resourceId, p);

                        if (group_permission)
                            break;
                    }

                    //groupName = groupNames[0];
                }
            }
            else
            {
                string groupName = Security.UserRoles.GetDefaultGuestGroupName();// ConfigurationManager.AppSettings["GuestGroupName"];
                group_permission = AuthorizeUserGroup(groupName, resourceType, resourceId, p);

            }

            return group_permission;

        }

        public static bool AuthorizeUserGroup(string groupName, ResourceType resourceType,
                        int resourceId, Permission p)
        {
            AccessControl ac = SelectAccessControlByGroupName(groupName, resourceType, resourceId);
            if (ac == null) // no result
            {
                if (groupName == string.Empty)  // empty group name means anonymous user
                    return false;

                // try again as anonymous user
                ac = SelectAccessControlByGroupName(string.Empty, resourceType, resourceId);
                if (ac == null) // no result
                    return false;
            }

            return ac.HasPermission(p);
        }


        static string selectQuery =
            "SELECT * FROM AccessControlList WHERE ResType={0} AND ResId={1}";
        public static List<AccessControl> SelectAccessControlList(ResourceType resourceType,
                                                int resourceId)
        {
            return DataAccess.SelectCollection<AccessControl>(
                string.Format(selectQuery, (int)resourceType, resourceId), getAccessControlFromReader);
        }

        static string selectByGrouNameQueryFormat =
        @"SELECT * FROM AccessControlList WHERE ResType={0} AND ResId={1} AND PrincipalName=N'{2}'";
        public static AccessControl SelectAccessControlByGroupName(string groupName,
            ResourceType resourceType, int resourceId)
        {
            if (groupName == string.Empty)
                groupName = Security.UserRoles.GetDefaultGuestGroupName();// ConfigurationManager.AppSettings["GuestGroupName"];

            return DataAccess.SelectInstanceById<AccessControl>(
                string.Format(selectByGrouNameQueryFormat, (int)resourceType, resourceId, groupName),
                        getAccessControlFromReader);
        }

        public static AccessControl SelectAccessControlByUserName(string userName,
            ResourceType resourceType, int resourceId)
        {
            string groupName = string.Empty;
            if (!string.IsNullOrEmpty(userName))
            {
                string[] groupNames = System.Web.Security.Roles.GetRolesForUser(userName);
                if (groupNames.Length > 0)
                    groupName = groupNames[0];
            }

            AccessControl ac = SelectAccessControlByGroupName(groupName, resourceType, resourceId);
            if (ac == null && groupName != string.Empty)    // no ACL by that group
                // try again as anonymous user
                ac = SelectAccessControlByGroupName(string.Empty, resourceType, resourceId);

            return ac;
        }


        static string insertCommand =
            @"INSERT INTO AccessControlList (ResType, ResId, PrincipalName) VALUES
            (@ResType, @ResId, @PrincipalName);SET @NewId=SCOPE_IDENTITY()";
        public static int InsertAccessControl(string principalName, ResourceType resourceType,
            int resourceId)
        {
            SqlCommand cmd = new SqlCommand(insertCommand);

            cmd.Parameters.Add(new SqlParameter("@ResType", (int)resourceType));
            cmd.Parameters.Add(new SqlParameter("@ResId", resourceId));
            cmd.Parameters.Add(DataAccess.GetNVarCharParam("@PrincipalName", principalName));

            SqlParameter outParam = new SqlParameter("@NewId", SqlDbType.Int);
            outParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outParam);

            if (1 == DataAccess.ExecuteCommand(cmd))
                return (int)outParam.Value;
            else
                return 0;
        }

        static Permission getPermission(bool fullControl, bool list, bool read,
            bool write, bool comment, bool modify, bool delete, bool reply)
        {
            Permission p = Permission.None;

            if (fullControl) p |= Permission.FullControl;
            if (list) p |= Permission.List;
            if (read) p |= Permission.Read;
            if (write) p |= Permission.Write;
            if (comment) p |= Permission.Comment;
            if (modify) p |= Permission.Modify;
            if (delete) p |= Permission.Delete;
            if (reply) p |= Permission.Reply;

            return p;
        }

        public static bool UpdatePermissions(int acId, bool fullControl, bool list, bool read,
            bool write, bool comment, bool modify, bool delete, bool reply)
        {
            return UpdatePermissions(acId, getPermission(fullControl, list, read,
                    write, comment, modify, delete, reply));
        }

        static string updateCommand =
            @"UPDATE AccessControlList SET Permissions=@Permissions WHERE Id=@Id";
        public static bool UpdatePermissions(int acId, Permission permissions)
        {
            SqlCommand cmd = new SqlCommand(updateCommand);

            cmd.Parameters.Add(new SqlParameter("@Permissions", permissions));
            cmd.Parameters.Add(new SqlParameter("@Id", acId));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }

        public static bool DeletePermission(int acId)
        {
            return DataAccess.DeleteRowById("AccessControlList", "Id", acId);
        }

        static string delByResCommand =
            "DELETE AccessControlList WHERE ResType=@ResType AND ResId=@ResId";
        public static bool DeletePermissionByResource(ResourceType resourceType, int resourceId)
        {
            SqlCommand cmd = new SqlCommand(delByResCommand);

            cmd.Parameters.Add(new SqlParameter("@ResType", (int)resourceType));
            cmd.Parameters.Add(new SqlParameter("@ResId", resourceId));

            return (1 == DataAccess.ExecuteCommand(cmd));
        }

        //public static bool HasPermission(string principalName, Permission permission)
        //{
        //    return true;
        //}

        public bool HasPermission(Permission permission)
        {

            return ((Permissions & permission) != Permission.None ||
                    (Permissions & Permission.FullControl) == Permission.FullControl);
        }

        static AccessControl getAccessControlFromReader(SqlDataReader r)
        {
            return new AccessControl((int)r["Id"],
                        (ResourceType)r["ResType"], (int)r["ResId"],
                        r["PrincipalName"].ToString(), (Permission)r["Permissions"]);
        }

    }
}
