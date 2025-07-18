using ZhooSoft.Auth;
using ZhooSoft.Core;
using ZhooSoft.Core.NavigationBase;

namespace ZTaxiApp.Views.Common;

public partial class SplashScreen : ContentPage
{
    private readonly IMainAppNavigation? _mainService;

    public SplashScreen()
	{
		InitializeComponent();
        _mainService = ServiceHelper.GetService<IMainAppNavigation>();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        ServiceHelper.GetService<IMainAppNavigation>().NavigateToMain(true);
    }
}