using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Linq;

using EPM.Legacy.Data;
using EPM.Legacy.Security;


    public partial class Mobile_Common_MobileAdList1 : System.Web.UI.UserControl
    {
        int row_per_page;

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            row_per_page = 10;
            if (!IsPostBack)
            {

                if (string.IsNullOrEmpty(Request.QueryString["CatId"]))
                    CatId.Value = "2";// Default Menu ID
                else
                    CatId.Value = Request.QueryString["CatId"].ToString();
                bindData(context.SiteMenus.Single(c => c.MenuId == int.Parse(CatId.Value.ToString())).Param);//    Knn.Website.SiteMenu1.SelectMenuById(int.Parse(CatId.Value.ToString())).Parameter);
                loadselector();

                // CatId = MenuId

            }
        }


        void loadselector()
        {
            AdCatRepeator.DataSource = context.SiteMenus.Select(c => c.ParentId == int.Parse(ConfigurationManager.AppSettings["AdPageCat"].ToString()) && c.Visible == true).ToList();// Knn.Website.SiteMenu.SelectMenuByParentId(int.Parse(ConfigurationManager.AppSettings["AdPageCat"].ToString()));
            AdCatRepeator.DataBind();
        }




        void bindData(string adPage)
        {
            int pageNum;

            if (string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                pageNum = 1;
                PageNumber.Value = pageNum.ToString();
                
            }
            else
            {
                pageNum = int.Parse(Request.QueryString["page"]);
                PageNumber.Value = pageNum.ToString();
            }

            //DataFilterCollection filters = getFilter(int.Parse(adPage));

            bindPage(int.Parse(adPage));
        }

        public String GetDefaultPage()
        {

            //MobileAppSettings app_set = new MobileAppSettings();


            string pages = "1,2,3,4,5";
            string[] AdPage = pages.Split(',');// app_set.AdPage.Split(',');

            

            //Knn.Website.SiteMenu siteMenu = Knn.Website.SiteMenu.SelectMenuById(int.Parse(AdPage[0].ToString()));


            //return siteMenu.Parameter.ToString();

            return context.SiteMenus.Single(c => c.MenuId == int.Parse(AdPage[0].ToString())).Param;

        }

        void bindPage(int CatId)
        {
            
            int counter = context.ClassifiedAds.Count(c => c.Category == CatId);

            setMaxPage(counter);


            List<ClassifiedModel.ClassifiedAd> ads = (from c in context.ClassifiedAds
                                                    where c.Category == CatId && c.Suspended == false
                                                    select c).Take(row_per_page).ToList();

            AdRepeater.DataSource = ads;
            AdRepeater.DataBind();
        }


        void setMaxPage(int cnt)
        {
            if (cnt == 0)
                MaxPageNumber.Value = "0";
            else
                MaxPageNumber.Value = ((cnt / row_per_page) + 1).ToString();
        }



        protected void AdRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            string artUpRoot;
            string fullwebsite;
            string artThumbPath;

            if (e.Item.ItemType == ListItemType.Header)
            {
                Label myHeader = (Label)e.Item.FindControl("listheader1");

                myHeader.Text = context.SiteMenus.Single(c => c.MenuId == int.Parse(CatId.Value)).Label;
                //myHeader.Text = Knn.Website.SiteMenu.SelectMenuById(int.Parse(CatId.Value)).Label.ToString();

            }

            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Ad a = e.Item.DataItem as Ad;

                ClassifiedModel.ClassifiedAd a = e.Item.DataItem as ClassifiedModel.ClassifiedAd;



                Image img = e.Item.FindControl("Thumb") as Image;
                if (string.IsNullOrEmpty(a.Thumb))
                {
                    img.ImageUrl = "~/images/noThumb.png";
                    img.Visible = false;
                }
                else
                {
                    artUpRoot = EPM.Core.Admin.SiteSettings.ClassifiedUploadRoot;// KNN.Core.Admin.SiteSettings.GetSiteSettingValueByName("Classified Upload Root");
                    //fullwebsite = ConfigurationManager.AppSettings["FullWebSiteAddress"];
                    fullwebsite = Request.ApplicationPath;

                    artThumbPath = artUpRoot + "/Thumb/";
                    img.ImageUrl = fullwebsite + artThumbPath + a.Thumb;
                }

            }
        }

    }
