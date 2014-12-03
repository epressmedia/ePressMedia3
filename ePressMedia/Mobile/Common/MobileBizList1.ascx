<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mobile_Common_MobileBizList1" Codebehind="MobileBizList1.ascx.cs" %>
<form id="bizlist" runat="server">
    <asp:HiddenField ID="PageNumber" runat="server" />
    <asp:HiddenField ID="MaxPageNumber" runat="server" />
    <asp:HiddenField ID="CatId" runat="server" />
    <asp:Repeater ID="BizRepeater" runat="server" >
      <HeaderTemplate>
      <ul data-role="listview"  data-theme="g" data-inset="true">
      </HeaderTemplate>
      <ItemTemplate>
      <li>

       <a href=<%# string.Concat("Biz.aspx?bizId=",Eval("BusinessEntityId")) %> rel="external">
<h3><%# Eval("PrimaryName")%> - <%# Eval("SecondaryName")%></h3>
      
      <p><%# Eval("ShortDesc")%></p>
      <p><%# Eval("Phone1")%></p>
      </a>



       </li>
      </ItemTemplate>
      <FooterTemplate>
      </ul>
      </FooterTemplate>
    </asp:Repeater>

          <fieldset class="ui-grid-a">
    <div class="ui-block-b" id="div_prev">
        <a id="btn_prev" href="<%= string.Concat("biz.aspx?catId=",CatId.Value,"&page=", (int.Parse(PageNumber.Value)-1)  ) %>"
            rel="external" data-role="button" data-icon="arrow-l">Prev</a></div>
    <div class="ui-block-b" id="div_next">
        <a id="btn_next" href="<%= string.Concat("biz.aspx?catId=",CatId.Value,"&page=", (int.Parse(PageNumber.Value)+1)  ) %>"
            rel="external" data-role="button" data-icon="arrow-r" data-iconpos="right">Next</a></div>
</fieldset>
</form>