using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Cp_Biz_AddAdPrices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AdName.Focus();
        if (!IsPostBack)
        {
            AdTypeList.DataSource = BizAdPriceDAL.AdTypes;
            AdTypeList.DataBind();
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        bool res = BizAdPriceDAL.InsertAdPrice(AdName.Text, AdTypeList.SelectedIndex + 1, SizeInMm.Text, SizeInInch.Text,
            double.Parse(Price.Text));

        Utility.RegisterJsResultAlert(this, res, "등록되었습니다", "등록중 오류가 발생했습니다", "AdPrices.aspx");
    }
}