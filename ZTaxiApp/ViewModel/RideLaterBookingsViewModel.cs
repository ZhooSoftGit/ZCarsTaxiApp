using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxi.Services.Contracts;
using ZTaxiApp.Helpers;
using ZTaxiApp.Services.AppService;
using ZTaxiApp.UIModel;
using ZTaxiApp.Views.Driver;
using ZTaxiApp.Views.UserApp;

namespace ZTaxiApp.ViewModel
{
    public class RentalPackage
    {
        #region Properties

        public string DisplayText => $"{Hours} hr, {Kilometers} km - ₹{Rate}";

        public int Hours { get; set; }

        public int Kilometers { get; set; }

        public int PackageId { get; set; }

        public decimal Rate { get; set; }

        #endregion
    }

    public partial class RideLaterBookingsViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private LocationInfo _dropLocation;

        [ObservableProperty]
        private double? _duration;

        private IOsrmService _osrmService;

        [ObservableProperty]
        private List<RentalPackage> _packages;

        [ObservableProperty]
        private LocationInfo _pickupLocation;

        [ObservableProperty]
        private double? _routeDistance;

        [ObservableProperty]
        private RentalPackage _selectedPackage;

        [ObservableProperty]
        private Traveller _selectedTraveller;

        [ObservableProperty]
        private List<Traveller> _travellers = Enumerable.Range(1, 10)
        .Select(i => new Traveller
        {
            Count = i,
            CountText = i == 1 ? "1 Passenger" : $"{i} Passengers"
        })
        .ToList();

        [ObservableProperty]
        private ObservableCollection<string> _tripTypes;

        [ObservableProperty]
        private ObservableCollection<string> _vehicleTypes;

        [ObservableProperty]
        string _dropAddress;

        [ObservableProperty]
        bool _isFormValid;

        [ObservableProperty]
        bool _isOutstation;

        [ObservableProperty]
        string _pickupAddress;

        [ObservableProperty]
        DateTime _pickupDate = DateTime.Now;

        [ObservableProperty]
        TimeSpan _pickupTime = TimeSpan.FromHours(9);

        [ObservableProperty]
        DateTime? _returnDate = DateTime.Now.AddDays(2);

        [ObservableProperty]
        TimeSpan? _returnTime = TimeSpan.FromHours(9);

        [ObservableProperty]
        string _selectedVehicleType;

        [ObservableProperty]
        string _tripType;

        #endregion

        #region Constructors

        public RideLaterBookingsViewModel()
        {
            IsOutstation = AppHelper.SelectedAction == ActionEnum.OutStation;
            PageTitleName = IsOutstation ? "Outstation Trip" : "Rental Trip";
            TripType = "OneWay";
            ValidateForm();
            _osrmService = ServiceHelper.GetService<IOsrmService>();
            SelectPickupLocationCommand = new AsyncRelayCommand(OnSelectPickupLocation);
            SelectDropLocationCommand = new AsyncRelayCommand(OnSelectDropLocation);
            OnReviewBookingCmd = new AsyncRelayCommand(OnReviewBooking);

            LoadDatas();

            PropertyChanged += (_, _) => ValidateForm();
        }

        #endregion

        #region Properties

        public CustomMapView CurrentMap { get; internal set; }

        public ICommand OnReviewBookingCmd { get; }

        public ICommand OpenDropMapCommand => new Command(() => OnOpenMap("drop"));

        public ICommand OpenPickupMapCommand => new Command(() => OnOpenMap("pickup"));

        public ICommand SelectDropLocationCommand { get; }

        public ICommand SelectPickupLocationCommand { get; }

        #endregion

        #region Methods

        public async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (!IsLoaded)
            {
                await GetCurrentLocationInfo();
                await UpdateMapUI();
            }

            if (NavigationParams != null && NavigationParams.ContainsKey("selectedlocation"))
            {
                if (NavigationParams["selectedlocation"] is LocationInfo locinfo)
                {
                    if (locinfo.LocationType == UIHelper.LocationType.Pickup)
                    {
                        PickupLocation = locinfo;
                        if (!IsOutstation)
                        {
                            await UpdateMapUI();
                        }
                    }
                    else
                    {
                        DropLocation = locinfo;
                    }
                }
            }

            await EvaluateRideOptionsVisibility();
            IsLoaded = true;
            IsBusy = false;
            NavigationParams?.Clear();
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
        }

        private async Task EvaluateRideOptionsVisibility()
        {
            if (IsOutstation)
            {
                var isValid = !string.IsNullOrWhiteSpace(PickupLocation?.Address)
                          && !string.IsNullOrWhiteSpace(DropLocation?.Address);

                if (isValid)
                {
                    IsBusy = true;
                    await Task.Delay(100);
                    await PlotRouteOnMap(new Location(PickupLocation.Latitude, PickupLocation.Longitude), new Location(DropLocation.Latitude, DropLocation.Longitude));
                    IsBusy = false;
                }
            }
            else
            {
                await UpdateMapUI();
            }
            
        }

        private async Task GetCurrentLocationInfo()
        {
            try
            {
                var currentLoc = await AppHelper.GetUserLocation();
                if (currentLoc != null)
                {
                    var location = new LocationInfo
                    {
                        Latitude = currentLoc.Latitude,
                        Longitude = currentLoc.Longitude,
                        LocationType = UIHelper.LocationType.Pickup
                    };
                    DropLocation = new LocationInfo();
                    var placedetails = await ServiceHelper.GetService<IAddressService>().GetPlaceNameAsync(location.Latitude, location.Longitude);
                    PickupLocation = new LocationInfo { Address = placedetails, Latitude = location.Latitude, Longitude = location.Longitude, LocationType = UIHelper.LocationType.Pickup };
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location: {ex.Message}");
            }
        }

        private void LoadDatas()
        {
            VehicleTypes = new ObservableCollection<string>(new List<string> { "Hatchback", "Sedan", "SUV", "MPV", "Luxury", "Bike Taxi" });

            Packages = new()
                            {
                                new RentalPackage { PackageId = 1, Hours = 1, Kilometers = 10, Rate = 300 },
                                new RentalPackage { PackageId = 2, Hours = 2, Kilometers = 20, Rate = 550 },
                                new RentalPackage { PackageId = 3, Hours = 4, Kilometers = 40, Rate = 999 }
                            };
        }

        private async Task OnReviewBooking()
        {
            var reviewbooking = new ReviewBookingModel
            {
                PickupLocation = PickupLocation,
                DropLocation = DropLocation,
                DistanceKm = RouteDistance ?? 0,
                Duration = TimeSpan.FromSeconds(Duration ?? 0),
                StartDateTime = PickupDate,
                EndDateTime = ReturnDate ?? null,
                EstimatedFare = 120,
                InsuranceCost = 20,
                VehicleType = SelectedVehicleType,
                TripType = TripType,
                RideInclusions =
                                [
                                    "Regularly audited cars",
                                    "24/7 on-road assistance",
                                    "Real-time tracking"
                                ],
                RulesAndRestrictions =
                                    [
                                        "Excludes toll costs, parking, permits and state tax",
                                        "₹10/km will be charged for additional hours",
                                        "₹11/min will be charged for extra km",
                                        "Driver allowance at 24 hours: ₹210",
                                        "Night time allowance (11:00 PM - 06:00 AM): ₹250/night",
                                        "Extra fare may apply if you don't end trip at Coimbatore"
                                    ],
                TermsAndConditions =
                                    [
                                        "Excludes tolls, parking, permits and state tax",
                                        "₹10/km will be charged for extra km",
                                        "₹11/min will be charged for extra time"
                                    ]
            };
            var nvparam = new Dictionary<string, object>
            {
                {"reviewBooking", reviewbooking }
            };
            await _navigationService.PushAsync(ServiceHelper.GetService<ReviewBookingPage>(), nvparam);
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

                RouteDistance = result.Distance;

                Duration = result.duration;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error plotting route: {ex.Message}");
            }
        }

        void OnOpenMap(string target)
        {
            // Navigation logic to open Map page can go here
            // E.g., MessagingCenter or Shell navigation with params
            Console.WriteLine($"Opening map for {target}");
        }

        void ValidateForm()
        {
            if (PickupLocation == null || string.IsNullOrEmpty(PickupLocation.Address) ||
                string.IsNullOrWhiteSpace(SelectedVehicleType))
            {
                IsFormValid = false;
                return;
            }

            if (IsOutstation)
            {
                if (DropLocation == null || string.IsNullOrEmpty(DropLocation.Address))
                {
                    IsFormValid = false;
                    return;
                }
            }
            else
            {
                if (SelectedPackage == null)
                {
                    IsFormValid = false;
                    return;
                }
            }

            IsFormValid = true;
        }

        #endregion
    }

    public class Traveller
    {
        #region Properties

        public int Count { get; set; }

        public string CountText { get; set; }

        #endregion
    }
}
