using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Knn;

[Description("Search Rank")]
public partial class Controls_TrendRankBox : System.Web.UI.UserControl
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (HeaderName.EndsWith(".jpg") || HeaderName.EndsWith(".png") || HeaderName.EndsWith(".bmp") || HeaderName.EndsWith(".gif"))
            {
                img_searchrank.Visible = true;
                img_searchrank.Src = HeaderName;
            }
            else
            {
                lbl_searchrank.Visible = true;
                lbl_searchrank.Text = HeaderName;
            }

            var targeDate = DateTime.Now.AddHours(NoOfHours * -1);

            var results = (from c in context.SearchQueries
                           where c.QueryDate >= targeDate
                           group c by c.Query into g
                           select new { Count = g.Count(), Query = g.Key, }).Take(NoOfItems);

            //var newResults = results.OrderByDescending(c=>c.Count()).Take(NoOfItems).AsEnumerable().Select((u, index) => new { Query = u, Index = index });

            var newresults = results.OrderByDescending(c => c.Count).AsEnumerable().Select((obj, index) => new { obj.Query, Index = index+1 });

            RankRepeater.DataSource = newresults;// SearchQuery.GetWeeklyRanks(5);
            RankRepeater.DataBind();
        }
    }
    protected void RankRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Label lbl_text = ((Label)(e.Item.FindControl("lbl_text")));
            int rank = int.Parse(lbl_text.Attributes["ref"].ToString());

            HtmlImage img = e.Item.FindControl("Img1") as HtmlImage;
            img.Src = "~/Img/num" + rank.ToString() + ".png";

                        

            var FromtargeDate = DateTime.Now.AddHours(NoOfHours * -2);
            var TotargeDate = DateTime.Now.AddHours(NoOfHours * -1);

            Image RankImg = e.Item.FindControl("RankImg") as Image;
            Label l = e.Item.FindControl("RankDiff") as Label;

            var results = (from c in context.SearchQueries
                           where c.QueryDate >= FromtargeDate && c.QueryDate < TotargeDate
                           group c by c.Query into g
                           select new { Count = g.Count(), Query = g.Key, }).Take(NoOfItems);
            if (results.Count() > 0)
            {

                var newresults = results.OrderByDescending(c => c.Count).AsEnumerable().Select((obj, index) => new { obj.Query, Index = index + 1 });
                

                var finalRankResult = newresults.Where(c => c.Query == lbl_text.Text);
                int oldRank = -1;
                  if (finalRankResult.Count() > 0)
                  {
                      oldRank = (newresults.Single(c => c.Query == lbl_text.Text)).Index;
                      
                  }
                  


                  int diff = oldRank - rank;

                //QueryRank r = e.Item.DataItem as QueryRank;
                
                if ((diff > NoOfItems) || (oldRank == -1))
                {
                    RankImg.ImageUrl = "~/img/new.png";
                    l.Visible = false;
                }
                else if (diff > 0 && diff <= noOfItems)
                {
                    RankImg.ImageUrl = "~/img/up.png";
                    l.Text = diff.ToString();
                }
                else if (diff < 0)
                {
                    RankImg.ImageUrl = "~/img/dn.png";
                    l.Text = Math.Abs(diff).ToString();
                }
                else // randiff == 0
                {
                    RankImg.Visible = false;
                    l.Text = "---";
                }
            }
            else
            {
                 RankImg.ImageUrl = "~/img/new.png";
                 l.Visible = false;
            }
        }
    }

    private string headerName;
    [Category("KNNProperty"), Description("Name displayed at the top of the control(Text or Image URL)"), Required(ErrorMessage = "HeaderName is required")]
    public string HeaderName
    {
        get;
        set;
    }

    private int noOfItems = 5;
    [Category("KNNProperty"), Description("No of Items to display (Default to 5 items)"), DefaultValue(typeof(System.Int32), "5")]
    public int NoOfItems
    {
        get;
        set;
    }

    private Double noOfHours = 24;
    [Category("KNNProperty"), Description("No of Hours to compare(Default to 24 hours)"), DefaultValue(typeof(System.Double), "24")]
    public Double NoOfHours
    {
        get;
        set;
    }

}