<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.IO;
using EPM.Core;
using EPM.ImageLibrary;

public class ImageHandler : AsyncUploadHandler, System.Web.SessionState.IRequiresSessionState
{
   
    protected override IAsyncUploadResult Process(UploadedFile file, HttpContext context, IAsyncUploadConfiguration configuration, string tempFileName)
    {
        ImageAsyncUploadResult result = CreateDefaultUploadResult<ImageAsyncUploadResult>(file);

        result.TempFileLocation = FullPath;
        result.NewFileName = FileHelper.GetSafeFileName(configuration.TargetFolder, file.FileName);
        
        
        SaveToTempFolder(file, configuration, context, tempFileName);
        return result;
    }

}


