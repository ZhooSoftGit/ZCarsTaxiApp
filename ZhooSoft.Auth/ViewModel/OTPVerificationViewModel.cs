using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZCarsDriver.Services.Session;
using ZhooCars.Common;
using ZhooCars.Model.DTOs;
using ZhooCars.Services;
using ZhooSoft.Auth.Model;
using ZhooSoft.Core;

namespace ZhooSoft.Auth.ViewModel
{
    public partial class OTPVerificationViewModel : ViewModelBase
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
        private string _timerText;

        [ObservableProperty]
        private bool showError;

        [ObservableProperty]
        private string _phoneNumber;

        [ObservableProperty]
        private bool _isTimerVisible = true;

        [ObservableProperty]
        private bool _isResendVisible;

        private int _secondsRemaining;

        public OTPVerificationViewModel(IAccountService accountService, IUserSessionManager userSessionManager)
        {
            _secondsRemaining = 60; // Set initial countdown time (1:30)
            StartTimer();
            SubmitCommand = new AsyncRelayCommand(OnSubmit);
            ResendCodeCommand = new AsyncRelayCommand(OnResendCode);
            ChangePhoneNumberCommand = new AsyncRelayCommand(OnChangePhoneNumber);
            _accountService = accountService;
            _userSessionManager = userSessionManager;
        }
        public IAsyncRelayCommand SubmitCommand { get; }
        public IAsyncRelayCommand ResendCodeCommand { get; }
        public IAsyncRelayCommand ChangePhoneNumberCommand { get; }

        private readonly IAccountService _accountService;
        private readonly IUserSessionManager _userSessionManager;

        private async void StartTimer()
        {
            while (_secondsRemaining > 0)
            {
                TimerText = TimeSpan.FromSeconds(_secondsRemaining).ToString("mm\\:ss");
                await Task.Delay(1000);
                _secondsRemaining--;
            }
            TimerText = "";
            IsTimerVisible = false;
            IsResendVisible = true;
        }

        private async Task OnSubmit()
        {
            string enteredOtp = Otp1.Trim() + Otp2.Trim() + Otp3.Trim() + Otp4.Trim();


            if (enteredOtp.Length == 4)
            {
                IsBusy = true;
                var result = await _accountService.VerifyOtpAsync(PhoneNumber, enteredOtp);
                IsBusy = false;
                if (result.IsSuccess)
                {
                    // Create a API to get session details

                    await _userSessionManager.SaveUserSessionAsync(new Core.Session.UserSession
                    {
                        Name = "Rajesh",
                        PhoneNumber = PhoneNumber,
                        RefreshToken = result.Data.TokenResponse.RefreshToken,
                        Token = result.Data.TokenResponse.Token,
                        Roles = new List<ZhooCars.Common.UserRoles> { ZhooCars.Common.UserRoles.User }
                    });


                    UserDetails.getInstance().Phone1 = PhoneNumber;
                    UserDetails.getInstance().UserRoles = new List<ZhooCars.Common.UserRoles> { ZhooCars.Common.UserRoles.User };
                    ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain(true);
                }
                else
                {
                    Otp1 = Otp2 = Otp3 = Otp4 = string.Empty;
                    ShowError = true;
                }
            }
            else
            {
                ShowError = true;
            }
        }

        private List<UserRoles> GetRoles(List<UserRoleDto> roles)
        {
            List<UserRoles> roless = new List<UserRoles>();
            foreach(var item in roles)
            {
                roless.Add(item.RoleId);
            }

            return roless;
        }

        private async Task OnResendCode()
        {
            Otp1 = string.Empty;
            Otp2 = string.Empty;
            Otp3 = string.Empty;
            Otp4 = string.Empty;
            _secondsRemaining = -1;
            IsResendVisible = false;
            IsTimerVisible = true;
            var result = await _accountService.ReSendOtpAsync(PhoneNumber);
            if (result.IsSuccess)
            {
                _secondsRemaining = 90;
                StartTimer();
            }
            else
            {
                await _alertService.ShowAlert("Error", "Otp Send is Failed", "Ok");
            }
        }

        private async Task OnChangePhoneNumber()
        {
            _secondsRemaining = -1;
            await _navigationService.PopAsync();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (NavigationParams != null)
            {
                var str = NavigationParams["phoneNumber"].ToString();
                PhoneNumber = str;
            }
        }
    }
}
