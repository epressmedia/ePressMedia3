<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewControl.aspx.cs" Inherits="ePressMedia.Admin.AddNewControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/PageBuilder.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="width:800px">
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
</script>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
    <div>

    </div>
    </form>
</body>
</html>
