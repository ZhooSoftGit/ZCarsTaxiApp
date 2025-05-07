using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZhooSoft.Core.Alerts;
using ZhooSoft.Core.NavigationBase;

namespace ZhooSoft.Core
{
    public partial class ViewModelBase : ObservableObject
    {
        #region Fields

        private bool _isBusy;

        [ObservableProperty]
        private string _pageTitleName;



        #endregion

        #region Constructors

        public ViewModelBase()
        {
            _navigationService = ServiceHelper.GetService<INavigationService>();
            _alertService = ServiceHelper.GetService<IAlertService>();
            BackCommand = new AsyncRelayCommand(GoBack);
        }

        #endregion

        #region Properties

        public IAlertService _alertService { get; set; }

        public INavigationService _navigationService { get; set; }

        public IAsyncRelayCommand BackCommand { get; }

        

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                ShowProgress(_isBusy);

            }
        }

        public bool IsLoaded { get; set; }

        public Dictionary<string, object> NavigationParams { get; set; } = new Dictionary<string, object>();

        public ICommand OpenFlyout { get; }

        #endregion

        #region Methods

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        protected void ShowProgress(bool show)
        {
            if (show)
            {
                ServiceHelper.GetService<IProgressService>().ShowProgress("Please wait");
            }
            else
            {
                ServiceHelper.GetService<IProgressService>().HideProgress();
            }
        }

        private async Task GoBack()
        {
            await _navigationService.PopAsync();
        }

        public virtual void OnNavigatedTo()
        {

        }

        #endregion
    }
}
