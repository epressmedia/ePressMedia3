<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditEvent.ascx.cs" Inherits="ePressMedia.Calendar.Controls.EditEvent" %>


  <table class="postTbl">
    <tr>
      <td class="label">비밀번호 *</td>
      <td class="data">
        <asp:TextBox runat="server" ID="Password" Width="120px" TextMode="Password" MaxLength="8" />
        <asp:RequiredFieldValidator ID="Req7" runat="server" ControlToValidate="Password"
          ErrorMessage="*" Display="Dynamic" />
        <br />
        내용 수정시에 필요합니다 (최대 8자)
        </td>
    </tr>
    <tr>
      <td class="label">행사명*</td>
      <td class="data">
        <asp:TextBox runat="server" ID="EventTitle" Width="400px" />
        <asp:RequiredFieldValidator ID="SubjectReq" runat="server" ControlToValidate="EventTitle" 
            Display="Dynamic" ErrorMessage="*" />
      </td>
    </tr>
    <tr>
      <td class="label">날짜*</td>
      <td class="data">
          <telerik:RadDatePicker ID="StartDate" runat="server">
          </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="StartDate"
          ErrorMessage="*" Display="Dynamic" />&nbsp;부터&nbsp;
             <telerik:RadDatePicker ID="EndDate" runat="server">
          </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="EndDate"
          ErrorMessage="*" Display="Dynamic" />&nbsp;까지
      </td>
    </tr>
    <tr>
      <td class="label">시간</td>
      <td class="data">
        <asp:DropDownList runat="server" ID="HourList" >
          <asp:ListItem>07</asp:ListItem>
          <asp:ListItem>08</asp:ListItem>
          <asp:ListItem>09</asp:ListItem>
          <asp:ListItem>10</asp:ListItem>
          <asp:ListItem>11</asp:ListItem>
          <asp:ListItem>12</asp:ListItem>
          <asp:ListItem Selected="True">13</asp:ListItem>
          <asp:ListItem>14</asp:ListItem>
          <asp:ListItem>15</asp:ListItem>
          <asp:ListItem>16</asp:ListItem>
          <asp:ListItem>17</asp:ListItem>
          <asp:ListItem>18</asp:ListItem>
          <asp:ListItem>19</asp:ListItem>
          <asp:ListItem>20</asp:ListItem>
          <asp:ListItem>21</asp:ListItem>
          <asp:ListItem>22</asp:ListItem>
          <asp:ListItem>23</asp:ListItem>
          <asp:ListItem>0</asp:ListItem>
        </asp:DropDownList>
        &nbsp;시&nbsp;
        <asp:DropDownList runat="server" ID="MinuteList" >
          <asp:ListItem>00</asp:ListItem>
          <asp:ListItem>10</asp:ListItem>
          <asp:ListItem>20</asp:ListItem>
          <asp:ListItem>30</asp:ListItem>
          <asp:ListItem>40</asp:ListItem>
          <asp:ListItem>50</asp:ListItem>
        </asp:DropDownList>
        &nbsp;분
      </td>
    </tr>
    <tr>
      <td class="label">장소</td>
      <td class="data">
        <asp:TextBox runat="server" ID="HeldAt" Width="320px" MaxLength="60" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="HeldAt"
          ErrorMessage="*" Display="Dynamic" />
      </td>
    </tr>
    <tr>
      <td class="label">주소</td>
      <td class="data">
        <asp:TextBox runat="server" ID="Address" Width="320px" MaxLength="100" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Address"
          ErrorMessage="*" Display="Dynamic" />
        <br />
        행사 장소의 주소를 입력해 주세요
      </td>
    </tr>
    <tr>
      <td class="label">주최</td>
      <td class="data">
          <asp:TextBox runat="server" ID="Host" Width="320px" MaxLength="60" />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Address"
            ErrorMessage="*" Display="Dynamic" />
          <br />
          행사 주최 단체 또는 조직 명을 입력하세요
      </td>
    </tr>
    <tr>
      <td class="label">문의처</td>
      <td class="data">
        <asp:TextBox runat="server" ID="Contact" Width="320px" MaxLength="100" /><br />
        문의 전화번호, 담당자 명 등을 입력합니다.
      </td>
    </tr>
    <tr>
      <td class="label">관련 사이트</td>
      <td class="data">
        http://<asp:TextBox runat="server" ID="SiteUrl" Width="320px" MaxLength="100" /><br />
        관련 웹사이트가 있는 경우 주소를 입력하세요.
      </td>
    </tr>
    <tr>
      <td class="label">행사 설명</td>
      <td class="data" align="center">
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

  <div class="cntrPnl">
    <asp:LinkButton ID="PostLink" runat="server" CssClass="boxLnk" Text="저장" 
      onclick="PostLink_Click" />
    <span class="colSep"></span>
    <asp:HyperLink ID="ListLink" runat="server" CssClass="boxLnk" Text="취소" />    
  </div>

  <div class="cntrPnl">&nbsp;</div>