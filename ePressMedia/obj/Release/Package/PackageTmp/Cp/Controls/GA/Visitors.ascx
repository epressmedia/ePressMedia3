<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Visitors.ascx.cs" Inherits="ePressMedia.Cp.Controls.GA.Visitors" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>
<div runat="server" id="ga_visitors" style="margin:10px; padding:10px; border:1px solid #EEE;" visible="false">
<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
<div >

    <telerik:RadNumericTextBox ID="txt_number" Runat="server" Value = "10" DataType="System.Int">
<NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
    </telerik:RadNumericTextBox>

    <telerik:RadComboBox ID="cbo_type" Runat="server"  
        EmptyMessage="Select Demension Type" 
        onselectedindexchanged="RadComboBox1_SelectedIndexChanged">
        <Items>
            <telerik:RadComboBoxItem runat="server" Selected="True" Text="Day" Value="ga:date" />
            <telerik:RadComboBoxItem runat="server" Text="Year" Value="ga:year" />
            <telerik:RadComboBoxItem runat="server" Text="Month" Value="ga:year,ga:month" />
            <telerik:RadComboBoxItem runat="server" Text="Week" Value="ga:week" />
        </Items>
    </telerik:RadComboBox>
    <asp:Button ID="btn_refresh" runat="server" Text="refresh" onclick="btn_refresh_Click"/>
    

</div>
<div>
<telerik:RadHtmlChart ID="RadHtmlChart1" runat="server"  >
     <PlotArea>
        <Series>
            <telerik:LineSeries DataFieldY="Value" >
                                            <Appearance>
                                    <FillStyle BackgroundColor="#5ab7de" />
                                </Appearance>
                <LabelsAppearance Visible="false" />
                <TooltipsAppearance DataFormatString="{0}" />
                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" />
                
            </telerik:LineSeries>
        </Series>
        <XAxis DataLabelsField="Name">
        <MajorGridLines Color="#EFEFEF" Width="1" />
                            <MinorGridLines Color="#F7F7F7" Width="1" />
        </XAxis>
        <YAxis>
        <MajorGridLines Color="#EFEFEF" Width="1" />
                            <MinorGridLines Color="#F7F7F7" Width="1" />
        </YAxis>
    </PlotArea>
    <Legend>
        <Appearance Visible="false" />
    </Legend>
        <ChartTitle Text="Vistors in last 7 days">
    </ChartTitle>
    </telerik:RadHtmlChart>

    </div>
    </div>