<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HtmlControlEditor.ascx.cs"
    Inherits="ePressMedia.Pages.HtmlControlEditor" %>

<div class="html_control_editor">
<div>
    <asp:Label ID="lbl_title" Text="" runat="server" class="editor_title" />
</div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
<div class="select_fields" >
    <asp:Label ID="lbl_html" runat="server" Text="Html"></asp:Label>
    <telerik:RadComboBox ID="ddl_htmlcontrols" runat="server" OnSelectedIndexChanged="ddl_htmlcontrols_ItemSelected"
        AutoPostBack="true">
    </telerik:RadComboBox>
</div>
<div style="clear: both">
</div>
<div>

    
    
    <telerik:RadEditor ID="html_editor" runat="server"  Width="100%" AllowScripts="True" ToolsFile="/Styles/ArticlePost.xml">
                                   <CssFiles>
                            <telerik:EditorCssFile Value="/Styles/EditorStyle.css" />
                        </CssFiles>
                        <ImageManager UploadPaths="/Pics" EnableAsyncUpload="true" EnableImageEditor="true"
                            EnableThumbnailLinking="true" ViewMode="Thumbnails" ViewPaths="/Pics" />
                         
    </telerik:RadEditor>
</div>
</telerik:RadAjaxPanel>
        <div>
            <div class="html_control_editor_field">
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