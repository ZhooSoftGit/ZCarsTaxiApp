using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZCarsDriver.Services.Contracts;
using ZCarsDriver.UIModel;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class PeakHoursViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<string> _locations;

        public ObservableCollection<TimeSlot> TimeSlots { get; } = new();

        [ObservableProperty]
        private string _selectedLocation;

        [ObservableProperty]
        private DateTime _selectedDate;

        public ICommand RefreshCmd { get; }

        private IPeakHoursService _peakHoursService { get; }

        public PeakHoursViewModel()
        {
            PageTitleName = "Peak hours";
            RefreshCmd = new Command(async()=> await OnRefresh());
            _peakHoursService = ServiceHelper.GetService<IPeakHoursService>();
        }

        public async override void OnAppearing()
        {
            IsBusy = true;
            base.OnAppearing();
            await LoadLocations();
            await LoadTimeSlots();
            IsBusy = false;
        }

        private async Task LoadLocations()
        {
            Locations = new ObservableCollection<string> { "Erode", "Salem", "CBE" };
        }

        private async Task OnRefresh()
        {
            IsBusy = true;
            await LoadTimeSlots();
            IsBusy = false;
        }

        private async Task LoadTimeSlots()
        {
            var result = await _peakHoursService.GetAllPeakHoursAsync();

            TimeSlots.Clear();

            // Example static data – replace with API or database if needed
            TimeSlots.Add(new TimeSlot { FromTime = new TimeSpan(8, 30, 0), ToTime = new TimeSpan(12, 30, 0) });
            TimeSlots.Add(new TimeSlot { FromTime = new TimeSpan(17, 0, 0), ToTime = new TimeSpan(22, 30, 0) });
        }
    }
}
