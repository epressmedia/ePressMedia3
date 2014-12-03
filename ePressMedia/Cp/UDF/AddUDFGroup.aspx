<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUDFGroup.aspx.cs" Inherits="ePressMedia.Cp.UDF.AddUDFGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" lang="javascript">
        function GetRadWindow() {
            var oWindow = null; if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog                
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)              
            return oWindow;
        }

        function CloseDataEntry() {

            GetRadWindow().close();
        }

    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
  </telerik:RadScriptManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
    <div>
        <asp:Label ID="lbl_groupname" runat="server" Text="UDF Group Name:"></asp:Label>
        <asp:TextBox ID="txt_groupname" runat="server"></asp:TextBox>
    </div>
        <div>
        <asp:Label ID="lbl_description" runat="server" Text="Description:"></asp:Label>
        <asp:TextBox ID="txt_description" runat="server"></asp:TextBox>
    </div>
        <div>
        <asp:Label ID="Label2" runat="server" Text="No of Columns:"></asp:Label>
        <telerik:RadNumericTextBox ID="txt_noofcolumn" runat="server" MinValue="1" MaxLength="10" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
    </div>
        <div>
            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click"/>
        </div>
        </telerik:RadAjaxPanel>
    </div>
    </form>
</body>
</html>
