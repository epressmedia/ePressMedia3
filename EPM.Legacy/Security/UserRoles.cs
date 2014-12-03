using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EPM.Legacy.Security
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
            return GetSiteSettingValueByName("Default Admin Group Name");
        }

        public static string GetDefaultUserGroupName()
        {
            return GetSiteSettingValueByName("Default User Group Name");
        }

        public static string GetDefaultGuestGroupName()
        {
            return GetSiteSettingValueByName("Default Guest Group Name");
        }
        public static string GetSiteSettingValueByName(string settingname)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string result = "";
            List<SiteModel.SiteSetting> sitesetting = context.SiteSettings.Where(c => c.SettingName == settingname).ToList();
            if (sitesetting.Count() > 0)
            {
                result = sitesetting[0].SettingValue.ToString();
            }

            return result;
        }
    }
}
