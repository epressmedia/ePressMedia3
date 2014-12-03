<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullCalendarView.ascx.cs" Inherits="FullCalendarView" %>
<div class="FullCalendarView_Container">
  <script type="text/javascript">
      $(document).ready(function () {
          $('td.aday').hover(function () {
              $(this).css('backgroundColor', '#f0f0f0');
              //$(this).css('cursor', 'pointer');
          }, function () {
              $(this).css('backgroundColor', '#fff');
          });
      });
  
  </script>

  <style type="text/css">
  table.ttl { border: 1px solid #d0d0d0; font-size:10pt; font-weight:bold; height:48px; }
  td.aday, td.dimmed { padding:6px; border: 1px solid #d0d0d0; text-align:left; vertical-align:top; }
  th { border: 1px solid #d0d0d0; background-color:#f7f7f7; font-weight:bold; }
  .evtCal ul { margin-left:12px; }
  .evtCal li { margin-top:4px; margin-bottom:4px;line-height:12pt; list-style:disc; }
  .evtCal li a { font-size:8pt; }
  td.dimmed { color:#dbdbdb; font-size:8pt; font-weight:normal; }
  </style>


  <asp:Button ID="ModalTarget" runat="server" style="display:none" />

  <asp:Label ID="CurEvent" runat="server" Visible ="false"></asp:Label>

  <div id="eventView" runat="server" visible="false">
    <div class="thrdVw">
      <div class="ttl">
        <asp:Literal ID="EvtTitle" runat="server" />
      </div>
      <div class="inf">
        <span class="lbl">날짜: </span><asp:Literal ID="EvtDate" runat="server" /><span class="colSep"></span>
        <span class="lbl">시간: </span><asp:Literal ID="EvtTime" runat="server" /><span class="colSep"></span>
      </div>
      <div class="inf">
        <span class="lbl">장소: </span><asp:Literal ID="HeltAt" runat="server" />
        <asp:HyperLink ID="MapLink" runat="server" Target="_blank" />
      </div>
      <div class="inf">
        <span class="lbl">주최: </span><asp:Literal ID="EvtHost" runat="server" /><span class="colSep"></span>
        <span class="lbl">문의: </span><asp:Literal ID="Inquiry" runat="server" /><span class="colSep"></span>
      </div>
      <div class="inf">
        <span class="lbl">관련사이트: </span><asp:HyperLink ID="SiteLink" runat="server" Target="_blank" />
      </div>

      <div class="msg">
        <asp:Literal ID="Desc" runat="server" />
      </div>

    </div>
    <div class="cmdPnlR">
      <asp:Button ID="DelButton" runat="server" CssClass="boxLnk" Text="행사 삭제" />
      <asp:HyperLink ID="ModLink" runat="server" CssClass="boxLnk" Text="행사 수정" />
      <asp:LinkButton ID="ListLink" runat="server" CssClass="boxLnk" Text="달력 보기" 
        onclick="ListLink_Click" />
    </div>
    <div class="cntrPnl">&nbsp;</div>
  </div>

  <asp:Calendar ID="Calendar1" runat="server" Width="100%" 
    ondayrender="Calendar1_DayRender" Visible="true" 
    CssClass="evtCal" onselectionchanged="Calendar1_SelectionChanged" 
    SelectionMode="None" onvisiblemonthchanged="Calendar1_VisibleMonthChanged" 
    CellPadding="0">
    <DayHeaderStyle Height="30px" />
    <DayStyle CssClass="aday" />
    <OtherMonthDayStyle CssClass="dimmed" />
    <TitleStyle BorderColor="#D0D0D0" BorderStyle="Solid" BorderWidth="1px" 
      Font-Bold="True" Font-Size="12pt" ForeColor="#333333" Height="40px" />
    <TodayDayStyle Font-Bold="True" />
  </asp:Calendar>
  <div class="secClr"></div>
  <div class="cmdPnlR">
    <asp:HyperLink ID="PostLink" runat="server" CssClass="boxLnk" Text="행사 등록" Visible="true" />
    &nbsp;
  </div>
  <div class="secClr"></div>

    <epm:EntryPopup ID="DeletePopup" runat="server" />
  </div>