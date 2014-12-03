<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_User_AddUser" Codebehind="AddUser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <h1>사용자 추가</h1>
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
        AutoGeneratePassword="True" 
        onactivestepchanged="CreateUserWizard1_ActiveStepChanged" 
        oncreateduser="CreateUserWizard1_CreatedUser1" ActiveStepIndex="0" 
        oncreatinguser="CreateUserWizard1_CreatingUser">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="right" class="label">
                                User Group:
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="GroupList" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    InitialValue="--그룹을 선택하세요--" 
                                    ControlToValidate="GroupList" ErrorMessage="Select User Group" 
                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="label">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                            </td>
                            <td class="data">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="label">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                            </td>
                            <td class="data">
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                    ControlToValidate="Email" ErrorMessage="E-mail is required." 
                                    ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="EmailInvalid" runat="server" ControlToValidate="Email" 
                                    ErrorMessage="Invalid E-mail address" ValidationGroup="CreateUserWizard1" Display="Dynamic" 
                                    ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="center" colspan="2">
                                사용자 등록 완료</td>
                        </tr>
                        <tr>
                            <td>
                                새로운 사용자가 추가되었습니다. 임시비밀번호는
                                &quot;<asp:Label ID="TempPassword" runat="server" Font-Bold="true" />&quot;
                                입니다.
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" 
                                    CommandName="계속" Text="Continue" ValidationGroup="CreateUserWizard1" OnClick="ContinueButtonClick" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>

</asp:Content>

