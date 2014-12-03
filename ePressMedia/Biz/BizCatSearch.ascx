<%@ Control Language="C#" AutoEventWireup="true" Inherits="Biz_BizCatSearch" Codebehind="BizCatSearch.ascx.cs" %>

    <asp:UpdatePanel runat="server" ID="CatMenuUpdatePanel">
      <ContentTemplate>
 <div class="rankBox">
    <p class="rankName">카테고리 검색</p>
    <%--<img id="Img1" runat="server" src="~/Img/bizMenu.jpg" alt="" />--%>

        <asp:HiddenField ID="CurLetter" runat="server" Value="" />
        <div class="letterMenu">
         <asp:LinkButton ID="Lb1" runat="server" Text="ㄱ" CommandName="가" CommandArgument="나" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb2" runat="server" Text="ㄴ" CommandName="나" CommandArgument="다" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb3" runat="server" Text="ㄷ" CommandName="다" CommandArgument="라" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb4" runat="server" Text="ㄹ" CommandName="라" CommandArgument="마" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb5" runat="server" Text="ㅁ" CommandName="마" CommandArgument="바" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb6" runat="server" Text="ㅂ" CommandName="바" CommandArgument="사" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb7" runat="server" Text="ㅅ" CommandName="사" CommandArgument="아" OnClick="CatLinkClick" />
         <br />
         <asp:LinkButton ID="Lb8" runat="server" Text="ㅇ" CommandName="아" CommandArgument="자" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb9" runat="server" Text="ㅈ" CommandName="자" CommandArgument="차" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb10" runat="server" Text="ㅊ" CommandName="차" CommandArgument="카" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb11" runat="server" Text="ㅋ" CommandName="카" CommandArgument="타" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb12" runat="server" Text="ㅌ" CommandName="타" CommandArgument="파" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb13" runat="server" Text="ㅍ" CommandName="파" CommandArgument="하" OnClick="CatLinkClick" />
         <asp:LinkButton ID="Lb14" runat="server" Text="ㅎ" CommandName="하" CommandArgument="" OnClick="CatLinkClick" />
        </div>
        <div class="catSearchDetail">
          <ul>
            <asp:Repeater ID="CatMenuRepeater" runat="server" onitemcommand="CatMenuRepeater_ItemCommand">
            <HeaderTemplate></HeaderTemplate>
              <ItemTemplate>
                <li>
                 <asp:HyperLink ID="CatLink" runat="server" NavigateUrl='<%# Eval("CatId", "Biz.aspx?cat={0}") %>' 
                        Text='<%# Eval("CatName") %>' />
                      <%# Eval("Count", "({0})") %>
                </li>              
              </ItemTemplate>
            <FooterTemplate></FooterTemplate>
            </asp:Repeater>
          </ul>
        </div>
        
        

  
  </div>
   <div class="secClr">
   </div>
         </ContentTemplate>
    </asp:UpdatePanel>
