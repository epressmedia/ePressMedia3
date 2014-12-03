using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EPM.Core;
using System.Xml;
using System.Xml.Linq;

namespace ePressMedia.Admin
{
    public partial class PageConfig : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                UseForType = (EPMBasePage.UseForTypes)Enum.Parse(typeof(EPMBasePage.UseForTypes), Page.Session["PageInfo-UseForType"].ToString());
                ContentType = (EPMBasePage.ContentTypes)Enum.Parse(typeof(EPMBasePage.ContentTypes), Page.Session["PageInfo-ContentType"].ToString());
                CategoryID = int.Parse(HttpContext.Current.Session["PageInfo-CategoryID"].ToString());

                LoadBasicInfo();
                LoadMasterPageFromXml();
            }
        }

        void LoadBasicInfo()
        {

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            string name = "";

            if (ContentType == EPMBasePage.ContentTypes.Article)
            {
                ArticleModel.ArticleCategory article = (context.ArticleCategories.Single(a => a.ArtCatId == CategoryID));
                if (UseForType == EPMBasePage.UseForTypes.ListView)
                    ContentXML = article.metadataStr;
                else
                    ContentXML = article.DetailMetadataStr;
                name = article.CatName;

            }
            else if (ContentType == EPMBasePage.ContentTypes.Forum)
            {
                ForumModel.ForumConfig forum = (context.ForumConfigs.Single(a => a.ForumId == CategoryID));
                if (UseForType == EPMBasePage.UseForTypes.ListView)
                    ContentXML = forum.MetadataStr;
                else
                    ContentXML = forum.DetailMetadataStr;
                name = forum.Forum.ForumName;
            }
            else if (ContentType == EPMBasePage.ContentTypes.Page)
            {
                SiteModel.CustomPage customPage = (context.CustomPages.Single(a => a.CustomPageId == CategoryID));

                ContentXML = customPage.MetadataStr;
                name = customPage.Name;

            }
            else if (ContentType == EPMBasePage.ContentTypes.Classified)
            {
                ClassifiedModel.ClassifiedCategory classified = (context.ClassifiedCategories.Single(a => a.CatId == CategoryID));
                if (UseForType == EPMBasePage.UseForTypes.ListView)
                    ContentXML = classified.MetadataStr;
                else
                    ContentXML = classified.DetailMetadataStr;
                name = classified.CatName;
            }
            else if (ContentType == EPMBasePage.ContentTypes.Biz)
            {
                BizModel.BusinessEntityConfig Biz = (context.BusinessEntityConfigs.Single(a => 1 == 1));
                if (UseForType == EPMBasePage.UseForTypes.ListView)
                    ContentXML = Biz.MetadataStr;
                else
                    ContentXML = Biz.DetailMetadataStr;

                name = "Business Directory";

            }


            lbl_Name.Text = name;

            lbl_Area.Text = ContentType.ToString();


        }

        private void LoadMasterPageFromXml()
        {
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            if (!String.IsNullOrEmpty(ContentXML))
            {
                XmlDocument xmlMainDoc = new XmlDocument();
                xmlMainDoc.LoadXml(ContentXML);
                XmlNode targetNode = (xmlMainDoc.SelectSingleNode("/PageRoot/Configs/MasterPage"));
                string name_prefix = targetNode.Attributes["Value"].Value.ToString().Replace("~/Master/", "").Replace(".master", "") + " - ";
                string name = "";
                // if Name attribute doesn't exists, then pick the first one in the database
                if (targetNode.Attributes["Name"] == null)
                {
                    string master_page_name = targetNode.Attributes["Value"].Value.ToString().Replace("~/Master/", "");
                    if (context.MasterPageConfigs.Any(c => c.Name == master_page_name && c.DeletedFg == false))
                        name = (context.MasterPageConfigs.Where(c => c.Name == master_page_name && c.DeletedFg == false).OrderBy(c => c.MasterPageId).ToList())[0].Description;
                }
                else
                    name = targetNode.Attributes["Name"].Value.ToString();

                MasterPageID = (context.MasterPageConfigs.Where(c => c.MasterPagePath == targetNode.Attributes["Value"].Value.ToString() && c.Description == name).ToList())[0].MasterPageId;
                lbl_Master.Text = name_prefix + name;
                lbl_path.Text = targetNode.Attributes["Value"].Value.ToString();
                PlaceholderLoad();
            }
        }

        void PlaceholderLoad()
        {


            cbo_placeholders.DataSource = EPM.Core.Admin.PageBuilder.GetPlaceholdersByMasterPagePath(lbl_path.Text);
                cbo_placeholders.DataBind();

                cbo_placeholders.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("--- Select ---", "0"));
            
        }

        

        void PlaceholderChage()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(ContentXML);

        XmlNodeList nodelist = xmlDoc.SelectNodes("/PageRoot/Contents/Content[@ID='" + cbo_placeholders.SelectedItem.Text.ToString() + "']/Control");
            // Get the current placholder name selected
        CurrentPlaceholder = cbo_placeholders.SelectedItem.Text.ToString();

            // Get the max control id
            // get the next sequence
            List<int> max_control_id_list = new List<int>();
            List<int> next_sequence_list = new List<int>();
            
        foreach (XmlNode node in nodelist)
        {
            string cont_id = node.Attributes["ID"].Value.ToString();
            string seq = node.Attributes["Seq"].Value.ToString();
            max_control_id_list.Add(int.Parse(cont_id.Substring(cont_id.LastIndexOf("_") + 1, cont_id.Length - cont_id.LastIndexOf("_") - 1)));
            next_sequence_list.Add(int.Parse(seq));
        }

        if (max_control_id_list.Count == 0)
            MaxControlID =  0;
        else
            MaxControlID = max_control_id_list.Max();
        if (next_sequence_list.Count == 0)
            NextSequence = 1;
        else
            NextSequence = next_sequence_list.Max() + 1;

            

        DataTable dt = XmlHelper.ConvertXmlNodeListToDataTable(nodelist);

        DataView vw = dt.DefaultView;
        vw.Sort = "Seq Asc";


        gv_controls.DataSource = dt;
        gv_controls.DataBind();
    }

        private static EPMBasePage.ContentTypes contentType;
        public static EPMBasePage.ContentTypes ContentType
        {
            get;
            set;
        }

        private static EPMBasePage.UseForTypes useForType;
        public static EPMBasePage.UseForTypes UseForType
        {
            get;
            set;
        }
        private static int categoryID;
        public static int CategoryID
        {
            get;
            set;
        }

        private static string contentXML;
        public static string ContentXML
        {
            get { return contentXML; }
            set {
                HttpContext.Current.Session["contentXML"] = value;
                contentXML = value; }
        }
        private static int masterPageID;
        public static int MasterPageID
        {
            get;
            set;
        }


        private static string currentPlaceholder;
        public static string CurrentPlaceholder
        {
            get;
            set;
        }
        private static int nextSequence;
        public static int NextSequence
        {
            get;
            set;
        }
        private static int maxControlID;
        public static int MaxControlID
        {
            get;
            set;
        }


        protected void cbo_placeholders_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            btn_Apply_Control.Visible = false;
            btn_Add_Control.Visible = false;
            PlaceholderChage();
            if (cbo_placeholders.SelectedIndex > 0)
                btn_Add_Control.Visible = true;

        }

        protected void btn_Add_Control_Click(object sender, EventArgs e)
        {
            EPM.Core.CP.PopupContoller.OpenWindow("/Admin/AddNewControl.aspx?type=SelectControlType", PageConfig_Container, null);
        }

        protected void gv_controls_RowDrop(object sender, Telerik.Web.UI.GridDragDropEventArgs e)
        {
           ContentXML = EPM.Core.Admin.PageBuilder.OrderControl(ContentXML, cbo_placeholders.SelectedItem.Value.ToString(), e.DraggedItems[0].ItemIndex, e.DestDataItem.ItemIndex);
           btn_Apply_Control.Visible = true;
           PlaceholderChage();



        }

        protected void btn_Apply_Control_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ContentXML);
            EPM.Core.CP.ContentBuilderContoller.UpdateContentXML(CategoryID, xmlDoc, ContentType, UseForType);
            Response.Redirect(Request.RawUrl);
            
        }

    }
}