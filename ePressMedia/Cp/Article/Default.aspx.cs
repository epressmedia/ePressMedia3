using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using log4net;
using System.Linq;

namespace ePressMedia.Cp.Article
{
    public partial class Articles : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                toolbox1.EnableButtons("Add", true);


            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
        }

        void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();


            if (action == "add")
            {
                toolbox1.EnableButtons("Cancel", true);
                AddPanel.Visible = true;
            }
            else if (action == "cancel")
            {
                reset_AddPanel();
            }

        }

        void reset_AddPanel()
        {
            AddPanel.Visible = false;
            ArticleName.Text = "";
            rdb_normal.Checked = true;

            toolbox1.EnableButtons("Add", true);
        }


        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName.Equals("mod"))
            {
                RadGrid1.Items[int.Parse(e.CommandArgument.ToString())].Edit = true;
                RadGrid1.Rebind();

            }
            else if (e.CommandName.Equals("del"))
            {

                int catid = int.Parse(e.CommandArgument.ToString());
                DeleteArticleCat(catid);
            }
            else if (e.CommandName.Equals("permission"))
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/Permissions.aspx?type=article&id=" + e.CommandArgument.ToString(), listing_div, null);
            }
            else if (e.CommandName.Equals("config"))
            {
                EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/PageConfig.aspx?type=article&cat=" + e.CommandArgument.ToString(), listing_div, null, 1048, 900);
            }

        }



        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ArticleName.Text))
            {
                if (!context.ArticleCategories.Any(c => c.CatName.ToLower().Trim() == ArticleName.Text.ToLower().Trim()))
                {
                    ArticleModel.ArticleCategory article = new ArticleModel.ArticleCategory();
                    article.CatName = ArticleName.Text.Trim();
                    article.DetailMetadataStr = "";
                    article.metadataStr = "";

                    if (rdb_link.Checked)
                        article.LinkArticle_fg = true;

                    if (rdb_virtual.Checked)
                        article.VirtualCat_fg = true;

                    context.Add(article);
                    context.SaveChanges();


                    reset_AddPanel();

                    RadGrid1.Rebind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Article Name must be unique');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Article Name must be provided');", true);
            }
        }


        void DeleteArticleCat(int ArtCatId)
        {
            int counter = (from c in context.Articles
                           where c.CategoryId == ArtCatId && c.Suspended == false
                           select c).Count();

            

            //int cnt = ForumThread.GetThreadCount(forumId, ThreadFilter.None, null, true); // BizDAL.GetBizCount("CatId=" + CatId.Value);
            if (counter > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete article category because it contains one or more items.');", true);
            }
            else
            {
                if (EPM.Business.Model.Admin.MainMenuController.ContentCategoryUsed("Article/News", ArtCatId))
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete article category because it is being used in the main menu.');", true);
                else
                {

                    ArticleModel.ArticleCategory art_cat = (from c in context.ArticleCategories
                                                            where c.ArtCatId == ArtCatId
                                                            select c).Single();
                    art_cat.Deleted_fg = true;

                    context.SaveChanges();
                    RadGrid1.Rebind();
                    //Forum.DeleteForum(forumId);
                }
            }
        }

        protected void CatRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }



        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    GridEditManager manager = item.EditManager;
                    GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("ArtCatID") as GridTextBoxColumnEditor;
                    SettingName.TextBoxControl.Enabled = false;
                    SettingName.TextBoxControl.Width = Unit.Pixel(800);


                    GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("CatName") as GridTextBoxColumnEditor;
                    SettingDescr.TextBoxControl.Width = Unit.Pixel(800);

                    GridCheckBoxColumnEditor SettingLink = manager.GetColumnEditor("LinkArticle_fg") as GridCheckBoxColumnEditor;
                    SettingLink.CheckBoxControl.Enabled = false;

                    GridCheckBoxColumnEditor SettingVirtual = manager.GetColumnEditor("VirtualCat_fg") as GridCheckBoxColumnEditor;
                    SettingVirtual.CheckBoxControl.Enabled = false;

                }
            }
        }

        protected void OpenAccessLinqDataSource1_Deleting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceDeleteEventArgs e)
        {

        }

        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem updateitem = e.Item as GridEditableItem;
            string artid = updateitem.GetDataKeyValue("ArtCatId").ToString();
            ArticleModel.ArticleCategory articleCatUpdate = context.ArticleCategories.Single(c => c.ArtCatId == int.Parse(artid));
            articleCatUpdate.CatName = (updateitem["CatName"].Controls[0] as TextBox).Text;
            context.SaveChanges();
        }
    }
}