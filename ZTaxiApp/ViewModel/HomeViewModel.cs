using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxiApp.Helpers;
using ZTaxiApp.Services.Contracts;
using ZTaxiApp.Services.Session;
using ZTaxiApp.UIModel;
using ZTaxiApp.Views.Driver;

namespace ZTaxiApp.ViewModel
{
    public partial class HomeViewModel : ViewModelBase
    {
        #region Fields

        private readonly IUserSessionManager _sessionManager;

        private readonly IUserService _userService;

        [ObservableProperty]
        private ObservableCollection<QuickAction> _bookingItems;

        [ObservableProperty]
        private ObservableCollection<QuickAction> _serviceItems;

        

        [ObservableProperty]
        private string userName = "User Name";

        [ObservableProperty]
        private string userRole = "User Role";

        #endregion

        #region Constructors

        public HomeViewModel()
        {
            _userService = ServiceHelper.GetService<IUserService>();
            OnActionCmd = new AsyncRelayCommand<QuickAction>(async (obj) => await OnAction(obj));
            LoadData();
        }

        #endregion

        #region Properties

        public ICommand LogoutCommand { get; }

        public IAsyncRelayCommand<QuickAction> OnActionCmd { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            IsBusy = true;
            base.OnAppearing();
            await LoadUserdata();
            IsBusy = false;
        }

        [ObservableProperty]
        private List<string> _bannerImages;

        private void LoadData()
        {
            var Items = new List<QuickAction>
            {
                new QuickAction { Name = "Cab", Icon = "cabbooking.png", Action = ActionEnum.Cab, ActionType = DashboardActionType.Ride },
                new QuickAction { Name = "Auto", Icon = "autobooking.png", Action = ActionEnum.Auto, ActionType = DashboardActionType.Ride },
                new QuickAction { Name = "Outstation", Icon = "outstation.png", Action = ActionEnum.OutStation, ActionType = DashboardActionType.Ride },
                new QuickAction { Name = "Rental", Icon = "rentalbooking.png", Action = ActionEnum.Rental, ActionType = DashboardActionType.Ride },
                new QuickAction { Name = "Request Driver", Icon = "driverbooking.png", Action = ActionEnum.RequestDriver, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "Request Service", Icon = "service.png", Action = ActionEnum.RequestService, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "Request Parts", Icon = "spar.png", Action = ActionEnum.RequestSparParts, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "Buy Vehicle", Icon = "buy.png", Action = ActionEnum.BuyVehicle, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "Sell Vehicle", Icon = "sell.png", Action = ActionEnum.SellVehicle, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "Insurance", Icon = "insurance.png", Action = ActionEnum.Insurance, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "Loan", Icon = "loan.png", Action = ActionEnum.Loan, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "FastTag", Icon = "fasttag.png", Action = ActionEnum.FastTag, ActionType = DashboardActionType.Service },
                new QuickAction { Name = "Emergency", Icon = "emergency.png", Action = ActionEnum.Emergency, ActionType = DashboardActionType.Service },
            };

            BannerImages = new List<string>
            {
                "banner1.png",
                "banner2.png",
                "banner3.png"
            };

            ////TODO Temp
            //foreach (var item in Items)
            //{
            //    item.Icon = "car_icon.png";
            //}

            BookingItems = new ObservableCollection<QuickAction>(Items.Where(x => x.ActionType == DashboardActionType.Ride));

            ServiceItems = new ObservableCollection<QuickAction>(Items.Where(x => x.ActionType != DashboardActionType.Ride));
        }

        private async Task LoadUserdata()
        {
            var userdata = await _userService.GetUserDetailsAsync();

            if (userdata.IsSuccess && userdata.Data != null)
            {
                userName = userdata.Data.FirstName;
            }
        }

        private async Task OnAction(QuickAction obj)
        {
            if (obj is QuickAction action)
            {
                if (action.Action == ActionEnum.Cab)
                {
                    await _navigationService.PushAsync(ServiceHelper.GetService<RideMapBasePage>());
                }
            }
        }

        #endregion
    }
}
