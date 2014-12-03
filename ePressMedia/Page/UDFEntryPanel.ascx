<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UDFEntryPanel.ascx.cs" Inherits="ePressMedia.Pages.UDFEntryPanel" %>
<style>
.udf_group > span {
float: left;
margin: 5px;
}
.div_linebreak
{
    clear:both;
}
</style>
<div id="udf_group" class="udf_group" runat="server">
<asp:Repeater ID="udf_repeater" runat="server" 
        onitemdatabound="udf_repeater_ItemDataBound" ViewStateMode="Enabled">
<ItemTemplate>
<div id="div_linebreak" class="div_linebreak" runat="server" visible = "false"></div>
<epm:UDF ID="UDF" runat="server" ViewStateMode = "Enabled" />
</ItemTemplate>
</asp:Repeater>
<div style="clear:both">
</div>
</div>