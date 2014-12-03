<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    Inherits="Cp.Widget.CpWidgetDefault" CodeBehind="Default.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <style>
        .widget_area
        {
            margin: 10px;
            padding: 10px;
            background: #F3F5F5;
            float: left;
        }
        .widget_area > div
        {
            float: left;
        }
        .widget_area span
        {
            display: block;
        }
        
        .widget_add
        {
            margin: 10px;
            padding: 10px;
            background: #F3F5F5;
        }
        
        .widget_add > div
        {
            margin: 5px 0 5px 0;
        }
        .page_header
        {
            margin: 10px;
            font-size: 20px;
            font-weight: bold;
        }
    </style>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div style="height: auto">
        <div>
            <asp:Label ID="lbl_page_header" CssClass="page_header" runat="server" Text="Widget Maintenance"></asp:Label>
        </div>
        <div>
        </div>
        <div class="widget_area">
            <div>
                <asp:Label ID="txt_area" runat="server" Text="Category: "></asp:Label>
                <asp:DropDownList ID="ddl_area" runat="server" OnSelectedIndexChanged="DdlAreaSelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label ID="lbl_control_type" runat="server" Text="Control Type: "></asp:Label>
                <asp:DropDownList ID="ddl_control_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_control_type_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div>
                <asp:Label ID="Label2" runat="server" Style="height: 15px;" Text=" "></asp:Label>
                <asp:Button ID="btn_add" runat="server" Text="Add" OnClick="btn_add_Click" />
            </div>
        </div>
        <div style="clear: both">
        </div>
        <div id="ascx_add_container" class="widget_add" runat="server" visible="false">
            <style>
                .ascx_form_container > div
                {
                    line-height: 30px;
                }
                .ascx_form_container
                {
                    float: left;
                    margin-right: 50px !important;
                }
            </style>
            <div class="ascx_form_container">
                <div>
                    <asp:Label ID="lbl_control_name" runat="server" Text="Name"></asp:Label>
                    <asp:TextBox ID="txt_control_name" runat="server" Width="183px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="val_control_name" runat="server" ErrorMessage="Required"
                        ControlToValidate="txt_control_name" ValidationGroup="AddControl" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label ID="lbl_control_path" runat="server" Text="Control Path"></asp:Label>
                    <telerik:RadTextBox ID="txt_control_path" runat="server" Width="343px" />
                    <asp:Button ID="selectFile" OnClientClick="OpenFileExplorerDialog(); return false;"
                        Text="Open..." runat="server" />
                    <script type="text/javascript">
                        function OpenFileExplorerDialog() {
                            var wnd = $find("<%= ExplorerWindow.ClientID %>");
                            wnd.show();

                        }

                        //This function is called from a code declared on the Explorer.aspx page
                        function OnFileSelected(fileSelected) {
                            var textbox = $find("<%= txt_control_path.ClientID %>");
                            textbox.set_value('~' + fileSelected);

                            var image_url = fileSelected.replace("ascx", "png");
                            if (urlExists(image_url) == 200) {

                                $(".widget_image").attr("src", image_url);
                            }
                            else {
                                $(".widget_image").attr("src", "/img/WidgetNoImage.png");
                            }


                        }
                        function urlExists(testUrl) {
                            var http = jQuery.ajax({
                                type: "HEAD",
                                url: testUrl,
                                async: false
                            })
                            return http.status;
                            // this will return 200 on success, and 0 or negative value on error
                        }

                    </script>
                    <telerik:RadWindow runat="server" Width="550px" Height="560px" VisibleStatusbar="false"
                        ShowContentDuringLoad="false" NavigateUrl="~/CP/Widget/Explorer.aspx" ID="ExplorerWindow"
                        Modal="true" Behaviors="Close,Move">
                    </telerik:RadWindow>
                    <asp:RequiredFieldValidator ID="val_control_path" runat="server" ErrorMessage="Required"
                        ControlToValidate="txt_control_path" ValidationGroup="AddControl" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Button ID="btn_add_user_control" runat="server" Text="Add User Control" ValidationGroup="AddControl"
                        OnClick="BtnAddUserControlClick" />
                </div>
            </div>
            <div style="height: 120px">
                <image id="widget_image" class="widget_image" src="" />
            </div>
        </div>
        <div id="html_add_container" class="widget_add" runat="server" visible="false">
            <div>
                <asp:Label ID="lbl_html_name" runat="server" Text="Name"></asp:Label>:
                <asp:TextBox ID="txt_html_name" runat="server" Width="183px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
                    ControlToValidate="txt_html_name" ValidationGroup="AddHtmlControl" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="lbl_html_descr" runat="server" Text="Description"></asp:Label>:
                <asp:TextBox ID="txt_html_descr" runat="server" Width="183px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required"
                    ControlToValidate="txt_html_descr" ValidationGroup="AddHtmlControl" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div>
             <asp:CheckBox ID="chk_html_hide_edit" Text="Hide from Front End Edit" runat="server"  />
            </div>
            <telerik:RadEditor ID="html_editor_text" runat="server" Width="100%" AllowScripts="True"
                ToolsFile="/Styles/ArticlePost.xml">
                <CssFiles>
                    <telerik:EditorCssFile Value="/Styles/EditorStyle.css" />
                </CssFiles>
                <ImageManager UploadPaths="/Pics" EnableAsyncUpload="true" EnableImageEditor="true"
                    EnableThumbnailLinking="true" ViewMode="Thumbnails" ViewPaths="/Pics" />
                <Modules>
                    <telerik:EditorModule Name="RadEditorStatistics" Enabled="true" Visible="true" />
                </Modules>
            </telerik:RadEditor>
            <asp:Button ID="btn_add_html_control" runat="server" Text="Add Html Control" OnClick="btn_add_html_control_Click"
                ValidationGroup="AddHtmlControl" />
            <asp:Button ID="btn_update_html_control" runat="server" Text="Update Html Control"
                Visible="false" ValidationGroup="AddHtmlControl" OnClick="btn_update_html_control_Click" />
        </div>
        <div>
            <telerik:RadGrid ID="gv_widjet_list" runat="server" AllowPaging="True" AllowFilteringByColumn="true"
                AllowSorting="True" CellSpacing="0" DataSourceID="OpenAccessLinqDataSource1"
                GridLines="None" OnSelectedIndexChanged="gv_widjet_list_SelectedIndexChanged"
                OnItemCommand="gv_widjet_list_ItemCommand" OnDataBound="gv_widjet_list_DataBound"
                OnItemDataBound="gv_widjet_list_ItemDataBound">
                <ClientSettings EnablePostBackOnRowClick="true">
                    <Selecting CellSelectionMode="None" AllowRowSelect="True"></Selecting>
                </ClientSettings>
                <MasterTableView DataKeyNames="Widget_id" CommandItemDisplay="Top" PageSize="20"
                    Name="ParentGrid" AutoGenerateColumns="False" DataSourceID="OpenAccessLinqDataSource1">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                    <CommandItemSettings ShowAddNewRecordButton="false" />
                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="Widget_detail_id" DataSourceID="OpenAccessLinqDataSource2"
                            Name="ChildGrid" Width="100%" runat="server">
                            <Columns>
                            </Columns>
                            <ParentTableRelation>
                                <telerik:GridRelationFields DetailKeyField="Widget_id" MasterKeyField="Widget_id" />
                            </ParentTableRelation>
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            </ExpandCollapseColumn>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Widget_id" HeaderText="ID" DataType="System.Int32"
                            FilterControlAltText="Filter Widget_id column" ReadOnly="True" SortExpression="Widget_id"
                            UniqueName="Widget_id">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WidgetType.Widget_type_descr" FilterControlAltText="Filter Widget Type Name column"
                            HeaderText="Widget Type" ReadOnly="True" SortExpression="WidgetType.Widget_type_descr"
                            UniqueName="Widget_type_descr">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <img id="control_img" runat="server" alt="" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Widget_name" HeaderText="Name" FilterControlAltText="Filter Widget_name column"
                            SortExpression="Widget_name" UniqueName="Widget_name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Widget_descr" HeaderText="Description" FilterControlAltText="Filter Widget_descr column"
                            SortExpression="Widget_descr" UniqueName="Widget_descr">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="File_path" FilterControlAltText="Filter File_path column"
                            HeaderText="File Path" SortExpression="File_path" UniqueName="File_path" Visible = "false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ContentTypeId" DataType="System.Int32" FilterControlAltText="Filter ContentTypeId column"
                            HeaderText="ContentTypeId" SortExpression="ContentTypeId" UniqueName="ContentTypeId"
                            Visible="False">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Active_fg" FilterControlAltText="Filter Active column"
                            HeaderText="Active" ReadOnly="True" SortExpression="Active_fg" UniqueName="Active">
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="FrontEditable" FilterControlAltText="Filter FrontEditable column"
                            HeaderText="Front Editable" ReadOnly="True" SortExpression="FrontEditable" UniqueName="FrontEditable">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
            </telerik:RadGrid>
            <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel"
                EntityTypeName="" ResourceSetName="Widgets" Where="ContentTypeId == @ContentTypeId">
                <WhereParameters>
                    <asp:ControlParameter ControlID="ddl_area" DefaultValue="" Name="ContentTypeId" PropertyName="SelectedValue"
                        Type="Int32" />
                </WhereParameters>
            </telerik:OpenAccessLinqDataSource>
            <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource2" runat="server" ContextTypeName="EPM.Data.Model.EPMEntityModel"
                EntityTypeName="" ResourceSetName="Widget_details" Where="Widget_id == @Widget_id">
                <WhereParameters>
                    <asp:SessionParameter DefaultValue="" Name="Widget_id" SessionField="widget_id" Type="Int32" />
                </WhereParameters>
            </telerik:OpenAccessLinqDataSource>
        </div>
        <div style="margin: 10px">
            <asp:Button ID="btn_edit" runat="server" Text="Edit" Visible="False" OnClick="btn_edit_Click" />
            <asp:Button ID="btn_update" runat="server" Text="Update Control" Visible="False"
                OnClick="btn_update_Click" />
            <asp:Button ID="btn_delete" runat="server" Text="Delete" Visible="False" />
            <asp:Button ID="btn_activate" runat="server" Text="Activate" OnClick="btn_activate_Click"
                Visible="False" />
            <asp:Button ID="btn_deactivate" runat="server" Text="Deactivate" OnClick="btn_deactivate_Click"
                Visible="False" />
        </div>
    </div>
    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
