using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Ad
{
    public partial class Banner : System.Web.UI.Page
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

                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Ad/BannerEntry.aspx?mode=" + action, listing_div, "OnClientclose");
            }
            else if (action == "edit")
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Ad/BannerEntry.aspx?mode=edit&id=" + BannerId, listing_div, "OnClientclose");
            }
            else if (action == "delete")
            {

                EPM.Business.Model.Ad.BannerContoller.DeleteBanner(BannerId);
                BannerGrid.Rebind();
                toolbox1.EnableButtons("Add", true);
            }

        }

        protected void BannerGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDataItem item = (GridDataItem)BannerGrid.SelectedItems[0];
            toolbox1.EnableButtons("Edit",false);
            toolbox1.EnableButtons("Delete", false);

            BannerId = int.Parse(item.GetDataKeyValue("AdBannerId").ToString());
        }


        private static int bannerId;
        public static int BannerId
        {
            get;
            set;
        }


      
    }
}