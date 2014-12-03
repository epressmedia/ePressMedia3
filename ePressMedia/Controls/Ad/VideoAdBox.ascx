<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_VideoAdBox" Codebehind="VideoAdBox.ascx.cs" %>
<script src="Scripts/VideoAd.js" type="text/javascript"></script>
<asp:HiddenField ID="AdCount" runat="server" Value="10" />
<div>
  <img runat="server" src="~/img/vidad.png" alt="" />
</div>
<div id="vidAd">
  <div>
    <div id="vidPnl">
      1
    </div>
    <div id="descPnl">
      <asp:Repeater ID="DescRepeater" runat="server">
        <ItemTemplate>

          <div id='<%# Eval("VideoId") %>'>
            <div class="descSec">
              <h1><%# Eval("Name") %></h1>
              <h2><%# Eval("EngName") %></h2>
            </div>
            <div class="descSec">
              <%# Eval("ShortDesc") %><br />
              TEL: <%# Eval("BizPhone1") %><br />
              <%# Eval("Address") + ", " %><%# Eval("AreaName") + ", " %>
              <%# Eval("Province") + " " %><%# Eval("PostalCode") %>
            </div>
            <div class="bizLnk">
              <img runat="server" src="~/img/detail.png" alt="" />
              <a href='~/Biz/Biz.aspx?id=<%# Eval("BizId") %>' target="_blank">상세보기</a>
            </div>
          </div>
        
        </ItemTemplate>
      
      </asp:Repeater>
    </div>
  </div>
  
  <div id='carousel_container'> 
    <div id='navPrev'><img runat="server" src='~/img/prevad.png' /></div> 
      <div id='carousel_inner'> 
        <ul id='carousel_ul'>
          <asp:Repeater ID="ImgRepeater" runat="server">
            <ItemTemplate>
              <li>
                <a href='#<%# Eval("VideoId") %>'><img width="100" height="64"
                  src='<%# "http://img.youtube.com/vi/" + Eval("VideoId") + "/default.jpg" %>' /></a>
                <div class="bizLbl"><%# Eval("Name") %></div>
              </li>            
            </ItemTemplate>
          </asp:Repeater>
        </ul> 
      </div> 
    <div id='navNext'><img runat="server" src='~/img/nextad.png' /></div> 
  </div> 
</div>