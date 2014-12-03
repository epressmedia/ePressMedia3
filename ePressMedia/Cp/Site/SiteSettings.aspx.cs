using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Site
{
    public partial class SiteSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["showhidden"] != null)
            {
                if (Request.QueryString["showhidden"].ToString().ToLower() == "true")
                    OpenAccessLinqDataSource1.Where = "";
            }
        }

        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    GridEditManager manager = item.EditManager;
                    GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("SettingName") as GridTextBoxColumnEditor;
                    SettingName.TextBoxControl.Enabled = false;
                    SettingName.TextBoxControl.Width = Unit.Pixel(800);


                    GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("SettingDescr") as GridTextBoxColumnEditor;
                    SettingDescr.TextBoxControl.Enabled = false;
                    SettingDescr.TextBoxControl.Width = Unit.Pixel(800);


                    GridTextBoxColumnEditor SettingValue = manager.GetColumnEditor("SettingValue") as GridTextBoxColumnEditor;
                    SettingValue.TextBoxControl.Width = Unit.Pixel(800);

                  

                }
            }
        }

        protected void OpenAccessLinqDataSource1_Updating(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceUpdateEventArgs e)
        {
            SiteModel.SiteSetting newsettings = (SiteModel.SiteSetting)e.NewObject;

            newsettings.ExposeUI_fg = true;
        }
    }
}