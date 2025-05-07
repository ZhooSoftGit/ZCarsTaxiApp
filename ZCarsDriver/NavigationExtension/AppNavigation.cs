using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCars.Model.DTOs.DriverApp;
using ZCarsDriver.ViewModel;
using ZCarsDriver.Views.Driver;
using ZhooCars.Common;
using ZhooSoft.Core;

namespace ZCarsDriver.NavigationExtension
{
    public class AppNavigation : IAppNavigation
    {
        public async Task LaunchDriverDashBoard()
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage)
                {
                    if (nvpage.Navigation.NavigationStack.LastOrDefault() is DriverDashboardPage dvPage)
                    {
                        if (dvPage.BindingContext is DriverDashboardViewModel vm)
                        {
                            await vm.RefreshPage();
                        }
                    }
                    else
                    {
                        Application.Current.MainPage = new NavigationPage(new DriverDashboardPage());
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
