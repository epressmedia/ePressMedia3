<%@ Page Language="C#" AutoEventWireup="true" Inherits="Biz_GalleryView" Codebehind="GalleryView.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title></title>
  <script src="/Scripts/jquery-latest.js" type="text/javascript"></script>

  <script type="text/javascript">
      $(document).ready(
    function () {
        $('.picLnk').hover(
        function () {
            $('img').css('display', 'none');
            $('img:eq(' + $(this).index() + ')').css('display', 'inline');
        },
        function () {
        }
      );
    }
    );

  </script>

  <style type="text/css">
  a.picLnk { display:inline-block; width:20px; text-align:center; color:#333; font-family:'malgun gothic', gulim; font-size:10pt; text-decoration:none;}
  a { outline:none; }
  a img { border:none; }
  </style>

</head>
<body style="margin:0;background-color:#f7f7f7;">
    <form id="form1" runat="server">
    <div id="galView" style="width:100%; text-align:center;">
      <div style="width:100%; height:320px; padding:10px;">
        <asp:Repeater ID="PhotoRepeater" runat="server" 
          onitemdatabound="PhotoRepeater_ItemDataBound" >
          <ItemTemplate>
            <a id="A1" href='<%# Container.DataItem %>' runat="server" target="_blank"><asp:Image ID="BizImg" 
              runat="server" ImageUrl='<%# Container.DataItem %>' alt="" /></a>
          </ItemTemplate>
        </asp:Repeater>
      </div>
      <div>
        <asp:Repeater ID="LinkRepeater" runat="server">
          <ItemTemplate>
            <a id="A2" href='' runat="server" target="_blank" class="picLnk"><%# Container.ItemIndex + 1 %></a>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>
    </form>
</body>
</html>
