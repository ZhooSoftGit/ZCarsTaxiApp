using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using ZCarsDriver.DPopup;
using ZCarsDriver.Helpers;
using ZCarsDriver.Services.Contracts;
using ZCarsDriver.Services.Session;
using ZCarsDriver.Views.Driver;
using ZhooCars.Common;
using ZhooSoft.Auth.Model;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class HomeViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private string userName = "User Name";

        [ObservableProperty]
        private string userRole = "User Role";

        #endregion

        #region Constructors

        public HomeViewModel()
        {
            ShowRideHistoryCommand = new RelayCommand(ShowRideHistory);
            ShowPaymentCommand = new RelayCommand(ShowPayment);
            OpenNotificationCommand = new RelayCommand(OpenNotification);
            OpenReferFriendCommand = new RelayCommand(OpenReferFriend);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            LogoutCommand = new RelayCommand(Logout);
            TileClickCmd = new RelayCommand<string>(OnTileClicked);

            _userService = ServiceHelper.GetService<IUserService>();
        }

        #endregion

        #region Properties

        public ICommand LogoutCommand { get; }

        public ICommand OpenNotificationCommand { get; }

        public ICommand OpenReferFriendCommand { get; }

        public ICommand OpenSettingsCommand { get; }

        public ICommand ShowPaymentCommand { get; }

        public ICommand ShowRideHistoryCommand { get; }

        public ICommand TileClickCmd { get; }

        private readonly IUserService _userService;
        private readonly IUserSessionManager _sessionManager;

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            IsBusy = true;
            base.OnAppearing();
            await LoadUserdata();
            IsBusy = false;
        }

        private async Task LoadUserdata()
        {
            var userdata = await _userService.GetUserDetailsAsync();

            if (userdata.IsSuccess && userdata.Data != null)
            {
                userName = userdata.Data.FirstName;
            }
        }

        private void Logout()
        {
        }

        private async void OnTileClicked(string option)
        {
            if (string.IsNullOrEmpty(option))
                return;
            option = option.Replace(" ", "");
            var nvparam = new Dictionary<string, object>();
            var userroles = UserDetails.getInstance().UserRoles;
            switch (option)
            {
                case "Driver":
                    AppHelper.CurrentModule = MobileModule.Driver;
                    if (userroles.Contains(UserRoles.Driver))
                    {
                        await _navigationService.PushAsync(ServiceHelper.GetService<DriverDashboardPage>());
                    }
                    else
                    {
                        var page2 = ServiceHelper.GetService<BaseProfilePage>();
                        await _navigationService.PushAsync(ServiceHelper.GetService<BaseProfilePage>());
                    }
                    break;

                case "Vendor":
                    AppHelper.CurrentModule = MobileModule.Vendor;
                    await _navigationService.PushAsync(ServiceHelper.GetService<BaseProfilePage>());
                    break;

                case "ServiceProvider":
                    AppHelper.CurrentModule = MobileModule.ServiceProvider;
                    await _navigationService.PushAsync(ServiceHelper.GetService<BaseProfilePage>());
                    break;

                case "SparParts":
                    AppHelper.CurrentModule = MobileModule.SparParts;
                    await _navigationService.PushAsync(ServiceHelper.GetService<BaseProfilePage>());
                    break;

                case "BuyAndSell":
                    AppHelper.CurrentModule = MobileModule.BuyAndSell;
                    var page = ServiceHelper.GetService<RegistrationBasePage>();
                    nvparam = new Dictionary<string, object>
                    {
                        {"Tile", UserRoles.BuyAndSell }
                    };
                    await _navigationService.PushAsync(page, nvparam);
                    break;

                default:
                    // Handle unknown case
                    break;
            }
        }

        private void OpenNotification()
        {
        }

        private void OpenReferFriend()
        {
        }

        private void OpenSettings()
        {
        }

        private void ShowPayment()
        {
        }

        private void ShowRideHistory()
        {
        }

        #endregion
    }
}
