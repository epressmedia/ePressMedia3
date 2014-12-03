using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EPM.Business.Model.Biz;
using Knn.Data;
using Knn.Common;



public partial class Biz_BizList : System.Web.UI.UserControl
{
    string videoId;
    protected string VideoId
    {
        get { return videoId; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            CurState.Value = Request.QueryString["st"];
            CurArea.Value = Request.QueryString["area"];

            CurCat.Value = Request.QueryString["cat"];
            SearchVal.Value = Request.QueryString["q"];

            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                bindBizView(id);
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(Request.QueryString["cat"]) == false)
            {
                try
                {
                    int cat = int.Parse(Request.QueryString["cat"]);
                    CurCat.Value = cat.ToString();
                    bindBizByCat(cat);
                }
                catch
                {
                }
            }
            else if (Request.QueryString["q"] != null)
            {
                SearchVal.Value = Request.QueryString["q"];
                bindBizByName();
            }
            else
            {
                bindRecentBiz();
            }
        }
    }

    //void bindPopup()
    //{
    //    List<Popup> popups = Popup.SelectPopupToShow();

    //    if (popups.Count > 0)
    //    {
    //        if (Request.Cookies["pop" + popups[0].PopupId] == null)
    //        {
    //            PopupText1.Text = popups[0].Body;
    //            PopupLink1.NavigateUrl = popups[0].LinkUrl;
    //            PopupLink1.Target = popups[0].LinkTarget;
    //            PopupLink1.Visible = !string.IsNullOrEmpty(popups[0].LinkUrl);
    //            Pop1.Width = popups[0].Width;
    //            Pop1.Height = popups[0].Height;
    //            PopCheck1.Attributes["value"] = popups[0].PopupId.ToString();
    //        }
    //        else
    //        {
    //            Pop1.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        Pop1.Visible = false;
    //    }

    //    if (popups.Count > 1)
    //    {
    //        if (Request.Cookies["pop" + popups[1].PopupId] == null)
    //        {
    //            PopupText2.Text = popups[1].Body;
    //            PopupLink2.NavigateUrl = popups[1].LinkUrl;
    //            PopupLink2.Target = popups[1].LinkTarget;
    //            PopupLink2.Visible = !string.IsNullOrEmpty(popups[1].LinkUrl);
    //            Pop2.Attributes["style"] += "left: " + (popups[0].Width + 100) + "px";
    //            Pop2.Width = popups[1].Width;
    //            Pop2.Height = popups[1].Height;
    //            PopCheck2.Attributes["value"] = popups[1].PopupId.ToString();
    //        }
    //        else
    //        {
    //            Pop2.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        Pop2.Visible = false;
    //    }
    //}


    void bindRecentBiz()
    {
        PageTitle.Text = "* 신규등록된 업소입니다. 업소명을 클릭하시면 자세한 정보를 볼 수 있습니다.";

        List<BizModel.BusinessEntity> biz = BEController.GetALLActiveBEs().OrderBy(c => c.BusinessEntityId).Take(5).ToList();// BizDAL.SelectBiz("Suspended=0 AND closed=0 AND Approved=1", "BizId DESC", 1, 5);
        BizRepeater.DataSource = biz;
        BizRepeater.DataBind();

        FitPager1.Visible = false;
    }

    void bindBizByCat(int cat)
    {
       // VideoAdPnl.Visible = MainFlash.Visible = false;
        //NameValueCollection c = BizCategory.GetBizCategories("CatId=" + cat);
        //PageTitle.Text = "* 업종 &quot;" + c.AllKeys[0] + "&quot;로 검색한 결과입니다.";
        bindPageTitle();
        bindBiz();
    }

    void bindBizByName()
    {
        //VideoAdPnl.Visible = MainFlash.Visible = false;
        //PageTitle.Text = "* 업소명 &quot;" + SearchVal.Value + "&quot;로 검색한 결과입니다.";
        bindPageTitle();
        bindBiz();
    }

    void bindPageTitle()
    {
        StringBuilder sb = new StringBuilder("* ");
        if (!string.IsNullOrEmpty(CurState.Value))
            sb.Append(CurState.Value + " 주");

        if (!string.IsNullOrEmpty(CurArea.Value))
        {
            //List<Area> area = Area.SelectAreasbyID(int.Parse(CurArea.Value));
            //if (area.Count > 0)
            sb.Append(" " + CurArea.Value + "시");
            
        }

        if (!string.IsNullOrEmpty(CurCat.Value))
        {
            //NameValueCollection c = BizCategory.GetBizCategories("CatId=" + CurCat.Value);
            //sb.Append(" 업종 &quot;<span class='selCat'>" + c.AllKeys[0] + "</span>&quot; ");
            string name = BECategoryController.GetBusinessCatgoryByID(int.Parse(CurCat.Value)).CategoryName;
            sb.Append(" 업종 &quot;<span class='selCat'>" + name + "</span>&quot; ");
            
        }

        if (sb.Length > 2)
            sb.Append("에서 ");

        if (!string.IsNullOrEmpty(SearchVal.Value))
            sb.Append(" 업소명 &quot;<span class='selCat'>" + SearchVal.Value + "</span>&quot;로 검색한 결과 입니다.");
        else
            sb.Append(" 전체 업소를 검색한 결과입니다.");

        PageTitle.Text = sb.ToString();

    }

    void bindBiz()
    {
        int cnt = BEController.GetALLActiveBEs().Count();// BizDAL.GetBizCount(getFilter());
        FitPager1.Visible = (cnt > 0);
        FitPager1.TotalRows = cnt;
        FitPager1.CurrentPage = 1;

        bindBizPage(1);
    }

    void bindBizPage(int page)
    {
        var biz = BEController.GetALLActiveBEs().OrderBy(c => c.PrimaryName).Skip((page - 1) * FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage);


        BizRepeater.DataSource = biz;// BizDAL.SelectBiz(getFilter(), "BizName", page, FitPager1.RowsPerPage);
        BizRepeater.DataBind();

        if (page == 1)
        {
            AdOwnerBizRepeater.DataSource = null;// BizDAL.SelectAdOwnerBiz(getFilter());
            AdOwnerBizRepeater.DataBind();
            AdOwnerBizRepeater.Visible = true;
        }
        else
        {
            AdOwnerBizRepeater.Visible = false;
        }
    }

    string getFilter()
    {
        StringBuilder filter = new StringBuilder();

        if (false == string.IsNullOrEmpty(CurCat.Value))   // category set
            filter.Append("Biz.CatId=" + CurCat.Value + " AND ");

        filter.Append("Suspended=0 AND closed=0 AND Approved=1");


        if (!string.IsNullOrEmpty(CurState.Value) && !CurState.Value.Equals("0"))
            filter.Append(" AND AdState='" + CurState.Value + "'");

        if (!string.IsNullOrEmpty(CurArea.Value) && !CurArea.Value.Equals("0"))
            filter.Append(" AND Area='" + CurArea.Value+"'");
        

        filter.Append(getBizNameFilter());

        return filter.ToString();
    }

    string getBizNameFilter()
    {
        if (string.IsNullOrEmpty(SearchVal.Value.Trim()))
            return "";

        char[] del = { ' ' };

        string[] words = SearchVal.Value.Trim().Split(del);

        StringBuilder filter = new StringBuilder(" AND (");
        for (int i = 0; i < words.Length; i++)
        {
            if (string.IsNullOrEmpty(words[i]))
                continue;

            filter.Append("BizName LIKE N'%" + words[i] + "%' OR BizNameEng LIKE N'%" + words[i] +
                "%' OR BizPhone1 LIKE N'%" + words[i] + "%' OR BizPhone2 LIKE N'%"+ words[i]+"%'");
            if (i != words.Length - 1)
                filter.Append(" OR ");
        }

        filter.Append(")");

        return filter.ToString();
    }

    void bindBizView(int id)
    {
        //VideoAdPnl.Visible = MainFlash.Visible = false;
        BizViewPanel.Visible = true;


        BizModel.BusinessEntity b = BEController.GetBEbyBEID(id);
        //Biz b = BizDAL.SelectBizById(id);


        BEController.AddBusinessEntityViewHistory(id);
        //BizDAL.IncreaseViewCount(id, Request, Response);

        BizTitle.Text = b.PrimaryName;
        EngBizTitle.Text = b.SecondaryName;
        ShortDesc.Text = b.ShortDesc;
        BizDescription.Text = b.Description.Replace("\r\n", "<br />");

        Phone.Text = b.Phone1 + "&nbsp;&nbsp;&nbsp;" + b.Phone2;
        Fax.Text = b.Fax;
        Address.Text = b.Address + ((string.Empty == b.Address) ? "" : ", " + b.City + ", " + b.State) +
                    " " + b.ZipCode;
        HomePage.Text = b.Website;
        HomePage.NavigateUrl = "http://" + b.Website;
        CatName.Text = b.BusienssCategory.CategoryName;
        Email.Text = b.Email;

        //BizHour.Text = b.BizHour;

        if (!string.IsNullOrEmpty(b.Address))
        {
            string addr = Address.Text.Replace(' ', '+');

            MapFrame.Visible = true;
            MapFrame.Attributes.Add("src", "MapView.aspx?addr=" + addr.Replace("#", ""));
            NoMap.Visible = false;
        }
        else
        {
            MapFrame.Visible = false;
            NoMap.Visible = true;
        }

        if (b.BusinessEntityImages.Count()> 0)// ImageCount > 0)
        {
            PicFrame.Visible = true;
            PicFrame.Attributes.Add("src", "GalleryView.aspx?id=" + id);
            NoPic.Visible = false;
        }
        else
        {
            PicFrame.Visible = false;
            NoPic.Visible = true;
        }

        if (!string.IsNullOrEmpty(b.VideoURL))
        {
            videoId = b.VideoURL;
            ytubeView.Visible = true;
            NoVid.Visible = false;
        }
        else
        {
            ytubeView.Visible = false;
            NoVid.Visible = true;
        }

        if (b.BusinessEntityImages.Count() > 0)// ImageCount > 0)
        {
            //GalFrame.Attributes.Add("src", "SlideShow.aspx?id=" + b.BizId);
            //GalPanel.Visible = true;
        }
        else
        {
            //GalPanel.Visible = false;
        }

        movIcon.Visible = !string.IsNullOrEmpty(b.VideoURL);
        picIcon.Visible = BEThumbnailController.BizThumbnailExists(b.BusinessEntityId);// !string.IsNullOrEmpty( b. Thumbnail);
    }

    protected void AdOwnerBizRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            BizModel.BusinessEntity b = e.Item.DataItem as BizModel.BusinessEntity;

            HyperLink l = e.Item.FindControl("BizLink1") as HyperLink;
            l.NavigateUrl = "Default.aspx?id=" + b.BusinessEntityId;
            if (!string.IsNullOrEmpty(CurCat.Value))
                l.NavigateUrl += "&cat=" + CurCat.Value;
            if (!string.IsNullOrEmpty(SearchVal.Value))
                l.NavigateUrl += "&q=" + Server.UrlEncode(SearchVal.Value);
            if (!string.IsNullOrEmpty(CurState.Value))
                l.NavigateUrl += "&st=" + CurState.Value;
            if (!string.IsNullOrEmpty(CurArea.Value))
                l.NavigateUrl += "&area=" + CurArea.Value;

            if (b.SecondaryName != string.Empty)
            {
                HyperLink lnk = e.Item.FindControl("BizLink2") as HyperLink;
                lnk.NavigateUrl = l.NavigateUrl;
            }

            /*
            if (!string.IsNullOrEmpty(CurCat.Value))
                l.NavigateUrl = "Default.aspx?id=" + b.BizId + "&cat=" + CurCat.Value;
            else if (!string.IsNullOrEmpty(SearchVal.Value))
                l.NavigateUrl = string.Format("Default.aspx?id={0}&q={1}&st={2}&area={3}",
                    b.BizId, Server.UrlEncode(SearchVal.Value), CurState.Value, CurArea.Value);
            */
            Image i = e.Item.FindControl("BizImg") as Image;
            if (i.ImageUrl == "")
                i.ImageUrl = "~/Img/biz_noThumb.png";

            e.Item.FindControl("MovIcon").Visible = !string.IsNullOrEmpty(b.VideoURL);
            e.Item.FindControl("PicIcon").Visible = BEThumbnailController.BizThumbnailExists(b.BusinessEntityId);// !string.IsNullOrEmpty(b.Thumbnail);
        }
    }


    protected void BizRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            BizModel.BusinessEntity b = e.Item.DataItem as BizModel.BusinessEntity;

            HyperLink l = e.Item.FindControl("BizLink1") as HyperLink;
            l.NavigateUrl = "Biz.aspx?id=" + b.BusinessEntityId;
            if (!string.IsNullOrEmpty(CurCat.Value))
                l.NavigateUrl += "&cat=" + CurCat.Value;
            if (!string.IsNullOrEmpty(SearchVal.Value))
                l.NavigateUrl += "&q=" + Server.UrlEncode(SearchVal.Value);
            if (!string.IsNullOrEmpty(CurState.Value))
                l.NavigateUrl += "&st=" + CurState.Value;
            if (!string.IsNullOrEmpty(CurArea.Value))
                l.NavigateUrl += "&area=" + CurArea.Value;

            if (b.SecondaryName != string.Empty)
            {
                HyperLink lnk = e.Item.FindControl("BizLink2") as HyperLink;
                lnk.NavigateUrl = l.NavigateUrl;
            }

            /*
            if (!string.IsNullOrEmpty(CurCat.Value))
                l.NavigateUrl = "Default.aspx?id=" + b.BizId + "&cat=" + CurCat.Value;
            else if (!string.IsNullOrEmpty(SearchVal.Value))
                l.NavigateUrl = string.Format("Default.aspx?id={0}&q={1}&st={2}&area={3}",
                    b.BizId, Server.UrlEncode(SearchVal.Value), CurState.Value, CurArea.Value);
            */
            Image i = e.Item.FindControl("BizImg") as Image;
            if (i.ImageUrl == "")
                i.ImageUrl = "~/Img/biz_noThumb.png";

            e.Item.FindControl("MovIcon").Visible = !string.IsNullOrEmpty(b.VideoURL);
            e.Item.FindControl("PicIcon").Visible = BEThumbnailController.BizThumbnailExists(b.BusinessEntityId);// !string.IsNullOrEmpty(b.Thumbnail);
        }
    }

    protected void PageNumber_Changed(object sender, EventArgs e)
    {
        bindBizPage(FitPager1.CurrentPage);
    }

    protected void ListLink_Click(object sender, EventArgs e)
    {
        BizViewPanel.Visible = false;
    }

    protected void EditLink_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditBiz.aspx?id=" + Request.QueryString["id"]);
    }

}