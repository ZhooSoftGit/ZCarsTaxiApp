using ZTaxiApp.ViewModel;
using ZhooSoft.Core;
using Microsoft.Maui.Handlers;

namespace ZTaxiApp.Views.Driver;

public partial class RideLaterBookingsPage : BaseContentPage<RideLaterBookingsViewModel>
{
    bool isExpand;
    public RideLaterBookingsPage()
    {
        InitializeComponent();
        ViewModel.CurrentMap = BookingMap;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        isExpand = !isExpand;
        if (isExpand)
        {
            AbsoluteLayout.SetLayoutBounds(bottomsheet, new Rect(0, 1, 1, 0.75));
        }
        else
        {
            AbsoluteLayout.SetLayoutBounds(bottomsheet, new Rect(0, 1, 1, 0.5));
        }
    }


    private async void pickupdate_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (!ViewModel.IsLoaded) return;

#if ANDROID
        var handler = pickuptime.Handler as ITimePickerHandler;
        await Task.Delay(50);
        handler.PlatformView.PerformClick();
#endif
    }

    private async void dropdate_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (!ViewModel.IsLoaded) return;

#if ANDROID
        var handler = droptime.Handler as ITimePickerHandler;
        await Task.Delay(50);
        handler.PlatformView.PerformClick();
#endif

    }
}
