using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EPM.Business.Model.Admin;

namespace ePressMedia.Cp.Site
{
    public partial class EditEmailTemplate : System.Web.UI.Page
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["p"] != null)
                    LoadData(int.Parse(Request.QueryString["p"].ToString()));

            }
        }

        private void LoadData(int ee_id)
        {
            EmailModel.EmailEvent ee = EmailTemplateController.GetEmailTemplateById(ee_id);
            lbl_email_template_title.Text = ee.EmailEventName;
            txt_subject.Text = ee.Subject;
            html_editor_text.Content = ee.Body;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["p"] != null)
                {

                    EmailTemplateController.SaveEmailTemplate(int.Parse(Request.QueryString["p"].ToString()), txt_subject.Text, html_editor_text.Content, User.Identity.Name.ToString());
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "success", "alert('Successfully Saved.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "failed", "alert('Failed: " + ex.Message + ".');", true);
            }

        }
    }
}