<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeBehind="RecoverUserInfo.aspx.cs" Inherits="ePressMedia.Account.RecoverUserInfo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
<link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Content" runat="server">
<h1 class="RecoveryUserInfo_Header">
<asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindUserNamePassword %>" />
</h1>

<div class="ForgetUserInfo_Container">
<table class="ForgetUserInfoTable">
<tr>
<td>
<div><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindUserName %>" /></div>
<div><a href="ForgotUsername.aspx"><img src="/img/forgot_ID.png" alt="Forgot ID"/></a></div>
</td>
<td>
<div><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindPassword %>" /></div>
<div>
<a href="RecoverPassword.aspx"><img src="/img/forgot_pw.png" alt="Forgot Password" /></a>
</div>
</td>
</tr>
</table>

</div>
</asp:Content>
