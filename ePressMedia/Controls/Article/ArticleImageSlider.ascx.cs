using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EPM.ImageUtil;

[Description("Article Photo Slider")]
public partial class Controls_Article_ArticleImageSlider : System.Web.UI.UserControl
{

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

    private int controlheight = 240;
    [Category("EPMProperty"), Description("Control Height)"),DefaultValue(typeof(System.Int32),"240")]
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


    

    protected void Page_Load(object sender, EventArgs e)
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        if (!IsPostBack)
        {
            //var CatIds = CategoryIDs.Split(',').ToList();

            IQueryable<ArticleModel.Article> Iarticles = EPM.Business.Model.Article.ArticleContoller.GetArticlesByCatIDs(CategoryIDs,true);

            //articles = articles.

            var articles = (from c in Iarticles
                            //where  c.Thumb_Path.Length > 0
                           orderby c.PostDate descending
                           select new
                           {
                               ArticleId = c.ArticleId,
                               CategoryId = c.CategoryId,
                               Title = c.Title,
                               SubTitle = c.SubTitle,
                               ImageURL = EPM.Business.Model.Article.ArticleThumbnailController.GetArticleThumbSourceImagePath(c.ArticleId),
                               ArticleURL = "../Article/view.aspx?p="+c.CategoryId.ToString()+"&aid="+c.ArticleId.ToString()
                           }).Take(NumberOfImage);


            if (articles == null) 
                ArticleImageSilder_Container.Visible = false;
            else
            {
                if (articles.ToList().Count() == 0)
                    ArticleImageSilder_Container.Visible = false;
                else
                {
                    image_repeater.DataSource = articles;
                    image_repeater.DataBind();
                }
            }

            gallery.Attributes.Add("style","height: "+ ControlHeight.ToString()+"px");
            hf_interval.Value = ImageInterval.ToString();

        }
    }
}