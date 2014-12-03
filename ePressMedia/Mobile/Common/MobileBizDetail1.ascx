<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mobile_Common_MobileBizDetail1" Codebehind="MobileBizDetail1.ascx.cs" %>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="Scripts/knn_mobile_map_helper.js"></script>
    <form id = form1 runat="server">
       <asp:Panel ID="BizViewPanel" runat="server" Visible="false">
          <asp:HiddenField ID="VidIdField" runat="server" />
          <ul data-role="listview" data-inset="true"  >
<li data-role="list-divider"><span id = "bizTitle"><asp:Literal ID="BizTitle" runat="server" /></span></li>
           <li>
            <asp:Literal ID="ShortDesc" runat="server" /></li>
            </ul>
          <a id="tel_link" runat="server" data-role="button" rel="external" >Tel : <asp:Literal ID="Phone1" runat="server" /></a>
          <a id="fax_link" runat="server" data-role="button" rel="external" >Fax : <asp:Literal ID="Fax" runat="server" /></a>
           <a id = "homepage_link" runat="server" data-role="button" rel="external">HomePage :  <asp:Literal ID="HomePage" runat="server" /></a>

               <ul id="addressPanel" data-role="listview" data-inset="true">
               <li data-role="list-divider">주소</li>
                  <li><a tag = "gmap"><asp:Literal ID="Address" runat="server"/></a></li>
                  <li><span class="bizLabel">현재위치으로부터 거리: </span>  <asp:Label ID="distance" runat=server></asp:Label></li>
                </ul>
          
        <%--  <a id="getDirection" href="#" data-role="button" data-icon="search">Get directions</a>--%>

          <div style="display:none;">
              <p><span class="infoLbl">영업시간: </span><asp:Literal ID="BizHour" runat="server" /></p>
              <p><span class="infoLbl">주차: </span><asp:Literal ID="Parking" runat="server" /></p>
              <p><span class="infoLbl">결제수단: </span><asp:Literal ID="Payments" runat="server" /></p>
</div>

          
          <asp:HiddenField ID="zipcode" runat="server" />
          <asp:HiddenField ID="lat1" runat="server" />
          <asp:HiddenField ID="lon1" runat="server" />
          <asp:HiddenField ID="lat2" runat="server" />
          <asp:HiddenField ID="lon2" runat="server" />
          <div class="bizDescPnl">
            <p class="descPnl">
              <asp:Label ID="Desc" runat="server" />
            </p>
            
<%--            <asp:Panel ID="VideoPanel" runat="server" HorizontalAlign="Center" CssClass="vidPnl">
              <object width="430" height="320">
                <param name="movie" value="http://www.youtube.com/v/<% =VideoId %>"></param>
                <param name="allowFullScreen" value="false"></param>
                <param name="allowscriptaccess" value="always"></param>
                <embed src='http://www.youtube.com/v/<% =VideoId %>' type="application/x-shockwave-flash" 
                    allowscriptaccess="always" allowfullscreen="false" width="430" height="320">
                </embed>
              </object>
            </asp:Panel>        --%>

<%--            <asp:Panel ID="GalPanel" runat="server" CssClass="galPnl" Visible="false">
              <iframe runat="server" id="GalFrame" width="440" height="380" 
                scrolling="no" frameborder="0" />
            </asp:Panel>--%>

<%--            <asp:Panel ID="MapPanel" runat="server" CssClass="mapPnl" Visible="false">
              <iframe runat="server" id="MapFrame" width="430" height="324" 
                scrolling="no" frameborder="0" />
            </asp:Panel>--%>
          </div>
          

        </asp:Panel>
        </form>