<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_FitPager" Codebehind="FitPager.ascx.cs" %>
<asp:Panel ID="PagerContainer" runat="server" HorizontalAlign="Center">
    <asp:LinkButton ID="PrevLink" runat="server" onclick="PrevLink_Click" CssClass="selPage" >◀</asp:LinkButton>
    <asp:LinkButton ID="Link1" runat="server" onclick="PageNumLink_Click" Width="18px" >1</asp:LinkButton>
    <asp:LinkButton ID="Link2" runat="server" onclick="PageNumLink_Click" Width="18px" >2</asp:LinkButton>
    <asp:LinkButton ID="Link3" runat="server" onclick="PageNumLink_Click" Width="18px" >3</asp:LinkButton>
    <asp:LinkButton ID="Link4" runat="server" onclick="PageNumLink_Click" Width="18px" >4</asp:LinkButton>
    <asp:LinkButton ID="Link5" runat="server" onclick="PageNumLink_Click" Width="18px" >5</asp:LinkButton>
    <asp:LinkButton ID="Link6" runat="server" onclick="PageNumLink_Click" Width="18px" >6</asp:LinkButton>
    <asp:LinkButton ID="Link7" runat="server" onclick="PageNumLink_Click" Width="18px" >7</asp:LinkButton>
    <asp:LinkButton ID="Link8" runat="server" onclick="PageNumLink_Click" Width="18px" >8</asp:LinkButton>
    <asp:LinkButton ID="Link9" runat="server" onclick="PageNumLink_Click" Width="18px" >9</asp:LinkButton>
    <asp:LinkButton ID="Link10" runat="server" onclick="PageNumLink_Click" Width="18px" >10</asp:LinkButton>
    <asp:LinkButton ID="NextLink" runat="server" onclick="LinkNext_Click" CssClass="selPage" >▶</asp:LinkButton>
</asp:Panel>