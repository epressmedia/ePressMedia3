<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginLinkLine.ascx.cs" Inherits="ePressMedia.Controls.Logins.LoginLinkLine" %>
<style>
    .LoginLinkLine_Container a:hover
    {
        text-decoration:underline;
    }
</style>
  <div Id="LoginLink" class="LoginLinkLine_Container" runat="server">
  <div id="login_panel" runat="server">
  <a runat="server" id="login_link"><asp:Label id="lbl_login" runat="server" Text="<%$ Resources: Resources, Login.lbl_login %>" /></a> | 
  <a runat="server" id="find_login"><asp:Label id="lbl_login_name" runat="server" Text="<%$ Resources: Resources, Login.lbl_LostUserName %>" /></a><asp:Literal ID="l_find_login" runat="server" Text=" | "></asp:Literal>
  <a runat="server" id="find_password"><asp:Label id="lbl_password" runat="server" Text="<%$ Resources: Resources, Login.lbl_LostPassword %>" /></a><asp:Literal ID="l_find_password" runat="server" Text=" | "></asp:Literal> 
  <a href="/account/signup.aspx"><asp:Label ID="lbl_signup" runat="server" Text="<%$ Resources: Resources, Login.lbl_signup %>" /></a>
  </div>
  <div id="login_info" runat="server">
  <asp:Label ID="user_name" runat="server" style="font-weight:bold"></asp:Label> | 
  <asp:LinkButton ID="user_info" runat="server"  onclick="btn_user_info_Click" Text="<%$ Resources: Resources, Login.lbl_Userinfo %>"></asp:LinkButton> | 
      <asp:LinkButton ID="btn_logout" runat="server" Text="<%$ Resources: Resources, Login.lbl_logout %>" 
          onclick="btn_logout_Click"></asp:LinkButton>
  </div>
  </div>