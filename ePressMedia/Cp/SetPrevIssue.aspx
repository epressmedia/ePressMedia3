<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_SetPrevIssue" Codebehind="SetPrevIssue.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <h1>지난 신문 보기 정보 설정</h1>
  <asp:HiddenField ID="IssueId" runat="server" />
  <table>
    <tr>
      <td class="label">제목</td>
      <td class="data">
        <asp:TextBox ID="IssueName" runat="server" Width="360" /></td>
    </tr>
    <tr>
      <td class="label">링크 URL</td>
      <td class="data">
        <asp:TextBox ID="Url" runat="server" Width="480" /></td>
    </tr>
    <tr>
      <td class="label">이미지 URL</td>
      <td class="data">
        <asp:TextBox ID="ImgUrl" runat="server" Width="480" /></td>
    </tr>
  
  </table>

  <div class="cmdPnl cntr">
  
    <asp:Button ID="SaveButton" runat="server" Text="저장 " 
      onclick="SaveButton_Click" />
    <span class="sep24"></span>
    <asp:Button ID="CancelButton" runat="server" Text="취소 " 
      onclick="CancelButton_Click" />
  
  </div>
</asp:Content>

