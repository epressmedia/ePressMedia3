<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumDetail.ascx.cs" Inherits="ePressMedia.Controls.Forum.DetailView.ForumDetail" %>

  <div id="threadView" class="thrdVw">
    <div class="ttl">
      <asp:Literal ID="MsgTitle" runat="server" />
    </div>
    <div class="inf">
      <span class="lbl">
          <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Name %>"></asp:Literal>: </span><asp:Literal ID="PostBy" runat="server" /><span class="colSep"></span>
      <span class="lbl">
          <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Date %>"></asp:Literal>: </span><asp:Literal ID="PostDate" runat="server" /><span class="colSep"></span>
      <span class="lbl">
          <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Views %>"></asp:Literal>: </span><asp:Literal ID="ViewCount" runat="server" />
    </div>
    <asp:Repeater ID="AttRepeater" runat="server">
      <HeaderTemplate>
        <div class="inf">
        <span class="lbl">
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Forum.lbl_Attachments %>"></asp:Literal> : </span>
      </HeaderTemplate>
      <ItemTemplate>
        <a id="A1" runat="server" target="_blank" href='<%# Eval("FileName", "~/Pics/Frm/{0}") %>'><%# Eval("FileName") %></a>
      </ItemTemplate>
      <SeparatorTemplate>
        ,&nbsp;&nbsp;&nbsp;
      </SeparatorTemplate>
      <FooterTemplate>
        </div>
      </FooterTemplate>
    </asp:Repeater>


    <div class="msg">
      <div class="cntrPnl" id="viewPwdPanel" runat="server" visible="false">
          <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Forum.lbl_PasswordEnter%>"></asp:Literal>
        <br /><br />
        <asp:TextBox ID="ViewPwd" runat="server" TextMode="Password" MaxLength="8" />
        <asp:Button ID="ViewButton" runat="server" Text="<%$ Resources: Resources, Gen.lbl_Ok%> " onclick="ViewButton_Click" />
      </div>
      <div id="msgBody" runat="server">

      <asp:Repeater ID="PicRepeater" runat="server">
        <ItemTemplate>
          <div style="text-align:center; margin-bottom:8px;">
            <img id="Img1" runat="server" alt="" src='<%# Eval("FileName", "~/Pics/Frm/{0}") %>' class="frm_image_view" />
          </div>
        </ItemTemplate>
      </asp:Repeater>
      <asp:Literal ID="Message" runat="server" />
      </div>
    </div>

  </div>

  <div class="cmdPnlR">
    <asp:Button ID="DelButton" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_Delete%>" />
    <asp:Button ID="btn_update" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_Edit%>" />
    <asp:HyperLink ID="ListLink" runat="server" CssClass="boxLnk" Text="<%$ Resources: Resources, Gen.lbl_List %>" />

    <div class="snsLnk">
                 <telerik:RadSocialShare runat="server" ID="socialShareNetworkShare" >
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
    Target Forum: <asp:DropDownList runat="server" ID="ForumList" /> 
    <asp:LinkButton ID="MoveLink" runat="server" CssClass="boxLnk" Text="Move" 
      onclick="MoveLink_Click" />
  </div>
  <div>
<epm:EntryPopup ID="EntryPopupEdit" runat="server"/>
      <epm:EntryPopup ID="DeletePopup" runat="server"/>
  </div>

  <div class="secClr"></div>