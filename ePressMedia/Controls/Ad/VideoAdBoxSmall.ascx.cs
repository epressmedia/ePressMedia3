using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Controls_VideoAdBoxSmall : System.Web.UI.UserControl
{
    string VideoAdCount
    {
        get { return AdCount.Value; }
        set { AdCount.Value = value; }
    }
    private string title;
    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    
    public string ItemPerPage
    {
        set { VideoAdBoxSmall_itemPerPage.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VideoAdBoxSmall_lbl.Text = Title;

            List<BizModel.BusinessEntity> biz = null; // BizDAL.SelectBizForVideoAd(int.Parse(AdCount.Value));
            DescRepeater.DataSource = biz;
            DescRepeater.DataBind();

            ImgRepeater.DataSource = biz;
            ImgRepeater.DataBind();
        }
    }
}