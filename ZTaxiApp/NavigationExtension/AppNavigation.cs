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
        public async Task LaunchUserDashBoard()
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
                }
            }
        }
    }
}
