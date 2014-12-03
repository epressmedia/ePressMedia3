<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PageBuilder.ascx.cs"
    Inherits="ePressMedia.Cp.Controls.PageBuilder" %>

    <%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<style>
    .my
    {
        color: white !important;
padding-top: 0px !important;
padding-bottom: 0px !important
    }
</style>
<telerik:RadAjaxLoadingPanel ID ="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
    
        <div class="PageBuilder_Controls" id = "PageBuilder_Controls" runat="server">
            <div class="toolBar" style="text-align: right; border-top: 0px;">
                <span class="btnSep"></span>
                <asp:LinkButton ID="btn_SavePage" runat="server" Text="<img src='../Img/save.png' alt='Save' /> Save Page Configuration"
                    Visible="true" OnClick="btn_SavePage_Click" OnClientClick="javascript:if(!confirm('Are you sure you would like to save the change?')){return false;}" />
                <span class="btnSep"></span>
            </div>
            <div style="float: right">

            </div>
            <div style="clear: both">
            </div>
            <table width="100%" class="PageBuilder_Table">
                <tr  runat="server">
                    <td style="width:50%">
                        <div>Master Page:</div>
                        <asp:DropDownList ID="ddl_master" runat="server" OnSelectedIndexChanged="DdlMasterSelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:Button ID="btn_update_cont_struct" runat="server" OnClick="BtnUpdateContStructClick"
                            Visible="false" Text="Update placeholder structure" OnClientClick="javascript:if(!confirm('The Master Page Structure will be updated. Are you sure you would like to update the master page structure?')){return false;}" />
                    </td>
                    <td style="width:50%">
                        <div>Content Placeholders:</div>
                                                <asp:DropDownList ID="ddl_cont_placedholer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlContPlacedholerSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                <div>Controls:</div>
                <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "add,edit,save,delete,cancel" ></uc:toolbox>

                                  <div id="div_control_type" runat="server" style="padding: 10px; margin: 5px; background-color: #F3F5F5"
                            visible="false">
                            <asp:Label ID="lbl_control_type" runat="server" Text="Control Type:"></asp:Label>
                            <asp:DropDownList ID="ddl_control_types" runat="server" OnSelectedIndexChanged="DdlControlTypesSelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div id="div_ascx_control" runat="server" style="padding: 10px; margin: 5px; background-color: #F3F5F5"
                            visible="False">
                            <telerik:RadComboBox ID="ddl_contrls" runat="server" AutoPostBack="true" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" Width="600px" DataTextField="Widget_name" DataValueField="Widget_id"
                                EmptyMessage="Select a Control" OnSelectedIndexChanged="DdlContrlsSelectedIndexChanged">
                                <HeaderTemplate>
                                    <ul>
                                        <li class="col1">Name</li>
                                        <li class="col2">Description</li>
                                        <li class="col3">Path</li>
                                    </ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <ul>
                                        <li class="col1">
                                            <%# DataBinder.Eval(Container.DataItem, "Widget_name") %></li>
                                        <li class="col2">
                                            <%# DataBinder.Eval(Container.DataItem, "Widget_descr") %></li>
                                        <li class="col3">
                                            <%# DataBinder.Eval(Container.DataItem, "File_Path") %></li>
                                    </ul>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                            <asp:Repeater ID="ctrl_params" runat="server" OnItemDataBound="ctrl_params_ItemDataBound">
                                <HeaderTemplate>
                                    <div style="border: 1px solid gray; padding: 5px; margin: 5px;">
                                        <asp:Label ID="ctrl_params_header" runat="server" Text=""></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="margin: 5px 0 5px 0">
                                        <asp:Label ID="param_name" runat="server" Text='<%# Eval("field_name")%>'></asp:Label>:
                                        <asp:Label ID="param_type" runat="server" Text='<%# Eval("field_data_type")%>' Visible="false"></asp:Label>
                                        <asp:TextBox ID="param_value" runat="server" ReadOnly='<%# Eval("read_only_fg")%>'
                                            Text='<%# Eval("default_value")%>' Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="param_value_int" runat="server" ReadOnly='<%# Eval("read_only_fg")%>'
                                            Text='<%# Eval("default_value")%>' Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="param_value_list" runat="server" Visible="false">
                                        </asp:DropDownList>
                                        <asp:RadioButton ID="param_value_true" runat="server" Visible="false" Text="Yes"
                                            GroupName="param_value_radio" />
                                        <asp:RadioButton ID="param_value_false" runat="server" Visible="false" Text="No"
                                            GroupName="param_value_radio" />
                                        <asp:Image runat="server" ID="helpicon" ImageUrl="../../Img/helpIconSmall.gif" ToolTip='<%# Eval("field_descr") %>' />
                                        <asp:RequiredFieldValidator ID="param_required" runat="server" ErrorMessage="Required"
                                            ControlToValidate="param_value" ValidationGroup="ControlAddValidation" Visible="false"
                                            Enabled='<%# Eval("required_fg")%>'></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="param_value_int_required" runat="server" ErrorMessage="Required"
                                            ControlToValidate="param_value_int" ValidationGroup="ControlAddValidation" Visible="false"
                                            Enabled='<%# Eval("required_fg")%>'></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ValidationExpression="^\d+$" ID="param_value_int_datatype"
                                            runat="server" ErrorMessage="Must be number" ControlToValidate="param_value_int"
                                            ValidationGroup="ControlAddValidation" Visible="false"></asp:RegularExpressionValidator>
                                        <asp:PlaceHolder ID="controlSpot" runat="server"></asp:PlaceHolder>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            <div>
                                <asp:Label ID="lbl_control_container_style" runat="server" Text="Control Container Style: "></asp:Label>
                                <asp:TextBox ID="txt_control_container_style" runat="server" Width="100%"></asp:TextBox>
                            </div>
                                                        <div>
                                <asp:Label ID="Label6" runat="server" Text="Class: "></asp:Label>
                                <asp:TextBox ID="txt_ascx_class" runat="server" Width="100%"></asp:TextBox>
                            </div>

                        </div>
                        <div id="div_html_control" runat="server" style="padding: 10px; margin: 5px; background-color: #F3F5F5;"
                            visible="false">
                            <div>
                            <asp:Label ID="lbl_html" runat="server" Text="HTML Control: "></asp:Label>
                            <asp:DropDownList ID="ddl_html_control" runat="server">
                            </asp:DropDownList>
                                                          </div>                          <div>
                                <asp:Label ID="Label4" runat="server" Text="Container Style: "></asp:Label>
                                <asp:TextBox ID="txt_html_style" runat="server" Width="100%"></asp:TextBox>
                            </div>

                              <div>
                                <asp:Label ID="Label5" runat="server" Text="Class: "></asp:Label>
                                <asp:TextBox ID="txt_html_class" runat="server" Width="100%"></asp:TextBox>
                            </div>

                        </div>
                        <div id="div_pic_control" runat="server" style="padding: 10px; margin: 5px; background-color: #F3F5F5;"
                            class="div_pic_control" visible="false">
                            <div>
                                <asp:Label ID="lbl_pic_name" runat="server" Text="Image Name: "></asp:Label>
                                <asp:TextBox ID="txt_pic_name" runat="server" Width="200px"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="lbl_pic" runat="server" Text="Image URL: "></asp:Label>
                                <asp:TextBox ID="txt_pic" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="lbl_pic_style" runat="server" Text="Image Style: "></asp:Label>
                                <asp:TextBox ID="txt_pic_style" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="lbl_pic_container_style" runat="server" Text="Image Container Style: "></asp:Label>
                                <asp:TextBox ID="txt_pic_container_style" runat="server" Width="100%"></asp:TextBox>
                            </div>
                                                        <div>
                                <asp:Label ID="Label1" runat="server" Text="Link URL(href): "></asp:Label>
                                <asp:TextBox ID="txt_pic_link_url" runat="server" Width="100%"></asp:TextBox>
                            </div>
                                                        <div>
                                <asp:Label ID="Label2" runat="server" Text="Link Type(target): "></asp:Label>
                                <asp:TextBox ID="txt_pic_link_target" runat="server" Width="100%"></asp:TextBox>
                            </div>
                                                        <div>
                                <asp:Label ID="Label3" runat="server" Text="Class: "></asp:Label>
                                <asp:TextBox ID="txt_pic_class" runat="server" Width="100%"></asp:TextBox>
                            </div>

                        </div>

                          <div id="div_ad_control" runat="server" style="padding: 10px; margin: 5px; background-color: #F3F5F5;"
                             visible="false">
                            <div>
                                <asp:Label ID="Label7" runat="server" Text="Ad Zone: "></asp:Label>
                                <telerik:RadComboBox ID="cbo_zone" runat="server"></telerik:RadComboBox>
                            </div>
                            <div>
                                <asp:Label ID="Label10" runat="server" Text="Zone Container Style: "></asp:Label>
                                <asp:TextBox ID="txt_zone_style" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="Label13" runat="server" Text="Class: "></asp:Label>
                                <asp:TextBox ID="txt_zone_class" runat="server" Width="100%"></asp:TextBox>
                            </div>

                        </div>

                        <div style="padding:5px;">
  
                            <telerik:RadGrid ID="ControlGrid" runat="server" Width="100%" 
                                OnRowDrop="ControlGrid_RowDrop" AutoGenerateColumns="False" AllowSorting="false"
                                onselectedindexchanged="ControlGrid_SelectedIndexChanged"  >
                                <MasterTableView Width="100%" ShowHeader = "true"   DataKeyNames="ID" >
                                    <Columns>

                                    <telerik:GridButtonColumn Text="Select" CommandName="Select">
                </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn  UniqueName="Seq"
                    SortExpression="" HeaderText="<a>Sequence</a>" DataField="Seq"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn  UniqueName="ID"
                    SortExpression="" HeaderText="<a>Control ID</a>" DataField="ID" AllowSorting = "false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Name"
                    SortExpression="" HeaderText="<a>Widget Name</a>" DataField="Name"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn  UniqueName="Path"
                    SortExpression="" HeaderText="<a>Path</a>" DataField="Path"></telerik:GridBoundColumn>

                                        <telerik:GridDragDropColumn HeaderStyle-Width="18px" Visible="false">
                                        </telerik:GridDragDropColumn>
                                        
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
                </td>
                </tr>

                <tr>
                    <td>
                        <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                            <ProgressTemplate>
                                Loading...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <td style="text-align: right">
                    </td>
                </tr>
            </table>
        </div>
        <div class="PageBuilder_XMLDiv">
            <div style="text-align: right">

                            <asp:Label ID="lbl_tempalte" runat="server" Text="Template:" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddl_template" runat="server" Visible="false">
                </asp:DropDownList>
                <asp:Button ID="btn_template" runat="server" Visible="false" Text="Import Template"
                    OnClick="btn_template_Click" OnClientClick="javascript:if(!confirm('Current page setting will be overwritten. Are you sure you would like to apply this template?')){return false;}"
                    Style="margin-bottom: 0px" />
                    <asp:Button ID="btn_page_preview" runat="server" Text="Preview" 
                                onclick="btn_page_preview_Click"  />

             
                <asp:Label ID="PageBuilder_ShowXML" runat="server" Text="Show XML" CssClass="PageBuilder_ShowXML" />
            </div>
            <div id="PageBuilder_XML" class="PageBuilder_XML" style="display: none">
                <div>
                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="false" TextMode="MultiLine" Rows="20"
                        Width="100%"></asp:TextBox>
                </div>
                <div style="padding-top: 10px; text-align: right;">
                    <asp:Button ID="btn_import" runat="server" Text="Import XML" OnClick="BtnImportClick" />
                      
                </div>
            </div>
                
        </div>
</telerik:RadAjaxPanel>
