using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using EPM.Core.Classified;

public partial class Classified_PostAd : ClassifiedPostPage
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {

                Control myPanel = Page.LoadControl("~/Classified/Controls/ClassifiedPost.ascx");
                myPanel.ID = "AddEditPanel";
                PlaceHolder.Controls.Add(myPanel);
            
        
    }


}