$(document).ready(function () {


    $("[tag='gmap']").click(
       function () {
           var lat2, lon2, lat1, lon1;
           lat2 = document.getElementById("ctl00_lat2").value
           lon2 = document.getElementById("ctl00_lon2").value
           lat1 = document.getElementById("ctl00_lat1").value
           lon1 = document.getElementById("ctl00_lon1").value

           window.open('http://maps.google.ca/maps?daddr=' + lat2 + ',' + lon2 + '&saddr=' + lat1 + ',' + lon1);
       });


       $("#getDirection").click(
       function () {
           var lat2, lon2, lat1, lon1;
           lat2 = document.getElementById("ctl00_lat2").value
           lon2 = document.getElementById("ctl00_lon2").value
           lat1 = document.getElementById("ctl00_lat1").value
           lon1 = document.getElementById("ctl00_lon1").value

           window.open('BizMap.aspx?to=' + lat2 + ',' + lon2 + '&from=' + lat1 + ',' + lon1);
       });





    $("[tag='gmap']").ready(initiate_geolocation);



    function initiate_geolocation() {

        if (document.getElementById("ctl00_zipcode").value == "") {
//            var address = document.getElementById("addressPanel")
//            address.visiable = false;
            document.getElementById("addressPanel").style.display = "none"
        }
        else
        {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(handle_geolocation_query, handle_errors);
            }
            else {
                yqlgeo.get('visitor', normalize_yql_response);
            }
        }
    }


    function handle_errors(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED: 
            //alert("user did not share geolocation data");
                break;

            case error.POSITION_UNAVAILABLE: alert("could not detect current position");
                break;

            case error.TIMEOUT: alert("retrieving position timed out");
                break;

            default: alert("unknown error");
                break;
        }
    }
    function normalize_yql_response(response) {
        if (response.error) {
            var error = { code: 0 };
            handle_error(error);
            return;
        }

        var position = {
            coords:
            {
                latitude: response.place.centroid.latitude,
                longitude: response.place.centroid.longitude
            },
            address:
            {
                city: response.place.locality2.content,
                region: response.place.admin1.content,
                country: response.place.country.content
            }
        };

        handle_geolocation_query(position);
    }

    function handle_geolocation_query(position) {
        var zipcode = document.getElementById("ctl00_zipcode").value;

        //        alert('Lat: ' + position.coords.latitude +
        //                  ' Lon: ' + position.coords.longitude);
        calculateDistance(position, zipcode);
    }





    function calculateDistance(position, zipcode) {


        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': zipcode },
        function (results, status) {

            if (status == google.maps.GeocoderStatus.OK) {

                var lat = results[0].geometry.location.lat()
                var lng = results[0].geometry.location.lng()
                try {

                    var glatlng1 = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    var glatlng2 = new google.maps.LatLng(lat, lng);

                    var lat1 = position.coords.latitude;
                    var lon1 = position.coords.longitude;

                    var lat2 = lat;
                    var lon2 = lng;

                    var R = 6371; // km (change this constant to get miles)
                    var dLat = (lat2 - lat1) * Math.PI / 180;
                    var dLon = (lon2 - lon1) * Math.PI / 180;
                    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
                    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
                    var d = R * c;

                    var displayvalue;
                    if (d > 1) displayvalue = Math.round(d) + "km";
                    else if (d <= 1) displayvalue = Math.round(d * 1000) + "m";

                    //var miledistance = glatlng1.distanceFrom(glatlng2, 3959).toFixed(1);
                    //var kmdistance = (miledistance * 1.609344).toFixed(1);

                    // get distance
                    document.getElementById('ctl00_distance').innerHTML = displayvalue; // miledistance + ' miles (or ' + kmdistance + ' kilometers)';
                    //window.open("http://www.google.ca",'_blank');
                    //window.open('http://maps.google.ca/maps?daddr=' + lat2 + ',' + lon2 + '&saddr=' + lat1 + ',' + lon1);

                    document.getElementById("ctl00_lat2").value = lat2;
                    document.getElementById("ctl00_lon2").value = lon2;
                    document.getElementById("ctl00_lat1").value = lat1;
                    document.getElementById("ctl00_lon1").value = lon1;


                }
                catch (error) {
                    alert(error);
                    //return false;
                }
            }
            else {
                //return false;
                alert(status);
            }

        });
    }



    //        function handle_geolocation_query(position) {
    //            var image_url = "http://maps.google.com/maps/api/staticmap?sensor=false&center=" + position.coords.latitude + "," +
    //                    position.coords.longitude + "&zoom=14&size=300x400&markers=color:blue|label:S|" +
    //                    position.coords.latitude + ',' + position.coords.longitude;

    //            //jQuery("#map").remove();
    //            jQuery(document.body).append(
    //        jQuery(document.createElement("img")).attr("src", image_url).attr('id', 'map')
    //    );
    //        }  

});







