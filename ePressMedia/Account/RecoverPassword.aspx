<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_11131.master" AutoEventWireup="true" Inherits="Account_RecoverPassword" Codebehind="RecoverPassword.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_11131.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
<div class="RecoveryPassword_Header">
  <div>&nbsp;</div>
  <h1><asp:Literal ID ="literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_ResetPassword %>"/></h1>
  <p class="guide">
  <asp:Literal ID ="literal2" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindPasswordDescr %>"/>
  </p>
  <p class="guide">
  <asp:Literal ID ="literal3" runat="server" Text=""/>
  
  </p>
  <table class="nb">
    <tr>
      <td class="lbl"><asp:Literal ID ="literal4" runat="server" Text="<%$ Resources: Resources, Login.lbl_UserName %>"/></td>
      <td>
        <asp:TextBox ID="UserName" runat="server" MaxLength="18" /><a href="ForgotUsername.aspx" class="RecoverPassword_username_link"><asp:Literal ID ="literal6" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindUserName %>"/></a>
        <asp:RequiredFieldValidator ID="ReqVal1" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_UserNameReq %>"
          ControlToValidate="UserName" Display="Dynamic"  />
      </td>
    </tr>
    <tr>
      <td class="lbl"><asp:Literal ID ="literal5" runat="server" Text="<%$ Resources: Resources, Account.lbl_EmailAddress %>"/></td>
      <td>
        <asp:TextBox ID="Email" runat="server" MaxLength="40" Columns="40" />
        <asp:RequiredFieldValidator ID="ReqVal2" runat="server" ErrorMessage="<%$ Resources: Resources, Account.lbl_EnterEmailAddress %>"
          ControlToValidate="Email" Display="Dynamic"  />
        <asp:RegularExpressionValidator ID="RegExEmail1" runat="server" 
          ControlToValidate="Email" ErrorMessage="<%$ Resources: Resources, Account.msg_EmailValidation %>" 
          ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
          Display="Dynamic"></asp:RegularExpressionValidator>

      </td>
    </tr>
  </table>

  <div class="cntrPnl">
    <asp:Button ID="SetPassword" runat="server" Text="<%$ Resources: Resources, Gen.lbl_Ok %>" 
      onclick="SetPassword_Click" />
  </div>
  </div>
</asp:Content>


