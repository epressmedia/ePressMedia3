<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_RecentMultiThreads2"
    CodeBehind="RecentMultiThreads2.ascx.cs" %>

<div class="RecentMultiArticles2_container">
    <div class="RecentMultiArticles2_Header">
        <asp:Label ID="lbl_title" runat="server" Text=""></asp:Label>
        <img id = "img_title" runat="server" src="" alt="" visible="false"/>
        <a href="<%= MoreLink %>">더보기</a>
    </div>

    <ul class="RecentMultiArticles2_contents">
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:Image id="img_thumb" runat="server" alt='<%# Eval("Title") %>' Visible="false" />
                    <asp:Label ID="number_count" runat="server" Text=""></asp:Label>
                    <a id="art_link" runat="server">
                        <asp:Label ID="lbl_catName" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_contents" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </a>
                 </li>
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
    </ul>

</div>
