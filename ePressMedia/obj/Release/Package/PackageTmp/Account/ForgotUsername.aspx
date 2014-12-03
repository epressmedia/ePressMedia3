<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_11131.master" AutoEventWireup="true" Inherits="Account_ForgotUsername" Codebehind="ForgotUsername.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_11131.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
<div class="forgotUserName_Content">
  <div>&nbsp;</div>
  <h1><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindUserName %>" /></h1>
  <p class="guide">
  <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_FindUserNameDescr %>" />
  </p>
  <p class="guide" style="display:none">
  <a href="RecoverPassword.aspx"><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: Resources, Login.lbl_LostPassword %>" /></a>
  </p>
  <table class="nb">
    <tr>
      <td class="lbl"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: Resources, Account.lbl_EmailAddress %>" /></td>
      <td>
        <asp:TextBox ID="Email" runat="server" MaxLength="40" Columns="40" />
        <asp:RequiredFieldValidator ID="ReqVal2" runat="server" ErrorMessage="<%$ Resources: Resources, Account.lbl_EnterEmailAddress %>"
          ControlToValidate="Email" Display="Dynamic"  />
        <asp:RegularExpressionValidator ID="RegExEmail1" runat="server" 
          ControlToValidate="Email" ErrorMessage="<%$ Resources: Resources, Account.lbl_WrongEmailFormat %>" 
          ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
          Display="Dynamic"></asp:RegularExpressionValidator>

      </td>
    </tr>
  </table>

  <div class="cntrPnl">
    <asp:Button ID="SendId" runat="server" Text="<%$ Resources: Resources, Account.lbl_EmailMe %>" onclick="SendId_Click" />
  </div>
  </div>
</asp:Content>


