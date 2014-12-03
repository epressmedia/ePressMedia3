<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UDFReference.aspx.cs" Inherits="ePressMedia.Cp.UDF.UDFReference" %>
    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            <script src="/Scripts/jquery-latest.js" type="text/javascript"></script>
    <script src="/cp/Scripts/Common.js" type="text/javascript"></script>
    <script src="/cp/Scripts/custom_jquery.js" type="text/javascript"></script>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
    
   <style>
   html, body, form
{
    margin: 0;
    padding: 0;
    height: 100%;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" MinDisplayTime="50"></telerik:RadAjaxLoadingPanel>
              <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
  </telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    
            
            <h1>
                UDF References</h1>
             <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,cancel" ></uc:toolbox>
            </div>
                    <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
                        <div>
            <div style="float:left">
            <asp:Label ID="lbl_ArticleName" runat= "server" Text="Display Value:" />   
                <asp:TextBox ID="txt_displayvalue" runat="server" />
                </div>
                            <div  style="float:left">
            <asp:Label ID="Label1" runat= "server" Text="Code:" />   
                <asp:TextBox ID="txt_internalvalue" runat="server" />
                </div>
                            </div>
                        <div style="clear:both"></div>
                        <div>
                                    <div  style="float:left">
            <asp:Label ID="Label2" runat= "server" Text="Start Date:" />   
                <telerik:RadDatePicker ID="dp_start_date" runat="server"></telerik:RadDatePicker>
                </div>
                                    <div  style="float:left">
            <asp:Label ID="Label3" runat= "server" Text="End Date:" />   
                <telerik:RadDatePicker ID="dp_end_date" runat="server"></telerik:RadDatePicker>
                </div>
                </div>
                        <div>
                            <asp:Button ID="btn_add" runat="server" OnClick="btn_add_Click" Text="Add" />
                        </div>
            </div>
            <div style="clear:both"></div>
            <div id="listing_div"  runat="server">
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                    AllowSorting="True" CellSpacing="0" DataSourceID="OpenAccessLinqDataSource1"  OnItemCommand ="RadGrid1_ItemCommand" OnItemCreated="RadGrid1_ItemCreated"
                    OnUpdateCommand ="RadGrid1_UpdateCommand"
                    GridLines="None"  >
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="False" DataSourceID="OpenAccessLinqDataSource1"
                        PageSize="20" DataKeyNames="ReferenceID" >
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                    
                              <telerik:GridBoundColumn DataField="DisplayValue" FilterControlAltText="Filter DisplayValue column"
                                HeaderText="DisplayValue" SortExpression="DisplayValue" UniqueName="DisplayValue" DataType="System.String" AllowFiltering ="false" >  
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="InternalValue" Visible="true" FilterControlAltText="Filter InternalValue column"  AllowFiltering ="false"
                                HeaderText="InternalValue" SortExpression="InternalValue" UniqueName="InternalValue">
                            </telerik:GridBoundColumn>
                                                        <telerik:GridDateTimeColumn DataField="EffrectiveDate" Visible="true" FilterControlAltText="Filter EffrectiveDate column" AllowFiltering ="false"
                                HeaderText="EffrectiveDate" SortExpression="EffrectiveDate" UniqueName="EffrectiveDate" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                            </telerik:GridDateTimeColumn>
                                                        <telerik:GridDateTimeColumn DataField="TerminateDate" Visible="true" FilterControlAltText="Filter TerminateDate column"  AllowFiltering ="false"
                                HeaderText="TerminateDate" SortExpression="TerminateDate" UniqueName="TerminateDate" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions<a/>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditLink" runat="server" CommandName="mod" CommandArgument='<%#Container.ItemIndex%>'
                                      CssClass="icon-5 info-tooltip" title="Edit" />   
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
                <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EnableUpdate ="True"
                    EntityTypeName="" OrderBy="DisplayValue" ResourceSetName="UDFReferences" Where="UDFID == @UDFID">

                    <WhereParameters>
                        <asp:QueryStringParameter DefaultValue="" Name="UDFID" QueryStringField="UDFID" Type="Int32" />
                    </WhereParameters>

                </telerik:OpenAccessLinqDataSource>
            </div>
</telerik:RadAjaxPanel>
    </div>
    </form>
</body>
</html>
