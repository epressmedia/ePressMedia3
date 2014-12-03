<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUDFAttachment.aspx.cs" Inherits="ePressMedia.Cp.UDF.AddUDFAttachment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript" lang="javascript">
            function GetRadWindow() {
                var oWindow = null; if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog                
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)              
                return oWindow;
            }

            function CloseDataEntry() {

                GetRadWindow().close();
            }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            Available UDF Groups
        </h1>
    <div>

                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
  </telerik:RadScriptManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID = "RadAjaxLoadingPanel1">
    <telerik:RadGrid ID="grid_UDFGroups" runat="server" CellSpacing="0" GridLines="None"  AllowMultiRowSelection ="true" >
<MasterTableView AutoGenerateColumns="false" DataKeyNames="UDFGroupId" >
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridButtonColumn Text="Select" CommandName="Select">
                    </telerik:GridButtonColumn>
                      <telerik:GridBoundColumn DataField="UDFGroupId" FilterControlAltText="Filter UDFGroupId column"
                                HeaderText="ID" SortExpression="UDFGroupId" UniqueName="UDFGroupId"  AllowFiltering ="false" >  
                            </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="UDFGroupName" FilterControlAltText="Filter UDFGroupName column"
                                HeaderText="Group Name" SortExpression="UDFGroupName" UniqueName="UDFGroupName"  AllowFiltering ="false" >  
                            </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="UDFGroupDescription" FilterControlAltText="Filter UDFGroupDescription column"
                                HeaderText="Description" SortExpression="UDFGroupDescription" UniqueName="UDFGroupDescription"  AllowFiltering ="false" >  
                            </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="NoOfColumns" FilterControlAltText="Filter NoOfColumns column"
                                HeaderText="No Of Columns" SortExpression="NoOfColumns" UniqueName="NoOfColumns"  AllowFiltering ="false" >  
                            </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
        </telerik:RadGrid>
        <asp:button ID="btn_cancel" runat="server" Text ="Cancel"  OnClick ="btn_cancel_Click" />
        <asp:button ID="btn_select" runat="server" Text ="Select" OnClick ="btn_select_Click" />
        </telerik:RadAjaxPanel>
       
    </div>
    </form>
</body>
</html>
