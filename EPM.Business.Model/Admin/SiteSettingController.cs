using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Business.Model.Admin
{
    public static class SiteSettingController
    {
        public static void UpdateSiteSetting(string settingname, string settingvalue)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            SiteModel.SiteSetting setting = new SiteModel.SiteSetting();
            setting = context.SiteSettings.Single(c => c.SettingName == settingname);
            setting.SettingValue = settingvalue;
            context.SaveChanges();
        }
        public static string GetSiteSettingValueByName(string settingname)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string result = "";
            List<SiteModel.SiteSetting> sitesetting = context.SiteSettings.Where(c => c.SettingName == settingname).ToList();
            if (sitesetting.Count() > 0)
            {
                if (sitesetting[0].SettingValue != null)
                    result = sitesetting[0].SettingValue.ToString();
            }

            return result;
        }
    }
}
