<%@ Control Language="C#" AutoEventWireup="true" Inherits="Biz_BizRankBox" Codebehind="BizRankBox.ascx.cs" %>

  
  <div class="rankBox">
    <p class="rankName">최다 조회 업소</p>
    <ul>
      <asp:Repeater ID="MostViewRepeater" runat="server" 
            onitemdatabound="MostViewRepeater_ItemDataBound">
        <ItemTemplate>
          <li>
          <asp:Image ID="IdxImg" runat="server" />&nbsp;
          <a href='/biz/viewbiz.aspx?id=<%#  Eval("BusinessEntityId") %>'><%#  Eval("PrimaryName")%></a> </li>
        </ItemTemplate>
      </asp:Repeater>
    </ul>
  </div>
  <div class="secClr"></div>
    <div class="rankBox">
    <p class="rankName">최근 등록 업소</p>
    <ul>
      <asp:Repeater ID="RecRepeater" runat="server" 
            onitemdatabound="RecRepeater_ItemDataBound">
        <ItemTemplate>
          <li>
          <asp:Image ID="IdxImg" runat="server" />&nbsp;
          <a href='/biz/viewbiz.aspx?id=<%#  Eval("BusinessEntityId")  %>'><%# Eval("PrimaryName")%></a> </li>
        </ItemTemplate>
      </asp:Repeater>
    </ul>
  </div>