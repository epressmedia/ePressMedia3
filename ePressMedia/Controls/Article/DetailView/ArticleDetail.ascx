<%@ Control Language="C#" AutoEventWireup="true" Inherits="Article_DetailView_ArticleDetail"
    CodeBehind="ArticleDetail.ascx.cs" %>

<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
<script src="../Scripts/ViewArticle.js" type="text/javascript"></script> 

</telerik:RadScriptBlock>

<asp:Button ID="ModalTarget" runat="server" Style="display: none" />
<div>
    <asp:Image ID="HeaderImg" runat="server" Visible="false" />
</div>
<div id="threadView" class="thrdVw">
    <div class="threadHeader">
        <div class="ttl">
            <asp:Literal ID="MsgTitle" runat="server" />
        </div>

        <div class="inf" >
            <div class="poster">
            <asp:Label ID="IssueDate" runat="server" CssClass="IssueDate" ></asp:Label> <asp:Label ID="IssueTime" runat="server" CssClass="IssueTime" ></asp:Label>
                <asp:Label ID="PostBy" runat="server" CssClass="PostBy" ></asp:Label>
            <span class="article_view_counter" style="margin-left:10px"><asp:Label ID="lbl_ArtivleView" runat="server" Text="<%$ Resources: Resources, Article.lbl_Views %>"></asp:Label> <asp:Label ID = "ViewCount" runat="server"></asp:Label></span>
            </div>
            <div class="inf_icons">
            <span>Text Size:</span> <a href="#" id="incFont">
                <img style="padding: 0px; vertical-align: middle" alt="Larger" src="/Img/font_plus.gif" title="Larger"/></a>
            <a href="#" id="decFont">
                <img style="padding: 0px; vertical-align: middle" alt="Smaller" src="/Img/font_minus.gif" title="Smaller"/></a>
            <a href="#" onclick="window.print()">
                <img style="padding: 0px; vertical-align: middle" alt="Small" src="/Img/btn_print.gif" title="print"/></a>
                                    <telerik:RadSocialShare runat="server" ID="RadSocialShare1">
            <MainButtons>
                <telerik:RadSocialButton SocialNetType="ShareOnFacebook" />
                <telerik:RadSocialButton SocialNetType="ShareOnTwitter" />
                <telerik:RadSocialButton SocialNetType="GoogleBookmarks" />
                <telerik:RadSocialButton SocialNetType="StumbleUpon" />
            </MainButtons>
        </telerik:RadSocialShare>
        </div>
        </div>
 


    </div>
    <div class="msg">
             <div class="sub_ttl">
            <asp:Label ID="SubTitle" runat="server" CssClass="subTtl" />
        </div>

        <div id="youtube_player" runat="server" clientidmode="Static" visible="false">
            <iframe class="youtube-player" type="text/html" width="4" height="3" src="http://www.youtube.com/embed/<% =VideoId %>"
                frameborder="0"></iframe>
        </div>
        <div id="mainArtBody" onselectstart="return false">
            <asp:Literal ID="Message" runat="server" />
        </div>
    </div>
               <div class="tagdiv" id = "tag_container" runat="server">
               <asp:Label ID="lbl_tagname" CssClass="lbl_tagname" runat="server" Text="Tags: "></asp:Label>
            <asp:Label ID="lbl_tags" CssClass="lbl_tags" runat="server" Text=""></asp:Label>
            </div>

</div>
<div class="cmdPnlR">
    <asp:Button ID="DelButton" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_Delete %>" ></asp:Button>
                  <asp:Button ID="EditLink_popup" runat="server"  CssClass="boxLnk"
          Text="<%$ Resources: Resources, Gen.lbl_Edit %>" Visible="false" />
    <asp:HyperLink ID="ListLink" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_List %>" />
    <div class="snsLnk">
        <telerik:RadSocialShare runat="server" ID="socialShareNetworkShare">
            <MainButtons>
                <telerik:RadSocialButton SocialNetType="ShareOnFacebook" />
                <telerik:RadSocialButton SocialNetType="ShareOnTwitter" />
                <telerik:RadSocialButton SocialNetType="GoogleBookmarks" />
                <telerik:RadSocialButton SocialNetType="StumbleUpon" />
            </MainButtons>
        </telerik:RadSocialShare>
    </div>
</div>
<div id="movePnl" class="cntrPnl" runat="server" visible="false">
    <asp:DropDownList runat="server" ID="CatList" />
    <asp:LinkButton ID="MoveLink" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_Move %>"
        OnClick="MoveLink_Click" />
</div>

  <div>
<epm:EntryPopup ID="EntryPopupEdit" runat="server" />
      <epm:EntryPopup ID="DeletePopup" runat="server" />
  </div>
