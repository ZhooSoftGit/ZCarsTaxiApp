
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace ZhooSoft.Controls
{
    public class CustomMapView : Map
    {
        public CustomMapView()
        {
            
        }

        public CustomMapView(MapSpan span) : base(span)
        {

        }
    }
}
