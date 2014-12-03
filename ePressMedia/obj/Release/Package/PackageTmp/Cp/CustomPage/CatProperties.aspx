<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatProperties.aspx.cs"
    Inherits="ePressMedia.Cp.Pages.CatProperties" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CP/Style/Cp.css?v=2" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            function CloseAndRebind(args) {

                GetRadWindow().close();
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        Custom Page Properties - <asp:Literal ID="PageName" runat="server" /></h1>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

  <div class="gen_field" >
  <div>

  <asp:Label Id="lbl_page_title" runat="server" Text="Page Title" CssClass="title"></asp:Label>
        <asp:TextBox ID="txt_page_title" runat="server" CssClass="mid_field"></asp:TextBox>
      </div>
      <div>
      <asp:Label Id="meta_description" runat="server" Text="Page Metatag Description"  CssClass="title"></asp:Label>
        <asp:TextBox ID="txt_page_meta_descr" runat="server" TextMode="MultiLine" CssClass="mid_field"></asp:TextBox>
      </div>
      </div>
      <div>
      <div style="clear:both"></div>
      <div style="float:right">
      <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_clicked" />
      <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_clicked" />
      </div>
      </div>
  </ContentTemplate>
  </asp:UpdatePanel>
    </form>
</body>
</html>
