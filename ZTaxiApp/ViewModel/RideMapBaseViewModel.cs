using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Auth.Model;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxi.Services.Contracts;
using ZTaxiApp.Common;
using ZTaxiApp.DPopup;
using ZTaxiApp.Helpers;
using ZTaxiApp.Services;
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
            ShareCommand = new AsyncRelayCommand(OnShowDetails);
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

        private async Task OnShowDetails()
        {
            var model = new BookingRequestModel
            {
                BookingType = RideTypeEnum.Local,
                Fare = "₹ 194",
                DistanceAndPayment = "0.1 Km / Cash",
                PickupLocation = "Muthanampalayam, Tiruppur",
                PickupAddress = "3/21, Muthanampalayam, Tiruppur, Tamil Nadu 641606, India",
                PickupLatitude = 11.0176,
                PickupLongitude = 76.9674,
                PickupTime = "06 Feb 2024, 07:15 PM",
                DropoffLocation = "Tiruppur Old Bus Stand",
                DropLatitude = 10.9902,
                DropLongitude = 76.9629,
                RemainingBids = 3
            };

            var nvparam = new Dictionary<string, object> { { "BookingDetails", model } };

            await _navigationService.OpenPopup(ServiceHelper.GetService<BookingDetailsPopup>(), nvparam);
        }

        private async void OnCall()
        {
            // Logic to open phone call
            await _callService?.MakePhoneCall("8344273152");
        }

        private async Task OnConfirmBooking()
        {
            if (NearbyDrivers.Count > 0)
            {
                var ids = NearbyDrivers.Select(x => x.DriverId);

                var model = new BookingRequestModel
                {
                    BookingType = RideTypeEnum.Local,
                    Fare = "₹ 194",
                    DistanceAndPayment = "0.1 Km / Cash",
                    PickupLocation = "Muthanampalayam, Tiruppur",
                    PickupAddress = "3/21, Muthanampalayam, Tiruppur, Tamil Nadu 641606, India",
                    PickupLatitude = 11.0176,
                    PickupLongitude = 76.9674,
                    PickupTime = "06 Feb 2024, 07:15 PM",
                    DropoffLocation = "Tiruppur Old Bus Stand",
                    DropLatitude = 10.9902,
                    DropLongitude = 76.9629,
                    RemainingBids = 3,
                    UserId = UserDetails.getInstance().UserID
                };
                var popup = new BookingPopup();
                _navigationService.OpenPopup(popup);
                var result = await _signalR.SendSequentialRequests(ids.ToList(), model);
                popup.Close();
                if (!result)
                {
                    await _alertService.ShowAlert("OOPS", "Driver is not accepted", "ok");
                }
                else
                {
                    ShowPickup = false;
                    OngoingTrip();
                }
            }
            else
            {
                await _alertService.ShowAlert("OOPS", "No Drivers nearby your location.", "ok");
            }
        }

        #endregion

        #region Properties

        private UserSignalRService _signalR;

        public List<DriverLocation> NearbyDrivers { get; set; } = new();

        public IRelayCommand OpenSideBarCmd { get; }

        public IAsyncRelayCommand ConfirmBookingCmd { get; }

        [ObservableProperty]
        private double? _routeDistance;

        [ObservableProperty]
        private double? _duration;

        public ICommand SelectDropLocationCommand { get; }

        public ICommand SelectPickupLocationCommand { get; }

        #endregion

        #region Methods


        public async void OngoingTrip()
        {
            MainThread.BeginInvokeOnMainThread(async () => { await UpdateOnTripStarted(); });
            _signalR.OnDriverLocationUpdated -= _signalR_OnDriverLocationUpdated;
            _signalR.OnDriverLocationUpdated += _signalR_OnDriverLocationUpdated;
        }

        private void _signalR_OnDriverLocationUpdated(DriverLocation obj)
        {
            
        }

        private async Task UpdateOnTripStarted()
        {
            ShowDriver = true;
        }

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

            if (!IsLoaded)
            {
                _ = Task.Run(InitializeSignalR);
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
                EvaluateRideOptionsVisibility();
            }

            NavigationParams?.Clear();
        }

        private async Task InitializeSignalR()
        {
            _signalR = ServiceHelper.GetService<UserSignalRService>();
            await _signalR.ConnectAsync();
            await RefreshNearbyDrivers();
            _signalR.OnNearbyDriversUpdated += _signalR_OnNearbyDriversUpdated;
        }

        private void _signalR_OnNearbyDriversUpdated(List<DriverLocation> nearbyDrivers)
        {
            NearbyDrivers = nearbyDrivers;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentMap.Pins.Clear();
                var driverpins = new List<Pin>();


                foreach (var driver in nearbyDrivers)
                {
                    var customPin = new CustomPin
                    {
                        Label = "Driver",
                        Type = PinType.Generic,
                        Location = new Location(driver.Latitude, driver.Longitude),
                        Address = "Driver",
                        ImageSource = "pin.png",
                        IsDriverPins = true
                    };
                    CurrentMap.Pins.Add(customPin);
                }
            });
        }

        private async Task RefreshNearbyDrivers()
        {
            var userLocation = await AppHelper.GetUserLocation();
            await _signalR.GetNearbyDriversAsync(userLocation);
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
                LoadRideOptions();
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
            var result = await _navigationService.OpenPopupAsync(new CommonMenu());

            await Task.Delay(100);
            IsBusy = false;
        }

        private async Task PlotRouteOnMap(LocationInfo? fromLocation, LocationInfo? toLocation)
        {
            try
            {
                // Get encoded polyline from OSRM
                var result = await _osrmService.GetRoutePoints(fromLocation.Latitude, fromLocation.Longitude, DropLocation.Latitude, DropLocation.Longitude);

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

                Duration = result.duration;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error plotting route: {ex.Message}");
            }
        }

        #endregion
    }
}
