<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Ad_AddPopup" Codebehind="AddPopup.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">

  <h1>팝업 추가</h1>
  <br />
  <table width="600px">
    <tr>
      <td class="label" style="width:72px">
        팝업명*</td>
      <td class="data">
        <asp:TextBox ID="AdName" runat="server" Width="400px" MaxLength="64" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AdName" 
          Display="Dynamic" ErrorMessage="*" />
        <br />
        팝업 관리 목록에 나타날 이름을 입력합니다.</td>
    </tr>
    <tr>
      <td class="label">
        설명</td>
      <td class="data">
        <asp:TextBox ID="Comment" runat="server" Width="460px" MaxLength="128" />
        <br />
        팝업에 대한 추가적인 설명이 필요한 경우 입력하세요.
      </td>
    </tr>
    <tr>
      <td class="label">
        광고주명</td>
      <td class="data">
        <asp:TextBox ID="AdOwner" runat="server" Width="400px" MaxLength="64" />
        <br />
        팝업 광고주 명을 입력합니다.</td>
    </tr>
    <tr>
      <td class="label">
        연락처</td>
      <td class="data">
        <asp:TextBox ID="Contact" runat="server" Width="400px" MaxLength="64" />
        <br />
        광고주 연락처를 입력합니다.</td>
    </tr>
    <tr>
      <td class="label">
        링크 주소</td>
      <td class="data">
        http://<asp:TextBox ID="LinkUrl" runat="server" Width="400px" MaxLength="128" />
        <br />
        팝업을 클릭했을 때 이동할 주소를 입력합니다.</td>
    </tr>
    <tr>
      <td class="label">
        링크 표시</td>
      <td class="data">
        <asp:RadioButton ID="RadioLinkNew" runat="server" GroupName="LinkTo" Text="새 창으로" Checked="true" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="RadioLinkCur" runat="server" GroupName="LinkTo" Text="현재 창에서" />
        <br />
        팝업 클릭시 위의 링크 주소를 표시하는 방법을 선택합니다.</td>
    </tr>
    <tr>
      <td class="label">
        게시기간*</td>
      <td class="data">
          <telerik:RadDatePicker ID="StartDate" runat="server">
          </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="StartDate" 
          Display="Dynamic" ErrorMessage="*" />

        부터&nbsp;
                  <telerik:RadDatePicker ID="EndDate" runat="server">
          </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EndDate" 
          Display="Dynamic" ErrorMessage="*" />
        
        까지</td>
    </tr>
    <tr>
      <td class="label">
        상태</td>
      <td class="data">
        <asp:RadioButton ID="RadioPublish" runat="server" GroupName="Status" Text="게시" Checked="true" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="RadioPause" runat="server" GroupName="Status" Text="일시 중지" />
        <br />
        일시 중지를 선택하면 게시 기간 중이라도 팝업이 표시되지 않습니다.</td>
    </tr>
    <tr>
      <td class="label">
        크기</td>
      <td class="data">
        가로 <asp:TextBox ID="HorSize" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="HorSize" 
          Display="Dynamic" ErrorMessage="*" />
        <span class="sep24">세로</span><asp:TextBox ID="VertSize" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="VertSize" 
          Display="Dynamic" ErrorMessage="*" />
        <br />
        가로 세로 크기를 픽셀 단위로 입력하세요.
      </td>
    </tr>
    <tr>
      <td class="label">
        팝업내용</td>
      <td class="data">
        <CKEditor:CKEditorControl ID="CkEditor" runat="server"  Toolbar="Source
        Undo|Redo|Bold|Italic|Underline|Strike|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|Link|Table
      /
      NumberedList|BulletedList|-|Outdent|Indent|TextColor|BGColor
      /
      Styles|Format|Font|FontSize" 
            Width="100%" Height="300px">
        </CKEditor:CKEditorControl> 
      </td>
    </tr>
  </table>
  <br />
  <div class="cntr cmdPnl"> 
    <asp:Button runat="server" ID="AddButton" Text="등록" Width="120px" onclick="AddButton_Click" />
  </div>

</asp:Content>

