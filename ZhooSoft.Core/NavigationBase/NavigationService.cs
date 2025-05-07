


using CommunityToolkit.Maui.Views;
using ZCars.Model.DTOs.DriverApp;

namespace ZhooSoft.Core.NavigationBase
{
    public class NavigationService : INavigationService
    {
        public async Task ClosePopup()
        {
            
        }

        public async Task<object?> OpenPopup(Popup popup)
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage && nvpage.Navigation.NavigationStack.Count > 0)
                {
                    var page = nvpage.Navigation.NavigationStack.Last();
                    return await page.ShowPopupAsync(popup);
                }
            }

            return null;
        }

        public Task ClosePopup(Dictionary<string, object> navigationParams, bool IsBackPopup = false)
        {
            throw new NotImplementedException();
        }

        public Task OnTabClicked(object obj)
        {
            throw new NotImplementedException();
        }

        public async Task PopAsync()
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage)
                {
                    await nvpage.PopAsync();
                }
            }
        }

        public async Task PopAsync(Dictionary<string, object> NavigationParams)
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage)
                {
                    await nvpage.PopAsync();
                    SetPreviousPageParams(NavigationParams, nvpage.Navigation);
                }
            }
        }

        private void SetPreviousPageParams(Dictionary<string, object> NavigationParams, INavigation navigation)
        {
            if (NavigationParams != null)
            {
                var pages = navigation.NavigationStack.ToList();
                if (pages.Count > 2)
                {
                    var page = pages.LastOrDefault();

                    if (page is ContentPage cpage && cpage.BindingContext is ViewModelBase vm)
                    {
                        vm.NavigationParams = NavigationParams;
                        vm.OnAppearing();
                    }
                }
            }
        }


        public async Task PopToRootAsync()
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage)
                {
                    await nvpage.PopToRootAsync();
                }
            }
        }

        public async Task PushAsync(Page page)
        {
            if (Application.Current != null && Application.Current.Windows != null && Application.Current.Windows.Count > 0)
            {
                if (Application.Current.Windows[0].Page is NavigationPage nvpage)
                {
                    await nvpage.PushAsync(page);
                }
            }
        }

        public async Task PushAsync(Page page, Dictionary<string, object> navigationParams)
        {
            if (page.BindingContext is ViewModelBase vm)
            {
                vm.NavigationParams = navigationParams;
            }
            await PushAsync(page);
        }

        public Task PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }

        
    }
}
