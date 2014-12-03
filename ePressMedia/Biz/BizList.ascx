    <%@ Control Language="C#" AutoEventWireup="true" Inherits="Biz_BizList" Codebehind="BizList.ascx.cs" %>
<%@ Register src="../Controls/Pager/FitPager.ascx" tagname="FitPager" tagprefix="uc1" %>
<%@ Register src="../Controls/Ad/VideoAdBox.ascx" tagname="VideoAdBox" tagprefix="uc2" %>





<%--          <div runat="server" id="MainFlash">
            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" width="632" height="138"> 
                <param name="movie" value="Img/main.swf" /> 
                <param name="quality" value="high" />
                <embed src="Img/main.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="632" height="138">
                </embed>
            </object>
            <%--<asp:Image ID="MainImg" runat="server" ImageUrl="Img/main.png" alt="메인이미지" />
          </div>--%>

          <div class="pgTtl">
            <asp:Literal ID="PageTitle" runat="server" />
            <asp:HiddenField ID="SearchVal" runat="server" />
            <asp:HiddenField ID="CurCat" runat="server" />
            <asp:HiddenField ID="CurState" runat="server" />
            <asp:HiddenField ID="CurArea" runat="server" />
          </div>
          
          <asp:Panel ID="BizViewPanel" runat="server" CssClass="bizPnl" Visible="false">
            <div class="bizDesc">
              <span class="bizTtl2"><asp:Literal ID="BizTitle" runat="server" /></span>&nbsp;&nbsp;
              <asp:Label ID="EngBizTitle" runat="server" CssClass="bizTtlEn" />&nbsp;
              <img id="movIcon" runat="server" src="../img/movie_icon.gif" alt="" visible="false" title="동영상이 있습니다" />
              <img id="picIcon" runat="server" src="../img/pic_icon.gif" alt="" visible="false" title="사진이 있습니다" />
            </div>
            <div class="bizDesc" >
              <asp:Literal ID="ShortDesc" runat="server" />&nbsp;
            </div>
            <ul class="detail">
              <li><span class="label">TEL</span>: <asp:Literal ID="Phone" runat="server" /></li>
              <li><span class="label">FAX</span>: <asp:Literal ID="Fax" runat="server" /></li>
              <li><span class="label">ADDRESS</span>: <asp:Literal ID="Address" runat="server" /></li>
              <li><span class="label">OPEN</span>: <asp:Literal ID="BizHour" runat="server" /></li>
              <li><span class="label">WEB SITE</span>: <asp:HyperLink ID="HomePage" runat="server" Target="_blank" /></li>
              <li><span class="label">E-mail</span>: <asp:Literal ID="Email" runat="server" /></li>
              <li><span class="label">CATEGORY</span>: <asp:Literal ID="CatName" runat="server" /></li>
              
            </ul>
            <div style="margin:5px 0 5px 0">
            <asp:Literal ID="BizDescription" runat="server" />
            </div>
            <div class="tabs">
              <span class="tab">지도보기</span>
              <span class="tab">사진보기</span>
              <span class="tab">동영상보기</span>
<%--              <a id="mapLink" href="javascript:onMapViewClick();">지도보기</a>
              |
              <a id="picLink" href="javascript:onPicViewClick();">사진보기</a>
              |
              <a id="vidLink" href="javascript:onVidViewClick();">동영상보기</a>--%>
            </div>
            <div id="mapView" class="multiPnl">
              <asp:Literal ID="NoMap" runat="server" Text="주소 정보가 존재하지 않습니다" />
              <iframe runat="server" id="MapFrame" width="100%" height="360" 
                scrolling="no" frameborder="0" />
            </div>
            <div id="picView" class="multiPnl">
              <asp:Literal ID="NoPic" runat="server" Text="등록된 이미지가 없습니다" />
              <iframe runat="server" id="PicFrame" width="100%" height="360" 
                scrolling="no" frameborder="0" />
            </div>
            <div id="vidView" class="multiPnl" style="border:2px solid #dbdbdb;">
              <asp:Literal ID="NoVid" runat="server" Text="등록된 동영상이 없습니다" />
              <div id="ytubeView" runat="server" style="margin-bottom:-3px;">
                <object width="100%" height="360">
                  <param name="movie" value="http://www.youtube.com/v/<% =VideoId %>" />
                  <param name="allowFullScreen" value="false" />
                  <param name="allowscriptaccess" value="always" />
                  <embed src='http://www.youtube.com/v/<% =VideoId %>' type="application/x-shockwave-flash" 
                      allowscriptaccess="always" allowfullscreen="false" width="100%" height="360">
                  </embed>
                </object>
              </div>
            </div>
            <div class="tabs">
              <asp:Button ID="EditLink" runat="server" Text="정보수정" CssClass="flat" OnClick="EditLink_Click" />
              <span class="inptSep"></span>
              <asp:Button ID="ListLink" runat="server" Text="목록보기" CssClass="flat" OnClick="ListLink_Click" />
            </div>
          </asp:Panel>

          <asp:Repeater id="AdOwnerBizRepeater" runat="server" 
        onitemdatabound="BizRepeater_ItemDataBound">
            <ItemTemplate>
              <div class="biz">
                <div class="bizImg2">
               <%--   <img src="../img/bbad.png" alt="" style="margin-bottom:2px;" />--%>
                  <div>
                    <asp:Image ID="BizImg" runat="server" AlternateText="" class="thumb"
                      ImageUrl='' style="border:3px solid #dbdbdb;" />
                  </div>
                </div>
                <div class="bizInfo">
                  <div class="bizTtl">
                    <asp:HyperLink ID="BizLink1" runat="server" CssClass="lnkBbAd"><%# Eval("Name") %></asp:HyperLink>&nbsp;
                    <asp:HyperLink ID="BizLink2" runat="server" CssClass="lnkEng"><%# Eval("EngName") %></asp:HyperLink>&nbsp;
                    <asp:Image ID="MovIcon" runat="server" src="../img/movie_icon.gif" ToolTip="동영상이 있습니다" />
                    <asp:Image ID="PicIcon" runat="server" src="../img/pic_icon.gif" ToolTip="사진이 있습니다" />
                  </div>
                  <ul class="detail">
                    <li><span class="shrtDsc"><%# Eval("ShortDesc") %></span>&nbsp;</li>
                    <li><span class="label">TEL</span>: <%# Eval("BizPhone1") %></li>
                    <li><span class="label">ADDRESS</span>: <%# Eval("Address") + ", " %><%# Eval("AreaName") + ", " %><%# Eval("Province") + " " %><%# Eval("PostalCode") %></li>
                    <li><span class="label">WEB SITE</span>: <a href='<%# Eval("Url", "http://{0}") %>' target="_blank"><%# Eval("Url") %></a></li>
                    <li><span class="label">CATEGORY</span>: <%# Eval("CatName") %></li>
                  </ul>
                </div>
              </div>
            </ItemTemplate>
          </asp:Repeater>

          <asp:Repeater id="BizRepeater" runat="server" 
            onitemdatabound="BizRepeater_ItemDataBound">
            <ItemTemplate>
              <div class="<%# Container.ItemIndex % 2 == 0 ? "biz" : "bizAlt" %>">
                <div class="bizImg">
                  <asp:Image ID="BizImg" runat="server" AlternateText="" class="thumb"
                    ImageUrl='' style="border:3px solid #dbdbdb;" />
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

          <br />
  <div class="centerPnl"><a href="/Biz/PostBiz.aspx" class="boxLink" visible="false">업소 등록</a></div>
              <%--      <div runat="server" id="VideoAdPnl">
            <uc2:VideoAdBox ID="VideoAdBox1" runat="server" />
          </div>--%>

