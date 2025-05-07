using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace ZhooSoft.Controls.Platforms.Android
{

    internal class CustomMarkerClickListener : Java.Lang.Object, GoogleMap.IOnMarkerClickListener
    {
        private readonly CustomMapHandler mapHandler;

        public CustomMarkerClickListener(CustomMapHandler mapHandler)
        {
            this.mapHandler = mapHandler;
        }

        public bool OnMarkerClick(Marker marker)
        {
            var pin = mapHandler.Markers.FirstOrDefault(x => x.Value.Id == marker.Id);
            pin.Key?.SendMarkerClick();
            marker.ShowInfoWindow();
            return true;
        }
    }
}
