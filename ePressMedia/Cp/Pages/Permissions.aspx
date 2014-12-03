<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permissions.aspx.cs" Inherits="ePressMedia.Cp.Pages.Permissions" %>

<%@ Register src="../Controls/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/CP/Style/Cp.css" rel="stylesheet" type="text/css" />
    
   
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:auto">
  <h1><asp:Literal ID="CatName" runat="server" /> Access Control</h1>
    <div>

  <uc1:AccessControl ID="AccessControl1" runat="server" />
  </div> 
  </div>     
</form>
</body>
</html>

