<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageControlEditor.ascx.cs"
    Inherits="ePressMedia.Page.ImageControlEditor" %>
<div class="image_control_editor">
    <div>
        <asp:Label ID="lbl_title" Text="" runat="server" class="editor_title" />
        <asp:Label ID="lbl_control_id" Text="" runat="server" Visible="false" />
    </div>
    <div class="image_control_editor_field">

       <div>
            <asp:Label ID="lbl_name" runat="server" Text="Image Name">
            </asp:Label>
            <asp:TextBox ID="txt_image_name" CssClass="txt_imagef_path" runat="server" Text=""></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lbl_image_path" runat="server" Text="Image Path">
            </asp:Label>
            <asp:TextBox ID="txt_image_path" CssClass="txt_image_path" runat="server" Text=""></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label3" runat="server" Text="Link Path(href)"></asp:Label>
            <asp:TextBox ID="txt_link_href" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label4" runat="server" Text="Link option(target)"></asp:Label>
            <asp:TextBox ID="txt_link_target" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Image Style"></asp:Label>
            <asp:TextBox ID="txt_image_style" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Control Container Style"></asp:Label>
            <asp:TextBox ID="txt_control_container_style" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label5" runat="server" Text="Class"></asp:Label>
            <asp:TextBox ID="txt_control_container_class" runat="server"></asp:TextBox>
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
