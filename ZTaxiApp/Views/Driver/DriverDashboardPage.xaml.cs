using Microsoft.Maui.Controls.Maps;
using ZTaxiApp.ViewModel;
using ZhooSoft.Core;

namespace ZTaxiApp.Views.Driver;

public partial class DriverDashboardPage : BaseContentPage<DriverDashboardViewModel>
{
	public DriverDashboardPage()
	{
		InitializeComponent();
        ViewModel.CurrentMap = MyMap;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        //ViewModel.InitializeMap();
    }
}