<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpCommingEvent.ascx.cs" Inherits="ePressMedia.Controls.Calendar.UpCommingEvent" %>
    <div class="UpCommingEvent_container">
        <div class="UpCommingEvent_Header">
        <a href="<%= MoreLink %>">
        <asp:Label ID="lbl_title" runat="server" Text=""></asp:Label>
        <img id = "img_title" runat="server" src="" alt="" visible="false"/>
        </a>
    </div>
<div class="UpCommingEvent_itemcontainer">

    <ul class="UpCommingEvent_contents">
    <asp:Label ID = "lbl_empty" CssClass="MUpCommingEvent_Empty" Text = "No Content" runat="server" Visible="false"></asp:Label>
        <asp:Repeater ID="UpCommingEvent_Repeater" runat="server" 
            onitemdatabound="UpCommingEvent_Repeater_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:Label ID="number_count" runat="server" Text=""></asp:Label>
                    <a id="cal_link" runat="server" href='<%# String.Format("~/Calendar/MonthlyCalendar.aspx?p={0}&eid={1}",Eval("CalID"),Eval("EventID")) %>' title='<%# Eval("Description") %>'>
                         &#8226; <asp:Label ID="lbl_eventdate" runat="server" class="UpCommingEvent_eventdate" Text='<%# Eval("StartDate","{0:[MM/dd]}") %>'></asp:Label>
                        <asp:Label ID="lbl_contents" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
</div>