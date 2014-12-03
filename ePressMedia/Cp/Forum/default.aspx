<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    Inherits="Cp_Forum_Forums" CodeBehind="default.aspx.cs" %>

    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID ="RadAjaxLoadingPanel1" >
        
            
            <h1>
                Forum Category</h1>
             <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            </div>
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
            <asp:Label ID="lbl_ForumName" runat= "server" Text="Forum Name:" />   
                <asp:TextBox ID="ForumName" runat="server" />
                </div>
                <div>
            <asp:Label ID="lbl_forum_description" runat= "server" Text="Description:" />   
                <asp:TextBox ID="txt_description" runat="server" width="400px"/>
                <asp:Button ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" />
                </div>
                
                
            </div>
            <div style="clear:both"></div>
            <div id="listing_div"  runat="server">
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                    AllowSorting="True" CellSpacing="0" DataSourceID="OpenAccessLinqDataSource1"
                    GridLines="None" OnItemCommand="RadGrid1_ItemCommand" OnItemCreated="RadGrid1_ItemCreated"
                    OnUpdateCommand="RadGrid1_UpdateCommand" >
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="False" DataSourceID="OpenAccessLinqDataSource1"
                        PageSize="20" DataKeyNames="ForumId" CommandItemDisplay="Top" EditMode="InPlace">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ForumId" FilterControlAltText="Filter ForumId column"
                                HeaderText="ID" SortExpression="ForumId" UniqueName="ForumId" DataType="System.Int32" CurrentFilterFunction="EqualTo" ShowFilterIcon = "false" FilterControlWidth="100px"  AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ForumName" Visible="true" FilterControlAltText="Filter ForumName column" CurrentFilterFunction = "StartsWith" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay="2000" FilterControlWidth="300px"
                                HeaderText="Forum Name" SortExpression="ForumName" UniqueName="ForumName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"  CurrentFilterFunction = "StartsWith" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay="2000" FilterControlWidth="300px"
                                UniqueName="Description" FilterControlAltText="Filter Description column">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions<a/>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                      CssClass="icon-5 info-tooltip" title="Edit" />
                                    <asp:LinkButton ID="DetailLink" runat="server" CommandName="properties" CommandArgument='<%#Eval("ForumId").ToString()%>'
                                        CssClass="icon-7 info-tooltip" title="Properties" />                                      
                                    <asp:LinkButton ID="ConfigLink" runat="server" CommandName="config" CommandArgument='<%#Eval("ForumId").ToString()%>'
                                        class="icon-1 info-tooltip" title="Configure Page"></asp:LinkButton>
                                    <asp:LinkButton ID="PermissionLink" runat="server" CommandName="permission" CommandArgument='<%#Eval("ForumId").ToString()%>'
                                        class="icon-6 info-tooltip" title="Permission"></asp:LinkButton>
                                    <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("ForumId") %>'
                                        CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this forum?')){return false;}" />
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
                <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel"
                    EntityTypeName="" OrderBy="ForumID" ResourceSetName="Forums" EnableUpdate="true">
                </telerik:OpenAccessLinqDataSource>
            </div>
    </telerik:RadAjaxPanel>
</asp:Content>
