using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ePressMedia.Controls.Article
{
    public partial class TagCloud : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                lbl_header.Text = HeaderName;

                EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
                List<EPM.Data.Model.GetTagCloudResultSet0> result = context.GetTagCloud(DayPeriod).ToList();

                RadTagCloud1.Distribution = Distribution;
                RadTagCloud1.Sorting = Sorting;
                if (!string.IsNullOrEmpty(DataNavigateUrlFormatString))
                {
                    RadTagCloud1.DataNavigateUrlFormatString = DataNavigateUrlFormatString;
                    RadTagCloud1.DataNavigateUrlField = "Tags";
                }
                RadTagCloud1.MinFontSize = Unit.Pixel(MinFontSize);
                RadTagCloud1.MaxFontSize = Unit.Pixel(MaxFontSize);
                RadTagCloud1.MaxNumberOfItems = MaxNumberOfItems;

                RadTagCloud1.DataSource = result;
                RadTagCloud1.DataBind();

                
            }
            catch
            {
            }
        }


        private string headerName;
        [Category("EPMProperty"), Description("Specifies the name of content to be displayed in the control header")]
        public string HeaderName
        {
            get { return headerName; }
            set { headerName = value; }
        }

        private int dayPeriod = 7;
        [Category("EPMProperty"), Description("Number of days to look up to calculate tag cloud weight"), DefaultValue(typeof(Int32),"7"), Required(ErrorMessage = "Day Period is required.")]
        public int DayPeriod
        {
            get { return dayPeriod; }
            set { dayPeriod = value; }
        }


        private TagCloudDistribution distribution = TagCloudDistribution.Linear;
        [Category("EPMProperty"), Description("Specifies how the font size will be distributed among the items. When set to Linear the font size is distributed linearly and in the case of Logarithmic the items are weighted logarithmically."), DefaultValue(typeof(TagCloudDistribution), "Linear")]
        public TagCloudDistribution Distribution
        {
            get { return distribution; }
            set { distribution = value; }
        }



        private TagCloudSorting sorting = TagCloudSorting.NotSorted;
        [Category("EPMProperty"), Description("Specifies in what order the TagCloud items will be listed"), DefaultValue(typeof(TagCloudSorting), "NotSorted")]
        public TagCloudSorting Sorting
        {
            get { return sorting; }
            set { sorting = value; }
        }

        private int maxNumberOfItems = 40;
        [Category("EPMProperty"), Description("Number of days to look up to calculate tag cloud weight"), DefaultValue(typeof(Int32), "40"), Required(ErrorMessage = "MaxNumberOfItems is required.")]
        public int MaxNumberOfItems
        {
            get { return maxNumberOfItems; }
            set { maxNumberOfItems = value; }
        }


        private int minFontSize = 10;
        [Category("EPMProperty"), Description("Minimal number of items that can (will) be shown in the cloud."), DefaultValue(typeof(Int32), "10"), Required(ErrorMessage = "MinFontSize is required.")]
        public int MinFontSize
        {
            get { return minFontSize; }
            set { minFontSize = value; }
        }


        private int maxFontSize = 20;
        [Category("EPMProperty"), Description("Maximal number of items that can (will) be shown in the cloud"), DefaultValue(typeof(Int32), "20"), Required(ErrorMessage = "MaxFontSize is required.")]
        public int MaxFontSize
        {
            get { return maxFontSize; }
            set { maxFontSize = value; }
        }



        private string dataNavigateUrlFormatString = "";
        [Category("EPMProperty"), Description("Specifies the URL to be redirected when the tag is clicked.(ex.\"~/Search/Search.aspx?q={0}\"")]
        public string DataNavigateUrlFormatString
        {
            get { return dataNavigateUrlFormatString; }
            set { dataNavigateUrlFormatString = value; }
        }
        


    }
    
}