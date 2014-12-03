using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core;
using EPM.Core.Classified;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using log4net;
using EPM.Legacy.Security;
using System.Web.UI.HtmlControls;
using EPM.Business.Model.Classified;

[Description("Classified Detail Control")]
public partial class Classified_AdView : System.Web.UI.UserControl
{

    private static readonly ILog log = LogManager.GetLogger(typeof(Classified_AdView));
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {


                int adId = int.Parse(Request.QueryString["aid"]);


                bindAdView(adId);


                int i = Request.Url.Query.IndexOf("&aid");
                ListLink.NavigateUrl = "~/Classified/list.aspx" +
                                    Request.Url.Query.Substring(0, i);


                int catId = int.Parse(Request.QueryString["p"]);

                AccessControl ac = AccessControl.SelectAccessControlByUserName(
                                Page.User.Identity.Name, ResourceType.Classified, catId);
                if (ac == null)
                    ac = new AccessControl(Permission.None);

                if (!ac.CanRead)
                    System.Web.Security.FormsAuthentication.RedirectToLoginPage();



                EditLink_popup.Visible = ac.CanModify;
                DelButton.Visible = ac.CanDelete;
                if (DelButton.Visible)
                {
                    string msg = "삭제를 위해 글 작성시에 설정한 비밀번호를 입력하세요. 로그인한 상태이고 작성자 본인이면 입력하지 않아도 됩니다.";
                    string path = "/Page/DataEntry.aspx?cid=" + adId.ToString() + "&area=classified&mode=Delete&p=" + catId.ToString() + "&passwordinput=true&returnURL=" + Server.UrlEncode(ListLink.NavigateUrl.Replace("~", "")) + "&msg=" + msg;
                    DeletePopup.Title = "Delete Ad";// +Ad.Subject;
                    DeletePopup.Width = 300;
                    DeletePopup.Height = 200;
                    DelButton.OnClientClick = DeletePopup.GetOpenPath(path);
                }

            }
            catch(Exception ex) {
                log.Error(ex.Message);
            }
        }
    }

    void bindAdView(int id)
    {
        //Ad a = Ad.SelectAdById(id);

        

        ClassifiedModel.ClassifiedAd Ad = context.ClassifiedAds.Single(c => c.AdId == id && c.Suspended == false);
        if (Ad == null)
            return;

        string path = "/Page/DataEntry.aspx?cid=" + id.ToString() + "&area=classified&mode=Edit&p=" + Request.QueryString["p"];
        EntryPopupEdit.Title = "Edit Ad - " + Ad.Subject;
        EditLink_popup.OnClientClick = EntryPopupEdit.GetOpenPath(path);// EntryPopup.OpenMethod + "('" + path + "','" + result.Title.Replace("'", "") + "'); return false;";

        Subject.Text = Ad.Subject;
        PostBy.Text = Ad.PostBy;
        PostDate.Text = Ad.RegDate.ToShortDateString() + " " + Ad.RegDate.ToShortTimeString();
        Email.Text = Ad.Email;
        Phone.Text = Ad.Phone.Trim();
        ViewCount.Text = Ad.ViewCount.ToString();
        IpAddr.Text = Ad.IpAddr.Contains('.')?Ad.IpAddr.Substring(0,Ad.IpAddr.LastIndexOf('.'))+".xxx":"";
        Desc.Text = Ad.Description;


        if (Ad.ClassifiedImages.Count() > 0)
        {
            ImageRepeater.DataSource = ClassifiedImageController.GetImageByClassifiedId(id);
            ImageRepeater.DataBind();
            //PicFrame.Attributes["src"] = "Controls/GalleryView.aspx?aid=" + id;
        }

        // remove picture frame done by iframe due to mobile view limitation
        PicFrame.Visible = false;// (Ad.ClassifiedImages.Count() > 0);

        ClassifiedHelper.IncreaseViewCount(id);
    }


    public void BindAddress(string addr, string areaName)
    {
        if (!string.IsNullOrEmpty(addr))
        {
            
            MapFrame.Attributes.Add("src", "Controls/MapView.aspx?addr=" + Server.UrlEncode(addr));
            mapPnl.Visible = true;
        }
    }

  



    protected void ImageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {


            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ClassifiedModel.ClassifiedImage AdImage = ((ClassifiedModel.ClassifiedImage)(e.Item.DataItem));
                HtmlImage image = e.Item.FindControl("adImage") as HtmlImage;
                image.Src = AdImage.FileName;
            }
        
    }

 
}