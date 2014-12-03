<%@ Control Language="C#" AutoEventWireup="true" Inherits="Article_ArticleList2" Codebehind="ArticleList2.ascx.cs" %>
<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>
<style>
    .ArticleListSummary_Line
{
	display: block;
	clear: left;
	font-size: 12px;
}

.ArticleListSummary_Line a div
{
	
	margin: 5px 10px 5px 10px;
}



.ArticleListSummary_Line a:hover
{
		
		text-decoration: underline;
}

.ArticleListSummary_Title
{
	font-weight: bold;
color: #010A6F;
width: 65%;
display: inline;
float: left;
overflow: hidden;
text-overflow: ellipsis;
white-space: nowrap;
}

.ArticleListSummary_Title span
{
	color: #343434;
}



.ArticleListSummary_Author
{
	font-weight: bold;
color: #8C8C8C;
font-size: 11px;
float:left;
display: inline;
}

.ArticleListSummary_List
{
	
border-bottom: 1px dotted #AAA;
padding-bottom: 40px
}


.ArticleListSummary_Headline
{
	margin: 10px 10px 10px 10px;
	border-bottom: 1px dotted #AAA;
	padding-bottom: 10px;
}

.ArticleListSummary_HeadlineTitle
{
	font-size: 25px;
font-weight: bold;
color: #010a6f;
margin-bottom: 10px;
overflow: hidden;
text-overflow: ellipsis;
white-space: nowrap;
}

.ArticleListSummary_HeadlineSubTitle
{
	font-size: 13px;
font-weight: bold;
color: #343434;
margin-bottom: 10px;
overflow: hidden;
text-overflow: ellipsis;
white-space: nowrap;
}


.ArticleListSummary_HeadlineAbstract
{
	color: #8c8c8c;
	font-weight: bold;
	line-height: 20px;
	
	
}

.ArticleListSummary_HeadlineReporter
{
	text-align: right;
		color: #8c8c8c;
	font-weight: bold;
}
</style>


<asp:HiddenField ID="CatId" runat="server" />
<div class="ArticleListSummary_Headline" runat="server" id = "ArticleListSummary_Headline">
<asp:HyperLink ID="ArticleListSummary_HeadlineLink" runat="server">
<div class="ArticleListSummary_HeadlineTitle">
    <asp:Label ID="ArticleListSummary_HeadlineTitle" runat="server" Text=""></asp:Label></div>
<div class="ArticleListSummary_HeadlineSubTitle"><asp:Label ID="ArticleListSummary_HeadlineSubTitle" runat="server" Text=""></asp:Label></div>
<div class="ArticleListSummary_HeadlineAbstract"><asp:Label ID="ArticleListSummary_HeadlineAbstract" runat="server" Text=""></asp:Label></div>
<div class="ArticleListSummary_HeadlineReporter"><asp:Label ID="ArticleListSummary_HeadlineReporter" runat="server" Text=""></asp:Label></div>
</asp:HyperLink>
</div>

  <asp:Repeater ID="ArticleListSummary_Repeater" runat="server" 
    onitemdatabound="ArticleListSummary_Repeater_ItemDataBound">
    <HeaderTemplate><div class="ArticleListSummary_List"></HeaderTemplate>
    <ItemTemplate>
      <div class="ArticleListSummary_Line"><a id ="ArticleListSummary" runat="server">
      <div class="ArticleListSummary_Title">
          <asp:Label ID="ArticleListSummary_CatName" runat="server" Text=""></asp:Label> <%# Eval("Title")%></div>
      <div class="ArticleListSummary_Author"><%# Eval("Reporter") %> / <%# Eval("IssueDate", "{0:yyyy.MM.dd}") %></div>
      </a>
        
      </div>
    </ItemTemplate>
    <FooterTemplate></div><div class="ArticleListSummary_ListFooter"></div></FooterTemplate>
  </asp:Repeater>
  <div class="cntrPnl">
    <asp:HyperLink ID="PostLink" runat="server" CssClass="boxLnk toRite" Text="Add a New Article" Visible="false"
      style="margin-right:8px;margin-top:8px;" />
    &nbsp;
    <uc1:FitPager ID="FitPager1" runat="server" RowsPerPage="10" OnPageNumberChanged="PageNumber_Changed" Visible="true" />
    &nbsp;
  </div>
  <div class="cntrPnl" id ="article_search_panel" runat="server" >
    <asp:TextBox ID="SearchValue" runat="server" Width="240px" />
    <asp:Button ID="SearchButton" runat="server" Text=" Search " onclick="SearchButton_Click" CausesValidation="false" />
  </div>