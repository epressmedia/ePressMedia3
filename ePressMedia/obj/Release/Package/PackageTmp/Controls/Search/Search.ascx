<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="ePressMedia.Controls.Search.Search" %>
<div class="Search_Container">
<div class="Search_srchPnl">
    <div class="Search_srchSec" id = "classified_srchSec" runat="server" visible = "false">
      <h2>생활정보 검색결과 (<asp:Literal ID="AdCount" runat="server" />)</h2>
      <asp:Repeater ID="AdRepeater" runat="server" 
        onitemdatabound="AdRepeater_ItemDataBound">
        <ItemTemplate>
          <div class="Search_srchRslt">
            <asp:HyperLink ID="ViewLink" runat="server" CssClass="Search_ttlLnk" Text='<%# Eval("Subject") %>' />
            <asp:Literal ID="Abstract" runat="server" Text='' />
          </div>
        </ItemTemplate>
      </asp:Repeater>
      <div class="Search_ritePnl" id = "AdLinkDiv" runat="server">
        <asp:HyperLink ID="AdLink" runat="server" Text="생활정보 검색결과 더 보기 &gt" />
      </div>
    </div>

    <div class="Search_srchSec" id = "article_srchSec" runat="server" visible = "false">
      <h2>기사 검색결과 (<asp:Literal ID="ArticleCount" runat="server" />)</h2>
      <asp:Repeater ID="ArticleRepeater" runat="server">
        <ItemTemplate>
          <div class="Search_srchRslt">
            <asp:HyperLink ID="ViewLink" runat="server" CssClass="Search_ttlLnk" 
              NavigateUrl='<%# "~/Article/view.aspx?p=" + Eval("CategoryId") + "&aid=" + Eval("ArticleId") %>' 
              Text='<%# Eval("Title") %>' />
            <asp:Literal ID="Literal1" Text='<%# (Eval("Abstract").ToString().Length >= 50) ? Eval("Abstract").ToString().Substring(0,50): Eval("Abstract")  %>' runat="server" />
          </div>
        </ItemTemplate>
      </asp:Repeater>
      <div class="Search_ritePnl" id = "ArtLinkDiv" runat="server">
        <asp:HyperLink ID="ArtLink" runat="server" Text="기사 검색결과 더 보기 &gt" Visible="false"/>
      </div>
    </div>
    <div class="Search_srchSec" id = "forum_srchSec" runat="server" visible = "false">
      <h2>게시판 검색결과 (<asp:Literal ID="ThreadCount" runat="server" />)</h2>
      <asp:Repeater ID="ForumRepeater" runat="server">
        <ItemTemplate>
          <div class="Search_srchRslt">
            <asp:HyperLink ID="ViewLink" runat="server" CssClass="Search_ttlLnk" 
              NavigateUrl='<%# "~/Forum/view.aspx?p=" + Eval("ForumId") + "&tid=" + Eval("ThreadId") %>' 
              ><%# Eval("Forum.ForumName", "[{0}] ")%><%# Eval("Subject") %></asp:HyperLink>
            <asp:Literal ID="Abstract" runat="server" />
          </div>
        </ItemTemplate>
      </asp:Repeater>
      <div class="Search_ritePnl" id="ForumLinkDiv" runat="server">
        <asp:HyperLink ID="ForumLink" runat="server" Text="게시판 검색결과 더 보기 &gt" />
      </div>
    </div>

    <div class="Search_srchSec" id = "biz_srchSec" runat="server" visible = "false">
      <h2>업소록 검색결과 (<asp:Literal ID="BizCount" runat="server" />)</h2>
      <asp:Repeater ID="BizRepeater" runat="server" onitemdatabound="BizRepeater_ItemDataBound">
        <ItemTemplate>
          <div class="Search_srchRslt">
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="Search_ttlLnk" Target="_blank" NavigateUrl='<%# Eval("BusinessEntityID", "/Biz/ViewBiz.aspx?id={0}") %>'>
<%--<asp:HyperLink ID="HyperLink1" runat="server" CssClass="Search_ttlLnk" Target="_blank" NavigateUrl='<%# Eval("BusinessEntityID", "http://www.bluebook114.com/default.aspx?id={0}") %>'>--%>
                <asp:Literal ID="lbl_catname" runat="server"></asp:Literal><%# Eval("PrimaryName") %>
            </asp:HyperLink>
            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("ShortDesc") %>' />
            <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("Phone1") %>' />
          </div>
        </ItemTemplate>
      </asp:Repeater>
      <div class="Search_ritePnl" id = "BizLinkDiv" runat="server">
        <asp:HyperLink ID="BizLink" runat="server" Target="_blank" Text="업소록 검색결과 더 보기 &gt" />
      </div>
    </div>
  </div>
  <div>&nbsp;</div>
  </div>

