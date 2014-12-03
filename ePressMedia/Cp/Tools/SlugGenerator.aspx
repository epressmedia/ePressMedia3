<%@ Page Language="C#" MasterPageFile="~/Cp/Master.master"  AutoEventWireup="true" CodeBehind="SlugGenerator.aspx.cs" Inherits="ePressMedia.Cp.Tools.SlugGenerator" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="margin:10px 0 0 10px">
    <h1>URL Slug Generator</h1>
    <h2>This tool will update the URL slug (friendly-URL) of all exsiting articles</h2><br />
        <asp:Button ID="btn_generate" runat="server" Text="Generate" OnClick="btn_generate_click" />
        </div>
</asp:Content>


