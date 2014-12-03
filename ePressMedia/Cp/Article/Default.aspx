<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ePressMedia.Cp.Article.Articles" %>
    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server"> 
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
            
            <h1>
                Article Category</h1>
             <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            </div>
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
            <asp:Label ID="lbl_ArticleName" runat= "server" Text="Article Name:" />   
                <asp:TextBox ID="ArticleName" runat="server" />
                </div>
                <div>
                <asp:RadioButton ID="rdb_normal" Text="Normal" runat="server"  GroupName="cattype" Checked="true"/>
                <asp:RadioButton ID="rdb_link" Text="Link" runat="server" GroupName="cattype"/>
                <asp:RadioButton ID="rdb_virtual" Text="Virtual" runat="server" GroupName="cattype"/>
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
                        PageSize="10" DataKeyNames="ArtCatId">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ArtCatId" FilterControlAltText="Filter ArtCatId column"
                                HeaderText="Article Category ID" SortExpression="ArtCatId" UniqueName="ArtCatId" DataType="System.Int32" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterDelay="2000" CurrentFilterFunction="StartsWith">  
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CatName" Visible="true" FilterControlAltText="Filter ArticleName column" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay = "1000" CurrentFilterFunction="StartsWith" FilterControlWidth="300px"
                                HeaderText="Category Name" SortExpression="CatName" UniqueName="CatName">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="LinkArticle_fg" HeaderText="Link Article" SortExpression="LinkArticle_fg"
                                UniqueName="LinkArticle_fg" FilterControlAltText="Filter Link Article column" ShowFilterIcon ="false" AllowFiltering = "false">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="VirtualCat_fg" HeaderText="Virtual" SortExpression="VirtualCat_fg"
                                UniqueName="VirtualCat_fg" FilterControlAltText="Filter Virtual Cat column" ShowFilterIcon ="false" AllowFiltering="false">
                                </telerik:GridCheckBoxColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions<a/>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                      CssClass="icon-5 info-tooltip" title="Edit" />                                 
                                    <asp:LinkButton ID="ConfigLink" runat="server" CommandName = "config" CommandArgument='<%# Eval("ArtCatId") %>'
                                        class="icon-1 info-tooltip" title="Configure Page" Visible='<%# (bool.Parse(Eval("VirtualCat_fg").ToString()) || bool.Parse(Eval("LinkArticle_fg").ToString())) ?false:true %>'></asp:LinkButton>
                                    <asp:LinkButton ID="PermissionLink" runat="server" CommandName="permission"  CommandArgument='<%# Eval("ArtCatId") %>'
                                        class="icon-6 info-tooltip" title="Page Permission" Visible='<%#(bool.Parse(Eval("VirtualCat_fg").ToString()) || bool.Parse(Eval("LinkArticle_fg").ToString())) ?false:true %>'></asp:LinkButton>
                                    <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("ArtCatId") %>'
                                        CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this article category?')){return false;}" />
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
                    EntityTypeName="" OrderBy="ArtCatId" ResourceSetName="ArticleCategories" 
                    EnableUpdate="True" Where="Deleted_fg == @Deleted_fg">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="false" Name="Deleted_fg" Type="Boolean" />
                    </WhereParameters>
                </telerik:OpenAccessLinqDataSource>
            </div>
</telerik:RadAjaxPanel>
</asp:Content>
