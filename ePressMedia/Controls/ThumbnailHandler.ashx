<%@ WebHandler Language="C#" Class="ThumbnailHandler" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


//<asp:Image ID="img1" runat="server" ImageUrl="~/Handler.ashx?h=50&w=50&file=Winter.jpg" />



public class ThumbnailHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        
        // for new height of image   
        int h = int.Parse(context.Request.QueryString["h"].ToString());
        // for new width of image         
        int w = int.Parse(context.Request.QueryString["w"].ToString());
        // for  image file name         
        string file = context.Request.QueryString["file"].ToString();
        // Path of image folder where images files are placed  
        string filePath = context.Server.MapPath("~/images/" + file);
        // Resize proccess    
        using (System.Drawing.Image img = System.Drawing.Image.FromFile(filePath))
        {
            Bitmap objBmp = new Bitmap(img, w, h); string extension = Path.GetExtension(filePath);
            MemoryStream ms; byte[] bmpBytes;
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    ms = new MemoryStream();
                    objBmp.Save(ms, ImageFormat.Jpeg); bmpBytes = ms.GetBuffer(); context.Response.ContentType = "image/jpeg"; context.Response.BinaryWrite(bmpBytes); objBmp.Dispose(); ms.Close(); context.Response.End(); break;
                case ".png": ms = new MemoryStream(); objBmp.Save(ms, ImageFormat.Png); bmpBytes = ms.GetBuffer(); context.Response.ContentType = "image/png"; context.Response.BinaryWrite(bmpBytes); objBmp.Dispose(); ms.Close(); context.Response.End(); break;
                case ".gif": ms = new MemoryStream(); objBmp.Save(ms, ImageFormat.Gif); bmpBytes = ms.GetBuffer(); context.Response.ContentType = "image/png"; context.Response.BinaryWrite(bmpBytes); objBmp.Dispose(); ms.Close(); context.Response.End(); break;
            } img.Dispose();
        }
    }
    public bool IsReusable { get { return false; } } 
}
