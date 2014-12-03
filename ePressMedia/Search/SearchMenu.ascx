<%@ Control Language="C#" AutoEventWireup="true" Inherits="Search_SearchMenu" Codebehind="SearchMenu.ascx.cs" %>
<div class="srchMenu">
  <ul>
    <li id="liSearch" runat="server">
      <span>&gt;</span>
      <asp:HyperLink ID="SearchLink" runat="server" Text="통합검색" />
    </li>
    <li id="liCls" runat="server">
      <span>&gt;</span>
      <asp:HyperLink ID="ClsLink" runat="server" Text="생활정보 검색" />
    </li>
    <li id="liBiz" runat="server">
      <span>&gt;</span>
      <asp:HyperLink ID="BizLink" runat="server"  Text="업소록 검색" />
    </li>
    <li id="liArt" runat="server">
      <span>&gt;</span>
      <asp:HyperLink ID="ArtLink" runat="server" Text="기사 검색" />
    </li>
    <li id="liForum" runat="server">
      <span>&gt;</span>
      <asp:HyperLink ID="ForumLink" runat="server" Text="게시판 검색" />
    </li>
  </ul>
</div>