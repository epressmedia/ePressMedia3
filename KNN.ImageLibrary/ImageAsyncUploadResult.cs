using System;
using Telerik.Web.UI;

namespace EPM.ImageLibrary
{
    // The result object is returned from the handler to the page.
    // You can include custom fields in the result by extending the AsyncUploadResult class.
    public class ImageAsyncUploadResult : AsyncUploadResult
    {

        private string tempFileLocation;

        public string TempFileLocation
        {
            get { return tempFileLocation.Replace("\\", "\\\\"); }
            set { tempFileLocation = value; }
        }

        private string newFileName;
        public string NewFileName
        {
            get { return newFileName; }
            set { newFileName = value; }
        }

    }
}