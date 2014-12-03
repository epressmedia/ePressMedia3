<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_LatestImage"
    CodeBehind="LatestImage.ascx.cs" %>
    <div class="LatestImage_container">
        <div class="LatestImage_Header">
        <a href="<%= MoreLink %>">
        <asp:Label ID="lbl_title" runat="server" Text=""></asp:Label>
        <img id = "img_title" runat="server" src="" alt="" visible="false"/>
        </a>
    </div>
<div class="LatestImage_itemcontainer">

    
    <asp:Label ID = "lbl_empty" CssClass="MultiContentSummary_Empty" Text = "No Content" runat="server" Visible="false"></asp:Label>
    <asp:hyperlink ID = "LatestImage_Img" runat="server" Width="100%"></asp:hyperlink>
        
    
</div>
</div>