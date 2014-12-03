<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" CodeBehind="SiteSettings.aspx.cs" Inherits="ePressMedia.Cp.Site.SiteSettings" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

      <h1>Site Settings</h1>
      <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
          AllowSorting="True" AutoGenerateEditColumn="True" CellSpacing="0" 
          DataSourceID="OpenAccessLinqDataSource1" GridLines="None" 
           onitemcreated="RadGrid1_ItemCreated" >

<MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" 
              DataKeyNames="SettingId" DataSourceID="OpenAccessLinqDataSource1" >
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="SettingId" 
            FilterControlAltText="Filter SettingId column" HeaderText="SettingId" 
            SortExpression="SettingId" UniqueName="SettingId" DataType="System.Int32"  Visible="false"
            ReadOnly="True" >
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="SettingName" 
            FilterControlAltText="Filter SettingName column" HeaderText="Name" 
             SortExpression="SettingName" UniqueName="SettingName">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="SettingDescr" 
            FilterControlAltText="Filter SettingDescr column" HeaderText="Description" 
             SortExpression="SettingDescr" UniqueName="SettingDescr">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="SettingValue" 
            FilterControlAltText="Filter SettingValue column" 
            HeaderText="Value" SortExpression="SettingValue" 
            UniqueName="SettingValue">
        </telerik:GridBoundColumn>
        <telerik:GridCheckBoxColumn DataField="ExposeUI_fg" DataType="System.Boolean" 
            FilterControlAltText="Filter ExposeUI_fg column" HeaderText="ExposeUI_fg" 
            SortExpression="ExposeUI_fg" UniqueName="ExposeUI_fg" Visible="false" ReadOnly = "true">
        </telerik:GridCheckBoxColumn>
    </Columns>
    
<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>

</EditFormSettings>



</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

</telerik:RadGrid>
          <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" 
          Runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" 
          EnableUpdate="True" EnableDelete="false"
          EntityTypeName="" OrderBy="SettingName" ResourceSetName="SiteSettings" 
          Where="ExposeUI_fg == @ExposeUI_fg" 
          onupdating="OpenAccessLinqDataSource1_Updating">
              <WhereParameters>
                  <asp:Parameter DefaultValue="true" Name="ExposeUI_fg" Type="Boolean" />
              </WhereParameters>
          </telerik:OpenAccessLinqDataSource>
</asp:Content>
