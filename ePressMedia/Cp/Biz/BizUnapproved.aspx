<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Biz_BizUnapproved" Codebehind="BizUnapproved.aspx.cs" %>
<%@ Register src="~/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <h1>등록 승인 대기 업소</h1>

      <div class="cmdPnl">&nbsp;</div>
      <div class="cmdPnl">
        <span class="sep24">업소명</span>
        <asp:TextBox ID="SearchValue" runat="server" Width="120px" />
        <span class="sep12"></span>
        <asp:Button ID="SearchButton" runat="server" Width="64px" Text="검색" onclick="SearchButton_Click" />    
      </div>
      <asp:HiddenField ID="SortValue" runat="server" Value="BizId" />
      <asp:HiddenField ID="SortOrder" runat="server" Value="DESC" />
      <asp:Repeater ID="DataView" runat="server" 
          onitemcommand="DataView_ItemCommand" onitemdatabound="DataView_ItemDataBound">
        <HeaderTemplate>
          <table width="720">
            <thead class="label">
            <tr>
              <td class="label"><asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="true" oncheckedchanged="CheckAll_CheckedChanged" /></td>
              <td class="label" style="width:270px"><asp:LinkButton ID="SortBizName" runat="server" CommandName="BizName" CommandArgument="ASC" Text="업소명" /></td>
              <td class="label"><asp:LinkButton ID="SortBizId" runat="server" CommandName="BizId" CommandArgument="ASC" Text="등록일" /></td>
            </tr>
            </thead>
          </HeaderTemplate>
          <ItemTemplate>
            <tr>
              <td class="data" align="center"><asp:CheckBox ID="ItemCheck" runat="server" />
                  <asp:Label ID="BizId" runat="server" Visible="false" Text='<%# Eval("BizId") %>' />
              </td>
              <td class="data">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("BizId", "BizInfo.aspx?id={0}") + "&pn=" + FitPager1.CurrentPage %>' 
                  Text='<%# Eval("Name") %>' />
              </td>
              <td class="data" align="center"><%# Eval("RegDate", "{0:MM/dd/yyyy}") %></td>
            </tr>
          </ItemTemplate>

          <FooterTemplate>
              </table>
          </FooterTemplate>
      </asp:Repeater>
       
      <div class="cmdPnl">
        <uc:FitPager ID="FitPager1" runat="server" OnPageNumberChanged="PageNumberChanged" Visible="false" HorizontalAligh="Center" Width="700" />
      </div>  
      <div class="cmdPnl">
        <asp:Button runat="server" ID="ApproveButton" Text="선택업소 등록 승인" 
          onclick="ApproveButton_Click" />&nbsp;&nbsp;
        <span class="sep24"></span>
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


