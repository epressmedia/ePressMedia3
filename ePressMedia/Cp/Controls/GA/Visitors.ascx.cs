using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core;
using Google.GData.Analytics;
using Google.GData.Extensions;


namespace ePressMedia.Cp.Controls.GA
{
    public partial class Visitors : System.Web.UI.UserControl
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        private static string aaKey = "2Pr2ssM2dia";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGA(cbo_type.SelectedItem.Value.ToString(), int.Parse(txt_number.Text));
            }
        }
        void GetGA(string Dimensions, int number)
        {

            List<SiteModel.SiteSetting> Settings = (from c in context.SiteSettings
                                                    where c.SettingName == "GA Email" || c.SettingName == "GA Password" || c.SettingName == "GA Account ID"
                                                    select c).ToList();



            if (Settings.Count() == 3)
            {
                string userName = Settings.Single(c => c.SettingName == "GA Email").SettingValue;
                string passwordPlain = Settings.Single(c => c.SettingName == "GA Password").SettingValue;
                string ID = Settings.Single(c => c.SettingName == "GA Account ID").SettingValue;

                if (userName.Length == 0 || passwordPlain.Length == 0 || ID.Length == 0)
                {
                    Label1.Text = "Google Analytics has not been configured.";
                }
                else
                {
                    string password = CommonUtility.Decrypt(passwordPlain, aaKey, true);



                    string result = "";
                    
                    AnalyticsService service = new AnalyticsService("ePressMediaGA");

                    service.setUserCredentials(userName, password);

                    DataQuery pageViewQuery = new DataQuery("https://www.google.com/analytics/feeds/data");
                    //pageViewQuery.Ids = ID;
                    //pageViewQuery.Metrics = "ga:visits";
                    //pageViewQuery.Dimensions = "ga:pagePath";
                    //pageViewQuery.Sort = "-ga:visits";
                    //pageViewQuery.GAStartDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    //pageViewQuery.GAEndDate = DateTime.Now.ToString("yyyy-MM-dd");
                    pageViewQuery.Ids = ID;
                    //pageViewQuery.Metrics = "ga:pageviews";
                    //pageViewQuery.Dimensions = "ga:browser";
                    //pageViewQuery.Sort = "ga:browser,ga:pageviews";
                    pageViewQuery.Metrics = "ga:visitors";
                    pageViewQuery.Dimensions = Dimensions;
                    pageViewQuery.Sort = Dimensions;

                    string startDate = "";


                    if (Dimensions == "ga:year")
                    {
                        startDate = DateTime.Now.AddYears(number * -1).ToString("yyyy-MM-dd");
                    }
                    else if (Dimensions == "ga:week")
                    {
                        startDate = DateTime.Now.AddDays(number * -7).ToString("yyyy-MM-dd");
                    }
                    else if (Dimensions == "ga:date")
                    {
                        startDate = DateTime.Now.AddDays(number * -1).ToString("yyyy-MM-dd");
                    }
                    else if (Dimensions == "ga:year,ga:month")
                    {
                        startDate = DateTime.Now.AddMonths(number * -1).ToString("yyyy-MM-dd");
                    }

                    pageViewQuery.GAStartDate = startDate;
                    pageViewQuery.GAEndDate = DateTime.Now.ToString("yyyy-MM-dd");


                    DataFeed feed = service.Query(pageViewQuery);


                    List<GADataList> gadatalist = new List<GADataList>();
                    foreach (DataEntry entry in feed.Entries)
                    {
                        string date = "";

                        if (Dimensions == "ga:year,ga:month")
                        {
                            date = entry.Dimensions[0].Value.ToString() + "-" + entry.Dimensions[1].Value.ToString();
                        }
                        else
                        {
                            date = entry.Dimensions[0].Value.ToString(); 
                        }



                        gadatalist.Add(new GADataList(date, entry.Metrics[0].Value.ToString()));

                    }


                    RadHtmlChart1.ChartTitle.Text = "Visitors in last " + txt_number.Text + " " + cbo_type.SelectedItem.Text + "s";

                    RadHtmlChart1.DataSource = gadatalist;
                    RadHtmlChart1.DataBind();
                    ga_visitors.Visible = true;
                }
            }
            else
            {
                Label1.Text = "Google Analytics has not been configured.";
            }
        }

        protected void RadComboBox1_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            if ((txt_number.Text.Length > 0) && (cbo_type.SelectedItem.Index > -1))
            {
                try
                {
                    GetGA(cbo_type.SelectedItem.Value.ToString(), int.Parse(txt_number.Text));
                }
                catch
                {
                }
            }
        }
    }
    public class GADataList
    {
        public GADataList(string name, string value)
        {
            _name = name;
            _value = value;
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}