using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Description("Popup Ad")]
public partial class Controls_MainPopup : System.Web.UI.UserControl
{

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



        }
        MainLoad();
    }

    void MainLoad()
    {
        List<SiteModel.Popup> popups = (from c in context.Popups
                                        where c.Enabled == true && c.ValidFrom <= DateTime.Today && c.ValidTo >= DateTime.Today
                                        select c).Take(2).ToList();

        if (popups.Count() > 0)
        {
            if (Request.Cookies["pop" + popups[0].PopupId] == null)
            {
                PopupText1.Text = popups[0].Body;
                PopupLink1.NavigateUrl = popups[0].LinkUrl;
                PopupLink1.Target = popups[0].LinkTarget;
                PopupLink1.Visible = !string.IsNullOrEmpty(popups[0].LinkUrl);
                Pop1.Width = popups[0].Width;
                Pop1.Height = popups[0].Height;
                PopCheck1.Attributes["value"] = popups[0].PopupId.ToString();
            }
            else
            {
                Pop1.Visible = false;
            }
        }
        else
            Pop1.Visible = false;

        if (popups.Count() > 1)
        {
            if (Request.Cookies["pop" + popups[1].PopupId] == null)
            {
                PopupText2.Text = popups[1].Body;
                PopupLink2.NavigateUrl = popups[1].LinkUrl;
                PopupLink2.Target = popups[1].LinkTarget;
                PopupLink2.Visible = !string.IsNullOrEmpty(popups[1].LinkUrl);
                Pop2.Attributes["style"] += "left: " + (popups[0].Width + 100) + "px";
                Pop2.Width = popups[1].Width;
                Pop2.Height = popups[1].Height;
                PopCheck2.Attributes["value"] = popups[1].PopupId.ToString();
            }
            else
            {
                Pop2.Visible = false;
            }
        }
        else
            Pop2.Visible = false;
    }
}