using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZCarsDriver.Views.Rides;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class RideListViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DateTime _fromDate = DateTime.Today;

        [ObservableProperty]
        private DateTime _toDate = DateTime.Today;

        [ObservableProperty]
        private ObservableCollection<string> _cabOptions = new();

        [ObservableProperty]
        private string _selectedCab;

        [ObservableProperty]
        private ObservableCollection<RideModel> _rides = new();


        public IAsyncRelayCommand OnDateChanged { get; }
        public IAsyncRelayCommand OnRideDetailCmd { get; }

        public RideListViewModel()
        {
            
            PageTitleName = "Ride List";
            OnDateChanged = new AsyncRelayCommand(LoadRideList);
            OnRideDetailCmd = new AsyncRelayCommand<RideModel>(async(obj)=> await OpenRideDetails(obj));
        }

        private async Task OpenRideDetails(RideModel obj)
        {
            var param = new Dictionary<string, object>
            {
                {"selectedItem",  obj}
            };
            await _navigationService.PushAsync(ServiceHelper.GetService<RideDetailsPage>());
        }

        private async Task LoadRideList()
        {
            //API call
            IsBusy = true;
            await LoadRides();
            IsBusy = false;
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadCabData();
            await LoadRides();
        }



        private async Task LoadCabData()
        {
            CabOptions = new ObservableCollection<string> { "TN01AB1234", "TN02XY9876", "TN03PQ4567" };
            SelectedCab = CabOptions.FirstOrDefault();
        }

        private async Task LoadRides()
        {
            // Sample data; you should replace this with actual API/data fetching logic
            Rides = new ObservableCollection<RideModel>
            {
                new RideModel
                {
                    CabImage = "car_icon.png",
                    CabNumber = "TN01AB1234",
                    FromLocation = "Chennai",
                    ToLocation = "Bangalore",
                    Distance = "340 KM",
                    Fare = 4500,
                    DriverName = "John Doe",
                    DriverPhoto = "driver1.png"
                },
                new RideModel
                {
                    CabImage = "car_icon.png",
                    CabNumber = "TN02XY9876",
                    FromLocation = "Coimbatore",
                    ToLocation = "Salem",
                    Distance = "160 KM",
                    Fare = 2200,
                    DriverName = "Jane Smith",
                    DriverPhoto = "driver2.png"
                }
            };
        }
    }
}
