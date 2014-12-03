<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_11131.master" AutoEventWireup="true" Inherits="Account_ManageAccount" Codebehind="ManageAccount.aspx.cs" %>
<%@ MasterType VirtualPath="~/Master/MasterPage_11131.master" %>
<%@ Register src="/Page/UDFEntryPanel.ascx" tagname="UDFPanel" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
<div class="manageaccount_content">  <div>&nbsp;</div>

  <h1><asp:Literal ID ="literal17" runat="server" Text="<%$ Resources: Resources, Admin.lbl_UpdatePassword%>"></asp:Literal></h1>
  <table class="signUp">
    <tr>
      <td class="lbl">   <asp:Literal ID ="literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_CurrentPassword %>"></asp:Literal></td>
      <td>
        <asp:TextBox ID="OldPassword" runat="server" TextMode="Password" MaxLength="16" />
        <asp:RequiredFieldValidator ID="ReqVal1" runat="server" ErrorMessage=" <%$ Resources: Resources, Account.msg_EmailReq %>  "
          ControlToValidate="OldPassword" Display="Dynamic" ValidationGroup="g1" />
      </td>
    </tr>
    <tr>
      <td class="lbl"><asp:Literal ID ="literal2" runat="server" Text="<%$ Resources: Resources,Account.lbl_NewPassword %>"></asp:Literal></td>
      <td>
        <asp:TextBox ID="Password1" runat="server" TextMode="Password" MaxLength="16" /><br />
        <span class="guide"><asp:Literal ID ="literal50" runat="server" Text="<%$ Resources: Resources,Account.msg_PasswordValidation %>" /></span><br />
        <asp:TextBox ID="Password2" runat="server" TextMode="Password" MaxLength="16" /><br />
        <span class="guide"><asp:Literal ID ="literal51" runat="server" Text="<%$ Resources: Resources,Account.lbl_EnterPasswordAgain %>"/></span>
        <asp:CompareValidator ID="CompVal1" runat="server" ErrorMessage="<%$ Resources: Resources,Account.msg_PasswordMatch %>" 
          ControlToCompare="Password2" ControlToValidate="Password1" Display="Dynamic" ValidationGroup="g1" />
        <asp:RequiredFieldValidator ID="ReqVal2" runat="server" ErrorMessage="<%$ Resources: Resources,Account.msg_PasswordReq %>"
          ControlToValidate="Password1" Display="Dynamic" ValidationGroup="g1" />
        <asp:RegularExpressionValidator ID="RegEx3" runat="server" 
          ControlToValidate="Password1" ErrorMessage="<%$ Resources: Resources,Account.msg_PasswordValidation %>"
          ValidationExpression="^.{6,12}$" Display="Dynamic" ValidationGroup="g1" />
      </td>
    </tr>
  </table>
  <div class="cntrPnl">
    <asp:Button ID="SetPassword" runat="server" Text="<%$ Resources: Resources,Admin.lbl_UpdatePassword%>" ValidationGroup="g1" 
      onclick="SetPassword_Click" />
  </div>

  <div>&nbsp;</div>  
  <h1><asp:Literal ID ="literal4" runat="server" Text="<%$ Resources: Resources, Account.lbl_UpdateProfile %>"></asp:Literal></h1>
      <h2><asp:Literal ID ="literal3" runat="server" Text="<%$ Resources: Resources, Account.lbl_RequiredInfo %>"></asp:Literal></h2>
  <table class="signUp">
    <tr>
      <td class="lbl"><asp:Literal ID ="literal5" runat="server" Text="<%$ Resources: Resources, Account.lbl_EmailAddress %>"></asp:Literal></td>
      <td>
        <asp:TextBox ID="Email" runat="server" MaxLength="40" Columns="40" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources: Resources, Account.lbl_EnterEmailAddress %><br />이메일 주소를 입력하세요." ValidationGroup="submit"
          ControlToValidate="Email" Display="Dynamic"  />
        <asp:RegularExpressionValidator ID="RegExEmail1" runat="server"  ValidationGroup="submit"
          ControlToValidate="Email" ErrorMessage="<%$ Resources: Resources, Account.msg_EmailValidation %>" 
          ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
          Display="Dynamic"></asp:RegularExpressionValidator>
      </td>
    </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal6" runat="server" Text="<%$ Resources: Resources, Account.lbl_LastName%>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="FirstName" runat="server" MaxLength="40" Columns="40" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_LastNameReq %>" ValidationGroup="submit"
                    ControlToValidate="FirstName" Display="Dynamic" /><br />
            </td>
        </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal7" runat="server" Text="<%$ Resources: Resources, Account.lbl_FirstName %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="LastName" runat="server" MaxLength="40" Columns="40" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_FirstNameReq %>" ValidationGroup="submit"
                    ControlToValidate="LastName" Display="Dynamic" /><br />
            </td>
        </tr>
  </table>
  <p class="guide">

  </p>
      <h2><asp:Literal ID ="literal8" runat="server" Text="<%$ Resources: Resources, Account.lbl_OptionalInfo %>"></asp:Literal></h2>
  <table class="signUp">
        <tr>
            <td class="lbl"><asp:Literal ID ="literal9" runat="server" Text="<%$ Resources: Resources, Account.lbl_ZipCode %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="txt_postal_code" runat="server" MaxLength="6" OnTextChanged="txt_postal_code_TextChanged"
                    AutoPostBack="True" /><span class="guide"></span><br />
            </td>
        </tr>
        <tr>
            <td class="lbl"><asp:Literal ID ="literal10" runat="server" Text="<%$ Resources: Resources, Account.lbl_Address1 %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="txt_address1" runat="server" MaxLength="40" /><span class="guide">
                </span>
                <br />
            </td>
        </tr>
        <tr>
            <td class="lbl"><asp:Literal ID ="literal11" runat="server" Text="<%$ Resources: Resources, Account.lbl_Address2 %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="txt_address2" runat="server" MaxLength="40" />
                <span class="guide"></span>
                <br />
            </td>
        </tr>
        <tr>
            <td class="lbl"><asp:Literal ID ="literal12" runat="server" Text="<%$ Resources: Resources, Account.lbl_State %>"></asp:Literal>
            </td>
            <td>
                <telerik:RadComboBox ID="ddl_Province" runat="server" AutoPostBack="true" AllowCustomText="true"
                    MarkFirstMatch="true" EmptyMessage="Please Select Province/State" Height="200px"
                    Width="300px" OnSelectedIndexChanged="ddl_Province_SelectedIndexChanged">
                </telerik:RadComboBox>
                <span class="guide"></span>
                <br />
            </td>
        </tr>
        <tr>
            <td class="lbl"><asp:Literal ID ="literal13" runat="server" Text="<%$ Resources: Resources, Account.lbl_City %>"></asp:Literal>
            </td>
            <td>
                <telerik:RadComboBox ID="ddl_city" runat="server" MarkFirstMatch="true" EmptyMessage="Please Select City"
                    Height="200px" Width="300px">
                </telerik:RadComboBox>
                <span class="guide"></span>
                <br />
            </td>
        </tr>
        <tr style="display: none">
            <td class="lbl">
                이메일 구독신청
            </td>
            <td>
                <asp:CheckBox ID="ChkSubs" runat="server" Text="이메일 구독" Checked="false" /><br />
                <span class="guide"></span>
            </td>
        </tr>
        <tr>
            <td class="lbl"><asp:Literal ID ="literal15" runat="server" Text="<%$ Resources: Resources, Account.lbl_PhoneNumber%>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="Phone" runat="server" MaxLength="20" /><span class="guide"><asp:Literal ID ="literal14" runat="server" Text="<%$ Resources: Resources, Gen.lbl_Example%>"></asp:Literal>)425-712-1236</span><br />
            </td>
        </tr>
        <tr>
            <td class="lbl"><asp:Literal ID ="literal16" runat="server" Text="<%$ Resources: Resources, Account.lbl_Comment%>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="ToKcr" runat="server" TextMode="MultiLine" Rows="5" Columns="50"
                    MaxLength="30" />
                <br />
                <span class="guide"></span>
            </td>
        </tr>
        <tr>
        <td colspan="2">
            <asp:Panel ID="UDF_Panel" runat="server">
    </asp:Panel>
        </td>
        </tr>
    </table>



  
  <div class="cntrPnl">
    <asp:Button ID="SetButton" runat="server" Text="<%$ Resources: Resources, Account.lbl_UpdateProfile%>"  ValidationGroup="submit"
      onclick="SetButton_Click" />
  </div>
  </div>

</asp:Content>




