<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ePressMedia.Cp.Comment._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
       

         
            <h1>
                Comment Management</h1>
             <div>

            <div id="listing_div"  runat="server">
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                    AllowSorting="True" CellSpacing="0" DataSourceID="OpenAccessLinqDataSource1"
                    GridLines="None" OnItemCommand="RadGrid1_ItemCommand"  OnItemDataBound="RadGrid1_ItemDataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="False" DataSourceID="OpenAccessLinqDataSource1"
                        PageSize="50" DataKeyNames="Id">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>

                             <telerik:GridBoundColumn DataField="ContentTypeName" Visible="true" FilterControlAltText="Filter ContentTypeName column"
                                HeaderText="ContentTypeName" SortExpression="ContentTypeName" UniqueName="ContentTypeName" ShowFilterIcon = "false" CurrentFilterFunction = "EqualTo" AutoPostBackOnFilter="true"
                              ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SrcId" Visible="true" FilterControlAltText="Filter CatId column"
                                HeaderText="Content ID" SortExpression="SrcId" UniqueName="SrcId" ShowFilterIcon = "false" CurrentFilterFunction = "EqualTo" AutoPostBackOnFilter="true"
                                DataType="System.Int32" ReadOnly="True">
                            </telerik:GridBoundColumn>

                            <telerik:GridHyperLinkColumn HeaderText ="Link" DataTextField="Link" AllowSorting ="false" AllowFiltering ="false">

                            </telerik:GridHyperLinkColumn>
                                                                 <telerik:GridBoundColumn DataField="Comment" Visible="true" FilterControlAltText="Filter Comment column"
                                HeaderText="Comment" SortExpression="Comment" UniqueName="Comment" ShowFilterIcon = "false" CurrentFilterFunction = "EqualTo" AutoPostBackOnFilter="true"
                              ReadOnly="True">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PostBy" Visible="true" FilterControlAltText="Filter PostBy column"
                                HeaderText="Posted By" SortExpression="PostBy" UniqueName="PostBy" ShowFilterIcon = "false" CurrentFilterFunction = "EqualTo" AutoPostBackOnFilter="true"
                              ReadOnly="True">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PostDate" Visible="true" FilterControlAltText="Filter PostDate column"
                                HeaderText="Posted Date" SortExpression="PostDate" UniqueName="PostDate" ShowFilterIcon = "false" CurrentFilterFunction = "EqualTo" AutoPostBackOnFilter="true"
                             ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IPAddr" FilterControlAltText="Filter CatName column"
                                HeaderText="IP Address" SortExpression="IPAddr" UniqueName="IPAddr" ShowFilterIcon = "false" CurrentFilterFunction = "StartsWith" AutoPostBackOnFilter="true" >  
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions<a/>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%#Eval("Id") + ";" +Eval("ContentTypeId")%>'
                                        CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this comment?')){return false;}" />
                                        
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
                <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel"
                    EntityTypeName="" ResourceSetName="EPM_vw_comments" 
                    EnableUpdate="True" OrderBy="PostDate desc" Where="Blocked == @Blocked">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="false" Name="Blocked" Type="Boolean" />
                    </WhereParameters>
                </telerik:OpenAccessLinqDataSource>
            </div>



    </telerik:RadAjaxPanel> 
</asp:Content>