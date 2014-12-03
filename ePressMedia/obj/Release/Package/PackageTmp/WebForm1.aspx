<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ePressMedia.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
   
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="label1" runat="server"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
    </div>
    <asp:Button ID="aa" runat="server" Text="HTLM" class="uni" />
    <asp:Button ID="Button1" runat="server" Text="WIDGET" class="uni" />
    <asp:Button ID="Button2" runat="server" Text="IMAGE" class="uni" />
    <style>
        .uni
        {
           background: #BEBEBE;
color: #DFDFDF;
border: 1px solid #FFF;
width: 140px;
height: 120px;
text-align: left;
font-size: 26px;
font-weight: bold;
line-height: 200px;
border-radius: 8px;
font-family: Verdana;
letter-spacing: -2px;}
        </style>
    </form>
</body>
</html>

