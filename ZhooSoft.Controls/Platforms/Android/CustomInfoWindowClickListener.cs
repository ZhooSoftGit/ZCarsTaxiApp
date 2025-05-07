using Android.Gms.Maps;
using Android.Gms.Maps.Model;
namespace ZhooSoft.Controls.Platforms.Android
{
    internal class CustomInfoWindowClickListener : Java.Lang.Object, GoogleMap.IOnInfoWindowClickListener
    {
        private readonly CustomMapHandler mapHandler;

        public CustomInfoWindowClickListener(CustomMapHandler mapHandler)
        {
            this.mapHandler = mapHandler;
        }

        public void OnInfoWindowClick(Marker marker)
        {
            var pin = mapHandler.Markers.FirstOrDefault(x => x.Value.Id == marker.Id);
            pin.Key?.SendInfoWindowClick();
        }
    }
}
