using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EPM.Core;

public partial class Cp_SetPrevIssue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        //    try
        //    {
        //        int id = int.Parse(Request.QueryString["id"]);
        //    //    PrevIssue iss = PrevIssue.SelectById(id);
        //        IssueId.Value = iss.Id.ToString();
        //        IssueName.Text = iss.Title;
        //        Url.Text = iss.Url;
        //        ImgUrl.Text = iss.ImageUrl;
        //    }
        //    catch
        //    {
        //        SaveButton.Enabled = false;
        //    }
        }
    }

    //protected void SaveButton_Click(object sender, EventArgs e)
    //{
    // //   PrevIssue.SetIssue(int.Parse(Request.QueryString["id"]),
    //        IssueName.Text, Url.Text, ImgUrl.Text);

    //    Response.Redirect("PrevIssues.aspx");
    //}
    
    //protected void CancelButton_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("PrevIssues.aspx");
    //}
}