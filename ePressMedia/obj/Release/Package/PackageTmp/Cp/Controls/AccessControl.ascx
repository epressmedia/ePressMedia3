<%@ Control Language="C#" AutoEventWireup="True" Inherits="Cp_Controls_AccessControl" Codebehind="AccessControl.ascx.cs" %>
  

  <asp:HiddenField ID="ResType" runat="server" />
  <asp:HiddenField ID="ResId" runat="server" />
    <div class="cmdPnl" style="text-align:right">
    <asp:Label ID = "lblGroup" runat="server" Text="Group Name:"></asp:Label>
    <asp:DropDownList ID="UserGroupList" runat="server" />
    <asp:Button ID="AddButton" runat="server" Text="Add Group" 
      onclick="AddButton_Click" />
    </div>

  <table id="product-table">
    <tr>
      <th class="table-header-repeat line-left" style="text-align:center" ><a href="#">&nbsp;</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">User Role Name</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">Full Access</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">View List</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">View Content</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">Write Content</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">Write Comment</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">Edit Content</a></th>
      <th class="table-header-repeat line-left" style="text-align:center"><a href="#">Delete Content</a></th>
    </tr>
    <asp:Repeater ID="DataRepeater" runat="server">
      <ItemTemplate>
        <tr>
          <td >
            <asp:CheckBox ID="ChkSelected" runat="server" />
            <asp:HiddenField ID="AcId" runat="server" Value='<%# Eval("Id") %>' />
          </td>        
          <td class="data"><asp:Literal ID="GroupName" runat="server" Text='<%# Eval("PrincipalName") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="HasFullControl" CssClass="HasFullControl" runat="server" Checked='<%# Eval("HasFullControl") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanList" CssClass="CanList" runat="server" Checked='<%# Eval("CanList") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanRead" CssClass="CanRead" runat="server" Checked='<%# Eval("CanRead") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanWrite" CssClass ="CanWrite" runat="server" Checked='<%# Eval("CanWrite") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanComment" CssClass ="CanComment" runat="server" Checked='<%# Eval("CanComment") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanModify" CssClass ="CanModify" runat="server" Checked='<%# Eval("CanModify") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanDelete" CssClass ="CanDelete" runat="server" Checked='<%# Eval("CanDelete") %>' /></td>        
        </tr>
      </ItemTemplate>
      <AlternatingItemTemplate>
        <tr>
          <td>
            <asp:CheckBox ID="ChkSelected" runat="server" />
            <asp:HiddenField ID="AcId" runat="server" Value='<%# Eval("Id") %>' />
          </td>        
          <td class="altdata"><asp:Literal ID="GroupName" runat="server" Text='<%# Eval("PrincipalName") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="HasFullControl" CssClass="HasFullControl"  runat="server" Checked='<%# Eval("HasFullControl") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanList" CssClass="CanList" runat="server" Checked='<%# Eval("CanList") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanRead" CssClass="CanRead" runat="server" Checked='<%# Eval("CanRead") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanWrite" CssClass ="CanWrite" runat="server" Checked='<%# Eval("CanWrite") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanComment" CssClass ="CanComment" runat="server" Checked='<%# Eval("CanComment") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanModify" CssClass ="CanModify" runat="server" Checked='<%# Eval("CanModify") %>' /></td>        
          <td align="center" class="data"><asp:CheckBox ID="CanDelete" CssClass ="CanDelete" runat="server" Checked='<%# Eval("CanDelete") %>' /></td>        
        </tr>
      </AlternatingItemTemplate>
    </asp:Repeater>
  </table>
      <div class="cmdPnl" style="text-align:right">
    <asp:Button ID="SaveButton" runat="server" Text="Save Changes" 
      onclick="SaveButton_Click" />
    
    <asp:Button ID="DelButton" runat="server" Text="Delete" 
      onclick="DelButton_Click" />
  </div>

