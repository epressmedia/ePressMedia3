<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    Inherits="Cp.User.CpUserUsersEx" CodeBehind="default.aspx.cs" EnableEventValidation="false" %>

<%@ Register Src="~/Controls/Pager/FitPager.ascx" TagName="FitPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function ReloadOnClientClose(sender, eventArgs) {
                $find("<%=  RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                //window.location.href=window.location.href;
            }

        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="listing_div">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listing_div" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <h1>
        User Maintenance
    </h1>
    <div style="margin: 40px 0 10px 0">
        <asp:Label ID="lbl_searchby" runat="server" Text="Filter By User Role"></asp:Label>
        <asp:DropDownList ID="ddl_userroles" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_userroles_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div id="listing_div" runat="server">
        <telerik:RadGrid ID="UserGrid" runat="server" AllowPaging="True" AllowSorting="True"
            CellSpacing="0" GridLines="None" AllowFilteringByColumn="True" OnItemCommand="UserGrid_ItemCommand"
            ViewStateMode="Enabled" OnPageIndexChanged="UserGrid_PageIndexChanged" OnPageSizeChanged="UserGrid_PageSizeChanged" EnableLinqExpressions = "false"
            OnSortCommand="UserGrid_SortCommand">
            <GroupingSettings CaseSensitive="false" />
            <ClientSettings EnableRowHoverStyle="true">
            </ClientSettings>
            <MasterTableView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false"
                CommandItemDisplay="Top" DataKeyNames="UserId,LoginName" AllowMultiColumnSorting="false" 
                PageSize="15" ViewStateMode="Enabled"
                FilterExpression = "[IsValidated] = True AND [Status] = True AND [IsLockedOut] = False">
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                    ShowAddNewRecordButton="false" ShowExportToCsvButton="true" ShowExportToPdfButton="false">
                </CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="FullName" FilterControlAltText="Filter FullName column"
                        CurrentFilterFunction="StartsWith" HeaderText="FullName" SortExpression="FullName"
                        UniqueName="FullName" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LoginName" FilterControlAltText="Filter LoginName column"
                        CurrentFilterFunction="StartsWith" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        HeaderText="LoginName" SortExpression="LoginName" UniqueName="LoginName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column"
                        CurrentFilterFunction="StartsWith" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        HeaderText="Email" SortExpression="Email" UniqueName="Email">
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="IsValidated" FilterControlAltText="Filter IsValidated column"
                        DataType="System.Boolean" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" CurrentFilterValue = "True"
                        AutoPostBackOnFilter="true" HeaderText="Approved" SortExpression="IsValidated"
                        UniqueName="IsValidated">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="Status" FilterControlAltText="Filter Status column"
                        DataType="System.Boolean" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" CurrentFilterValue = "True"
                        AutoPostBackOnFilter="true" HeaderText="Active" SortExpression="Status" UniqueName="Status">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="IsLockedOut" FilterControlAltText="Filter IsLockedOut column"
                        DataType="System.Boolean" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" CurrentFilterValue = "False"
                        AutoPostBackOnFilter="true" HeaderText="Locked" SortExpression="IsLockedOut"
                        UniqueName="IsLockedOut">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridDateTimeColumn DataField="LastLoginDate" FilterControlAltText="Filter LastLoginDate column"
                        FilterControlWidth="120px" CurrentFilterFunction="EqualTo" HeaderText="Last Activity Date"
                        SortExpression="LastLoginDate" UniqueName="LastLoginDate" PickerType="DatePicker" ShowFilterIcon = "false"
                        AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="RegisterDate" FilterControlAltText="Filter RegisterDate column"
                        FilterControlWidth="120px" CurrentFilterFunction="EqualTo" HeaderText="RegisterDate" ShowFilterIcon = "false"
                        SortExpression="RegisterDate" UniqueName="RegisterDate" PickerType="DatePicker"
                        AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridTemplateColumn UniqueName="Option" AllowFiltering="false" HeaderText="<a>Options</a>"
                        DataField="Option">
                        <ItemTemplate>
                            <asp:LinkButton ID="DeactivateLink" runat="server" CommandName="Deactivate" CommandArgument='<%# Eval("LoginName") %>'
                                Visible='<%# Eval("Status") %>' CssClass="icon-2 info-tooltip" title="Deactivate"
                                OnClientClick="javascript:if(!confirm('Are you sure that you would like to dectivate this user?')){return false;}" />
                            <asp:LinkButton ID="ActivateLink" runat="server" CommandName="Activate" CommandArgument='<%# Eval("LoginName") %>'
                                Visible='<%# bool.Parse(Eval("Status").ToString())?false:true %>' CssClass="icon-10 info-tooltip"
                                title="Activate" OnClientClick="javascript:if(!confirm('Are you sure that you would like to activate this user?')){return false;}" />
                            <asp:LinkButton ID="Approve" runat="server" CommandName="Approve" CommandArgument='<%# Eval("LoginName") %>'
                                Visible='<%# bool.Parse(Eval("IsValidated").ToString())?false:true %>' CssClass="icon-12 info-tooltip"
                                title="Approve" OnClientClick="javascript:if(!confirm('Are you sure that you would like to approve this user?')){return false;}" />
                            <asp:LinkButton ID="DetailLink" runat="server" CommandName="config" CommandArgument='<%#Eval("LoginName")%>'
                                CssClass="icon-11 info-tooltip" title="User Detail" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="float: right; top: 20px">
        <asp:Button ID="btn_Export" runat="server" Text="Export to Excel w/ UDFs" OnClick="btn_Export_Click" />
    </div>
    <div style="clear: both">
    </div>
</asp:Content>
