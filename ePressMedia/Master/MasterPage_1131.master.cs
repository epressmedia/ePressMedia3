using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;






public partial class MasterPage_1131 : EPM.Core.EPMMasterBasePage
{


    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //bindMenu();
     
    }
    

    //void bindMenu()
    //{
    //    Menu.Controls.Add(Page.LoadControl("~/Controls/iKoreatimesHeader.ascx"));
    //    Menu.Controls.Add(Page.LoadControl("~/Controls/Menu/StandardKNNMenu.ascx"));
    //    RightBarContent.Controls.Add(Page.LoadControl("~/Controls/iKoreatimesTempAds.ascx"));
    //}







}
