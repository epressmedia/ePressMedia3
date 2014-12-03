using Brettle.Web.NeatUpload;
using System;
using System.Collections.Generic;
using System.Configuration;
using Sd = System.Drawing;
using Sd2d = System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Core;
using EPM.Legacy.Common;


public partial class Cp_Site_FileManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string homeDir = EPM.Business.Model.Admin.SiteSettingController.GetSiteSettingValueByName("Default Upload Root");
                CurUrl.Value = homeDir;
                HomeUrl.Value = homeDir;

                bindData(CurUrl.Value.ToString());
            }
            catch
            {
                Response.Redirect("~/CP");
            }
        }
    }

    void bindData(string path)
    {
        

        int idx = path.IndexOf('/', 2); // path starts with ~/
        if (idx < 0)
            CurFolder.Text = "Home";
        else
            CurFolder.Text = "Home" + path.Substring(idx);

        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(path));

        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        DirList.DataSource = dirs;
        DirList.DataBind();


        FileInfo[] files = dirInfo.GetFiles();
        FileList.DataSource = files;
        FileList.DataBind();
    }

    protected void FileList_ItemCommand(object source, DataListCommandEventArgs e) //RepeaterCommandEventArgs e)
    {
        LinkButton l = e.CommandSource as LinkButton;

        if (1 == int.Parse(e.CommandArgument.ToString()))
        {
            CurUrl.Value = CurUrl.Value.ToString() + "/" + l.Text;
            showPreview(false);
            bindData(CurUrl.Value.ToString());
        }
        else
        {
            ImageURL.Text = CurUrl.Value.ToString() + "/" + l.Text;
            //CurUrl.Value.ToString().Substring(1) + "/" + l.Text;

            if (l.Text.EndsWith("pdf") == false && l.Text.EndsWith("PDF") == false)
            {
                FileStream fs = new FileStream(Server.MapPath(CurUrl.Value.ToString() + "/" + l.Text),
                                        FileMode.Open, FileAccess.Read, FileShare.Read);

                using (System.Drawing.Image image = System.Drawing.Image.FromStream(fs))
                {
                    Sd.Size s = getResizedImageSize(image.Width, image.Height, 200);

                    PreviewImage.ImageUrl = CurUrl.Value.ToString() + "/" + l.Text;
                    PreviewImage.Width = new Unit(s.Width, UnitType.Pixel);
                    PreviewImage.Height = new Unit(s.Height, UnitType.Pixel);

                    SelectedFileName.Text = l.Text;
                    SelectedSize.Text = string.Format("({0} X {1})", image.Width, image.Height);

                    showPreview(true);
                }

                fs.Close();
            }
        }

    }
    protected void FileList_ItemDataBound(object sender, DataListItemEventArgs e) // RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image img = e.Item.FindControl("FileIcon") as Image;
            LinkButton l = e.Item.FindControl("FileLink") as LinkButton;
            string ext = Path.GetExtension(l.Text).ToLower();
            if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".gif"))
                img.ImageUrl = "../Img/imgfile.png";
            else if (ext.Equals(".pdf"))
                img.ImageUrl = "../Img/pdf.png";
        }
    }

    protected void ToParent_Click(object sender, EventArgs e)
    {
        string curUrl = CurUrl.Value.ToString();
        if (curUrl.Equals(HomeUrl.Value.ToString()))
            return;

        int i = curUrl.LastIndexOf('/');
        if (i < 0)
            return;

        CurUrl.Value = curUrl.Substring(0, i);

        showPreview(false);
        bindData(CurUrl.Value.ToString());
    }

    protected void Refresh_Click(object sender, EventArgs e)
    {
        bindData(CurUrl.Value.ToString());
    }

    protected void NewFolderButton_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath(CurUrl.Value.ToString()) + "\\" + FolderName.Text;

            Directory.CreateDirectory(path);

            bindData(CurUrl.Value.ToString());

        }
        catch
        {
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        foreach (Brettle.Web.NeatUpload.UploadedFile file in MultiFile1.Files)
        {
            //ListBox1.Items.Add(file.FileName + "," + file.ContentLength);

            if (file.FileName.EndsWith(".pdf") || file.FileName.EndsWith(".PDF"))
                file.MoveTo(Utility.GetSafeFileName(Server.MapPath(CurUrl.Value.ToString()) + "\\" + file.FileName),
                    MoveToOptions.Overwrite);
            else
                resizeAndSave(file);
        }

        bindData(CurUrl.Value.ToString());
    }

    const int maxWidth = 1200;
    void resizeAndSave(UploadedFile file)
    {
        string saveName = Server.MapPath(CurUrl.Value.ToString()) + "\\" + file.FileName;

        FileStream fs = file.TmpFile.Open(FileMode.Open, FileAccess.Read);
        using (Sd.Image srcImage = Sd.Image.FromStream(fs))
        {
            fs.Close();

            if (srcImage.Width > maxWidth)
            {
                int height = maxWidth * srcImage.Height / srcImage.Width;
                //Sd.Size s = getResizedImageSize(srcImage.Width, srcImage.Height, maxLength);

                using (Sd.Image resImage = new Sd.Bitmap(maxWidth, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                {
                    using (Sd.Graphics g = Sd.Graphics.FromImage(resImage))
                    {
                        g.CompositingQuality = Sd2d.CompositingQuality.HighQuality;
                        g.SmoothingMode = Sd2d.SmoothingMode.HighQuality;
                        g.InterpolationMode = Sd2d.InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = Sd2d.PixelOffsetMode.HighQuality;

                        g.DrawImage(srcImage, new Sd.Rectangle(0, 0, maxWidth, height));

                        resImage.Save(Utility.GetSafeFileName(saveName));
                    }
                }
            }
            else
            {
                file.SaveAs(Utility.GetSafeFileName(saveName));
            }
        }
    }
    /*
    void resizeAndSave(UploadedFile file)
    {
        string saveName = Server.MapPath(CurUrl.Value.ToString()) + "\\" + file.FileName;

        FileStream fs = file.TmpFile.Open(FileMode.Open, FileAccess.Read);
        using (Sd.Image srcImage = Sd.Image.FromStream(fs))
        {
            fs.Close();

            if (srcImage.Width > maxLength || srcImage.Height > maxLength)
            {
                Sd.Size s = getResizedImageSize(srcImage.Width, srcImage.Height, maxLength);

                using (Sd.Image resImage = new Sd.Bitmap(s.Width, s.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                {
                    using (Sd.Graphics g = Sd.Graphics.FromImage(resImage))
                    {
                        g.CompositingQuality = Sd2d.CompositingQuality.HighQuality;
                        g.SmoothingMode = Sd2d.SmoothingMode.HighQuality;
                        g.InterpolationMode = Sd2d.InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = Sd2d.PixelOffsetMode.HighQuality;

                        g.DrawImage(srcImage, new Sd.Rectangle(0, 0, s.Width, s.Height));

                        resImage.Save(Utility.GetSafeFileName(saveName));
                    }
                }
            }
            else
            {
                file.SaveAs(Utility.GetSafeFileName(saveName));
            }
        }
    }
    */


    Sd.Size getResizedImageSize(int w, int h, int max)
    {
        Sd.Size s = new Sd.Size();

        if (w >= h)
        {
            s.Width = max;
            s.Height = max * h / w;
        }
        else
        {
            s.Height = max;
            s.Width = max * w / h;
        }

        return s;
    }

    void showPreview(bool show)
    {
        PreviewImage.Visible = SelectedFileName.Visible =
        ImageURL.Visible = SelectedSize.Visible = show;
    }

}