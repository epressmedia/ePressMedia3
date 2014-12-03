using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


public partial class Cp_Site_SiteMenu : System.Web.UI.Page
{
    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMenuTree();
            bindContentTypes();

            toolbox1.EnableButtons("", true);
            
        }
        
        toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked);
    }

    

    void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        string action = e.Item.Text.ToLower();

        if (action == "add")
            AddButtonEvent();
        else if (action == "up")
            UpButtonEvent();
        else if (action == "down")
            DownButtonEvent();
        else if (action == "move")
            MoveButtonEvent();
        else if (action == "edit")
            EditButtonEvent();
        else if (action == "delete")
            DeleteButtonEvent();
        else if (action == "save")
            SaveButtonEvent();
        else if (action == "save changes")
            SaveChangesButtonEvent();
        else if (action == "cancel")
            CancelButtonEvent();
        
  
    }

    void toolbox1_btnHandler(string aa)
    {
        Response.Write("Main PAge Event value:"+aa);
    }

    void bindContentTypes()
    {
        
        var result = from c in context.ContentTypes
                     where c.Enabled == true
                     select c;

        TypeList.DataTextField = "ContentTypeName";
        TypeList.DataValueField = "ContentTypeID";
        TypeList.DataSource = result;
        TypeList.DataBind();

        //NameValueCollection col = Knn.Website.ContentType.SelectSupportedTypes();
        //foreach (string key in col.AllKeys)
        //    TypeList.Items.Add(new ListItem(col[key], key));

        TypeList.Items.Insert(0, new ListItem("-- Select Content Type --", "-1"));
        //TypeList.Items.Insert(TypeList.Items.Count, (new ListItem("사용자 정의", "0")));

        bindContentView();
        bindContentData();
    }

    // 선택된 ContentType에서 사용 가능한 데이터를 표시한다.
    void bindContentData()
    {
        DataList.Items.Clear();

        if (TypeList.SelectedIndex > 0)
        {
            NameValueCollection col = null;
            switch (TypeList.SelectedItem.Text)
            {
                case "Article/News": 
                    //col = Knn.Article.ArticleCategory.SelectCategories(); 
                    List<ArticleModel.ArticleCategory> article_categories = (from c in context.ArticleCategories 
                                                                             orderby c.CatName
                                  select c).ToList();

                    DataList.DataTextField = "CatName";
                    DataList.DataValueField = "ArtCatId";
                    DataList.DataSource = article_categories;
                    DataList.DataBind();


                    

                    //foreach (ArticleModel.ArticleCategory article_category in article_categories)
                    //    DataList.Items.Add(new ListItem(article_category.CatName, article_category.ArtCatId.ToString()));
                    break;
                case "Forum":
                    List<ForumModel.Forum> forums = (from c in context.Forums
                                                     orderby c.ForumName
                                                     select c).ToList();
                    //col = Knn.Forum.Forum.SelectForums();
                    foreach (ForumModel.Forum forum in forums)
                        DataList.Items.Add(new ListItem(forum.ForumName, forum.ForumId.ToString()));
                    break;
                case "Classified":
                    List<ClassifiedModel.ClassifiedCategory> classifiedAds = (from c in context.ClassifiedCategories
                                                     orderby c.CatName
                                                     select c).ToList();
                    foreach (ClassifiedModel.ClassifiedCategory classified in classifiedAds)
                        DataList.Items.Add(new ListItem(classified.CatName, classified.CatId.ToString()));
                    break;
                case "Calendar":

                    List<CalendarModel.Calendar> calendars = (from c in context.Calendars
                                                             select c).ToList();


                    //col = Knn.Calendar.Calendar.SelectCalendars();
                    foreach (CalendarModel.Calendar calendar in calendars)
                        DataList.Items.Add(new ListItem(calendar.CalName, calendar.CalId.ToString()));
                    break;
                case "Page":

                    List<SiteModel.CustomPage> pages = (from c in context.CustomPages
                                                        where c.DeletedFg == false
                                                     orderby c.Name
                                                     select c).ToList();
                    //col = Knn.Forum.Forum.SelectForums();
                    foreach (SiteModel.CustomPage page in pages)
                        DataList.Items.Add(new ListItem(page.Name, page.CustomPageId.ToString()));
                    break;
            }

            if (DataList.Items.Count > 0)
            {

                DataList.Items.Insert(0, new ListItem("-- Select -- ", "0"));
                dataRow.Visible = true;
            }
            else
            {
                //DataList.Items.Add(new ListItem("N/A", "0"));
                dataRow.Visible = false;
            }
        }

    }

    // 선택된 ContentType에서 적용 가능한 View Page의 목록을 표시한다.
    void bindContentView()
    {
        ViewList.Items.Clear();

        if (TypeList.SelectedIndex == 0)
        {
            ViewList.Items.Add(new ListItem("N/A", "0"));
            viewRow.Visible = false;
        }
        else
        {

            List<SiteModel.ContentView> contentviews = (from c in context.ContentViews
                                                        where c.ContentTypeId == int.Parse(TypeList.SelectedValue) && c.Enabled == true
                                                        select c).ToList();

            foreach (SiteModel.ContentView contentview in contentviews)
                ViewList.Items.Add(new ListItem(contentview.ContentViewName, contentview.ContentViewId.ToString()));

            ViewList.Items.Insert(0, (new ListItem("-- Select -- ", "0")));

            viewRow.Visible = true;
        }
    }

    void bindMenuInfo()
    {
        lk_config.Visible = lk_page.Visible = false;
        var result = (from c in context.SiteMenus
                      where c.MenuId == int.Parse(TreeView1.SelectedNode.Value) 
                      select c).Single();

        //SiteMenu menu = SiteMenu.SelectMenuById(int.Parse(TreeView1.SelectedNode.Value));
        MenuLabel.Text = result.Label;// menu.Label;
        Desc.Text = result.Description;// menu.Description;
        Submenu_image_url.Text = result.ExParam1;

        TypeList.SelectedValue = result.ContentView.ContentType.ContentTypeId.ToString();// menu.ContentType.ToString();
        bindContentData();
        bindContentView();

        if (int.Parse(result.Param) > 0)
        {
            DataList.SelectedValue = result.Param.ToString();// menu.Parameter.ToString();
        }

        if (ViewList.Items.IndexOf(ViewList.Items.FindByValue(result.ContentView.ContentViewId.ToString())) > -1)
            ViewList.SelectedValue = result.ContentView.ContentViewId.ToString();// menu.ViewId.ToString();

        Url.Text = result.Url;// menu.Url;
        TargetList.SelectedValue = result.Target;// menu.Target;

        ChkVisible.Checked = result.Visible;// menu.Visible;

        urlRow.Visible = (result.ContentView.ContentType.ContentTypeId == 0);// (menu.ContentType == 0);

        submenu_image.Visible = (result.ParentId == 0);

        if (result.ContentView.ContentType.ContentTypeName == "Article/News")
        {
            lk_config.Visible = lk_page.Visible = true;
            lk_config.PostBackUrl = "/cp/Article/ArticleCatConfig.aspx?cat=" + result.Param.ToString();
            lk_page.PostBackUrl = result.Url;
        }
    }

    void clearMenuInfo()
    {
        MenuLabel.Text = "";
        Desc.Text = "";

        TypeList.SelectedIndex = 0;
        bindContentData();
        bindContentView();

        Url.Text = "";
        TargetList.SelectedIndex = 0;

        ChkVisible.Checked = true;

        Submenu_image_url.Text = "";

        ErrMsg.Text = "";
    }

    void bindMenuTree()
    {
        TreeView1.Nodes.Clear();

        TreeNode node = new TreeNode("Root", "0");
        TreeView1.Nodes.Add(node);

        bindChildNode(node);

        TreeView1.CollapseAll();
        node.Expand();
        }

    void bindChildNode(TreeNode node)
    {


        List<SiteModel.SiteMenu> submenus = (from c in context.SiteMenus
                      where c.ParentId == int.Parse(node.Value)
                      orderby c.DispOrder
                      select c).ToList();


        foreach (SiteModel.SiteMenu submenu in submenus)
        {
            TreeNode n = new TreeNode(submenu.Label, submenu.MenuId.ToString());
            node.ChildNodes.Add(n);
            bindChildNode(n);
        }

    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (TreeView1.SelectedNode.Text.Equals("Root"))
        {
            menuInfo.Visible = false;

        }
        else
        {
            menuInfo.Visible = true;
            bindMenuInfo();
            enableControls(false);
        }

        if (int.Parse(TreeView1.SelectedValue.ToString()) >=0)
             EnableButtons("select");
    }

    protected void TypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        urlRow.Visible = (TypeList.SelectedItem.Value == "0");
        bindContentView();
        bindContentData();
    }


    void AddButtonEvent()
    {

            if (TreeView1.SelectedNode == null)
                return;

            clearMenuInfo();
            menuInfo.Visible = true;
            enableControls(true);

            EnableButtons("add");
        
    }

    private bool SaveValidation()
    {
        bool valid = true;
        string errorMsg = "";
        if (MenuLabel.Text.Length == 0)
        {
            errorMsg = "- Menu Label is required <br/>";
        }
        if (TypeList.SelectedIndex == 0 )
        {
            errorMsg = errorMsg+"- Content Type is required. <br/>";
        }

        if ((TypeList.SelectedIndex > 0) && (dataRow.Visible = true) && (DataList.SelectedIndex == 0))
        {
            errorMsg = errorMsg + "- Category is required. <br/>";
        }

        if (errorMsg.Length > 0)
        {
            valid = false;

            ErrMsg.Text = errorMsg;
        }

        return valid;
    }

    void EditButtonEvent()
    {
        if (TreeView1.SelectedNode == null)
            return;

        menuInfo.Visible = true;
        enableControls(true);
        bindMenuInfo();

        EnableButtons("edit");
    }

    void SaveButtonEvent()
    {
        if (SaveValidation())
        {

            string url = MakeURL(int.Parse(TypeList.SelectedValue), int.Parse(ViewList.SelectedValue));// context.ContentTypes.Single(c => c.ContentTypeId == )).BaseUrl + context.ContentViews.Single(c => c.ContentViewId == ).Page + "?p=" + DataList.SelectedValue;

            int order = (from c in context.SiteMenus
                         where c.ParentId == int.Parse(TreeView1.SelectedNode.Value)
                         select c.DispOrder).Max();


            SiteModel.SiteMenu siteMenu = new SiteModel.SiteMenu();

            siteMenu.ParentId = int.Parse(TreeView1.SelectedNode.Value);
            siteMenu.Label = MenuLabel.Text;
            siteMenu.Description = Desc.Text;
            siteMenu.Url = url;
            siteMenu.Param = DataList.SelectedIndex > 0? DataList.SelectedValue:"0";
            siteMenu.Target = TargetList.SelectedValue;
            siteMenu.Visible = ChkVisible.Checked;
            siteMenu.ContentViewId = int.Parse(ViewList.SelectedValue);
            siteMenu.DispOrder = order + 1;
            siteMenu.ExParam1 = Submenu_image_url.Text;
            siteMenu.ExParam2 = "";

            context.Add(siteMenu);
            context.SaveChanges();


            menuInfo.Visible = false;
            ErrMsg.Text = "";
            //SaveNewButton.Visible = false;

            toolbox1.DisableButton("save changes");


            TreeView1.SelectedNode.ChildNodes.Clear();
            bindChildNode(TreeView1.SelectedNode);


            bindMenuTree();
        }
    }

    void SaveChangesButtonEvent()
    {
        if (SaveValidation())
        {
            SiteModel.SiteMenu siteMenu = context.SiteMenus.Single(c => c.MenuId == int.Parse(TreeView1.SelectedNode.Value));

            siteMenu.Label = MenuLabel.Text;
            siteMenu.Description = Desc.Text;
            siteMenu.Url = MakeURL(int.Parse(TypeList.SelectedValue), int.Parse(ViewList.SelectedValue));
            siteMenu.Param = DataList.SelectedIndex > 0 ? DataList.SelectedValue : "0";
            siteMenu.Target = TargetList.SelectedValue;
            siteMenu.Visible = ChkVisible.Checked;
            siteMenu.ContentViewId = int.Parse(ViewList.SelectedValue);


            siteMenu.ExParam1 = Submenu_image_url.Text;
            //siteMenu.ExParam2 = "";

            context.SaveChanges();

            TreeView1.SelectedNode.Text = MenuLabel.Text;

            enableControls(false);

            EnableButtons("select");
            ErrMsg.Text = "";
        }
    }

    void DeleteButtonEvent()
    {
        if (context.SiteMenus.Count(c => c.ParentId == int.Parse(TreeView1.SelectedNode.Value)) > 0)
        {
            MsgBoxMpe.Show();
        }
        else
        {
            var siteMenu = (from c in context.SiteMenus
                            where c.MenuId == int.Parse(TreeView1.SelectedNode.Value)
                            select c).Single();
            context.Delete(siteMenu);
            context.SaveChanges();

            TreeNode p = TreeView1.SelectedNode.Parent;
            p.ChildNodes.Remove(TreeView1.SelectedNode);

            menuInfo.Visible = false;

        }
    }

    void MoveButtonEvent()
    {
        ConfirmMpe.Show();




        var result = from c in context.SiteMenus
                     where c.MenuId != int.Parse(TreeView1.SelectedNode.Value)
                     orderby c.DispOrder
                     select new
                     {
                         MenuName = c.Label,
                         MenuID = c.MenuId,
                         ParentMenuID = c.ParentId
                     };



        RadMenuTree.DataSource = result;
        RadMenuTree.DataBind();



        Telerik.Web.UI.RadTreeNode node = new Telerik.Web.UI.RadTreeNode("Root", "0");
        RadMenuTree.Nodes.Insert(0, node);

        RadMenuTree.CollapseAllNodes();

        menuInfo.Visible = false;
    }


    void CancelButtonEvent()
    {
        enableControls(false);

        clearMenuInfo();
        EnableButtons("select");

        menuInfo.Visible = false;

    }

    void UpButtonEvent()
    {
        TreeNode prevNode = getPrevNode(TreeView1.SelectedNode);

        if (prevNode == null)
            return;

        swapNodePosition(TreeView1.SelectedNode, prevNode);
        menuInfo.Visible = false;
    }

    void DownButtonEvent()
    {
        TreeNode nextNode = getNextNode(TreeView1.SelectedNode);

        if (nextNode == null)
            return;

        swapNodePosition(TreeView1.SelectedNode, nextNode);
        menuInfo.Visible = false;
    }


    void swapNodePosition(TreeNode node1, TreeNode node2)
    {

        SiteModel.SiteMenu menu1 = context.SiteMenus.Single(c => c.MenuId == int.Parse(node1.Value));
        SiteModel.SiteMenu menu2 = context.SiteMenus.Single(c => c.MenuId == int.Parse(node2.Value));

        int menu1Seq = menu1.DispOrder;
        int menu2Seq = menu2.DispOrder;


        menu1.DispOrder = menu2Seq;
        menu2.DispOrder = menu1Seq;

        context.SaveChanges();


        TreeNode parent = node1.Parent;

        ////////////////////////////////////
        //  Swap node data
        string tmp = node1.Value;
        node1.Value = node2.Value;
        node2.Value = tmp;

        tmp = node1.Text;
        node1.Text = node2.Text;
        node2.Text = tmp;

        node2.Select();

        //parent.ChildNodes.Clear();
        //bindChildNode(parent);

        //parent.Expand();
    }

    TreeNode getPrevNode(TreeNode node)
    {
        if (node == null)
            return null;

        if (node.Parent == null)
            return null;

        int i = node.Parent.ChildNodes.IndexOf(node);
        if (i == 0)
            return null;

        return node.Parent.ChildNodes[i - 1];
    }

    TreeNode getNextNode(TreeNode node)
    {
        if (node == null)
            return null;

        if (node.Parent == null)
            return null;

        int i = node.Parent.ChildNodes.IndexOf(node);
        if (i == node.Parent.ChildNodes.Count - 1)  // selected node is the bottom one.
            return null;

        return node.Parent.ChildNodes[i + 1];
    }


    void enableControls(bool enabled)
    {
        MenuLabel.Enabled = Desc.Enabled = TypeList.Enabled = DataList.Enabled = Submenu_image_url.Enabled = 
        ViewList.Enabled = Url.Enabled = TargetList.Enabled = ChkVisible.Enabled = enabled ;
    }

    private string MakeURL(int ContentTypeID, int ContentViewID)
    {
        string url_return = "";
        string contentTypeName = context.ContentTypes.Single(c => c.ContentTypeId == ContentTypeID).ContentTypeName;
        // Custom Page returns page name + .aspx
        if (contentTypeName == "Page")
        {
            url_return = context.ContentTypes.Single(c => c.ContentTypeId == ContentTypeID).BaseUrl+ context.CustomPages.Single(c=>c.CustomPageId == int.Parse(DataList.SelectedValue.ToString())).Name+".aspx";
        }
        else
        {
            if (ContentTypeID > 0)
                url_return = context.ContentTypes.Single(c => c.ContentTypeId == ContentTypeID).BaseUrl + context.ContentViews.Single(c => c.ContentViewId == ContentViewID).Page + "?p=" + DataList.SelectedValue;
            else
                url_return = Url.Text;
        }
        return url_return;
    }

    private void EnableButtons(string mode)
    {

        if (mode.ToLower() == "select")
        {

            if (int.Parse(TreeView1.SelectedNode.Value) > 0)
            {

                toolbox1.EnableButtons("add,up,down,move,delete,edit", true);
            }
            else if (int.Parse(TreeView1.SelectedNode.Value) == 0)
            {
                toolbox1.EnableButtons("add", true);
            }
            
            
        }
        else if (mode.ToLower() == "edit")
        {

            toolbox1.EnableButtons("up,down,delete,save changes,cancel", true);
        }
        else if (mode.ToLower() == "add")
        {

            toolbox1.EnableButtons("up,down,save,cancel", true);
        }

    }

    protected void btn_menuupdate_Click(object sender, EventArgs e)
    {
        if (RadMenuTree.SelectedNodes.Count > 0)
        {

            int selectedMenuID = int.Parse(TreeView1.SelectedNode.Value);
            int selectedMenuParentID = int.Parse(TreeView1.SelectedNode.Parent.Value);

            SiteModel.SiteMenu menu1 = context.SiteMenus.Single(c => c.MenuId == selectedMenuID);
            // get all node under this ID in the same level
            List<SiteModel.SiteMenu> menus = (from c in context.SiteMenus
                                              where c.ParentId == selectedMenuParentID && c.DispOrder > menu1.DispOrder
                                             select c).ToList();
            // And Update then DisplayOrder + 1
            foreach (SiteModel.SiteMenu sitemenu in menus)
            {
                sitemenu.DispOrder = sitemenu.DispOrder - 1;
            }

            // Get the latest display order in the new parent
            
            int NewParentMenuID = int.Parse(RadMenuTree.SelectedNode.Value);
            int noOfChild = context.SiteMenus.Where(c => c.ParentId == NewParentMenuID).Select(c=>c.DispOrder).Max();

            menu1.ParentId = NewParentMenuID;
            menu1.DispOrder = noOfChild + 1;

            context.SaveChanges();

            bindMenuTree();
        }
    }


    
}
