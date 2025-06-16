using ZTaxiApp.ViewModel;
using ZhooSoft.Core;

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
}
