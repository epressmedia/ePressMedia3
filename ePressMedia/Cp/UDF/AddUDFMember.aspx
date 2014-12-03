<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUDFMember.aspx.cs" Inherits="ePressMedia.Cp.UDF.AddUDFMember" %>

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
        <div class="span_title"><asp:Label ID="lbl_udfname" runat="server"></asp:Label></div>
                       <div>UDF: <asp:DropDownList ID="ddl_udf" runat="server"></asp:DropDownList></div>
                         <div>Default Value: <asp:TextBox ID="txt_default_value" runat="server"></asp:TextBox> </div>
                   <div><asp:CheckBox ID="chk_required" runat="server" Text="Required"></asp:CheckBox> </div>
                   <div><asp:CheckBox ID="chk_search" runat="server" Text="Searchable"></asp:CheckBox> </div>
                   <div><asp:CheckBox ID="chk_active" runat="server" Text="Active"></asp:CheckBox> </div>

                         <div>Display Order: <telerik:RadNumericTextBox ID="txt_display_order" runat="server" MinValue="1" MaxLength="999" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox> </div>
                   <div style="float:right">
                       <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="blue_button" OnClick="btn_cancel_Click"/>
                       <asp:Button ID="btn_add" runat="server" Text="Add" CssClass="blue_button" OnClick="btn_add_Click" Visible="false"/></div>
    </div>
        </telerik:RadAjaxPanel>
    </div>
    </form>
</body>
</html>
