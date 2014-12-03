<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Weather.ascx.cs" Inherits="Weather" %>
<style>
 .w_cc_temp {
font-size: 150%;
float: left;
padding: 0 5px;
padding-right: 8px;
font-weight: bold;
}
.w_ccis {
padding: 1px;
float: left;
}
.w_cci {

padding: 1px;

}
.w_cc_text {
overflow: hidden;
}
.w_fcs {

overflow: hidden;
padding: 2px 0 0;
}
.w_fc {
text-align: center;
padding: 3px;
float: left;
margin-right: 2px;
background: whiteSmoke;
}
.w_fci {
padding: 1px;

}

</style>

<div id = "weatherSection" runat="server" class="Weather_Container">
       <div class="Weather_Header">
        
            <asp:Label ID="lbl_header" runat="server"></asp:Label>
        </div>
        <div class="Weather_Contents">
    <div class="modlabel" style="padding-top: 2px; font-weight:bold">
        <asp:Label id="lbl_cityname" runat="server"></asp:Label></div>
    <div class="w_box">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td>
                        <div class="w_ccs">
                            <div class="w_ccis">
                                <img  class="w_cci" id="current_icon" runat="server" src=""><br/>
                                    <asp:Label ID="lbl_current_temp" CssClass="w_cc_temp" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="w_cc_temp" id="w_13_c0_temp">
                                </div>
                            <div class="w_cc_text" style="float: " id="w_13_c0_text">
                                <asp:Label ID="lbl_current_weather" runat="server" Text=""></asp:Label><br/>
                                <asp:Label ID="lbl_curent_wind" runat="server" Text=""></asp:Label><br/>
                               <asp:Label ID="lbl_current_humidity" runat="server" Text=""></asp:Label></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="w_fcs" style="width: 100%" id="w_13_c0_fcs">

                            <asp:Repeater ID="Weather_Repeater" runat="server">
        <ItemTemplate>


            <div class="w_fc" title='<%# Eval("Condition") %>'>
                                <%# Eval("DayLabel")%><br>
                                <div class="w_fcid">
                                    <img class="w_fci" src='<%# Eval("IconUrl") %>'
                                        ></div>
                                <nobr> <%# Eval("Low") %>° | <%# Eval("High") %>°</nobr>
                            </div>
        </ItemTemplate>
    </asp:Repeater>

                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
</div>