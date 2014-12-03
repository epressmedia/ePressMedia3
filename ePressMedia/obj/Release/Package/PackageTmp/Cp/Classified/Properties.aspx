<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true" CodeBehind="Properties.aspx.cs" Inherits="ePressMedia.Cp.Classified.Properties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<h1><asp:Literal ID="CatName" runat="server" /> Classified Module Properties</h1>
 <epm:SlideDownPanel ID="SlideDownPanel1" runat="server" Title="General Properties" Description="Gerneral properties being used in Classified module" 
                Orientation="Vertical" Enabled="true" Expanded="true">
                  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
                  </telerik:RadAjaxLoadingPanel>
                  <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1"  >
                  
                <div class="sliderContainer">
                
                <div>
                <span class="title">Max Number of Ads Per Classified Category</span>
                <telerik:radnumerictextbox  ID="txt_max_cls" runat="server"  showspinbuttons="true" NumberFormat-DecimalDigits="0"  > </telerik:radnumerictextbox >
                </div>
                <div>
                <span class="title">Number of Hours to show New in Category</span>
                <telerik:radnumerictextbox  ID="txt_hours_new" runat="server"  showspinbuttons="true" NumberFormat-DecimalDigits="0"  > </telerik:radnumerictextbox > Hours
                </div>
                <div>
                    <asp:Button ID="btn_general_setting_save" runat="server" Text="Save" 
                        onclick="btn_general_setting_save_Click" />
                </div>
                </div>
                </telerik:RadAjaxPanel>
                </epm:SlideDownPanel>
</asp:Content>
