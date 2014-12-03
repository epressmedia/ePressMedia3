<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_ForumThreadBox" Codebehind="ForumThreadBox.ascx.cs" %>
        <asp:HiddenField ID="ForumID" runat="server" />
        <div class="secPnl">
          <%--<p class="title">
            <asp:HyperLink ID="MoreLink" runat="server" />
          </p>--%>
          <asp:HyperLink ID="MoreLink" runat="server">
            <asp:Image ID="HdrImg" runat="server" />
          </asp:HyperLink>
          <ul class="ttlList">
            <asp:Repeater ID="Repeater1" runat="server">
              <ItemTemplate>

                <li>
                <a runat="server" href='<%# "~/Forum/ViewThread.aspx?p=" + Eval("ForumId") + "&tid=" + Eval("ThreadId")%>'>
                <%# Eval("ShortSubject") %></a></li>

              
              </ItemTemplate>
            </asp:Repeater>
          </ul>
        </div>