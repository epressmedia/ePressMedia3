<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="UDFAttachment.aspx.cs" Inherits="ePressMedia.Cp.UDF.UDFAttachment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OnClientclose(sender, eventArgs) {
            document.location.reload(true);
        }

        function RefreshMemberList(sender, eventArgs) {

            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                 ajaxManager.ajaxRequest("close");
                 return false;
             }


 </script>
        </telerik:RadCodeBlock>
                <style>
                    .rlbGroupRight
                    {
                        margin-right: 30px;
                    }
                </style>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" OnAjaxSettingCreated="RadAjaxManager1_AjaxSettingCreated">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lb_udfgroups" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
            <h1>
                UDF Attachment</h1>
         <div id ="listing_div" runat="server">
    <div>
        <div>Module:<asp:DropDownList ID="ddl_module" runat="server" OnSelectedIndexChanged="ddl_module_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
        <div id="category_panel" runat="server" visible="false"><asp:Label ID="lbl_category" runat="server" Text="Category:"></asp:Label>
            <asp:DropDownList ID="ddl_category" runat="server" OnSelectedIndexChanged ="ddl_category_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></div>
    </div>
    <div>
        <telerik:RadListBox ID="lb_udfgroups" runat="server" AllowReorder =" true" AllowDelete ="true" Width="400px" Height="300px" Visible ="false">
            
        </telerik:RadListBox>
     </div>

                 <div id="group_panel" runat="server" visible="false">
        UDF Group:<asp:DropDownList ID="ddl_udfgroups" runat="server" OnSelectedIndexChanged="ddl_udfgroups_SelectedIndexChanged" AutoPostBack="true">
            
        </asp:DropDownList>
     </div>
             
    <div>
        <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" Visible ="false"/>
        <asp:Button ID="btn_add" runat="server" Text="Add" OnClick="btn_add_Click" Visible ="false"/>
    </div>
             </div>   
</asp:Content>
