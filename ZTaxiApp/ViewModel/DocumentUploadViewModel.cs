using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZTaxiApp.UIModel;
using ZTaxiApp.Model.DTOs;
using ZhooSoft.Core;

namespace ZTaxiApp.ViewModel
{
    public partial class DocumentUploadViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty] private string _backFileName;

        [ObservableProperty] private string _backFileSize;

        [ObservableProperty] private ImageSource _backLicenseImage;

        [ObservableProperty] private bool _bothSide;

        private CheckListItem _checklistItem;

        [ObservableProperty] private string _frontFileName;

        [ObservableProperty] private string _frontFileSize;

        [ObservableProperty] private ImageSource _frontLicenseImage;

        [ObservableProperty] private string _instructions;

        [ObservableProperty] private bool _isBackUploaded;

        [ObservableProperty] private bool _isFrontUploaded;

        [ObservableProperty] private bool _isSaveEnabled;

        #endregion

        #region Constructors

        public DocumentUploadViewModel()
        {
            UploadFrontCommand = new AsyncRelayCommand<string>(UploadFrontAsync);
            UploadBackCommand = new AsyncRelayCommand<string>(UploadBackAsync);
            RemoveFrontCommand = new Command(RemoveFront);
            RemoveBackCommand = new Command(RemoveBack);
            SaveCommand = new AsyncRelayCommand(SaveLicense);
        }

        #endregion

        #region Properties

        public ICommand RemoveBackCommand { get; }

        public ICommand RemoveFrontCommand { get; }

        public IAsyncRelayCommand SaveCommand { get; }

        public IAsyncRelayCommand<string> UploadBackCommand { get; }

        public IAsyncRelayCommand<string> UploadFrontCommand { get; }

        #endregion

        #region Methods

        public override async void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (NavigationParams != null && NavigationParams["checklist"] is CheckListItem item)
            {
                _checklistItem = item;
                UpdatePageData();
                BothSide = !item.FrontOnly;
                await LoadData();
            }
            await Task.Delay(100);
            IsBusy = false;
        }

        private async Task LoadData()
        {
            if (_checklistItem.CheckListItemStatus != ZTaxiApp.Common.ActionType.New)
            {
                if (NavigationParams.ContainsKey("DocumentData") && NavigationParams["DocumentData"] is DocumentDto dto)
                {
                    await LoadFrontData(dto);
                    if (!_checklistItem.FrontOnly)
                    {
                        await LoadBackData(dto);
                    }
                }
            }
        }

        private void RemoveBack()
        {
            BackLicenseImage = null;
            BackFileName = null;
            BackFileSize = null;
            IsBackUploaded = false;
            UpdateSaveButtonState();
        }

        private void RemoveFront()
        {
            FrontLicenseImage = null;
            FrontFileName = null;
            FrontFileSize = null;
            IsFrontUploaded = false;
            UpdateSaveButtonState();
        }

        private async Task SaveLicense()
        {
            if (!IsSaveEnabled) return;
            _checklistItem.IsCompleted = true;
            await _navigationService.PopAsync();
        }

        private void UpdatePageData()
        {
            PageTitleName = _checklistItem.ItemName + " Document";
            Instructions = $"Upload clear pictures of {_checklistItem.ItemName}. \n Documents should be less than 5MB." +
                $"\n Documents should be JPEG, PNG, or PDF format";
        }

        private void UpdateSaveButtonState()
        {
            if (_checklistItem.FrontOnly)
            {
                IsSaveEnabled = IsFrontUploaded;
            }
            else
            {
                IsSaveEnabled = IsFrontUploaded && IsBackUploaded;
            }
        }

        private async Task UploadBackAsync(string obj)
        {
            if (obj == "Camera")
            {
                var result = await UIHelper.UIHelper.TakePhoto();
                if (result != null)
                {
                    BackLicenseImage = await UIHelper.UIHelper.GetImageSource(result) ?? BackLicenseImage;
                    BackFileName = result.FileName;
                    BackFileSize = UIHelper.UIHelper.GetFileSizeAsync(result).ToString() + " kb";
                    IsBackUploaded = true;
                    UpdateSaveButtonState();
                }
            }
            else
            {
                var result = await UIHelper.UIHelper.PickFile();
                if (result != null)
                {
                    BackLicenseImage = await UIHelper.UIHelper.GetImageSource(result) ?? BackLicenseImage;
                    BackFileName = result.FileName;
                    BackFileSize = UIHelper.UIHelper.GetFileSizeAsync(result).ToString() + " kb";
                    IsBackUploaded = true;
                    UpdateSaveButtonState();
                }
            }

        }

        private async Task LoadBackData(DocumentDto document)
        {
            BackLicenseImage = ImageSource.FromFile("license_dummy.png");
            BackFileName = document.DocumentUrl;
            BackFileSize = document.DocumentId + " kb";
            IsBackUploaded = true;
            UpdateSaveButtonState();
        }

        private async Task LoadFrontData(DocumentDto document)
        {
            FrontLicenseImage = ImageSource.FromFile("license_dummy.png");
            FrontFileName = document.DocumentUrl;
            FrontFileSize = document.DocumentId + " kb";
            IsBackUploaded = true;
            UpdateSaveButtonState();
        }

        private async Task UploadFrontAsync(string obj)
        {
            if (obj == "Camera")
            {
                var result = await UIHelper.UIHelper.TakePhoto();
                if (result != null)
                {
                    FrontLicenseImage = await UIHelper.UIHelper.GetImageSource(result) ?? FrontLicenseImage;
                    FrontFileName = result.FileName;
                    FrontFileSize = UIHelper.UIHelper.GetFileSizeAsync(result).ToString() + " kb";
                    IsFrontUploaded = true;
                    UpdateSaveButtonState();
                }
            }
            else
            {
                var result = await UIHelper.UIHelper.PickFile();
                if (result != null)
                {
                    FrontLicenseImage = await UIHelper.UIHelper.GetImageSource(result) ?? FrontLicenseImage;
                    FrontFileName = result.FileName;
                    FrontFileSize = UIHelper.UIHelper.GetFileSizeAsync(result).ToString() + " kb";
                    IsFrontUploaded = true;
                    UpdateSaveButtonState();
                }
            }
        }

        #endregion
    }
}
