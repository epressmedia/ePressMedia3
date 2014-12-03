using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Core.Common
{
    public static class SiteSettingHelper
    {

        public static void CreateSettingField(string field_name, string description)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (context.SiteSettings.Count(c => c.SettingName == field_name) == 0)
            {
                SiteModel.SiteSetting setting = new SiteModel.SiteSetting();
                setting.SettingName = field_name;
                setting.SettingValue = "";
                setting.SettingDescr = description;
                setting.ExposeUI_fg = false;

                context.Add(setting);
                context.SaveChanges();
            }
        }

        public static void UpdateSettingValue(string field_name, string value)
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            SiteModel.SiteSetting setting = context.SiteSettings.Single(c => c.SettingName == field_name);

            if (setting == null)
            {
                CreateSettingField(field_name, field_name);
            }

                setting.SettingValue = value;
                context.SaveChanges();
            
        }
    }
}
