using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxi.Services.Contracts;
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

        private ITaxiBookingService? _taxiService;

        #endregion

        #region Constructors

        public RideMapBaseViewModel()
        {
            OpenSideBarCmd = new AsyncRelayCommand(OpenSideBar);

            PickupLocation = new LocationInfo();
            DropLocation = new LocationInfo();

            SelectPickupLocationCommand = new AsyncRelayCommand(OnSelectPickupLocation);
            SelectDropLocationCommand = new AsyncRelayCommand(OnSelectDropLocation);

            LoadRideOptions();

            InitializeService();
        }

        #endregion

        #region Properties

        public IRelayCommand OpenSideBarCmd { get; }

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

                    CurrentMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)));

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

            if (AppHelper.SelectedLocation != null)
            {
                if (AppHelper.SelectedLocation.LocationType == UIHelper.LocationType.Pickup)
                {
                    PickupLocation = AppHelper.SelectedLocation;
                }
                else
                {
                    DropLocation = AppHelper.SelectedLocation;
                }
            }

            AppHelper.SelectedLocation = null;
            EvaluateRideOptionsVisibility();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        internal async Task RefreshPage()
        {
        }

        private void EvaluateRideOptionsVisibility()
        {
            ShowCabOptions = !string.IsNullOrWhiteSpace(PickupLocation?.Address)
                          && !string.IsNullOrWhiteSpace(DropLocation?.Address);
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

        #endregion
    }
}
