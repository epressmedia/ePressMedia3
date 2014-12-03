using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Controls_VideoAdBox : System.Web.UI.UserControl
{
    string VideoAdCount
    {
        get { return AdCount.Value; }
        set { AdCount.Value = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<BizModel.BusinessEntity> biz = null;// BizDAL.SelectBizForVideoAd(int.Parse(AdCount.Value));
            DescRepeater.DataSource = biz;
            DescRepeater.DataBind();

            ImgRepeater.DataSource = biz;
            ImgRepeater.DataBind();
        }
    }
}