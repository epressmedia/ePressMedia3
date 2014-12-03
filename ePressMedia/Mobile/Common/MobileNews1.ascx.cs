using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections.Specialized;


using EPM.Legacy.Data;
using EPM.Legacy.Security;




    public partial class Mobile_Common_MobileNews1 : System.Web.UI.UserControl
    {

        int row_per_page;

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            row_per_page = 10;
            if (!IsPostBack)
            {

                if (string.IsNullOrEmpty(Request.QueryString["CatId"]))
                    CatId.Value = GetDefaultPage().ToString();
                else
                    CatId.Value = Request.QueryString["CatId"].ToString();


                bindData(CatId.Value);
                loadselector();

                
            }
        }

        void PageConfig()
        {

        }


        void loadselector()
        {
            var menus =  from c in context.SiteMenus
                            where  c.ParentId ==  int.Parse(ConfigurationManager.AppSettings["NewsPageCat"].ToString()) && c.Visible == true
                            select c;

            NewsCatRepeator.DataSource = menus; //Knn.Website.SiteMenu.SelectMenuByParentId(int.Parse(ConfigurationManager.AppSettings["NewsPageCat"].ToString()));
            NewsCatRepeator.DataBind();
        }

        public String GetDefaultPage()
        {

            //MobileAppSettings app_set = new MobileAppSettings();
            //string[] newspages = app_set.NewsPage.Split(',');

            //Knn.Website.SiteMenu siteMenu = Knn.Website.SiteMenu.SelectMenuById(int.Parse(newspages[0].ToString()));


            //return siteMenu.Parameter.ToString();

            return context.SiteMenus.Single(c => c.MenuId == 12).Param;
            //return context.SiteMenus.Single(c => c.MenuId == int.Parse(newspages[0].ToString())).Param;
        
        }

        void bindData(string newsPages)
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




            var results = from c in context.Articles
                          where c.CategoryId == int.Parse(CatId.Value) && c.Suspended == false && c.IssueDate <= DateTime.Now
                          orderby c.IssueDate descending
                          select c;

            setMaxPage(results.Count());// int.Parse(Article.GetArticleCount(filters).ToString()));

            var final_results = results.Skip((int.Parse(PageNumber.Value) - 1) * row_per_page).Take(row_per_page);

            ArtRepeater1.DataSource = final_results;
            ArtRepeater1.DataBind();
        }

        void setMaxPage(int cnt)
        {
            MaxPageNumber.Value = ((cnt / row_per_page) + 1).ToString();
        }

        protected void ArticleRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {


            
            try
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    
                    Label myHeader = (Label)e.Item.FindControl("listheader1");
                    string HeaderName = ((List<ArticleModel.Article>)(((Repeater)(sender)).DataSource))[0].ArticleCategory1.CatName;
                    int HeaderNameIndex = HeaderName.IndexOf("_")+1;
                    myHeader.Text = HeaderName.Substring(HeaderNameIndex , HeaderName.Length - HeaderNameIndex );
                    
                }
                if (e.Item.ItemType == ListItemType.Item ||
                    e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ArticleModel.Article a = e.Item.DataItem as ArticleModel.Article;

                    //HyperLink l = e.Item.FindControl("ViewLink") as HyperLink;
                    //l.NavigateUrl = "#";//Params.Value + "&page=" + FitPager1.CurrentPage +"&aid=" + a.ArticleId;

                    if (a.Thumb_Path != null)
                    {
                        Image img = e.Item.FindControl("Thumb") as Image;
                        if (a.Thumb_Path.Trim().Length == 0)
                        {
                            HtmlControl hc = e.Item.FindControl("NewsImageDiv") as HtmlControl;
                            hc.Visible = false;
                            img.ImageUrl = "~/images/noThumb.png";
                            
                        }

                        else
                        {

                            //artUpRoot = ConfigurationManager.AppSettings["ArticleUploadRoot"];
                            //fullwebsite = ConfigurationManager.AppSettings["FullWebSiteAddress"];
                            // fullwebsite = Request.ApplicationPath;

                            // artThumbPath = artUpRoot + "/Thumb/";
                            img.ImageUrl = a.Thumb_Path;// fullwebsite + artThumbPath + a.Thumb_Path;// SiteSettings.ArticleThumbnailPath + a.Thumbnail;
                        }
                    }

                }
            }
            catch
            {
            }
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsolutePath + "?CatId=" + CatId.Value + "&page=" + (int.Parse(PageNumber.Value) + 1).ToString());
            

        }



    }
