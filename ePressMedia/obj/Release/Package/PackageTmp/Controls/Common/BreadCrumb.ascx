<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="BreadCrumb.ascx.cs" Inherits="ePressMedia.Controls.Common.BreadCrumb" %>

            <asp:Panel ID="BreadCrumb_Panel" runat="server" CssClass="brdCrmb" >
          <a id="A1" runat="server" href="~/">Home</a>
          <asp:Literal ID="BcPath" runat="server" />
        </asp:Panel>
