using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZTaxiApp.DPopup;
using ZTaxiApp.Helpers;
using ZhooSoft.Core;

namespace ZTaxiApp.DPopupVM
{
    public partial class TripOptionViewModel : ViewModelBase
    {
        internal TripOptionPopup CurrentPopup;

        #region Constructors

        public TripOptionViewModel()
        {
            TripDetailsCommand = new AsyncRelayCommand(ShowTripDetails);
            CallCustomerCommand = new AsyncRelayCommand(CallCustomer);
            CancelTripCommand = new AsyncRelayCommand(CancelTrip);
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand CallCustomerCommand { get; }

        public IAsyncRelayCommand CancelTripCommand { get; }

        public IAsyncRelayCommand TripDetailsCommand { get; }

        #endregion

        #region Methods

        private async Task CallCustomer()
        {
            await CurrentPopup.CloseAsync();
        }

        private async Task CancelTrip()
        {
            await CurrentPopup.CloseAsync();
        }

        private async Task ShowTripDetails()
        {
            await CurrentPopup.CloseAsync();
        }

        #endregion
    }
}
