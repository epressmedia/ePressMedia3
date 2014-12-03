using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using Knn.Data;


public partial class Controls_ForumThreadBox : System.Web.UI.UserControl
{
    public string SectionTitle
    {
        //get { return MoreLink.Text; }
        //set { MoreLink.Text = value; }
        set { HdrImg.ToolTip = HdrImg.AlternateText = value; }
    }

    public string ImageFile
    {
        set { HdrImg.ImageUrl = "~/img/" + value; }
    }

    public string Forum
    {
        get { return ForumID.Value; }
        set 
        { 
            ForumID.Value = value;
            MoreLink.NavigateUrl = "~/Forum/ForumThreads.aspx?p=" + value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindData();
    }

    void bindData()
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        
        int count = 5;

        var result = (from c in context.ForumThreads
                      where c.ForumId == int.Parse(ForumID.Value) && c.Suspended == false  
                      orderby c.PostDate descending
                      select new
                      {
                          ForumId = c.ForumId,
                          ThreadId = c.ThreadId,
                          ShortSubject = c.Subject.Length > 13 ? c.Subject.Substring(0, 13) : c.Subject
                      }).Take(count);

        Repeater1.DataSource = result;// ForumThread.SelectRecentThreads(int.Parse(ForumID.Value), 5);
        Repeater1.DataBind();
    }
}