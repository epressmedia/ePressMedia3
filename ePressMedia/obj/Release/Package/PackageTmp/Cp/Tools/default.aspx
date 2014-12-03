<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ePressMedia.Cp.Tools._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<style>
    .tool_link a:hover
    {
        text-decoration:underline;
        font-weight:bold;
    }
</style>
<div style="margin:10px 0 0 10px" class="tool_link">
<asp:HyperLink ID = "HyperLink1" runat="server" NavigateUrl="/Cp/Tools/ThumbnailGenerator.aspx">Thumbnail Generator</asp:HyperLink><br />
<asp:HyperLink ID = "HyperLink2" runat="server" NavigateUrl="/Cp/Tools/EmailTester.aspx">Email Tester</asp:HyperLink><br />
<asp:HyperLink ID = "HyperLink3" runat="server" NavigateUrl="/Cp/Tools/SlugGenerator.aspx">Article URL Slug Generator</asp:HyperLink><br />
</div>
</asp:Content>

