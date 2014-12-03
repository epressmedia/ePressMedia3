using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Linq;
using EPM.Core;



public partial class Cp_Forum_ForumConfig : System.Web.UI.Page
{

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    private static readonly ILog log = LogManager.GetLogger(typeof(Cp_Forum_ForumConfig));
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                
                ControlMode(false);
                int id = int.Parse(Request.QueryString["id"]);
                bindData(id);
                toolbox1.EnableButtons("edit", true);
                
            }
            catch (Exception ex)
            {
                toolbox1.EnableButtons("", true);
                log.Error(ex.Message);
            }
        }
        toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
    }


    void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        string action = e.Item.Text.ToLower();

        if (action == "edit")
        {
            ControlMode(true);
            toolbox1.EnableButtons("save",true);
            toolbox1.EnableButtons("cancel", false);
        }
        else if (action == "cancel")
        {
            ControlMode(false);
            toolbox1.EnableButtons("edit", true);
        }
        else if (action == "save")
        {
            SaveButtonClick();
            ControlMode(false);
            toolbox1.EnableButtons("edit", true);
        }
    }



    void ControlMode(bool enable)
    {
        AllowAttach.Enabled = AllowPrivacy.Enabled = PrivateOnly.Enabled = NotifyPost.Enabled = MailList.Enabled =  enable;
    }

    void bindData(int id)
    {

        var c = (from con in context.ForumConfigs
                 where con.ForumId == id
                     select new
                     {
                         ForumName = con.Forum.ForumName,
                         HeaderImage = con.HdrImg,
                         RowsPerPage = con.RowsPerPage,
                         AllowPrivacy = con.AllowPrivacy,
                         AllowAttachment = con.AllowAttach,
                         PrivateOnly = con.PrivateOnly,
                         NotifyPost = con.NotifyPost,
                         MailingList = con.MailList
                     }).Single();
        

        //ForumConfig c = ForumConfig.SelectById(id);
        ForumName.Text = c.ForumName;

        
        
        AllowPrivacy.Checked = c.AllowPrivacy;
        PrivateOnly.Checked = c.PrivateOnly;
        AllowAttach.Checked = c.AllowAttachment;
        NotifyPost.Checked = c.NotifyPost;
        MailList.Text = c.MailingList;

        
    }


    void SaveButtonClick()
    {
        string saveName = "";

        try
        {
            ForumModel.ForumConfig config = (from c in context.ForumConfigs
                                             where c.ForumId == int.Parse(Request.QueryString["id"])
                                             select c).Single();


            config.HdrImg = saveName;
            config.RowsPerPage = 0; // int.Parse(RowsPerPage.Text);
            config.AllowPrivacy = AllowPrivacy.Checked;
            config.AllowAttach = AllowAttach.Checked;
            config.PrivateOnly = PrivateOnly.Checked;
            config.NotifyPost = NotifyPost.Checked;
            config.MailList = MailList.Text;

            context.SaveChanges();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Successfully updated.')", true);
           

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Error: "+ex.Message+"')", true);
        }
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("forums.aspx");
    }
}