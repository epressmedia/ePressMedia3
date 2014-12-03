using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model;

namespace ePressMedia.Cp.UDF
{
    public partial class UDFAttachment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadModules();
            }
        }

        public void LoadUDFGroups(string CurrentGropupId)
        {
            ddl_udfgroups.DataSource = EPM.Business.Model.UDF.UDFGroupController.GetAllUDFGroups();
                ddl_udfgroups.DataValueField = "UDFGroupId";
            ddl_udfgroups.DataTextField = "UDFGroupName";
            ddl_udfgroups.DataBind();

            ddl_udfgroups.Items.Insert(0, new ListItem("--Select--", "0"));

            if (!string.IsNullOrEmpty(CurrentGropupId))
            {
                ddl_udfgroups.SelectedIndex = ddl_udfgroups.Items.IndexOf(ddl_udfgroups.Items.FindByValue(CurrentGropupId));
                
            }
            
            
        }

        public void LoadModules()
        {
            EPM.Data.Model.EPMEntityModel _context = new EPM.Data.Model.EPMEntityModel();
            ddl_module.DataSource = _context.ContentTypes.Where(x => x.UDFAllowedFg == true);
            ddl_module.DataTextField = "ContentTypeName";
            ddl_module.DataValueField = "ContentTypeId";
            ddl_module.DataBind();

            ddl_module.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ddl_module_SelectedIndexChanged(object sender, EventArgs e)
        {

            btn_save.Visible = false;

            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

            lb_udfgroups.Items.Clear();

            ddl_category.DataSource = null;
            ddl_category.DataBind();
            ddl_category.Visible = false;
            category_panel.Visible = false;

            ddl_udfgroups.DataSource = null;
            ddl_udfgroups.DataBind();
            group_panel.Visible = false;
            

            if (ddl_module.SelectedIndex > 0)
            {
                if (context.ContentTypes.Where(x => x.ContentTypeId == int.Parse(ddl_module.SelectedItem.Value)).Single().CategoryFg)
                {
                    ddl_category.Visible = true;
                    category_panel.Visible = true;

                }
                else
                    group_panel.Visible = true;

                    switch (ddl_module.SelectedItem.Text.ToLower())
                    {
                        case "article":
                            LoadArticles();
                            break;
                        case "user":
                            LoadAttachment();
                            break;
                        case "form":
                            LoadForms();
                            break;
                        default:
                            ddl_category.DataSource = null;
                            ddl_category.DataBind();
                            break;
                    }
                
            }
        }

        public void LoadArticles()
        {
            
            ddl_category.DataSource = EPM.Business.Model.Article.ArticleCategoryContoller.GetAllActiveCategories();
            ddl_category.DataValueField = "ArtCatId";
            ddl_category.DataTextField = "CatName";
            ddl_category.DataBind();

            ddl_category.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void LoadForms()
        {
           
            ddl_category.DataSource = EPM.Business.Model.Form.FormController.GetForms();
            ddl_category.DataValueField = "FormId";
            ddl_category.DataTextField = "FormName";
            ddl_category.DataBind();

            ddl_category.Items.Insert(0, new ListItem("--Select--", "0"));
        }


        public void LoadAttachment()
        {
            //lb_udfgroups.DataSource = EPM.Business.Model.UDF.UDFGroupController.GetUDFGroupsByContentType(int.Parse(ddl_module.SelectedItem.Value), (ddl_category.Visible ? int.Parse(ddl_category.SelectedItem.Value) : 0));
            //lb_udfgroups.DataValueField = "UDFGroupId";
            //lb_udfgroups.DataTextField = "UDFGroupName";
            //lb_udfgroups.DataBind();


            int UDFGroupID = 0;
            if (EPM.Business.Model.UDF.UDFGroupController.GetUDFGroupsByContentType(int.Parse(ddl_module.SelectedItem.Value), (ddl_category.Visible ? int.Parse(ddl_category.SelectedItem.Value) : 0)).Count() > 0)
                UDFGroupID = EPM.Business.Model.UDF.UDFGroupController.GetUDFGroupsByContentType(int.Parse(ddl_module.SelectedItem.Value), (ddl_category.Visible ? int.Parse(ddl_category.SelectedItem.Value) : 0)).Single().UDFGroupId;
            LoadUDFGroups(UDFGroupID.ToString());

           
        }

        protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_category.SelectedIndex > 0)
            {
                LoadAttachment();
                group_panel.Visible = true;
            }
            else
            {
                group_panel.Visible = false;
                ddl_udfgroups.DataSource = null;
                ddl_udfgroups.DataBind();
            }

            
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            // open the list of UDF Groups
            string category_id = "";
            if ((ddl_category.Visible) & (ddl_category.SelectedIndex > 0))
                category_id = "&CategoryId=" + ddl_category.SelectedItem.Value.ToString();
            else
                category_id = "&CategoryId=0";
            EPM.Core.CP.PopupContoller.OpenWindow("/CP/UDF/AddUDFAttachment.aspx?ContentTypeId=" + ddl_module.SelectedItem.Value.ToString() + category_id, listing_div, "RefreshMemberList");
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "close")
            {
                LoadAttachment();
            }
        }

        protected void RadAjaxManager1_AjaxSettingCreated(object sender, Telerik.Web.UI.AjaxSettingCreatedEventArgs e)
        {

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                int category_id = 0;
                if ((group_panel.Visible) & (ddl_category.SelectedIndex > 0))
                    category_id = int.Parse(ddl_category.SelectedItem.Value.ToString());


                EPM.Business.Model.UDF.UDFAttachmentContoller.AttachUDFGroup(int.Parse(ddl_module.SelectedItem.Value.ToString()), category_id, int.Parse(ddl_udfgroups.SelectedItem.Value));
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Saved successfully');", true);
            }
            catch (Exception ex)
            {
            }
            
        }

        protected void ddl_udfgroups_SelectedIndexChanged(object sender, EventArgs e)
        {
        
                btn_save.Visible = ddl_udfgroups.SelectedIndex > -1;

        }
    }
}