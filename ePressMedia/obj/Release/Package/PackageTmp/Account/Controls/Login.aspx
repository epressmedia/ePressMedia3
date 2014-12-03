﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ePressMedia.Account.Controls.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div id="cerceve">
<div class="header_image"><img src="img/logo/epm_logo_s.png" /></div>
<div class="header"><div class="Login_text" style="float:left">Administration Login</div>
</div>
<div class="formbody">
<asp:TextBox ID="UserName" name="username"  placeholder="Username" CssClass="text" runat="server" MaxLength="16" TabIndex="1" style="background:url(/cp/img/login/username.png) no-repeat;"/>
<asp:TextBox ID="Password" name="password" placeholder="Password" CssClass="text" runat="server" MaxLength="16" TabIndex="2" TextMode="Password" style="background:url(/cp/img/login/password.png) no-repeat;" />
<asp:Label ID="Message" runat="server" Visible="false" Text="Invalid Username or Password"></asp:Label>
<asp:Button ID="BtnLogin" runat="server" Text="Login" AccessKey="S" 
                    TabIndex="3"  CssClass="submit" onclick="BtnLogin_Click" style="background-color:rgb(47, 126, 177)"/>
<asp:CheckBox ID="ChkRememberMe" runat="server" Text=" Remember me" CssClass="checkbox-size"  />
</div>

<div class="footer_sign">
Powered by <a href="http://www.epressmedia.com">ePressMedia</a> <asp:Label ID="version" runat="server" Text=""></asp:Label></div>
</div>
    </form>
</body>
</html>
