using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Legacy.Common;


//

public partial class Cp_Ad_AddPopup : System.Web.UI.Page
{


    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        try
        {
            SiteModel.Popup popup = new SiteModel.Popup();
            popup.AdName = AdName.Text;
            popup.Description = Comment.Text;
            popup.AdOwner = AdOwner.Text;
            popup.Contact = Contact.Text;
            popup.SiteId = 1;
            popup.LinkUrl = "http://" + LinkUrl.Text;
            popup.LinkTarget = RadioLinkCur.Checked ? "_self" : "_blank";
            popup.ValidFrom = StartDate.SelectedDate.Value;
            popup.ValidTo = EndDate.SelectedDate.Value;
            popup.Enabled = RadioPublish.Checked;
            popup.Body = CkEditor.Text;
            popup.Width = int.Parse(HorSize.Text);
            popup.Height = int.Parse(VertSize.Text);

            context.Add(popup);
            context.SaveChanges();
            Utility.RegisterJsAlert(this, "Successfully Added");
            Response.Redirect("Popups.aspx");
        }
        catch (Exception ex)
        {
            Utility.RegisterJsAlert(this, "An error occured." + ex.Message);
        }
        //bool res = Popup.InsertPopup(0, //WebSite.GetSessionSiteId(),
        //    AdName.Text, Comment.Text, AdOwner.Text, Contact.Text, "http://" + LinkUrl.Text,
        //    RadioLinkCur.Checked ? "_self" : "_blank", DateTime.Parse(StartDate.Text),
        //    DateTime.Parse(EndDate.Text), RadioPublish.Checked, FCKeditor1.Value,
        //    int.Parse(HorSize.Text), int.Parse(VertSize.Text));

        //EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, res, "등록되었습니다", "등록하지 못했습니다", "Popups.aspx");
    }
}