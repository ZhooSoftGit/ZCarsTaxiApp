using Microsoft.Maui.Controls.Maps;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxiApp.ViewModel;

namespace ZTaxiApp.Views.Common;

public partial class MapViewPage : BaseContentPage<MapViewViewModel>
{
    IDispatcherTimer timer;
    public MapViewPage()
	{
		InitializeComponent();
        ViewModel.CurrentMap = MyMap;
       // MyMap.PropertyChanged += MyMap_PropertyChanged;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        StartMonitoringMapCenter(); ;
    }

    //private async void MyMap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    //{
    //    if (e.PropertyName == nameof(MyMap.VisibleRegion))
    //    {
    //        var center = MyMap.VisibleRegion.Center;


    //        // If needed, you can also place a pin dynamically here
    //        UpdateCenterPin(center);
    //    }
    //}

    private async void UpdateCenterPin(Location location)
    {
        //MyMap.Pins.Clear();
        //MyMap.Pins.Add(new CustomPin
        //{
        //    Label = "Your Location",
        //    Type = PinType.Place,
        //    Location = location,
        //    Address = "Location",
        //    ImageSource = "car_icon.png"
        //});

       // var newCenter = MyMap.VisibleRegion.Center;

        ViewModel.PinPosUpdated(location);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        timer.Stop();
    }

    private bool isPinUpdated = false;

    private int waitForDelay = 0;
    private Location _oldCenter;

    private async Task StartMonitoringMapCenter()
    {
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(1);
        timer.Tick += (s, e) =>
        {
            if (MyMap.VisibleRegion != null)
            {
                var newCenter = MyMap.VisibleRegion.Center;

                // Update the pin position if the center has changed
                if (_oldCenter?.Latitude != newCenter.Latitude ||
                    _oldCenter?.Longitude != newCenter.Longitude)
                {
                    _oldCenter = newCenter;
                    //MyMap.Pins.Clear();
                    //MyMap.Pins.Add(CenterPin);
                    waitForDelay = 0;
                    isPinUpdated = true;
                }
                else
                {
                    if (isPinUpdated && waitForDelay > 500)
                    {
                        isPinUpdated = false;
                        waitForDelay = 0;
                        ViewModel.PinPosUpdated(_oldCenter);
                    }

                    waitForDelay++;
                }
            }
        };
        timer.Start();
    }
}