﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_RecentMultiThreads"
    CodeBehind="RecentMultiThreads.ascx.cs" %>
    <div class="RecentMultiArticles_container">
        <div class="RecentMultiArticles_Header">
        <a href="<%= MoreLink %>">
        <asp:Label ID="lbl_title" runat="server" Text=""></asp:Label>
        <img id = "img_title" runat="server" src="" alt="" visible="false"/>
        </a>
    </div>
<div class="RecentMultiArticles_itemcontainer">

    <ul class="RecentMultiArticles_contents">
    <asp:Label ID = "lbl_empty" CssClass="MultiContentSummary_Empty" Text = "No Content" runat="server" Visible="false"></asp:Label>
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:Label ID="number_count" runat="server" Text=""></asp:Label>
                    <a id="art_link" runat="server">
                        <asp:Label ID="lbl_catName" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_contents" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </a></li>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:Label ID="number_count" runat="server" Text=""></asp:Label>
                    <a id="art_link" runat="server">
                        <asp:Label ID="lbl_catName" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_contents" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                    </a></li>
            </ItemTemplate>
        </asp:Repeater>
                <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:Label ID="number_count" runat="server" Text=""></asp:Label>
                    <a id="art_link" runat="server">
                        <asp:Label ID="lbl_catName" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_contents" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                    </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
</div>