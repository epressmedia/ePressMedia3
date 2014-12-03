<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ASCXControlEditor.ascx.cs"
    Inherits="ePressMedia.Pages.ASCXControlEditor" %>
<div class="ascx_control_editor">
    <div>
        <asp:Label ID="lbl_title" Text="" runat="server" class="editor_title" />
    </div>
    <div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Parameters"></asp:Label>
        </div>
        <div>
            <asp:Repeater ID="ctrl_params" runat="server" OnItemDataBound="ctrl_params_ItemDataBound">
                <HeaderTemplate>
                    <div style="border: 1px solid gray; padding: 5px; margin: 5px;">
                        <asp:Label ID="ctrl_params_header" runat="server" Text=""></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="margin: 5px 0 5px 0">
                        <asp:Label ID="param_display_name" runat="server" Text=""></asp:Label>
                        <asp:Label ID="param_name" runat="server" Text='<%# Eval("field_name")%>' Visible="false"></asp:Label>:
                        <asp:Label ID="param_type" runat="server" Text='<%# Eval("field_data_type")%>' Visible="false"></asp:Label>
                        <asp:Label ID="param_assembly" runat="server" Text='<%# Eval("AssemblyName")%>' Visible="false"></asp:Label>
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
                        <asp:RequiredFieldValidator InitialValue="" ID="param_value_ddl_required" Operator="NotEqual"
                            ValueToCompare="" ValidationGroup="ControlAddValidation" runat="server" ControlToValidate="param_value_list"
                            ErrorMessage="Required" Enabled='<%# Eval("required_fg")%>'></asp:RequiredFieldValidator>
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
        </div>
        <div>
            <div class="ascx_control_editor_field">
                <div>
                    <asp:Label ID="Label1" runat="server" Text="Style"></asp:Label>
                    <asp:TextBox ID="txt_control_container_style" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="Label3" runat="server" Text="Class"></asp:Label>
                    <asp:TextBox ID="txt_control_css_class" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div style="margin: 10px 0 20px 0;">
        <div style="float: left">
            <telerik:RadButton ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" OnClientClicked="OnClientDeleteClicked" Visible="false">
                <Icon PrimaryIconUrl="/img/trash.png" />
            </telerik:RadButton>
        </div>
        <div style="float: right; height: 22px;">
            <telerik:RadButton ID="btn_Cancel" runat="server" Text="Close" OnClick="btn_Cancel_Click">
                <Icon PrimaryIconCssClass="rbCancel" />
            </telerik:RadButton>
            <telerik:RadButton ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click"
                ValidationGroup="ControlAddValidation">
                <Icon PrimaryIconCssClass="rbSave" />
            </telerik:RadButton>
        </div>
        </div>
    </div>
</div>
