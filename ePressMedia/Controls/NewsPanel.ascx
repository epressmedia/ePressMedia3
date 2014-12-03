<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_NewsPanel" Codebehind="NewsPanel.ascx.cs" %>
<link href=<%= this.ResolveClientUrl("~/Styles/NewsPanel.css") %> rel="stylesheet" type="text/css" />
<script src='<%= this.ResolveClientUrl("~/Scripts/NewsPanel.js")%>' type="text/javascript"></script>
        <div class="NewsPanel_Main">
                    <div id="NewsPanel_newsTabs">
                    <a class="NewsPanel_empty_news"><%= Title %></a><a class="sel" href="#News1"><%= ArticleCatName1 %></a><a href="#News2"><%= ArticleCatName2 %></a>
            </div>
            <div id="NewsPanel_tabPnl">

                <div id="News1" >
                             <div class="NewsPanel_ImgContainer">
                             <a id = "NewsPanel_ImgContainerLink1" href ="#" runat="server">
                                 <asp:Image ID="NewsPanel_ImgContainer1" runat="server" />
                             <div>
                                 <asp:Label ID="lbl_mainImage1" runat="server" Text=""></asp:Label></div>
                                 </a>
                             </div>   
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                  <ItemTemplate>
                    <li>
                      <a id = "a_link" href="#" runat="server">
                          <asp:Label ID="lbl_title" runat="server" Text="Label"></asp:Label> </a>
                    </li>
                  </ItemTemplate>
                </asp:Repeater>

              </div>
              <div id="News2" style="display:none;">
                                           <div class="NewsPanel_ImgContainer">
                             <a id = "NewsPanel_ImgContainerLink2" href ="#" runat="server">
                                 <asp:Image ID="NewsPanel_ImgContainer2" runat="server" />
                             <div>
                                 <asp:Label ID="lbl_mainImage2" runat="server" Text=""></asp:Label></div>
                                 </a>
                             </div>   
                <asp:Repeater ID="Repeater2" runat="server" onitemdatabound="Repeater2_ItemDataBound">
                  <ItemTemplate>
                    <li>
                          <a id = "a_link2" href="#" runat="server">
                          <asp:Label ID="lbl_title2" runat="server" Text="Label"></asp:Label> </a>
                    </li>
                  </ItemTemplate>
                </asp:Repeater>

              </div>
            </div>
        </div>
        <div class="secClr"></div>