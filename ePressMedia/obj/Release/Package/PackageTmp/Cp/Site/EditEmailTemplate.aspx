<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEmailTemplate.aspx.cs" Inherits="ePressMedia.Cp.Site.EditEmailTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Edit Email Template</title>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
    <div>
    <h1>
        <asp:Label ID="lbl_email_template_title" runat="server" Text="Label"></asp:Label></h1>
    <div>
    <div>Email Subject</div>
    <div>
        <asp:TextBox ID="txt_subject" runat="server"></asp:TextBox></div>
    </div>
    <div>
    <div>Email Body</div>
    <div>

                        <telerik:RadEditor ID="html_editor_text" runat="server" Width="100%" Height="500px" AllowScripts="True">
                    </telerik:RadEditor>
                    </div>
    </div>
    </div>
    <div style="float:right">
    <asp:Button ID="btn_Save" runat="server" Text="Save" onclick="btn_Save_Click" />
    <asp:Button ID="btn_Close" runat="server" Text="Close"  OnClientClick="closeWin(); return false;"/>
            <script type="text/javascript">
                function closeWin() {
                    GetRadWindow().close();
                }
                function GetRadWindow() {
                    var oWindow = null; if (window.radWindow)
                        oWindow = window.radWindow; else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; return oWindow;
                }
        </script>
    </div>
    </form>
</body>
</html>
