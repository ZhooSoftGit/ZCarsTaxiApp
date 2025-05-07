using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ZhooCars.Services;
using ZhooSoft.Auth.Resources.Strings;
using ZhooSoft.Auth.Views;
using ZhooSoft.Core;

namespace ZhooSoft.Auth.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        #region Fields

        private IAccountService _accountService;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private bool _isPhoneValid;

        [ObservableProperty]
        private string _phoneNumber;

        #endregion

        #region Constructors

        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            SendOtpCommand = new Command(async () => await OnSendOtp());
        }

        #endregion

        #region Properties

        public ICommand SendOtpCommand { get; }

        #endregion

        #region Methods

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(PhoneNumber))
            {
                if (ValidatePhoneNumber())
                {
                    IsPhoneValid = true;
                    ErrorMessage = string.Empty;
                }
                else
                {
                    IsPhoneValid = false;
                }
            }
        }

        private async Task OnSendOtp()
        {
            ErrorMessage = string.Empty;
            if (!ValidatePhoneNumber())
            {
                ErrorMessage = "Enter a valid phone number";
                return;
            }

            IsBusy = true;

            var result = await _accountService.SendOtpAsync(PhoneNumber);

            IsBusy = false;

            if (result.IsSuccess)
            {
                var nvparm = new Dictionary<string, object>()
                            {
                                {"phoneNumber", PhoneNumber },
                                {"sessionId", "" }
                            };
                await _navigationService.PushAsync(ServiceHelper.GetService<OTPVerificationPage>(), nvparm);
            }
            else
            {

                await ShowOtpError();
            }
        }

        private async Task ShowOtpError()
        {
            var isRetry = await _alertService.ShowConfirmation(AppResources.Error, AppResources.OtpFailedMsg, AppResources.Retry, AppResources.Cancel);
            if (isRetry)
            {
                await OnSendOtp();
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

        #endregion
    }
}
