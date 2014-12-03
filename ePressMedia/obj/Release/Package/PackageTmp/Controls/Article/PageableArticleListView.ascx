<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageableArticleListView.ascx.cs"
    Inherits="ePressMedia.Controls.Article.PageableArticleListView" %>

<div id="PageableArticleListView_Container" runat="server" class="PageableArticleListView_Container">
    
            <div class="PageableArticleListView_Header">
                <asp:Label ID="lbl_header" runat="server"></asp:Label>
                <img id = "img_title" runat="server" src="" alt="" visible="false"/>
                <a href="#" runat="server" id="NextNavi"><img src="/img/next.png" alt="Next" /></a>
                <a href="#" runat="server" id="PrevNavi"><img src="/img/prev.png" alt="Previous" /></a>
              
            </div>
            <div class="PageableArticleListView_Contents">
            
                <asp:Repeater ID="Category_Repeater" runat="server" OnItemDataBound="Category_Repeater_ItemDataBound">
                <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                    <div class="CatCotainer">
                        <div class="ArtCat">
                            <asp:HyperLink ID="CatLink" runat="server">
                                <asp:Label ID="ArtCatName" runat="server" />
                            </asp:HyperLink>
                        </div>
                        <asp:Repeater ID="PageableArticleListView_Repeater" runat="server" OnItemDataBound="PageableArticleListView_Repeater_ItemDataBound">
                            <ItemTemplate>
                                <div class="ArtRotator">
                                    <div class="ArtContainer">
                                        <div class="ArtThumb" id="ArtThumb" runat="server">
                                            <asp:HyperLink ID="ViewImageLink" runat="server">
                                                <asp:Image ID="Thumb" runat="server" />
                                            </asp:HyperLink></div>
                                        <div class="ArtInf">
                                            <asp:HyperLink ID="ViewContentLink" runat="server">
            <div class="ArtDesc">
                <div class="Arttitle"><%# Eval("Title") %></div>
                </div>
                <div class="ArtAbstract">
                    <asp:Literal ID="lbl_abstract" runat="server"></asp:Literal>
                </div>
                                            </asp:HyperLink></div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate></FooterTemplate>
                </asp:Repeater>
            </div>
        
</div>
