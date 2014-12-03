using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;




public partial class Cp_Ad_Popups : System.Web.UI.Page
{

    EPM.Data.Model.EPMEntityModel context = new EPM.Data.Model.EPMEntityModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindData();
    }

    void bindData()
    {
        FitPager1.Visible = true;

        FitPager1.TotalRows = context.Popups.Count();// Popup.GetPopupCount();

        bindPage();
    }

    void bindPage()
    {
        //List<Popup> popups = Popup.SelectPopups(
        //            FitPager1.CurrentPage, FitPager1.RowsPerPage);


        List<SiteModel.Popup> popups = (from c in context.Popups
                                        select c).Skip((FitPager1.CurrentPage-1)*FitPager1.RowsPerPage).Take(FitPager1.RowsPerPage).ToList();

        DataView.DataSource = popups;
        DataView.DataBind();
    }

    protected void PageNumberChanged(object sender, EventArgs e)
    {
        bindPage();
    }

    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = DataView.Controls[0].FindControl("CheckAll") as CheckBox;
        bool isChecked = chk.Checked;

        foreach (RepeaterItem item in DataView.Items)
        {
            (item.FindControl("ItemCheck") as CheckBox).Checked = isChecked;
        }
    }


 

    protected void DataView_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());

        SiteModel.Popup p = context.Popups.Single(c => c.PopupId == id);

        //Popup p = Popup.SelectPopupById(id);

        MsgText.Text = p.Body;
        PopBox.Width = p.Width;
        PopBox.Height = p.Height;

        MsgBoxMpe.Show();
    }

}