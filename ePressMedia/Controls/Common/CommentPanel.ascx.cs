using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPM.Core;
using EPM.Legacy.Security;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


[Description("General Comment Control for Ariicle, Forum and Classified")]
public partial class Controls_CommentPanel : System.Web.UI.UserControl
{
    /// <summary>
    /// Aritcle인 경우 ArticleId, Forum 인 경우 ThreadId, 
    /// </summary>
    /// 
    private static  int itemID = -1;
    public static int ItemID
    {
        get { return itemID; }
        set { itemID = value; }
    }

    private static CommentTypes commentType;
    [Category("EPMProperty"), Description("Content Type"), Required()]
    public CommentTypes CommentType
    {
        get { return commentType; }
        set { commentType = value; }
    }


    public enum CommentTypes
    {
        Article = 1,
        Forum = 2,
        Classified = 3

    }

    private Random random = new Random();
    string captchaChars = "ACEFGHJKLMNPQRSTXYZ2345679";

    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 4; i++)
            s = String.Concat(s, captchaChars[random.Next(26)]);
        return s;
    }
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Session["CaptchaImageText"] = GenerateRandomCode();

            getItemID();
            bool auther_fg = false;
            //ItemID = articleID, threadID or adID
            int srcId = itemID;

            if (srcId > 0)
            {
                if (CommentType == CommentTypes.Article)
                {
                    int catid = (from c in context.Articles
                                 where c.ArticleId == srcId
                                 select c.CategoryId).Single();

                    auther_fg = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                            ResourceType.Article, catid, Permission.Comment);



                }
                else if (CommentType == CommentTypes.Forum)
                {
                    int catid = (from c in context.ForumThreads
                                 where c.ThreadId == srcId
                                 select c.ForumId).Single();

                    auther_fg = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                            ResourceType.Forum, catid, Permission.Comment);



                }
                else if (CommentType == CommentTypes.Classified)
                {
                    int catid = (from c in context.ClassifiedAds
                                 where c.AdId == srcId
                                 select c.ClassifiedCategory.CatId).Single();
                    auther_fg = AccessControl.AuthorizeUser(Page.User.Identity.Name,
                            ResourceType.Classified, catid, Permission.Comment);

                }
                comment_post_panel.Visible = auther_fg;
            }
            else
                comment_post_panel.Visible = true;

            



            PostBy.Text = Page.User.Identity.Name;

            bindData();
        }
    }

    private void getItemID()
    {
        if (CommentType == CommentTypes.Article)
        {
            if (Request.QueryString["aid"] != null)
                ItemID = int.Parse(Request.QueryString["aid"].ToString());
        }
        else if (CommentType == CommentTypes.Forum)
        {
            if (Request.QueryString["tid"] != null)
                ItemID = int.Parse(Request.QueryString["tid"].ToString());
        }
        else if (CommentType == CommentTypes.Classified)
        {
            if (Request.QueryString["aid"] != null)
                ItemID = int.Parse(Request.QueryString["aid"].ToString());
        }
    }

    void bindData()
    {
        int srcId = ItemID;


        // CommentTypes c_type = (CommentTypes)(int.Parse(CommType.Value));


        if (CommentType == CommentTypes.Article)// (c_type.ToString() == "Article")
        {
            FitPager1.TotalRows = (from c in context.ArticleComments
                                   where c.ArticleId == srcId && c.Blocked == false
                                   select c).Count();
        }
        else if (CommentType == CommentTypes.Forum)
        {
            FitPager1.TotalRows = (from c in context.ForumComments
                                   where c.SrcId == srcId && c.Blocked == false
                                   select c).Count();
        }
        else if (CommentType == CommentTypes.Classified)
        {
            FitPager1.TotalRows = (from c in context.ClassifiedComments
                                   where c.SrcId == srcId && c.Blocked == false
                                   select c).Count();
            //FitPager1.TotalRows = UserComment.GetCommentCount((CommentType)int.Parse(CommType.Value),
            //                                        srcId);


        }
        CommentCount.Text = FitPager1.TotalRows.ToString();
        if (FitPager1.TotalRows > FitPager1.RowsPerPage)
            FitPager1.Visible = true;

        if (FitPager1.TotalRows > 0)
            CommentListPanel.Visible = true;

        bindPage(1);
    }

    void bindPage(int pageNum)
    {
        getItemID();
        int srcId = itemID;
        int startRowIndex = (pageNum - 1) * FitPager1.RowsPerPage + 1;

        //CommentTypes c_type = (CommentTypes)(int.Parse(CommType.Value));


        if (CommentType == CommentTypes.Article)//(c_type.ToString() == "Article")
        {
            var result = (from c in context.ArticleComments
                          where c.ArticleId == srcId && c.Blocked == false
                          orderby c.PostDate descending
                          select c).Skip(startRowIndex - 1).Take(FitPager1.RowsPerPage);
            CommentList.DataSource = result;
        }
        else if (CommentType == CommentTypes.Forum)//(c_type.ToString() == "Forum")
        {
            var result = (from c in context.ForumComments
                          where c.SrcId == srcId && c.Blocked == false
                          orderby c.PostDate descending
                          select c).Skip(startRowIndex - 1).Take(FitPager1.RowsPerPage);
            CommentList.DataSource = result;
        }
        else if (CommentType == CommentTypes.Classified)
        {

            var result = (from c in context.ClassifiedComments
                          where c.SrcId == srcId && c.Blocked == false
                          orderby c.PostDate descending
                          select c).Skip(startRowIndex - 1).Take(FitPager1.RowsPerPage);
            CommentList.DataSource = result;

        }
        CommentList.DataBind();


        if ((comment_post_panel.Visible) || (CommentList.Items.Count > 0))
        {
            comment_header.Visible = true;
        }
    }

    protected void PostButton_Click(object sender, EventArgs e)
    {


        if (PostBy.Text.Trim() == "" || Password.Text == "" ||
            Captcha.Text == "" || Comment.Text.Trim() == "") //|| Subject.Text.Trim() == "" 
        {

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('모든 항목을 빠짐없이 입력해야 합니다.')", true);
        }
        else if (Captcha.Text.ToUpper().Equals(Session["CaptchaImageText"].ToString()) == false)
        {


            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('자동 입력 방지 코드가 맞지 않습니다.')", true);
        }
        else
        {
            bool res = false;
            getItemID();


            if (CommentType == CommentTypes.Article)// (c_type.ToString() == "Article")
            {
                ArticleModel.ArticleComment comment = new ArticleModel.ArticleComment();
                comment.IPAddr = Request.UserHostAddress;
                comment.Password = Password.Text;
                comment.PostBy = PostBy.Text;
                comment.PostDate = DateTime.Now;
                comment.Removed = false;
                comment.Subject = Subject.Text;
                comment.UserName = Page.User.Identity.Name;
                comment.Blocked = false;
                comment.ArticleId = itemID;
                comment.Comment = ForumUtility.GetCleanText(Server.HtmlEncode(Comment.Text).Replace(
                                        Environment.NewLine, "<br />"));

                context.Add(comment);
                context.SaveChanges();

                if (comment.Id > 0)
                    res = true;

            }
            else if (CommentType == CommentTypes.Forum)//(c_type.ToString() == "Forum")
            {
                ForumModel.ForumComment comment = new ForumModel.ForumComment();
                comment.IPAddr = Request.UserHostAddress;
                comment.Password = Password.Text;
                comment.PostBy = PostBy.Text;
                comment.PostDate = DateTime.Now;
                comment.Removed = false;
                comment.Subject = Subject.Text;
                comment.UserName = Page.User.Identity.Name;
                comment.Blocked = false;
                comment.SrcId = itemID;
                comment.Comment = ForumUtility.GetCleanText(Server.HtmlEncode(Comment.Text).Replace(
                                        Environment.NewLine, "<br />"));

                context.Add(comment);
                context.SaveChanges();

                if (comment.Id > 0)
                {
                    res = true;
                    sendPostNotification();
                }

            }

            else if (CommentType == CommentTypes.Classified)
            {

                ClassifiedModel.ClassifiedComment comment = new ClassifiedModel.ClassifiedComment();
                comment.IPAddr = Request.UserHostAddress;
                comment.Password = Password.Text;
                comment.PostBy = PostBy.Text;
                comment.PostDate = DateTime.Now;
                comment.Removed = false;
                comment.Subject = Subject.Text;
                comment.UserName = Page.User.Identity.Name;
                comment.Blocked = false;
                comment.SrcId = itemID;
                comment.Comment = ForumUtility.GetCleanText(Server.HtmlEncode(Comment.Text).Replace(
                                        Environment.NewLine, "<br />"));

                context.Add(comment);
                context.SaveChanges();

                if (comment.Id > 0)
                    res = true;


            }



            if (res)
            {
                Captcha.Text = Subject.Text = Comment.Text = "";
                this.Session["CaptchaImageText"] = GenerateRandomCode();
                CapImg.ImageUrl = "~/Controls/JpegCaptcha.aspx";
                bindData();
            }
        }
    }


    protected void PageNumberChanged(object sender, EventArgs e)
    {
        bindPage(FitPager1.CurrentPage);
    }

    // to be called back when '삭제' link clicked
    protected void CommentList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int CommentId = int.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "delete")
        {

                    //String cType = ((CommentTypes)(int.Parse(CommType.Value))).ToString(); ;

        // TODO : ResourceType 과 CommentType 이 이중으로 존재하여 혼란 초래. 통합해야 함.
        if (AccessControl.AuthorizeUser(Page.User.Identity.Name, (ResourceType)(CommentType), //  ResourceType.Forum,
                    int.Parse(Request.QueryString["p"]), // int.Parse(SrcId.Value), 
                    Permission.FullControl))
        {
            DeleteComment(CommentId);
        }
        else
        {

            HtmlControl div = (HtmlControl)e.Item.FindControl("commet_pw_div");
            div.Visible = true;

            //Button btn_delete = (Button)e.Item.FindControl("btn_delete_confirm");
            //btn_delete.CommandArgument = CommentId.ToString();
            //btn_delete.Click += new EventHandler(btn_delete_Click);
        }
        }
        else if (e.CommandName == "PasswordConfirm")
        {
            DeleteComment(CommentId,((TextBox)e.Item.FindControl("txt_comm_pw")).Text);
        }
        
    }

    //void btn_delete_Click(object sender, EventArgs e)
    //{
    //    Button btn_delete = (Button)sender;
    //    HtmlControl div = (HtmlControl)btn_delete.Parent;
    //    DeleteComment(int.Parse(btn_delete.CommandArgument.ToString()), ((TextBox)div.FindControl("txt_comm_pw")).Text);
    //}

    public void DeleteComment(int CommentId)
    {
        if (CommentType == CommentTypes.Article)
        {
            var comment = (from c in context.ArticleComments
                           where c.Id == CommentId
                           select c).Single();
            comment.Blocked = true;
            context.SaveChanges();
        }
        else if (CommentType == CommentTypes.Forum)
        {
            var comment = (from c in context.ForumComments
                           where c.Id == CommentId
                           select c).Single();
            comment.Blocked = true;
            context.SaveChanges();
        }
        else if (CommentType == CommentTypes.Classified)
        {
            var comment = (from c in context.ClassifiedComments
                           where c.Id == CommentId
                           select c).Single();
            comment.Blocked = true;
            context.SaveChanges();

        }

        bindData();
    }

    public void DeleteComment(int CommentId, string password)
    {

        bool err = false;
        if (CommentType == CommentTypes.Article)//(cType == "Article")
        {
            var comment = (from c in context.ArticleComments
                           where c.Id == CommentId
                           select c).Single();
            if (comment.Password.Trim() == password.Trim())
            {

                comment.Blocked = true;
                context.SaveChanges();
                bindData();
            }
            else
                err = true;
        }
        else if (CommentType == CommentTypes.Forum)//(cType == "Forum")
        {
            var comment = (from c in context.ForumComments
                           where c.Id == CommentId
                           select c).Single();
            if (comment.Password.Trim() == password.Trim())
            {

                comment.Blocked = true;
                context.SaveChanges();
                bindData();
            }
            else
                err = true;
        }
        else if (CommentType == CommentTypes.Classified)
        {


            var comment = (from c in context.ClassifiedComments
                           where c.Id == CommentId
                           select c).Single();
            if (comment.Password.Trim() == password.Trim())
            {

                comment.Blocked = true;
                context.SaveChanges();
                bindData();
            }
            else
                err = true;

        }


        if (err)
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "strscript", "alert('Password does not match.')", true);
    }

    void sendPostNotification()
    {

        var cfg = (from c in context.ForumConfigs
                   where c.ForumId == int.Parse(Request.QueryString["p"])
                   select c).Single();

        if (cfg.NotifyPost || cfg.MailList.Trim().Length > 0)
        {

            EPM.Email.EmailSender.SendEmail( cfg.MailList, "", "[Comment Post Notification] " + Subject.Text, Comment.Text, true);
        }

    }

    protected void CommentList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (AccessControl.AuthorizeUser(Page.User.Identity.Name, (ResourceType)(CommentType), //  ResourceType.Forum,
             int.Parse(Request.QueryString["p"]), // int.Parse(SrcId.Value), 
             Permission.FullControl))
            {
                LinkButton btn_delete = (LinkButton)e.Item.FindControl("LinkButton1");
               btn_delete.Attributes.Add("OnClick",  "return confirm('Are you sure you want to delete this item?');");
            }
        }
    }
}