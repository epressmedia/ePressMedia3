<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="ePressMedia.Cp.Pages.CustomPages" %>
    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
    <h1>
        Custom Page</h1>


        <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
                    <asp:Label ID="lbl_TempName" runat="server" Text="Page Name:" />
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
                PageSize="20"  DataKeyNames="CustomPageId">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="CustomPageId" DataType="System.Int32" HeaderText="CustomPageId"
                        SortExpression="CustomPageId" UniqueName="CustomPageId" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlWidth = "100px" AndCurrentFilterFunction ="EqualTo">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlWidth = "300px" AndCurrentFilterFunction ="StartsWith"
                        HeaderText="Name" SortExpression="Name" UniqueName="Name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter Description column" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlWidth = "400px" AndCurrentFilterFunction ="StartsWith"
                        HeaderText="Description" SortExpression="Description" UniqueName="Description">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                CssClass="icon-5 info-tooltip" title="Edit" />
                                                                    <asp:LinkButton ID="DetailLink" runat="server" CommandName="properties" CommandArgument='<%#Eval("CustomPageId").ToString()%>'
                                        CssClass="icon-7 info-tooltip" title="Properties" />                  
                            <asp:LinkButton ID="ConfigLink" runat="server" CommandName ="config" CommandArgument='<%# Eval("CustomPageId") %>' 
                                class="icon-1 info-tooltip" title="Configure Page"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("CustomPageId") %>'
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
        <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" EnableUpdate="True"
            runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName="" OrderBy="CustomPageId"
            ResourceSetName="CustomPages"  
            OnUpdating="OpenAccessLinqDataSource1_Updating" Where="DeletedFg == @DeletedFg">
            <WhereParameters>
                <asp:Parameter DefaultValue="False" Name="DeletedFg" Type="Boolean" />
            </WhereParameters>
 
        </telerik:OpenAccessLinqDataSource>
    </div>
    </telerik:RadAjaxPanel>
</asp:Content>
