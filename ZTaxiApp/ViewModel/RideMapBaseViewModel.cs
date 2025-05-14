using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxiApp.Services.AppService;
using ZTaxiApp.Services.Contracts;
using ZTaxiApp.UIModel;
using ZTaxiApp.Views.Common;

namespace ZTaxiApp.ViewModel
{
    public partial class RideMapBaseViewModel : ViewModelBase
    {
        #region Fields

        public CustomMapView CurrentMap;

        [ObservableProperty]
        private LocationInfo _pickupLocation;

        [ObservableProperty]
        private LocationInfo _dropLocation;

        [ObservableProperty]
        private bool _showCabOptions;

        [ObservableProperty]
        private ObservableCollection<RideOption> _rideOptions = new();

        public ICommand SelectPickupLocationCommand { get; }
        public ICommand SelectDropLocationCommand { get; }


        private IOsrmService _osrmService;
        private ICallService? _callService;
        private IRideTripService? _rideTripService;
        private ITaxiBookingService? _taxiService;

        public IRelayCommand OpenSideBarCmd { get; }

        #endregion

        #region Constructors

        public RideMapBaseViewModel()
        {
            OpenSideBarCmd = new AsyncRelayCommand(OpenSideBar);

            PickupLocation = new LocationInfo();
            DropLocation = new LocationInfo();

            SelectPickupLocationCommand = new RelayCommand(OnSelectPickupLocation);
            SelectDropLocationCommand = new RelayCommand(OnSelectDropLocation);

            LoadRideOptions();

            InitializeService();
        }

        private void OnSelectPickupLocation()
        {
            // Simulate a location selection
            PickupLocation = new LocationInfo { Address = "123 Main Street" };
            EvaluateRideOptionsVisibility();
        }

        private void OnSelectDropLocation()
        {
            DropLocation = new LocationInfo { Address = "456 Elm Street" };
            EvaluateRideOptionsVisibility();
        }

        private void EvaluateRideOptionsVisibility()
        {
            ShowCabOptions = !string.IsNullOrWhiteSpace(PickupLocation?.Address)
                          && !string.IsNullOrWhiteSpace(DropLocation?.Address);
        }

        private void LoadRideOptions()
        {
            RideOptions.Add(new RideOption { Name = "Mini", Price = "$10", Icon = "cab.png" });
            RideOptions.Add(new RideOption { Name = "Sedan", Price = "$15", Icon = "cab.png" });
            RideOptions.Add(new RideOption { Name = "SUV", Price = "$20", Icon = "cab.png" });
        }

        private async Task OpenSideBar()
        {
            IsBusy = true;
            var result = await _navigationService.OpenPopup(new CommonMenu());

            await Task.Delay(100);
            IsBusy = false;
        }

        private void InitializeService()
        {
            _osrmService = ServiceHelper.GetService<IOsrmService>();
            _callService = ServiceHelper.GetService<ICallService>();
            _rideTripService = ServiceHelper.GetService<IRideTripService>();
            _taxiService = ServiceHelper.GetService<ITaxiBookingService>();
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public async override void OnAppearing()
        {
            base.OnAppearing();
            IsLoaded = true;
        }

        internal async Task RefreshPage()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
