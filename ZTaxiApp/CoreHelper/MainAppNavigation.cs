using ZhooSoft.Auth;
using ZhooSoft.Auth.Model;
using ZhooSoft.Auth.Views;
using ZhooSoft.Core;
using ZhooSoft.Core.Alerts;
using ZTaxi.Core.Storage;
using ZTaxiApp.Helpers;
using ZTaxiApp.PlatformHelper;
using ZTaxiApp.Services.Session;
using ZTaxiApp.Views;
using ZTaxiApp.Views.Common;
using ZTaxiApp.Views.Driver;

namespace ZTaxiApp.CoreHelper
{
    public class MainAppNavigation : IMainAppNavigation
    {
        private readonly IUserSessionManager? _userSession;
        #region Methods

        public MainAppNavigation()
        {
            _userSession = ServiceHelper.GetService<IUserSessionManager>();
        }

        public void NavigateToDetail(NavigationPage detailPage)
        {
            throw new NotImplementedException();
        }

        public async void NavigateToMain(bool IsInitialLoad = false)
        {
            if (IsInitialLoad)
            {
                var session = await GetSessionDetails();

                if (!session)
                {
                    Application.Current.Windows[0].Page = new NavigationPage(new LoginPage());
                    return;
                }

                if (await CheckPermission())
                {
                    if (await CheckOpenRide())
                    {
                        Application.Current.MainPage = new NavigationPage(new RideMapBasePage());
                    }
                    else
                    {
                        Application.Current.Windows[0].Page = new NavigationPage(new HomeViewPage());
                    }
                }
                else
                {
                    Application.Current.Windows[0].Page = new EnableLocationPage();
                }
            }
            else
            {
                Application.Current.Windows[0].Page = new NavigationPage(new HomeViewPage());
            }
        }

        public void OnLogout()
        {
            Application.Current.Windows[0].Page = new NavigationPage(new LoginPage());
        }

        private async Task<bool> CheckOpenRide()
        {

            //For testing
            //AppHelper.CurrentRide = null;
            //return true;

            var rideInfo = AppHelper.CurrentRide;
            if (rideInfo != null)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> GetSessionDetails()
        {
            var session = await _userSession.GetUserSessionAsync();
            if (session != null)
            {
                UserDetails.Instance.SetUser(session);
                return true;
            }
            return false;
        }

        public void NavigateToNotification()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CheckPermission()
        {
            var locationPermission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (locationPermission != PermissionStatus.Granted)
            {
                return false;
            }
            else
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();
                    return true;
                }
                catch (PermissionException e)
                {
                    await ServiceHelper.GetService<IAlertService>().ShowAlert("Error", "Enable your location", "ok");
                }
                catch (FeatureNotEnabledException e)
                {
                    // Location turned OFF, show popup to user, etc...
                    await ServiceHelper.GetService<IAlertService>().ShowAlert("Error", "Location is Off. Please enable the location", "ok");
                    ServiceHelper.GetService<ILocationHelper>().OpenLocationSettings();
                }
                catch (FeatureNotSupportedException e)
                {
                    // Location services not supported, show popup to user, etc...
                    await ServiceHelper.GetService<IAlertService>().ShowAlert("Error", "Location service is not enabled. Please enable the location", "ok");
                    ServiceHelper.GetService<ILocationHelper>().OpenLocationSettings();

                }
                catch (Exception e)
                {
                    // Something else went wrong
                }
                return false;
            }
        }

        #endregion
    }
}
