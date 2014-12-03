using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace EPM.Core.CP
{
    public class ToolBarController : System.Web.UI.UserControl
    {


        private string buttonAvailable;
        public string ButtonAvailable
        {
            get { return buttonAvailable; }
            set { buttonAvailable = value; }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {

                RadToolBar RadToolBar1 = this.FindControl("RadToolBar1") as RadToolBar;
                string[] buttons = ButtonAvailable.Split(',').ToArray();
                foreach (string button in buttons)
                {
                    try
                    {
                        RadToolBarItem item = RadToolBar1.Items.FindItemByText(button, true);
                        if (item != null)
                            item.Visible = true;
                        else
                        {
                            RadToolBarButton newitem = new RadToolBarButton(button);
                            newitem.Visible = true;
                            RadToolBar1.Items.Add(newitem);
                        }


                    }
                    catch
                    {
                        break;
                    }
                }
            }


        }

        public void EnableButtons(string controlnames, bool resetFist)
        {
            RadToolBar RadToolBar1 = (RadToolBar)this.FindControl("RadToolBar1");
            if (resetFist)
            {


                foreach (RadToolBarItem button in RadToolBar1.Items)
                {
                    button.Enabled = false;
                }
            }

            string[] controlCollection = controlnames.Split(',').ToArray();

            if (controlCollection.Count() > 0)
            {
                foreach (string buttonname in controlCollection)
                {

                    RadToolBarItem baritem = RadToolBar1.Items.FindItemByText(buttonname, true);
                    if (baritem != null)
                        baritem.Enabled = true;
                }
            }
        }

        public void DisableButton(string controlname)
        {

            RadToolBar RadToolBar1 = (RadToolBar)this.FindControl("RadToolBar1");


                    RadToolBarItem baritem = RadToolBar1.Items.FindItemByText(controlname, true);
                    if (baritem != null)
                        baritem.Enabled = false;
      
        }

        public void SetConfirm(string controlname, string comfirmMassage)
        {
              RadToolBar RadToolBar1 = (RadToolBar)this.FindControl("RadToolBar1");


                    RadToolBarItem baritem = RadToolBar1.Items.FindItemByText(controlname, true);
                    if (baritem != null)
                        baritem.Attributes.Add("OnClientClick", "alert('" + comfirmMassage + "');");
        }

        public void SetValidationGroupButtons(string controlNames, string validationGroupName, bool resetFirst)
        {
            RadToolBar RadToolBar1 = (RadToolBar)this.FindControl("RadToolBar1");

            if (resetFirst)
            {

                foreach (RadToolBarButton button in RadToolBar1.Items)
                {
                    button.ValidationGroup = "";
                }
            }


            string[] controlCollection = controlNames.Split(',').ToArray();

            if (controlCollection.Count() > 0)
            {
                foreach (string buttonname in controlCollection)
                {

                    RadToolBarButton baritem = (RadToolBar1.Items.FindItemByText(buttonname, true)) as RadToolBarButton;
                    if (baritem != null)
                        baritem.ValidationGroup = validationGroupName;
                }
            }
        }

        public void SetViewStateModeGroupButtons(string controlNames, ViewStateMode stateMode)
        {
            RadToolBar RadToolBar1 = (RadToolBar)this.FindControl("RadToolBar1");
            string[] controlCollection = controlNames.Split(',').ToArray();
            if (controlCollection.Count() > 0)
            {
                foreach (string buttonname in controlCollection)
                {

                    RadToolBarItem baritem = RadToolBar1.Items.FindItemByText(buttonname, true);
                    if (baritem != null)
                        baritem.ViewStateMode = stateMode;
                }
            }
        }
    }
}

