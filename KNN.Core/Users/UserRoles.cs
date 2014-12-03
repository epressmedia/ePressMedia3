using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPM.Business.Model.Admin;

namespace EPM.Core.Users
{
    public class UserRoles
    {

        
        public static List<String> DefaultUserRoles()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();    
            List<String> userRoles = (from c in context.SiteSettings
                                      where (c.SettingName == "Default Guest Group Name" || c.SettingName == "Default Admin Group Name" || c.SettingName == "Default User Group Name") && c.SettingValue.Length > 0
                                      select c.SettingValue).ToList<String>();
            return userRoles;
        }

        public static string GetDefaultAdminGroupName()
        {
            return SiteSettingController.GetSiteSettingValueByName("Default Admin Group Name");
        }

        public static string GetDefaultUserGroupName()
        {
            return SiteSettingController.GetSiteSettingValueByName("Default User Group Name");
        }

        public static string GetDefaultGuestGroupName()
        {
            return SiteSettingController.GetSiteSettingValueByName("Default Guest Group Name");
        }
    }
}
