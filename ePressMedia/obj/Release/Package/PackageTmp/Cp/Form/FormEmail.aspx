<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormEmail.aspx.cs" Inherits="ePressMedia.Cp.Form.FormEmail" %>





<%@ Register Src="~/CP/Controls/Toolbox.ascx" TagName="Toolbox" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.OpenAccess.Web, Version=2014.2.918.1, Culture=neutral, PublicKeyToken=7ce17eeaf1d59342" Namespace="Telerik.OpenAccess" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-latest.js" type="text/javascript"></script>
    <script src="/cp/Scripts/Common.js" type="text/javascript"></script>
    <script src="/cp/Scripts/custom_jquery.js" type="text/javascript"></script>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        html, body, form {
            margin: 0;
            padding: 0;
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            </telerik:RadScriptManager>
            <h1>Submit Email -
                <asp:Label ID="CatName" runat="server" Text=""></asp:Label></h1>
            <uc:Toolbox ID="toolbox1" runat="server" ButtonAvailable="add,cancel"></uc:Toolbox>
        </div>
        <div id="AddPanel" runat="server" class="cmdPnl" visible="false">

            <div>
                <asp:Label ID="lbl_EmailEvent" runat="server" Text="Email Event" />
                <asp:DropDownList ID="dll_emailevent" runat="server"></asp:DropDownList>
            </div>
            <div>
                <asp:CheckBox ID="chk_selectfromudf" runat="server" Text="Select Email field from UDF" AutoPostBack="true" OnCheckedChanged="chk_selectfromudf_CheckedChanged"/>
            </div>
            <div id ="udf_email" runat="server" visible ="false">
                <asp:DropDownList ID="ddl_udfs" runat="server"></asp:DropDownList>
            </div>
            <div id="manual_email" runat="server">
                <asp:Label ID="lbl_receipients" runat="server" Text="Recipient Emails(Seperated by semicolon)" />
                <asp:TextBox ID="txt_recipients" runat="server" Width="400px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="email_validator" runat="server" ValidationExpression ="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*;\s*|\s*$))*"
                    ValidationGroup="save" ControlToValidate ="txt_recipients" ErrorMessage="Email Address is not in correct format" ></asp:RegularExpressionValidator>

            </div>
            <div>
                <asp:Button ID="btn_add" runat="server" OnClick="btn_add_Click" Text="Add" ValidationGroup="save"/>
            </div>
        </div>
        <div>
            <telerik:RadGrid ID="Email_List" runat="server" AllowPaging="True" AutoGenerateEditColumn="True" CellSpacing="0" 
                DataSourceID="OpenAccessLinqDataSource1" GridLines="None" OnItemCreated="Email_List_ItemCreated" OnUpdateCommand="Email_List_UpdateCommand" OnItemDataBound="Email_List_ItemDataBound" OnDeleteCommand="Email_List_DeleteCommand">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="FormEmailId" DataSourceID="OpenAccessLinqDataSource1" >
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>

                    <Columns>
                        <telerik:GridBoundColumn DataField="FormEmailId" DataType="System.Int32" FilterControlAltText="Filter FormEmailId column" HeaderText="FormEmailId" ReadOnly="True" SortExpression="FormEmailId" UniqueName="FormEmailId">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmailEvent.EmailEventName" FilterControlAltText="Filter EmailEventId column" HeaderText="Email Event" SortExpression="EmailEventId" UniqueName="EmailEvent" AllowSorting="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Receipients" FilterControlAltText="Filter Receipients column" HeaderText="Receipients" SortExpression="Receipients" UniqueName="Receipients">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="UDFName" HeaderText="UDF Name">
                            <ItemTemplate>
                                <asp:Label ID="lbl_UDFName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="FormId" DataType="System.Int32" FilterControlAltText="Filter FormId column" HeaderText="FormId" SortExpression="FormId" UniqueName="FormId" Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" />
                    </Columns>

                    <EditFormSettings EditFormType="Template">
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                        <FormTemplate>
                                        <div>
                <asp:Label ID="lbl_EmailEvent" runat="server" Text="Email Event" />
                <asp:DropDownList ID="dll_emailevent" runat="server"  DataTextField="EmailEventName" DataValueField="EmailEventId"></asp:DropDownList>
            </div>


                        <div>
                <asp:CheckBox ID="chk_edit_selectfromudf" runat="server" Text="Select Email field from UDF" AutoPostBack="true" OnCheckedChanged="chk_edit_selectfromudf_CheckedChanged"/>
            </div>
            <div id ="edit_udf_email" runat="server" visible ="false">
                <asp:DropDownList ID="ddl_edit_udfs" runat="server"></asp:DropDownList>
            </div>
            <div id="edit_manual_email" runat="server">
                <asp:Label ID="lbl_receipients" runat="server" Text="Recipient Emails(Seperated by semicolon)" />
                <asp:TextBox ID="txt_edit_recipients" runat="server" Width="300px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="email_validator" runat="server" ValidationExpression ="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*;\s*|\s*$))*"
                    ValidationGroup="update"  ControlToValidate="txt_edit_recipients" ErrorMessage="Email Address is not in correct format"></asp:RegularExpressionValidator>

            </div>
                            <div>
                                      <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' ValidationGroup="update">
                                </asp:Button>&nbsp;
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                    CommandName="Cancel"></asp:Button>
                            </div>
                        </FormTemplate>
                    </EditFormSettings>

                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                </MasterTableView>

                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

                <FilterMenu EnableImageSprites="False"></FilterMenu>
            </telerik:RadGrid>

            <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" ResourceSetName="FormEmails" Where="FormId == @FormId">
                <WhereParameters>
                    <asp:QueryStringParameter DefaultValue="" Name="FormId" QueryStringField="formid" Type="Int32" />
                </WhereParameters>
            </telerik:OpenAccessLinqDataSource>
        </div>
      
        
    </form>
</body>
</html>
