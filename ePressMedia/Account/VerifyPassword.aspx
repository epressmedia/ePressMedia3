<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_11131.master" AutoEventWireup="true" Inherits="Account_VerifyPassword" Codebehind="VerifyPassword.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_11131.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
<div class="VarifyPassword_Content">

  <div class="siFrm">

    <h1><asp:Literal ID ="literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_UpdateProfile %>"/></h1>
    <div class="siBox">
      <p class="guide"><asp:Literal ID ="literal2" runat="server" Text="<%$ Resources: Resources, Account.msg_VarifyPassword %>"/></p>
      <asp:TextBox ID="Password" runat="server" MaxLength="16" TextMode="Password" TabIndex="1" />
      <br />
      <asp:Label ID="Message" runat="server" Visible="false" Text="<%$ Resources: Resources, Account.msg_WrongPassword %>" ForeColor="Red" />
      <br />
      <asp:Button ID="VerifyButton" runat="server" Text="<%$ Resources: Resources, Gen.lbl_Ok %>" AccessKey="S" 
        TabIndex="2" onclick="VerifyButton_Click" />

    </div>
  </div>
  </div>
</asp:Content>


