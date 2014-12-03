<%@ Page Language="C#" AutoEventWireup="true" Inherits="Classified_GalleryView" Codebehind="GalleryView.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <script src="../../Scripts/jquery-latest.min.js" type="text/javascript"></script>
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
<body style="margin:0;">
  <form id="form1" runat="server">
    <div id="galView" style="width:480px; text-align:center;">
      <div style="width:480px; height:360px;">
        <asp:Repeater ID="PhotoRepeater" runat="server" 
          onitemdatabound="PhotoRepeater_ItemDataBound" >
          <ItemTemplate>
            <a href="" runat="server"  id ="BizImg_link"
            target="_blank"><asp:Image ID="BizImg" runat="server" 
            ImageUrl="" alt="" /></a>
          </ItemTemplate>
        </asp:Repeater>
      </div>
      <div>
        <asp:Repeater ID="LinkRepeater" runat="server" onitemdatabound="LinkRepeater_ItemDataBound">
          <ItemTemplate>
            <a href="" runat="server" target="_blank" class="picLnk" id="image_counter_link">
                <asp:Label ID="image_counter" runat="server" Text=""></asp:Label></a>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </div>
  </form>
    
</body>
</html>
