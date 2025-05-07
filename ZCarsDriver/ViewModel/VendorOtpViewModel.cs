using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Text.RegularExpressions;
using ZCarsDriver.Views.Driver;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class VendorOtpViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private bool _isPhoneValid;

        public IAsyncRelayCommand GetOtpCommand { get; }

        public VendorOtpViewModel()
        {
            PageTitleName = "Vendor OTP";
            GetOtpCommand = new AsyncRelayCommand(GetOtp);
        }

        private async Task GetOtp()
        {
            if (!ValidatePhoneNumber())
            {
                await _alertService.ShowAlert("Error", "Please enter a valid phone number.", "OK");
                return;
            }
            //TODO API OTP

            var nvparm = new Dictionary<string, object>()
                            {
                                {"phoneNumber", PhoneNumber },
                                {"sessionId", "" }
                            };
            await _navigationService.PushAsync(ServiceHelper.GetService<OtpVerificationPage>(), nvparm);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(PhoneNumber))
            {
                if (ValidatePhoneNumber())
                {
                    IsPhoneValid = true;
                }
                else
                {
                    IsPhoneValid = false;
                }
            }
        }

        private bool ValidatePhoneNumber()
        {
            if (PhoneNumber == null) return false;

            // Regex for validating an Indian phone number
            string pattern = @"^(\+91[-\s]?)?[7-9]{1}[0-9]{9}$";

            string phoneNumber = PhoneNumber;

            Regex regex = new Regex(pattern);

            // Return true if the phone number matches the regex pattern

            if (PhoneNumber == null)
            {
                return false;
            }
            if (regex.IsMatch(phoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
