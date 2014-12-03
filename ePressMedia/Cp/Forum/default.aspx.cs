using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using log4net;
using System.Linq;
using EPM.Business.Model.Forum;
using EPM.Legacy.Security;

public partial class Cp_Forum_Forums : System.Web.UI.Page
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
            //toolbox1.DisableButton("Add");
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
        txt_description.Text = ForumName.Text = "";
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

            DeleteForum(int.Parse(e.CommandArgument.ToString()));

        }
        else if (e.CommandName.Equals("properties"))
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/forum/CatProperties.aspx?id=" + e.CommandArgument.ToString(), listing_div, null);
        }
        else if (e.CommandName.Equals("config"))
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/PageConfig.aspx?type=forum&cat=" + e.CommandArgument.ToString(), listing_div, null, 1048, 900);
        }
        else if (e.CommandName.Equals("permission"))
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/Pages/permissions.aspx?type=forum&id=" + e.CommandArgument.ToString(), listing_div, null);
        }
    }



    protected void AddButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ForumName.Text))
        {
            ForumModel.Forum forum = new ForumModel.Forum();
            forum.ForumName = ForumName.Text.Trim();
            forum.Description = txt_description.Text.Trim();
            forum.AllowComment = false;
            forum.AllowPost = false;
            forum.SiteId = 1;
            forum.RegDate = DateTime.Now;

            context.Add(forum);
            context.SaveChanges();


            ForumModel.ForumConfig f_config = new ForumModel.ForumConfig();
            f_config.ForumId = forum.ForumId;
            f_config.AllowPrivacy = false;
            f_config.AllowAttach = false;
            f_config.HdrImg = "";
            f_config.MailList = "";
            f_config.MetadataStr = "";
            f_config.NotifyPost = false;
            f_config.PrivateOnly = false;
            f_config.RowsPerPage = 10;

            context.Add(f_config);
            context.SaveChanges();

            reset_AddPanel();

            RadGrid1.Rebind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Forum Name must be provided');", true);   
        }
    }


    void DeleteForum(int catid)
    {
                    if (context.ForumThreads.Any(c => c.Forum.ForumId == catid && c.Suspended == false))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete forum category because it has one or more items.');", true);
            }
            else
            {
                if (EPM.Business.Model.Admin.MainMenuController.ContentCategoryUsed("Forum", catid))
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Cannot delete forum category because it is being used in the main menu.');", true);
                else
                {
                    ForumController.DeleteForumCategory(catid);
                    RadGrid1.Rebind();
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
                GridTextBoxColumnEditor SettingName = manager.GetColumnEditor("ForumID") as GridTextBoxColumnEditor;
                SettingName.TextBoxControl.Enabled = false;
                SettingName.TextBoxControl.Width = Unit.Pixel(800);


                GridTextBoxColumnEditor SettingDescr = manager.GetColumnEditor("ForumName") as GridTextBoxColumnEditor;
                SettingDescr.TextBoxControl.Width = Unit.Pixel(800);


                GridTextBoxColumnEditor SettingValue = manager.GetColumnEditor("Description") as GridTextBoxColumnEditor;
                SettingValue.TextBoxControl.Width = Unit.Pixel(800);



            }
        }
    }

    protected void OpenAccessLinqDataSource1_Deleting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceDeleteEventArgs e)
    {

    }

    protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        GridEditableItem updateitem = e.Item as GridEditableItem;
        ForumModel.Forum forumupdate = context.Forums.Single(c => c.ForumId == int.Parse(updateitem["ForumID"].Text.ToString()));
        forumupdate.ForumName = (updateitem["ForumName"].Controls[0] as TextBox).Text;
        forumupdate.Description = (updateitem["Description"].Controls[0] as TextBox).Text;
        context.SaveChanges();
    }

}