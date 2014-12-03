using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ePressMedia.Cp.Site
{
    public partial class EditStyleSheet : System.Web.UI.Page
    {

        EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["p"] != null)
                {
                    int ss_id = int.Parse(Request.QueryString["p"].ToString());

                    if (context.StyleSheets.Count(c => c.StyleSheetId == ss_id && c.SystemFg == false) > 0)
                    {


                        string path = context.StyleSheets.Single(c => c.StyleSheetId == ss_id).Name;
                        string mapPath = Server.MapPath(path);
                        lbl_mapPath.Value = mapPath;

                        StreamReader myStyle = new StreamReader(mapPath);
                        string myString = myStyle.ReadToEnd();

                        myStyle.Close();

                        txt_css.Text = myString;

                    }

                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {


            try
            {
                StreamWriter sw = new StreamWriter(lbl_mapPath.Value.ToString());
                sw.WriteLine(txt_css.Text);
                sw.Close();

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "success", "alert('Successfully Saved.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "failed", "alert('Failed: "+ex.Message+".');", true);
            }
            
        }
    }
}