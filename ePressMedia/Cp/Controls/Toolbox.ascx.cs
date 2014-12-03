using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using EPM.Core.CP;

namespace ePressMedia.Cp.Controls
{
    public partial class Toolbox : ToolBarController
    {


        //delagate


        public event Telerik.Web.UI.RadToolBarEventHandler ToolBarClicked;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (ToolBarClicked != null)
            {
                ToolBarClicked(this, e);
            }
            
            
        }

    }
}