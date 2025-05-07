using ZCarsDriver.ViewModel;
using ZhooSoft.Core;

namespace ZCarsDriver.Views;

public partial class HomeViewPage : BaseContentPage<HomeViewModel>
{
    public HomeViewPage()
    {
        InitializeComponent();
        transparentLayout.IsVisible = false;
        menuLayer.IsVisible = false;
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        transparentLayout.IsVisible = false;
        menuLayer.IsVisible = false;
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        transparentLayout.IsVisible = true;
        menuLayer.IsVisible = true;

    }
}
