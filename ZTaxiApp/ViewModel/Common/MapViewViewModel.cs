using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxi.Model.Response;
using ZTaxi.Services.Contracts;
using ZTaxiApp.Helpers;
using ZTaxiApp.UIModel;

namespace ZTaxiApp.ViewModel
{
    public partial class MapViewViewModel : ViewModelBase
    {
        #region Fields

        public CustomMapView CurrentMap;

        [ObservableProperty]
        private string _buttonText;

        [ObservableProperty]
        private LocationInfo _currentLocation;

        private LocationInfo _locationInfo;

        [ObservableProperty]
        private MapSpan _mapRegion;

        [ObservableProperty]
        private string _pinLocationDetails;

        [ObservableProperty]
        private ObservableCollection<Pin> _pins;
        private Location _selectedLocation;

        #endregion

        #region Constructors

        public MapViewViewModel()
        {
            OnSelectAddress = new Command(SelectAddress);
            PageTitleName = "Select Pickup";
            ButtonText = "Select Pickup";
        }

        #endregion

        #region Properties

        public ICommand OnSelectAddress { get; }

        #endregion

        #region Methods

        public async override void OnAppearing()
        {
            base.OnAppearing();

            if (NavigationParams.ContainsKey("locationInfo") && NavigationParams["locationInfo"] is LocationInfo info)
            {
                _locationInfo = new LocationInfo { LocationType = info.LocationType };

                ButtonText = $"Select {info.LocationType.ToString()}";

                PageTitleName = ButtonText;
            }

            if (NavigationParams != null && NavigationParams.ContainsKey("selectedAddress"))
            {
                var address = NavigationParams["selectedAddress"] as SearchAddressResult;
                if (address != null)
                {
                    var loc = new Location(address.Geometry.Location.Lat, address.Geometry.Location.Lng);
                    await PinCurrentUserLocationOnMap(loc);
                }
            }
            else
            {
                await PinCurrentUserLocationOnMap();
            }

            NavigationParams?.Clear();
        }

        public async void PinPosUpdated(object obj)
        {
            if (obj is Location location)
            {
                _selectedLocation = location;
                PinLocationDetails = await GetAddressFromCoordinates(location.Latitude, location.Longitude);
            }
        }

        private async Task<string> GetAddressFromCoordinates(double latitude, double longitude)
        {
            try
            {
                var result = await ServiceHelper.GetService<IAddressService>().GetPlaceNameAsync(latitude, longitude);
                return result ?? "";
            }
            catch (Exception ex)
            {
                return "Unable get location info";
            }
        }

        private async Task PinCurrentUserLocationOnMap(Location? mapPosition = null)
        {
            try
            {
                if (mapPosition == null)
                {
                    mapPosition = await AppHelper.GetUserLocation();
                }

                if (mapPosition != null)
                {
                    var position = new Location(mapPosition.Latitude, mapPosition.Longitude);

                    CurrentMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5)));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location: {ex.Message}");
            }
        }

        private async void SelectAddress(object obj)
        {
            try
            {
                if (_selectedLocation != null)
                {
                    var selectedAddress = new LocationInfo
                    {
                        Address = PinLocationDetails,
                        Latitude = _selectedLocation.Latitude,
                        Longitude = _selectedLocation.Longitude,
                        LocationType = _locationInfo.LocationType
                    };
                    await _navigationService.PopAsync();
                    await Task.Delay(100);
                    var nvparam = new Dictionary<string, object> { { "selectedlocation", selectedAddress } };
                    await _navigationService.PopAsync(nvparam);
                }
            }
            catch(Exception ex)
            {

            }
        }

        #endregion
    }
}
