<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdZoneControlEditor.ascx.cs" Inherits="ePressMedia.Admin.AdZoneControlEditor" %>

<div class="adzone_control_editor">
<div>
    <asp:Label ID="lbl_title" Text="" runat="server" class="editor_title" />
</div>

        <div>
            <div class="adzone_control_editor_field">
            <div>
                <asp:Label ID="lbl_adzone" runat="server" Text="Ad Zones"></asp:Label>
    <telerik:RadComboBox ID="ddl_adzonecontrols" runat="server" OnSelectedIndexChanged="ddl_adzonecontrols_ItemSelected"
        AutoPostBack="true">
    </telerik:RadComboBox>
    <asp:Label ID="lbl_adzone_error" runat="server" Text="Pelase select an Ad Zone" Visible = "false" Style="color:Red"></asp:Label>
            </div>
                <div>
                    <asp:Label ID="Label1" runat="server" Text="Style"></asp:Label>
                    <asp:TextBox ID="txt_control_container_style" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="Label3" runat="server" Text="Class"></asp:Label>
                    <asp:TextBox ID="txt_control_css_class" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div style="margin: 10px 0 20px 0;">
        <div style="float: left">
            <telerik:RadButton ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" OnClientClicked="OnClientDeleteClicked" Visible="false">
                <Icon PrimaryIconUrl="/img/trash.png" />
            </telerik:RadButton>
        </div>
        <div style="float: right; height: 22px;">
            <telerik:RadButton ID="btn_Cancel" runat="server" Text="Close" OnClick="btn_Cancel_Click">
                <Icon PrimaryIconCssClass="rbCancel" />
            </telerik:RadButton>
            <telerik:RadButton ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click"
                ValidationGroup="ControlAddValidation">
                <Icon PrimaryIconCssClass="rbSave" />
            </telerik:RadButton>
        </div>
        </div>

</div>
