using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZCarsDriver.Helpers;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class CancelTripViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private ObservableCollection<CancelReason> _cancelReasons;

        [ObservableProperty]
        private CancelReason _selectedReason;

        [ObservableProperty]
        private string _reasonTxt;


        public IRelayCommand SelectReasonCommand { get; }
        #endregion

        #region Constructors

        public CancelTripViewModel()
        {
            Initiate();
            SubmitCommand = new AsyncRelayCommand(Submit);
            CloseCommand = new AsyncRelayCommand(Close);
            SelectReasonCommand = new RelayCommand<CancelReason>((obj) => OnSelect(obj));
            PageTitleName = "Cancel Trip";
        }

        private void OnSelect(CancelReason reason)
        {
            CancelReasons.Select(x => x.IsSelected = false);
            reason.IsSelected = true;
            SelectedReason = reason;
        }

        private void Initiate()
        {
            var cancelreasons = new List<CancelReason>
            {
                new CancelReason{ Reason = "No response", IsSelected = false},
                new CancelReason{ Reason = "Request to cancel", IsSelected = false},
                new CancelReason{ Reason = "Time change", IsSelected = false},
                new CancelReason{ Reason = "Others", IsSelected = false},
            };
            CancelReasons = new ObservableCollection<CancelReason>(cancelreasons);
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand CloseCommand { get; }

        public IAsyncRelayCommand SubmitCommand { get; }

        #endregion

        #region Methods

        private async Task Close()
        {
            Console.WriteLine("Popup closed");
            await _navigationService.PopAsync();
        }

        private async Task Submit()
        {
            if (SelectedReason == null)
            {
                Console.WriteLine("Please select a reason before submitting.");
                return;
            }
            Console.WriteLine($"Selected reason: {SelectedReason}");
            //call the API
            AppHelper.CurrentRide = null;
            await _navigationService.PopAsync();
        }

        #endregion
    }


    public partial class CancelReason : ObservableObject
    {
        public string Reason { get; set; }

        [ObservableProperty]
        private bool _isSelected;
    }
}
