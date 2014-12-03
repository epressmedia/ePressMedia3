<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Biz_AddAdPrices" Codebehind="AddAdPrice.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <h1>광고 단가 추가</h1>
  <div class="cmdPnl">&nbsp;</div>
  <table>
    <tr>
      <td class="label">광고유형</td>
      <td class="data">
        <asp:DropDownList ID="AdTypeList" runat="server">
        </asp:DropDownList>
      </td>
    </tr>  
    <tr>
      <td class="label">제목</td>
      <td class="data"><asp:TextBox ID="AdName" runat="server" Width="260px" /></td>
    </tr>
    <tr>
      <td class="label">사이즈(mm)</td>
      <td class="data"><asp:TextBox ID="SizeInMm" runat="server" /></td>
    </tr>
    <tr>
      <td class="label">사이즈(inch)</td>
      <td class="data"><asp:TextBox ID="SizeInInch" runat="server" /></td>
    </tr>
    <tr>
      <td class="label">단가($)</td>
      <td class="data">
        <asp:TextBox ID="Price" runat="server" />
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />금액으로 입력해야 합니다"
            Display="Dynamic" ControlToValidate="Price" Operator="DataTypeCheck" 
          Type="Currency"  />
      </td>
    </tr>

  </table>

  <div class="cmdPnl">
    <asp:Button ID="AddButton" runat="server" Text="단가 추가" 
      onclick="AddButton_Click" />  
  </div>
</asp:Content>

