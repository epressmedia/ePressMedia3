<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Calendar_Calendars" Codebehind="default.aspx.cs" %>

<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
    <h1>
        Calendar</h1>

                    <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
        <asp:Label ID="lbl_calendar" runat="server" Text="Calendar Name:" />
        <asp:TextBox ID="txt_calendar" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="AddButton_Click" />
                </div>
                
            </div>
            <div style="clear:both"></div>
            <div id="listing_div"  runat="server">

        <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
            AllowSorting="True" CellSpacing="0" DataSourceID="OpenAccessLinqDataSource1"
            GridLines="None" OnItemCommand="RadGrid1_ItemCommand" AllowAutomaticUpdates="True"
            OnItemCreated="RadGrid1_ItemCreated" OnUpdateCommand ="RadGrid1_UpdateCommand">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="False" />
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" DataSourceID="OpenAccessLinqDataSource1"
                PageSize="20" DataKeyNames="CalId">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                                    <telerik:GridBoundColumn DataField="CalId" FilterControlAltText="Filter CalId column"  CurrentFilterFunction="EqualTo" ShowFilterIcon = "false" FilterControlWidth="80px"  AutoPostBackOnFilter="true"
                        HeaderText="Calendar ID" SortExpression="CalId" UniqueName="CalId" 
                        DataType="System.Int32" ReadOnly="True">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CalName" HeaderText="CalName"  CurrentFilterFunction="StartsWith" ShowFilterIcon = "false" FilterControlWidth="300px"  AutoPostBackOnFilter="true"
                        SortExpression="Calendar Name" UniqueName="CalName" 
                        FilterControlAltText="Filter CalName column">
                    </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                CssClass="icon-5 info-tooltip" title="Edit" />
                                                                   <asp:LinkButton ID="PermissionLink" runat="server" CommandName="permission" CommandArgument='<%#Eval("CalId").ToString()%>'
                                        class="icon-6 info-tooltip" title="Permission"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("CalId") %>'
                                CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this calendar?')){return false;}" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
           
                <PagerStyle PageSizeControlType="RadComboBox" />
           
            </MasterTableView>
   
            <PagerStyle PageSizeControlType="RadComboBox" />
   
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </telerik:RadGrid>
        <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" EnableUpdate="True"
            runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName=""
            ResourceSetName="Calendars" OnUpdating="OpenAccessLinqDataSource1_Updating">
        </telerik:OpenAccessLinqDataSource>
    </div>
    </telerik:RadAjaxPanel>



</asp:Content>



