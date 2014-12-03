<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageLoginPanel.ascx.cs" Inherits="ePressMedia.Controls.Logins.PageLoginPanel" %>
 <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
   <div class="psiFrm">
    <h1><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Login.lbl_login%>" /></h1>
    <div class="psiBox">
    <asp:Panel ID="login_panel" runat="server" DefaultButton = "BtnLogin">
      <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName" accesskey="N" Text="<%$ Resources: Resources, Login.lbl_UserName %>" />
      <br />
      <asp:TextBox ID="UserName" runat="server" MaxLength="16" TabIndex="1"  CssClass=""/>
      <br />
      <asp:Label ID="Label2" runat="server" AssociatedControlID="Password" AccessKey="P" Text="<%$ Resources: Resources, Login.lbl_Password %>" />
      <br />
      <asp:TextBox ID="Password" runat="server" MaxLength="16" TextMode="Password" TabIndex="2" />
      <br />
      <asp:Label ID="Message" runat="server" Visible="false" Text="<%$ Resources: Resources, Login.FailMassage %>" ForeColor="Red" />
      <asp:CheckBox ID="ChkRememberMe" runat="server" Text="<%$ Resources: Resources, Login.ChkRememberMe %>" ToolTip="<%$ Resources: Resources, Login.ChkRememberMe %>" />
      <br />
      <asp:Button ID="BtnLogin" runat="server" Text="<%$ Resources: Resources, Login.btn_login %>" AccessKey="S" TabIndex="3"
          onclick="BtnLogin_Click" />
    <div>
      <br />
      <img src="../Img/bullet.png" />&nbsp;<a href="ForgotUsername.aspx"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Login.lbl_LostUserName %>" /></a><span class="colSep"></span>
      <img src="../Img/bullet.png" />&nbsp;<a href="RecoverPassword.aspx"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: Resources, Login.lbl_LostPassword %>" /></a><br />
      <img src="../Img/bullet.png" />&nbsp;<a href="SignUp.aspx"><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: Resources, Login.lbl_signup %>" /></a>
    </div>    
        </asp:Panel>
    </div>
  </div>