using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace ZhooSoft.Controls
{
    public class UserCustomMap : Map
    {
        public static readonly BindableProperty MapRegionProperty =
            BindableProperty.Create(
                nameof(MapRegion),
                typeof(MapSpan),
                typeof(CustomMapView),
                default(MapSpan),
                propertyChanged: OnMapRegionChanged);

        public static readonly BindableProperty PinsSourceProperty =
        BindableProperty.Create(
            nameof(PinsSource),
            typeof(ObservableCollection<Pin>),
            typeof(CustomMapView),
            default(ObservableCollection<Pin>));

        public ObservableCollection<Pin> PinsSource
        {
            get => (ObservableCollection<Pin>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        public UserCustomMap()
        {
            PropertyChanged -= CustomMapView_PropertyChanged;
            PropertyChanged += CustomMapView_PropertyChanged;
        }

        private void CustomMapView_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PinsSource))
            {
                OnPinsSourceChanged();
            }
        }

        public MapSpan MapRegion
        {
            get => (MapSpan)GetValue(MapRegionProperty);
            set => SetValue(MapRegionProperty, value);
        }

        private static void OnMapRegionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = (CustomMapView)bindable;
            var newRegion = (MapSpan)newValue;

            if (newRegion != null)
            {
                map.MoveToRegion(newRegion);
            }
        }

        public bool PinPosUpdate = false;

        public Pin? CenterPin { get; set; }

        private void OnPinsSourceChanged()
        {
            if (PinsSource != null)
            {
                Pins.Clear();
                foreach (var pin in PinsSource)
                {
                    CenterPin = pin;
                    Pins.Add(CenterPin);
                }
            }
        }
    }
}

