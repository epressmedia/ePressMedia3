using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using EPM.Core;
using EPM.Core.Common;
using Google.GData.Analytics;
using Google.GData.Client;
using Google.GData.Extensions;


namespace ePressMedia.Cp.Site
{
    
    public partial class GoogleAnalytics : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        private static string aaKey = "2Pr2ssM2dia";    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_gaaccount.Items.Clear();
                List<SiteModel.SiteSetting> GAEmail = (from c in context.SiteSettings
                                                       where c.SettingName == "GA Email"
                                                       select c).ToList();
                
                if (GAEmail.Count > 0)
                {
                    //txt_password.Text = CommonUtility.Decrypt(GAEmail.SettingValue, aaKey, true);
                    txt_emailaddress.Text = GAEmail[0].SettingValue;

                    getGAInfo();
                }


                toolbox1.EnableButtons("", true);

            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
        }

        void getGAInfo()
        {
            List<SiteModel.SiteSetting> GAWebID = (from c in context.SiteSettings
                                                   where c.SettingName == "GA Web Property ID"
                                                   select c).ToList();
            if (GAWebID.Count > 0)
            {
                if (GAWebID[0].SettingValue.Length > 0)
                    lbl_current.Text = " - Web ID: " + GAWebID[0].SettingValue.ToString();
            }
            else
            {
                lbl_current.Text = "";
            }
        }

        
        private String EmailAddress
        {
            get { return ViewState["emailaddress"].ToString(); }
            set { ViewState["emailaddress"] = value; }
        }

        private String Password
        {
            get { return ViewState["password"].ToString(); }
            set { ViewState["password"] = value; }
        }

        void listGAAccount()
        {


            //String accountFeedUrl = "https://www.google.com/analytics/feeds/datasources/ga/accounts?";
            AccountQuery query = new AccountQuery("https://www.googleapis.com/analytics/v2.4/management/accounts?start-index=1&max-results=100&key=AIzaSyCHnesV1qqzl7juppcuFTCsP7cJgY91IEw");
//            AccountQuery query = new AccountQuery("https://www.googleapis.com/analytics/v3/management/accounts?key=AIzaSyCHnesV1qqzl7juppcuFTCsP7cJgY91IEw");

            string ids = "";
            AnalyticsService service = new AnalyticsService("EPM");
        
            //query.ExtraParameters = "key=AIzaSyCHnesV1qqzl7juppcuFTCsP7cJgY91IEw";

            
            if (!string.IsNullOrEmpty(txt_emailaddress.Text))
            {
                service.setUserCredentials(txt_emailaddress.Text, txt_password.Text);
            }


            //DataFeed accountFeed = service.Query(new DataQuery("https://www.googleapis.com/analytics/v2.4/management/accounts"));

           AccountFeed accountFeed =  service.Query(query);

            

            foreach (AtomEntry entry in accountFeed.Entries)
            {
                string aa = entry.Links[1].HRef.Content;

                ddl_gaaccount.Items.Add(new ListItem(entry.Title.Text.Replace("Google Analytics Account ",""), entry.Links[1].HRef.Content));
                //ddl_gaaccount.Items.Add(new ListItem(entry.Title.Text, entry.ProfileId.Value+";"+((Google.GData.Extensions.SimpleAttribute)((new List<Google.GData.Analytics.Property>(entry.Properties))[3])).Value));
            }

            if (ddl_gaaccount.Items.Count > 0)
            {
                ddl_gaaccount.Items.Insert(0, new ListItem("-- Select a Account -- ", "0"));
                EmailAddress = txt_emailaddress.Text;
                Password = txt_password.Text;
            }
            else
            {
                ddl_gaaccount.Items.Insert(0, new ListItem("-- No Account Available -- ", "0"));
            }
        }



        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if ((txt_emailaddress.Text.Length > 0) && (txt_password.Text.Length > 0))
            {
                ddl_gaaccount.Items.Clear();
                try
                {
                    listGAAccount();
                    edit_panel.Visible = true;
                    ;
                    toolbox1.EnableButtons("cancel", true);
                    toolbox1.EnableButtons("delete", false);
                }
                catch(Google.GData.Client.AuthenticationException ex)
                {

                    
                    EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Login Failed due to the following reasons");
                }
            }
            else
            {
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Email Address and Password must be entered.");
            }
        }



        void RemoveSetting()
        {
            SiteSettingHelper.UpdateSettingValue("GA Email", "");
            SiteSettingHelper.UpdateSettingValue("GA Password", "");
            SiteSettingHelper.UpdateSettingValue("GA Account ID", "");
            SiteSettingHelper.UpdateSettingValue("GA Web Property ID", "");

            
            getGAInfo();
            edit_panel.Visible = false;
            EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Google Analytics setting has been removed.");
        }

        void SaveSetting()
        {
            SiteSettingHelper.CreateSettingField("GA Email", "Google Analytics Email Address");
            SiteSettingHelper.CreateSettingField("GA Password", "Google Analytics Password");
            SiteSettingHelper.CreateSettingField("GA Account ID", "Google Analytics Account ID");
            SiteSettingHelper.CreateSettingField("GA Web Property ID", "Google Analytics Web Property ID");

            SiteSettingHelper.UpdateSettingValue("GA Email", EmailAddress);
            SiteSettingHelper.UpdateSettingValue("GA Password", CommonUtility.Encrypt(Password, aaKey, true));
           // List<string> values = ddl_gaaccount.SelectedItem.Value.ToString().Split(';').ToList<string>();
            SiteSettingHelper.UpdateSettingValue("GA Account ID",ddl_gaaccount.SelectedItem.Value.ToString());//  values[0].ToString());
            SiteSettingHelper.UpdateSettingValue("GA Web Property ID", ddl_gawebproperty.SelectedItem.Text.ToString());// values[1].ToString());

            

            getGAInfo();
            edit_panel.Visible = false;

            EPM.Legacy.Common.Utility.RegisterJsAlert(this.Page, "Google Analytics setting has been saved.");
        }

        protected void ddl_gaaccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_gaaccount.SelectedIndex > 0)
            {

                AnalyticsService service = new AnalyticsService("EPM");
                service.setUserCredentials(EmailAddress, Password);
                DataFeed accountFeed = service.Query(new DataQuery(ddl_gaaccount.SelectedItem.Value.ToString()));

                foreach (AtomEntry entry in accountFeed.Entries)
                {
                    string aa = entry.Links[1].HRef.Content;

                    ddl_gawebproperty.Items.Add(new ListItem(entry.Title.Text.Replace("Google Analytics Web Property ", ""), entry.Links[1].HRef.Content));
                    //ddl_gaaccount.Items.Add(new ListItem(entry.Title.Text, entry.ProfileId.Value+";"+((Google.GData.Extensions.SimpleAttribute)((new List<Google.GData.Analytics.Property>(entry.Properties))[3])).Value));
                }

                toolbox1.EnableButtons("Save", false);
            }
            else
                toolbox1.DisableButton("Save");
        }


        protected void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();

            if (action == "save")
                SaveSetting();
            else if (action == "delete")
                RemoveSetting();
            else if (action == "cancel")
                edit_panel.Visible = false;

        }


    }
}
