<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_11131.master" AutoEventWireup="true" Inherits="Account_VerifyAccount" Codebehind="VerifyAccount.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_11131.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
<div class="VerifyAccount_Conent">
  <h1><asp:Literal ID ="literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_FinishRegistration %>"/></h1>
  <asp:Panel ID="ErrPnl" runat="server">
    <h2 class="err">
    <asp:Literal ID ="literal2" runat="server" Text="<%$ Resources: Resources,Account.msg_AccountVerifyFail1  %>"/>
    </h2>
    <p>
    <asp:Literal ID ="literal3" runat="server" Text="<%$ Resources: Resources, Account.msg_AccountVerifyFail2 %>"/>
    </p>
  </asp:Panel>

  <asp:Panel ID="SuccPnl" runat="server">
    <p><asp:Literal ID ="literal4" runat="server" Text="<%$ Resources: Resources,Account.msg_AccountVerifySuccess1  %>"/></p>
    <h2 class="noerr">
    <asp:Literal ID ="literal5" runat="server" Text="<%$ Resources: Resources,Account.msg_AccountVerifySuccess1  %>"/>
    
    </h2>
    <p>
      <img style="vertical-align:middle;" src="../Img/next.png"  alt="GoToLoginPage"/>&nbsp;<a href="SignIn.aspx"><asp:Literal ID ="literal7" runat="server" Text="<%$ Resources: Resources, Account.lbl_GoToLoginPage %>"/></a>
    </p>
  </asp:Panel>
  </div>
</asp:Content>

