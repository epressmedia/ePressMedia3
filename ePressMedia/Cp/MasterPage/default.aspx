<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="ePressMedia.Cp.Pages.MasterPages" %>

    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    <h1>
        Master Page Configuration</h1>


              <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
            <div>
        <asp:Label ID="lbl_TempName" runat="server" Text="Master Page Name:" />
        <asp:DropDownList ID="ddl_master" runat="server"></asp:DropDownList>
        </div><div>
        <asp:Label ID="lbl_TempDescription" runat="server" Text="Description: " />
        <asp:TextBox ID="txt_TempDescription" runat="server" Width="300px" />
        <asp:RegularExpressionValidator runat="server" ID="rexNumber" ControlToValidate="txt_TempDescription" ValidationGroup = "description" ValidationExpression="^[a-zA-Z].*" ErrorMessage="Description cannot start with a number."></asp:RegularExpressionValidator>
        </div>
        <asp:Button ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" ValidationGroup = "description"/>
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
                PageSize="20"  DataKeyNames="MasterPageId">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="MasterPageId" DataType="System.Int32" HeaderText="MasterPageId" ShowFilterIcon="false" FilterControlWidth="100px" AutoPostBackOnFilter="true"
                        SortExpression="MasterPageId" UniqueName="MasterPageId">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" ShowFilterIcon="false" FilterControlWidth="300px" AutoPostBackOnFilter="true" FilterDelay="2000"
                        HeaderText="Name" SortExpression="Name" UniqueName="Name">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter Description column"  ShowFilterIcon="false" FilterControlWidth="300px" AutoPostBackOnFilter="true" FilterDelay="2000"
                        HeaderText="Description" SortExpression="Description" UniqueName="Description">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemTemplate>
<%--                            <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                CssClass="icon-5 info-tooltip" title="Edit" />--%>
                            <asp:LinkButton ID="ConfigLink" runat="server" CommandName="config" CommandArgument='<%# Eval("MasterPageId") %>'
                                class="icon-1 info-tooltip" title="Configure Page"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("MasterPageId") %>'
                                CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this master page?')){return false;}" />
                            
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                    <ItemTemplate>
                    <img id="MasterPage_image" src='<%# String.Format("../Img/Masters/{0}", Eval("Name").ToString().Replace(".master",".png")) %>' alt='<%# Eval("Name") %>' width="40px" />
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
            runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName="" OrderBy="MasterPageId"
            ResourceSetName="MasterPageConfigs"  
            OnUpdating="OpenAccessLinqDataSource1_Updating" Where="DeletedFg == @DeletedFg">
            <WhereParameters>
                <asp:Parameter DefaultValue="False" Name="DeletedFg" Type="Boolean" />
            </WhereParameters>
        </telerik:OpenAccessLinqDataSource>
    </div>
    </telerik:RadAjaxPanel>
</asp:Content>
