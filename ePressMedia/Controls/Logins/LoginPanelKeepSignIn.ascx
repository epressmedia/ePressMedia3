<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginPanelKeepSignIn.ascx.cs" Inherits="ePressMedia.Controls.Logins.LoginPanelKeepSignIn" %>
  <div Id="LoginPanel" class="LoginPanel_Container" runat="server">
    <div class="siBox">
      <asp:Label ID="lbl_UserName" runat="server" CssClass="Login_Label" AssociatedControlID="txt_UserName" accesskey="N" Text="<%$ Resources: Resources, Login.lbl_UserName %>" />
      <br />
      <asp:TextBox ID="txt_UserName" runat="server" CssClass="Login_TextBox" MaxLength="16" TabIndex="1" />
      <br />
      <asp:Label ID="lbl_Password" runat="server" CssClass="Login_Label" AssociatedControlID="txt_Password" AccessKey="P" Text="<%$ Resources: Resources, Login.lbl_Password %>" />
      <br />
      <asp:TextBox ID="txt_Password" runat="server" CssClass="Login_TextBox" MaxLength="16" TextMode="Password" TabIndex="2" />
      <br />
      
      <asp:CheckBox ID="ChkRememberMe" runat="server" Text="<%$ Resources: Resources, Login.ChkKeepMeSignIn %>" />
      <br />
      <div class="login_button">
      <asp:Button ID="BtnLogin" runat="server" Text="<%$ Resources: Resources, Login.btn_login %>" AccessKey="S" TabIndex="3" CssClass="login_button"
          onclick="BtnLogin_Click" />
        </div>
    </div>

    <div class="login_links">
      <asp:LinkButton ID = "lb_singup" Text="<%$ Resources: Resources, Login.lbl_signup %>" CssClass="signup_button" runat="server"
      PostBackUrl="~/Account/SignUp.aspx"></asp:LinkButton> 
      <a href="../Account/ForgotUsername.aspx"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Login.lbl_LostUserName %>" /></a> &#183;
      <a href="../Account/RecoverPassword.aspx"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Login.lbl_LostPassword %>" /></a> 
      
      
      </div>   
     
        
  </div>