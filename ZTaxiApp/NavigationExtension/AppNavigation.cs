using CommunityToolkit.Maui.Views;
using ZhooSoft.Core;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxiApp.Common;
using ZTaxiApp.ViewModel;
using ZTaxiApp.Views.Driver;

namespace ZTaxiApp.NavigationExtension
{
    public class AppNavigation : IAppNavigation
    {
        public async Task LaunchDriverDashBoard()
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage)
                {
                    if (nvpage.Navigation.NavigationStack.LastOrDefault() is RideMapBasePage dvPage)
                    {
                        if (dvPage.BindingContext is RideMapBaseViewModel vm)
                        {
                            await vm.RefreshPage();
                        }
                    }
                    else
                    {
                        Application.Current.MainPage = new NavigationPage(new RideMapBasePage());
                    }
                }
            }
        }

        public async Task OpenRidePopup(BookingRequestModel requestModel, Popup popup)
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage && nvpage.Navigation.NavigationStack.Count > 0)
                {
                    if (popup.BindingContext is ViewModelBase vm)
                    {
                        vm.NavigationParams = new Dictionary<string, object> { { "RequestModel", requestModel } };
                        vm.OnNavigatedTo();
                    }

                    var page = nvpage.Navigation.NavigationStack[0] as Page;
                    var result = await page.ShowPopupAsync(popup);
                    if (result is RideStatus status && status == RideStatus.Assigned)
                    {
                        await LaunchDriverDashBoard();
                    }
                }
            }
        }
    }
}
