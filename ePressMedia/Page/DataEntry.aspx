<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataEntry.aspx.cs" Inherits="ePressMedia.Pages.DataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js"  type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery-ui.css">
</head>
<body>
    <form id="form1" runat="server">    
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All"
        EnableRoundedCorners="false" />
       <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function GetRadWindow() {
                var oWindow = null; if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog                
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)              
                return oWindow;
            }
            function CloseDataEntry() {

                GetRadWindow().close();
            }
            function CloseDataEntryWithArg(arg) {
                
                GetRadWindow().close(arg);
            }

            
        </script>
        </telerik:RadCodeBlock>
    <div id="DataEntry_Content" class="DataEntry_Content" runat="server">
    
    </div>
    </form>
</body>
</html>
