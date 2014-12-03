using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;  
using System.Xml.Linq;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


[Description("Weather Control")]
    public partial class Weather : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_header.Text = HeaderName;
            //bindWeather();
            bindMsnWeather();
            
        }

        void bindMsnWeather()
        {
            try
            {

                 string t_type = "C";
                 string culture = EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Culture Info").ToString();
                if (Unit == Units.Fahrenheit)
                    t_type = "F";


                var root = XElement.Load(@"http://weather.service.msn.com/data.aspx?weadegreetype=" + t_type + @"&culture=" + culture + "&weasearchstr=" + City + "");
                root.Descendants("weatherdata");

                lbl_cityname.Text = root.Element("weather").Attribute("weatherlocationname").Value;
                //string _Unit = root.Element("weather").Element("forecast_information").Element("unit_system").Attribute("data").Value;
                var curElem = root.Element("weather").Element("current");
                current_icon.Src = "http://wst.s-msn.com/i/en-us/law/" + curElem.Attribute("skycode").Value + ".gif";
                // old http://blst.msn.com/as/wea3/i/en-us/law/
                current_icon.Alt = curElem.Attribute("skytext").Value;
                lbl_current_weather.Text = curElem.Attribute("skytext").Value;
                lbl_curent_wind.Text = curElem.Attribute("winddisplay").Value;
                lbl_current_temp.Text = Unit == Units.Fahrenheit ? curElem.Attribute("temperature").Value + "&deg;F" : curElem.Attribute("temperature").Value + "&deg;C";
                lbl_current_humidity.Text = curElem.Attribute("humidity").Value+" %";

                //bool convertToC = false;
                //bool convertToF = false;

                //if ((_Unit == "US") && (Unit == Units.Celsius))
                //    convertToC = true;
                //if ((_Unit == "SI") && (Unit == Units.Fahrenheit))
                //    convertToF = true;

                var forecast = (from condition in root.Descendants("forecast")
                                select new
                                {
                                    DayLabel = condition.Attribute("shortday").Value,
                                    Low = condition.Attribute("low").Value,
                                    High = condition.Attribute("high").Value,
                                    IconUrl = "http://wst.s-msn.com/i/en-us/law/" + condition.Attribute("skycodeday").Value + ".gif",
                                    Condition = condition.Attribute("skytextday").Value
                                }).Skip(1).Take(NumOfForecast).ToList();


                Weather_Repeater.DataSource = forecast;
                Weather_Repeater.DataBind();
                
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                weatherSection.Visible = false;
            }
        }
        //void bindWeather()
        //{
        //    try
        //    {


        //        var root = XElement.Load(@"http://www.google.com/ig/api?weather=" + City + "&hl=" + ISOCode.ToString());
        //        root.Descendants("current_conditions");

        //        lbl_cityname.Text = root.Element("weather").Element("forecast_information").Element("city").Attribute("data").Value;
        //        string _Unit = root.Element("weather").Element("forecast_information").Element("unit_system").Attribute("data").Value;
        //        var curElem = root.Element("weather").Element("current_conditions");
        //        current_icon.Src = "http://www.google.com" + curElem.Element("icon").Attribute("data").Value;
        //        lbl_current_weather.Text = curElem.Element("condition").Attribute("data").Value;
        //        lbl_curent_wind.Text = curElem.Element("wind_condition").Attribute("data").Value;
        //        lbl_current_temp.Text = Unit == Units.Fahrenheit ? curElem.Element("temp_f").Attribute("data").Value + "&deg;F" : curElem.Element("temp_c").Attribute("data").Value + "&deg;C";
        //        lbl_current_humidity.Text = curElem.Element("humidity").Attribute("data").Value;

        //        bool convertToC = false;
        //        bool convertToF = false;

        //        if ((_Unit == "US") && (Unit == Units.Celsius))
        //            convertToC = true;
        //        if ((_Unit == "SI") && (Unit == Units.Fahrenheit))
        //            convertToF = true;

        //        var forecast = (from condition in root.Descendants("forecast_conditions")
        //                        select new
        //                        {
        //                            DayLabel = condition.Element("day_of_week").Attribute("data").Value,
        //                            Low = convertToC == true ? FahrenheitToCelsius(condition.Element("low").Attribute("data").Value) : (convertToF == true ? CelsiusToFahrenheit(condition.Element("low").Attribute("data").Value) : condition.Element("low").Attribute("data").Value),
        //                            High = convertToC == true ? FahrenheitToCelsius(condition.Element("high").Attribute("data").Value) : (convertToF == true ? CelsiusToFahrenheit(condition.Element("high").Attribute("data").Value) : condition.Element("high").Attribute("data").Value),
        //                            IconUrl = condition.Element("icon").Attribute("data").Value,
        //                            Condition = condition.Element("condition").Attribute("data").Value
        //                        }).Take(NumOfForecast).ToList();


        //        Weather_Repeater.DataSource = forecast;
        //        Weather_Repeater.DataBind();
        //        //WeatherRep.DataSource = getWeatherConditions();
        //        //WeatherRep.DataBind();
        //    }
        //    catch
        //    {
        //        weatherSection.Visible = false;
        //    }
        //}

        public enum Units
        {
            Fahrenheit = 0,
            Celsius = 1
        }

        private Units unit = Units.Fahrenheit;
        [Category("EPMProperty"), Description("ISO Language Code"), DefaultValue(typeof(Units), "Fahrenheit")]
        public Units Unit
        {
            get;
            set;
        }


        private string city;
        [Category("EPMProperty"), Description("City Name"), Required(ErrorMessage = "City name is required. (e.g. Vancouver,BC)")]
        public string City
        {
            get;
            set;
        }


        private string headerName;
        [Category("EPMProperty"), Description("Name displayed at the top of the control"), Required(ErrorMessage = "HeaderName is required")]
        public string HeaderName
        {
            get;
            set;
        }

        private int numOfForecast =2;
        [Category("EPMProperty"), Description("Number of Days to show in forcast area."), DefaultValue(typeof(Int32), "2")]
        public int NumOfForecast
        {
            get {return numOfForecast;}
            set { numOfForecast = value; }
        }
    
        public static string FahrenheitToCelsius(string temperatureFahrenheit)
        {
            double fahrenheit = System.Double.Parse(temperatureFahrenheit);
            return (Convert.ToInt32((5.0 / 9.0) * (fahrenheit - 32))).ToString();
        }

        public static string CelsiusToFahrenheit(string temperatureCelsius)
        {
            double celsius = System.Double.Parse(temperatureCelsius);
            return (Convert.ToInt32((celsius * (9 / 5))+32)).ToString();
        }
    }

  
