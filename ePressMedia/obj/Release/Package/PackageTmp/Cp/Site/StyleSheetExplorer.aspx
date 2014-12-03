<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StyleSheetExplorer.aspx.cs" Inherits="ePressMedia.Cp.Site.StyleSheetExplorer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>File Explorer Dialog</title>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        //A function that will return a reference to the parent radWindow in case the page is loaded in a RadWindow object
        function getRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function OnClientFileOpen(sender, args) {// Called when a file is open.


            var item = args.get_item();

            //If file (and not a folder) is selected - call the OnFileSelected method on the parent page
            if (item.get_type() == Telerik.Web.UI.FileExplorerItemType.File) {
                // Cancel the default dialog;
                args.set_cancel(true);

                // get reference to the RadWindow
                var wnd = getRadWindow();

                //Get a reference to the opener parent page using RadWndow
                var openerPage = wnd.BrowserWindow;

                //if you need the URL for the item, use get_url() instead of get_path()
                openerPage.OnFileSelected(item.get_path()); // Call the method declared on the parent page


                //Close the window which hosts this page
                wnd.close();
            }
        }
    </script>
    <telerik:RadFileExplorer runat="server" ID="FileExplorer1" Width="530" Height="500"
        OnClientFileOpen="OnClientFileOpen">
        <Configuration ViewPaths="/Custom/Styles" SearchPatterns="*.css" UploadPaths = "/Custom/Styles" />
    </telerik:RadFileExplorer>
    </form>
</body>
</html>