<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ePressMedia.Cp.UDF._default" %>
    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
            
            <h1>
                User Defined Fields</h1>
             <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            </div>
                    <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
            <asp:Label ID="lbl_ArticleName" runat= "server" Text="Name:" />   
                <asp:TextBox ID="txt_udfname" runat="server" />
                </div>
                                         <div>
            <asp:Label ID="lbl_desc" runat= "server" Text="Description:" />   
                <asp:TextBox ID="txt_descr" runat="server" />
                </div>
                        <div>
            <asp:Label ID="Label1" runat= "server" Text="Data Type:" />   
                <asp:DropDownList ID="ddl_datatype" runat="server" />
                </div>
       
                        <div>
            <asp:Label ID="lbl_Label" runat= "server" Text="Label:" />   
                <asp:TextBox ID="txt_Label" runat="server" />
                </div>
                        <div>
            <asp:Label ID="Label4" runat= "server" Text="Prefix:" />   
                <asp:TextBox ID="txt_prefix" runat="server" />
                </div>
                        <div>
            <asp:Label ID="Label5" runat= "server" Text="Postfix:" />   
                <asp:TextBox ID="txt_postfix" runat="server" />
                </div>
                
                        <div>
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
                        PageSize="10" DataKeyNames="UDFID">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="UDFID" FilterControlAltText="Filter UDFID column"
                                HeaderText="UDF ID" SortExpression="UDFID" UniqueName="UDFID" DataType="System.Int32" AllowFiltering ="False" CurrentFilterFunction="StartsWith">  
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn DataField="UDFDataType.DataTypeDescr" FilterControlAltText="Filter DataTypeName column"
                                HeaderText="Data Type" SortExpression="UDFDataType.DataTypeDescr" UniqueName="DataTypeName" DataType="System.String" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay = "1000" CurrentFilterFunction="StartsWith" >  
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UDFName" Visible="true" FilterControlAltText="Filter UDFName column" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay = "1000" CurrentFilterFunction="StartsWith" 
                                HeaderText="UDF Name" SortExpression="UDFName" UniqueName="UDFName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UDFDescription" Visible="true" FilterControlAltText="Filter UDFDescription column" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay = "1000" CurrentFilterFunction="StartsWith" 
                                HeaderText="UDF Desc" SortExpression="UDFDescription" UniqueName="UDFDescription">
                            </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="Label" Visible="true" FilterControlAltText="Filter Label column" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay = "1000" CurrentFilterFunction="StartsWith" 
                                HeaderText="Label" SortExpression="Label" UniqueName="Label">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PrefixLabel" Visible="true" FilterControlAltText="Filter PrefixLabel column" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay = "1000" CurrentFilterFunction="StartsWith" 
                                HeaderText="Prefix" SortExpression="PrefixLabel" UniqueName="PrefixLabel">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PostfixLabel" Visible="true" FilterControlAltText="Filter PostfixLabel column" ShowFilterIcon = "false" AutoPostBackOnFilter="true" FilterDelay = "1000" CurrentFilterFunction="StartsWith" 
                                HeaderText="Postfix" SortExpression="PostfixLabel" UniqueName="PostfixLabel">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="ReferenceFg" HeaderText="Reference?" SortExpression="ReferenceFg"
                                UniqueName="ReferenceFg" FilterControlAltText="Filter Link Article column" ShowFilterIcon ="false" AllowFiltering = "false">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions<a/>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                      CssClass="icon-5 info-tooltip" title="Edit" />                                 
                                    <asp:LinkButton ID="ConfigLink" runat="server" CommandName = "config" CommandArgument='<%# Eval("UDFID") %>'
                                        class="icon-1 info-tooltip" title="Configure Page" Visible='<%# (bool.Parse(Eval("ReferenceFg").ToString())) ?true:false %>'></asp:LinkButton>
                                                                        <asp:LinkButton ID="DeleteLink" runat="server" CommandName="delete" CommandArgument='<%# Eval("UDFID") %>'
                                        CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this UDF?')){return false;}" />
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
                <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EnableUpdate ="true"
                    EntityTypeName="" OrderBy="UDFID" ResourceSetName="UDFInfos">

                </telerik:OpenAccessLinqDataSource>
            </div>
</telerik:RadAjaxPanel>
</asp:Content>
