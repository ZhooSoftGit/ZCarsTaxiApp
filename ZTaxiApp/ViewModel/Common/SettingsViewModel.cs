using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Auth;
using ZhooSoft.Core;
using ZTaxiApp.CoreHelper;
using ZTaxiApp.Services.Session;
using ZTaxiApp.Views.Common;

namespace ZTaxiApp.ViewModel
{
    public partial class SettingsViewModel : ViewModelBase
    {
        public ICommand MenuCommand { get; }

        public SettingsViewModel()
        {
            MenuCommand = new Command<string>(OnMenuSelected);
        }


        private async void OnMenuSelected(string action)
        {
            switch (action)
            {
                case "Profile":
                    await GoToProfile();
                    break;


                case "About":
                    await ShowAboutDialog();
                    break;

                case "Logout":
                    await LogoutAsync();
                    break;

                case "DeleteAccount":
                    await DeleteAccountAsync();
                    break;

                default:
                    // optional default handling
                    break;
            }

        }

        private async Task ShowAboutDialog()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<AboutPage>());
        }

        private async Task GoToProfile()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<ProfilePage>());
        }
        private async Task LogoutAsync()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Logout",
                "Do you really want to logout?",
                "Logout", "Cancel"
            );

            if (confirm)
            {
                IsBusy = true;
                ServiceHelper.GetService<IUserSessionManager>().ClearSession();
                IsBusy = false;
                ServiceHelper.GetService<IMainAppNavigation>().OnLogout();
            }
        }

        private async Task DeleteAccountAsync()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Delete Account",
                "This action cannot be undone. Delete your account?",
                "Delete", "Cancel"
            );

            if (confirm)
            {
                // Implement delete logic
                IsBusy = true;
                ServiceHelper.GetService<UserSessionManager>().ClearSession();
                IsBusy = false;
                ServiceHelper.GetService<MainAppNavigation>().OnLogout();
            }
        }

    }
}
