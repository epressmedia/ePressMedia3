<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="ePressMedia.Cp.User.UserDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-latest.js" type="text/javascript"></script>
    <script src="/cp/Scripts/Common.js" type="text/javascript"></script>
    <script src="/cp/Scripts/custom_jquery.js" type="text/javascript"></script>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <style>
        .user_detail_main
        {
            margin-top:10px;
        }
        .user_basic_info
        {
            width: 100%;
        }
        .user_basic_info .label
        {
            margin: 5px 10px 5px 0;
        }
        .user_basic_info > div
        {
            margin: 5px 0;
        }
        .header_info > div
        {
            float: left;
            width: 30%;
        }
        .header_info .caption
        {
            display: block;
            font-size: 12px;
            color: Gray;
            font-weight: bold;
        }
        .user_header_info, .header_info
        {
            margin: 10px 0;
        }
        .user_header_info .value
        {
            margin-left: 10px;
        }
        .user_header_info .divider
        {
            border: 1px solid #689CBE;
            width: 100%;
            margin-bottom: 10px;
        }
    </style>
    <div class="user_header_info">
        <h1>
            User Detail
        </h1>
        <div class="divider">
        </div>
        <div class="header_info">
            <div>
                <asp:Label ID="label2" runat="server" Text="Full Name" CssClass="caption"></asp:Label>
                <asp:Label ID="lbl_fullname" runat="server" Text="" CssClass="value"></asp:Label>
            </div>
            <div>
                <asp:Label ID="label3" runat="server" Text="Login Name" CssClass="caption"></asp:Label>
                <asp:Label ID="lbl_loginname" runat="server" Text="" CssClass="value"></asp:Label>
            </div>
            <div>
                <asp:Label ID="Label5" runat="server" Text="Email Address" CssClass="caption"></asp:Label>
                <asp:Label ID="lbl_emailaddress" runat="server" Text="" CssClass="value"></asp:Label>
            </div>
            <div style="clear: both">
            </div>
        </div>
    </div>
    <div class="user_detail_main">
        <div style="float: left; width: 100%">
            <epm:SlideDownPanel ID="SlideDownPanel2" runat="server" Title="Basic Information"
                Description="" Expanded="true">
                <div class="user_basic_info">
                    <div>
                        <span class="label">Address1:</span><asp:Label ID="Address1" runat="server" CssClass="UDF_ReadValue"></asp:Label></div>
                    <div>
                        <span class="label">Address2:</span><asp:Label ID="Address2" runat="server" CssClass="UDF_ReadValue"></asp:Label></div>
                    <div>
                        <span class="label">Zip/Postal Code:</span><asp:Label ID="PostalCode" runat="server"
                            CssClass="UDF_ReadValue"></asp:Label></div>
                    <div>
                        <span class="label">City:</span><asp:Label ID="City" runat="server" CssClass="UDF_ReadValue"></asp:Label></div>
                    <div>
                        <span class="label">State/Province:</span><asp:Label ID="Province" runat="server"
                            CssClass="UDF_ReadValue"></asp:Label></div>
                    <div>
                        <span class="label">Comments:</span><asp:Label ID="UserComment" runat="server" CssClass="UDF_ReadValue"></asp:Label></div>
                </div>
            </epm:SlideDownPanel>
        </div>
        <div style="float: left; width: 100%">
            <epm:SlideDownPanel ID="SlideDownPanel3" runat="server" Title="Additional Information"
                Description="From User Defined Fields" Expanded="true">
                <asp:Panel ID="UDF_Panel" runat="server">
                </asp:Panel>
            </epm:SlideDownPanel>
        </div>
        <div id="div_userrole" runat="server" class="UsersExSubContainer" visible="false"
            style="float: left; width: 100%">
            <epm:SlideDownPanel ID="SlideDownPanel1" runat="server" Title="User Role" Description="User Role Assignement"
                Expanded="true">
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
                </telerik:RadAjaxLoadingPanel>
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div>
                        User Role:
                        <asp:DropDownList ID="ddl_userrole" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="btn_userrole" runat="server" Text="Add User Role" OnClick="btn_userrole_Click" />
                    </div>
                    <telerik:RadGrid ID="rg_userrole" runat="server" OnItemCommand="rg_userrole_ItemCommand">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="RoleId,RoleName">
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="RoleName" DataType="System.String" FilterControlAltText="Filter RoleName column"
                                    HeaderText="<a>Role Name</a>" SortExpression="RoleName" UniqueName="RoleName">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn Text="Delete" CommandName="Delete" UniqueName="DeleteColumn">
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </epm:SlideDownPanel>
        </div>
        <div style="clear: both">
        </div>
        <div>
            <asp:Label ID="NewPass" runat="server" Text=""></asp:Label>
            <asp:Button ID="btn_ResetPw" runat="server" Text="Reset Password" OnClick="btn_ResetPw_Click" />
        </div>
    </div>
    </form>
</body>
</html>
