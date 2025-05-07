using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZCarsDriver.Views.Driver;
using ZCarsDriver.Views.Vendor;
using ZhooCars.Common;
using ZhooCars.Model.DTOs;
using ZhooSoft.Auth.Model;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class VehicleListViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private ObservableCollection<VehicleDto> _filteredVehicles;

        [ObservableProperty]
        private ObservableCollection<VehicleDto> _vehicles;

        [ObservableProperty]
        private string searchText;


        [ObservableProperty]
        private bool _showRegistrationOption;

        #endregion

        #region Constructors

        public VehicleListViewModel()
        {
            PageTitleName = "Vehicle List";

            Vehicles = new ObservableCollection<VehicleDto>();
            FilteredVehicles = new ObservableCollection<VehicleDto>();

            SearchCommand = new RelayCommand(PerformSearch);
            EditVehicleCommand = new AsyncRelayCommand<VehicleDto>(EditVehicle);
            ViewVehicleCommand = new AsyncRelayCommand<VehicleDto>(ViewVehicle);
            DeleteVehicleCommand = new AsyncRelayCommand<VehicleDto>(DeleteVehicle);
            AddVehicleCommand = new AsyncRelayCommand(AddVehicle);
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand AddVehicleCommand { get; }

        public IAsyncRelayCommand<VehicleDto> DeleteVehicleCommand { get; }

        public IAsyncRelayCommand<VehicleDto> EditVehicleCommand { get; }

        public ICommand SearchCommand { get; }

        public IAsyncRelayCommand<VehicleDto> ViewVehicleCommand { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            base.OnAppearing();

            IsBusy = true;
            if (NavigationParams.ContainsKey("showregistrationoption"))
            {
                ShowRegistrationOption = true;
            }
            await LoadData();
            PerformSearch();
            IsBusy = false;
        }

        private async Task AddVehicle()
        {
            var param = new Dictionary<string, object>
                    {
                        { "regType",  RegsitrationType.VechicleDetails }
                    };
            await _navigationService.PushAsync(ServiceHelper.GetService<RegistrationBasePage>(), param);
        }

        private async Task DeleteVehicle(VehicleDto vehicle)
        {
            Vehicles.Remove(vehicle);
            PerformSearch();
        }

        private async Task EditVehicle(VehicleDto vehicle)
        {
            var param = new Dictionary<string, object>
                    {
                        { "regType",  RegsitrationType.VechicleDetails }
                    };
            await _navigationService.PushAsync(ServiceHelper.GetService<RegistrationBasePage>(), param);
        }

        private async Task LoadData()
        {
            //CAL API ad get vechicle list
            var vehicles = UIHelper.VehicleDtoSamples.GetSampleVehicles();


            if (ShowRegistrationOption)
            {
                foreach (var item in vehicles)
                {
                    item.ShowRegistrationOption = true;
                }
            }

            Vehicles = new ObservableCollection<VehicleDto>(vehicles);
        }

        private void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredVehicles = new ObservableCollection<VehicleDto>(Vehicles);
            }
            else
            {
                FilteredVehicles = new ObservableCollection<VehicleDto>(Vehicles.Where(v => v.VehicleRegistrationNumber.Contains(SearchText) || v.VehicleRegistrationNumber.Contains(SearchText)));
            }
        }

        private async Task ViewVehicle(VehicleDto vehicle)
        {
            if (ShowRegistrationOption)
            {
                var param = new Dictionary<string, object>
                    {
                        { "regType",  RegsitrationType.VechicleDetails }
                    };
                await _navigationService.PushAsync(ServiceHelper.GetService<RegistrationBasePage>(), param);
            }
            else
            {
                var param = new Dictionary<string, object>
                    {
                        { "selecteItem",  vehicle }
                    };
                await _navigationService.PushAsync(ServiceHelper.GetService<VehicleDetailsPage>(), param);
            }
        }

        #endregion
    }
}
