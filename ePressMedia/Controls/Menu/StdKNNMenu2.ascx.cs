using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Menu_tester : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this.Page);
        //sm.EnablePartialRendering = false;

        LoadMenu();
    }


    void LoadMenu()
    {


        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        var menus = from sm in context.SiteMenus
                    where sm.Visible == true
                    orderby sm.DispOrder
                    select sm;


        RadMenu1.DataFieldID = "menuid";
        RadMenu1.DataFieldParentID = "parentid";
        RadMenu1.DataTextField = "label";
        RadMenu1.DataNavigateUrlField = "url";
        RadMenu1.DataSource = menus.ToList();
        RadMenu1.DataBind();


    }
}