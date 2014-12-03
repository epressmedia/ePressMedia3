<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BizDetail.ascx.cs" Inherits="ePressMedia.Controls.Biz.DetailView.BizDetail" %>
  <script src="../Scripts/Biz.js" type="text/javascript"></script>  
  <script src="../Scripts/VideoAd.js" type="text/javascript"></script>
          <asp:Panel ID="BizViewPanel" runat="server" CssClass="bizPnl" Visible="false">
            <div class="bizName">
              <span class="bizTtl2"><asp:Literal ID="BizTitle" runat="server" /></span>&nbsp;&nbsp;
              <asp:Label ID="EngBizTitle" runat="server" CssClass="bizTtlEn" />&nbsp;
              <img id="movIcon" runat="server" src="/img/movie_icon.gif" alt="" visible="false" title="동영상이 있습니다" />
              <img id="picIcon" runat="server" src="/img/pic_icon.gif" alt="" visible="false" title="사진이 있습니다" />
            </div>
            <div class="bizDesc" >
              <asp:Literal ID="ShortDesc" runat="server" />&nbsp;
            </div>
            <ul class="bizDetail">
            
            <li><span class="label">ADDRESS</span>: <asp:Literal ID="Address" runat="server" /></li>
              <li><span class="label">TEL</span>: <asp:Literal ID="Phone" runat="server" /></li>
              <li><span class="label">FAX</span>: <asp:Literal ID="Fax" runat="server" /></li>
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
            </div>
            <div id="mapView" class="multiPnl">
              <asp:Literal ID="NoMap" runat="server" Text="주소 정보가 존재하지 않습니다" />
              <iframe  runat="server" id="MapFrame" width="100%" height="360" 
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