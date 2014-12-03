using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EPM.Data.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Description("Site Map Display")]

public partial class Controls_Sitemap_StandardKNNSitemap : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadSiteMap();

        Telerik.Web.UI.SiteMapLevelSetting mysetting = RadSiteMap1.LevelSettings[0];
        mysetting.ListLayout.RepeatColumns = NumberOfColumns;

        }
    void LoadSiteMap()
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        var menus = from sm in context.SiteMenus
                    where sm.Visible == true
                    orderby sm.DispOrder
                    select sm;

        RadSiteMap1.DataFieldID = "menuid";
        RadSiteMap1.DataFieldParentID = "parentid";
        RadSiteMap1.DataTextField = "label";
        RadSiteMap1.DataNavigateUrlField = "url";
        RadSiteMap1.DataSource = menus.ToList();
        RadSiteMap1.DataBind();
        
    }

    private int numberOfColumns = 4;
    [Category("EPMProperty"), Description("Number of column to show the top menu"), DefaultValue(typeof(System.Int32), "4"), Required(ErrorMessage = "Number of Images is required.")]
    public int NumberOfColumns
    {
        get { return numberOfColumns; }
        set { numberOfColumns = value; }
    }
}