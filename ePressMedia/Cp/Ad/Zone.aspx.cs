using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Ad
{
    public partial class Zone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                toolbox1.EnableButtons("Add", true);


            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);

        }

        protected void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();


            if (action == "add")
            {

                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Ad/ZoneEntry.aspx?mode=" + action, listing_div, "OnClientclose");
            }
            else if (action == "edit")
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Ad/ZoneEntry.aspx?mode=edit&id=" + ZoneId, listing_div, "OnClientclose");
            }

        }

        protected void ZoneGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDataItem item = (GridDataItem)ZoneGrid.SelectedItems[0];
            toolbox1.EnableButtons("Edit", false);

            ZoneId = int.Parse(item.GetDataKeyValue("AdZoneId").ToString());
        }


        private static int zoneId;
        public static int ZoneId
        {
            get;
            set;
        }
    }
}