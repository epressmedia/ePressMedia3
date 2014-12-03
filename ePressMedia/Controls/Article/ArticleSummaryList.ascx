<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Article_ArticleSummaryList"
    CodeBehind="ArticleSummaryList.ascx.cs" %>
<div class="ArticleSummaryList_Container">
<div>
    <asp:Image ID="HeaderImg" runat="server" Visible="false" />
</div>
<asp:Repeater ID="ArtRepeater_Headline" runat="server" OnItemDataBound="ArtRepeater_Headline_ItemDataBound">
    <ItemTemplate>
    
        <div class="ArticleSummaryList_art">
                <div class="ArticleSummaryList_desc">
            <asp:HyperLink ID="ViewLink" runat="server" Text=''>
            <div class="ArticleSummaryList_title"><%# Eval("Title") %></div>
            </asp:HyperLink>
        </div>
            <div class="ArticleSummaryList_thumb" id="ArticleSummaryList_thumb" runat="server">
                <asp:HyperLink ID="ViewImageLink" runat="server">
                    <asp:Image ID="Thumb" runat="server" />
                </asp:HyperLink>
            </div>
            <div class="ArticleSummaryList_artInf">
            <asp:HyperLink ID="ViewLink2" runat="server" Text=''>
            <div class="ArticleSummaryList_desc">
                <div class="ArticleSummaryList_subtitle"><%# Eval("SubTitle") %></div>
                </div>
                <div class="ArticleSummaryList_prv">
                    <asp:Literal ID="ltr_abstract" runat="server"></asp:Literal>
                    <asp:Label ID = "lbl_issuedate" runat="server"></asp:Label>
                </div>
                </asp:HyperLink>
            </div>
        </div>
        
    </ItemTemplate>
</asp:Repeater>
<asp:Repeater ID="ArtRepeater" runat="server" OnItemDataBound="ArtRepeater_ItemDataBound">
    <ItemTemplate>
        <div class="ArticleSummaryList_smallart">
            <img id="new_bullet" src="../../Img/hn4_ico_arrow03.gif" alt="" />
            <asp:HyperLink ID="ViewLink" runat="server" Text='<%# Eval("Title") %>' /><img id="img_new"
                runat="server" src="~/Img/inew.gif" alt="new" visible="false" />
        </div>
    </ItemTemplate>
</asp:Repeater>
</div>