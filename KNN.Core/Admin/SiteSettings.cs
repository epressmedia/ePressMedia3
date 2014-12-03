using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EPM.Business.Model.Admin;

namespace EPM.Core.Admin
{
    public class SiteSettings
    {
        static string clsUpRoot;
        public static string ClassifiedUploadRoot
        {
            get { return clsUpRoot; }
        }

        static string clsThumbPath;
        public static string ClassifiedThumbnailPath
        {
            get { return clsThumbPath; }
        }

        static string frmUpRoot;
        public static string ForumUploadRoot
        {
            get { return frmUpRoot; }
        }

        static string frmThumbPath;
        public static string ForumThumbnailPath
        {
            get { return frmThumbPath; }
        }

        static string artUpRoot;
        public static string ArticleUploadRoot
        {
            get { return artUpRoot; }
        }

        static string artThumbPath;
        public static string ArticleThumbnailPath
        {
            get { return artThumbPath; }
        }



        static string bizUpRoot;
        public static string BizUploadRoot
        {
            get { return bizUpRoot; }
        }

        static string bizThumbPath;
        public static string BizThumbnailPath
        {
            get { return bizThumbPath; }
        }

        static string adsUpRoot;
        public static string AdsUploadRoot
        {
            get { return adsUpRoot; }
        }


        static string uploadRoot;
        public static string UploadRoot
        {
            get { return uploadRoot; }
        }

        static Size clsThumbSize = new Size(72, 54);
        public static Size ClassifiedThumbnailSize
        {
            get { return clsThumbSize; }
        }

        static SiteSettings()
        {
            uploadRoot = SiteSettingController.GetSiteSettingValueByName("Default Upload Root");

            clsUpRoot = SiteSettingController.GetSiteSettingValueByName("Classified Upload Root");
            clsThumbPath = clsUpRoot + "/Thumb/";

            frmUpRoot = SiteSettingController.GetSiteSettingValueByName("Forum Upload Root");
            frmThumbPath = frmUpRoot + "/Thumb/";

            artUpRoot = SiteSettingController.GetSiteSettingValueByName("Article Upload Root");
            artThumbPath = artUpRoot + "/Thumb/";

            bizUpRoot = SiteSettingController.GetSiteSettingValueByName("Biz Upload Root");
            bizThumbPath = bizUpRoot + "/Thumb/";

            adsUpRoot = SiteSettingController.GetSiteSettingValueByName("Ad Upload Root");


        }


    }
}
