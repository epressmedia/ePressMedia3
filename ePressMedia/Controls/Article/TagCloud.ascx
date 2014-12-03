<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagCloud.ascx.cs" Inherits="ePressMedia.Controls.Article.TagCloud" %>
<div class="TagCloud_Container">
       <div class="TagCloud_Header">
            <asp:Label ID="lbl_header" runat="server"></asp:Label>
        </div>
        <div class="TagCloud_Content">
<telerik:RadTagCloud ID="RadTagCloud1" runat="server" MaxNumberOfItems="40" DataTextField = "Tags" DataWeightField= "count"  
TakeTopWeightedItems="true" RenderItemWeight="false" >
</telerik:RadTagCloud>
</div>
</div>