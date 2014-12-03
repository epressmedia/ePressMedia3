<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Biz_BizModUnapproved" Codebehind="BizModUnapproved.aspx.cs" %>

<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <h1>정보 수정 승인 대기 업소</h1>


      <div class="cmdPnl">
      &nbsp;
      </div>
      <asp:HiddenField ID="SortValue" runat="server" Value="RegDate" />
      <asp:HiddenField ID="SortOrder" runat="server" Value="DESC" />
      <asp:Repeater ID="DataView" runat="server" 
          onitemcommand="DataView_ItemCommand" onitemdatabound="DataView_ItemDataBound">
        <HeaderTemplate>
          <table width="720">
            <thead class="label">
            <tr>
              <td class="label"><asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="true" oncheckedchanged="CheckAll_CheckedChanged" /></td>
              <td class="label" style="width:270px"><asp:LinkButton ID="SortBizName" runat="server" CommandName="BizName" CommandArgument="ASC" Text="업소명" /></td>
              <td class="label"><asp:LinkButton ID="SortRegDate" runat="server" CommandName="RegDate" CommandArgument="ASC" Text="수정 요청일" /></td>
              <td class="label">기존 정보 보기</td>
            </tr>
            </thead>
          </HeaderTemplate>
          <ItemTemplate>
            <tr>
              <td class="data" align="center"><asp:CheckBox ID="ItemCheck" runat="server" />
                  <asp:Label ID="BizId" runat="server" Visible="false" Text='<%# Eval("BizId") %>' />
                  <asp:Label ID="ReqId" runat="server" Visible="false" Text='<%# Eval("ReqId") %>' />
              </td>
              <td class="data">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ReqId", "BizModInfo.aspx?id={0}") %>' 
                  Text='<%# Eval("Name") %>' />
              </td>
              <td class="data" align="center"><%# Eval("RegDate", "{0:MM/dd/yyyy HH:mm}") %></td>
              <td class="data" align="center">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("BizId", "~/Biz/Biz.aspx?id={0}") %>' Target="_blank" Text="보기" />
              </td>
            </tr>
          </ItemTemplate>

          <FooterTemplate>
              </table>
          </FooterTemplate>
      </asp:Repeater>
       
<%--      <div class="cmdPnl">
        <uc:FitPager ID="FitPager1" runat="server" OnPageNumberChanged="PageNumberChanged" Visible="false" HorizontalAligh="Center" Width="700" />
      </div>  --%>
      <div class="cmdPnl">
        <asp:Button runat="server" ID="ApproveButton" Text="정보 수정 승인" 
          onclick="ApproveButton_Click" />&nbsp;&nbsp;
        <span class="sep24"></span>
        <asp:Button runat="server" ID="DeleteButton" Text="수정 요청 삭제" 
          onclick="DeleteButton_Click" />&nbsp;&nbsp;
        <span class="sep24"></span>
      </div>

      <asp:Panel ID="MsgBox" runat="server" CssClass="confirmBox" style="display:none">
        <asp:Label ID="MsgText" runat="server" />
        <br /><br />
        <input id="OkButton" runat="server" type="button" value=" 확인 " onclick="javascript:hideMessageBox();" />
      </asp:Panel>
      
      <cc1:modalpopupextender ID="MsgBoxMpe" runat="server" DynamicServicePath="" 
        Enabled="True" TargetControlID="MsgBoxTarget" PopupControlID="MsgBox"
        BehaviorID="MsgBoxMpe" 
        BackgroundCssClass="modalBg"  />

      <asp:Button ID="MsgBoxTarget" runat="server" style="display:none" />

</asp:Content>


