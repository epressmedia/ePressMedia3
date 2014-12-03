<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchByModule.ascx.cs" Inherits="ePressMedia.Controls.Search.SearchByModule" %>
<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>

<div class="SearchByModule_Container">
<div class="Search_srchPnl">
  <div class="Search_srchSec">
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Search.lbl_SearchResult%>"></asp:Literal> (<asp:Literal ID="ItemCount" runat="server" />)</h2>
    <asp:Repeater ID="SearchItemRepeater" runat="server" 
      onitemdatabound="SearchItemRepeater_ItemDataBound">
      <ItemTemplate>
        <div class="Search_srchRslt Search_srchItem">
          <asp:Image ID="ThumbImg" runat="server" ImageUrl=""  />
          <div class="Search_srchDesc">
            <asp:HyperLink ID="ViewLink" runat="server" CssClass="Search_ttlLnk" Text=""
              NavigateUrl="" />
            <asp:Literal ID="Abstract" runat="server" Text="" />
          </div>
        </div>
      </ItemTemplate>
    </asp:Repeater>

      <uc1:FitPager ID="FitPager1" runat="server" RowsPerPage="10" OnPageNumberChanged="PageNumber_Changed" />
  </div>



 
</div>
</div>









