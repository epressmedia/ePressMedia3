<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_PrevIssues" Codebehind="PrevIssues.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <h1>지난 신문 보기 관리</h1>

  <div class="cmdPnl">
  * 정보를 수정하려면 제목을 클릭하세요.
  이미지를 클릭하면 신문보기 창을 띄웁니다.오레곤 바로보기 설정은 마지막 항목을 이용하세요.
  </div>
  <table>
    <asp:Repeater ID="Repeater1" runat="server">
      <ItemTemplate>
        <tr>
          <td align="center">
            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" 
              NavigateUrl='<%# Eval("Url") %>'><asp:Image ID="ImgRecent" runat="server" Height="60px"
              ImageUrl='<%# Eval("ImageUrl") %>' AlternateText='<%# Eval("Title") %>' 
              ToolTip='<%# Eval("Title") %>' /></asp:HyperLink>
          </td>
          <td>
            <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl='<%# Eval("Id", "SetPrevIssue.aspx?id={0}") %>' Text='<%# Eval("Title") %>' />
          </td>        
        </tr>      
      </ItemTemplate>
    </asp:Repeater>
  </table>
</asp:Content>

