<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage_11131.master"
    AutoEventWireup="true" Inherits="Account_SignUp" CodeBehind="SignUp.aspx.cs" %>

<%--<%@ MasterType VirtualPath="~/Master/MasterPage_11131.master" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Account.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="Server">
<div class="Signup_Header">
    <h1>
      <asp:Literal ID ="literal1" runat="server" Text="<%$ Resources: Resources, Account.lbl_UserReg %>"/></h1>
    <h2>
        <asp:Literal ID ="literal2" runat="server" Text="<%$ Resources: Resources, Account.lbl_RequiredInfo %>" /></h2>
    <p class="guide">
    <asp:Literal ID ="literal18" runat="server" Text="<%$ Resources: Resources, Account.lbl_RequiredField %>" />
    </p>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="signUp" style="border-bottom: 1px solid #f7f7f7;">
                <tr>
                    <td class="lbl">
                        <asp:Literal ID ="literal3" runat="server" Text="<%$ Resources: Resources, Login.lbl_UserName %>"/>*
                    </td>
                    <td>
                        <asp:TextBox ID="UserName" runat="server" MaxLength="16" />
                        <asp:LinkButton ID="DupCheck" runat="server" Text="<%$ Resources: Resources, Acount.lbl_UserNameAvail %>" ValidationGroup="g1" OnClick="DupCheck_Click" /><asp:Label
                            ID="DupChkResult" runat="server" /><br />
                        <asp:RequiredFieldValidator ID="Req2" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_UserNameReq %>" ValidationGroup="submit"
                            ControlToValidate="UserName" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="RegEx2" runat="server" ControlToValidate="UserName" ValidationGroup="submit"
                            ErrorMessage="<%$ Resources: Resources, Account.msg_UserNameValidation %>" ValidationExpression="^[a-z0-9_]{4,16}$"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        
    <table class="signUp" style="border-top: 1px solid #f7f7f7">
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal4" runat="server" Text="<%$ Resources: Resources, Account.lbl_EmailAddress %>"/>*
            <td>
                <asp:TextBox ID="Email" runat="server" MaxLength="40" Columns="40" /><br />
                <span class="guide"><asp:Literal ID ="literal19" runat="server" Text="<%$ Resources: Resources, Account.lbl_EnterEmailAgain %>"/></span><br />
                
                <asp:TextBox ID="Email2" runat="server" MaxLength="40" Columns="40" /><br />
                
                  <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_EmailMatch %>" ValidationGroup="submit"
                    ControlToCompare="Email" ControlToValidate="Email2" Display="Dynamic" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_EmailReq %>" ValidationGroup="submit"
                    ControlToValidate="Email" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegExEmail1" runat="server" ControlToValidate="Email" ValidationGroup="submit"
                    ErrorMessage="<%$ Resources: Resources, Account.msg_EmailValidation %>" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="lbl">
               <asp:Literal ID ="literal5" runat="server" Text="<%$ Resources: Resources, Login.lbl_Password %>"/>*
            </td>
            <td>
                <asp:TextBox ID="Password1" runat="server" TextMode="Password" MaxLength="16" /><br />
                <span class="guide"><asp:Literal ID ="literal17" runat="server" Text="<%$ Resources: Resources, Account.lbl_EnterPasswordAgain %>"/></span><br />
                <asp:TextBox ID="Password2" runat="server" TextMode="Password" MaxLength="16" /><br />
                <asp:CompareValidator ID="CompPass" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_PasswordMatch %>" ValidationGroup="submit"
                    ControlToCompare="Password2" ControlToValidate="Password1" Display="Dynamic" />
                <asp:RequiredFieldValidator ID="ReqPass1" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_PasswordReq %>" ValidationGroup="submit"
                    ControlToValidate="Password1" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegEx3" runat="server" ControlToValidate="Password1" ValidationGroup="submit"
                    ErrorMessage="<%$ Resources: Resources, Account.msg_PasswordValidation %>" ValidationExpression="^.{6,12}$" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal6" runat="server" Text="<%$ Resources: Resources, Account.lbl_LastName %>"/>*
            </td>
            <td>
                <asp:TextBox ID="FirstName" runat="server" MaxLength="40" Columns="40" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_FirstNameReq %>" ValidationGroup="submit"
                    ControlToValidate="FirstName" Display="Dynamic" /><br />
            </td>
        </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal7" runat="server" Text="<%$ Resources: Resources, Account.lbl_FirstName %>"/>*
            </td>
            <td>
                <asp:TextBox ID="LastName" runat="server" MaxLength="40" Columns="40" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources: Resources, Account.msg_LastNameReq %>" ValidationGroup="submit"
                    ControlToValidate="LastName" Display="Dynamic" /><br />
            </td>
        </tr>
    </table>
    <h2>
        <asp:Literal ID ="literal8" runat="server" Text="<%$ Resources: Resources,Account.lbl_OptionalInfo  %>"/></h2>
    <p class="guide">
    </p>
    <table class="signUp">
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal9" runat="server" Text="<%$ Resources: Resources,  Account.lbl_ZipCode%>"/>
            </td>
            <td>
                <asp:TextBox ID="txt_postal_code" runat="server" MaxLength="20" OnTextChanged="txt_postal_code_TextChanged"
                    AutoPostBack="True" /><span class="guide"></span><br />
            </td>
        </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal10" runat="server" Text="<%$ Resources: Resources, Account.lbl_Address1 %>"/>
            </td>
            <td>
                <asp:TextBox ID="txt_address1" runat="server" MaxLength="40" Columns="60" /><span class="guide">
                </span>
                <br />
            </td>
        </tr>
        </tr>
        <tr>
            <td class="lbl">
               <asp:Literal ID ="literal11" runat="server" Text="<%$ Resources: Resources,  Account.lbl_Address2%>"/> 
            </td>
            <td>
                <asp:TextBox ID="txt_address2" runat="server" MaxLength="40" Columns="60" />
                <span class="guide"></span>
                <br />
            </td>
        </tr>
        <tr>
            <td class="lbl">
               <asp:Literal ID ="literal12" runat="server" Text="<%$ Resources: Resources, Account.lbl_State %>"/>
            </td>
            <td>
                <telerik:RadComboBox ID="ddl_Province" runat="server" AutoPostBack="true" 
                    MarkFirstMatch="true" EmptyMessage="Please Select Province/State" Height="200px"
                    Width="300px" OnSelectedIndexChanged="ddl_Province_SelectedIndexChanged">
                </telerik:RadComboBox>
                <span class="guide"></span>
                <br />
            </td>
        </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal13" runat="server" Text="<%$ Resources: Resources, Account.lbl_City %>"/>
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
                <asp:Literal ID ="literal14" runat="server" Text="<%$ Resources: Resources, Account.lbl_PhoneNumber %>"/>
            </td>
            <td>
                <asp:CheckBox ID="ChkSubs" runat="server" Text="이메일 구독" Checked="false" /><br />
                <span class="guide"></span>
            </td>
        </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal15" runat="server" Text="<%$ Resources: Resources,Account.lbl_PhoneNumber  %>"/>
            </td>
            <td>
                <asp:TextBox ID="Phone" runat="server" MaxLength="20" /><span class="guide"> 예)425-712-1236</span><br />
            </td>
        </tr>
        <tr>
            <td class="lbl">
                <asp:Literal ID ="literal16" runat="server" Text="<%$ Resources: Resources, Account.lbl_Comment %>"/>
            </td>
            <td>
                <asp:TextBox ID="ToKcr" runat="server" TextMode="MultiLine" Rows="5" Columns="50"
                    MaxLength="30" />
                <br />
                
            </td>
        </tr>
        <td colspan ="2">
        <asp:Panel ID="UDF_Panel" runat="server">
    </asp:Panel>
        </td>
        <tr>
        </tr>
    </table>
        
    </ContentTemplate>
    </asp:UpdatePanel>
    <div class="cntrPnl">
        <asp:Button ID="RegButton" runat="server" Text="<%$ Resources: Resources, Account.btn_RegSubmit %>" ValidationGroup="submit"
            OnClick="RegButton_Click" />
    </div>
</asp:Content>
