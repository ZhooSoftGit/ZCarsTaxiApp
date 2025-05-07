using System.Windows.Input;
using ZhooSoft.Auth;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel.Common
{
    public class EnableLocationViewModel : ViewModelBase
    {
        #region Constructors

        public EnableLocationViewModel()
        {
            EnableLocationCommand = new Command(async () => await RequestLocationPermission());
        }

        #endregion

        #region Properties

        public ICommand EnableLocationCommand { get; }

        public ICommand SkipCommand { get; }

        #endregion

        #region Methods

        private async Task RequestLocationPermission()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain(true);
            }
            else
            {
                await Shell.Current.DisplayAlert("Permission Denied", "Please enable location in settings.", "OK");
            }
        }

        #endregion
    }
}
