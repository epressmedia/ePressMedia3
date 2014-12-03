<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    CodeBehind="StyleSheets.aspx.cs" Inherits="ePressMedia.Cp.Site.StyleSheets" %>
    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h1>
        StyleSheet</h1>
        <h4>The stylesheets applied later will overwrite the CSS elements in the previous stylesheets. </h4>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

    <ContentTemplate>
      <script type="text/javascript">
          function OpenFileExplorerDialog() {
              var wnd = $find("<%= ExplorerWindow.ClientID %>");
              wnd.show();

          }

          //This function is called from a code declared on the Explorer.aspx page
          function OnFileSelected(fileSelected) {
              var textbox = $find("<%= txt_control_path.ClientID %>");
              textbox.set_value('~' + fileSelected);
          }
                        </script>
        <div id="StyleSheetDiv" runat="server">

        <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "up,down,add,edit" ></uc:toolbox>
            </div>
                                 
            <div id="add_panel" runat="server" visible="false">
            <asp:Label ID="lbl_control_path" runat="server" Text="Stylesheet Path"></asp:Label>:
            <telerik:RadTextBox ID="txt_control_path" runat="server" Width="343px" />
                                    <asp:Button ID="selectFile" OnClientClick="OpenFileExplorerDialog(); return false;"
                            Text="Browse" runat="server" />
             <asp:Button ID="btn_add" runat="server" Text="Add" onclick="btn_add_Click" />
                        <telerik:RadWindow runat="server" Width="550px" Height="560px" VisibleStatusbar="false"
                            ShowContentDuringLoad="false" NavigateUrl="/CP/Site/StyleSheetExplorer.aspx" ID="ExplorerWindow"
                            Modal="true" Behaviors="Close,Move">
                        </telerik:RadWindow>
                        
            </div> 
    <telerik:RadGrid ID="ControlGrid" runat="server" Width="100%" AutoGenerateColumns="False"
        AllowSorting="false" onselectedindexchanged="ControlGrid_SelectedIndexChanged" OnItemCommand = "RadGrid1_ItemCommand">
        <MasterTableView Width="100%" ShowHeader="true"  DataKeyNames="StyleSheetID">
            <Columns>
                <telerik:GridButtonColumn Text="Select" CommandName="Select">
                </telerik:GridButtonColumn>
                           <telerik:GridBoundColumn UniqueName="StyleSheetID" SortExpression="" HeaderText="<a>StyleSheetID</a>" Visible="false"
                    DataField="StyleSheetID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="SequenceNo" SortExpression="" HeaderText="<a>Apply Sequence</a>"
                    DataField="SequenceNo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Name" SortExpression="" HeaderText="<a>CSS File Path</a>"
                    DataField="Name" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="enabled" SortExpression="" HeaderText="<a>Active?</a>"
                    DataField="enabled">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions</a>">
                                <ItemTemplate>
                                                                        <asp:LinkButton ID="DeactivateLink" runat="server"  CommandName="Deactivate" CommandArgument='<%# Eval("StyleSheetID") %>' Visible='<%# Eval("Enabled") %>'
                                        CssClass="icon-9 info-tooltip" title="Deactivate" OnClientClick="javascript:if(!confirm('Are you sure that you would like to dectivate this stylesheet?')){return false;}" />
                                    <asp:LinkButton ID="ActivateLink" runat="server"  CommandName="Activate" CommandArgument='<%# Eval("StyleSheetID") %>' Visible='<%# bool.Parse(Eval("Enabled").ToString())?false:true %>'
                                        CssClass="icon-8 info-tooltip" title="Activate" OnClientClick="javascript:if(!confirm('Are you sure that you would like to activate this stylesheet?')){return false;}" />
                                    <asp:LinkButton ID="DeleteLink" runat="server" CommandName="del" CommandArgument='<%# Eval("StyleSheetID") %>'
                                        CssClass="icon-2 info-tooltip" title="Delete" OnClientClick="javascript:if(!confirm('Are you sure you would like to delete this stylesheet?')){return false;}" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
            </Columns>
            <NoRecordsTemplate>
                <div style="height: 30px; cursor: pointer;">
                    No items to view</div>
            </NoRecordsTemplate>
           
        </MasterTableView>
        
        <ClientSettings AllowRowsDragDrop="True">
            <Selecting EnableDragToSelectRows="false"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
