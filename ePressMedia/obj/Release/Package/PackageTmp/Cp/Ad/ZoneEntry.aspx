<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZoneEntry.aspx.cs" Inherits="ePressMedia.Cp.Ad.ZoneEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-latest.js" type="text/javascript"></script>
        <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {

            GetRadWindow().close();
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function validateCombo(source, args) {
            if (args.Value == "-- Select --") {
                args.IsValid = false;
            } else {
                args.IsValid = true;
            }
        }

    </script>
    
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="UpdateLoadingPanel" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UpdatePanel" runat="server" LoadingPanelID="UpdateLoadingPanel">
        <div>
            <div class="gen_field">
                <asp:Label ID="Label9" runat="server" Text="Name" CssClass="title"></asp:Label>
                <telerik:RadTextBox ID="txt_name" runat="server" Width="90%">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="txt_name_validator" runat="server" CssClass="error" ErrorMessage="Name is required." ControlToValidate="txt_name" ValidationGroup="save"></asp:RequiredFieldValidator>
            </div>
            <div class="gen_field">
                <asp:Label ID="Label1" runat="server" Text="Description" CssClass = "title"></asp:Label>
                <telerik:RadTextBox ID="txt_description" runat="server" Width="90%">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="error" ErrorMessage="Description is required." ControlToValidate="txt_description" ValidationGroup="save"></asp:RequiredFieldValidator>
            </div>
            <div class="gen_field">
                <asp:Label ID="Label2" runat="server" Text="Action Type" CssClass="title"></asp:Label>
                <telerik:RadComboBox ID="cbo_actoin_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbo_actoin_type_SelectedIndexChanged" Width="90%">
                </telerik:RadComboBox>
                                            <asp:CustomValidator ID="CustomValidator1" ControlToValidate="cbo_actoin_type"
                runat="server" ValidateEmptyText="true" ClientValidationFunction="validateCombo"
                ValidationGroup="save" CssClass="error" ErrorMessage="Please select an action type">
            </asp:CustomValidator>
            </div>
            <div id="div_active" runat="server" class="gen_field" visible="false">
                <asp:Label ID="Label10" runat="server" Text="Active" CssClass="title"></asp:Label>
                <asp:CheckBox ID="chk_active" runat="server" />
            </div>
            <div style="clear:both"></div>
            <div id="search_panel" runat="server" class="adzone_search_panel" >
                <asp:Label ID="Label4" runat="server" Text="Banner Search" style="font-weight:bold"></asp:Label>
                <br />
                <telerik:RadComboBox ID="cbo_businessEntity" runat="server" DropDownAutoWidth="Enabled"
                    Height="150" EmptyMessage="Select"  EnableLoadOnDemand="True" 
                    ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true" Label="Advertiser Name"
                    OnSelectedIndexChanged="cbo_businessEntity_SelectedIndexChanged" OnItemsRequested="cbo_businessEntity_ItemsRequested">
                </telerik:RadComboBox>
                <telerik:OpenAccessLinqDataSource ID="BE_Source" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel"
                    EntityTypeName="" ResourceSetName="BusinessEntities" Select="new (BusinessEntityId, PrimaryName)">
                </telerik:OpenAccessLinqDataSource>
                <telerik:RadComboBox ID="cbo_banner_by_be" runat="server" Label="Banner Name">
                </telerik:RadComboBox>
                <asp:Button ID="btn_add" runat="server" OnClick="btn_add_Click" Text="Add Banner" />
            </div>

                
                <div class="banner_list">
                <telerik:RadListBox runat="server" ID="Multi_Banner_List" AllowReorder="True" AutoPostBack = "true"
                        Height="200px" Width="400px" AllowDelete="True" 
                        onselectedindexchanged="Multi_Banner_List_SelectedIndexChanged">
	<ButtonSettings Position="Right" HorizontalAlign="Center" />
</telerik:RadListBox>
<div style="padding:10px; width:200px; height:180px; border: 1px solid gray; background-color:#F5F5F5;">
<asp:Image ID="img_banner" runat="server" style="max-height:100%; max-width:100%;"/>
</div>
                </div>
                
                <div id="div_banner_weight" runat="server" visible="false" class="gen_field">
                <asp:Label ID="Label3" runat="server" Text="Apply Banner Weight" CssClass="title"></asp:Label>
                <asp:CheckBox ID="chk_weight" runat="server" />
            </div>
            <div style="clear:both">
            </div>



        </div>
    </telerik:RadAjaxPanel>
            <div style="float:right">
            <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" ValidationGroup="save"/>
        </div>
    </form>
</body>
</html>
