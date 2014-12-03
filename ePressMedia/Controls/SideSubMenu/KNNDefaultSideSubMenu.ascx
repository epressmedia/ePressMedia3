<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_SideSubMenu_KNNDefaultSideSubMenu"
    CodeBehind="KNNDefaultSideSubMenu.ascx.cs" %>
<div class="KNNDefaultSideSubMenu_Container">
    <div id="KNNDefaultSideSubMenu_Header" runat="server" class="KNNDefaultSideSubMenu_Header"
        visible="false">
        <asp:Label ID="lbl_header" runat="server"></asp:Label>
    </div>
    <div runat="server" id="leftMenu" class="leftMenu " visible="false">
        <asp:Image ID="MenuImage" runat="server" />
        <asp:Repeater runat="server" ID="SideRepeater" OnItemDataBound="SideMenu_ItemDataBound">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="MenuLink" runat="server" Text='<%# Eval("Label") %>' Target='<%# Eval("Target") %>'
                        NavigateUrl='<%# Eval("Url") %>' />
                    <asp:Repeater runat="server" ID="ChildRepeater" OnItemDataBound="SideChildMenu_ItemDataBound">
                        <ItemTemplate>
                            <li class="sub">
                                <asp:HyperLink ID="MenuLink" runat="server" Text='<%# Eval("Label") %>' Target='<%# Eval("Target") %>'
                                    NavigateUrl='<%# Eval("Url") %>' />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
        </asp:Repeater>

    </div>
</div>
