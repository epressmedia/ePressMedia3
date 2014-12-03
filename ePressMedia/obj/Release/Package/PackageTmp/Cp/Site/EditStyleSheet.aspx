<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditStyleSheet.aspx.cs" Inherits="ePressMedia.Cp.Site.EditStyleSheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Style Sheet</title>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server"  LoadingPanelID="RadAjaxLoadingPanel1">
        
        
        <div>
    <asp:TextBox ID="txt_css" runat="server" TextMode="MultiLine" Width="100%" 
            Height="500px" ViewStateMode="Enabled" ></asp:TextBox>
    </div>
    <div style="float:right">
    <asp:HiddenField ID = "lbl_mapPath" runat="server" Value="" />
    <asp:Button ID="btn_Save" runat="server" Text="Save" onclick="btn_Save_Click" />
    <asp:Button ID="btn_Close" runat="server" Text="Close"  OnClientClick="closeWin(); return false;"/>
    
            <script type="text/javascript">
                function closeWin() {
                    GetRadWindow().close();
                }
                function GetRadWindow() {
                    var oWindow = null; if (window.radWindow)
                        oWindow = window.radWindow; else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; return oWindow;
                }
        </script>
    </div>
    </telerik:RadAjaxPanel>
    </form>
</body>
</html>
