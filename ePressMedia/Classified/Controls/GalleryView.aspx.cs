using System;
using System.Collections.Generic;
using Sd = System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using System.Web.UI.HtmlControls;

public partial class Classified_GalleryView : System.Web.UI.Page
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int id = int.Parse(Request.QueryString["aid"]);
                bindImages(id);
            }
            catch
            { }
        }
    }

    void bindImages(int id)
    {
        List<ClassifiedModel.ClassifiedImage> fileNames = (from c in context.ClassifiedImages
                              where c.AdId == id
                              select c).ToList();

        if (fileNames.Count() > 0)
        {
            PhotoRepeater.DataSource = fileNames;
            PhotoRepeater.DataBind();

            LinkRepeater.DataSource = fileNames;
            LinkRepeater.DataBind();
        }
    }

    protected void PhotoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {

            ClassifiedModel.ClassifiedImage im = e.Item.DataItem as ClassifiedModel.ClassifiedImage;

            Image BizImg = e.Item.FindControl("BizImg") as Image;
            BizImg.ImageUrl = im.FileName;

            HtmlAnchor BizImg_link = e.Item.FindControl("BizImg_link") as HtmlAnchor;
            BizImg_link.HRef = im.FileName;


            resizeImage(BizImg, e.Item.ItemIndex);
        }
    }

    protected void LinkRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
    e.Item.ItemType == ListItemType.AlternatingItem)
        {

            ClassifiedModel.ClassifiedImage im = e.Item.DataItem as ClassifiedModel.ClassifiedImage;


            HtmlAnchor image_counter_link = e.Item.FindControl("image_counter_link") as HtmlAnchor;
            image_counter_link.HRef = im.FileName;

            Label image_counter = e.Item.FindControl("image_counter") as Label;
            image_counter.Text = (e.Item.ItemIndex + 1).ToString();


        }
    }


    void resizeImage(Image img, int index)
    {
        FileStream fs = new FileStream(Server.MapPath(img.ImageUrl),
                                FileMode.Open, FileAccess.Read, FileShare.Read);

        using (System.Drawing.Image image = System.Drawing.Image.FromStream(fs))
        {
            Sd.Size s = getResizedImageSize(image.Width, image.Height, 480, 360);

            img.Width = s.Width;
            img.Height = s.Height;

            img.Attributes["style"] = img.Attributes["style"] + "margin-top:" + ((360 - s.Height) / 2) + "px;";
            if (index != 0)
                img.Attributes["style"] = img.Attributes["style"] + "display:none;";

        }

        fs.Close();
    }

    Sd.Size getResizedImageSize(int w, int h, int frameWidth, int frameHeight)
    {
        Sd.Size s = new Sd.Size();

        if (frameHeight < h * frameWidth / w) // the image is more like portrait than the frame
        {                                     // thus, resize the image according to the height
            s.Height = frameHeight;
            s.Width = w * frameHeight / h;
        }
        else
        {
            s.Width = frameWidth;
            s.Height = h * frameWidth / w;
        }

        return s;
    }

}