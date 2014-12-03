using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace EPM.Core.Common
{
     public static class BindHelper
    {


         public static void ImageBinder(RepeaterItemEventArgs e, String ImageControlID, int articleID, string ThumbnailTypeString)
         {
             ImageBinder(e, ImageControlID, articleID, ThumbnailTypeString, "");
         }

         public static void ImageBinder(RepeaterItemEventArgs e, String ImageControlID, int articleID, string ThumbnailTypeString, string AltText)
         {
             if (e.Item.FindControl(ImageControlID) != null)
             {
                 
                 Type t = (e.Item.FindControl(ImageControlID)).GetType();

                 if (t == typeof(HtmlImage))
                 {
                     //HtmlImage image = e.Item.FindControl(HTMLControlName) as System.Web.UI.HtmlControls.HtmlImage;

                     string thumb_path = EPM.Business.Model.Article.ArticleThumbnailController.GetArticleThumbnailPath(articleID, ThumbnailTypeString); //t.Thumb_Path;
                     if (!string.IsNullOrEmpty(thumb_path))
                     {
                         ((HtmlImage)e.Item.FindControl(ImageControlID)).Attributes.Add("src", (string.IsNullOrEmpty(thumb_path)) ? "" : thumb_path);
                         ((HtmlImage)e.Item.FindControl(ImageControlID)).Attributes.Add("alt", (string.IsNullOrEmpty(AltText)) ? "" : AltText);
                     }
                     else
                         ((HtmlImage)e.Item.FindControl(ImageControlID)).Visible = false;
                 }
                 else if (t == typeof(Image))
                 {
                     //System.Web.UI.WebControls.Image image = e.Item.FindControl(WebControlName) as System.Web.UI.WebControls.Image;


                     string thumb_path = EPM.Business.Model.Article.ArticleThumbnailController.GetArticleThumbnailPath(articleID, ThumbnailTypeString); //t.Thumb_Path;
                     if (!string.IsNullOrEmpty(thumb_path))
                     {
                         ((Image)e.Item.FindControl(ImageControlID)).ImageUrl = (string.IsNullOrEmpty(thumb_path)) ? "" : thumb_path;
                         ((Image)e.Item.FindControl(ImageControlID)).AlternateText = (string.IsNullOrEmpty(AltText)) ? "" : AltText;
                     }
                     else
                         ((Image)e.Item.FindControl(ImageControlID)).Visible = false;
                     

                 }
             }

         }
    }
}
