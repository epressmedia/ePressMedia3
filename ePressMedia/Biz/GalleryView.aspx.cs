using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Biz;
using Sd = System.Drawing;



public partial class Biz_GalleryView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                bindImages(id);
            }
            catch
            { }
        }
    }

    void bindImages(int id)
    {

        List<BizModel.BusinessEntityImage> images = BEImageController.GetBEImagesByBEID(id).ToList();
        List<string> fileNames = new List<string>();
        foreach (BizModel.BusinessEntityImage image in images)
        {
            fileNames.Add(image.BusinessEntityThumbnails.Where(c=>c.ThumbnailType.OriginalFg == true).Single().ThumbnailPath);
        }
        if (images.Count() > 0)
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
            Image img = e.Item.FindControl("BizImg") as Image;
            resizeImage(img, e.Item.ItemIndex);
            
        }
    }


    void resizeImage(Image img, int index)
    {
        FileStream fs = new FileStream(Server.MapPath(img.ImageUrl),
                                FileMode.Open, FileAccess.Read, FileShare.Read);

        using (System.Drawing.Image image = System.Drawing.Image.FromStream(fs))
        {
            Sd.Size s = getResizedImageSize(image.Width, image.Height, 500, 320);

            img.Width = s.Width;
            img.Height = s.Height;

            img.Attributes["style"] = img.Attributes["style"] + "margin-top:" + ((320 - s.Height) / 2) + "px;";
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