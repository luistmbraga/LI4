<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BingMap.aspx.cs" Inherits="FitChef.View.BingMap" %>





<!DOCTYPE html>
<html>
<head>
    <title>Bing Map</title>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <style type='text/css'>
        body {
            margin: 2%;
            padding: 2%;
            overflow: auto;
            font-family: 'Segoe UI',Helvetica,Arial,Sans-Serif
        }
        div {
            opacity: 1;
        }
        #page-wrap {
             width: 1000px;
             margin: 0 auto;
        }
        .google_map {
            width: 55%;
            margin-right: 2%;
            float: left;
        }

        .google_map iframe {
            width: 100%;
        }

        .paragraph {
            width: 42%;
            float: left;
        }

        .clearfix {
            clear: both
        }
    </style>
</head>
<body background="../imgs/frutosSimples.jpg">

    <div id="page-wrap">
    
    <div class="google_map" id='myMap' style='width: 500px; height: 500px;'>
        <iframe></iframe>
    </div>
    <div class="paragraph">
        <p id='directionsInputContainer'></p>
        <p id='printoutPanel'></p>
    </div>
    <div class="clearfix"></div>
    
        </div>

    <script type='text/javascript'>
        function loadMapScenario() {

            var map = new Microsoft.Maps.Map(document.getElementById('myMap'), {
                /* No need to set credentials if already passed in URL */
                credentials: "AiBLnskBdAfHMQ9868ZuugboFWrhz_1c6HcjrlrqIQEmsLJZBuukzYyEDAd2qJOt",
                center: new Microsoft.Maps.Location(41.561760, -8.396108), //Universidade
                zoom: 12
            });


            let cities = [
                ["Pingo Doce São Vítor", 41.551131, -8.410891],
                ["Pingo Doce Braga Parque", 41.558453, -8.404967],
                ["Pingo Doce Frossos", 41.565839, -8.449257]
            ];

            let lat;
            let long;

            let destName = "Default";
            let destLat = 41.561760; 
            let destLong = -8.396108;

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(UserLocation);
            }
            // Default to Washington, DC
            else
            {
                lat = 38.8951;
                long = -77.0367;
            }
                
        
            // Callback function for asynchronous call to HTML5 geolocation
            function UserLocation(position) {
                lat = position.coords.latitude;
                long = position.coords.longitude;
            }


            // Convert Degress to Radians
            function Deg2Rad(deg) {
                return deg * Math.PI / 180;
            }

            function PythagorasEquirectangular(lat1, lon1, lat2, lon2) {
                lat1 = Deg2Rad(lat1);
                lat2 = Deg2Rad(lat2);
                lon1 = Deg2Rad(lon1);
                lon2 = Deg2Rad(lon2);
                var R = 6371; // km
                var x = (lon2 - lon1) * Math.cos((lat1 + lat2) / 2);
                var y = (lat2 - lat1);
                var d = Math.sqrt(x * x + y * y) * R;
                return d;
            }

            function NearestCity(latitude, longitude) {
                var minDif = 99999;
                let closest;

                for (index = 0; index < cities.length; ++index) {
                    var dif = PythagorasEquirectangular(latitude, longitude, cities[index][1], cities[index][2]);
                    if (dif < minDif) {
                        closest = index;
                        minDif = dif;
                    }
                }              

                destName = cities[closest][0];
                destLat = cities[closest][1];
                destLong = cities[closest][2];
            }


            Microsoft.Maps.loadModule('Microsoft.Maps.Directions', function () {
                var directionsManager = new Microsoft.Maps.Directions.DirectionsManager(map);

                NearestCity(lat, long);

                // Set Route Mode to driving
                directionsManager.setRequestOptions({ routeMode: Microsoft.Maps.Directions.RouteMode.driving });  
                var waypoint1 = new Microsoft.Maps.Directions.Waypoint({ address: 'My Location', location: new Microsoft.Maps.Location(lat, long) }); // universidade 41.561760, -8.396108
                var waypoint2 = new Microsoft.Maps.Directions.Waypoint({ address: destName, location: new Microsoft.Maps.Location(destLat, destLong) }); // braga parque 41.558453, -8.404967
                directionsManager.addWaypoint(waypoint1);
                directionsManager.addWaypoint(waypoint2);
                // Set the element in which the itinerary will be rendered
                directionsManager.setRenderOptions({ itineraryContainer: document.getElementById('printoutPanel') });
                directionsManager.showInputPanel('directionsInputContainer');
                directionsManager.calculateDirections();
            });

        }
    </script>

    <script type='text/javascript' src='https://www.bing.com/api/maps/mapcontrol?key=YourBingMapsKey&callback=loadMapScenario' async defer></script>
</body>
</html>


<!--

 credentials : "AiBLnskBdAfHMQ9868ZuugboFWrhz_1c6HcjrlrqIQEmsLJZBuukzYyEDAd2qJOt"

 // Unniversidade
Unnamed Road
Braga
41.561760, -8.396108

// Pingo Doce:

São Vítor
Braga
41.551131, -8.410891


Braga (Braga Parque)
41.558453, -8.404967

Frossos
Braga
41.565839, -8.449257

-->
