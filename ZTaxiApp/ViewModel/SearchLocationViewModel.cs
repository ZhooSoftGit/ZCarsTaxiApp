
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxi.Model.Response;
using ZTaxi.Services.Contracts;
using ZTaxiApp.UIModel;
using ZTaxiApp.Views.Common;

namespace ZTaxiApp.ViewModel
{
    public partial class SearchLocationViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _locationType;

        public ICommand MapviewClicked { get; }
        public ICommand SearchCommand { get; }

        public ICommand OnSelectLocationCmd { get; }

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private bool _showSearchResult;

        [ObservableProperty]
        private ObservableCollection<SearchAddressResult> searchLocations;
        private LocationInfo _locationinfo;

        private IAddressService _addressService { get; }


        public SearchLocationViewModel()
        {
            PageTitleName = "Select Location";

            MapviewClicked = new Command(Onmapview);
            SearchCommand = new Command(OnSearchItem);
            OnSelectLocationCmd = new Command(OnSelectLocation);
            _addressService = ServiceHelper.GetService<IAddressService>();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (NavigationParams != null && NavigationParams.Count > 0)
            {
                if (NavigationParams.ContainsKey("locationInfo") && NavigationParams["locationInfo"] is LocationInfo info)
                {
                    _locationinfo = info;
                    LocationType = "Select " + info.LocationType.ToString() + " Location";
                }
            }
        }

        private async void OnSelectLocation(object obj)
        {
            if (_locationinfo != null && obj is SearchAddressResult result)
            {
                var vm = new Dictionary<string, object>();
                var selectedAddress = new LocationInfo
                {
                    Address = result.FormattedAddress,
                    Latitude = result.Geometry.Location.Lat,
                    Longitude = result.Geometry.Location.Lng,
                    LocationType = _locationinfo.LocationType
                };

                var nvparam = new Dictionary<string, object> { { "selectedlocation", selectedAddress } };
                await _navigationService.PopAsync(nvparam);
            }
        }

        private void Onmapview(object obj)
        {
            var vm = new Dictionary<string, object>();
            vm.Add("locationInfo", _locationinfo);
            _navigationService.PushAsync(ServiceHelper.GetService<MapViewPage>(), vm);
        }

        private void OnSearchItem(object obj)
        {
            if (SearchText?.Length > 2)
            {
                LoadItems(SearchText);
            }
            else
            {
                ShowSearchResult = false;
            }
        }

        private async void LoadItems(string searchText)
        {
            var result = await _addressService.GetAddressFromSearchAsync(searchText);
            ShowSearchResult = true;
            SearchLocations = new ObservableCollection<SearchAddressResult>(result);
        }
    }
}
