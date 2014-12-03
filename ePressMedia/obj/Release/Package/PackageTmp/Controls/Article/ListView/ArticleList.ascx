<%@ Control Language="C#" AutoEventWireup="true" Inherits="Article_ArticleList" CodeBehind="ArticleList.ascx.cs" %>
<%@ Register Src="~/Controls/Pager/FitPager.ascx" TagName="FitPager" TagPrefix="uc1" %>
<script type="text/javascript" src="/scripts/jquery.prettyPhoto.js"></script>
<link rel="stylesheet" type="text/css" href="/styles/prettyPhoto.css" media="screen" />
<asp:HiddenField ID="CatId" runat="server" />

<div class="cntrPnl">
    <asp:Button ID="PostLink_popup2" runat="server" Text="<%$ Resources: Resources, Article.lbl_Post %>" Visible="false" CssClass="boxLnk toRite"/>
</div>

<div class="ArticleList_Headlineart" id="ArticleList_Headline" runat="server">
    <asp:Repeater ID="ArtRepeater_Headline" runat="server" OnItemDataBound="ArtRepeater_Headline_ItemDataBound">
        <ItemTemplate>
            <div class="ArticleList_Headart" id="ArticleList_Headart" runat="server">
                <div class="ArticleList_Headlinethumb">
                    <asp:HyperLink ID="ArticleList_HeadlineLinkImage" runat="server">
                        <asp:Image ID="ArticleList_TitleThumb" runat="server" />
                    </asp:HyperLink>
                </div>
                <div class="ArticleList_HeadlineartInf">
                    <asp:HyperLink ID="ArticleList_HeadlineLink" runat="server">
                        <div class="ArticleList_HeadlineTitle">
                            <asp:Label ID="ArticleList_HeadlineTitle" runat="server" Text=""></asp:Label>
                            <img id="Img1" runat="server" src="~/Img/inew.gif" alt="" visible="false" />
                        </div>
                        <div class="ArticleList_HeadlineSubTitle">
                            <asp:Label ID="ArticleList_HeadlineSubTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </div>
                        <div class="ArticleList_HeadlineAbstract">
                            <asp:Label ID="ArticleList_HeadlineAbstract" runat="server" Text=""></asp:Label>
                        </div>
                    </asp:HyperLink>
                    <div class="ArticleList_HeadlineReporter">
                        <div>
                            <asp:Label ID="ArticleList_HeadlineReporter" runat="server" Text='<%# Eval("Reporter") %>'></asp:Label></div>
                        <div>
                            <asp:Label ID="ArticleList_HeadlinePostDate" runat="server" Text='<%# Eval("IssueDate", "{0:yyyy/MM/dd}") %>'></asp:Label></div>
                        <div>
                            <asp:Label ID="ArticleList_HeadlineHit" runat="server" Text='<%# Eval("ViewCount") %>'></asp:Label></div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<asp:Repeater ID="ArtRepeater" runat="server" OnItemDataBound="ArtRepeater_ItemDataBound">
    <ItemTemplate>
        <div class="ArticleList_art" id="ArticleList_art" runat="server">
            <div id="thumb_div" class="ArticleList_thumb" runat="server">
                <asp:HyperLink ID="ViewImageLink" runat="server">
                    <asp:Image ID="Thumb" runat="server" />
                </asp:HyperLink>
            </div>
            <div class="ArticleList_artInf">
                <div class="ArticleList_desc">
                    <asp:HyperLink ID="ViewLink" runat="server" Text='<%# Eval("Title") %>' />
                    <asp:Label ID="CommCount" CssClass="comCnt" runat="server" />
                    <img id="img_new" runat="server" src="~/Img/inew.gif" alt="" visible="false" />
                </div>
                <div class="ArticleList_desc">
                    <div class="ArticleList_poster">
                        <%# Eval("Reporter") %></div>
                    Date:
                    <%# Eval("IssueDate", "{0:yyyy/MM/dd}")%>&nbsp;/&nbsp;Views:
                    <%# Eval("ViewCount") %>
                </div>
                <div class="prv">
                    <asp:HyperLink ID="Article_Abstract" runat="server" Text="" />
                </div>
            </div>
        </div>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <div class="ArticleList_art ArticleList_art_alt" id="ArticleList_art" runat="server">
            <div id="thumb_div" class="ArticleList_thumb" runat="server">
                <asp:HyperLink ID="ViewImageLink" runat="server">
                    <asp:Image ID="Thumb" runat="server" />
                </asp:HyperLink>
            </div>
            <div class="ArticleList_artInf">
                <div class="ArticleList_desc">
                    <asp:HyperLink ID="ViewLink" runat="server" Text='<%# Eval("Title") %>' />
                    <asp:Label ID="CommCount" CssClass="comCnt" runat="server" />
                    <img id="img_new" runat="server" src="~/Img/inew.gif" alt="" visible="false" />
                </div>
                <div class="ArticleList_desc">
                    <div class="ArticleList_poster">
                        <%# Eval("Reporter") %></div>
                    Date:
                    <%# Eval("IssueDate", "{0:yyyy/MM/dd}")%>&nbsp;/&nbsp;Views:
                    <%# Eval("ViewCount") %>
                </div>
                <div class="prv">
                    <asp:HyperLink ID="Article_Abstract" runat="server" Text="" />
                </div>
            </div>
        </div>
    </AlternatingItemTemplate>
</asp:Repeater>
<asp:Repeater ID="DataImageRepeater" runat="server" OnItemDataBound="DataImageRepeater_ItemDataBound">
    <HeaderTemplate>
        <div class="DataImage_Container">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="ArtPicBox">
            <div class="picNew">
                <asp:Image ID="ImgNew" runat="server" CssClass="mdl" ImageUrl="~/img/inew.gif" AlternateText="New"
                    Visible="false" />
            </div>
            <div class="ArtPicFrm">
                <asp:HyperLink ID="ViewLink1" runat="server">
                    <img id="Img1" runat="server" alt="" src="" /></asp:HyperLink>
            </div>
            <div class="picTtl">
                <asp:HyperLink ID="ViewLink2" runat="server"><%# Eval("Title") %></asp:HyperLink>
            </div>
            <div class="picDesc">
                <asp:Label ID="lbl_issuedate" runat="server" Text='<%# Eval("IssueDate", "{0:MM/dd}")%>'></asp:Label>
                <asp:Label ID="lbl_viewcount" runat="server" Text='<%$ Resources: Resources, Gen.lbl_count %>'></asp:Label>
                <asp:Label ID="lbl_viewcountnumber" runat="server" Text='<%# Eval("ViewCount") %>'></asp:Label>
                <asp:Literal ID="CommCount" runat="server" />
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        </div></FooterTemplate>
</asp:Repeater>
<script type="text/javascript">
    function showimage() {
        api_images = ['http://photo.hankooki.com/gisaphoto/inews/2013/11/01/1101243135016_0.jpg', 'http://www.youtube.com/watch?v=BtnoRPQoaJ8', 'http://photo.hankooki.com/gisaphoto/inews/2013/11/01/1101225132427_0.jpg'];
        $.prettyPhoto.open(api_images);
    }
</script>
<div class="secClr">
</div>
<div class="cntrPnl">
    <asp:HyperLink ID="ViewSwitchLink" runat="server" CssClass="boxLnk toRite" Text="Switch to Normal View"
        Visible="false" Style="margin-right: 8px; margin-top: 8px;" />
    <asp:HyperLink ID="PostLink" runat="server" CssClass="boxLnk toRite" Text="Add a New Article"
        Visible="false" Style="margin-right: 8px; margin-top: 8px;" />
    <asp:Button ID="PostLink_popup" runat="server" Text="<%$ Resources: Resources, Article.lbl_Post %>" Visible="false" CssClass="boxLnk toRite"/>
    &nbsp;
    <uc1:FitPager ID="FitPager1" runat="server" OnPageNumberChanged="PageNumber_Changed"
        Visible="true" />
    &nbsp;
</div>
<div class="cntrPnl" id="article_search_panel" runat="server">
<asp:Panel ID ="searchPanel" runat="server" DefaultButton = "SearchButton">
    <asp:TextBox ID="SearchValue" runat="server" Width="240px" />
    <asp:Button ID="SearchButton" runat="server" Text=" <%$ Resources: Resources, Gen.lbl_Search %> "
        OnClick="SearchButton_Click" CausesValidation="false" />
        </asp:Panel>
</div>
<div>
<epm:EntryPopup ID="EntryPopup" runat="server"/>

</div>
