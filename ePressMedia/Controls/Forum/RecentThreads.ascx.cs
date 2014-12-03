using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using Knn.Data;
//using Knn.Forum;

public partial class Forum_RecentThreads : System.Web.UI.UserControl
{
    public string ImageFile
    {
        set { HdrImg.ImageUrl = "~/img/" + value; }
    }

    public string SectionTitle
    {
        set { HdrImg.ToolTip = HdrImg.AlternateText = value; }
        //set { SecTitle.Text = value; }
    }

    public string Forums
    {
        set { ForumIDs.Value = value; }
    }

    public string HeaderLinkForum
    {
        set { HdrLink.NavigateUrl = "~/Forum/ForumThreads.aspx?p=" + value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindData();
    }

    void bindData()
    {
        if (string.IsNullOrEmpty(ForumIDs.Value))
            return;


        var forumIds = ForumIDs.Value.Split(',').ToList();

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        var query = (from c in context.ForumThreads
                     where forumIds.Contains(c.ForumId.ToString()) && c.Suspended == false 
                     orderby c.PostDate descending
                     select new
                     {
                         ForumId = c.ForumId,
                         ThreadId = c.ThreadId,
                         ForumName = c.Forum.ForumName,
                         Subject = c.Subject,
                         ShortSubject = c.Subject.Length > 14 ? c.Subject.Substring(0, 20) : c.Subject
                     }).Take(6);
        Repeater1.DataSource = query;
        Repeater1.DataBind();






        //char[] dels = { ',' };
        //if (string.IsNullOrEmpty(ForumIDs.Value))
        //    return;
        //string[] idStrs = ForumIDs.Value.Split(dels);
        //int[] ids = new int[idStrs.Length];

        //for (int i = 0; i < idStrs.Length; i++)
        //    ids[i] = int.Parse(idStrs[i]);

        //Repeater1.DataSource = ForumThread.SelectRecentThreads(ids, 6);
        //Repeater1.DataBind();
    }
}