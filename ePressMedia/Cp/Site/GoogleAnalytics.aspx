<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.Master" AutoEventWireup="true"
    CodeBehind="GoogleAnalytics.aspx.cs" Inherits="ePressMedia.Cp.Site.GoogleAnalytics" %>

<%@ Register Src="~/CP/Controls/Toolbox.ascx" TagName="Toolbox" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h1>
        Google Analytics<asp:Label ID="lbl_current" runat="server" Text=""></asp:Label></h1>
    <div style="margin: 10px; padding: 10px; background: #F0F0F0;">
        <div style="font-size:16px; font-weight:bold;  margin-bottom:5px;">Google Analytics Login</div>
        Email Address:
        <asp:TextBox ID="txt_emailaddress" runat="server"></asp:TextBox>
        Password:<asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div id="edit_panel" runat="server" visible="false">
        <div>
            <uc:Toolbox ID="toolbox1" runat="server" ButtonAvailable="delete,save,cancel"></uc:Toolbox>
        </div>
        <div style="margin: 10px">
            <div style="font-size: 20px; font-weight: bold; margin-bottom: 5px">
                Available Google Analytics Accounts
            </div>
            <div>
            <asp:Label ID="txt_gaaccount" runat="server" Text="Account: "></asp:Label>
            <asp:DropDownList ID="ddl_gaaccount" runat="server" OnSelectedIndexChanged="ddl_gaaccount_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
            </div>
            <div>
            <asp:Label ID="lbl_webproperty" runat="server" Text="Web Property: "></asp:Label>
            <asp:DropDownList ID="ddl_gawebproperty" runat="server" >
            </asp:DropDownList>
            </div>
            <div style ="display:none">
            <asp:Label ID="lbl_profile" runat="server" Text="Profile: "></asp:Label>
            <asp:DropDownList ID="ddl_gaprofile" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList>
            </div>
        </div>
    </div>
</asp:Content>
