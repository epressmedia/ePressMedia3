<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleEntry.aspx.cs" Inherits="ePressMedia.Article.ArticleEntryTerminated" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js"  type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery-ui.css">
    <script src="../Scripts/tag-it.js" type="text/javascript"></script>
    <link href="../Styles/Tagit.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All"
        EnableRoundedCorners="false" />

    </form>
</body>
</html>
