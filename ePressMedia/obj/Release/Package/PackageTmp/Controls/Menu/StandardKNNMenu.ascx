<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Menu_StandardKNNMenu" Codebehind="StandardKNNMenu.ascx.cs" %>

   <div id="menuBar">
    <div id="mainMenu">
      <div class="menuPnl">
        <asp:Repeater ID="MmRepeater" runat="server">
          <ItemTemplate>
            <a id="A1" runat="server" href='<%# Eval("Url") %>' target='<%# Eval("Target") %>'><%# Eval("Label") %></a>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>

    <div id="subMenu">
      <div class="menuPnl">
        <asp:Repeater ID="MmRepeater2" runat="server" 
          onitemdatabound="MmRepeater2_ItemDataBound">
          <ItemTemplate>
            <span class="sep">&nbsp;</span>
            <span class="sm">
              <asp:Repeater ID="SmRepeater" runat="server">
                <ItemTemplate>
                  <a id="MenuLink" runat="server" href='<%# Eval("Url") %>' target='<%# Eval("Target") %>'><%# Eval("Label") %></a>
                </ItemTemplate>
              </asp:Repeater>
            </span>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>
  </div>

