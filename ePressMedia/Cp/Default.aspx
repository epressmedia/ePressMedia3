<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ePressMedia.Cp.Default" %>

<%@ Register src="~/CP/Controls/GA/Visitors.ascx" tagname="visitor" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <style>
.default_container
{
    display:block;
    position:absolute;
}
.default_container p
{
color: #9F9F9F;font-size: 20px;padding: 7px 0 10px 0; font-family:Arial
}
</style>
<div>
<p>ePressMedia Administration Panel</p>
<asp:Label ID="version" runat="server" Text=""></asp:Label>
</div>
<div>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" >
    <uc:visitor ID = "visitor" runat="server" />
    </telerik:RadAjaxPanel>
    
    

</div>
</asp:Content>
