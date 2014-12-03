<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forum_RecentThreads" Codebehind="RecentThreads.ascx.cs" %>
          <asp:HiddenField ID="ForumIDs" runat="server" />
          <div class="frmSecTtl">
            <asp:HyperLink ID="HdrLink" runat="server">
            <asp:Image ID="HdrImg" runat="server" />
            </asp:HyperLink>
            <%--asp:Literal ID="SecTitle" runat="server" /--%>
          </div>
          <div class="frmPnl">
            <ul>
            <asp:Repeater ID="Repeater1" runat="server">
              <ItemTemplate>
                <li><span class="cat"><%# Eval("ForumName", "[{0}]") %></span>
                  <a href='<%# "Forum/ViewThread.aspx?p=" + Eval("ForumId") + "&tid=" + Eval("ThreadId")%>'>
                  <%# Eval("ShortSubject") %></a>
                  <img id="img_new" runat="server" src="~/Img/inew.gif" alt=""
                    Visible="false" />
                </li>
              </ItemTemplate>
            </asp:Repeater>
            </ul>
          </div>
