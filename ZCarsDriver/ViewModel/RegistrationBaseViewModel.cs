using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZCarsDriver.UIModel;
using ZCarsDriver.Views.Driver;
using ZhooCars.Common;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class RegistrationBaseViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private ObservableCollection<CheckListItem> _checkListItems;

        [ObservableProperty]
        private bool _isSubmitEnabled;

        private UserRoles _userRole;
        private RegsitrationType _registrationType;
        private object _selectedObj;

        #endregion

        #region Constructors

        public RegistrationBaseViewModel()
        {
            CheckListCmd = new AsyncRelayCommand<CheckListItem>(ToggleItemStatus);
            SubmitApplicationCommand = new RelayCommand(SubmitApplication, CanSubmit);
            PageTitleName = "Registration";
        }

        #endregion

        #region Properties

        public IAsyncRelayCommand CheckListCmd { get; }

        public ICommand SubmitApplicationCommand { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (NavigationParams.Count > 0)
            {
                if (NavigationParams.ContainsKey("regType") && NavigationParams["regType"] is RegsitrationType regType)
                {
                    _registrationType = regType;
                }

                if (NavigationParams.ContainsKey("selectedObj"))
                {
                    _selectedObj = NavigationParams["selectedObj"];
                }

                NavigationParams.Clear();
            }
            LoadCheckList();
            await Task.Delay(100);
            IsBusy = false;
        }

        private bool CanSubmit() => IsSubmitEnabled;

        private void LoadCheckList()
        {
            if (CheckListItems == null)
            {
                var checklist = new List<CheckListItem>();

                switch (_registrationType)
                {
                    case RegsitrationType.DriverApplication:
                        checklist.Add(new CheckListItem { ItemName = "Profile Photo", IsCompleted = false, IsDocument = true, FrontOnly = true, CheckListType = UIHelper.CheckListType.ProfilePhoto });
                        checklist.Add(new CheckListItem { ItemName = "Aadhaar", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.AadharCard });
                        checklist.Add(new CheckListItem { ItemName = "Driving License", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.DrivingLicense });
                        break;

                    case RegsitrationType.VendorApplication:
                        checklist.Add(new CheckListItem { ItemName = "Profile Photo", IsCompleted = false, IsDocument = true, FrontOnly = true, CheckListType = UIHelper.CheckListType.ProfilePhoto });
                        checklist.Add(new CheckListItem { ItemName = "Aadhaar", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.AadharCard });
                        //checklist.Add(new CheckListItem { ItemName = "Vehicle Details", IsCompleted = false, CheckListType = UIHelper.CheckListType.VehicleDetails, IsForm = true });
                        break;

                    case RegsitrationType.VechicleDetails:
                        checklist.Add(new CheckListItem { ItemName = "Vehicle Details", IsCompleted = false, CheckListType = UIHelper.CheckListType.VehicleDetails, IsForm = true });
                        checklist.Add(new CheckListItem { ItemName = "Rc Book", IsCompleted = false, IsDocument = true, CheckListType = UIHelper.CheckListType.RCBook });
                        checklist.Add(new CheckListItem { ItemName = "Insurance Document", IsCompleted = false, IsOptional = true, IsDocument = true, CheckListType = UIHelper.CheckListType.Insurance });
                        break;

                    case RegsitrationType.ServiceProviderApplication:
                        checklist.Add(new CheckListItem { ItemName = "Service Details", IsCompleted = false, CheckListType = UIHelper.CheckListType.ServiceStationDetails, IsForm = true });
                        checklist.Add(new CheckListItem { ItemName = "GST Document", IsCompleted = false, FrontOnly = true, IsDocument = true, CheckListType = UIHelper.CheckListType.GSTDocument });
                        break;

                    case RegsitrationType.SparPartsApplication:
                        checklist.Add(new CheckListItem { ItemName = "Shop Details", IsCompleted = false, CheckListType = UIHelper.CheckListType.SpartPartsShopDetails, IsForm = true });
                        checklist.Add(new CheckListItem { ItemName = "GST Document", IsCompleted = false, FrontOnly = true, IsDocument = true, CheckListType = UIHelper.CheckListType.GSTDocument });
                        break;

                    default:
                        break;
                }

                CheckListItems = new ObservableCollection<CheckListItem>(checklist);
            }

            UpdateSubmitButtonState();
        }

        private void SubmitApplication()
        {
        }

        private async Task ToggleItemStatus(CheckListItem item)
        {
            if (item != null)
            {
                if (item.IsDocument)
                {
                    var param = new Dictionary<string, object>
                    {
                        {"checklist", item }
                    };
                    await _navigationService.PushAsync(ServiceHelper.GetService<DocumentUploadPage>(), param);
                }

                if (item.IsForm)
                {
                    var param = new Dictionary<string, object>
                    {
                        {"checklist", item },
                        {"data", _selectedObj }
                    };

                    await _navigationService.PushAsync(ServiceHelper.GetService<CommonFormPage>(), param);
                }

                UpdateSubmitButtonState();
            }
        }

        private void UpdateSubmitButtonState()
        {
            IsSubmitEnabled = CheckListItems.All(item => item.IsCompleted || item.IsOptional);
        }

        #endregion
    }
}
