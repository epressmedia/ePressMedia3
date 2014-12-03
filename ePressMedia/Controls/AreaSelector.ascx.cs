using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Knn.Common;

public partial class Controls_AreaSelector : System.Web.UI.UserControl
{
    protected string _areaID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Header.DataBind();



            List<GeoModel.Ref_province> provinces = EPM.Core.Common.GeoUtility.GetAllProvinces();
            cbo_province.Items.Insert(0, new ListItem("-- Select State/Province -- ", "0"));
            cbo_province.DataValueField = "Province_cd";
            cbo_province.DataTextField = "Province_name";
            cbo_province.DataSource = provinces;
            cbo_province.DataBind();



            //if (AreaID.Length > 0)
            //{
            //    NameValueCollection nvc = Geo.GetAreaInfoByAreaID(AreaID);
            //    cbo_province.SelectedValue = nvc.GetKey(0).ToString();
            //    txtCity.Text = nvc[0].ToString();
            //}

        }
    }

    public Controls_AreaSelector() 
        { 
 
            _areaID = string.Empty;
        } 
 
 
        public string AreaID 
        { 
            get { return _areaID; } 
            set { _areaID = value; } 
        } 


    protected void cbo_province_SelectedIndexChanged(object sender, EventArgs e)
    {
        CityAutoComplete.ContextKey = cbo_province.SelectedValue.ToString();
        txtCity.Text = "";
    }

    protected void validate_SelectedArea(object sender, ServerValidateEventArgs e)
    {

        List<GeoModel.Ref_city> cities = EPM.Core.Common.GeoUtility.ValidateProvinceCity(cbo_province.SelectedValue.ToString(), txtCity.Text.Trim());


        if (cities.Count > 0)
        {

            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }

        //NameValueCollection col =  Geo.ValidateProvinceCity(cbo_province.SelectedValue.ToString(), txtCity.Text.Trim());
        //if (col.Count > 0)
        //{
        //    AreaID = col[0].ToString();
        //    e.IsValid = true;
        //}
        //else
        //{
        //    AreaID = string.Empty;
        //    e.IsValid = false;
        //}
    }
}