<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Article_NewsLinks" Codebehind="NewsLinks.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../../Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
  .lnkLst { list-style:none; line-height:20pt; }
  
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
  
<h1>External Article Link -  <asp:Label ID="lbl_catname" runat="server" Text="" /></h1>
<div class="backLink">
<asp:Button ID="lb_back" Text="<< Back To Listing" runat="server" PostBackUrl="/CP/Article/ArticleCategories.aspx"></asp:Button>
</div>
    <cc1:ModalPopupExtender ID="ConfirmMpe" runat="server" DynamicServicePath="" 
      Enabled="True" TargetControlID="ConfirmBoxTarget" PopupControlID="ModConfirmPanel" 
      BehaviorID="ConfirmMpe" 
      CancelControlID="CancelButton"
      BackgroundCssClass="modalBg" />
      <asp:Button ID="ConfirmBoxTarget" runat="server" style="display:none" />
      <asp:Panel ID="ModConfirmPanel" runat="server" CssClass="confirmBox" style="display:none">
    <div style="text-align:left">
      <h2>Edit Article Link</h2>
      <br />
      Title: <asp:TextBox ID="NewTitle" runat="server" MaxLength="128" Width="400px" /><br />
      URL: http://<asp:TextBox ID="NewURL" runat="server" MaxLength="256" Width="600px" /><br />

      </div>
      <div style="text-align:right">
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="NewURL" ErrorMessage="Wrong URL Format" ValidationExpression="[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$" ValidationGroup = "Edit"></asp:RegularExpressionValidator>
            <asp:Button ID="ModButton" runat="server" Text=" Update " onclick="ModButton_Click"   ValidationGroup = "Edit"/>
      <asp:Button ID="CancelButton" runat="server" Text=" Cancel " 
            OnClientClick="javascript:hideMessageBox();" onclick="CancelButton_Click"/>
      </div>
      <br />
    </asp:Panel>  
    
<div class="cmdPnl" style="line-height:20pt; float:right; text-align:right;">
  Article Title : <asp:TextBox ID="Subject" runat="server" MaxLength="128" Width="400px" /> <br />
  Link URL : http://<asp:TextBox ID="LinkUrl" runat="server" MaxLength="256" Width="600px" /> 
  <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="LinkUrl" ErrorMessage="Wrong URL Format" ValidationExpression="[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$" ValidationGroup = "Add"></asp:RegularExpressionValidator>
  <asp:Button ID="AddButton" runat="server" Text="Add"  ValidationGroup="Add"
    onclick="AddButton_Click" />
</div>
<div>
<asp:HiddenField ID="ArtId" runat="server" />
  <asp:Repeater ID="Repeater1" runat="server" onitemcommand="Repeater1_ItemCommand">
  <HeaderTemplate>
        <table id="product-table" style="width:100%">
        <tr>
          <th class="table-header-repeat line-left" style="width:200px"><a href="#">Date</a></th>
          <th class="table-header-repeat line-left"><a href="#">Title</a></th>
          <th class="table-header-repeat line-left" ><a href="#">URL</a></th>
          <th class="table-header-repeat line-left" style="width:100px"><a href="#">Options</a></th>
        </tr>
  </HeaderTemplate>
    <ItemTemplate>
      <tr>
      <td>
      <asp:HiddenField ID="LinkId" runat="server" Value='<%# Eval("ArticleId") %>' />
        <asp:Label ID="Label1" runat="server" Width="60px" Text='<%# Eval("PostDate", "{0:yyyy/MM/dd}") %>' />
        </td>
        <td>
         <asp:Label ID="lbl_title" runat="server" Text = '<%# Eval("Title") %>'></asp:Label>  
        </td>
        
        <td>
        <asp:HyperLink ID="ViewLink" runat="server" Target="_blank" Text='<%# Eval("LinkArticle_URL") %>'
            NavigateUrl='<%# Eval("LinkArticle_URL","http://{0}") %>' />
            </td>
            <td>
                <asp:LinkButton ID="EditLink"  runat="server" CommandName="mod" CommandArgument='<%# Eval("ArticleId") %>'  Cssclass="icon-5 info-tooltip" title="Edit" />
                  <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("ArticleId") %>' Cssclass="icon-2 info-tooltip"  title="Delete"/>
            </td>
      </tr>    
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
  </asp:Repeater>
</div>
<br />
  <uc1:FitPager ID="FitPager1" runat="server" RowsPerPage="10" OnPageNumberChanged="Page_Changed" Visible  = "false"/>
  </ContentTemplate>
  </asp:UpdatePanel>


</asp:Content>

