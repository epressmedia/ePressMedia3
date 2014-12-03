using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for App_Settings
/// </summary>
public class MobileAppSettings
{
    public MobileAppSettings()
        {
            WeatherCity = ConfigurationManager.AppSettings["WeatherCity"];
            TempertureScale = ConfigurationManager.AppSettings["TempertureScale"];
            NewsPage = ConfigurationManager.AppSettings["NewsPageCat"];
            AdPage = ConfigurationManager.AppSettings["AdPageCat"];
        }

        private string m_WeatherCity;
        public string WeatherCity
        {
            get { return m_WeatherCity; }
            set { m_WeatherCity = value; }
        }

        private string m_TempertureScale;
        public string TempertureScale
        {
            get { return m_TempertureScale; }
            set { m_TempertureScale = value; }
        }

        private string m_NewsPage;
        public string NewsPage
        {
            get { return m_NewsPage; }
            set { m_NewsPage = value; }
        }

        private string m_AdPage;
        public string AdPage
        {
            get { return m_AdPage; }
            set { m_AdPage = value; }
        }
}