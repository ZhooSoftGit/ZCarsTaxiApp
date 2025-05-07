namespace ZCarsDriver.Services
{
    public class PolylineDecoder
    {
        public static List<Location> DecodePolyline(string encoded)
        {
            if (string.IsNullOrEmpty(encoded)) return null;

            var polyline = new List<Location>();
            int index = 0, len = encoded.Length;
            int lat = 0, lng = 0;

            while (index < len)
            {
                int result = 0, shift = 0;
                int b;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1F) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlat = (result & 1) != 0 ? ~(result >> 1) : (result >> 1);
                lat += dlat;

                result = 0;
                shift = 0;
                do
                {
                    b = encoded[index++] - 63;
                    result |= (b & 0x1F) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlng = (result & 1) != 0 ? ~(result >> 1) : (result >> 1);
                lng += dlng;

                polyline.Add(new Location(lat / 1e5, lng / 1e5));
            }

            return polyline;
        }


        public static double CalculateBearing(Location start, Location end)
        {
            var lat1 = DegreesToRadians(start.Latitude);
            var lon1 = DegreesToRadians(start.Longitude);
            var lat2 = DegreesToRadians(end.Latitude);
            var lon2 = DegreesToRadians(end.Longitude);

            var dLon = lon2 - lon1;
            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            var bearing = Math.Atan2(y, x);

            return (RadiansToDegrees(bearing) + 360) % 360; // Normalize to 0-360
        }

        private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;
        private static double RadiansToDegrees(double radians) => radians * 180 / Math.PI;
    }
}
