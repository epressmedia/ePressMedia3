<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="EmailTemplates.aspx.cs" Inherits="ePressMedia.Cp.Site.EmailTemplates" %>
<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function ReloadOnClientClose(sender, eventArgs) {
                $find("<%=  RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                //window.location.href=window.location.href;
            }

        </script>
    </telerik:RadScriptBlock>

       <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents ></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="EmailTemplateDiv">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="EmailTemplateDiv" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
        <h1>
        Email Templates</h1>
        <div id="EmailTemplateDiv" runat="server">
        <div>
        <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "up,down,add,edit" ></uc:toolbox>
                        <div id ="AddPanel" runat="server" class="cmdPnl" visible="false">
            <div>
        <asp:Label ID="lbl_email_name" runat="server" Text="Email Template Name:" />
        <asp:TextBox ID="txt_email_name" runat="server" />
                </div><div>
                            <asp:Label ID="lbl_form_description" runat="server" Text="Description:" />
        <asp:TextBox ID="txt_email_description" runat="server" />
                      </div>
               
                            

                <div>
        <asp:Button ID="btn_add" runat="server" Text="Add" OnClick="btn_add_Click" />
                </div>
                
            </div>
            <div style="clear:both"></div>
        </div>
            <telerik:RadGrid ID="EmailTemplateGrid" runat="server" Width="100%" AutoGenerateColumns="False"
        AllowSorting="false" onselectedindexchanged="EmailTemplateGrid_SelectedIndexChanged" OnItemCommand = "EmailTemplateGrid_ItemCommand">
        <MasterTableView Width="100%" ShowHeader="true"  DataKeyNames="EmailEventId">
            <Columns>
                           <telerik:GridBoundColumn UniqueName="EmailEventId" SortExpression="" HeaderText="<a>Email Event ID</a>" Visible="false"
                    DataField="EmailEventId">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="EmailEventName" SortExpression="" HeaderText="<a>Email Name</a>"
                    DataField="EmailEventName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Description" SortExpression="" HeaderText="<a>Description</a>"
                    DataField="Description" >
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText = "<a>Actions</a>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="EditLink" runat="server" CommandName="modify" CommandArgument='<%# Eval("EmailEventId") %>'
                                        CssClass="icon-5 info-tooltip" title="Edit" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
            </Columns>
            <NoRecordsTemplate>
                <div style="height: 30px; cursor: pointer;">
                    No items to view</div>
            </NoRecordsTemplate>
            
        </MasterTableView>
        
        <ClientSettings AllowRowsDragDrop="True" EnableRowHoverStyle="true">
            <Selecting EnableDragToSelectRows="false"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
        </div>

</asp:Content>
