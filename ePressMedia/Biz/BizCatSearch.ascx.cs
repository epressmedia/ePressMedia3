using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Knn.Data;
using Knn.Common;
using EPM.Business.Model.Biz;


public partial class Biz_BizCatSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cat"]))
            {
                try { bindCategory(int.Parse(Request.QueryString["cat"])); }
                catch { }
            }
        }

    }
    protected void CatLinkClick(object sender, EventArgs e)
    {
        bindCategory((sender as LinkButton).ID);
    }
    static string[] indexLetters = { "가", "나", "다", "라", "마", "바", "사", "아", "자", "차", "카", "타", "파", "하" };
    void bindCategory(int catId)
    {
        string name = BECategoryController.GetBusinessCatgoryByID(catId).CategoryName;
        if (name == null)
            return;

        int i;
        for (i = indexLetters.Length - 1; i >= 0; i--)
            if (name.CompareTo(indexLetters[i]) > 0)
                break;
        if (i < 0)  // shouldn't happen
            return;

        string filter = "CatName >= N'" + indexLetters[i] + "' ";
        if (i < indexLetters.Length - 1)    // less than '하'
            filter += "AND CatName < N'" + indexLetters[i + 1] + "'";

        //List<BizCategory> bizCats = BizCategory.SelectBizCategories(filter);
        CatMenuRepeater.DataSource = null;// bizCats;
        CatMenuRepeater.DataBind();

        i++;
        LinkButton l = CatMenuUpdatePanel.FindControl("Lb" + i) as LinkButton;
        l.CssClass = "selected";
    }

    void bindCategory(string senderId)
    {
        for (int i = 1; i < 15; i++)
        {
            LinkButton l = CatMenuUpdatePanel.FindControl("Lb" + i) as LinkButton;
            l.CssClass = "";
        }

        if (string.IsNullOrEmpty(senderId))
        {
            CatMenuRepeater.DataSource = null;
            CatMenuRepeater.DataBind();
            return;
        }

        LinkButton curLink = CatMenuUpdatePanel.FindControl(senderId) as LinkButton;
        curLink.CssClass = "selected";

        string filter = "CatName >= N'" + curLink.CommandName + "' ";

        if (!string.IsNullOrEmpty(curLink.CommandArgument))
            filter += "AND CatName < N'" + curLink.CommandArgument + "'";

        List<BizModel.BusinessCategory> bizCats = null;// BizCategory.SelectBizCategories(filter);
        CatMenuRepeater.DataSource = bizCats;
        CatMenuRepeater.DataBind();
    }
    protected void CatMenuRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (string.IsNullOrEmpty(Request.QueryString["cat"]))
                return;
            int cat;
            try { cat = int.Parse(Request.QueryString["cat"]); }
            catch { return; }

            BizModel.BusinessCategory c = e.Item.DataItem as BizModel.BusinessCategory;
            if (c.CategoryId == cat)
                (e.Item.FindControl("CatLink") as HyperLink).CssClass = "selCat";
        }
    }

}