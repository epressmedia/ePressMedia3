using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;




public partial class Cp_Ad_EditPopup : System.Web.UI.Page
{

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindData();
    }

    void bindData()
    {
        int id;
        try { id = Int32.Parse(Request.QueryString["id"]); }
        catch { return; }

        //Popup p = Popup.SelectPopupById(id);

        var p = (from c in context.Popups
                            where c.PopupId == id
                            select c).Single();
        

        PopupId.Text = p.PopupId.ToString();
        AdName.Text = p.AdName;
        Comment.Text = p.Description;
        AdOwner.Text = p.AdOwner;
        Contact.Text = p.Contact;

        LinkUrl.Text = p.LinkUrl;
        RadioLinkCur.Checked = p.LinkTarget.Equals("_self");
        RadioLinkNew.Checked = !RadioLinkCur.Checked;
        RadioPublish.Checked = p.Enabled;
        RadioPause.Checked = !RadioPublish.Checked;

        StartDate.SelectedDate = p.ValidFrom;
        EndDate.SelectedDate = p.ValidTo;
        CkEditor.Text = p.Body;

        

        HorSize.Text = p.Width.ToString();
        VertSize.Text = p.Height.ToString();

    }

    protected void AddButton_Click(object sender, EventArgs e)
    {

        try
        {
            SiteModel.Popup popup = context.Popups.Single(c => c.PopupId == int.Parse(PopupId.Text));
            popup.AdName = AdName.Text;
            popup.Description = Comment.Text;
            popup.AdOwner = AdOwner.Text;
            popup.Contact = Contact.Text;
            popup.LinkUrl = LinkUrl.Text;
            popup.LinkTarget = RadioLinkCur.Checked ? "_self" : "_blank";
            popup.ValidFrom = StartDate.SelectedDate.Value;
            popup.ValidTo = EndDate.SelectedDate.Value;
            popup.Enabled = RadioPublish.Checked;
            popup.Body = CkEditor.Text; 
            popup.Width = int.Parse(HorSize.Text);
            popup.Height = int.Parse(VertSize.Text);

            context.SaveChanges();

            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, true, "등록되었습니다", "등록하지 못했습니다", "Popups.aspx");
        }
        catch
        {
            EPM.Legacy.Common.Utility.RegisterJsResultAlert(this, false, "등록되었습니다", "등록하지 못했습니다", "Popups.aspx");
        }


    }
}