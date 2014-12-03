<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="Banner.aspx.cs" Inherits="ePressMedia.Cp.Ad.Banner" %>
<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
 <script>
     function OnClientclose(sender, eventArgs) {
         document.location.reload(true);
     }
 </script>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
            
            <h1>
                Ad Banners</h1>
 
             <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,edit,delete" ></uc:toolbox>
            </div>
            <div id="listing_div"  runat="server">
                <telerik:RadGrid ID="BannerGrid" runat="server" AllowFilteringByColumn="True" 
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    CellSpacing="0" DataSourceID="BannerSource" GridLines="None" OnSelectedIndexChanged="BannerGrid_SelectedIndexChanged" PageSize="50">
                    <MasterTableView DataKeyNames="AdBannerId" DataSourceID="BannerSource"  >
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                        <telerik:GridButtonColumn Text="Select" CommandName="Select">
                </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn DataField="BusinessEntity.PrimaryName" 
                                FilterControlAltText="Filter BusinessEntityId column" 
                                HeaderText="Advertiser" SortExpression="BusinessEntityId" 
                                UniqueName="BusinessEntityId">
                            </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn DataField="Description" 
                                FilterControlAltText="Filter Description column" 
                                HeaderText="Description" SortExpression="Description" 
                                UniqueName="Description">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StartDate" DataType="System.DateTime" 
                                FilterControlAltText="Filter StartDate column" HeaderText="StartDate" 
                                SortExpression="StartDate" UniqueName="StartDate" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EndDate" DataType="System.DateTime" 
                                FilterControlAltText="Filter EndDate column" HeaderText="End Date" DataFormatString="{0:d}" 
                                SortExpression="EndDate" UniqueName="EndDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="ActiveFg" DataType="System.Boolean" 
                                FilterControlAltText="Filter ActiveFg column" HeaderText="Active" 
                                SortExpression="ActiveFg" UniqueName="ActiveFg">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn DataField="AdMediaType.MediaTypeName" DataType="System.Int32" 
                                FilterControlAltText="Filter MediaTypeId column" HeaderText="Media Type" 
                                SortExpression="MediaTypeId" UniqueName="MediaTypeId">
                            </telerik:GridBoundColumn>

                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
        </ClientSettings>
                    <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                <telerik:OpenAccessLinqDataSource ID="BannerSource" runat="server" 
                    ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName="" 
                    ResourceSetName="AdBanners">
                </telerik:OpenAccessLinqDataSource>
            </div>
            </telerik:RadAjaxPanel>
      
            <asp:HiddenField ID="height" runat="server" value="600"/>
</asp:Content>
