using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;



public partial class Biz_BizRankBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RecRepeater.DataSource = BEController.GetALLActiveBEs().Where(x=>x.ApprovedFg).OrderByDescending(x => x.CreatedDate).Take(10);// 
            RecRepeater.DataBind();

            MostViewRepeater.DataSource = BEController.GetMostViewBEsInDays(7).Take(10);
            MostViewRepeater.DataBind();
        }
    }
    protected void RecRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
    e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image i = e.Item.FindControl("IdxImg") as Image;
            i.ImageUrl = "~/img/b_num" + (e.Item.ItemIndex + 1) + ".png";
        }
    }
    protected void MostViewRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      
        if (e.Item.ItemType == ListItemType.Item ||
    e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image i = e.Item.FindControl("IdxImg") as Image;
            i.ImageUrl = "~/img/b_num" + (e.Item.ItemIndex + 1) + ".png";
        }
    }
}