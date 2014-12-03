<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_11131.master" AutoEventWireup="true" Inherits="Account_ChangePassword" Codebehind="ChangePassword.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_11131.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">


  <div class="siFrm">
    <h1><asp:Literal ID ="literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_UpdatePassword %>" /></h1>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>

    <div class="siBox">
      <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName" accesskey="N" Text="<%$ Resources: Resources, Login.lbl_UserName %>" />
      <br />
      <asp:TextBox ID="UserName" runat="server" MaxLength="16" TabIndex="1" />
      <br />
      <asp:Label ID="Label2" runat="server" AssociatedControlID="CurPwd" AccessKey="C" Text="<%$ Resources: Resources, Account.lbl_CurrentPassword %>" />
      <br />
      <asp:TextBox ID="CurPwd" runat="server" MaxLength="16" TextMode="Password" TabIndex="2" />
      <br />
      <asp:Label ID="Label3" runat="server" AssociatedControlID="NewPwd" AccessKey="W" Text="<%$ Resources: Resources, Account.lbl_NewPassword %>" />
      <br />
      <asp:TextBox ID="NewPwd" runat="server" MaxLength="16" TextMode="Password" TabIndex="3" />
      <br />
      <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPwd" AccessKey="P" Text="<%$ Resources: Resources, Account.lbl_ConfirmPassword %>" />
      <br />
      <asp:TextBox ID="ConfirmPwd" runat="server" MaxLength="16" TextMode="Password" TabIndex="4" />
      <br />

      <asp:Label ID="Message" runat="server" Visible="true" ForeColor="Red" />
      <br />      
 
      <asp:Button runat="server" ID="BtnChange" Text="<%$ Resources: Resources, Gen.lbl_Ok %>" onclick="BtnChange_Click" TabIndex="5" />

    </div>
        
      </ContentTemplate>
    </asp:UpdatePanel>
  </div>

</asp:Content>


