using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using EPM.Data.Model;
using System.ComponentModel;

[Description("Standard KNN Menu")]
public partial class Controls_Menu_StandardKNNMenu : System.Web.UI.UserControl
{
    EPM.Data.Model.EPMEntityModel context;
    private static readonly ILog log = LogManager.GetLogger(typeof(Controls_Menu_StandardKNNMenu));
    protected void Page_Load(object sender, EventArgs e)
    {
        context = new EPM.Data.Model.EPMEntityModel();
        LoadMenu();
    }

    void LoadMenu()
    {

        
        var menus = from sm in context.SiteMenus
                    where sm.ParentId == 0 && sm.Visible == true
                    orderby sm.DispOrder
                    select sm;

        MmRepeater.DataSource = menus.ToList();
        MmRepeater.DataBind();

        MmRepeater2.DataSource = menus.ToList();
        MmRepeater2.DataBind();
        
    }

    protected void MmRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {

            SiteModel.SiteMenu mm = e.Item.DataItem as SiteModel.SiteMenu;
            Repeater r = e.Item.FindControl("SmRepeater") as Repeater;

            var submenus = from sm in context.SiteMenus
                        where sm.ParentId == mm.MenuId && sm.Visible == true
                        orderby sm.DispOrder
                        select sm;

            r.DataSource = submenus;
            r.DataBind();
        }
    }


}