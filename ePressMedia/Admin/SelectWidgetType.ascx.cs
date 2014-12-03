using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ePressMedia.Admin
{
    public partial class SelectWidgetType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Request.QueryString["area"].ToString();

            LoadWidgets();
        }
        void LoadWidgets()
        {
            List<string> files = GetFilesFromDictionary(EPM.Core.FileHelper.GetFiles(Server, "/Controls/" + Request.QueryString["area"].ToString(), "ascx", true));
            EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
            List<SiteModel.Widget> widgets = (from c in context.Widgets
                                              where files.Contains(c.File_path)
                                              select c).ToList();


            widget_repeater.DataSource = widgets;
            widget_repeater.DataBind();
        }

        private List<string> GetFilesFromDictionary(IDictionary<string, string> sources)
        {
            return sources.Select(x => "~"+x.Value).ToList();
        }

        protected void widget_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SiteModel.Widget widget = e.Item.DataItem as SiteModel.Widget;

                ((Label)e.Item.FindControl("lbl_name")).Text = widget.Widget_name;
                string widget_path = widget.File_path.Replace("~", "");
                ImageButton img_button = e.Item.FindControl("btn_button") as ImageButton;
                img_button.CommandName = "Add";
                img_button.CommandArgument = widget.Widget_id.ToString();
                if (EPM.Core.FileHelper.FileExists(Server.MapPath(widget_path.Replace("ascx", "png"))))
                {
                    img_button.ImageUrl = widget_path.Replace("ascx", "png");
                }
                else
                    img_button.ImageUrl = "/img/WidgetNoImage.png";
                
            }
        }

        protected void widget_repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                Response.Redirect("/Admin/AddNewControl.aspx?type=ascxcontroleditor&mode=add&src=" + e.CommandArgument);
            }
        }
    }
}