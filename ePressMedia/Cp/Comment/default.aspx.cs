using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ePressMedia.Cp.Comment
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            List<string> args = e.CommandArgument.ToString().Split(';').ToList();
            int contentTypeId = int.Parse(args[1].ToString());
            int commentId = int.Parse(args[0].ToString());
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string CommentType = context.ContentTypes.Where(x => x.ContentTypeId == contentTypeId).SingleOrDefault().ContentTypeName.ToLower();
            if (e.CommandName == "del")
            {

                if (CommentType == "article/news")
                {
                    var comment = (from c in context.ArticleComments
                                   where c.Id == commentId
                                   select c).Single();
                    comment.Blocked = true;
                    context.SaveChanges();
                }
                else if (CommentType == "forum")
                {
                    var comment = (from c in context.ForumComments
                                   where c.Id == commentId
                                   select c).Single();
                    comment.Blocked = true;
                    context.SaveChanges();
                }
                else if (CommentType == "classified")
                {
                    var comment = (from c in context.ClassifiedComments
                                   where c.Id == commentId
                                   select c).Single();
                    comment.Blocked = true;
                    context.SaveChanges();

                }

                RadGrid1.DataBind();
            }

        }


        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                //get the Hyperlink using the Column uniqueName
                HyperLink hyperLink = (HyperLink)dataItem["Link"].Controls[0];
                hyperLink.Text = "Lnik";
                hyperLink.Target = "_blank";

                string label = dataItem["ContentTypeName"].Text;
                string SrcId = dataItem["SrcId"].Text;

                string contentTypeName = label.ToLower();
                int srcId = int.Parse(SrcId);

                if (contentTypeName == "article")
                {
                    string catId = EPM.Business.Model.Article.ArticleCategoryContoller.GetArticleCategoryByArticleId(srcId).ArtCatId.ToString();
                    hyperLink.NavigateUrl = "~/Article/view.aspx?p=" + catId + "&aid=" + srcId.ToString();
                }
                else if (contentTypeName == "forum")
                {
                    string catId = EPM.Business.Model.Forum.ForumController.GetForumByThreadId(srcId).ForumId.ToString();
                    hyperLink.NavigateUrl = "/Forum/view.aspx?p=" + catId + "&tid=" + srcId.ToString();
                }
                else if (contentTypeName == "classified")
                {
                    string catId = EPM.Business.Model.Classified.ClassifiedController.GetClassifiedCategoryByAdId(srcId).CatId.ToString();
                    hyperLink.NavigateUrl = "/Classified/view.aspx?p=" + catId + "&aid=" + srcId.ToString();
                    
                }
                
            }
        }
    }
}