using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


[Description("Article YouTube Video Viewer")]
public partial class Controls_Article_ArticleYouTubeView : System.Web.UI.UserControl
    {

        private string categoryIDs;
        [Category("KNNProperty"), Description("Articl Category IDs (use comma as an identifier)"), Required(ErrorMessage = "Article Category ID is required")]
        public string CategoryIDs
        {
            get { return categoryIDs; }
            set { categoryIDs = value; }
        }

        private int controlheight = 375;
        [Category("KNNProperty"), Description("Control Height)"), DefaultValue(typeof(System.Int32), "375")]
        public int ControlHeight
        {
            get { return controlheight; }
            set { controlheight = value; }
        }

        private int controlwidth = 560;
        [Category("KNNProperty"), Description("Control Width)"), DefaultValue(typeof(System.Int32), "560")]
        public int ControlWidth
        {
            get { return controlwidth; }
            set { controlwidth = value; }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {


          EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        if (!IsPostBack)
        {
            var CatIds = CategoryIDs.Split(',').ToList();
            var articles = (from c in context.Articles
                            where CatIds.Contains(c.CategoryId.ToString()) && c.Suspended == false && c.VideoId.Length > 0 && c.IssueDate <= DateTime.Now
                            
                            orderby c.IssueDate descending
                           select new
                           {
                               YouTubeLink = "http://www.youtube.com/watch?v="+c.VideoId,
                               Subject = "<div>"+c.Title+"</div>"
                           }).Take(3);


            if (articles.Count() != 3)
                ArticleYouTubeView_Container.Visible = false;
            else
            {
                image_repeater.DataSource = articles;
                image_repeater.DataBind();
            }

            

        }

            
        }
    }
