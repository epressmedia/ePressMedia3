<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="EmailTester.aspx.cs" Inherits="ePressMedia.Cp.Tools.EmailTester" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<style>
    .EmailInfo
    {
        display:block;
    }
    .EmailValue
    {
       margin: 5px 0 5px 5px;
       font-weight: bold;
       font-size:12px;
    }
</style>
<div>
<div>
<asp:Label ID="lbl_from" runat="server" Text="From" CssClass="EmailInfo"></asp:Label>
<asp:Label ID="txt_from" runat="server" Text="" CssClass="EmailValue"></asp:Label>
</div>
<div>
<asp:Label ID="lbl_host" runat="server" Text="Host" CssClass="EmailInfo"></asp:Label>
<asp:Label ID="txt_host" runat="server" Text="" CssClass="EmailValue"></asp:Label>
</div>
<div>
<asp:Label ID="lbl_port" runat="server" Text="Port" CssClass="EmailInfo"></asp:Label>
<asp:Label ID="txt_port" runat="server" Text="" CssClass="EmailValue"></asp:Label>
</div>
<div>
<asp:Label ID="Label7" runat="server" Text="To" CssClass="EmailInfo"></asp:Label>
    <asp:TextBox ID="txt_To" runat="server"></asp:TextBox>
</div>
<div>
<asp:Label ID="Label2" runat="server" Text="Subject" CssClass="EmailInfo"></asp:Label>
    <asp:TextBox ID="txt_subject" runat="server" Text="Test Email"></asp:TextBox>
</div>
<div>
<asp:Label ID="Label1" runat="server" Text="Body" CssClass="EmailInfo"></asp:Label>
    <asp:TextBox ID="txt_body" runat="server" Text="This is test email."></asp:TextBox>
</div>
<div>
    <asp:Button ID="btn_send" runat="server" Text="Send" onclick="btn_send_Click" />
    <asp:Label ID="txt_result" runat="server" Text="" ></asp:Label>
</div>

</div>
</asp:Content>
