<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_PhotoNewsPanel" Codebehind="PhotoNewsPanel.ascx.cs" %>
<link href=<%= this.ResolveClientUrl("~/Styles/PhotoNewsPanel.css") %> rel="stylesheet" type="text/css" />
<script src='<%= this.ResolveClientUrl("~/Scripts/PhotoNewsPanel.js")%>' type="text/javascript"></script>
        <div class="PhotoNewsPanel_Main">
                    <div id="PhotoNewsPanel_newsTabs">
                    <a class="PhotoNewsPanel_empty_news"><%= Title %></a><a class="sel" href="#PhotoNews1"><%= ArticleCatName1 %></a><a href="#PhotoNews2"><%= ArticleCatName2 %></a>
            </div>
            <div id="PhotoNewsPanel_tabPnl">

                <div id="PhotoNews1" >
                             <div class="PhotoNewsPanel_ImgContainer">
                             <a id = "PhotoNewsPanel_ImgContainerLink1" href ="#" runat="server">
                                 <asp:Image ID="PhotoNewsPanel_ImgContainer1" runat="server" />
                             
                                 <asp:Label ID="lbl_mainImage1" runat="server" Text=""></asp:Label>
                                 <asp:Label ID="lbl_mainsubtitle1" runat="server" Text="" />
                                 </a>
                             </div>   
                <asp:Repeater ID="PhotoNewsPanel_Repeater1" runat="server" 
                                 onitemdatabound="PhotoNewsPanel_Repeater1_ItemDataBound" >
                <HeaderTemplate><div><ul id="PhotoNewsPanel_ImgDetailCont"></HeaderTemplate>
                  <ItemTemplate>
                   
                       <li class="PhotoNewsPanel_ImgItem" >
                          <a id ="PhotoNewsPanel_ImgItemLink1" class = "PhotoNewsPanel_ImgItemLink" href="#" runat="server">
                          <img id = "PhotoNewsPanel_ImgItemSrc1" class="PhotoNewsPanel_ImgItemSrc" runat="server" src=""/>
                          <asp:Label ID="lbl_title1" runat="server" Text="ABCDE asdasde asdas"></asp:Label>
                              
                          </a>
                    </li>
                  </ItemTemplate>
                  <FooterTemplate></ul></div></FooterTemplate>
                </asp:Repeater>

              </div>
              <div id="PhotoNews2" style="display:none;">
                                           <div class="PhotoNewsPanel_ImgContainer">
                             <a id = "PhotoNewsPanel_ImgContainerLink2" href ="#" runat="server">
                                 <asp:Image ID="PhotoNewsPanel_ImgContainer2" runat="server" />
                             
                                 <asp:Label ID="lbl_mainImage2" runat="server" Text=""></asp:Label>
                                 <asp:Label ID="lbl_mainsubtitle2" runat="server" Text=""></asp:Label>
                                 </a>
                             </div>   
                <asp:Repeater ID="PhotoNewsPanel_Repeater2" runat="server" 
                                               onitemdatabound="PhotoNewsPanel_Repeater2_ItemDataBound" >
                <HeaderTemplate><div>
                <ul id="PhotoNewsPanel_ImgDetailCont"></HeaderTemplate>
                  <ItemTemplate>
                    <li class="PhotoNewsPanel_ImgItem" >
                          <a id ="PhotoNewsPanel_ImgItemLink2" class = "PhotoNewsPanel_ImgItemLink" href="#" runat="server">
                          <img id = "PhotoNewsPanel_ImgItemSrc2" class="PhotoNewsPanel_ImgItemSrc" runat="server" src=""/>
                          <asp:Label ID="lbl_title2" runat="server" Text="ABCDE asdasde asdas"></asp:Label>
                              
                          </a>
                    </li>
                  </ItemTemplate>
                  <FooterTemplate></ul></div></FooterTemplate>
                </asp:Repeater>


              </div>
            </div>
        </div>

        <div class="secClr"></div>