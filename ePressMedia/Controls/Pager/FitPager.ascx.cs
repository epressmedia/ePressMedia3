using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_FitPager : System.Web.UI.UserControl
{
    const int numericLinkCount = 10;

    public event EventHandler PageNumberChanged;

    public Unit Width
    {
        get { return PagerContainer.Width; }
        set { PagerContainer.Width = value; }
    }

    public HorizontalAlign HorizontalAligh
    {
        get { return PagerContainer.HorizontalAlign; }
        set { PagerContainer.HorizontalAlign = value; }
    }

    public string CssClass
    {
        get { return PagerContainer.CssClass; }
        set { PagerContainer.CssClass = value; }
    }

    public int TotalRows
    {
        get
        {
            if (ViewState["TotalRows"] == null)
                return 0;
            else
                return (int)ViewState["TotalRows"];
        }
        set
        {
            ViewState["TotalRows"] = value;

            CurrentPage = 1;

            ViewState["TotalPages"] = (value - 1) / RowsPerPage + 1;

            setSatrtPageNum(1);
        }
    }

    public int TotalPages
    {
        get
        {
            if (ViewState["TotalPages"] == null)
                return 0;
            else
                return (int)ViewState["TotalPages"];
        }
    }

    public int RowsPerPage
    {
        get
        {
            if (ViewState["RowsPerPage"] == null)
                return 10;
            else
                return (int)ViewState["RowsPerPage"];
        }
        set
        {
            ViewState["RowsPerPage"] = value;
        }
    }

    public int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] == null)
                return 0;
            else
                return (int)ViewState["CurrentPage"];
        }
        set
        {
            ViewState["CurrentPage"] = value;
            enableAllLinks(true);

            setSatrtPageNum(((value - 1) / numericLinkCount) * numericLinkCount + 1);

            pageNumLinks[(value - 1) % numericLinkCount].Attributes["class"] = "selPage";
        }
    }

    public int StartPageNum
    {
        get
        {
            if (ViewState["StartPageNum"] == null)
                return 1;
            else
                return (int)ViewState["StartPageNum"];
        }
    }

    void enableAllLinks(bool enabled)
    {
        for (int i = 0; i < 10; i++)
        {
            pageNumLinks[i].Attributes.Remove("class");
            //pageNumLinks[i].Enabled = enabled;
        }
    }


    void setSatrtPageNum(int n)
    {
        ViewState["StartPageNum"] = n;

        for (int i = 0; i < 10; i++)
        {
            pageNumLinks[i].Text = (n + i).ToString();
            pageNumLinks[i].Visible = (n + i <= TotalPages);
        }

        PrevLink.Visible = (n != 1);
        NextLink.Visible = (n + numericLinkCount <= TotalPages);
    }

    LinkButton[] pageNumLinks;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LinkNext_Click(object sender, EventArgs e)
    {
        CurrentPage = StartPageNum + numericLinkCount;
        PageNumberChanged(this, null);
    }

    protected void PrevLink_Click(object sender, EventArgs e)
    {
        CurrentPage = StartPageNum - 1;
        //setSatrtPageNum(StartPageNum - numericLinkCount);
        if (PageNumberChanged != null)
            PageNumberChanged.Invoke(this, null);
    }

    protected void PageNumLink_Click(object sender, EventArgs e)
    {
        LinkButton l = sender as LinkButton;
        CurrentPage = Int32.Parse(l.Text);

        if (PageNumberChanged != null)
            PageNumberChanged.Invoke(this, null);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        pageNumLinks = new LinkButton[10] { Link1, Link2, Link3, Link4, Link5, Link6, Link7, Link8, Link9, Link10 };
    }

}