<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" CodeBehind="ImportUsers.aspx.cs" Inherits="ePressMedia.Cp.Tools.ImportUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="margin:10px 0 0 10px">
<h1>User Loading Tool</h1>
<h2>This tool will load users from the staging table news_member.</h2>
<br />
    <asp:Button ID="btn_import" runat="server" Text="Import Users" 
        onclick="btn_import_Click" />

        <asp:Label ID = "Label1" runat="server" Text=""></asp:Label><br />
        <asp:Label ID = "lbl_log" runat="server" Text=""></asp:Label>
        </div>
</asp:Content>
