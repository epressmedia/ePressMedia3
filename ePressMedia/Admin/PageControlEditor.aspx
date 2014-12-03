<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageControlEditor.aspx.cs" Inherits="ePressMedia.Pages.PageControlEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/PageBuilder.css" rel="stylesheet" type="text/css" />
</head>
<body class="ControlEditor_Body">
    <form id="form1" runat="server">
        <script type="text/javascript">
            function CloseAndRebind(args) {
                //GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();
            }
            function GetRadWindow() {
                var oWindow = null; if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog                
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)              
                return oWindow;
            }
            function CancelEdit() { GetRadWindow().close(); }

            function OnClientDeleteClicked(button, args) {
                if (window.confirm("Are you sure you want to delete this panel?")) {
                    button.set_autoPostBack(true);
                }
                else {
                    button.set_autoPostBack(false);
                }
            }
</script>


    <div>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
    <div>
    <asp:Label ID = "lbl_control_name" runat="server"></asp:Label>
    </div>
    <asp:PlaceHolder ID="ContentPlaceholder" runat="server"></asp:PlaceHolder>

    </div>
    </form>
</body>
</html>
