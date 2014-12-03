<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_1141.master" AutoEventWireup="true" Inherits="Search_ArticleSearch" Codebehind="ArticleSearch.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_1141.master" %>
<%@ Register src="../Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>
<%@ Register src="SearchMenu.ascx" tagname="SearchMenu" tagprefix="uc2" %>
<%@ Register src="~/Controls/Common/TrendRankBox.ascx" tagname="TrendRankBox" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Styles/Search.css" rel="stylesheet" type="text/css" />
  <script src="../Scripts/SearchMenu.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LeftBarContent" Runat="Server">
  <uc2:SearchMenu ID="SearchMenu1" runat="server" SelectedIndex="3" />
  <div class="secClr"></div>
  <uc3:TrendRankBox ID="TrendRankBox1" runat="server" NoOfHours="2" NoOfItems="5" HeaderName="최근 검색" />
  <div class="secClr"></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">

<div class="srchPnl">
  <div class="srchSec">
    <h2>기사 검색결과 (<asp:Literal ID="ArtCount" runat="server" />)</h2>
    <asp:Repeater ID="AdRepeater" runat="server" 
      onitemdatabound="AdRepeater_ItemDataBound">
      <ItemTemplate>
        <div class="srchRslt srchItem">
          <asp:Image ID="ThumbImg" runat="server" ImageUrl='<%#  Eval("Thumb_path") %>' Width="72" Height="54" />
          <div class="srchDesc">
            <asp:HyperLink ID="ViewLink" runat="server" CssClass="ttlLnk" Text='<%# Eval("Title") %>'
              NavigateUrl='<%# "~/Article/ViewArticle.aspx?p=" + Eval("CategoryId") + "&aid=" + Eval("ArticleId") %>' />
            <asp:Literal ID="Abstract" runat="server" Text='<%# (Eval("Abstract").ToString().Length >= 90) ? Eval("Abstract").ToString().Substring(0,90): Eval("Abstract") %>' />
          </div>
        </div>
      </ItemTemplate>
    </asp:Repeater>

      <uc1:FitPager ID="FitPager1" runat="server" RowsPerPage="10" OnPageNumberChanged="PageNumber_Changed" />
  </div>
</div>
<div class="secClr"></div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="RightBarContent" Runat="Server">
</asp:Content>

