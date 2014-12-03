using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Admin;

namespace ePressMedia.Cp.Pages
{
    public partial class CatProperties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"]);
                CPage = CustomPageContoller.GetCustomPageById(id);
                PageName.Text = CPage.Name;
                txt_page_meta_descr.Text = CPage.MetaDescription;
                txt_page_title.Text = CPage.PageTitle;
            }

        }

        public static SiteModel.CustomPage CPage
        {
            get;
            set;
        }


        protected void btn_save_clicked(object sender, EventArgs e)
        {
            try
            {
                CustomPageContoller.UpdateCustomPage(CPage.CustomPageId,
                    CPage.Name,
                    CPage.Description,
                    txt_page_title.Text,
                    txt_page_meta_descr.Text,
                    CPage.DeletedFg);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Successfully updated.')", true);
                closeWindow();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert('Error: " + ex.Message + "')", true);
            }
        }

        private void closeWindow()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mykey", "CloseAndRebind();", true);
        }


        protected void btn_cancel_clicked(object sender, EventArgs e)
        {
            closeWindow();
        }

    }
}