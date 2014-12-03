<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_TrendRankBox" Codebehind="TrendRankBox.ascx.cs" %>

      <div class="TrendRankBox_Container">
          <%--<p class="title">
          주간 인기 검색어
          </p>--%>
          <div class="TrendRankBox_title" >
            <asp:Label id="lbl_searchrank" runat="server" Text = "" Visible="true"></asp:Label>
            <img id = "img_searchrank" runat="server" src="" alt="Search Rank" visible="false"/>
        </div>
        <div class="TrendRankBox_Box">
          <ul class="ttlList" style="position:relative;">
            <asp:Repeater ID="RankRepeater" runat="server" 
              onitemdatabound="RankRepeater_ItemDataBound">
              <ItemTemplate>

            <li>
              
               <img id="Img1" runat="server" src='' alt="" />
               <a id="SrchLink" runat="server" href='<%# "~/Search/Search.aspx?q=" + Server.UrlEncode(Eval("Query").ToString()) %>'><asp:Label ID ="lbl_text" runat="server" ref='<%# Eval("Index") %>' Text='<%# Eval("Query")%>' /></a>
               <span class="rnkLbl"><asp:Image id="RankImg" runat="server" /><asp:Label ID="RankDiff" runat="server" /></span>
            </li>
              
              </ItemTemplate>
            </asp:Repeater>
          </ul>
        </div>
      </div>