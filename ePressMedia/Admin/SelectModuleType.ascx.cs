using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ePressMedia.Admin
{
    public partial class SelectModuleType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadButtons();
        }

        void LoadButtons()
        {
            String[] folders = EPM.Core.FileHelper.GetDirectories(Server, "/Controls");
            foreach (string folder in folders)
            {
                Button button = new Button();
                string folder_name = EPM.Core.FileHelper.GetLastDirectoryInFullPath(folder);
                button.ID = folder_name;
                button.Text = folder_name;
                button.CssClass = "moduleSelection";
                button.Click += new System.EventHandler(ButtonClickHandler);
                Areas.Controls.Add(button);
            }
        }
        private void ButtonClickHandler(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Response.Redirect("/Admin/AddNewControl.aspx?type=SelectWidgetType&area=" + button.Text);
        }
    }
}