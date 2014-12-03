using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EPM.Business.Model.Biz;

using Knn.Common;


public partial class Biz_BizSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //SearchName.Focus();
        if (!IsPostBack)
        {

            bindBizCat();
            bindState();

            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
                SearchName.Text = Request.QueryString["q"].ToString();

        }
    }

    void bindBizCat()
    {
        BizCatList.DataSource = BECategoryController.GetAllBusinessCatgories();// BizCategory.SelectBizCategories("1=1");
        BizCatList.DataValueField = "CategoryId";
        BizCatList.DataTextField = "CategoryName";
        BizCatList.DataBind();

        BizCatList.Items.Insert(0, new ListItem("전체", "0"));

        if (!string.IsNullOrEmpty(Request.QueryString["cat"]))
            BizCatList.SelectedValue = Request.QueryString["cat"].ToString();
    }

    void bindState()
    {

        List<GeoModel.Ref_province> provinces = EPM.Business.Model.Common.GeoController.GetAllProvinces();

        cbo_province.DataValueField = "Province_cd";
        cbo_province.DataTextField = "Province_name";
        cbo_province.DataSource = provinces;
        cbo_province.DataBind();

        cbo_province.Items.Insert(0, new ListItem("-- All -- ", "All"));



        if (!string.IsNullOrEmpty(Request.QueryString["st"]))
        {
            //StateList.SelectedValue = Request.QueryString["st"].ToString();
            cbo_province.SelectedValue = Request.QueryString["st"].ToString();
            CityAutoComplete.ContextKey = cbo_province.SelectedValue.ToString();
            LoadArea();
            if (!string.IsNullOrEmpty(Request.QueryString["area"]))
            {
                //AreaList.SelectedValue = Request.QueryString["areaid"].ToString();
                txtCity.Text = Request.QueryString["area"].ToString();
            }
        }

    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        string s = SearchName.Text.Trim();

        StringBuilder sb = new StringBuilder("Biz.aspx?q=");
        sb.Append(Server.UrlEncode(s));

        if (BizCatList.SelectedIndex > 0)
            sb.Append("&cat=" + BizCatList.SelectedValue);
        //if (StateList.SelectedIndex > 0)
        //    sb.Append("&st=" + StateList.SelectedValue);
        if (cbo_province.SelectedIndex > 0)
            sb.Append("&st=" + cbo_province.SelectedValue);
        if (txtCity.Text.Length > 0)
        {

            
        //List<GeoModel.Ref_city> cities = KNN.Core.Common.GeoUtility.ValidateProvinceCity(cbo_province.SelectedValue.ToString(), txtCity.Text.Trim());


        if(txtCity.Text.Trim().Length > 0)// (cities.Count > 0)
        {
            sb.Append("&area=" + txtCity.Text.Trim());//cities[0].City_id.ToString());
        }

            //NameValueCollection nv_areaid = Geo.ValidateProvinceCity(cbo_province.SelectedValue, txtCity.Text);
            //if (nv_areaid.Count > 0)
            //    sb.Append("&areaid=" + nv_areaid[0].ToString());
        }
        //if (AreaList.SelectedIndex > 0)
        //    sb.Append("&areaid=" + AreaList.SelectedValue);

        Response.Redirect(sb.ToString());


        //Response.Redirect(string.Format("Default.aspx?q={0}&st={1}&area={2}&cat={3}", 
        //    Server.UrlEncode(s), StateList.SelectedValue, AreaList.SelectedValue, BizCatList.SelectedValue));
    }
    protected void StateList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        //CityAutoComplete.ContextKey = StateList.SelectedItem.ToString();
        txtCity.Text = "";

    }

    void LoadArea()
    {
        //AreaList.Items.Clear();

        //if (StateList.SelectedIndex > 0)
        //{
        //    AreaList.Enabled = true;


        //    //NameValueCollection nvc = Geo.GetCitiesByProvince("BC");

        //    //AreaList.Items.Add(new ListItem("Ohter", ""));
        //    //foreach (string key in nvc)
        //    //    AreaList.Items.Add(new ListItem(nvc[key], key));

        //    //AreaList.DataSource = Area.SelectAreas(StateList.SelectedValue);
        //    //AreaList.DataValueField = "Id";
        //    //AreaList.DataTextField = "Name";
        //    //AreaList.DataBind();

        //    AreaList.Items.Insert(0, new ListItem("전체", "0"));
        //}
        //else
        //{
        //    AreaList.Enabled = false;
        //}
    }

    protected void cbo_province_SelectedIndexChanged(object sender, EventArgs e)
    {
        CityAutoComplete.ContextKey = cbo_province.SelectedValue.ToString();
        txtCity.Text = "";
    }

    private string _areaID = string.Empty;
    public string AreaID
    {
        get { return _areaID; }
        set { _areaID = value; }
    } 

    

}