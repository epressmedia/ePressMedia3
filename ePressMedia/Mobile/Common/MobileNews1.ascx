<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Mobile_Common_MobileNews1" Codebehind="MobileNews1.ascx.cs" %>
<asp:Panel runat="server" ID="ArticlePanel">
    <asp:HiddenField ID="ArticleCount" runat="server" />
    <asp:HiddenField ID="PageNumber" runat="server" />
    <asp:HiddenField ID="MaxPageNumber" runat="server" />
    <asp:HiddenField ID="CatId" runat="server" />
    <fieldset class="ui-grid-a">
    <div class="ui-block-b ddl_other">
        <asp:Repeater runat="server" ID="NewsCatRepeator">
            <HeaderTemplate>
                <select name="NewsCatSelector" id="NewsCatSelector">
                    <option value="-1">다른 뉴스 보기...</option>
            </HeaderTemplate>
            <ItemTemplate>
                <option value='<%# Eval("MenuId") %>'>
                    <%#Eval("Label") %></option>
            </ItemTemplate>
            <FooterTemplate>
                </select>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</fieldset>
    <asp:Repeater runat="server" ID="ArtRepeater1" OnItemDataBound="ArticleRepeater_ItemDataBound">
        <HeaderTemplate>
            <ul data-role="listview" data-theme="d" data-inset="true" class="knn_list">
                <li data-role="list-divider">
                    <asp:Label ID="listheader1" runat="server"></asp:Label></li>
        </HeaderTemplate>
        <ItemTemplate>
            <li data-icon="false">
                <div>
                    <div id="NewsLinkDiv">
                        <span id="newslink"><a href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                            rel="external">
                            <%#   (Eval("Title").ToString().Length >= 100) ? Eval("Title").ToString().Substring(0,100): Eval("Title") %></a></span>
                    </div>
                    <div>
                        <div id="NewsImageDiv" runat="server">
                            <a id="NewsImage" href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                                rel="external">
                                <asp:Image ID="Thumb" runat="server" CssClass="NewsThumb" />
                            </a>
                        </div>
                        <div id="NewsArticleDiv">
                            <%# (Eval("Abstract").ToString().Length >= 100) ? Eval("Abstract").ToString().Substring(0, 100) : Eval("Abstract")%>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>
<fieldset class="ui-grid-a">
    <div class="ui-block-b" id="div_prev">
        <a id="btn_prev" href="<%= string.Concat("news.aspx?CatId=",CatId.Value,"&page=", (int.Parse(PageNumber.Value)-1)  ) %>"
            rel="external" data-role="button" data-icon="arrow-l">Prev</a></div>
    <div class="ui-block-b" id="div_next">
        <a id="btn_next" href="<%= string.Concat("news.aspx?CatId=",CatId.Value,"&page=", (int.Parse(PageNumber.Value)+1)  ) %>"
            rel="external" data-role="button" data-icon="arrow-r" data-iconpos="right">Next</a></div>
</fieldset>

