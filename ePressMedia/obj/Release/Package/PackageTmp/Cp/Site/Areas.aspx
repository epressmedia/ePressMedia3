<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Site_Areas" Codebehind="Areas.aspx.cs" %>
<%@ Register Src="~/CP/Controls/Toolbox.ascx" TagName="Toolbox" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
  .idxPnl { margin-bottom:24px; }
  .idxPnl a { font-size:14pt; font-weight:bold; color:#3333cc; margin-left:12px; }
  .idxPnl a.sel { font-size:18pt; color:#ff0080; }
  .vis { background-color:Yellow; }
  
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <h1>Site Area Maintenance</h1>
          <div>
            <uc:Toolbox ID="toolbox1" runat="server" ButtonAvailable="delete,save"></uc:Toolbox>
        </div>
  <div>
  <div style="font-weight:bold">Available Areas</div>
  <div>
    <asp:DropDownList ID="ddl_province" runat="server" >
    </asp:DropDownList>
    <asp:Button ID="btn_add_area" runat="server" Text="Add" 
          onclick="btn_add_area_Click" />
          </div>
    </div>
    <div>
        <div style="font-weight:bold">Selected Areas</div>
        <div>
        <asp:ListBox ID="lb_areas" runat="server" Height="300px" 
            onselectedindexchanged="lb_areas_SelectedIndexChanged" Width="200px" 
            AutoPostBack="True"></asp:ListBox>
            </div>
    </div>
</asp:Content>

