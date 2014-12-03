<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="Zone.aspx.cs" Inherits="ePressMedia.Cp.Ad.Zone" %>
<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript">
    function OnClientclose(sender, eventArgs) {
        document.location.reload(true);
    }
 </script>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
            
            <h1>
                Ad Zones</h1>
 
             <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,edit" ></uc:toolbox>
            </div>
            <div id="listing_div"  runat="server">
                <telerik:RadGrid ID="ZoneGrid" runat="server" AllowFilteringByColumn="True" 
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    CellSpacing="0" DataSourceID="ZoneSource" GridLines="None" OnSelectedIndexChanged="ZoneGrid_SelectedIndexChanged">
                    <MasterTableView DataKeyNames="AdZoneId" DataSourceID="ZoneSource" >
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
                            <telerik:GridBoundColumn DataField="ZoneName" 
                                FilterControlAltText="Filter ZoneName column" 
                                HeaderText="Name" SortExpression="ZoneName" 
                                UniqueName="ZoneName">
                            </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn DataField="ZoneDescription" 
                                FilterControlAltText="Filter ZoneDescription column" 
                                HeaderText="Description" SortExpression="ZoneDescription" 
                                UniqueName="ZoneDescription">
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="ActiveFg" DataType="System.Boolean" 
                                FilterControlAltText="Filter ActiveFg column" HeaderText="ActiveFg" 
                                SortExpression="ActiveFg" UniqueName="ActiveFg">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="ApplyWeightFg" DataType="System.Boolean" 
                                FilterControlAltText="Filter ApplyWeightFg column" HeaderText="ApplyWeightFg" 
                                SortExpression="ApplyWeightFg" UniqueName="ApplyWeightFg">
                            </telerik:GridCheckBoxColumn>
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
                <telerik:OpenAccessLinqDataSource ID="ZoneSource" runat="server" 
                    ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName="" 
                    ResourceSetName="AdZones">
                </telerik:OpenAccessLinqDataSource>
            </div>
            </telerik:RadAjaxPanel>
             <asp:HiddenField ID="height" runat="server" value="600"/>
</asp:Content>
