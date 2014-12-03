<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ePressMedia.Cp.Form._default" %>
<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    
    <h1>
        Forms</h1>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
                    <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            
            <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
        <asp:Label ID="lbl_formname" runat="server" Text="Form Name:" />
        <asp:TextBox ID="txt_formname" runat="server" />
                </div><div>
                            <asp:Label ID="lbl_form_description" runat="server" Text="Description:" />
        <asp:TextBox ID="txt_form_description" runat="server" />
                      </div>
                <div>
                            
        <asp:CheckBox ID="chk_captcha" runat="server"  Text="Show Captcha"/>
                      </div>
                <div>
        <asp:Button ID="btn_add" runat="server" Text="Add" OnClick="btn_add_Click" />
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
                PageSize="20" DataKeyNames="FormId">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                                    <telerik:GridBoundColumn DataField="FormId" FilterControlAltText="Filter CalId column"  CurrentFilterFunction="EqualTo" ShowFilterIcon = "false" FilterControlWidth="80px"  AutoPostBackOnFilter="true"
                        HeaderText="Form ID" SortExpression="FormId" UniqueName="FormId" 
                        DataType="System.Int32" ReadOnly="True">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FormName" HeaderText="Form Name"  CurrentFilterFunction="StartsWith" ShowFilterIcon = "false" FilterControlWidth="300px"  AutoPostBackOnFilter="true"
                        SortExpression="FormName" UniqueName="FormName" 
                        FilterControlAltText="Filter FormName column">
                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="FormDescription" HeaderText="Description"  CurrentFilterFunction="StartsWith" ShowFilterIcon = "false" FilterControlWidth="300px"  AutoPostBackOnFilter="true"
                        SortExpression="FormDescription" UniqueName="FormDescription" 
                        FilterControlAltText="Filter FormDescription column">
                    </telerik:GridBoundColumn>
                                     <telerik:GridCheckBoxColumn DataField="CaptchaFg" HeaderText="Show Captcha"  CurrentFilterFunction="EqualTo" ShowFilterIcon = "false" FilterControlWidth="100px"  AutoPostBackOnFilter="true"
                        SortExpression="CaptchaFg" UniqueName="CaptchaFg" 
                        FilterControlAltText="Filter CaptchaFg column">
                    </telerik:GridCheckBoxColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                CssClass="icon-5 info-tooltip" title="Edit" />
                            <asp:LinkButton ID="ConfigLink" runat="server" CommandName = "config" CommandArgument='<%# Eval("FormId") %>'
                                        class="icon-1 info-tooltip" title="Form Email" Visible="true"></asp:LinkButton>
                                                                   
                            <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("FormId") %>'
                                CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this Form?')){return false;}" />
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
                </div>
        <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" EnableUpdate="True"
            runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName=""
            ResourceSetName="Forms" OnUpdating="OpenAccessLinqDataSource1_Updating" Where="DeletedFg == @DeletedFg">
            <WhereParameters>
                <asp:Parameter DefaultValue="false" Name="DeletedFg" Type="Boolean" />
            </WhereParameters>
        </telerik:OpenAccessLinqDataSource>
 </telerik:RadAjaxPanel>
</asp:Content>
