using Brettle.Web.NeatUpload;
using System.Drawing;
using System.Drawing.Drawing2D;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Net;


/// <summary>
/// Summary description for ImageUtility
/// </summary>
public static class ImageUtilityLegacy
{
    /// <summary>
    /// 지정된 크기 안에 원본 이미지를 자르지 않고 줄여 넣어 thumbnail 을 만든다.
    /// 지정된 비율보다 원본 이미지가 가로로 넓거나 세로로 길쭉한 경우는 이미지의 상하 또는 좌우에 여백을 넣어준다.
    /// </summary>
    /// <param name="srcFilePath">원본 이미지 파일 경로</param>
    /// <param name="thumbDirectory">썸네일 이미지를 저장할 디렉토리</param>
    /// <param name="thumbWidth">썸네일 가로 크기. 픽셀 단위</param>
    /// <param name="thumbHeight">썸네일 세로 크기. 픽셀 단위</param>
    /// <returns>저장된 썸네일 파일명</returns>
    public static string SqueezeThumbnail(string srcFilePath, string thumbDirectory, int thumbWidth, int thumbHeight)
    {
        string thumbFileName = Path.GetFileNameWithoutExtension(srcFilePath) + "_t.jpg";

        using (Image srcImg = Image.FromFile(srcFilePath))
        {
            Rectangle dstRect = getDestRect(srcImg.Width, srcImg.Height, thumbWidth, thumbHeight);

            using (Image thumbImg = new Bitmap(thumbWidth, thumbHeight, 
                                        System.Drawing.Imaging.PixelFormat.Format24bppRgb))
            {
                using (Graphics g = Graphics.FromImage(thumbImg))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.FillRectangle(Brushes.White, 0, 0, thumbWidth, thumbHeight);
                    g.DrawImage(srcImg, dstRect,
                        new Rectangle(0, 0, srcImg.Width, srcImg.Height), GraphicsUnit.Pixel);

                    thumbImg.Save(thumbDirectory + "\\" + thumbFileName);

                    return thumbFileName;
                    
                }
            }
        }
    }

    public static string GetThumbnailFileName(string fileName)
    {
        return (Path.GetFileNameWithoutExtension(fileName) + "_t.jpg");
    }

    static Rectangle getDestRect(int w, int h, int frameWidth, int frameHeight)
    {
        int nw, nh;

        if (frameHeight < h * frameWidth / w) // the image is more like portrait than the frame
        {                                     // thus, resize the image according to the height
            nh = frameHeight;
            nw = w * frameHeight / h;
        }
        else
        {
            nw = frameWidth;
            nh = h * frameWidth / w;
        }

        return new Rectangle((frameWidth - nw) / 2, (frameHeight - nh) / 2, nw, nh);
    }

    /// <summary>
    /// 업로드된 파일을 지정된 크기에 맞게 사이즈를 변환하여 저장한다.
    /// </summary>
    /// <param name="file">Upload 된 파일. Brettile.Web.NeatUpload.UploadFile 클래스의 인스턴스</param>
    /// <param name="saveFilePath">저장될 파일의 Full Path</param>
    /// <param name="maxLength">가로 또는 세로의 최대 크기.</param>
    /// <returns>저장된 파일명. 파라미터로 지정된 파일명이 이미 존재하는 경우 다른 이름으로 변경된다.</returns>
    public static string ResizeAndSaveImage(Brettle.Web.NeatUpload.UploadedFile file, string saveFilePath, int maxLength)
    {
        string safePath = EPM.Legacy.Common.Utility.GetSafeFileName(saveFilePath);

        resizeAndSaveImage(file, safePath, maxLength);

        return safePath;
    }

    public static string ResizeAndSaveImageByWidth(Brettle.Web.NeatUpload.UploadedFile file, string directory,
                                                string saveFileName, int maxWidth)
    {
        string safeName = EPM.Legacy.Common.Utility.GetSafeFileName(directory, saveFileName);

        resizeAndSaveImageByWidth(file, directory + "\\" + safeName, maxWidth);

        return safeName;
    }

    public static string ResizeAndSaveImage(Brettle.Web.NeatUpload.UploadedFile file, string directory,
                                            string saveFileName, int maxLength)
    {
        string safeName = EPM.Legacy.Common.Utility.GetSafeFileName(directory, saveFileName);

        resizeAndSaveImage(file, directory + "\\" + safeName, maxLength);

        return safeName;
    }




    static void resizeAndSaveImage(Brettle.Web.NeatUpload.UploadedFile file, string savePath, int maxLength)
    {
        FileStream fs = file.TmpFile.Open(FileMode.Open, FileAccess.Read);

        using (Image srcImage = Image.FromStream(fs))
        {
            fs.Close();

            if (srcImage.Width > maxLength || srcImage.Height > maxLength)
            {
                Size s = GetResizedImageSize(srcImage.Width, srcImage.Height, maxLength);

                using (Image resImage = new Bitmap(s.Width, s.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(resImage))
                    {
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        g.DrawImage(srcImage, new Rectangle(0, 0, s.Width, s.Height));

                        resImage.Save(savePath);
                    }
                }
            }
            else
            {
                file.SaveAs(savePath);
            }
        }

    }

    static void resizeAndSaveImageByWidth(Brettle.Web.NeatUpload.UploadedFile file, string savePath, int maxWidth)
    {
        FileStream fs = file.TmpFile.Open(FileMode.Open, FileAccess.Read);

        using (Image srcImage = Image.FromStream(fs))
        {
            fs.Close();

            if (srcImage.Width > maxWidth)
            {
                int height = maxWidth * srcImage.Height / srcImage.Width;

                using (Image resImage = new Bitmap(maxWidth, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(resImage))
                    {
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        g.DrawImage(srcImage, new Rectangle(0, 0, maxWidth, height));

                        resImage.Save(savePath);
                    }
                }
            }
            else
            {
                file.SaveAs(savePath);
            }
        }

    }

    /// <summary>
    /// 최대 크기를 넘지 않는 리사이즈된 이미지의 크기를 구한다.
    /// 리사이즈된 크기에서도 원본 비율은 유지된다.
    /// 예를 들어 이미지가 640*480 이고 최대 크기가 320이라면
    /// 리사이즈된 크기는 320*240 이 된다.
    /// </summary>
    /// <param name="w">원래 가로 크기</param>
    /// <param name="h">원래 세로 크기</param>
    /// <param name="max">가로 또는 세로 최대 크기</param>
    /// <returns></returns>
    public static Size GetResizedImageSize(int w, int h, int max)
    {
        Size s = new Size();

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

    /// <summary>
    /// Download and Save image from URL
    /// </summary>
    /// <param name="file">the file name leading the map path where the image is supposed to be stored.</param>
    /// <param name="url">URL where the image is downloaded from</param>
    /// <returns></returns>
    public static void save_file_from_url(string file_name, string url)
    {
        byte[] content;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        WebResponse response = request.GetResponse();

        Stream stream = response.GetResponseStream();

        using (BinaryReader br = new BinaryReader(stream))
        {
            content = br.ReadBytes(500000);
            br.Close();
        }
        response.Close();

        FileStream fs = new FileStream(file_name, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        try
        {
            bw.Write(content);
        }
        finally
        {
            fs.Close();
            bw.Close();
        }
    }

}