<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    CodeBehind="Thumbnails.aspx.cs" Inherits="ePressMedia.Cp.Site.Thumbnails" %>

<%@ Register Src="~/CP/Controls/Toolbox.ascx" TagName="Toolbox" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h1>
        Thumbnail Configuration</h1>
    <telerik:RadAjaxPanel ID="ajaxPanel" runat="server" LoadingPanelID="ajaxloadpanel">
    
    

            <style>
                .thumb_add_label
                {
                    display:block
                }
                .add_field_container
                {
                    float:left;
                    margin-right: 10px;
                }
                
            </style>
            <div id="StyleSheetDiv" runat="server">
                <div >
                    <uc:Toolbox ID="toolbox1" runat="server" ButtonAvailable="add,edit,cancel,delete">
                    </uc:Toolbox>
                </div>
                <div id="add_panel" runat="server" visible="false" style="margin:5px 0">
                <div class="add_field_container">
                <asp:Label ID="lbl_name" runat="server" Text="Name" CssClass="thumb_add_label"></asp:Label>
                 <telerik:RadTextBox  ID="txt_name" runat="server" InputType="Text"></telerik:RadTextBox>
                </div>
                <div class="add_field_container">
                <asp:Label ID="lbl_Description" runat="server" Text="Description" CssClass="thumb_add_label"></asp:Label>
                 <telerik:RadTextBox ID="txt_description" runat="server" Width="340px"></telerik:RadTextBox>
                </div>
                          <div class="add_field_container">
                <asp:Label ID="lbl_width" runat="server" Text="Width" CssClass="thumb_add_label"></asp:Label>
                <telerik:RadNumericTextBox ID="txt_width" runat="server" Type="Number" Width="100px" DataType="System.Int"></telerik:RadNumericTextBox>
                </div>

                <div class="add_field_container">
                <asp:Label ID="lbl_height" runat="server" Text="Height" CssClass="thumb_add_label"></asp:Label>
                <telerik:RadNumericTextBox ID="txt_height" runat="server" IType="Number" Width="100px" DataType="System.Int"></telerik:RadNumericTextBox>
                </div>
                <div class="add_field_container">
                <asp:Label ID="lbl_processType" runat="server" Text="Process Type" CssClass="thumb_add_label"></asp:Label>
                <telerik:RadComboBox ID="ddl_processtype" runat="server" EmptyMessage="Select a process type"></telerik:RadComboBox>
                </div>
                <div class="add_field_container">
                    <asp:Button ID="btn_add" runat="server" Text="Save" OnClick="btn_add_Click" style="margin-top: 15px;"/>
                 </div>
                                 <div class="add_field_container">
                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_Click" style="margin-top: 15px;"/>
                 </div>
                <div style="clear:both"></div>
                
                </div>


                <telerik:RadGrid ID="ThumbnailGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowSorting="false" OnSelectedIndexChanged="ThumbnailGrid_SelectedIndexChanged">
                    <MasterTableView Width="100%" ShowHeader="true" DataKeyNames="ThumbnailTypeID">
                        <Columns>
                                                    <telerik:GridButtonColumn Text="Select" CommandName="Select">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn UniqueName="ThumbnailTypeID" SortExpression="" HeaderText="ID"
                                Visible="false" DataField="ThumbnailTypeID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ThumbnailTypeName" SortExpression="" HeaderText="Name"
                                Visible="true" DataField="ThumbnailTypeName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ThubmnailTypeDescription" SortExpression=""
                                HeaderText="Description" Visible="true" DataField="ThubmnailTypeDescription">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Width" SortExpression="" HeaderText="Width"
                                Visible="true" DataField="Width">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Height" SortExpression="" HeaderText="Height"
                                Visible="true" DataField="Height">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ProcessType" SortExpression="" HeaderText="Process Type"
                                Visible="true" DataField="ProcessType">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SystemFg" SortExpression="" HeaderText="System"
                                Visible="true" DataField="SystemFg">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DefaultFg" SortExpression="" HeaderText="Default"
                                Visible="true" DataField="DefaultFg">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn Text="Set to Default" CommandName="SetDefault">
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

          
            </div>
            </telerik:RadAjaxPanel>
            <telerik:RadAjaxLoadingPanel ID="ajaxloadpanel" runat="server" >
            </telerik:RadAjaxLoadingPanel>
</asp:Content>
