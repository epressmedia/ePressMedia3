<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageConfig.ascx.cs"
    Inherits="ePressMedia.Admin.PageConfig" %>

 <h2>Control Add/Order</h2>
<div id="PageConfig_Container" runat="server" width="190px">
    <asp:Label ID="lbl_Area" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Name" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Master" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_path" runat="server" Text="" Visible="false"></asp:Label>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
    <div>
    <asp:Label ID="lbl_placeholder" runat="server" Text="Placeholders..."></asp:Label>
    </div>
        <div>
            <telerik:RadComboBox ID="cbo_placeholders" runat="server" OnSelectedIndexChanged="cbo_placeholders_SelectedIndexChanged"
                AutoPostBack="true">
            </telerik:RadComboBox>
        </div>
        <div>
            <telerik:RadGrid ID="gv_controls" runat="server" Width="100%" AutoGenerateColumns="False"
                AllowSorting="false" onrowdrop="gv_controls_RowDrop">
                <MasterTableView Width="100%" ShowHeader="true" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Seq" SortExpression="" HeaderText="Seq." DataField="Seq">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Name" SortExpression="" HeaderText="Cont.Name"
                            DataField="Name">
                        </telerik:GridBoundColumn>
                    </Columns>
                                                        <NoRecordsTemplate>
                                        <div style="height: 30px; cursor: pointer;">
                                           No Control</div>
                                    </NoRecordsTemplate>
                </MasterTableView>
                <ClientSettings AllowRowsDragDrop="True">
                    <Selecting AllowRowSelect="True"  EnableDragToSelectRows="false"></Selecting>
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <div style="margin-top:10px">
            <asp:Button ID="btn_Add_Control" runat="server" Text="Add Control" 
                Visible="false" OnClientClick="addControl('SelectControlType');" style="margin:5px"/>
                <asp:Button ID="btn_Apply_Control" runat="server" Text="Apply" OnClick="btn_Apply_Control_Click"  style="margin:5px"
                Visible="false"/>
        </div>
    </telerik:RadAjaxPanel>
</div>
