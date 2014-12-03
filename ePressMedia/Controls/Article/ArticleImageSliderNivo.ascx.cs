using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EPM.ImageUtil;

namespace ePressMedia.Controls.Article
{
    [Description("Article Photo Slider(Nivo) - The first image will determine the size of control")]
    public partial class ArticleImageSliderNivo : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                var CatIds = CategoryIDs.Split(',').ToList();
                var articles = (from c in context.Articles
                                where CatIds.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.Thumb_Path.Length > 0
                                orderby c.PostDate descending
                                select new
                                {
                                    ArticleId = c.ArticleId,
                                    CategoryId = c.CategoryId,
                                    Title = c.Title,
                                    SubTitle = c.SubTitle,
                                    ImageURL = EPM.Business.Model.Article.ArticleThumbnailController.GetArticleThumbSourceImagePath(c.ArticleId),
                                    ArticleURL = "../Article/view.aspx?p=" + c.CategoryId.ToString() + "&aid=" + c.ArticleId.ToString()
                                }).Take(NumberOfImage);


                if (articles.Count() == 0)
                    wrapper.Visible = false;
                else
                {
                    ArticleImageSliderNivo_Repeater.DataSource = articles;
                    ArticleImageSliderNivo_Repeater.DataBind();
                }

                //slider.Attributes.Add("style","height: "+ ControlHeight.ToString()+"px");
                hf_interval.Value = ImageInterval.ToString();
            }
        }



        public enum TransitionTypes
        {
            sliceDown,
            sliceDownLeft,
            sliceUp,
            sliceUpLeft,
            sliceUpDown,
            sliceUpDownLeft,
            fold,
            fade,
            random,
            slideInRight,
            slideInLeft,
            boxRandom,
            boxRain,
            boxRainReverse,
            boxRainGrow,
            boxRainGrowReverse,

        }
        private int numderOfImage;
        [Category("EPMProperty"), Description("Number of images to show"), DefaultValue(typeof(System.Int32), "2"), Required(ErrorMessage = "Number of Images is required.")]
        public int NumberOfImage
        {
            get { return numderOfImage; }
            set { numderOfImage = value; }
        }

        private string categoryIDs;
        [Category("EPMProperty"), Description("Articl Category IDs (use comma as an identifier)"), Required(ErrorMessage = "Article Category ID is required")]
        public string CategoryIDs
        {
            get { return categoryIDs; }
            set { categoryIDs = value; }
        }

        private TransitionTypes transitionType = TransitionTypes.fade;
        [Category("EPMProperty"), Description("Transition Type to show the image silder"), DefaultValue(typeof(TransitionTypes), "fade")]
        public TransitionTypes TransitionType
        {
            get { return transitionType; }
            set { transitionType = value; }
        }

        private int controlheight = 240;
        [Category("EPMProperty"), Description("Control Height)"), DefaultValue(typeof(System.Int32), "240")]
        public int ControlHeight
        {
            get { return controlheight; }
            set { controlheight = value; }
        }

        private int imageInterval = 4500;
        [Category("EPMProperty"), Description("Image transition interval"), DefaultValue(typeof(System.Int32), "4500")]
        public int ImageInterval
        {
            get { return imageInterval; }
            set { imageInterval = value; }
        }

        public string ControlHeightPx
        {
            get { return "height:"+ControlHeight.ToString() + "px"; }
        }

        private bool navigateToArticle = false;
        [Category("EPMProperty"), Description("Determine if the image is clickable and navigates to the article content."), DefaultValue(typeof(System.Boolean), "false")]
        public bool NavigateToArticle
        {
            get { return navigateToArticle; }
            set { navigateToArticle = value; }
        }

        protected void ArticleImageSliderNivo_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (!NavigateToArticle)
                {
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("Article_Navigator")).HRef = "";
                }
            }
        }


    }
}