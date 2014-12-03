<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    Inherits="Cp_Biz_BizList" CodeBehind="default.aspx.cs" %>


<%@ Register Src="../../Controls/Pager/FitPager.ascx" TagName="FitPager" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CP/Controls/Toolbox.ascx" TagName="Toolbox" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function hideMessageBox() {
            var m = $find('MsgBoxMpe');
            if (m)
                m.hide();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
<style>
    .quicklink
    {
        
    }
    .quicklink_title span
    {
        position: relative;
top: 8px;
font-size: 15px;
font-weight: bold;
left: 22px;
background: #FFF;
padding: 0 5px;
color: #7E7E7E;
    }
    .quicklink_buttons
    {
        border: 1px solid #CECECE;
        margin-bottom:3px;
        padding:10px 8px 8px 8px;
    }
</style>
    <h1>
        Business Entity</h1>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
        <div class="quicklink">
        <div class="quicklink_title">
            <asp:Label ID="lbl_quicklink" runat="server" Text="Quick Search"></asp:Label>
        </div>
        <div class="quicklink_buttons">
        <asp:Button ID="btn_ShowUnapproved" runat="server" Text="Show Un-Approved"  
            onclick="btn_preset_Click" />
        <asp:Button ID="btn_ShowAdvertiser" runat="server" Text="Show Advertiser" 
            onclick="btn_preset_Click" />
        <asp:Button ID="btn_ShowPendingChange" runat="server" 
            Text="Show Pending Changes" onclick="btn_preset_Click" />
                    <asp:Button ID="btn_Default" runat="server" 
            Text="Default" onclick="btn_preset_Click" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1" >

    <div>
        <uc:Toolbox ID="toolbox1" runat="server" ButtonAvailable="add,cancel,edit,delete,move,Approve">
        </uc:Toolbox>
    </div>
                <div id ="MovePanel" runat="server" class="cmdPnl" visible="false">
            <div>
            <asp:Label ID="lbl_catname" runat= "server" Text="Category Name:" />   
                <asp:DropDownList ID="ddl_CategoryNames" runat="server">
                </asp:DropDownList>
                </div>
                <div>
                <asp:Button ID="btn_Move" runat="server" Text="Move" OnClick="btn_Move_Click" />
                </div>
                
                
            </div>
            <div style="clear:both"></div>
        
    <div id="listing_div" runat="server">
    
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
          </telerik:RadWindowManager>
        
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
            AllowSorting="True" CellSpacing="0"         GridLines="None" 
            AllowMultiRowSelection="True" onneeddatasource="RadGrid1_NeedDataSource" 
            onitemdatabound="RadGrid1_ItemDataBound" >
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="false" />
            </ClientSettings>
            <MasterTableView AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="BusinessEntityId" 
                PageSize="20">
                <CommandItemSettings ShowExportToExcelButton="true" ExportToPdfText="Export to PDF">
                </CommandItemSettings>
                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                        <HeaderTemplate>
                            <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                runat="server"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" OnCheckedChanged="ToggleRowSelection" AutoPostBack="True"
                                runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="BusinessEntityId" DataType="System.Int32" FilterControlAltText="Filter BusinessEntityId column"
                        HeaderText="ID" ReadOnly="True" SortExpression="BusinessEntityId"
                        UniqueName="BusinessEntityId">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PrimaryName" FilterControlAltText="Filter PrimaryName column"
                        HeaderText="Primary Name" SortExpression="PrimaryName" UniqueName="PrimaryName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SecondaryName" FilterControlAltText="Filter SecondaryName column"
                        HeaderText="Secondary Name" SortExpression="SecondaryName" UniqueName="SecondaryName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BusienssCategory.CategoryName" FilterControlAltText="Filter BusinessCategory.CategoryName column"
                        HeaderText="Category" SortExpression="BusinessCategory.CategoryName" UniqueName="BusienssCategory.CategoryName">
                       <FilterTemplate>
    <telerik:RadComboBox runat="server" ID="FilterCombo" DataSourceID="OpenAccessLinqDataSource2"
      DataValueField="CategoryName" DataTextField="CategoryName" AutoPostBack="true"
      OnSelectedIndexChanged="FilterCombo_SelectedIndexChanged" OnDataBound="FilterCombo_DataBound">
    </telerik:RadComboBox>
  </FilterTemplate>
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="ApprovedFg" DataType="System.Boolean" FilterControlAltText="Filter ApprovedFg column"
                        HeaderText="ApprovedFg" SortExpression="ApprovedFg" UniqueName="ApprovedFg">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="DeletedFg" DataType="System.Boolean" Visible="false"
                        FilterControlAltText="Filter DeletedFg column" HeaderText="DeletedFg" SortExpression="DeletedFg"
                        UniqueName="DeletedFg">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="CreatedDate" DataType="System.DateTime" FilterControlAltText="Filter CreatedDate column"
                        HeaderText="Created Date" SortExpression="CreatedDate" UniqueName="CreatedDate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CreatedBy" DataType="System.Guid" Visible="false"
                        FilterControlAltText="Filter CreatedBy column" HeaderText="CreatedBy" SortExpression="CreatedBy"
                        UniqueName="CreatedBy">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ModifiedDate" DataType="System.DateTime"  Visible="false" FilterControlAltText="Filter ModifiedDate column"
                        HeaderText="ModifiedDate" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ModifiedBy" DataType="System.Guid" Visible="false"
                        FilterControlAltText="Filter ModifiedBy column" HeaderText="ModifiedBy" SortExpression="ModifiedBy"
                        UniqueName="ModifiedBy">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Address" Visible="false" FilterControlAltText="Filter Address column"
                        HeaderText="Address" SortExpression="Address" UniqueName="Address">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter City column"
                        HeaderText="City" SortExpression="City" UniqueName="City">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="State" FilterControlAltText="Filter State column"
                        HeaderText="State" SortExpression="State" UniqueName="State">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ZipCode" Visible="false" FilterControlAltText="Filter ZipCode column"
                        HeaderText="ZipCode" SortExpression="ZipCode" UniqueName="ZipCode">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VideoURL" Visible="false" FilterControlAltText="Filter VideoURL column"
                        HeaderText="VideoURL" SortExpression="VideoURL" UniqueName="VideoURL">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Password" Visible="false" FilterControlAltText="Filter Password column"
                        HeaderText="Password" SortExpression="Password" UniqueName="Password">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ShortDesc" Visible="false" FilterControlAltText="Filter ShortDesc column"
                        HeaderText="ShortDesc" SortExpression="ShortDesc" UniqueName="ShortDesc">
                    </telerik:GridBoundColumn>
<%--                    <telerik:GridBoundColumn DataField="Description" Visible="false" FilterControlAltText="Filter Description column"
                        HeaderText="Description" SortExpression="Description" UniqueName="Description">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="Website" Visible="false" FilterControlAltText="Filter Website column"
                        HeaderText="Website" SortExpression="Website" UniqueName="Website">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Email" Visible="false" FilterControlAltText="Filter Email column"
                        HeaderText="Email" SortExpression="Email" UniqueName="Email">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Phone1" Visible="false" FilterControlAltText="Filter Phone1 column"
                        HeaderText="Phone1" SortExpression="Phone1" UniqueName="Phone1">
                    </telerik:GridBoundColumn>
<%--                    <telerik:GridBoundColumn DataField="Phone2" Visible="false" FilterControlAltText="Filter Phone2 column"
                        HeaderText="Phone2" SortExpression="Phone2" UniqueName="Phone2">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="Fax" Visible="false" FilterControlAltText="Filter Fax column"
                        HeaderText="Fax" SortExpression="Fax" UniqueName="Fax">
                    </telerik:GridBoundColumn>
                                        <telerik:GridCheckBoxColumn DataField="AdOwner" DataType="System.Boolean" FilterControlAltText="Filter AdOwner column"
                        HeaderText="Advertiser" SortExpression="AdOwner" UniqueName="AdOwner">
                    </telerik:GridCheckBoxColumn>
                                                            <telerik:GridCheckBoxColumn DataField="PremiumListing" DataType="System.Boolean" FilterControlAltText="Filter PremiumListing column"
                        HeaderText="Pre. Listing" SortExpression="PremiumListing" UniqueName="PremiumListing">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn FilterControlAltText="Fitler PendingChange column"
                    HeaderText="PendingChange" SortExpression="PendingChange" UniqueName="PendingChange" DataType = "System.Boolean">
                    
                    </telerik:GridCheckBoxColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
            </MasterTableView>
            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </telerik:RadGrid>

        <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource2" runat="server" 
            ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName="" 
            OrderBy="CategoryName" ResourceSetName="BusinessCategories" 
            Select="new (CategoryName)">
        </telerik:OpenAccessLinqDataSource>
    </div>

    </telerik:RadAjaxPanel>
</asp:Content>
