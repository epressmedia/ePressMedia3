using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPM.Business.Model.Classified;

namespace ePressMedia.Cp.Classified
{
    public partial class CatProperties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            ClassifiedName.Text = ClassifiedController.GetClassifiedCategoryInfoById(id).CatName;

        }
        protected void TagList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName.Equals("del"))
            {

                int TagId = int.Parse(e.CommandArgument.ToString());
                DeleteTag(TagId);
                TagList.Rebind();
            }

        }

        void DeleteTag(int TagId)
        {
            ClassifiedTagController.DeleteTag(TagId);

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TagName.Text.Trim()))
            {
                ClassifiedTagController.AddTag(int.Parse(Request.QueryString["id"].ToString()), TagName.Text.Trim());
                TagList.Rebind();
            }
        }
    }
}