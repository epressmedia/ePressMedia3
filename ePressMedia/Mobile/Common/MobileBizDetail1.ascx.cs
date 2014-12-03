using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;

using EPM.Legacy.Data;


    public partial class Mobile_Common_MobileBizDetail1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int bizId = int.Parse(Request.QueryString["bizId"].ToString());
            bindBizView(bizId);
        }
        DataFilterCollection getfilter(int bizId)
        {
            DataFilterCollection filters = new DataFilterCollection();
            filters.AddFilter(new BizIdFilter(bizId.ToString()));

            return filters;
        }


        //Missing in KNN DLL -- This should be added to KNN.BizAd.BizFilter when versioning happens
        public class BizIdFilter : DataValueFilter
        {
            public BizIdFilter(string value) : base(value) { }

            public override string ToFilterString()
            {
                if (SelectedValue.Trim().Equals(string.Empty))
                    return string.Empty;
                else
                    return "bizId=" + SelectedValue;

            }
        }

        void bindBizView(int bizId)
        {
            //BizId.Value = bizId.ToString();

            BizViewPanel.Visible = true;

  
            BizModel.BusinessEntity b = BEController.GetBEbyBEID(bizId);

            //EditLink.NavigateUrl = "EditBiz.aspx?id=" + id;

            BizTitle.Text = b.PrimaryName;
            ShortDesc.Text = b.ShortDesc;

            // the biz thumb nail is not specific in the config. This can be recoded as the KNN is updated in the future.

            //ConfigurationManager.AppSettings["FullWebSiteAddress"] + ConfigurationManager.AppSettings["ClsUploadRoot"]
            //Thumb.ImageUrl = string.IsNullOrEmpty(b.Thumbnail) ? "~/Img/NoAdThumb.png" : b.Thumbnail;
            //Thumb.AlternateText = b.Name;

            if (string.IsNullOrEmpty(b.Phone1))
            {
                tel_link.Attributes.Add("Style", "DISPLAY: none;");
            }
            else
            {
                Phone1.Text = b.Phone1;
                tel_link.Attributes.Add("href", "tel:" + b.Phone1);
            }
            ;
            //Phone2.Text = b.BizPhone2;

            if (string.IsNullOrEmpty(b.Fax))
            {
                fax_link.Attributes.Add("Style", "DISPLAY: none;");
            }
            else
            {
                Fax.Text = b.Fax;
                fax_link.Attributes.Add("href", "fax:" + b.Fax);
            }



            if (string.IsNullOrEmpty(b.Website))
            {
                homepage_link.Attributes.Add("Style", "DISPLAY: none;");
            }
            else
            {
                HomePage.Text = b.Website;
                homepage_link.Attributes.Add("OnClick", "window.open('http://" + b.Website + "');");
                //homepage_link.Attributes.Add("href", b.Url);
                //homepage_link.Attributes.Add("target", "_blank");
            }


            Address.Text = b.Address;

            //BizHour.Text = b.BizHour;
            //Parking.Text = b.Parking;
            Payments.Text = "";

            Desc.Text = b.Description;


            zipcode.Value = b.ZipCode.ToString();
            

            //if (!string.IsNullOrEmpty(b.Address))
            //{
            //    string addr = Address.Text.Replace(' ', '+');
            //    if (addr.StartsWith("#"))
            //        addr = addr.Substring(1);

            //    MapPanel.Visible = true;
            //    MapFrame.Attributes.Add("src", "../Classified/MapView.aspx?addr=" + addr);
            //}
            //else
            //{
            //    MapPanel.Visible = false;
            //}

            //if (!string.IsNullOrEmpty(b.VideoId))
            //{
            //    VidIdField.Value = b.VideoId;
            //    VideoPanel.Visible = true;
            //}
            //else
            //{
            //    VideoPanel.Visible = false;
            //}

            //if (b.ImageCount > 0)
            //{
            //    GalFrame.Attributes.Add("src", "SlideShow.aspx?id=" + b.BizId);
            //    GalPanel.Visible = true;
            //}
            //else
            //{
            //    GalPanel.Visible = false;
            //}
        }
    }
