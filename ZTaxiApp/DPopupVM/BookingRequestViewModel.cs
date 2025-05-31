using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZhooSoft.Core;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxiApp.Common;
using ZTaxiApp.Helpers;
using ZTaxiApp.Services.Contracts;
using ZTaxiApp.UIModel;

namespace ZTaxiApp.DPopup
{
    public partial class BookingDetailsViewModel : ViewModelBase
    {

        #region Fields

        [ObservableProperty]
        private BookingRequestModel _bookingRequest;


        private System.Timers.Timer _timer;

        #endregion

        #region Constructors

        public BookingDetailsViewModel()
        {
            AcceptCommand = new AsyncRelayCommand(OnAccept);
            RejectCommand = new AsyncRelayCommand(OnReject);
            _taxiBookingService = ServiceHelper.GetService<ITaxiBookingService>();
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand AcceptCommand { get; }

        public Popup CurrentPopup { get; set; }

        public IAsyncRelayCommand RejectCommand { get; }

        private readonly ITaxiBookingService? _taxiBookingService;

        public int TimerValue { get; set; }

        #endregion

        #region Methods

        public override void OnNavigatedTo()
        {
            if (NavigationParams != null && NavigationParams.ContainsKey("BookingDetails"))
            {
                BookingRequest = NavigationParams["BookingDetails"] as BookingRequestModel;
            }
        }

        private async Task OnAccept()
        {
            await CurrentPopup.CloseAsync(true);
        }

        private async Task OnReject()
        {
            _timer.Dispose();
            // Perform logic for rejecting the ride
            Application.Current.MainPage.DisplayAlert("Info", "Ride Rejected", "OK");
            await CurrentPopup.CloseAsync(RideStatus.Cancelled);
        }

        #endregion
    }
}
