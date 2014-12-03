<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    Inherits="Cp.User.CpUserUserInfo" CodeBehind="UserInfo.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        td ul
        {
            padding: 0;
        }
        td ul li
        {
            list-style: none;
            line-height: 18pt;
        }
        span.label
        {
            width: 80px;
            text-align: right;
            display: inline-block;
            margin-right: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <h1>
        계정 정보</h1>
    <table width="600">
        <tr>
            <td class="label">
                USer Name
            </td>
            <td class="data">
                <asp:Literal ID="UserName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="label">
                이메일
            </td>
            <td class="data">
                <asp:Literal ID="Email" runat="server" />
            </td>
        </tr>
        <tr style="display: none">
            <td class="label">
                사용자 그룹
            </td>
            <td class="data">
                <asp:Literal ID="UserRole" runat="server" />
                <asp:DropDownList ID="RoleList" runat="server" />
                <asp:Button ID="SetRole" runat="server" Text="그룹 변경 " OnClick="SetRole_Click" />
            </td>
        </tr>
        <tr>
            <td class="label">
                등록일
            </td>
            <td class="data">
                <asp:Literal ID="RegDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="label">
                최근로그인
            </td>
            <td class="data">
                <asp:Literal ID="LastLogin" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="label">
                계정 상태
            </td>
            <td class="data">
                <asp:Literal ID="AccStat" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="EnableAcc" runat="server" OnClick="EnableAcc_Click" />
            </td>
        </tr>
        <tr>
            <td class="label">
                개인 정보
            </td>
            <td class="data">
                <ul>
                    <li><span class="label">이름:</span><asp:Literal ID="FirstName" runat="server" /></li>
                    <li><span class="label">성:</span><asp:Literal ID="LastName" runat="server" /></li>
                    <li><span class="label">이메일:</span><asp:Literal ID="EmailAddress" runat="server" /></li>
                    <li><span class="label">전화번호:</span><asp:Literal ID="Phone" runat="server" /></li>
                    <li><span class="label">주소1:</span><asp:Literal ID="Address1" runat="server" /></li>
                    <li><span class="label">주소2:</span><asp:Literal ID="Address2" runat="server" /></li>
                    <li><span class="label">우:</span><asp:Literal ID="PostalCode" runat="server" /></li>
                    <li><span class="label">도시:</span><asp:Literal ID="City" runat="server" /></li>
                    <li><span class="label">주:</span><asp:Literal ID="Province" runat="server" /></li>
                    <li><span class="label">구독신청:</span><asp:Literal ID="Subscriber" runat="server" /></li>
                    <li><span class="label">코멘트:</span><asp:Literal ID="UserComment" runat="server" /></li>
                </ul>
            </td>
        </tr>
    </table>
    <div class="cmdPnl">
        <%--asp:CheckBox ID="ChkMail" runat="server" Text=" 임시비밀번호를 메일로 전송" />
    <span class="sep12" /--%>
        <asp:Button ID="ResetPwd" runat="server" Text="비밀번호초기화 " OnClick="ResetPwd_Click" />
    </div>
    <div style="color: #0000bb; font-weight: bold;">
        <asp:Literal ID="NewPass" runat="server" Visible="false" />
    </div>
</asp:Content>
