<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_MainPopup" Codebehind="MainPopup.ascx.cs" %>

      <asp:Panel ID="Pop1" runat="server" style="position:absolute; left:50px; top:50px; z-index:30" CssClass="popupBox">
        <asp:Label ID="PopupText1" runat="server" />
        <div style="text-align:center">
          <asp:HyperLink ID="PopupLink1" runat="server" Text="[자세히보기]" />
          <span class="sep24"></span>
          <input type="checkbox" runat="server" id="PopCheck1" />
          <label for="PopCheck1">24시간동안 이창 다시 열지 않기</label>
          <span class="sep24"></span>
          <input id="OkButton1" runat="server" type="button" value=" 닫기 " onclick="javascript:hidePopup1();" />
        </div>
      </asp:Panel>
      
      <asp:Panel ID="Pop2" runat="server" style="position:absolute; top:50px; z-index:31;" CssClass="popupBox">
        <asp:Label ID="PopupText2" runat="server" />
        <div style="text-align:center">
          <asp:HyperLink ID="PopupLink2" runat="server" Text="[자세히보기]" />
          <span class="sep24"></span>
          <input type="checkbox" runat="server" id="PopCheck2" />
          <label for="PopCheck2">24시간동안 이창 다시 열지 않기</label>
          <span class="sep24"></span>
          <input id="OkButton2" runat="server" type="button" value=" 닫기 " onclick="javascript:hidePopup2();" />
        </div>
      </asp:Panel>
