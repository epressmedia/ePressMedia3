using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Knn;
using Knn.BizAd;

public partial class Cp_Biz_EditAdPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AdTypeList.DataSource = BizAdPriceDAL.AdTypes;
            AdTypeList.DataBind();

            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                BizAdPrice p = BizAdPriceDAL.SelectAdPriceById(id);

                AdTypeList.SelectedIndex = p.AdType - 1;
                AdName.Text = p.AdName;
                SizeInMm.Text = p.SizeInMm;
                SizeInInch.Text = p.SizeInInch;
                Price.Text = p.Price.ToString("0.00");
            }
            catch
            {
                SaveButton.Enabled = false;
            }
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        bool res = BizAdPriceDAL.UpdateAdPrice(int.Parse(Request.QueryString["id"]), AdName.Text,
            AdTypeList.SelectedIndex + 1, SizeInMm.Text, SizeInInch.Text,
            double.Parse(Price.Text));

        Utility.RegisterJsResultAlert(this, res, "저장 되었습니다.", "저장 중 오류가 발생했습니다", "AdPrices.aspx");
    }
}