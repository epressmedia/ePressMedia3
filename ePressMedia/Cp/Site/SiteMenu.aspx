<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true"
    Inherits="Cp_Site_SiteMenu" CodeBehind="SiteMenu.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/CP/Controls/Toolbox.ascx" tagname="Toolbox" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .canvas
        {
            
            margin-top:15px;
            height: 1%;
            overflow: hidden;
        }
        .treePane, .listPane
        {
            float: left;
            border: 1px solid #dbdbdb;
            background-color: #fff;
            
            height: 540px;
        }
        .treePane
        {
            width: 400px;
            margin-right:20px;
            overflow: auto;
        }
        table.menuInfo
        {
            width: 480px;
            margin: 8px 8px 8px 4px;
            border-collapse: collapse;
            float: left;
        }
        .pane
        {
            border-top: 1px solid #f7f7f7;
            border-left: 1px solid #f7f7f7;
            border-bottom: 1px solid #aaa;
            border-right: 1px solid #aaa;
            height: 1%;
            overflow: hidden;
            background: #eee;
        }
        



        .treePane td
        {
            padding: 2px;
        }
        
        td.data
        {
            border: 1px solid #b0b0b0;
            background-color: #ffffff;
            padding: 6px 8px 6px 8px;
            text-align: left;
        }
        
        td.data label
        {
            color: Black;
        }
        
        .menu_link_button
        {
            border-style: none;
border-color: inherit;
border-width: medium;
background: #4D4D4D;
padding: 3px 9px;
letter-spacing: 1px;
color: #FFF;
border-radius: 3px;
-moz-border-radius: 3px;
-webkit-border-radius: 3px;
-webkit-appearance: none;
cursor: pointer;
margin-left: 5px;
margin-right: 5px;
margin-bottom: 5px;
        }
        
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            setScrollHandler();
        });

        function hideMessageBox() {
            var m = $find('MsgBoxMpe');
            if (m)
                m.hide();
        }


        function setScrollHandler() {
            $('#treePane').scrollTop($('#dataPnl input').val());

            $('#treePane').scroll(function () {
                $('#dataPnl input').val($('#treePane').scrollTop());
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>--%>
    <h1>
        Site Main Menu</h1>
    <div id="dataPnl">
        <asp:HiddenField ID="ScrTop" runat="server" Value="0" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


           <cc1:ModalPopupExtender ID="ConfirmMpe" runat="server" DynamicServicePath="" 
      Enabled="True" TargetControlID="ConfirmBoxTarget" PopupControlID="ModConfirmPanel" 
      BehaviorID="ConfirmMpe" 
      CancelControlID="CancelButton"
      BackgroundCssClass="modalBg" />

    <asp:Button ID="ConfirmBoxTarget" runat="server" style="display:none" />
  
      <asp:Panel ID="ModConfirmPanel" runat="server" CssClass="confirmBox" style="display:none;">
    <div>
      Please select the parent menu where the selected menu moves under.
      <div style="text-align: left;background: white;PADDING: 10px;margin: 10px 0 10px 0;border: 1px solid #CCC;padding: 10px;">
                    <telerik:RadTreeView ID = "RadMenuTree" runat="server" DataFieldID="MenuID" DataFieldParentID="ParentMenuID" SingleExpandPath="True" DataTextField="MenuName" DataValueField = "MenuID" Height="400px" Width="400px">
                    <DataBindings>
                    <telerik:RadTreeNodeBinding Depth="1" Expanded="true" />
                    </DataBindings>
                    </telerik:RadTreeView>
                    </div>
                    <div>
      <asp:Button ID="btn_menuupdate" runat="server" Text=" Move " 
                            OnClientClick="javascript:hideMessageBox();" onclick="btn_menuupdate_Click" />
      <asp:Button ID="btn_menucancel" runat="server" Text=" Cancel " 
            OnClientClick="javascript:hideMessageBox();" />
            </div>
      </div>
      <br />
    </asp:Panel>  


            <asp:Panel ID="MsgBox" runat="server" CssClass="confirmBox" Style="display: none">
                <asp:Label ID="MsgLabel" runat="server" Text="The selected menu cannot be deleted because child menus exist." />
                <br />
                <br />
                <input id="OkButton" type="button" value=" OK " onclick="javascript:hideMessageBox();" />
            </asp:Panel>
            <cc1:ModalPopupExtender ID="MsgBoxMpe" runat="server" DynamicServicePath="" Enabled="True"
                TargetControlID="MsgBoxTarget" PopupControlID="MsgBox" BehaviorID="MsgBoxMpe"
                BackgroundCssClass="modalBg" />
            <asp:Button ID="MsgBoxTarget" runat="server" Style="display: none" />

            <div>
            <uc:toolbox id="toolbox1" runat="server" ButtonAvailable= "up,down,move,add,edit,save,delete,save changes,cancel" ></uc:toolbox>
            </div>

            <div class="canvas">
   
                <div>
                    <div id="treePane" class="treePane">
                        <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                            ShowExpandCollapse="True">
                            <NodeStyle VerticalPadding="0px" />
                            <SelectedNodeStyle Font-Bold="True" BackColor="#CCCCCC" />
                        </asp:TreeView>
                    </div>
                    <%--<div class="cmdPnl"><span class="sep24" style="font-weight:bold;">메뉴 상세 정보</span></div>--%>
                    <div style="overflow: hidden">
                    
                    <table  runat="server" id="menuInfo" visible="false">
                        <tr>
                            <td class="label" style="width: 120px;">
                                Menu Label*
                            </td>
                            <td class="data">
                                <asp:TextBox ID="MenuLabel" runat="server" Width="160" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Menu Description
                            </td>
                            <td class="data">
                                <asp:TextBox ID="Desc" runat="server" Width="240" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Content Type
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="TypeList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TypeList_SelectedIndexChanged" />
                            </td>
                        </tr>
                        <tr runat="server" id="dataRow">
                            <td class="label">
                                Category
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="DataList" runat="server" AutoPostBack="True"  />
                            </td>
                        </tr>
                        <tr runat="server" id="viewRow">
                            <td class="label">
                                Link Page
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="ViewList" runat="server" />
                            </td>
                        </tr>
                        <tr runat="server" id="urlRow">
                            <td class="label">
                                URL
                            </td>
                            <td class="data">
                                <asp:TextBox ID="Url" runat="server" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Link Type
                            </td>
                            <td class="data">
                                <asp:DropDownList ID="TargetList" runat="server">
                                    <asp:ListItem Selected="True" Text="Current Page" Value="_self" />
                                    <asp:ListItem Text="New Page" Value="_blank" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr runat="server" id="submenu_image">
                            <td class="label">
                                Submenu Image
                            </td>
                            <td class="data">
                                <asp:TextBox ID="Submenu_image_url" runat="server" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Visible?
                            </td>
                            <td class="data">
                                <asp:CheckBox ID="ChkVisible" runat="server" Text="Show Menu" />
                            </td>
                        </tr>
                          <tr>
                            <td class="data" colspan="2" >
                                <asp:LinkButton ID="lk_page" Visible = "false" CssClass="menu_link_button"
                                    runat="server">Open Page</asp:LinkButton>
                                <asp:LinkButton ID="lk_config" Visible = "false"   target="_blank" CssClass="menu_link_button"
                                    runat="server">Configuration</asp:LinkButton>
                                
                            </td>
                        </tr>
                            <tr>
                            <td class="data" colspan="2">
                                <asp:Label ID="ErrMsg" runat="server" Text="" Style="color:Red"></asp:Label>
                            
                            </td>
                        </tr>
                    </table>
                    
                    &nbsp;
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(endRequest);

        function endRequest(sender, args) {
            setScrollHandler();
        }
    </script>
</asp:Content>
