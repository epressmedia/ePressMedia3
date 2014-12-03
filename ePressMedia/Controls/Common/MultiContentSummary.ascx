<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Common_MultiContentSummary"
    CodeBehind="MultiContentSummary.ascx.cs" %>
<div id="af_pannel" runat="server" class="MultiContentSummary_Container">
       <div class="MultiContentSummary_Header">
        <a id="HeadMoreLink" runat="server">
            <asp:Label ID="lbl_header" runat="server"></asp:Label></a>
        </div>
    <div class="MultiContentSummary_TabContents">
 
        <div style="clear: both">
        </div>
        <asp:Repeater ID="ContentRepeater" runat="server">
            <HeaderTemplate>
                <div id="newsTabs" class="MultiContentSummary_newsTabs">
            </HeaderTemplate>
            <ItemTemplate>
                <a class=" " href='#<%# Eval("Value")%>'>
                    <%#Eval("Value")%>
                </a>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Repeater ID="ContentItemRepeater" runat="server" OnItemDataBound="ContentItemRepeater_ItemDataBound">
            <HeaderTemplate>
                <div id="tabPnl" class="MultiContentSummary_tabPnl">
            </HeaderTemplate>
            <ItemTemplate>
                <ul id='<%# Eval("Value")%>'>
                    <asp:Repeater ID="ContentItemSubRepeater" runat="server" OnItemDataBound="ContentItemSubRepeater_ItemDataBound">
                        <ItemTemplate>
                            <li class="MultiContentSummary_ContentItem">
                            <asp:HyperLink ID = "itemLink" runat="server" target="_self">
                                <asp:Label ID="itemTitle" runat="server" Text=""></asp:Label>
                                </asp:HyperLink>
                                </li>
                                </ItemTemplate>
                    </asp:Repeater>
                    <div ID = "MultiContentSummary_More" class="MultiContentSummary_More" runat="server" visible = "false">
                <asp:HyperLink ID="MultiContentSummary_MoreLink" runat="server" CssClass="MultiContentSummary_MoreLink"><asp:Literal ID="lt_more" runat="server"></asp:Literal>
                </asp:HyperLink>
            </div>
                </ul>
                              
            </ItemTemplate>
            <FooterTemplate>
            
            <asp:Label ID="lbl_Empty" Text="no result" runat="server" Visible='<%#bool.Parse((ContentItemRepeater.Items.Count==0).ToString())%>'>

</asp:Label>
                </div></FooterTemplate>
        </asp:Repeater>
    </div>
    <div style="clear: both">
    </div>
</div>
