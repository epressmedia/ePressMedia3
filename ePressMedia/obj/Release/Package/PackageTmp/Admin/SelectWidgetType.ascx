<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectWidgetType.ascx.cs" Inherits="ePressMedia.Admin.SelectWidgetType" %>
<style>
    .widget_item
    {
        float:left;
        margin:10px;
        text-align: center;
    }
    .widget_item input
    {
        display:block;
    }
    

</style>
<div>
<asp:Repeater ID="widget_repeater" runat="server" 
        onitemdatabound="widget_repeater_ItemDataBound" 
        onitemcommand="widget_repeater_ItemCommand">
<ItemTemplate>
<div class="widget_item">
<asp:ImageButton ID="btn_button" runat="server"/>
<asp:Label ID="lbl_name" runat="server"></asp:Label>
</div>
</ItemTemplate>
</asp:Repeater>
<div style="clear:both"></div>
</div>
<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
