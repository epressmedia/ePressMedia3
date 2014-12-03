<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_VideoAdBoxSmall" Codebehind="VideoAdBoxSmall.ascx.cs" %>
<link href=<%= this.ResolveClientUrl("~/Styles/VideoAdSmall.css") %> rel="stylesheet" type="text/css" />
<script src='<%= this.ResolveClientUrl("~/Scripts/VideoAdSmall.js")%>' type="text/javascript"></script>
<asp:HiddenField ID="AdCount" runat="server" Value="10" />
<asp:HiddenField ID="VideoAdBoxSmall_itemPerPage" runat="server" Value="2" />

<div class="VideoAdBoxSmall_Header">
  <img id = "VideoAdBoxSmall_Img" src="img/video_icon_trans.png" /><asp:Label ID="VideoAdBoxSmall_lbl"
      runat="server" Text=""></asp:Label>
</div>
<div class="VideoAdBoxSmall_Contents">
    <div id="VideoAdBoxSmall_vidPnl">
      1
    </div>
</div>

<div class="VideoAdBoxSmall_List">

  <div id='VideoAdBoxSmall_carousel_container'> 
    <div id='VideoAdBoxSmall_navPrev'><img id="Img3" runat="server" src='~/img/prevad.png' /></div> 
      <div id='VideoAdBoxSmall_carousel_inner'> 
        <ul id='VideoAdBoxSmall_carousel_ul'>
          <asp:Repeater ID="ImgRepeater" runat="server">
            <ItemTemplate>
              <li>
                <a href='#<%# Eval("VideoId") %>'><img 
                  src='<%# "http://img.youtube.com/vi/" + Eval("VideoId") + "/default.jpg" %>' /></a>
                <div class="VideoAdBoxSmall_bizLbl"><%# Eval("Name") %></div>
              </li>            
            </ItemTemplate>
          </asp:Repeater>
        </ul> 
      </div> 
    <div id='VideoAdBoxSmall_navNext'><img id="Img4" runat="server" src='~/img/nextad.png' /></div> 
  </div> 

</div>



    <div id="VideoAdBoxSmall_descPnl">
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
              <img id="Img2" runat="server" src="~/img/detail.png" alt="" />
              <a href='~/Biz/Biz.aspx?id=<%# Eval("BizId") %>' target="_blank">상세보기</a>
            </div>
          </div>
        
        </ItemTemplate>
      
      </asp:Repeater>
    </div>
