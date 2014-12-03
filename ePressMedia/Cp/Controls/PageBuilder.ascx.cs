using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using EPM.Core;
using log4net;
using Telerik.Web.UI;


namespace ePressMedia.Cp.Controls
{

    public partial class PageBuilder : System.Web.UI.UserControl
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        private static readonly ILog log = LogManager.GetLogger(typeof(PageBuilder));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    LoadMasterPages();
                    LoadControlTypes();

                    if (Request.QueryString["cat"] != null)
                        BindXMLData(int.Parse(Request.QueryString["cat"]));
                    if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Biz)
                        BindXMLData(0);

                    if ((ContentType == EPM.Core.EPMBasePage.ContentTypes.Article) || (ContentType == EPM.Core.EPMBasePage.ContentTypes.Forum) || (ContentType == EPM.Core.EPMBasePage.ContentTypes.Page) || (ContentType == EPM.Core.EPMBasePage.ContentTypes.Classified))
                    {
                        ddl_template.Visible = true;
                        lbl_tempalte.Visible = true;
                        btn_template.Visible = true;
                        LoadTemplates();
                    }

                    if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
                    {
                        ddl_master.Enabled = false;
                        btn_page_preview.Visible = false;
                        MasterPageIndexChange();

                    }
                    toolbox1.EnableButtons("", true);


                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
        }

        void LoadTemplates()
        {

            var pTemplates = from c in context.PageTemplates
                             where c.Deleted_fg == false
                             orderby c.Name
                             select new
                             {
                                 Name = c.Name + " - " + c.Description,
                                 ID = c.TemplateId
                             };

            ddl_template.DataValueField = "ID";
            ddl_template.DataTextField = "Name";
            ddl_template.DataSource = pTemplates;
            ddl_template.DataBind();

            ddl_template.Items.Insert(0, new ListItem("-- Select -- ", "-1"));
        }

        void LoadControlTypes()
        {

            var controlTypes = from c in context.WidgetTypes
                               select c;

            ddl_control_types.DataValueField = "widget_type_id";
            ddl_control_types.DataTextField = "widget_type_name";
            ddl_control_types.DataSource = controlTypes;
            ddl_control_types.DataBind();

            ddl_control_types.Items.Insert(0, new ListItem("--- Select Control Type ---", "0"));
        }

        void BindXMLData(int id)
        {


            string headerName = "";

            if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Article)
            {
                ArticleModel.ArticleCategory cat = context.ArticleCategories.Single(c => c.ArtCatId == id);
                    headerName = cat.CatName;

                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    TextBox1.Text = Server.HtmlDecode(cat.metadataStr);
                else
                    TextBox1.Text = Server.HtmlDecode(cat.DetailMetadataStr);

            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Forum)
            {
                ForumModel.ForumConfig cat = context.ForumConfigs.Single(c => c.ForumId == id);
                headerName = cat.Forum.ForumName;
                
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    TextBox1.Text = Server.HtmlDecode(cat.MetadataStr);
                else
                    TextBox1.Text = Server.HtmlDecode(cat.DetailMetadataStr);
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Template)
            {
                SiteModel.PageTemplate cat = context.PageTemplates.Single(c => c.TemplateId == id);
                headerName = cat.Name;
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                {
                    TextBox1.Text = Server.HtmlDecode(cat.MetadataStr);

                }
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
            {
                SiteModel.MasterPageConfig cat = context.MasterPageConfigs.Single(c => c.MasterPageId == id);
                headerName = cat.Name;
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                {
                    TextBox1.Text = Server.HtmlDecode(cat.MetadataStr);

                }
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Page)
            {
                SiteModel.CustomPage cat = context.CustomPages.Single(c => c.CustomPageId == id);
                headerName = cat.Name;
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                {
                    TextBox1.Text = Server.HtmlDecode(cat.MetadataStr);

                }
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Classified)
            {
                ClassifiedModel.ClassifiedCategory cat = context.ClassifiedCategories.Single(c => c.CatId == id);

                headerName = cat.CatName;

                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    TextBox1.Text = Server.HtmlDecode(cat.MetadataStr);
                else
                    TextBox1.Text = Server.HtmlDecode(cat.DetailMetadataStr);
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Biz)
            {
                BizModel.BusinessEntityConfig config = context.BusinessEntityConfigs.Single(c => 1 == 1);
                headerName = "Business Directory";

                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    TextBox1.Text = Server.HtmlDecode(config.MetadataStr);
                else
                    TextBox1.Text = Server.HtmlDecode(config.DetailMetadataStr);
            }

            // Display heaer Name
            //Label CatName = ((Label)(this.Parent.FindControl("CatName")));
            if (((Label)(this.Parent.FindControl("CatName"))) != null)
                ((Label)(this.Parent.FindControl("CatName"))).Text = headerName + " (" + ContentType.ToString() + ")";

            LoadMasterPageFromXml();
        }

        private void LoadMasterPageFromXml()
        {
            if (!String.IsNullOrEmpty(TextBox1.Text))
            {
                XmlDocument xmlMainDoc = new XmlDocument();
                xmlMainDoc.LoadXml(TextBox1.Text);
                XmlNode targetNode = (xmlMainDoc.SelectSingleNode("/PageRoot/Configs/MasterPage"));
                string name_prefix = targetNode.Attributes["Value"].Value.ToString().Replace("~/Master/", "").Replace(".master","")+" - ";
                string name = "";
                // if Name attribute doesn't exists, then pick the first one in the database
                if (targetNode.Attributes["Name"] == null)
                {
                    string master_page_name = targetNode.Attributes["Value"].Value.ToString().Replace("~/Master/", "");
                    if (context.MasterPageConfigs.Any(c => c.Name == master_page_name && c.DeletedFg == false))
                        name = (context.MasterPageConfigs.Where(c => c.Name == master_page_name && c.DeletedFg == false).OrderBy(c=>c.MasterPageId).ToList())[0].Description;
                }
                else
                    name = targetNode.Attributes["Name"].Value.ToString();
                // Master Page should be handled differently. Selete itself and populate the content placeholders
                if (name.Length == 0)
                {
                    if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
                    {
                        //SiteModel.MasterPageConfig cat = context.MasterPageConfigs.Single(c => c.MasterPageId == int.Parse(Request.QueryString["cat"]));
                        ddl_master.SelectedIndex = ddl_master.Items.IndexOf(ddl_master.Items.FindByValue(Request.QueryString["cat"].ToString()));
                    }
                }
                else
                    ddl_master.SelectedIndex = ddl_master.Items.IndexOf(ddl_master.Items.FindByText(name_prefix + name));
                MasterPageIndexChange();
            }
        }


        private void LoadMasterPages()
        {

            var masterPages = from c in context.MasterPageConfigs
                              where c.DeletedFg == false
                              select new
                              {
                                  masterid = c.MasterPageId,
                                  masterName = c.Name.Replace(".master","") + " - " + c.Description,
                                  masterPath = "~/Master/" + c.Name
                              };


            ddl_master.DataTextField = "masterName";
            ddl_master.DataValueField = "masterid";
            ddl_master.DataSource = masterPages;
            ddl_master.DataBind();

            ddl_master.Items.Insert(0, new ListItem("--- Select Master Page ---", "0"));


        }

        private void MasterPageIndexChange()
        {


            InitializeControls();

            try
            {
                if ((ddl_master.SelectedIndex > 0) || (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage))
                {
                    // Load the Master Page
                    string master_vPath = "";
                    if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
                    {
                        master_vPath = "~\\Master\\" + context.MasterPageConfigs.Single(c => c.MasterPageId == int.Parse(Request.QueryString["cat"].ToString()) && c.DeletedFg == false).Name.ToString();
                    }
                    else
                    {
                        master_vPath = context.MasterPageConfigs.Single(c => c.MasterPageId == int.Parse(ddl_master.SelectedValue)).MasterPagePath;// ddl_master.SelectedValue;
                    }
                    var siteMaster = LoadControl(master_vPath);

                    // Find the list of ContentPlaceHolder controls
                    var controls = WebHelper.FindControlsByType<ContentPlaceHolder>(siteMaster);

                    // Do something with each control that was found
                    string controlList = "";
                    if (controls.Count > 0)
                    {
                        IDictionary<string, string> controlListDic = new Dictionary<string, string>();
                        foreach (var control in controls)
                        {
                            controlList = controlList + control.ClientID.ToString() + "<br />";
                            controlListDic.Add(control.ClientID.ToString(), control.ClientID.ToString());
                        }

                        ddl_cont_placedholer.DataTextField = "key";
                        ddl_cont_placedholer.DataValueField = "value";
                        ddl_cont_placedholer.DataSource = controlListDic;
                        ddl_cont_placedholer.DataBind();

                        ddl_cont_placedholer.Items.Insert(0, new ListItem("--- Select Content Placeholder ---", "0"));

                        if (TextBox1.Text.Length > 0)
                        {
                            XmlDocument mainXmlDoc = new XmlDocument();
                            mainXmlDoc.LoadXml(TextBox1.Text);

                            //XmlNode masterPageNode = mainXmlDoc.SelectSingleNode("/PageRoot/Configs/MasterPage");
                            string MasterPageValue = context.MasterPageConfigs.Single(c => c.MasterPageId == int.Parse(ddl_master.SelectedValue.ToString())).MasterPagePath;

                            // replace the absolute path to relative path
                            XmlHelper.Update(mainXmlDoc, "/PageRoot/Configs/MasterPage", "Value", MasterPageValue.Replace("\\","/"));

                            string master_name = context.MasterPageConfigs.Single(c => c.MasterPageId == int.Parse(ddl_master.SelectedValue.ToString())).Description;// ddl_master.SelectedItem.Text.Replace((ddl_master.SelectedValue.ToString().Replace("~/Master/", "").Replace(".master", "") + " - "), "");


                            XmlNode insertPointNode = mainXmlDoc.SelectSingleNode("/PageRoot/Configs/MasterPage");
                            // if Name attribute doesn't exist, then insert. otherwise update the value.
                            if (insertPointNode.Attributes["Name"] == null)
                            {
                                XmlHelper.CreateAttribute(insertPointNode, "Name", master_name);
                            }
                            else
                                XmlHelper.Update(mainXmlDoc, "/PageRoot/Configs/MasterPage", "Name", master_name);
                            

                            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(mainXmlDoc));

                            InitializeControls();
                        }

                        if (TextBox1.Text == "")
                        {
                            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(GetDefaultPageSetting()));
                        }

                    }
                    else
                    {
                        ddl_cont_placedholer.Items.Clear();
                    }
                }
                else
                {
                    ddl_cont_placedholer.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ddl_cont_placedholer.Items.Clear();
                log.Error(ex.Message);
            }
        }

        protected void DdlMasterSelectedIndexChanged(object sender, EventArgs e)
        {
            //show structure update button
            btn_update_cont_struct.Visible = true;
            MasterPageIndexChange();
        }

        protected void DdlContPlacedholerSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();
                //ddl_contrls.Visible = false;

                ddl_contrls.Text = "--- Select a widget ---";
                ddl_contrls.ClearSelection();

                if (ddl_cont_placedholer.SelectedIndex == 0)
                {
                    //selected_control.Visible = false;

                    toolbox1.EnableButtons("", true);
                    ControlGrid.DataSource = null;
                    ControlGrid.DataBind();
                }
                else
                {
                    //selected_control.Visible = true;

                    EnableButtons(PageModes.View);

                    if (TextBox1.Text.Length > 0)
                        LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());

                    toolbox1.EnableButtons("add", true);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private void LoadControlsByContentID(string contentID)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(TextBox1.Text);

            XmlNodeList nodelist = xmlDoc.SelectNodes("/PageRoot/Contents/Content[@ID='" + contentID + "']/Control");
       
                DataTable dt = XmlHelper.ConvertXmlNodeListToDataTable(nodelist);

                DataView vw = dt.DefaultView;
                vw.Sort = "Seq Asc";


                ControlGrid.DataSource = dt;
                ControlGrid.DataBind();

            
        }

        private void LoadUserControls()
        {


            var materList = from c in context.Widgets
                            where c.active_fg == true && c.WidgetType.Widget_Type_Id == int.Parse(ddl_control_types.SelectedItem.Value.ToString())
                            select c;

            ddl_contrls.DataSource = materList;
            ddl_contrls.DataBind();
            log.Debug("User Contols are loaded");
        }



        private void LoadASCXBlankParameters(int widgetID)
        {
            var param = from c in context.Widget_details
                        where c.Widget_id == widgetID
                        select c;

            ctrl_params.DataSource = param;
            ctrl_params.DataBind();

        }
        private void LoadASCXEditParameters(int widgetID, XmlNode selectedNode)
        {


            List<SiteModel.Widget_detail> paramlist = (from c in context.Widget_details
                                                       where c.Widget_id == widgetID
                                                       select c).ToList();


            txt_control_container_style.Text = XmlHelper.GetAttributeValue(selectedNode, "C_Style");
            txt_ascx_class.Text = XmlHelper.GetAttributeValue(selectedNode, "Class");

            XmlNodeList NodeList = selectedNode.ChildNodes;

            XmlElement[] xmlarray = NodeList.Cast<XmlElement>().ToArray<XmlElement>();

            foreach (SiteModel.Widget_detail param in paramlist)
            {
                if (xmlarray.Count(c => c.Attributes["Name"].Value.ToString() == param.Field_name) > 0)
                {

                    param.Default_value = xmlarray.Single(c => c.Attributes["Name"].Value.ToString() == param.Field_name).Attributes["Value"].Value.ToString();
                }
            }

            ctrl_params.DataSource = paramlist;
            ctrl_params.DataBind();

        }
        private void BindHtmlEditParameters(string src, XmlNode selectedNode)
        {


            var materList = from c in context.Widgets
                            where c.active_fg == true && c.WidgetType.Widget_Type_Name.ToLower() == "html"
                            select c;
            ddl_html_control.DataTextField = "widget_name";
            ddl_html_control.DataValueField = "widget_id";
            ddl_html_control.DataSource = materList;
            ddl_html_control.DataBind();


            ddl_html_control.SelectedIndex = ddl_html_control.Items.IndexOf(ddl_html_control.Items.FindByValue(src));
            txt_html_class.Text = XmlHelper.GetAttributeValue(selectedNode, "Class");
            txt_html_style.Text = XmlHelper.GetAttributeValue(selectedNode, "C_Style");
               
            

        }
        private void BindImageEditParameters(XmlNode selectedNode)
        {
            txt_pic_name.Text = XmlHelper.GetAttributeValue(selectedNode, "Name");
            txt_pic_style.Text = XmlHelper.GetAttributeValue(selectedNode, "Style");
            txt_pic_container_style.Text = XmlHelper.GetAttributeValue(selectedNode, "C_Style");
            txt_pic.Text = XmlHelper.GetAttributeValue(selectedNode, "src");
            txt_pic_link_target.Text = XmlHelper.GetAttributeValue(selectedNode, "target");
            txt_pic_link_url.Text = XmlHelper.GetAttributeValue(selectedNode, "href");
            txt_pic_class.Text = XmlHelper.GetAttributeValue(selectedNode, "class");

        }

        private void BindAdZoneEditParameters(string src, XmlNode selectedNode)
        {

            LoadAdZoneControls();


            cbo_zone.SelectedIndex = cbo_zone.FindItemIndexByValue(src);
            txt_zone_class.Text = XmlHelper.GetAttributeValue(selectedNode, "Class");
            txt_zone_style.Text = XmlHelper.GetAttributeValue(selectedNode, "C_Style");
        }



        private XmlDocument GetDefaultPageSetting()
        {
            XmlDocument doc;
            doc = XmlHelper.CreateXmlDocument();

            XmlNode myroot = doc.CreateElement("PageRoot");
            XmlNode rootNode = doc.AppendChild(myroot);

            // Build Config
            XmlNode myconfig = doc.CreateElement("Configs");
            // Master Setting

            XmlNode mySubConfig;
            mySubConfig = doc.CreateElement("MasterPage");

            // if content Type =  MasterPage, the masterpage wouldn't be selected. select one from the basic info.
            if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
            {
                ddl_master.SelectedIndex = ddl_master.Items.IndexOf(ddl_master.Items.FindByValue(Request.QueryString["cat"].ToString()));
            }

            SiteModel.MasterPageConfig masterPageConfig = context.MasterPageConfigs.Single(c => c.MasterPageId == int.Parse(ddl_master.SelectedValue.ToString()));
            XmlHelper.CreateAttribute(mySubConfig, "Value", masterPageConfig.MasterPagePath.Replace('\\', '/'));
            XmlHelper.CreateAttribute(mySubConfig, "Name", masterPageConfig.Description);
            myconfig.AppendChild(mySubConfig);

            rootNode.AppendChild(myconfig);

            // Build Contents

            XmlNode myContents = doc.CreateElement("Contents");

            string[] placeholderList = new string[ddl_cont_placedholer.Items.Count - 1];
            for (int i = 0; i < placeholderList.Length; i++)
            {
                string value = ddl_cont_placedholer.Items[i + 1].Value.ToString();

                placeholderList[i] = value;
            }

            XmlHelper.CreateChildNodes(myContents, "Content", "ID", placeholderList);

            rootNode.AppendChild(myContents);

            return doc;
        }
        private void InitializeControls()
        {
            InitializeAddControl();

            // lb_controls.Items.Clear();

            div_control_type.Visible = false;
            HideAllControlAddPanel();
            ddl_control_types.SelectedIndex = -1;

        }
        private void InitializeAddControl()
        {
            // ascx controls
            ctrl_params.DataSource = null;
            ctrl_params.DataBind();
            ddl_contrls.Text = "--- Select a widget ---";
            ddl_contrls.ClearSelection();



            //image controls
            txt_pic.Text = "";

            //html controls
            ddl_html_control.SelectedIndex = -1;

            //Ad zone Controls
            cbo_zone.SelectedIndex = -1;
        }
        void DeleteSelectedControl()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);


            string placeholder_name = ddl_cont_placedholer.SelectedItem.ToString();
            int deleteIndex = ControlGrid.SelectedItems[0].ItemIndex + 1;

            //XmlNodeList nodeList = xmlMaindoc.SelectNodes("/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq > '" + deleteIndex.ToString() + "']");

            XmlHelper.Delete(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + deleteIndex.ToString() + "']");


            int counter = ControlGrid.Items.Count;
            int starting_index = deleteIndex + 1;
            while (starting_index <= counter)
            {
                XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + starting_index.ToString() + "']", "Seq", (starting_index - 1).ToString());
                starting_index++;
            }

            //foreach (XmlNode node in nodeList)
            //{
            //    int seqNo = int.Parse(node.Attributes["Seq"].Value.ToString());
            //    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + seqNo + "']", "Seq", (seqNo - 1).ToString());
            //}

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
        }


        protected void ParamEditRepeaterItemCommand(object source, RepeaterCommandEventArgs e)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            if (e.CommandName.ToLower() == "update")
            {
                //Label param_name = e.Item.Parent.FindControl("param_name") as Label;
                //string aa = param_name.Text;
                Repeater rpt = (Repeater)source;
                foreach (RepeaterItem ri in rpt.Items)
                {
                    string paramName = ((Label)ri.FindControl("param_name")).Text;
                    string paramValue = ((TextBox)ri.FindControl("param_value")).Text;

                    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedItem.ToString() + "']/Control[@Seq = '" + (ControlGrid.SelectedItems[0].ItemIndex + 1).ToString() + "']/Param[@Name='" + paramName + "']", "Value", paramValue);
                }
            }

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
        }

        protected ValidationDataType GetValidationType(string dbDatatype)
        {
            ValidationDataType vd;
            switch (dbDatatype)
            {
                case "System.Int32":
                    vd = ValidationDataType.Integer;
                    break;
                case "System.String":
                    vd = ValidationDataType.String;
                    break;
                default:
                    vd = ValidationDataType.String;
                    break;
            }

            return vd;
        }

        protected void BtnUpdateContStructClick(object sender, EventArgs e)
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            XmlNodeList nodelist = xmlMaindoc.SelectNodes("/PageRoot/Contents/Content");

            foreach (XmlNode node in nodelist)
            {
                string contentName = node.Attributes["ID"].Value.ToString();

                if (ddl_cont_placedholer.Items.IndexOf(ddl_cont_placedholer.Items.FindByText(contentName)) < 0)
                {
                    XmlHelper.Delete(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + contentName + "']");
                }
            }

            XmlNode myContents = xmlMaindoc.SelectSingleNode("/PageRoot/Contents");

            foreach (ListItem item in ddl_cont_placedholer.Items)
            {
                if (item.Value.ToString() != "0")
                {
                    XmlNode node = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + item.Text + "']");
                    if (node == null)
                    {
                        string[] missingCont = new string[1];
                        missingCont[0] = item.Text.ToString();
                        XmlHelper.CreateChildNodes(myContents, "Content", "ID", missingCont);
                    }
                }
            }

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
        }

        protected void BtnImportClick(object sender, EventArgs e)
        {

            ImportXML();


        }


        void ImportXML()
        {

            int catid = int.Parse(Request.QueryString["cat"]);
            if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Article)
            {
                ArticleModel.ArticleCategory artCat = context.ArticleCategories.Single(c => c.ArtCatId == catid);
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    artCat.metadataStr = TextBox1.Text;
                else
                    artCat.DetailMetadataStr = TextBox1.Text;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Forum)
            {
                ForumModel.ForumConfig forCat = context.ForumConfigs.Single(c => c.ForumId == catid);
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    forCat.MetadataStr = TextBox1.Text;
                else
                    forCat.DetailMetadataStr = TextBox1.Text;

            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Template)
            {
                SiteModel.PageTemplate pagCat = context.PageTemplates.Single(c => c.TemplateId == catid);
                pagCat.MetadataStr = TextBox1.Text;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
            {
                SiteModel.MasterPageConfig pagCat = context.MasterPageConfigs.Single(c => c.MasterPageId == catid);
                pagCat.MetadataStr = TextBox1.Text;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Page)
            {
                SiteModel.CustomPage pagCat = context.CustomPages.Single(c => c.CustomPageId == catid);
                pagCat.MetadataStr = TextBox1.Text;
            }
            else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Classified)
            {
                ClassifiedModel.ClassifiedCategory claCat = context.ClassifiedCategories.Single(c => c.CatId == catid);
                if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                    claCat.MetadataStr = TextBox1.Text;
                else
                    claCat.DetailMetadataStr = TextBox1.Text;
            }


            context.SaveChanges();

            EPM.Legacy.Common.Utility.RegisterJsAlert(this.Parent.Page, "Page setting has been updated successfully.");

            // Reload page
            Response.Redirect(Request.RawUrl);
        }

        protected void DdlControlTypesSelectedIndexChanged(object sender, EventArgs e)
        {
            HideAllControlAddPanel();
            InitializeAddControl();
            if (ddl_control_types.SelectedIndex > 0)
            {
                // div_ascx_control.Visible = true;
                switch (ddl_control_types.SelectedItem.ToString().ToUpper())
                {
                    case "HTML":
                        {
                            div_html_control.Visible = true;
                            LoadHtmlControls();
                            break;
                        }
                    case "ASCX":
                        {
                            div_ascx_control.Visible = true;
                            ddl_contrls.Visible = true;
                            SiteModel.WidgetType wType = (from c in context.WidgetTypes
                                                          where c.Widget_Type_Id == int.Parse(ddl_control_types.SelectedValue.ToString())
                                                          select c).Single();

                            if (wType.Widget_Type_Name.ToLower() == "ascx")
                            {
                                toolbox1.SetViewStateModeGroupButtons("save", ViewStateMode.Enabled);
                                toolbox1.SetValidationGroupButtons("save", "ControlAddValidation", true);

                            }
                            LoadUserControls();
                            break;
                        }
                    case "IMAGE":
                        {
                            div_pic_control.Visible = true;
                            break;
                        }
                    case "BANNER AD":
                        {
                            div_ad_control.Visible = true;
                            LoadAdZoneControls();
                            break;
                        }
                    default:

                        break;
                }
            }
        }

        void LoadHtmlControls()
        {

            var materList = from c in context.Widgets
                            where c.active_fg == true && c.WidgetType.Widget_Type_Id == int.Parse(ddl_control_types.SelectedValue.ToString())
                            select c;
            ddl_html_control.DataTextField = "widget_name";
            ddl_html_control.DataValueField = "widget_id";
            ddl_html_control.DataSource = materList;
            ddl_html_control.DataBind();
        }

        void LoadAdZoneControls()
        {
            cbo_zone.DataTextField = "ZoneName";
            cbo_zone.DataValueField = "AdZoneId";
            cbo_zone.DataSource = EPM.Business.Model.Ad.ZoneController.GetAllZones().Where(c => c.ActiveFg == true).ToList();
            cbo_zone.DataBind();
        }

        void HideAllControlAddPanel()
        {
            div_ascx_control.Visible = false;
            div_html_control.Visible = false;
            div_pic_control.Visible = false;
            div_ad_control.Visible = false;
            txt_control_container_style.Text = "";
            txt_ascx_class.Text = "";
            txt_html_class.Text = "";
            txt_html_style.Text = "";
            txt_pic_container_style.Text = "";
            txt_pic_style.Text = "";
            txt_pic_name.Text = "";
            txt_pic.Text = "";
            txt_zone_class.Text = "";
            txt_zone_style.Text = "";
        }

        protected void DdlContrlsSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (ddl_contrls.SelectedIndex > -1)
                    LoadASCXBlankParameters(int.Parse(ddl_contrls.SelectedValue.ToString()));
                else
                    InitializeControls();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        protected void ctrl_params_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item ||
              e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    SiteModel.Widget_detail detail = e.Item.DataItem as SiteModel.Widget_detail;

                    if (detail.Field_data_type == "System.Boolean")
                    {
                        RadioButton trueButton = e.Item.FindControl("param_value_true") as RadioButton;
                        trueButton.Visible = true;

                        RadioButton falseButton = e.Item.FindControl("param_value_false") as RadioButton;
                        falseButton.Visible = true;

                        if (detail.Default_value.ToLower() == "true")
                            trueButton.Checked = true;
                        else
                            falseButton.Checked = true;
                    }
                    else if (detail.Field_data_type.Contains("System.Int"))
                    {
                        TextBox param_value_int = e.Item.FindControl("param_value_int") as TextBox;
                        RegularExpressionValidator param_value_int_datatype = e.Item.FindControl("param_value_int_datatype") as RegularExpressionValidator;
                        RequiredFieldValidator param_value_int_required = e.Item.FindControl("param_value_int_required") as RequiredFieldValidator;

                        param_value_int.Visible = true;


                        param_value_int_datatype.Visible = true;
                        param_value_int_required.Visible = true;
                    }
                    else if (detail.Field_data_type.Contains("+") || detail.Field_data_type.Contains("Telerik.Web.UI."))
                    {

                        Type t = null;
                        if (detail.Field_data_type.Contains("Telerik.Web.UI."))
                        {
                            var assembly = System.Reflection.Assembly.Load("Telerik.Web.UI") ;
                            t = assembly.GetType(detail.Field_data_type);
                        }
                        else
                        {
                         t=   Type.GetType(detail.Field_data_type);
                        }
                        string[] items = t.GetEnumNames();

                        DropDownList param_value_list = e.Item.FindControl("param_value_list") as DropDownList;
                        param_value_list.Visible = true;
                        foreach (string item in items)
                        {
                            param_value_list.Items.Add(new ListItem(item, item));
                        }

                        param_value_list.SelectedIndex = param_value_list.Items.IndexOf(param_value_list.Items.FindByText(detail.Default_value));
                    }
                    else
                    {
                        RequiredFieldValidator param_required = e.Item.FindControl("param_required") as RequiredFieldValidator;

                        TextBox param_value = e.Item.FindControl("param_value") as TextBox;
                        param_required.Visible = true;
                        param_value.Visible = true;
                    }
                }
            }
            catch
            {
            }

        }




        public enum PageModes
        {
            View,
            Add,
            Edit
        }




        protected void btn_SavePage_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["cat"] != null)
            {


                if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Article)
                {
                    ArticleModel.ArticleCategory artCat = context.ArticleCategories.Single(c => c.ArtCatId == int.Parse(Request.QueryString["cat"]));
                    if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                        artCat.metadataStr = TextBox1.Text;
                    else
                        artCat.DetailMetadataStr = TextBox1.Text;
                }
                else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Forum)
                {
                    ForumModel.ForumConfig forCat = context.ForumConfigs.Single(c => c.ForumId == int.Parse(Request.QueryString["cat"]));
                    
                    if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                        forCat.MetadataStr = TextBox1.Text;
                    else
                        forCat.DetailMetadataStr = TextBox1.Text;
                }
                else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Template)
                {
                    SiteModel.PageTemplate pageCat = context.PageTemplates.Single(c => c.TemplateId == int.Parse(Request.QueryString["cat"]));
                    pageCat.MetadataStr = TextBox1.Text;
                }
                else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.MasterPage)
                {
                    SiteModel.MasterPageConfig pageCat = context.MasterPageConfigs.Single(c => c.MasterPageId == int.Parse(Request.QueryString["cat"]));
                    pageCat.MetadataStr = TextBox1.Text;
                }
                else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Page)
                {
                    SiteModel.CustomPage pageCat = context.CustomPages.Single(c => c.CustomPageId == int.Parse(Request.QueryString["cat"]));
                    pageCat.MetadataStr = TextBox1.Text;
                }
                else if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Classified)
                {

                    ClassifiedModel.ClassifiedCategory claCat = context.ClassifiedCategories.Single(c => c.CatId == int.Parse(Request.QueryString["cat"]));
                    if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                        claCat.MetadataStr = TextBox1.Text;
                    else
                        claCat.DetailMetadataStr = TextBox1.Text;
                }
         

                context.SaveChanges();

                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Parent.Page, "Page setting has been updated successfully.");
            }
      
            else   if (ContentType == EPM.Core.EPMBasePage.ContentTypes.Biz)
                {
                    
                    BizModel.BusinessEntityConfig bizconfig = context.BusinessEntityConfigs.Single(c => 1==1);
                    if (UseForType == EPM.Core.EPMBasePage.UseForTypes.ListView)
                        bizconfig.MetadataStr = TextBox1.Text;
                    else
                        bizconfig.DetailMetadataStr = TextBox1.Text;

                    context.SaveChanges();

                    EPM.Legacy.Common.Utility.RegisterJsAlert(this.Parent.Page, "Page setting has been updated successfully.");
                }
            
            else
            {
                EPM.Legacy.Common.Utility.RegisterJsAlert(this.Parent.Page, "An error occured.");
            }
        }




        private  static EPM.Core.EPMBasePage.ContentTypes contentType;
        public  EPM.Core.EPMBasePage.ContentTypes ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        private EPM.Core.EPMBasePage.UseForTypes useForType = EPM.Core.EPMBasePage.UseForTypes.ListView;
        public EPM.Core.EPMBasePage.UseForTypes UseForType
        {
            get { return useForType; }
            set { useForType = value; }
        }



        private static PageModes buildMode;
        public static PageModes BuildMode
        {
            get { return buildMode; }
            set { buildMode = value; }
        }



        protected void btn_template_Click(object sender, EventArgs e)
        {
            if (ddl_template.SelectedIndex > 0)
            {

                TextBox1.Text = Server.HtmlDecode(context.PageTemplates.Single(c => c.TemplateId == int.Parse(ddl_template.SelectedItem.Value)).MetadataStr);
                ImportXML();
            }
        }


        protected void ControlGrid_RowDrop(object sender, GridDragDropEventArgs e)
        {
            moveControl(e.DraggedItems[0].ItemIndex, e.DestDataItem.ItemIndex);
        }

        private void moveControl(int control_index, int destination_index)
        {

            string placeholder_name = ddl_cont_placedholer.SelectedItem.ToString();
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            int detinationItemSeq = destination_index + 1;
            int movingItemSeq = control_index + 1;

            // get the node of moving item
            XmlNode nodeMoving = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + movingItemSeq.ToString() + "']");
            // get the original seq of the moved item
            string MovingNodeID = nodeMoving.Attributes["ID"].Value.ToString();


            // Check if the item moved 
            if (movingItemSeq < detinationItemSeq) //Moving Below
            {
                //find all nodes having lower sequence than movingItemSeq and give it -1 to its seq
                int counter = movingItemSeq + 1;
                while (counter <= detinationItemSeq)
                {
                    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + counter.ToString() + "']", "Seq", (counter - 1).ToString());
                    counter++;
                }
            }
            else // Moving Above
            {
                //find all nodes having higher sequence than movingItemSeq and give it +1 to its seq
                int counter = movingItemSeq - 1;
                while (counter >= detinationItemSeq)
                {
                    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@Seq='" + counter.ToString() + "']", "Seq", (counter + 1).ToString());
                    counter--;
                }
            }

            // update the moving item seq
            XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + placeholder_name + "']/Control[@ID='" + MovingNodeID + "']", "Seq", detinationItemSeq.ToString());



            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));

            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            EnableButtons(PageModes.View);
        }

        

        protected void ControlGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ControlGrid.SelectedItems[0].ItemIndex > -1)
            {

                HideAllControlAddPanel();
                div_control_type.Visible = false;

                EnableButtons(PageModes.View);
            }
        }


        // Return the max number of ID in the control list
        private int getMaxControlNumber()
        {
            List<int> value_list = new List<int>();
            GridItemCollection gridRows = ControlGrid.Items;
            foreach (GridDataItem data in gridRows)
            {
                string cont_id = data.GetDataKeyValue("ID").ToString();
                value_list.Add(int.Parse(cont_id.Substring(cont_id.LastIndexOf("_") + 1, cont_id.Length - cont_id.LastIndexOf("_") - 1)));
            }

            if (value_list.Count == 0)
                return 1;
            else
                return value_list.Max();

        }

        // Get selected Item ID from the Grid
        private string getSelectItemID()
        {
            string selectedText = "";
            GridItemCollection gridRows = ControlGrid.Items;
            foreach (GridDataItem data in gridRows)
            {
                if (data.Selected)
                {
                    selectedText = data["ID"].Text;
                    break;
                }
            }

            return selectedText;
        }

        private void EnableButtons(PageModes pageMode)
        {

            if (pageMode == PageModes.View)
            {

                if (ControlGrid.SelectedItems.Count > 0)
                {

                    toolbox1.EnableButtons("add,delete,edit", true);
                }
                else
                {
                    toolbox1.EnableButtons("add", true);
                }
                toolbox1.SetValidationGroupButtons("", "", true);


            }
            else if (pageMode == PageModes.Edit)
            {

                toolbox1.EnableButtons("save,cancel", true);
                

                XmlDocument xmlMaindoc = new XmlDocument();
                xmlMaindoc.LoadXml(TextBox1.Text);
                XmlNode selectedNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedItem.ToString() + "']/Control[@Seq='" + (ControlGrid.SelectedItems[0].ItemIndex + 1).ToString() + "']");
                int widgetType = int.Parse(selectedNode.Attributes["Type"].Value.ToString());
                SiteModel.WidgetType wType = (from c in context.WidgetTypes
                         where c.Widget_Type_Id == widgetType
                         select c).Single();

                if (wType.Widget_Type_Name.ToLower() == "ascx")
                {
                    toolbox1.SetViewStateModeGroupButtons("save", ViewStateMode.Enabled);
                    toolbox1.SetValidationGroupButtons("save", "ControlAddValidation", true);

                }

            }
            else if (pageMode == PageModes.Add)
            {

                toolbox1.EnableButtons("save,cancel", true);



            }


            // Pannel Controls
            if (pageMode == PageModes.Add)
            {
                div_control_type.Visible = true;
                div_ascx_control.Visible = false;
                div_html_control.Visible = false;
                div_pic_control.Visible = false;
                div_ad_control.Visible = false;


            }
            else if (pageMode == PageModes.Edit)
            {
                div_control_type.Visible = false;
                div_ascx_control.Visible = false;
                div_html_control.Visible = false;
                div_pic_control.Visible = false;
                div_ad_control.Visible = false;


            }
            else
            {
                div_control_type.Visible = false;
                div_ascx_control.Visible = false;
                div_html_control.Visible = false;
                div_pic_control.Visible = false;
                div_ad_control.Visible = false;

            }

        }


        void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();

            if (action == "add")
                AddButtonEvent();
            else if (action == "edit")
                EditButtonEvent();
            else if (action == "delete")
                DeleteButtonEvent();
            else if (action == "save")
                SaveButtonEvent();
            else if (action == "cancel")
                CancelButtonEvent();

        }


        void AddButtonEvent()
        {
            div_control_type.Visible = true;
            div_ascx_control.Visible = false;
            div_html_control.Visible = false;
            div_pic_control.Visible = false;
            div_ad_control.Visible = false;
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            EnableButtons(PageModes.Add);
            BuildMode = PageModes.Add;
        }

        void EditButtonEvent()
        {
            //TextBox1.Text = lb_controls.SelectedIndex.ToString();
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            XmlNode selectedNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedItem.ToString() + "']/Control[@Seq='" + (ControlGrid.SelectedItems[0].ItemIndex + 1).ToString() + "']");
            //XmlNodeList paramList =  selectedNode.SelectNodes("//Param");



            int widgetType = int.Parse(selectedNode.Attributes["Type"].Value.ToString());
            string src = selectedNode.Attributes["src"].Value.ToString();

            SiteModel.WidgetType wType = (from c in context.WidgetTypes
                                          where c.Widget_Type_Id == widgetType
                                          select c).Single();
            EnableButtons(PageModes.Edit);
            if (wType.Widget_Type_Name.ToLower() == "ascx")
            {

                div_ascx_control.Visible = true;
                ddl_contrls.Visible = false;

                LoadASCXEditParameters(context.Widgets.Single(c => c.File_path == src && c.active_fg == true).Widget_id, selectedNode);


            }
            else if (wType.Widget_Type_Name.ToLower() == "html")
            {
                div_html_control.Visible = true;
                BindHtmlEditParameters(src, selectedNode);

            }
            else if (wType.Widget_Type_Name.ToLower() == "image")
            {
                div_pic_control.Visible = true;
                BindImageEditParameters(selectedNode);
            }
            else if (wType.Widget_Type_Name.ToLower() == "banner ad")
            {
                div_ad_control.Visible = true;
                BindAdZoneEditParameters(src,selectedNode);
            }

            BuildMode = PageModes.Edit;

        }

        void DeleteButtonEvent()
        {
            DeleteSelectedControl();
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
        }

        void SaveButtonEvent()
        {

            // if XML has not been configured, fill up with the structure from seleted master page.
            if (TextBox1.Text == "")
            {
                TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(GetDefaultPageSetting()));
            }

            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            int widgetType;
            string src = "";
            SiteModel.WidgetType wType;

            if (BuildMode == PageModes.Edit)
            {
                XmlNode selectedNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedItem.ToString() + "']/Control[@Seq='" + (ControlGrid.SelectedItems[0].ItemIndex + 1).ToString() + "']");
                //XmlNodeList paramList =  selectedNode.SelectNodes("//Param");

                widgetType = int.Parse(selectedNode.Attributes["Type"].Value.ToString());
                src = selectedNode.Attributes["src"].Value.ToString();
                wType = (from c in context.WidgetTypes
                         where c.Widget_Type_Id == widgetType
                         select c).Single();

            }
            else
            {
                wType = (from c in context.WidgetTypes
                         where c.Widget_Type_Id == int.Parse(ddl_control_types.SelectedValue.ToString())
                         select c).Single();
            }


            if (wType.Widget_Type_Name.ToLower() == "ascx")
            {
                if (BuildMode == PageModes.Add)
                {

                    AddASCX();
                }
                else if (BuildMode == PageModes.Edit)
                {

                    EditASCX();
                }
            }
            else if (wType.Widget_Type_Name.ToLower() == "html")
            {

                if (BuildMode == PageModes.Add)
                {
                    AddHTML();
                }
                else if (BuildMode == PageModes.Edit)
                {
                    EditHTML();
                }
            }
            else if (wType.Widget_Type_Name.ToLower() == "image")
            {
                if (BuildMode == PageModes.Add)
                {
                    AddImage();
                }
                else if (BuildMode == PageModes.Edit)
                {
                    EditImage();
                }
            }
            else if (wType.Widget_Type_Name.ToLower() == "banner ad")
            {
                if (BuildMode == PageModes.Add)
                {
                    AddAdZone();
                }
                else if (BuildMode == PageModes.Edit)
                {
                    EditAdZone();
                }
            }

        }
        void CancelButtonEvent()
        {
            div_control_type.Visible = false;
            div_ascx_control.Visible = false;
            div_html_control.Visible = false;
            div_pic_control.Visible = false;
            div_ad_control.Visible = false;
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            ddl_control_types.SelectedIndex = -1;
            EnableButtons(PageModes.View);
            BuildMode = PageModes.View;
        }


        void AddASCX()
        {



            if (TextBox1.Text == "")
            {
                TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(GetDefaultPageSetting()));
            }
            

            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);
            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");


            var param = (from c in context.Widgets
                         where c.Widget_id == int.Parse(ddl_contrls.SelectedValue.ToString())
                         select new
                         {
                             ID = c.Widget_id,
                             src = c.File_path,
                             name = c.Widget_name
                         }).Single();


            XmlHelper.CreateAttribute(newControl, "ID", ddl_cont_placedholer.SelectedValue.ToString() + "_ascx_" + (getMaxControlNumber() + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", ddl_control_types.SelectedValue.ToString());
            XmlHelper.CreateAttribute(newControl, "src", param.src.ToString());
            XmlHelper.CreateAttribute(newControl, "Name", param.name.ToString());
            XmlHelper.CreateAttribute(newControl, "Seq", (ControlGrid.Items.Count + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "C_Style", txt_control_container_style.Text);
            XmlHelper.CreateAttribute(newControl, "Class", txt_ascx_class.Text);


            foreach (RepeaterItem item in ctrl_params.Items)
            {
                string paramValue_text = "";
                XmlNode newParam = xmlMaindoc.CreateElement("Param");
                Label paramName = (Label)item.FindControl("param_name");
                Label paramType = (Label)item.FindControl("param_type");
                if (paramType.Text == "System.Boolean")
                {
                    RadioButton param_value_true = (RadioButton)item.FindControl("param_value_true");
                    paramValue_text = param_value_true.Checked ? "True" : "False";
                }
                else if (paramType.Text.Contains("System.Int"))
                {
                    TextBox param_value_int = (TextBox)item.FindControl("param_value_int");
                    paramValue_text = param_value_int.Text;
                }
                else if (paramType.Text.Contains("+"))
                {
                    DropDownList param_value_list = item.FindControl("param_value_list") as DropDownList;
                    paramValue_text = param_value_list.SelectedItem.Text;
                }
                else
                {
                    TextBox paramValue = (TextBox)item.FindControl("param_value");
                    paramValue_text = paramValue.Text;
                }
                RequiredFieldValidator required = (RequiredFieldValidator)item.FindControl("param_required");
                XmlHelper.CreateAttribute(newParam, "Name", paramName.Text);
                XmlHelper.CreateAttribute(newParam, "Type", paramType.Text);
                XmlHelper.CreateAttribute(newParam, "Value", paramValue_text);
                XmlHelper.CreateAttribute(newParam, "Required", required.Enabled.ToString());
                newControl.AppendChild(newParam);
            }

            insertPointNode.AppendChild(newControl);


            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1;
            txt_control_container_style.Text = "";
        }
        void EditASCX()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            string selectedText = getSelectItemID();

            string EditPointXPath = "/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']/Control[@ID='" + selectedText + "']";

            XmlHelper.Update(xmlMaindoc, EditPointXPath, "C_Style", txt_control_container_style.Text);
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "Class", txt_ascx_class.Text, true);

            
            foreach (RepeaterItem item in ctrl_params.Items)
            {
                string paramValue_text = "";
                

                XmlNode newParam = xmlMaindoc.CreateElement("Param");
                Label paramName = (Label)item.FindControl("param_name");
                Label paramType = (Label)item.FindControl("param_type");

                if (paramType.Text == "System.Boolean")
                {
                    RadioButton param_value_true = (RadioButton)item.FindControl("param_value_true");
                    paramValue_text = param_value_true.Checked ? "True" : "False";
                }
                else if (paramType.Text.Contains("System.Int"))
                {
                    TextBox param_value_int = (TextBox)item.FindControl("param_value_int");
                    paramValue_text = param_value_int.Text;
                }
                else if ((paramType.Text.Contains("+")) || (paramType.Text.Contains("Telerik.Web.UI")))
                {
                    DropDownList param_value_list = item.FindControl("param_value_list") as DropDownList;
                    paramValue_text = param_value_list.SelectedItem.Text;

                }
                else
                {
                    TextBox paramValue = (TextBox)item.FindControl("param_value");
                    paramValue_text = paramValue.Text;
                }
                string apath = "/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']/Control[@ID='" + selectedText + "']/Param[@Name='" + paramName.Text + "']";
                XmlNode test = xmlMaindoc.SelectSingleNode(apath);

                if ((xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']/Control[@ID='" + selectedText + "']/Param[@Name='" + paramName.Text + "']") == null))
                {
                    RequiredFieldValidator required = (RequiredFieldValidator)item.FindControl("param_required");
                    XmlHelper.CreateAttribute(newParam, "Name", paramName.Text);
                    XmlHelper.CreateAttribute(newParam, "Type", paramType.Text);
                    XmlHelper.CreateAttribute(newParam, "Value", paramValue_text);
                    XmlHelper.CreateAttribute(newParam, "Required", required.Enabled.ToString());
                    xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']/Control[@ID='" + selectedText + "']").AppendChild(newParam);
                }
                else
                {
                    // Update Control Params
                    XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']/Control[@ID='" + selectedText + "']/Param[@Name='" + paramName.Text + "']", "Value", paramValue_text);
                }

                // Update Style
                XmlHelper.Update(xmlMaindoc, "/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']/Control[@ID='" + selectedText + "']", "C_Style", txt_control_container_style.Text);

            }

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));

            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1;
            txt_control_container_style.Text = "";
            //lb_controls.SelectedIndex = -1;
        }
        void AddHTML()
        {
            if (TextBox1.Text == "")
            {
                TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(GetDefaultPageSetting()));
            }

            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);
            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");

            XmlHelper.CreateAttribute(newControl, "ID", ddl_cont_placedholer.SelectedValue.ToString() + "_html_" + (getMaxControlNumber() + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", ddl_control_types.SelectedValue.ToString());
            XmlHelper.CreateAttribute(newControl, "src", ddl_html_control.SelectedValue.ToString());
            XmlHelper.CreateAttribute(newControl, "Seq", (ControlGrid.Items.Count + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Name", ddl_html_control.SelectedItem.ToString().Trim());
            XmlHelper.CreateAttribute(newControl, "C_Style", txt_html_style.Text);
            XmlHelper.CreateAttribute(newControl, "Class", txt_html_class.Text);

            insertPointNode.AppendChild(newControl);

            //Telerik.Web.UI.RadListBoxItem raditem = new Telerik.Web.UI.RadListBoxItem((lb_controls.Items.Count + 1).ToString() + " - " + ddl_html_control.SelectedItem.ToString().Trim(), ddl_cont_placedholer.SelectedValue.ToString() + "_html_" + (getMaxControlNumber() + 1).ToString());
            //lb_controls.Items.Add(raditem);



            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            div_html_control.Visible = false;
            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1;
        }
        void EditHTML()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            string selectedText = getSelectItemID();
            string placeholder = ddl_cont_placedholer.SelectedValue.ToString();


            string EditPointXPath = "/PageRoot/Contents/Content[@ID='" + placeholder + "']/Control[@ID='" + selectedText + "']";
            // Update Control Params
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "src", ddl_html_control.SelectedValue.ToString());
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "Name", ddl_html_control.SelectedItem.ToString().Trim());
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "C_Style", txt_html_style.Text,true);
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "Class", txt_html_class.Text,true);

            div_html_control.Visible = false;

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1; ;
        }
        void AddImage()
        {
            if (TextBox1.Text == "")
            {
                TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(GetDefaultPageSetting()));
            }

            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);
            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");

            XmlHelper.CreateAttribute(newControl, "ID", ddl_cont_placedholer.SelectedValue.ToString() + "_Img_" + (getMaxControlNumber() + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", ddl_control_types.SelectedValue.ToString());
            XmlHelper.CreateAttribute(newControl, "src", txt_pic.Text);
            XmlHelper.CreateAttribute(newControl, "Style", txt_pic_style.Text);
            XmlHelper.CreateAttribute(newControl, "C_Style", txt_pic_container_style.Text);
            XmlHelper.CreateAttribute(newControl, "Name", txt_pic_name.Text.Trim());
            XmlHelper.CreateAttribute(newControl, "Seq", (ControlGrid.Items.Count + 1).ToString());

            XmlHelper.CreateAttribute(newControl, "class", txt_pic_class.Text);
            XmlHelper.CreateAttribute(newControl, "href", txt_pic_link_url.Text);
            XmlHelper.CreateAttribute(newControl, "target", txt_pic_link_target.Text);

            insertPointNode.AppendChild(newControl);

            //Telerik.Web.UI.RadListBoxItem raditem = new Telerik.Web.UI.RadListBoxItem((lb_controls.Items.Count + 1).ToString() + " - " + txt_pic_name.Text.Trim(), ddl_cont_placedholer.SelectedValue.ToString() + "_Img_" + (getMaxControlNumber() + 1).ToString());
            //lb_controls.Items.Add(raditem);

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            div_pic_control.Visible = false;

            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1;
        }
        void EditImage()
        {

            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            string selectedText = getSelectItemID();
            string placeholder = ddl_cont_placedholer.SelectedValue.ToString();
            string EditPointXpath = "/PageRoot/Contents/Content[@ID='" + placeholder + "']/Control[@ID='" + selectedText + "']";
            XmlHelper.Update(xmlMaindoc, EditPointXpath, "src", txt_pic.Text);
            XmlHelper.Update(xmlMaindoc, EditPointXpath, "Style", txt_pic_style.Text);
            XmlHelper.Update(xmlMaindoc, EditPointXpath, "C_Style", txt_pic_container_style.Text);
            XmlHelper.Update(xmlMaindoc, EditPointXpath, "Name", txt_pic_name.Text.Trim());
            XmlHelper.Update(xmlMaindoc, EditPointXpath, "class", txt_pic_class.Text.Trim(), true);
            XmlHelper.Update(xmlMaindoc, EditPointXpath, "href", txt_pic_link_url.Text.Trim(), true);
            XmlHelper.Update(xmlMaindoc, EditPointXpath, "target", txt_pic_link_target.Text.Trim(),true);
            

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            div_pic_control.Visible = false;

            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1;

        }

        void AddAdZone()
        {
            if (TextBox1.Text == "")
            {
                TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(GetDefaultPageSetting()));
            }

            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);
            XmlNode insertPointNode = xmlMaindoc.SelectSingleNode("/PageRoot/Contents/Content[@ID='" + ddl_cont_placedholer.SelectedValue.ToString() + "']");

            XmlNode newControl = xmlMaindoc.CreateElement("Control");

            XmlHelper.CreateAttribute(newControl, "ID", ddl_cont_placedholer.SelectedValue.ToString() + "_adzone_" + (getMaxControlNumber() + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Type", ddl_control_types.SelectedValue.ToString());
            XmlHelper.CreateAttribute(newControl, "src", cbo_zone.SelectedValue.ToString());
            XmlHelper.CreateAttribute(newControl, "Seq", (ControlGrid.Items.Count + 1).ToString());
            XmlHelper.CreateAttribute(newControl, "Name", cbo_zone.SelectedItem.Value.ToString().Trim());
            XmlHelper.CreateAttribute(newControl, "C_Style", txt_zone_style.Text);
            XmlHelper.CreateAttribute(newControl, "Class", txt_zone_class.Text);

            insertPointNode.AppendChild(newControl);


            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            div_ad_control.Visible = false;
            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1;
        }
        void EditAdZone()
        {
            XmlDocument xmlMaindoc = new XmlDocument();
            xmlMaindoc.LoadXml(TextBox1.Text);

            string selectedText = getSelectItemID();
            string placeholder = ddl_cont_placedholer.SelectedValue.ToString();


            string EditPointXPath = "/PageRoot/Contents/Content[@ID='" + placeholder + "']/Control[@ID='" + selectedText + "']";
            // Update Control Params
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "src", cbo_zone.SelectedValue.ToString());
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "Name", cbo_zone.SelectedItem.Value.ToString().Trim());
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "C_Style", txt_zone_style.Text, true);
            XmlHelper.Update(xmlMaindoc, EditPointXPath, "Class", txt_zone_class.Text, true);

            div_ad_control.Visible = false;

            TextBox1.Text = Server.HtmlDecode(XmlHelper.DocumentToString(xmlMaindoc));
            LoadControlsByContentID(ddl_cont_placedholer.SelectedItem.ToString());
            EnableButtons(PageModes.View);
            ddl_control_types.SelectedIndex = -1; ;
        }


        protected void btn_page_preview_Click(object sender, EventArgs e)
        {
            Session["Preview_XML"] = TextBox1.Text;

            string query_string = "";
            if (Request.QueryString["cat"] != null)
                query_string = "?p=" + Request.QueryString["cat"].ToString() + "&content=" + ContentType.ToString();

            RadWindow windowpreview = new RadWindow();
            windowpreview.NavigateUrl =  "/cp/pages/preview.aspx" + query_string;
            windowpreview.VisibleOnPageLoad = true;
            windowpreview.Modal = true;
            windowpreview.Behaviors = Telerik.Web.UI.WindowBehaviors.Maximize | Telerik.Web.UI.WindowBehaviors.Close | Telerik.Web.UI.WindowBehaviors.Resize | Telerik.Web.UI.WindowBehaviors.Move;
            windowpreview.OpenerElementID = btn_page_preview.ClientID;
            windowpreview.AutoSize = true;
            windowpreview.ShowContentDuringLoad = true;
            //windowpreview.RestrictionZoneID = "content-outer";

            PageBuilder_Controls.Controls.Add(windowpreview);


            //Telerik.Web.UI.RadScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AA", "window.open('" + ResolveClientUrl("~/cp/pages/preview.aspx") + query_string + "','_blank');", true);
            
        }




    }

}