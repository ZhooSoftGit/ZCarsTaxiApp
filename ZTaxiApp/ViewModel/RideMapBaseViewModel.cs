using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxi.Services.Contracts;
using ZTaxiApp.DPopup;
using ZTaxiApp.Helpers;
using ZTaxiApp.Services.AppService;
using ZTaxiApp.Services.Contracts;
using ZTaxiApp.UIModel;
using ZTaxiApp.Views.Common;
using ZTaxiApp.Views.Driver;

namespace ZTaxiApp.ViewModel
{
    public partial class RideMapBaseViewModel : ViewModelBase
    {
        #region Fields

        public CustomMapView CurrentMap;

        private ICallService? _callService;

        [ObservableProperty]
        private LocationInfo _dropLocation;

        private IOsrmService _osrmService;

        [ObservableProperty]
        private LocationInfo _pickupLocation;

        [ObservableProperty]
        private ObservableCollection<RideOption> _rideOptions = new();

        private IRideTripService? _rideTripService;

        [ObservableProperty]
        private bool _showCabOptions;

        [ObservableProperty]
        private bool _showPickup = true;

        [ObservableProperty]
        private bool _showDriver;

        private ITaxiBookingService? _taxiService;



        #endregion

        #region Constructors

        public RideMapBaseViewModel()
        {
            OpenSideBarCmd = new AsyncRelayCommand(OpenSideBar);
            ConfirmBookingCmd = new AsyncRelayCommand(OnConfirmBooking);

            PickupLocation = new LocationInfo();
            DropLocation = new LocationInfo();

            SelectPickupLocationCommand = new AsyncRelayCommand(OnSelectPickupLocation);
            SelectDropLocationCommand = new AsyncRelayCommand(OnSelectDropLocation);

            CancelCommand = new AsyncRelayCommand(OnCancel);
            ShareCommand = new AsyncRelayCommand(OnShare);
            CallCommand = new RelayCommand(OnCall);

            InitializeService();
        }

        private async Task OnCancel()
        {
            var rs = await _alertService.ShowConfirmation("Info", "Do you want to cancel", "Yes", "No");
            if (rs)
            {
                ShowDriver = false;
                ShowPickup = true;
            }
        }

        private async Task OnShare()
        {
            var pp = new BookingRequestPopup();
            await _navigationService.OpenPopup(pp);
        }

        private async void OnCall()
        {
            // Logic to open phone call
            await _callService?.MakePhoneCall("8344273152");
        }

        private async Task OnConfirmBooking()
        {
            var confirmPopup = new BookingPopup();
            var result = await _navigationService.OpenPopup(confirmPopup);
            if (result is bool res && res)
            {
                ShowPickup = false;
                ShowDriver = true;
            }
        }

        #endregion

        #region Properties

        public IRelayCommand OpenSideBarCmd { get; }

        public IRelayCommand ConfirmBookingCmd { get; }

        public double? RouteDistance { get; private set; }

        public ICommand SelectDropLocationCommand { get; }

        public ICommand SelectPickupLocationCommand { get; }

        #endregion

        #region Methods

        public async void InitializeMap()
        {
            try
            {
                var location = await AppHelper.GetUserLocation();
                if (location != null)
                {
                    CurrentMap.MapElements.Clear();
                    CurrentMap.Pins.Clear();
                    var position = new Location(location.Latitude, location.Longitude);
                    var pin = new CustomPin
                    {
                        Label = "Your Location",
                        Type = PinType.Place,
                        Location = position,
                        Address = "Location",
                        ImageSource = "car_icon.png"
                    };

                    CurrentMap.Pins.Add(pin);

                    CurrentMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5)));

                    var placedetails = await ServiceHelper.GetService<IAddressService>().GetPlaceNameAsync(location.Latitude, location.Longitude);

                    if (placedetails != null)
                    {
                        PickupLocation = new LocationInfo { Address = placedetails, Latitude = location.Latitude, Longitude = location.Longitude, LocationType = UIHelper.LocationType.Pickup };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location: {ex.Message}");
            }
        }
        public ICommand CancelCommand { get; }
        public ICommand ShareCommand { get; }
        public ICommand CallCommand { get; }
        public async override void OnAppearing()
        {
            base.OnAppearing();
            await GetCurrentRide();
            if (!IsLoaded && AppHelper.CurrentRide == null)
            {
                InitializeMap();
            }
            else if (!IsLoaded && AppHelper.CurrentRide != null)
            {
                await RefreshPage();
            }

            IsLoaded = true;

            if (NavigationParams != null && NavigationParams.ContainsKey("selectedlocation"))
            {
                if (NavigationParams["selectedlocation"] is LocationInfo locinfo)
                {
                    if (locinfo.LocationType == UIHelper.LocationType.Pickup)
                        PickupLocation = locinfo;
                    else
                        DropLocation = locinfo;
                }
            }

            EvaluateRideOptionsVisibility();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        internal async Task RefreshPage()
        {
        }

        private async void EvaluateRideOptionsVisibility()
        {
            var ridelocationConfirmed = !string.IsNullOrWhiteSpace(PickupLocation?.Address)
                          && !string.IsNullOrWhiteSpace(DropLocation?.Address);

            if (ridelocationConfirmed)
            {
                IsBusy = true;
                await Task.Delay(100);
                await PlotRouteOnMap(PickupLocation, DropLocation);
                ShowCabOptions = true;
                IsBusy = false;
            }
        }

        private async Task GetCurrentRide()
        {
        }

        private void InitializeService()
        {
            _osrmService = ServiceHelper.GetService<IOsrmService>();
            _callService = ServiceHelper.GetService<ICallService>();
            _rideTripService = ServiceHelper.GetService<IRideTripService>();
            _taxiService = ServiceHelper.GetService<ITaxiBookingService>();
        }

        private void LoadRideOptions()
        {
            RideOptions.Clear();
            RideOptions.Add(new RideOption { Name = "Mini", Price = "$10", Icon = "cab.png" });
            RideOptions.Add(new RideOption { Name = "Sedan", Price = "$15", Icon = "cab.png" });
            RideOptions.Add(new RideOption { Name = "SUV", Price = "$20", Icon = "cab.png" });
        }

        private async Task OnSelectDropLocation()
        {
            var parameter = new Dictionary<string, object>
            {
                { "locationInfo", new LocationInfo{ LocationType =  UIHelper.LocationType.Drop} }
            };
            await _navigationService.PushAsync(ServiceHelper.GetService<SearchLocationPage>(), parameter);
        }

        private async Task OnSelectPickupLocation()
        {
            var parameter = new Dictionary<string, object>
            {
                { "locationInfo", new LocationInfo{ LocationType =  UIHelper.LocationType.Pickup} }
            };
            await _navigationService.PushAsync(ServiceHelper.GetService<SearchLocationPage>(), parameter);
        }

        private async Task OpenSideBar()
        {
            IsBusy = true;
            var result = await _navigationService.OpenPopup(new CommonMenu());

            await Task.Delay(100);
            IsBusy = false;
        }

        private async Task PlotRouteOnMap(LocationInfo? pickupLocation, LocationInfo? dropLocation)
        {
            try
            {
                // Get encoded polyline from OSRM
                var result = await _osrmService.GetRoutePoints(pickupLocation.Latitude, pickupLocation.Longitude, DropLocation.Latitude, DropLocation.Longitude);

                if (result.Locations == null || result.Locations.Count() == 0)
                {
                    Console.WriteLine("No route found!");
                    RouteDistance = null;
                }

                // Clear previous routes
                CurrentMap.MapElements.Clear();

                // Draw the polyline
                var polyline = new Polyline
                {
                    StrokeColor = Colors.Green,
                    StrokeWidth = 5
                };

                foreach (var loc in result.Locations)
                {
                    polyline.Geopath.Add(loc);
                }

                CurrentMap.MapElements.Add(polyline);

                CurrentMap.Pins.Clear();

                var pin1 = new CustomPin
                {
                    Label = "Your Location",
                    Type = PinType.Place,
                    Location = new Location(result.Locations.First().Latitude, result.Locations.First().Longitude),
                    Address = "Location",
                    ImageSource = "car_icon.png"
                };

                var pin2 = new CustomPin
                {
                    Label = "Your Location",
                    Type = PinType.Generic,
                    Location = new Location(result.Locations.Last().Latitude, result.Locations.Last().Longitude),
                    Address = "Location",
                    ImageSource = "pin.png"
                };

                var centerLatitude = (pin1.Location.Latitude + pin2.Location.Latitude) / 2;
                var centerLongitude = (pin1.Location.Longitude + pin2.Location.Longitude) / 2;

                var latSpan = Math.Abs(pin1.Location.Latitude - pin2.Location.Latitude) * 1.5; // add padding
                var lonSpan = Math.Abs(pin1.Location.Longitude - pin2.Location.Longitude) * 1.5;

                if (latSpan < 0.01) latSpan = 0.01; // avoid too narrow span
                if (lonSpan < 0.01) lonSpan = 0.01;

                var region = MapSpan.FromCenterAndRadius(
                    new Location(centerLatitude, centerLongitude),
                    Distance.FromKilometers(Math.Max(latSpan, lonSpan) * 111) // approx 1 deg ≈ 111 km
                );

                // Center the map
                CurrentMap.MoveToRegion(region);

                CurrentMap.Pins.Add(pin1);
                CurrentMap.Pins.Add(pin2);

                RouteDistance = result.Distance;

                LoadRideOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error plotting route: {ex.Message}");
            }
        }

        #endregion
    }
}
