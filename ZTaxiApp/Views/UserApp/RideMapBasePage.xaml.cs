using ZhooSoft.Core;
using ZTaxiApp.ViewModel;

namespace ZTaxiApp.Views.Driver;

public partial class RideMapBasePage : BaseContentPage<RideMapBaseViewModel>
{
	public RideMapBasePage()
	{
		InitializeComponent();
        ViewModel.CurrentMap = BookingMap;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        // Simulate animation: slide up the bottom sheet
        await BottomSheet.TranslateTo(0, 0, 500, Easing.SinInOut);
    }
}