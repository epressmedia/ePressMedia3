<%@ Page Title="" Language="C#" MasterPageFile="~/Cp/Master.master" AutoEventWireup="true" Inherits="Cp_Calendar_CalendarAccessControl" Codebehind="CalendarAccessControl.aspx.cs" %>
<%@ Register src="../Controls/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
  <h1><asp:Literal ID="CalName" runat="server" /> 권한 관리</h1>

  <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>

