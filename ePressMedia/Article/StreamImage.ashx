<%@ WebHandler Language="C#" Class="StreamImage" %>

using System;
using System.Net;
using System.Web;
using System.IO;
using EPM.ImageLibrary;

public class StreamImage : IHttpHandler {

    private HttpContext _context;
    private HttpContext Context
    {
        get
        {
            return _context;
        }
        set
        {
            _context = value;
        }
    }
    
    public void ProcessRequest (HttpContext context) {

        Context = context;

        if (context.Request.QueryString["path"] == null)
        {
            return;
        }

        string filePath = Context.Server.MapPath((Context.Server.UrlDecode(Context.Request.QueryString["path"])));


        byte[] imageData;
        if (filePath.Contains("http"))
        {
            EPM.ImageLibrary.EPMImage image = new EPMImage(filePath);
            imageData = image.ByteArray;
            context.Response.ContentType = "image/jpeg";
            context.Response.OutputStream.Write(imageData, 0, imageData.Length);
            //context.Response.Flush();
        }
        else
        {
             imageData= GetImage(filePath);
             context.Response.ContentType = "image/jpeg";
             context.Response.BinaryWrite(imageData);
             //context.Response.Flush();

        }

    }
    private byte[] GetImage(string filePath)
    {
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);

        byte[] imageData = br.ReadBytes((int)br.BaseStream.Length);

        br = null;
        fs.Close();
        fs = null;

        return imageData;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}