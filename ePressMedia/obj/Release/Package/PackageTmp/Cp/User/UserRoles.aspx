<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp.User.CpUserUserRoles" Codebehind="UserRoles.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <h1>User Roles</h1>

    <div class="cmdPnl">
    User Role Name: 
    <asp:TextBox ID="RoleName" runat="server" />
    <asp:Button ID="BtnAdd" runat="server" Text="Add User Role" onclick="BtnAdd_Click" /> 
  </div>
                 <telerik:RadGrid ID="RadGrid1" runat="server" 
        AllowFilteringByColumn="True" AllowPaging="True"
                    AllowSorting="True" CellSpacing="0" DataSourceID="OpenAccessLinqDataSource1"
                    GridLines="None" OnItemCommand="RadGrid1_ItemCommand" AutoGenerateColumns="False" 
                    Width="30%">

                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                    <MasterTableView DataSourceID="OpenAccessLinqDataSource1"
                        PageSize="10"  DataKeyNames="RoleID">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                              <telerik:GridBoundColumn DataField="RoleName" HeaderText="User Role Name" SortExpression="RoleName" CurrentFilterFunction="StartsWith" ShowFilterIcon="false" FilterControlWidth="300px" AutoPostBackOnFilter = "true"
                                UniqueName="RoleName" FilterControlAltText="Filter RoleName column" 
                                ReadOnly="True">
                            </telerik:GridBoundColumn>

        
                              <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteLink" runat="server"  CommandName="del" CommandArgument='<%# Eval("RoleName") %>'
                                        CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this user role?')){return false;}" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                       
                    </MasterTableView>
                  
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" 
        runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel"
                    EntityTypeName="" ResourceSetName="UserRoles" 
        Select="new (Description, RoleId, RoleName )" >
                </telerik:OpenAccessLinqDataSource>
                    <telerik:RadWindowManager ID="win_manager" runat="server">
    </telerik:RadWindowManager>
                        </ContentTemplate>
                        

    </asp:UpdatePanel>

</asp:Content>


