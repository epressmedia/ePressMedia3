<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Ad_Popups" Codebehind="Popups.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript">

    function hideMessageBox() {
      var m = $find('MsgBoxMpe');
      if (m)
        m.hide();
    }

  </script> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">

  <h1>팝업 관리</h1>
  <div class="cmdPnl">
  </div>
  <div class="cmdPnl">
    <asp:DropDownList ID="ViewList" runat="server" AutoPostBack="true" >
      <asp:ListItem Text="전체" Value="0" />
      <asp:ListItem Text="게시중" Value="1" />
    </asp:DropDownList>
    <span class="sep24"></span>
    <span class="sep24"></span>
    <a href="AddPopup.aspx" class="adminButton">팝업추가</a>
  </div>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
      <asp:Repeater ID="DataView" runat="server"  onitemcommand="DataView_ItemCommand">
        <HeaderTemplate>
          <table width="600">
            <thead class="label">
            <tr>
              <td class="label"><asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="true" 
                  oncheckedchanged="CheckAll_CheckedChanged" /></td>
              <td class="label">팝업명</td>
              <td class="label">게시기간</td>
              <td class="label">활성화</td>
              <td class="label">보기</td>
            </tr>
            </thead>
        </HeaderTemplate>
        <ItemTemplate>
          <tr>
            <td class="data" align="center"><asp:CheckBox ID="ItemCheck" runat="server" />
              <asp:Label ID="PopupId" runat="server" Visible="false" Text='<%# Eval("PopupId") %>' />
            </td>
            <td class="data">
              <asp:HyperLink ID="HyperLink1" runat="server" 
                  NavigateUrl='<%# Eval("PopupId", "EditPopup.aspx?id={0}") %>'><%# Eval("AdName") %> </asp:HyperLink></td>
            <td class="data" align="center"><%# Eval("ValidFrom", "{0:MM/dd/yyyy}") %>~<%# Eval("ValidTo", "{0:MM/dd/yyyy}") %></td>
            <td class="data" align="center"><asp:Checkbox ID="Checkbox1" runat="server" Enabled="false" Checked='<%# Eval("Enabled") %>' /></td>
            <td class="data" align="center">
              <asp:LinkButton ID="ViewLink" runat="server" 
                  CommandArgument='<%# Eval("PopupId") %>'>보기</asp:LinkButton>
            </td>
          </tr>
        </ItemTemplate>
        <FooterTemplate>
          </table>
        </FooterTemplate>
      </asp:Repeater>
      <div class="cmdPnl" style="width:600px;text-align:center">
      <uc1:FitPager ID="FitPager1" runat="server" Visible="false" OnPageNumberChanged="PageNumberChanged" />
      </div>
      
      <asp:Panel ID="PopBox" runat="server" CssClass="popupBox" style="display:none">
        <asp:Label ID="MsgText" runat="server" />
        <div class="center">
          <asp:CheckBox ID="CheckBox1" runat="server" Text=" 24시간동안 이창 다시 열지 않기" />
          <span class="sep24"></span>
          <input id="OkButton" runat="server" type="button" value=" 닫기 " onclick="javascript:hideMessageBox();" />
        </div>
      </asp:Panel>
      
      <cc1:ModalPopupExtender ID="MsgBoxMpe" runat="server" DynamicServicePath="" 
        Enabled="True" TargetControlID="MsgBoxTarget" PopupControlID="PopBox"
        BehaviorID="MsgBoxMpe" 
        BackgroundCssClass="modalBg"  />

      <asp:Button ID="MsgBoxTarget" runat="server" style="display:none" />
      
    </ContentTemplate>
  </asp:UpdatePanel>


</asp:Content>

