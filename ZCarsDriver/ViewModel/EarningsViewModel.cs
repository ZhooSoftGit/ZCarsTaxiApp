using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class EarningsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Vehicle> _vehicles;

        [ObservableProperty]
        public ObservableCollection<Driver> _drivers;

        [ObservableProperty]
        private Vehicle selectedVehicle;

        [ObservableProperty]
        private Driver selectedDriver;

        [ObservableProperty]
        private DateTime fromDate = DateTime.Today.AddDays(-7);

        [ObservableProperty]
        private DateTime toDate = DateTime.Today;

        [ObservableProperty]
        private double _totalKilometers;

        [ObservableProperty]
        private TimeSpan _totalDrivingTime;

        [ObservableProperty]
        private double _totalEarnings;

        public ICommand RefreshEarningCmd { get; }

        public ICommand ExportReportCmd { get; }

        public EarningsViewModel()
        {
            PageTitleName = "Earnings";
            RefreshEarningCmd = new Command(async () => await RefreshEarnings());
            ExportReportCmd = new Command(async () => await ExportReports());
        }

        private async Task ExportReports()
        {
            IsBusy = true;
            // Export as report.
            IsBusy = false;
        }

        private async Task RefreshEarnings()
        {
            IsBusy = true;
            TotalDrivingTime = new TimeSpan(10, 1, 1);
            TotalEarnings = 300;
            TotalKilometers = 30;
            IsBusy = false;
        }
    }
}
