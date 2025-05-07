using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZCarsDriver.DPopup;
using ZhooSoft.Core;

namespace ZCarsDriver.DPopupVM
{
    public partial class OnStartOtpViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _otp1;
        [ObservableProperty]
        private string _otp2;
        [ObservableProperty]
        private string _otp3;
        [ObservableProperty]
        private string _otp4;

        [ObservableProperty]
        private bool _showEndTrip;

        [ObservableProperty]
        private bool _showOtp = true;

        [ObservableProperty]
        private bool _showRideSuccess;

        public OnStartOtpPopup CurrentPopup { get; internal set; }

        public IAsyncRelayCommand VerifyCommand { get; }
        public IAsyncRelayCommand CancelCommand { get; }

        public IAsyncRelayCommand EndTripCommand { get; }

        public OnStartOtpViewModel()
        {
            VerifyCommand = new AsyncRelayCommand(VerifyOtp);
            CancelCommand = new AsyncRelayCommand(CancelOtp);
            EndTripCommand = new AsyncRelayCommand(OnEndTrip);
        }


        private async Task OnEndTrip()
        {
            await CurrentPopup.CloseAsync(true);
        }

        private async Task VerifyOtp()
        {
            string enteredOtp = $"{Otp1}{Otp2}{Otp3}{Otp4}";

            await CurrentPopup.CloseAsync(enteredOtp);

        }

        private async Task CancelOtp()
        {
            await CurrentPopup.CloseAsync();
        }
    }
}
