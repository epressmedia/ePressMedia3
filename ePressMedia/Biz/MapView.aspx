<%@ Page Language="C#" AutoEventWireup="true" Inherits="Biz_MapView" Codebehind="MapView.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title></title>
  <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> 
  <script type="text/javascript">
      function loadMap() {
          geocoder = new google.maps.Geocoder();
          var myOptions = {
              zoom: 13,
              mapTypeId: google.maps.MapTypeId.ROADMAP
          }
          map = new google.maps.Map(document.getElementById("mapCanvas"), myOptions);

          if (geocoder) {
              geocoder.geocode({ 'address': '<%= Request.QueryString["addr"] %>' }, function (results, status) {
                  if (status == google.maps.GeocoderStatus.OK) {
                      map.setCenter(results[0].geometry.location);
                      var marker = new google.maps.Marker({
                          map: map,
                          position: results[0].geometry.location
                      });
                  } else {
                      //alert("Geocode was not successful for the following reason: " + status);
                  }
              });
          }
      }
  </script>    

</head>
<body style="margin:0;" onload="javascript:loadMap();">
  <form id="form1" runat="server">
    <div id="mapCanvas" style=" height: 360px;">
    
    </div>
  </form>
</body>
</html>