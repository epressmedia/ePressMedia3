<%@ Control Language="C#" AutoEventWireup="true" Inherits="Article_ArticleListSummary" Codebehind="ArticleListSummary.ascx.cs" %>
<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>
<link href=<%= this.ResolveClientUrl("~/Styles/Article_List.css") %> rel="stylesheet" type="text/css" />

<asp:HiddenField ID="Params" runat="server" />
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
          <asp:Label ID="ArticleListSummary_CatName" runat="server" Text=""></asp:Label> <%# Eval("title")%></div>
      <div class="ArticleListSummary_Author"><%# Eval("Reporter") %> / <%# Eval("IssueDate", "{0:yyyy.MM.dd}") %></div>
      </a>
        
      </div>
    </ItemTemplate>
    <FooterTemplate></div><div class="ArticleListSummary_ListFooter"></div></FooterTemplate>
  </asp:Repeater>
  <div class="cntrPnl">
    <asp:Button ID="PostLink_popup" runat="server" Text="<%$ Resources: Resources, Article.lbl_Post %>" Visible="false" CssClass="boxLnk toRite"/>
    <asp:HyperLink ID="PostLink" runat="server" CssClass="boxLnk toRite" Text="Add" Visible="false"
      style="margin-right:8px;margin-top:8px;" />
    &nbsp;
    <uc1:FitPager ID="FitPager1" runat="server" RowsPerPage="10" OnPageNumberChanged="PageNumber_Changed" Visible="true" />
    &nbsp;
  </div>
  <div class="cntrPnl" id = "article_search_panel" style="display:none" runat="server">
    <asp:TextBox ID="SearchValue" runat="server" Width="240px" />
    <asp:Button ID="SearchButton" runat="server" Text=" <%$ Resources: Resources, Gen.lbl_Search %> " onclick="SearchButton_Click" CausesValidation="false" />
  </div>
  <div>
    <telerik:RadWindowManager ID="WindowManager" runat="server" ReloadOnShow="true" ShowContentDuringLoad="true"
        VisibleStatusbar="false" Behaviors="Close, Maximize" InitialBehaviors="None" VisibleOnPageLoad="false"
        DestroyOnClose="true" Modal="true" OnClientClose="OnClientclose" Width="980px"
        Height="800px" Animation="Resize" AutoSizeBehaviors="HeightProportional" AutoSize="True">
        <Windows>
            <telerik:RadWindow ID="ArticleEditor" runat="server" Behaviors="Close, Maximize" Modal="true"
                AutoSize="true" OnClientClose="OnClientclose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <script type="text/javascript">
        function showArticleEditor(url, name) {
            var bodyOverflow = "";
            var htmlOverflow = "";
            bodyOverflow = document.body.style.overflow;
            htmlOverflow = document.documentElement.style.overflow;
            //hide the overflow   
            document.body.style.overflow = "hidden";
            document.documentElement.style.overflow = "hidden";
            var oWndManager = $find("<%= WindowManager.ClientID %>");
            oWndManager.open(url, name);
        }
           
    </script>
</div>
