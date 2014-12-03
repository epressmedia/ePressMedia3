using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using EPM.Business.Model.Article;
using EPM.Legacy.Security;
using log4net;

using EPM.Data.Model;

[Description("Article Detail Control")]
public partial class Article_DetailView_ArticleDetail : System.Web.UI.UserControl
{

    private static readonly ILog log = LogManager.GetLogger(typeof(Article_DetailView_ArticleDetail));

     
    public string VideoId
    {
        get
        {
            if (ViewState["VideoId"] == null)
                return "";
            else
                return ViewState["VideoId"].ToString();
        }
        set
        {
            ViewState["VideoId"] = value;
        }
    }

    
    public string BrowswerHeader_Prefix
    {
        get;
        set;
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int cat = -1;
                
                if(Request.QueryString["p"]!=null)
                    cat =int.Parse(Request.QueryString["p"]);
                 else if (Page.RouteData.Values["category"] != null)
                    cat = ArticleCategoryContoller.GetArticleCatIdByURLSlug(Page.RouteData.Values["category"].ToString());

                AccessControl ac = AccessControl.SelectAccessControlByUserName(
                                Page.User.Identity.Name, ResourceType.Article, cat);
                if (ac == null)
                    ac = new AccessControl(Permission.None);

                if (!ac.CanRead)
                {
                    if (!EPM.Core.Users.Security.GuestViewLimitValid(this.Page))
                    {
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                    }
                }



                ListLink.NavigateUrl =
                    string.Format("~/Article/list.aspx?p={0}&page={1}&q={2}",
                                  Request.QueryString["p"], Request.QueryString["page"],
                                  Request.QueryString["q"]);

                //ModLink.NavigateUrl =
                //        string.Format("~/Article/AddArticle.aspx?p={0}&aid={1}&mode=edit",
                //                  Request.QueryString["p"],
                //                  Request.QueryString["aid"]);

                //ModLink.Visible = ac.CanModify;
                EditLink_popup.Visible = ac.CanModify;
                DelButton.Visible = ac.CanDelete;



                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();




                // Load Move to Category Dropdown List
                

                // Moving is a part of deletion. Therefore, the user has to have the full permission to the currenct category.
                if (ac.HasFullControl)
                {
                    // The move to article cat list Needs to be filetered by the access.
                    List<ArticleModel.ArticleCategory> ArtCats = (from c in context.ArticleCategories
                                                                 where c.ArtCatId != cat
                                                                 select c).ToList();

                    // Loop through the article category to see if the user has write access to the category.
                    // for each cannot be used because collection cannot be modified while it is being looped within for each. 
                    for (int i = ArtCats.Count - 1; i >= 0; i--)
                    {
                        if (!AccessControl.AuthorizeUser(Page.User.Identity.Name,
                      ResourceType.Article, ArtCats[i].ArtCatId, Permission.Write))
                        {
                            ArtCats.RemoveAt(i);
                        }
                    }

                    CatList.DataTextField = "CatName";
                    CatList.DataValueField =  "ArtCatId";
                    CatList.DataSource = ArtCats;
                    CatList.DataBind();

                    // count available categories and if it is more than 1...
                    if (ArtCats.Count() > 0)
                    {
                        CatList.Items.Insert(0,new ListItem("-- Please select category to move --"));
                        
                    }
                    else
                    {
                        CatList.Items.Insert(0,new ListItem("-- You do not have access to any other categories. --"));
                    }


                    // move panel is never visible now as the category dropdown list is added to the entry page.
                    //movePnl.Visible = true;
                }


                // Load Header Image
                var header_result = context.ArticleCategories.Single(s=>s.ArtCatId == cat);

                int aid = -1;
                if (Request.QueryString["aid"] != null)
                    aid = int.Parse(Request.QueryString["aid"]);
                else if (Page.RouteData.Values["title"] != null)
                    aid =  ArticleContoller.GetArticleIDByURLSlug(Page.RouteData.Values["title"].ToString()).ArticleId;

                if (aid > 0)
                    bindData(aid);
                else
                    throw new Exception("No Article ID Provided");


                if (DelButton.Visible)
                {
                    string msg = "Are you sure you would like to delete this article?";
                    string path = "/Page/DataEntry.aspx?cid=" + aid.ToString() + "&area=article&mode=Delete&p=" + cat.ToString() + "&passwordinput=false&returnURL=" + Server.UrlEncode(ListLink.NavigateUrl.Replace("~", "")) + "&msg=" + msg;
                    DeletePopup.Title = "Delete Article";// +Ad.Subject;
                    DeletePopup.Width = 300;
                    DeletePopup.Height = 200;
                    DelButton.OnClientClick = DeletePopup.GetOpenPath(path);
                }


            }
            catch (Exception ex) {
                log.Error(ex.Message);
                Response.Redirect("/");
                
            }
        }
    }

    void bindData(int articleId)
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();



        ArticleModel.Article result = ArticleContoller.GetArticleByArticleID(articleId);





        if (result != null)
        {
            // Inject javascript to Edit button for RadWindow open
            string path = "/Page/DataEntry.aspx?cid=" + articleId.ToString() + "&area=article&mode=Edit&p=" + Request.QueryString["p"];
            EntryPopupEdit.Title = "Edit Article - " + result.Title;
            EditLink_popup.OnClientClick = EntryPopupEdit.GetOpenPath(path);// EntryPopup.OpenMethod + "('" + path + "','" + result.Title.Replace("'", "") + "'); return false;";

            MsgTitle.Text = result.Title;
            SubTitle.Text = result.SubTitle;
            PostBy.Text = result.Reporter;
            ViewCount.Text = result.ViewCount.ToString();
            IssueDate.Text = result.IssueDate.ToShortDateString(); // +" " + t.PostDate.ToShortTimeString();
            IssueTime.Text = " " + result.IssueDate.ToShortTimeString();
            ViewCount.Text = result.ViewCount.ToString();
            Message.Text = result.Body;

            this.Page.Title = BrowswerHeader_Prefix + result.Title;

             string tag = "\n<meta property=\"{0}\" content=\"{1}\" />";
            //Header.Controls.Add(new LiteralControl(string.Format(tag, name, value)));

             this.Page.Header.Controls.Add(new LiteralControl(string.Format(tag, "og:url", HttpContext.Current.Request.Url.AbsoluteUri)));
             this.Page.Header.Controls.Add(new LiteralControl(string.Format(tag, "og:title", result.Title)));
             this.Page.Header.Controls.Add(new LiteralControl(string.Format(tag, "og:description",  EPM.Legacy.Common.Utility.TruncateStringByWord(result.Abstract,300))));


             List<string> imagestring = EPM.ImageUtil.EPMImageExtractUtil.GetImagesFromArticleBody(result.Body, false);

            if (imagestring.Count > 0)
                this.Page.Header.Controls.Add(new LiteralControl(string.Format(tag, "og:image", EPM.ImageUtil.EPMImageExtractUtil.GetImagesFromArticleBody(result.Body,false)[0].ToString())));



            if (!string.IsNullOrEmpty(result.VideoId))
            {
                VideoId = result.VideoId.Trim();
                youtube_player.Visible = true;
            }

            // get tags
            if (context.ArticleTags.Count(c => c.ArticleId == articleId) > 0)
            {
                lbl_tags.Text = context.ArticleTags.Single(c => c.ArticleId == articleId).TagString;
            }
            else
            {
                tag_container.Visible = false;
            }


            ArticleContoller.IncreaseCounter(result.ArticleId);
            
        }
        else
        {
            this.Visible = false;
        }

    }



    protected void MoveLink_Click(object sender, EventArgs e)
    {
        try
        {
            if (CatList.SelectedIndex > 0)
            {
                int aid = int.Parse(Request.QueryString["aid"]);
                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                ArticleModel.Article article = context.Articles.Single(s => s.ArticleId == aid);
                article.CategoryId = int.Parse(CatList.SelectedValue);
                context.SaveChanges();

                Response.Redirect(ListLink.NavigateUrl);
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.Message);
        }
    }

}