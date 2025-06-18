using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Auth.Model;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxi.Core.Storage;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxi.Services.Contracts;
using ZTaxiApp.Common;
using ZTaxiApp.DPopup;
using ZTaxiApp.DPopupVM;
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
        private string _oTPText;

        [ObservableProperty]
        private bool _showPickup = true;

        [ObservableProperty]
        private bool _showDriver;

        [ObservableProperty]
        private bool _enableConfirm;

        [ObservableProperty]
        private string _driverInfo;

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

            OpenChatCmd = new RelayCommand(OnChat);

            InitializeService();
        }

        private async void OnChat()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<ZhooChatPage>());
        }

        private async Task OnCancel()
        {
            var rs = await _alertService.ShowConfirmation("Info", "Do you want to cancel trip", "Yes", "No");
            if (rs)
            {
                var rideservice = ServiceHelper.GetService<IRideTripService>();
                var result = await rideservice.CancelTripAsync(new Model.DTOs.CancelTripDto
                {
                    RideTripId = 1
                });
                if (result.IsSuccess)
                {
                    await _signalR.NotifyCancelTrip();
                }
                AppHelper.CurrentRide = null;
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
                PickupLocation = PickupLocation.Address,
                PickupAddress = PickupLocation.Address,
                PickupLatitude = PickupLocation.Latitude,
                PickupLongitude = PickupLocation.Longitude,
                PickupTime = "06 Feb 2024, 07:15 PM",
                DropoffLocation = DropLocation.Address,
                DropLatitude = DropLocation.Latitude,
                DropLongitude = DropLocation.Longitude,
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
            var bookingService = ServiceHelper.GetService<ITaxiBookingService>();
            var bookingResult = await bookingService.BookRideAsync(new Model.DTOs.RideRequestModel
            {
                DropOffLatitude = DropLocation.Latitude,
                DropOffLongitude = DropLocation.Longitude,
                PickUpLatitude = PickupLocation.Latitude,
                PickUpLongitude = PickupLocation.Longitude,
                DropOffLocation = DropLocation.Address,
                PickUpLocation = PickupLocation.Address,
                RideType = RideTypeEnum.Local,
                VehicleType = VehicleTypeEnum.Sedan
            });

            if (!bookingResult.IsSuccess)
            {
                await _alertService.ShowAlert("Error", "Sorry facing some issues", "ok");
                return;
            }

            var model = new BookingRequestModel
            {
                BookingType = RideTypeEnum.Local,
                Fare = "₹ 194",
                DistanceAndPayment = "0.1 Km / Cash",
                PickupLocation = PickupLocation.Address,
                PickupAddress = PickupLocation.Address,
                PickupLatitude = PickupLocation.Latitude,
                PickupLongitude = PickupLocation.Longitude,
                PickupTime = DateTime.Now.ToString(),
                DropoffLocation = DropLocation.Address,
                DropLatitude = DropLocation.Latitude,
                DropLongitude = DropLocation.Longitude,
                RemainingBids = 3,
                UserId = UserDetails.Instance.CurrentUser.UserId,
                BoookingRequestId = bookingResult.Data.RideRequestId
            };

            await ForTesting(model);
            //if (NearbyDrivers.Count > 0)
            //{
            //    var ids = NearbyDrivers.Select(x => x.DriverId);

               
            //    _bookingPopup = new BookingPopup();
            //    _navigationService.OpenPopup(_bookingPopup);
            //    Task.Run(async () => await _signalR.SendBookingRequest(ids.ToList(), model));
            //}
            //else
            //{
            //    await _alertService.ShowAlert("OOPS", "No Drivers nearby your location.", "ok");
            //}
        }

        private async Task ForTesting(BookingRequestModel model)
        {
            await _signalR.OnBookingConfirmed(model);
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
        private BookingPopup _bookingPopup;

        public ICommand SelectDropLocationCommand { get; }

        public ICommand OpenChatCmd { get; }

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
            if (AppHelper.CurrentRide.CurrentStatus == RideStatus.Assigned)
            {
                MainThread.BeginInvokeOnMainThread(async
                    () =>
                {
                    await PlotRouteOnMap(new Location(obj.Latitude, obj.Longitude), PickupLocation.GetLocation());
                });
            }
            else if (AppHelper.CurrentRide.CurrentStatus == RideStatus.Started)
            {
                MainThread.BeginInvokeOnMainThread(async
                    () =>
                {
                    await PlotRouteOnMap(new Location(obj.Latitude, obj.Longitude), DropLocation.GetLocation());
                });
            }
        }

        private async Task UpdateOnTripStarted()
        {
            ShowDriver = true;
        }

        public async void InitializeMap()
        {
            try
            {
                var currentLoc = await AppHelper.GetUserLocation();
                if (currentLoc != null)
                {
                    PickupLocation = new LocationInfo
                    {
                        Latitude = currentLoc.Latitude,
                        IsCurrentLocation = true,
                        Longitude = currentLoc.Longitude,
                        LocationType = UIHelper.LocationType.Pickup,
                        Address = "Your Location"
                    };
                    DropLocation = new LocationInfo();
                    await UpdateMapUI();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location: {ex.Message}");
            }
        }

        public async Task UpdateMapUI()
        {
            var location = PickupLocation.GetLocation();
            if (!PickupLocation.IsCurrentLocation)
            {
                var placedetails = await ServiceHelper.GetService<IAddressService>().GetPlaceNameAsync(location.Latitude, location.Longitude);
                PickupLocation = new LocationInfo { Address = placedetails, Latitude = location.Latitude, Longitude = location.Longitude, LocationType = UIHelper.LocationType.Pickup };
            }

            if (PickupLocation != null)
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
            }
            await RefreshNearbyDrivers();
        }
        public ICommand CancelCommand { get; }
        public ICommand ShareCommand { get; }
        public ICommand CallCommand { get; }
        public async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            await GetCurrentRide();

            if (!IsLoaded)
            {
                Task.Run(async () => await InitializeSignalR());
                RefreshPage();
            }

            IsLoaded = true;

            if (NavigationParams != null && NavigationParams.ContainsKey("selectedlocation"))
            {
                if (NavigationParams["selectedlocation"] is LocationInfo locinfo)
                {
                    if (locinfo.LocationType == UIHelper.LocationType.Pickup)
                    {
                        PickupLocation = locinfo;
                        await UpdateMapUI();
                    }
                    else
                    {
                        DropLocation = locinfo;
                    }
                }
            }

            await EvaluateRideOptionsVisibility();

            IsBusy = false;
            NavigationParams?.Clear();
        }

        private async Task InitializeSignalR()
        {
            _signalR.Initialize(UserDetails.Instance.CurrentUser.UserId);
            await _signalR.ConnectAsync();
            _signalR.OnNearbyDriversUpdated -= _signalR_OnNearbyDriversUpdated;
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
                        ImageSource = "car_icon.png",
                        IsDriverPins = true
                    };

                    CurrentMap.Pins.Add(customPin);
                }
            });
        }

        private async Task RefreshNearbyDrivers()
        {
            var userLocation = PickupLocation.GetLocation();
            await _signalR.GetNearbyDriversAsync(userLocation);
        }

        public override void OnDisappearing()
        {
            _signalR.DisconnectAsync();
            base.OnDisappearing();
        }

        internal async void RefreshPage(bool isFromHandler = false)
        {
            if (isFromHandler && _bookingPopup != null)
            {
                await _bookingPopup.CloseAsync();
                _bookingPopup = null;
            }

            if (AppHelper.CurrentRide != null)
            {
                ShowPickup = false;
                ShowDriver = true;
                OngoingTrip();
                await UpdateUIView();
            }
            else
            {
                InitializeMap();
            }
        }

        private async Task UpdateUIView()
        {
            if (AppHelper.CurrentRide != null && AppHelper.CurrentRide.TripOtpInfo != null)
            {
                if (AppHelper.CurrentRide.CurrentStatus == RideStatus.Assigned)
                {
                    DriverInfo = "Driver is on the way";
                    OTPText = $"Starts OTP is {AppHelper.CurrentRide.TripOtpInfo.StartOtp}";
                }
                if (AppHelper.CurrentRide.CurrentStatus == RideStatus.Started)
                {
                    DriverInfo = "Happy journey Buddy";
                    OTPText = $"End OTP is {AppHelper.CurrentRide.TripOtpInfo.EndOtp}";
                }
                if (AppHelper.CurrentRide.CurrentStatus == RideStatus.Reached)
                {
                    DriverInfo = "Driver is reached your location";
                }
            }
            else
            {
                DriverInfo = "Driver is started";
                OTPText = "Receive OTP shortly";
            }

            if (AppHelper.CurrentRide?.CurrentStatus == RideStatus.Completed)
            {
                _signalR.OnDriverLocationUpdated -= _signalR_OnDriverLocationUpdated;
                AppHelper.CurrentRide = null;
                ShowPickup = true;
                ShowDriver = false;
                InitializeMap();
                var pp = new OnStartOtpPopup();

                if (pp.BindingContext is OnStartOtpViewModel vm1)
                {
                    vm1.ShowEndTrip = false;
                    vm1.ShowOtp = false;
                    vm1.ShowRideSuccess = true;
                }

                _navigationService.OpenPopup(pp);
            }
            if (AppHelper.CurrentRide?.CurrentStatus == RideStatus.Cancelled)
            {
                _signalR.OnDriverLocationUpdated -= _signalR_OnDriverLocationUpdated;
                await _alertService.ShowAlert("Info", "Your request is cancelled", "ok");
                AppHelper.CurrentRide = null;
                ShowPickup = true;
                ShowDriver = false;
                InitializeMap();
            }
        }

        private async Task EvaluateRideOptionsVisibility()
        {
            EnableConfirm = !string.IsNullOrWhiteSpace(PickupLocation?.Address)
                          && !string.IsNullOrWhiteSpace(DropLocation?.Address);

            if (EnableConfirm)
            {
                IsBusy = true;
                await Task.Delay(100);
                await PlotRouteOnMap(new Location(PickupLocation.Latitude, PickupLocation.Longitude), new Location(DropLocation.Latitude, DropLocation.Longitude));
                LoadRideOptions();
                ShowCabOptions = true;
                IsBusy = false;
            }
        }

        private async Task GetCurrentRide()
        {
            //API call is any ride available

            if (AppHelper.CurrentRide != null)
            {

            }
        }

        private void InitializeService()
        {
            _osrmService = ServiceHelper.GetService<IOsrmService>();
            _callService = ServiceHelper.GetService<ICallService>();
            _rideTripService = ServiceHelper.GetService<IRideTripService>();
            _taxiService = ServiceHelper.GetService<ITaxiBookingService>();

            _signalR = ServiceHelper.GetService<UserSignalRService>();
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

        private async Task PlotRouteOnMap(Location fromLocation, Location toLocation)
        {
            try
            {
                // Get encoded polyline from OSRM
                var result = await _osrmService.GetRoutePoints(fromLocation.Latitude, fromLocation.Longitude, toLocation.Latitude, toLocation.Longitude);

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

                RouteDistance = (result.Distance ?? 0) / 1000;
                DistanceText = $"{RouteDistance:0.00} km";

                Duration = result.duration;
                DurationText = GetDurationText(Duration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error plotting route: {ex.Message}");
            }
        }

        [ObservableProperty]
        private string _durationText;

        [ObservableProperty]
        private string _distanceText;

        private string GetDurationText(double? durationInSeconds)
        {
            // Convert seconds to hr:min
            TimeSpan time = TimeSpan.FromSeconds(durationInSeconds ?? 0);
            string readableDuration;

            if (time.TotalHours >= 1)
                readableDuration = $"{(int)time.TotalHours} hr {(int)time.Minutes} min";
            else
                readableDuration = $"{(int)time.Minutes} min";

            return readableDuration;
        }

        #endregion
    }
}
