using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


using EPM.Legacy.Data;
using EPM.Legacy.Security;


    public partial class Mobile_Common_MobileAdDetail1 : System.Web.UI.UserControl
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int catId = 1;//int.Parse(Request.QueryString["p"]);
                    int aid = int.Parse(Request.QueryString["aid"]);

                    bindAdView(aid);
                    bindImages(aid);

                    AccessControl ac = AccessControl.SelectAccessControlByUserName(
                                    Page.User.Identity.Name, ResourceType.Classified, catId);
                    if (ac == null)
                        ac = new AccessControl(Permission.None);

                    if (!ac.CanRead)
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();

                    //ModLink.Visible = ac.CanModify;
                    //DelButton.Visible = ac.CanDelete;

                    //CommentPanel1.CommentType = CommentType.ClassifiedComment;

                    //((MasterPage1)this.Page.Master).SetSideMenu(3, catId, "광고 보기");
                }
                catch { }
            }
        }

        void bindAdView(int id)
        {



            ClassifiedModel.ClassifiedAd a = context.ClassifiedAds.SingleOrDefault(c => c.AdId == id);

            if (a == null)
                return;
            
            Subject.Text = a.Subject;
            PostBy.Text = a.PostBy;
            PostDate.Text = a.RegDate.ToString("MM/dd/yyyy");
            Email.Text = a.Email;
            Phone.Text = a.Phone;
            ViewCount.Text = a.ViewCount.ToString();
            //IpAddr.Text = a.IpAddress;
            Desc.Text = a.Description;

            if (string.IsNullOrEmpty(a.Description))
            {
                Desc.Text = "No Descrition";
            }

            if (string.IsNullOrEmpty(a.Phone))
            {
                tel_link.Attributes.Add("Style", "DISPLAY: none;");
            }

            if (string.IsNullOrEmpty(a.Email))
            {
                mail_link.Attributes.Add("Style", "DISPLAY: none;");
            }

            tel_link.Attributes.Add("href", "tel:" + a.Phone);
            mail_link.Attributes.Add("href", "mailto:" + a.Email);
            
            //if (a.ImageCount > 0)
            //    PicFrame.Attributes["src"] = "GalleryView.aspx?aid=" + id;

            //PicFrame.Visible = (a.ImageCount > 0);

            //Ad.IncreaseViewCount(id, Request, Response);
        }

        void bindImages(int id)
        {
            string[] fileNames = context.ClassifiedImages.Where(c=>c.AdId == id).Select(c=>c.FileName).ToArray();



            if (fileNames.Length > 0)
            {
                thumbnailPath(fileNames);

            }
        }

        void thumbnailPath(string[] filennames)
        {
            List<ImagePathes> imagepath = new List<ImagePathes>();
            foreach (string filename in filennames)
            {

                imagepath.Add(new ImagePathes(Request.ApplicationPath, filename, filename));

            }
            if (imagepath.Count() > 0)
            {
                ImageGallery.DataSource = imagepath;
                ImageGallery.DataBind();
            }
            
        }


        public class ImagePathes
        {
            

            private string fullsize;
            public string FullSize
            {
                get{return fullsize;}
                set { fullsize = value; }
            }

            private string thumbsize;
            public string ThumbSize
            {
                get { return thumbsize; }
                set { thumbsize = value; }
            }
            public ImagePathes(string rootpath, string fs, string ts)
            {
                FullSize = rootpath + EPM.Core.Admin.SiteSettings.ClassifiedUploadRoot + "/" + fs;
                ThumbSize = rootpath + EPM.Core.Admin.SiteSettings.ClassifiedUploadRoot + "/Thumb/" + ts.Insert(ts.LastIndexOf("."), "_t");
                    
            }
        }

        protected void ImageGallery_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {


             try
            {
                ImagePathes i = e.Item.DataItem as ImagePathes;

                if (e.Item.ItemType == ListItemType.Item ||
                    e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Image img = e.Item.FindControl("thumbnail") as Image;

                    //img.Attributes.Add("src", i.ThumbSize);
                    img.Attributes.Add("width", "72px");
                    img.Attributes.Add("height", "54px");
                    img.Attributes.Add("src", i.FullSize);
                    img.Attributes.Add("data-large", i.FullSize);
                }
            }
            catch
            {
            }
        }


        


        //public void BindAdView(Ad ad)
        //{
        //    if (ad == null)
        //        return;

        //    this.Page.Title = "시애틀 교차로-" + ad.Subject;

        //    Subject.Text = ad.Subject;
        //    PostBy.Text = ad.PostBy;
        //    PostDate.Text = ad.RegDate.ToString("MM/dd/yyyy HH:mm");
        //    Email.Text = ad.Email;
        //    Phone.Text = ad.Phone;
        //    ViewCount.Text = ad.ViewCount.ToString();
        //    IpAddr.Text = ForumUtility.ToMaskedIpAddress(ad.IpAddress);
        //    Desc.Text = ad.Description;

        //    UserName.Value = ad.UserName;

        //    if (ad.ImageCount > 0)
        //        PicFrame.Attributes["src"] = "../Common/GalleryView.aspx?aid=" + ad.AdId;

        //    PicFrame.Visible = (ad.ImageCount > 0);

        //    CommentPanel1.CommentType = CommentType.ClassifiedComment;
        //    CommentPanel1.SourceId = ad.AdId;

        //    Ad.IncreaseViewCount(ad.AdId, Request, Response);
        //}

        //public void BindAddress(string addr, string areaName)
        //{
        //    if (!string.IsNullOrEmpty(addr))
        //    {
        //        addr +=  " " + areaName + ",WA";
        //        MapFrame.Attributes.Add("src", "../Common/MapView.aspx?addr=" + Server.UrlEncode(addr));
        //        mapPnl.Visible = true;
        //    }
        //}

        //protected void ConfirmButton_Click(object sender, EventArgs e)
        //{
        //    if (Ad.VerifyPassword(CommentPanel1.SourceId, DelPassword.Text) ||
        //       (Page.User.Identity.Name != string.Empty && Page.User.Identity.Name.Equals(UserName.Value)))
        //    {
        //        Ad.DeleteAd(CommentPanel1.SourceId);
        //    }
        //    else if (AccessControl.AuthorizeUser(Page.User.Identity.Name, ResourceType.Classified,
        //        int.Parse(Request.QueryString["p"]), Permission.FullControl))
        //    {
        //        Ad.DeleteAd(CommentPanel1.SourceId);
        //    }
        //    else
        //    {
        //        ConfirmMpe.Show();
        //        return;
        //    }

        //    Response.Redirect(ListLink.NavigateUrl);

        //}

        //protected void DelButton_Click(object sender, EventArgs e)
        //{
        //    ConfirmMpe.Show();
        //}

        //protected void AdRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    try
        //    {
        //        ImagePathes i = e.Item.DataItem as ImagePathes;

        //        if (e.Item.ItemType == ListItemType.Item ||
        //            e.Item.ItemType == ListItemType.AlternatingItem)
        //        {
        //            Image img = e.Item.FindControl("imageviewer") as Image;

        //            img.Attributes.Add("src", i.ThumbSize);
        //            img.Attributes.Add("data-large", i.FullSize);
        //        }
        //    }
        //    catch
        //    {
        //    }

        //}
    }
