<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BizList.ascx.cs" Inherits="ePressMedia.Controls.Biz.ListView.BizList" %>
<%@ Register src="/Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>


       <div id="srchPnl" class="bizSearchBox BizList_SearchBox">
       <p class="bizSearchName">업소 검색</p>
          <div class="srchSub1">
            업소명<asp:TextBox ID="SearchName" runat="server" CssClass="srchBox" />
          </div>
          
          <div class="srchSub2">
            업종<asp:DropDownList ID="BizCatList" runat="server" CssClass="catBox" />
          </div>
          <div class="divClear"></div>
      
        
          <div class="srchSub3">
            주(State/Province) 
              <asp:DropDownList ID="cbo_province" runat="server">
              </asp:DropDownList>


            </div>
           <div class="srchSub4">
            도시(City)  
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            <div class="divClear"></div>
            <div class="srchButton">
            <asp:Button ID="SearchButton" runat="server" Text="검색" CausesValidation="false"
              onclick="SearchButton_Click" class="srchButton" />
              </div>
          </div>



          <div class="divClear"></div>
        
                      
        </div>
        <div class="BizList_Listing">
                  <div class="pgTtl">
            <asp:Literal ID="PageTitle" runat="server" />
            
          </div>
         <asp:Repeater id="PremiumRepeater" runat="server" 
            onitemdatabound="BizRepeater_ItemDataBound">
            <ItemTemplate>
              <div ID="biz_pnl" runat="server">
                <div class="bizImg">
                <div id = premium_ad_box runat="server" class="premium_ad_box" visible = "false"></div>
                  <asp:Image ID="BizImg" runat="server" AlternateText="" class="thumb" ImageUrl="" style="border:3px solid #dbdbdb;" />
                </div>
                <div class="bizInfo">
                  <div class="bizTtl">
                    <asp:HyperLink ID="BizLink1" runat="server" CssClass="lnkKor"><%# Eval("PrimaryName") %></asp:HyperLink>&nbsp;
                    <asp:HyperLink ID="BizLink2" runat="server" CssClass="lnkEng"><%# Eval("SecondaryName") %></asp:HyperLink>&nbsp;
                    <asp:Image ID="MovIcon" runat="server" src="../img/movie_icon.gif" ToolTip="동영상이 있습니다" />
                    <asp:Image ID="PicIcon" runat="server" src="../img/pic_icon.gif" ToolTip="사진이 있습니다" />
                  </div>
                  <ul class="detail">
                    <li><span class="label">TEL</span>: <%# Eval("Phone1") %></li>
                    <li><span class="label">ADDRESS</span>: <%# Eval("Address") + ", " %><%# Eval("City") + ", " %><%# Eval("State") + " " %><%# Eval("ZipCode") %></li>
                    <li><span class="label">WEB SITE</span>: <a href='<%# Eval("Website", "http://{0}") %>' target="_blank"><%# Eval("Website") %></a></li>
                    <li><span class="label">CATEGORY</span>: <%# Eval("BusienssCategory.CategoryName")%></li>
                  </ul>
                </div>
              </div>
            </ItemTemplate>
          </asp:Repeater>

          <asp:Repeater id="BizRepeater" runat="server" 
            onitemdatabound="BizRepeater_ItemDataBound">
            <ItemTemplate>
              <div ID="biz_pnl" runat="server">
                <div class="bizImg">
                <div id = premium_ad_box runat="server" class="premium_ad_box" visible = "false"></div>
                  <asp:Image ID="BizImg" runat="server" AlternateText="" class="thumb" ImageUrl="" style="border:3px solid #dbdbdb;" />
                </div>
                <div class="bizInfo">
                  <div class="bizTtl">
                    <asp:HyperLink ID="BizLink1" runat="server" CssClass="lnkKor"><%# Eval("PrimaryName") %></asp:HyperLink>&nbsp;
                    <asp:HyperLink ID="BizLink2" runat="server" CssClass="lnkEng"><%# Eval("SecondaryName") %></asp:HyperLink>&nbsp;
                    <asp:Image ID="MovIcon" runat="server" src="../img/movie_icon.gif" ToolTip="동영상이 있습니다" />
                    <asp:Image ID="PicIcon" runat="server" src="../img/pic_icon.gif" ToolTip="사진이 있습니다" />
                  </div>
                  <ul class="detail">
                    <li><span class="label">TEL</span>: <%# Eval("Phone1") %></li>
                    <li><span class="label">ADDRESS</span>: <%# Eval("Address") + ", " %><%# Eval("City") + ", " %><%# Eval("State") + " " %><%# Eval("ZipCode") %></li>
                    <li><span class="label">WEB SITE</span>: <a href='<%# Eval("Website", "http://{0}") %>' target="_blank"><%# Eval("Website") %></a></li>
                    <li><span class="label">CATEGORY</span>: <%# Eval("BusienssCategory.CategoryName")%></li>
                  </ul>
                </div>
              </div>
            </ItemTemplate>
          </asp:Repeater>
          <div class="cntrPnl">
            <uc1:FitPager ID="FitPager1" runat="server" RowsPerPage="10" OnPageNumberChanged="PageNumber_Changed" />
          </div>
          </div>

  <div class="centerPnl"><a href="/Biz/PostBiz.aspx" class="boxLink" visible="false">업소 등록</a></div>

  <%--          </telerik:RadAjaxPanel>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
</telerik:RadAjaxLoadingPanel>--%>
