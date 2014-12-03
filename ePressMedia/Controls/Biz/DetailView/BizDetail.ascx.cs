using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPM.Business.Model.Biz;

namespace ePressMedia.Controls.Biz.DetailView
{
    public partial class BizDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Loadiframe();
                int id = int.Parse(Request.QueryString["id"]);
                bindBizView(id);
                
            }
            catch
            {
            }
        }

        void Loadiframe()
        {
            HtmlIframe mapView = new HtmlIframe();
            mapView.ID = "MapFrame";
            this.FindControl("mapView").Controls.Add(mapView);
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
                MapFrame.Attributes.Add("src", "/Biz/MapView.aspx?addr=" + addr.Replace("#", ""));
                NoMap.Visible = false;
            }
            else
            {
                MapFrame.Visible = false;
                NoMap.Visible = true;
            }

            if (b.BusinessEntityImages.Count() > 0)// ImageCount > 0)
            {
                PicFrame.Visible = true;
                PicFrame.Attributes.Add("src", "/Biz/GalleryView.aspx?id=" + id);
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
        string videoId;
        protected string VideoId
        {
            get { return videoId; }
        }

        protected void EditLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditBiz.aspx?id=" + Request.QueryString["id"]);
        }

        protected void ListLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Biz");
        }
    }
}