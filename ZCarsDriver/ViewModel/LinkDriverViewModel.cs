using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZCarsDriver.Views.Driver;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class LinkDriverViewModel :ViewModelBase
    {
        private List<Cab> AvailableCabs;

        [ObservableProperty]
        private ObservableCollection<Cab> filteredCabs;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private bool hasRcBook;

        [ObservableProperty]
        private Cab _selectedCab;

        [ObservableProperty]
        private bool _canContinue;

        public ICommand SelectCabCommand { get; }
        public IAsyncRelayCommand ContinueCommand { get; }
        public IAsyncRelayCommand ChangeVendorCommand { get; }
        public ICommand SearchCommand { get; set; }

        public LinkDriverViewModel()
        {
            SelectCabCommand = new RelayCommand<Cab>(SelectCab);
            ContinueCommand = new AsyncRelayCommand(OnContinue);
            ChangeVendorCommand = new AsyncRelayCommand(OnChangeVendor);
            SearchCommand = new RelayCommand(FilterCabs);
            PageTitleName = "Link Vehicle";
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            AvailableCabs = new List<Cab>
            {
                new Cab { RegistrationNumber = "TN00000000", Model = "White Aspire" },
                new Cab { RegistrationNumber = "KA12345678", Model = "Black Sedan" },
                new Cab { RegistrationNumber = "MH87654321", Model = "Silver SUV" },
            };

            FilteredCabs = new ObservableCollection<Cab>(AvailableCabs);
        }



        private void SelectCab(Cab cab)
        {
            if (cab == null) return;

            foreach (var c in FilteredCabs)
                c.IsSelected = false;

            SelectedCab = cab;

            SelectedCab.IsSelected = true;

            CanContinue = true;
        }

        private async Task OnContinue()
        {
            if (SelectedCab != null)
            {
                await _navigationService.PushAsync(ServiceHelper.GetService<DriverDashboardPage>());
            }
        }

        private async Task OnChangeVendor()
        {
            IsBusy = true;
            await _navigationService.PushAsync(ServiceHelper.GetService<VendorOtpPage>());
            IsBusy = false;
        }

        private void FilterCabs()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredCabs = new ObservableCollection<Cab>(AvailableCabs);
            }
            else
            {
                var filtered = AvailableCabs.Where(c => c.RegistrationNumber.Contains(SearchText) ||
                                               c.Model.Contains(SearchText)).ToList();
                FilteredCabs = new ObservableCollection<Cab>(filtered);
            }
            OnPropertyChanged(nameof(FilteredCabs));
        }
    }
}
