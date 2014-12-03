<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="True" Inherits="Cp_Classified_ClassifiedCategories" Codebehind="default.aspx.cs" %>

<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
       

         
            <h1>
                Classified Category</h1>
             <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            </div>
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
            <asp:Label ID="lbl_ArticleName" runat= "server" Text="Category Name:" />   
                <asp:TextBox ID="txt_cls_cat_name" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="Add" OnClick="AddButton_Click" />
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
                        PageSize="20" DataKeyNames="CatId">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>

                            <telerik:GridBoundColumn DataField="CatId" Visible="true" FilterControlAltText="Filter CatId column"
                                HeaderText="ID" SortExpression="CatId" UniqueName="CatId" ShowFilterIcon = "false" CurrentFilterFunction = "EqualTo" AutoPostBackOnFilter="true"
                                DataType="System.Int32" ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CatName" FilterControlAltText="Filter CatName column"
                                HeaderText="Category Name" SortExpression="CatName" UniqueName="CatName" ShowFilterIcon = "false" CurrentFilterFunction = "StartsWith" AutoPostBackOnFilter="true" FilterControlWidth = "300px">  
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions<a/>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                      CssClass="icon-5 info-tooltip" title="Edit" />      
                                      <asp:LinkButton ID="DetailLink" runat="server" CommandName="properties" CommandArgument='<%#Eval("CatId").ToString()%>'
                                        CssClass="icon-7 info-tooltip" title="Properties" />                                  
                                    <asp:LinkButton ID="ConfigLink" runat="server" CommandName = "config" CommandArgument='<%# Eval("CatId") %>'
                                        class="icon-1 info-tooltip" title="Configure Page" ></asp:LinkButton>
                                    <asp:LinkButton ID="PermissionLink" runat="server" CommandName="permission"  CommandArgument='<%# Eval("CatId") %>'
                                        class="icon-6 info-tooltip" title="Page Permission" ></asp:LinkButton>
                                    <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("CatId") %>'
                                        CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this clasified category?')){return false;}" />
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
                    EntityTypeName="" ResourceSetName="ClassifiedCategories" 
                    EnableUpdate="True">
                </telerik:OpenAccessLinqDataSource>
            </div>



</telerik:RadAjaxPanel> 
</asp:Content>

