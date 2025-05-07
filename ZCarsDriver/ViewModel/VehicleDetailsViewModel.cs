using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class VehicleDetailsViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private Driver _assignedDriver;

        [ObservableProperty]
        private Vehicle _vehicle;

        [ObservableProperty]
        private ObservableCollection<VehicleDetail> _vehicleDetails;

        [ObservableProperty]
        private ObservableCollection<VehiclePricing> _vehiclePricing;

        #endregion

        #region Constructors

        public VehicleDetailsViewModel()
        {
            CallDriverCommand = new Command(CallDriver);
            PageTitleName = "Vehicle Details";
        }

        #endregion

        #region Properties

        public ICommand CallDriverCommand { get; }

        #endregion

        #region Methods

        public override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        private void CallDriver()
        {
            if (!string.IsNullOrWhiteSpace(AssignedDriver?.PhoneNumber))
            {
                // Your call logic here
                // For example: Launcher.Default.OpenAsync($"tel:{AssignedDriver.PhoneNumber}");
            }
        }

        private void LoadData()
        {
            Vehicle = new Vehicle
            {
                RegistrationNumber = "TN37DF5483",
                AssignmentStatus = "Assigned",
                CurrentLocation = "12.9716° N, 77.5946° E"
            };

            VehicleDetails = new ObservableCollection<VehicleDetail>
                {
                    new VehicleDetail { Icon="car_icon.png", Value="Toyota Innova - 2019" },
                    new VehicleDetail { Icon="fuel.png", Value="Diesel - 15 KM/L" },
                    new VehicleDetail { Icon="gear.png", Value="Automatic Transmission" },
                    new VehicleDetail { Icon="seat.png", Value="Seating Capacity: 6" }
                };

            VehiclePricing = new ObservableCollection<VehiclePricing>
                {
                    new VehiclePricing { Key="Base Fare", Value="₹50" },
                    new VehiclePricing { Key="Per KM", Value="₹12" },
                    new VehiclePricing { Key="Waiting Charge", Value="₹2/min" }
                };

            AssignedDriver = new Driver
            {
                Name = "Annai Raj R",
                PhoneNumber = "+91 9876543210",
                Photo = "driver_profile.png"
            };
        }

        #endregion
    }
}
