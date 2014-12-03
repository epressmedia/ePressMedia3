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

namespace ePressMedia.Cp.Site
{
    public partial class Thumbnails : System.Web.UI.Page
    {
        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();

        private static string mode;
        public static string Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataLoad();
                LoadProcessType();
                toolbox1.EnableButtons("Add", true);
            }
            toolbox1.ToolBarClicked += new Telerik.Web.UI.RadToolBarEventHandler(toolbox1_ToolBarClicked); 
        }


        void LoadProcessType()
        {
            foreach (string name in System.Enum.GetNames(typeof(EPM.ImageLibrary.ThumbnailMethod)))
            {
                
                ddl_processtype.Items.Add(new RadComboBoxItem(name, name));
            }
        }

        void toolbox1_ToolBarClicked(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string action = e.Item.Text.ToLower();


            if (action == "edit")
            {
                Mode = "Edit";
                LoadSelectedThumbType();
                add_panel.Visible = true;
                
            }

            else if (action == "add")
            {
                Mode = "Add";
                toolbox1.DisableButton("Add");
                add_panel.Visible = true;
            }
   

        }

        private string getSelectItemID()
        {
            string selectedText = "";
            GridItemCollection gridRows = ThumbnailGrid.Items;
            foreach (GridDataItem data in gridRows)
            {
                if (data.Selected)
                {
                    selectedText = data.GetDataKeyValue("ThumbnailTypeID").ToString();
                    break;
                }
            }

            return selectedText;
        }

        private void dataLoad()
        {

            var thumbTypes = from c in context.ThumbnailTypes
                             where c.OriginalFg == false
                              orderby c.SystemFg, c.ThumbnailTypeId
                              select c;
            ThumbnailGrid.DataSource = thumbTypes;
            ThumbnailGrid.DataBind();

        }

        void LoadSelectedThumbType()
        {
            SiteModel.ThumbnailType thumbType = (from c in context.ThumbnailTypes
                                                 where c.ThumbnailTypeId == int.Parse(getSelectItemID())
                                                 select c).Single();
            txt_name.Text = thumbType.ThumbnailTypeName;
            txt_description.Text = thumbType.ThubmnailTypeDescription;
            txt_width.Text = thumbType.Width.ToString();
            txt_height.Text = thumbType.Height.ToString();
            ddl_processtype.SelectedIndex = ddl_processtype.Items.IndexOf(ddl_processtype.Items.FindItemByText(thumbType.ProcessType));


            if (thumbType.SystemFg)
            {
                txt_name.ReadOnly = true;
                txt_description.ReadOnly = true;
            }

            LoadSelectedThumbnailToSeesion(thumbType.Width, thumbType.Height, thumbType.ProcessType);
        }

        void LoadSelectedThumbnailToSeesion(int width, int height, string processtype)
        {
            Session["t_width"] = width;
            Session["t_height"] = height;
            Session["t_processtype"] = processtype;
                
        }

        private bool CheckThumbnailTypeChange(int width, int height, string processtype)
        {
            bool changed = false;
            if (Session["t_width"] == null || Session["t_height"] == null || Session["t_processtype"] == null)
            {
                changed = true;
            }
            else 
            {
                if (int.Parse(Session["t_width"].ToString()) != width || int.Parse(Session["t_height"].ToString()) != height || Session["t_processtype"].ToString() != processtype)
                {
                    changed = true;
                }
            }
            return changed;
        }

        private RadComboBoxItem FindByValue(RadComboBox radComboBox, object value)
        {
            foreach (RadComboBoxItem item in radComboBox.Items)
            {
                if (item.Value.Equals(value))
                {
                    return (RadComboBoxItem)item;
                }
            }

            return null;
        } 


        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            

            if (e.CommandName.Equals("Deactivate"))
            {
                int stylesheetid = int.Parse(e.CommandArgument.ToString());
                SiteModel.StyleSheet selectedSS = context.StyleSheets.Single(c => c.StyleSheetId == stylesheetid);
                selectedSS.Enabled = false;
                context.SaveChanges();
                dataLoad();
                
            }
            else if (e.CommandName.Equals("Activate"))
            {
                int stylesheetid = int.Parse(e.CommandArgument.ToString());
                SiteModel.StyleSheet selectedSS = context.StyleSheets.Single(c => c.StyleSheetId == stylesheetid);
                selectedSS.Enabled = true;
                context.SaveChanges();
                dataLoad();
            }
            else if (e.CommandName.Equals("del"))
            {
                int stylesheetid = int.Parse(e.CommandArgument.ToString());
                
                DeleteStyleSheet(stylesheetid);
                dataLoad();
            }
        }

        protected void DeleteStyleSheet(int stylesheetid)
        {
            SiteModel.StyleSheet selectedSS = context.StyleSheets.Single(c => c.StyleSheetId == stylesheetid);

            int selectedsequence = selectedSS.SequenceNo;

            List<SiteModel.StyleSheet> stylesheets = (from c in context.StyleSheets
                                                    where c.SequenceNo > selectedsequence && c.SystemFg == false
                                                    select c).ToList();

            foreach (SiteModel.StyleSheet stylesheet in stylesheets)
            {
                stylesheet.SequenceNo = stylesheet.SequenceNo-1;
                context.SaveChanges();
            }

            context.Delete(selectedSS);
            context.SaveChanges();

        }


        protected void ThumbnailGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fieldReset();
            toolbox1.EnableButtons("add", true);
            if (ThumbnailGrid.SelectedItems[0].ItemIndex > -1)
            {
                toolbox1.EnableButtons("edit", false);
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mode.ToLower() == "add")
                    AddThumbnail();
                else if (Mode.ToLower() == "edit")
                    EditThumbnail();



                fieldReset();
            }
            catch (Exception ex)
            {
                fieldReset();
            }
            
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            fieldReset();
        }

        void fieldReset()
        {
            toolbox1.EnableButtons("Add", true);
            toolbox1.EnableButtons("Edit", false);

            ThumbnailGrid.SelectedIndexes.Clear();

            txt_name.Text = txt_description.Text = txt_height.Text =  txt_width.Text =  txt_name.Text = "";
            add_panel.Visible = false;
            Mode = "";
        }



        void AddThumbnail()
        {

            if (context.ThumbnailTypes.Count(c => c.ThumbnailTypeName == txt_name.Text && c.SystemFg == false) == 0)
            {
                SiteModel.ThumbnailType thumbnailType = new SiteModel.ThumbnailType();
                thumbnailType.SystemFg = false;
                thumbnailType.DefaultFg = false;
                thumbnailType.ThumbnailTypeName = txt_name.Text;
                thumbnailType.ThubmnailTypeDescription = txt_description.Text;
                thumbnailType.ProcessType = ddl_processtype.SelectedItem.Text;
                thumbnailType.Width = int.Parse(txt_width.Text);
                thumbnailType.Height = int.Parse(txt_height.Text);
                thumbnailType.OriginalFg = false;
                context.Add(thumbnailType);
                context.SaveChanges();

                // if changes are made in width, height and process type.
                //if (CheckThumbnailTypeChange(int.Parse(txt_width.Text), int.Parse(txt_height.Text), ddl_processtype.SelectedItem.Text))
                    InvokeNewThumbnailGeneration(txt_width.Text);

                dataLoad();
            }

            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Thumbnail type name already exists.')", true);

        }

        void InvokeNewThumbnailGeneration(string ThumbnailTypeName )
        {
            try
            {
                List<ArticleModel.Article> articles = (from c in context.Articles
                                                       where c.Suspended == false
                                                       select c).ToList();


                foreach (ArticleModel.Article article in articles)
                {
                    string imageURL = "";
                    if (context.ArticleThumbnails.Count(c => c.ThumbnailType.OriginalFg == true && c.ArticleId == article.ArticleId) > 0)
                    {
                        imageURL = context.ArticleThumbnails.Single(c => c.ThumbnailType.OriginalFg == true && c.ArticleId == article.ArticleId).ThumbnailPath;
                    }

                    if (string.IsNullOrEmpty(imageURL))
                    {
                        List<string> imagestring = EPM.ImageUtil.EPMImageExtractUtil.GetImagesFromArticleBody(article.Body, true);
                        if (imagestring.Count > 0)
                            imageURL = imagestring[0];
                    }
                    if (!string.IsNullOrEmpty(imageURL))
                    {
                        try
                        {
                            EPM.Core.Article.Thumbnail.GenerateThumbnails(article.ArticleId, imageURL, ThumbnailTypeName);
                        }
                        catch
                        {
                        }

                            
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }



        void EditThumbnail()
        {
            SiteModel.ThumbnailType thumbnailType = context.ThumbnailTypes.Single(c => c.ThumbnailTypeId == int.Parse(getSelectItemID()));
            thumbnailType.ThumbnailTypeName = txt_name.Text;
            thumbnailType.ThubmnailTypeDescription = txt_description.Text;
            thumbnailType.ProcessType = ddl_processtype.SelectedItem.Text;
            thumbnailType.Width = int.Parse(txt_width.Text);
            thumbnailType.Height = int.Parse(txt_height.Text);
            context.SaveChanges();

            // if changes are made in width, height and process type.
            if (CheckThumbnailTypeChange(int.Parse(txt_width.Text), int.Parse(txt_height.Text), ddl_processtype.SelectedItem.Text))
                InvokeNewThumbnailGeneration(txt_name.Text);

            dataLoad();
        }

 

        
    }
}