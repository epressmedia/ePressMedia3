using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;


    public partial class Mobile_News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Control a = LoadControl("Common/MobileNews1.ascx");
                placeholder1.Controls.Add(a);

                //getWeatherConditions();

            }
        }

        //  void getWeatherConditions()
        //{
        //    MobileAppSettings app = new MobileAppSettings();

        //    if (!string.IsNullOrEmpty(app.WeatherCity))
        //    {
        //        var root = XElement.Load("http://www.google.com/ig/api?weather=" + app.WeatherCity);
        //        root.Descendants("current_conditions");


        //        var curElem = root.Element("weather").Element("current_conditions");
        //        var curData = root.Element("weather").Element("forecast_information");
        //        if (!string.IsNullOrEmpty(curElem.ToString()))
        //        {

        //            weatherImg.ImageUrl = "http://www.google.com/" + curElem.Element("icon").Attribute("data").Value;
        //            if (app.TempertureScale == "F")
        //                weatherlbl.Text = "<font style=\"font-size: 12px; color: #999999\">" + curData.Element("city").Attribute("data").Value + "</font><br/>" + curElem.Element("temp_f").Attribute("data").Value + "<font style=\"font-size: 12px; color: #999999\">&deg;F</font>";
        //            else
        //                weatherlbl.Text = "<font style=\"font-size: 12px; color: #999999\">" + curData.Element("city").Attribute("data").Value + "</font><br/>" + curElem.Element("temp_c").Attribute("data").Value + "<font style=\"font-size: 12px; color: #999999\">&deg;C</font>";
        //        }
        //    }
        //    else
        //        weatherImg.Visible = false;

        //}
    }
