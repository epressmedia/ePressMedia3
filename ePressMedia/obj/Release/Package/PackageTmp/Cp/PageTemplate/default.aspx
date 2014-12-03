<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="ePressMedia.Cp.Pages.PageTemplates" %>
    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
            
    <h1>
        Page Templates</h1>

                    <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
        <asp:Label ID="lbl_TempName" runat="server" Text="Category Name:" />
        <asp:TextBox ID="txt_TempName" runat="server" />
        <asp:Label ID="lbl_TempDescription" runat="server" Text="Description: " />
        <asp:TextBox ID="txt_TempDescription" runat="server" Width="300px" />
        <asp:Button ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" />
                </div>
                
            </div>
            <div style="clear:both"></div>
            <div id="listing_div"  runat="server">

        <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
            AllowSorting="True" CellSpacing="0" DataSourceID="OpenAccessLinqDataSource1"
            GridLines="None" OnItemCommand="RadGrid1_ItemCommand" AllowAutomaticUpdates="True"
            OnItemCreated="RadGrid1_ItemCreated">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="False" />
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" DataSourceID="OpenAccessLinqDataSource1"
                PageSize="20" DataKeyNames="TemplateId">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="TemplateId" DataType="System.Int32" HeaderText="Template ID" CurrentFilterFunction="EqualTo" ShowFilterIcon = "false" FilterControlWidth="80px"  AutoPostBackOnFilter="true"
                        SortExpression="TemplateId" UniqueName="TemplateId">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" CurrentFilterFunction="StartsWith" ShowFilterIcon = "false" FilterControlWidth="300px"  AutoPostBackOnFilter="true" FilterDelay="2000"
                        HeaderText="Name" SortExpression="Name" UniqueName="Name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter Description column" CurrentFilterFunction="StartsWith" ShowFilterIcon = "false" FilterControlWidth="400px"  AutoPostBackOnFilter="true" FilterDelay="2000"
                        HeaderText="Description" SortExpression="Description" UniqueName="Description">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                CssClass="icon-5 info-tooltip" title="Edit" />
                            <asp:LinkButton ID="ConfigLink" runat="server" CommandName = "config" CommandArgument='<%# Eval("TemplateId") %>'
                                class="icon-1 info-tooltip" title="Configure Page"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("TemplateId") %>'
                                CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this template?')){return false;}" />
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
        <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" EnableUpdate="true"
            runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName="" OrderBy="TemplateId"
            ResourceSetName="PageTemplates" Where="Deleted_fg == @Deleted_fg" OnUpdating="OpenAccessLinqDataSource1_Updating">
            <WhereParameters>
                <asp:Parameter DefaultValue="false" Name="Deleted_fg" Type="Boolean" />
            </WhereParameters>
        </telerik:OpenAccessLinqDataSource>
    </div>
    </telerik:RadAjaxPanel>
</asp:Content>
