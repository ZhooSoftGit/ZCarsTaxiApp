using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZCarsDriver.Helpers;
using ZCarsDriver.UIModel;
using ZCarsDriver.Views.Driver;
using ZCarsDriver.Views.Vendor;
using ZhooCars.Common;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class BaseProfileViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _profileStatus = "Incomplete";

        [ObservableProperty]
        private bool _isVendor;

        [ObservableProperty]
        private bool _isDriver;

        [ObservableProperty]
        private string _ownerNumber;
        [ObservableProperty]
        private string _vehicleNo;

        [ObservableProperty]
        private string _applicationStatus = "Pending";
        private MobileModule _currentModule;

        public IAsyncRelayCommand ProfileTapCommand { get; }
        public IAsyncRelayCommand ApplicationTapCommand { get; }

        public IAsyncRelayCommand LaunchDashBoardCommand { get; }
        public IAsyncRelayCommand ResetDataCommand { get; }

        public IAsyncRelayCommand ContactSupportCommand { get; }

        public IAsyncRelayCommand VehiclesCommand { get; }

        public IAsyncRelayCommand DriverLinkCommand { get; }

        public ICommand LogoutCommand { get; }

        public BaseProfileViewModel()
        {
            ProfileTapCommand = new AsyncRelayCommand(OnProfileTapped);
            ApplicationTapCommand = new AsyncRelayCommand(OnApplicationTapped);
            DriverLinkCommand = new AsyncRelayCommand(OnLinkDriver);
            ResetDataCommand = new AsyncRelayCommand(OnResetData);
            LaunchDashBoardCommand = new AsyncRelayCommand(OnLaunchDashBoard);
            ContactSupportCommand = new AsyncRelayCommand(OnContactSupport);
            VehiclesCommand = new AsyncRelayCommand(OnVehiclesCmd);
            LogoutCommand = new RelayCommand(OnLogout);
        }

        private async Task OnLinkDriver()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<LinkDriverPage>());
        }

        private async Task OnVehiclesCmd()
        {
            var nvparam = new Dictionary<string, object>
                    {
                        {"showregistrationoption",true }
                    };
            await _navigationService.PushAsync(ServiceHelper.GetService<VehicleListPage>(), nvparam);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (AppHelper.CurrentModule is MobileModule module)
            {
                _currentModule = module;

                PageTitleName = module.ToString() + " Registration";

                if (_currentModule == MobileModule.Vendor)
                {
                    IsVendor = true;
                }
                if (_currentModule == MobileModule.Driver)
                {
                    IsDriver = true;
                    VehicleNo = "TN 38 DU 4090";
                    OwnerNumber = "ZCars";
                }
            }
        }

        private async Task OnLaunchDashBoard()
        {
            IsBusy = true;
            if(AppHelper.CurrentModule == MobileModule.Driver)
            {
                await _navigationService.PopToRootAsync();
                await _navigationService.PushAsync(ServiceHelper.GetService<DriverDashboardPage>());
            }
            else if(AppHelper.CurrentModule == MobileModule.Vendor)
            {
                //await _navigationService.PopToRootAsync();
                await _navigationService.PushAsync(ServiceHelper.GetService<DashboardPage>());
            }
            
            IsBusy = false;
        }

        private async Task OnResetData()
        {
            await _alertService.ShowAlert("info", "your data will reset", "Ok");
        }

        private async Task OnProfileTapped()
        {
            var param = new Dictionary<string, object>
                    {
                        {"checklist", new CheckListItem { ItemName = "Basic Details", IsCompleted = false, IsForm = true } }
                    };
            await _navigationService.PushAsync(ServiceHelper.GetService<CommonFormPage>(), param);
        }

        private async Task OnApplicationTapped()
        {
            var param = new Dictionary<string, object>
                    {
                        { "regType",  GetRegType() }
                    };
            await _navigationService.PushAsync(ServiceHelper.GetService<RegistrationBasePage>(), param);
        }

        private RegsitrationType GetRegType()
        {
            if (_currentModule == MobileModule.Driver)
            {
                return RegsitrationType.DriverApplication;
            }
            if (_currentModule == MobileModule.Vendor)
            {
                return RegsitrationType.VendorApplication;
            }
            if (_currentModule == MobileModule.ServiceProvider)
            {
                return RegsitrationType.ServiceProviderApplication;
            }
            if (_currentModule == MobileModule.SparParts)
            {
                return RegsitrationType.SparPartsApplication;
            }
            return RegsitrationType.BasicDetails;
        }

        private async Task OnContactSupport()
        {
            // Contact Support Logic
            Console.WriteLine("Contacting Support");
        }

        private void OnLogout()
        {
            // Logout Logic
            Console.WriteLine("Logging Out");
        }
    }
}
