using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;

namespace EPM.Core.CP
{
    public class PopupContoller
    {

        public static void OpenWindow(string NavigateURL, Control loadControl, string onClientCloseScript,int Width = 0, int Height = 0, WindowAnimation windowAnimation = WindowAnimation.None)
        {


            RadWindow windowpreview = new RadWindow();
            windowpreview.NavigateUrl = NavigateURL;
            windowpreview.VisibleOnPageLoad = true;
            windowpreview.Modal = true;
            windowpreview.Behaviors = Telerik.Web.UI.WindowBehaviors.Close;
            windowpreview.OpenerElementID = loadControl.ClientID;
            if (!string.IsNullOrEmpty(onClientCloseScript))
                windowpreview.OnClientClose = onClientCloseScript;
            windowpreview.ShowContentDuringLoad = false;
            windowpreview.Animation = windowAnimation;
            windowpreview.VisibleStatusbar = false;

            //windowpreview.MinWidth = 780;
            if ((Width > 0) || (Height > 0))
            {
                if ((Width == 0) && (Height > 0))
                {
                    windowpreview.MinWidth = System.Web.UI.WebControls.Unit.Pixel(800);
                    windowpreview.Height = System.Web.UI.WebControls.Unit.Pixel(Height);
                }
                else
                {
                    windowpreview.Width = System.Web.UI.WebControls.Unit.Pixel(Width);
                    windowpreview.Height = System.Web.UI.WebControls.Unit.Pixel(Height);
                }

            }
            else
            {
                windowpreview.MinWidth = System.Web.UI.WebControls.Unit.Pixel(800);
                windowpreview.AutoSize = true;
            }

            windowpreview.DestroyOnClose = true;    

            loadControl.Controls.Add(windowpreview);
        }
    }
}
