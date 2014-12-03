<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultLoginPanel.ascx.cs" Inherits="ePressMedia.Controls.Logins.DefaultLoginPanel" %>
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
      
      <asp:CheckBox ID="ChkRememberMe" runat="server" Text="<%$ Resources: Resources, Login.ChkRememberMe %>" />
      <br />
      <div class="login_button">
      <asp:Button ID="BtnLogin" runat="server" Text="<%$ Resources: Resources, Login.btn_login %>" AccessKey="S" TabIndex="3" CssClass="login_button"
          onclick="BtnLogin_Click" />
        </div>
    </div>

    <div class="login_links">
    <div>
      <asp:LinkButton ID = "lb_singup" Text="<%$ Resources: Resources, Login.lbl_signup %>" CssClass="signup_button" runat="server"
      PostBackUrl="~/Account/SignUp.aspx"></asp:LinkButton> 
      </div>
      <div>
      <a href="/Account/ForgotUsername.aspx" class="loginpanel_lostusername_link"><asp:Literal runat="server" Text="<%$ Resources: Resources, Login.lbl_LostUserName %>" /></a>
      <a href="/Account/RecoverPassword.aspx" class="loginpanel_lostpassword_link"><asp:Literal runat="server" Text="<%$ Resources: Resources, Login.lbl_LostPassword %>" /></a> 
      <a href="/Account/RecoverUserInfo.aspx" class="loginpanel_lostusernamepassword_link"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindUserNamePassword %>" /></a> 
      </div>
      
      
      </div>   
     
        
  </div>
  <div Id="AfterLoginPanel" class="LoginPanel_Container" runat="server" visible = "false">
  <div class="siBox">
  <div class="AfterLoginPanel_Welcome"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Login.lbl_Welcome %>" /></div>
  <div class="AfterLoginPanel_username_prefix"><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: Resources, Login.lbl_UserNamePrefix %>" /></div>
  <div class="AfterLoginPanel_username"><asp:Label ID="lbl_username2" runat="server"></asp:Label></div>
  <div class="AfterLoginPanel_username_postfix"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: Resources, Login.lbl_UsernamePostfix %>" /></div>
  </div>
  <div class="AfterLoginPanel_Links">
    <div class="AfterLoginPanel_UserAccount">
    <asp:Button ID="btn_myAccount" runat="server" Text="<%$ Resources: Resources, Login.lbl_MyProfile %>" AccessKey="M" TabIndex="3" CssClass="myaccount_button"
          onclick="btn_myAccount_Click" />
    </div>
    <div class="AfterLoginPanel_Logout"><asp:Button ID="btn_LogOut" runat="server" Text="<%$ Resources: Resources, Login.lbl_logout %>" AccessKey="O" TabIndex="3" CssClass="logout_button"
          onclick="btn_LogOut_Click" /></div>
  </div>
  </div>