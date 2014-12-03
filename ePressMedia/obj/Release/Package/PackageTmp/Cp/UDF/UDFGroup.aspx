<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="UDFGroup.aspx.cs" Inherits="ePressMedia.Cp.UDF.UDFGroup" %>
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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" OnAjaxSettingCreated="RadAjaxManager1_AjaxSettingCreated">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="Panel2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Panel2" />
            </UpdatedControls>
        </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    
            
            <h1>
                UDF Group Assignment</h1>
             <div id="listing_div" runat="server">
                
       <telerik:RadSplitter ID="RadSplitter1" runat="server" Orientation="Vertical" Width="90%">
          <telerik:RadPane ID="navigationPane" runat="server" Width="180" Locked="true">
              <div style="height:90%">
               <div class="span_title">UDF Groups</div>
              <telerik:RadListView ID="rpt_group" runat="server" OnItemDataBound="rpt_group_ItemDataBound"  >
                  <ItemTemplate>
                      <div class="udf_repeater"><asp:Button ID="lbl_groupname" runat="server" OnClick="lbl_groupname_Click" ></asp:Button> </div>
                  </ItemTemplate>
              </telerik:RadListView>
                  </div>
              <div style="height:10%; float:right">
                  <asp:Button ID="btn_groupadd" runat="server" Text="Add" OnClick="btn_groupadd_Click" CssClass="blue_button"/>
              </div>
              
              
          </telerik:RadPane>
          <telerik:RadSplitBar ID="RadSplitbar1" runat="server">
          </telerik:RadSplitBar>
          <telerik:RadPane ID="contentPane" runat="server" Width="180" Locked="true" >
                             <div style="height:90%">
                                 <asp:Panel ID="Panel1" runat="server" >
               <div class="span_title">Members</div>
                                 <telerik:RadListView ID="rpt_members" runat="server" OnItemDataBound="rpt_members_ItemDataBound" OnItemCommand="rpt_members_ItemCommand" >
                                   
                                     <ItemTemplate>
                                         <div class="udf_repeater"><asp:Button ID="lbl_member" runat="server" OnClick="lbl_member_Click" ></asp:Button> </div>
                                     </ItemTemplate>
                                 </telerik:RadListView>
                                     </asp:Panel>

                  </div>
              <div style="height:10%; float:right">
                  <asp:Button ID="btn_memberadd" runat="server" Text="Add" OnClick="btn_memberadd_Click" CssClass="blue_button"/>
              </div>
          </telerik:RadPane>
           <telerik:RadSplitBar ID="RadSplitbar2" runat="server">


          </telerik:RadSplitBar>
          <telerik:RadPane ID="RadPane1" runat="server" >
                 <telerik:RadSplitter ID="RadSplitbar3" runat="server" Orientation="Horizontal" >
                     <telerik:RadPane ID="udf_group_pane" runat="server"  >
                         <div id="udf_group_div" runat="server">
               <div class="span_title"><asp:Label ID="lbl_groupname" runat="server"></asp:Label></div>
                             <div><asp:HiddenField ID="lbl_GroupID" runat="server" /></div>
                         <div>Description: <asp:TextBox ID="txt_group_description" runat="server"></asp:TextBox> </div>
                         <div>No of Columns: <telerik:RadNumericTextBox ID="txt_noofcolumns" runat="server" MinValue="1" MaxLength="10" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox> </div>
                         <div style="float:right"><asp:Button ID="btn_update_group" runat="server" Text="Save" CssClass="blue_button" Visible="false" OnClick="btn_update_group_Click" />
                             <asp:Button ID="btn_delete_group" runat="server" Text="Delete" Visible="false" OnClick="btn_delete_group_Click" CssClass="blue_button" />
                         </div>
                             </div>
          </telerik:RadPane>
               <telerik:RadPane ID="udf_assignment_pane" runat="server"  >
                   <asp:Panel ID="Panel2" runat="server" >
                   <div id="udf_assignment_div" runat="server" visible="false">
                       
                <div class="span_title"><asp:Label ID="lbl_udfname" runat="server"></asp:Label></div>
                       <div><asp:HiddenField ID="lbl_assignment_id" runat="server" /></div>
                         <div>Default Value: <asp:TextBox ID="txt_default_value" runat="server"></asp:TextBox> </div>
                   <div><asp:CheckBox ID="chk_required" runat="server" Text="Required"></asp:CheckBox> </div>
                   <div><asp:CheckBox ID="chk_search" runat="server" Text="Searchable"></asp:CheckBox> </div>
                   <div><asp:CheckBox ID="chk_active" runat="server" Text="Active"></asp:CheckBox> </div>

                         <div>Display Order: <telerik:RadNumericTextBox ID="txt_display_order" runat="server" MinValue="1" MaxLength="999" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox> </div>
                         
                   <div style="float:right">
                       
                       <asp:Button ID="btn_remove_assignment" runat="server" Text="Remove" CssClass="blue_button" OnClick="btn_remove_assignment_Click"/>
                       <asp:Button ID="btn_update_assignment" runat="server" Text="Save" CssClass="blue_button" OnClick="btn_update_assignment_Click"/></div>
                       </div>
                         </asp:Panel>
          </telerik:RadPane>
                     
          </telerik:RadSplitter>
                
          </telerik:RadPane>
     </telerik:RadSplitter>
                 </div>
                
</asp:Content>
