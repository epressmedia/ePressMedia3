using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EPM.Core;

public partial class Cp_PrevIssues : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repeater1.DataSource = PrevIssue.Select();
            Repeater1.DataBind();
        }
    }
}