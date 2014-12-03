<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Mobile_Common_MobileBizSearch" Codebehind="MobileBizSearch.ascx.cs" %>
    <form id="bizsearch" runat="server">
    <asp:HiddenField ID="PageNumber" runat="server" />
    <asp:HiddenField ID="MaxPageNumber" runat="server" />
    <asp:HiddenField ID="QueryText" runat="server" />
            <ul data-role="listview" data-theme="g" data-inset="true">
            <li data-role="list-divider">
                <asp:Label ID="BizCount" runat="server" Text="Label"></asp:Label></li>
<asp:Repeater ID="BizRepeater" runat="server" onitemdatabound="BizRepeater_ItemDataBound">
    <HeaderTemplate>

    </HeaderTemplate>
    <ItemTemplate>
    <li>
        <a href='<%# Eval("BusinessEntityId", "Biz.aspx?bizId={0}") %>' rel="external">
            <h3>
                <asp:Literal ID="CatName" runat="server"></asp:Literal><asp:Literal ID="Name"
                    runat="server"></asp:Literal>
                </h3>
            <p>
                <asp:Literal ID="ShortDesc" runat="server"></asp:Literal>  </p>
            <p>
                <asp:Literal ID="BizPhone1" runat="server"></asp:Literal></p>
        </a>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

</ul>

      <fieldset class="ui-grid-a">
    <div class="ui-block-b" id="div_prev">
        <a id="btn_prev" href="<%= string.Concat("biz.aspx?q=",QueryText.Value,"&page=", (int.Parse(PageNumber.Value)-1)  ) %>"
            rel="external" data-role="button" data-icon="arrow-l">Prev</a></div>
    <div class="ui-block-b" id="div_next">
        <a id="btn_next" href="<%= string.Concat("biz.aspx?q=",QueryText.Value,"&page=", (int.Parse(PageNumber.Value)+1)  ) %>"
            rel="external" data-role="button" data-icon="arrow-r" data-iconpos="right">Next</a></div>
</fieldset>
</form>