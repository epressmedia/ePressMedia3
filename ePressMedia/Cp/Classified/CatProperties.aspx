<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatProperties.aspx.cs" Inherits="ePressMedia.Cp.Classified.CatProperties" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <h1>Classified Configuration - <asp:Literal ID="ClassifiedName" runat="server" /></h1>
             <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
  </telerik:RadScriptManager>
    <div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div class="cmdPnl">
    Title Tag Name: 
    <asp:TextBox ID="TagName" runat="server" />
    <asp:Button ID="BtnAdd" runat="server" Text="Add Tag" onclick="BtnAdd_Click" /> 
  </div>
        <telerik:RadGrid ID="TagList" runat="server" 
        AllowFilteringByColumn="True" AllowPaging="True"
                    AllowSorting="True" CellSpacing="0" DataSourceID="TagSource"
                    GridLines="None" OnItemCommand="TagList_ItemCommand" AutoGenerateColumns="False" 
                    Width="30%">
                              <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
            <MasterTableView DataSourceID="TagSource" PageSize="10"  DataKeyNames="TagId">
                <CommandItemSettings ExportToPdfText="Export to PDF" />

                <Columns>

                                                  <telerik:GridBoundColumn DataField="TagName" HeaderText="Tag Name" SortExpression="TagName" CurrentFilterFunction="StartsWith" ShowFilterIcon="false" FilterControlWidth="300px" AutoPostBackOnFilter = "true"
                                UniqueName="TagName" FilterControlAltText="Filter TagName column" 
                                ReadOnly="True">
                            </telerik:GridBoundColumn>

        
                              <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="DeleteLink" runat="server"  CommandName="del" CommandArgument='<%# Eval("TagId") %>'
                                        CssClass="icon-13 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this tag?')){return false;}" />
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
    </telerik:RadAjaxPanel>
    <telerik:OpenAccessLinqDataSource ID="TagSource" runat="server" 
          ContextTypeName="EPM.Data.Model.EPMEntityModel" EntityTypeName="" 
          OrderBy="TagName" ResourceSetName="ClassifiedTags" 
          Select="new (TagId, TagName)" Where="ClassifiedCatId == @ClassifiedCatId">
        <WhereParameters>
            <asp:QueryStringParameter DefaultValue="" Name="ClassifiedCatId" QueryStringField="id" 
                Type="Int32" />
        </WhereParameters>
    </telerik:OpenAccessLinqDataSource>
    </div>
    </form>
</body>
</html>
