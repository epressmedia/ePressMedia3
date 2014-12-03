<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthViewCalendar.ascx.cs" Inherits="ePressMedia.Controls.Calendar.MonthViewCalendar" %>
  <asp:Calendar ID="Calendar1" runat="server" Width="100%" 
    ondayrender="Calendar1_DayRender" Visible="true" 
    CssClass="evtCal" 
    SelectionMode="None" 
    CellPadding="0">
    <DayHeaderStyle Height="30px" />
    <DayStyle CssClass="aday" />
    <OtherMonthDayStyle CssClass="dimmed" />
    <TitleStyle BorderColor="#D0D0D0" BorderStyle="Solid" BorderWidth="1px" 
      Font-Bold="True" Font-Size="12pt" ForeColor="#333333" />
    <TodayDayStyle Font-Bold="True" />
  </asp:Calendar>