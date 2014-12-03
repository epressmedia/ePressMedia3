<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mobile_Common_MobileNewsMain1" Codebehind="MobileNewsMain1.ascx.cs" %>
<%--<asp:Panel runat="server" ID="ArticlePanel">
    <asp:HiddenField ID="ArticleCount" runat="server" />
    <asp:Repeater runat="server" ID="ArtRepeater1" OnItemDataBound="ArticleRepeater_ItemDataBound">
        <HeaderTemplate>
            <ul data-role="listview" data-theme="d" data-inset="true">
            <li data-role="list-divider"><asp:Label ID="listheader1" runat="server"></asp:Label></li>
        </HeaderTemplate>
        <ItemTemplate>
            <li data-icon="false">
                <div>
                    <div id="NewsLinkDiv">
                        <span id="newslink"><a href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                                rel="external">
                            <%# (Container.DataItem as Knn.Article.Article).GetShortTitle(100) %></a></span>
                    </div>
                    <div>
                        <div id="NewsImageDiv">
                            <a id="NewsImage" href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                                rel="external">
                                <asp:Image ID="Thumb" runat="server" Width="80" Height="60" />
                            </a>
                        </div>
                        <div Id="NewsArticleDiv">
                              <%# (Container.DataItem as Knn.Article.Article).GetShortAbstract(60)%>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>


    <asp:Repeater runat="server" ID="ArtRepeater2" OnItemDataBound="ArticleRepeater_ItemDataBound">
        <HeaderTemplate>
            <ul data-role="listview" data-theme="d" data-inset="true">
            <li data-role="list-divider"><asp:Label ID="listheader2" runat="server"></asp:Label></li>
        </HeaderTemplate>
        <ItemTemplate>
            <li data-icon="false">
                <div>
                    <div id="NewsLinkDiv">
                        <span id="newslink"><a href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                                rel="external">
                            <%# (Container.DataItem as Knn.Article.Article).GetShortTitle(100) %></a></span>
                    </div>
                    <div>
                        <div id="NewsImageDiv">
                            <a id="NewsImage" href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                                rel="external">
                                <asp:Image ID="Thumb" runat="server" Width="80" Height="60" />
                            </a>
                        </div>
                        <div Id="NewsArticleDiv">
                              <%# (Container.DataItem as Knn.Article.Article).GetShortAbstract(60)%>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>


       <asp:Repeater runat="server" ID="Repeater3" OnItemDataBound="ArticleRepeater_ItemDataBound">
        <HeaderTemplate>
            <ul data-role="listview" data-theme="d" data-inset="true">
            <li data-role="list-divider"><asp:Label ID="listheader3" runat="server"></asp:Label></li>
        </HeaderTemplate>
        <ItemTemplate>
            <li data-icon="false">
                <div>
                    <div id="NewsLinkDiv">
                        <span id="newslink"><a href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                                rel="external">
                            <%# (Container.DataItem as Knn.Article.Article).GetShortTitle(100) %></a></span>
                    </div>
                    <div>
                        <div id="NewsImageDiv">
                            <a id="NewsImage" href="<%# string.Concat("NewsDetail.aspx?aid=", Eval("ArticleId")) %>"
                                rel="external">
                                <asp:Image ID="Thumb" runat="server" Width="80" Height="60" />
                            </a>
                        </div>
                        <div Id="NewsArticleDiv">
                              <%# (Container.DataItem as Knn.Article.Article).GetShortAbstract(60)%>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>


</asp:Panel>--%>
