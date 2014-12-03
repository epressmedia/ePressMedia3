<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="delete.ascx.cs" Inherits="ePressMedia.Page.delete" %>
<div>
    <style>
        .delete_buttons
        {
            margin: 0 auto;
        }
    </style>
    <div style="padding:10px 0;">
    <asp:Label ID="lbl_msg" runat="server"></asp:Label>
        </div>
    <div style="padding-bottom: 10px;">
    <asp:TextBox ID="DelPassword" runat="server" TextMode="Password" MaxLength="8" />
        </div>
    <div class="delete_buttons">
    <asp:Button ID="ConfirmButton" runat="server" Text=" <%$ Resources: Resources, Gen.lbl_Delete %> " OnClick="ConfirmButton_Click"/>
        

    <asp:Button ID="CancelButton" runat="server" Text=" <%$ Resources: Resources, Gen.lbl_Cancel %> "  OnClick="CancelButton_Click"/>
    </div>
</div>