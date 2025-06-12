using ZTaxiApp.ViewModel;
using ZhooSoft.Core;
using ZTaxiApp.DPopup;
using ZhooSoft.Core.NavigationBase;

namespace ZTaxiApp.Views;

public partial class HomeViewPage : BaseContentPage<HomeViewModel>
{
    private CommonMenuPopup menuPopup;
    public HomeViewPage()
    {
        InitializeComponent();
        transparentLayout.IsVisible = false;
        menuLayer.IsVisible = false;
        menuPopup = new CommonMenuPopup();        
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        transparentLayout.IsVisible = false;
        menuLayer.IsVisible = false;
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        //transparentLayout.IsVisible = true;
        //menuLayer.IsVisible = true;

        ServiceHelper.GetService<INavigationService>().OpenPopup(new CommonMenuPopup());
    }
}
