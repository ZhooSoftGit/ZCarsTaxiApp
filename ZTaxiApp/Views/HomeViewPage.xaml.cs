using ZTaxiApp.ViewModel;
using ZhooSoft.Core;
using ZTaxiApp.DPopup;
using ZhooSoft.Core.NavigationBase;
using ZTaxiApp.Views.Common;

namespace ZTaxiApp.Views;

public partial class HomeViewPage : BaseContentPage<HomeViewModel>
{
    private CommonMenuPopup menuPopup;
    public HomeViewPage()
    {
        InitializeComponent();
        menuPopup = new CommonMenuPopup();        
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        //transparentLayout.IsVisible = true;
        //menuLayer.IsVisible = true;
        ServiceHelper.GetService<INavigationService>().PushAsync(ServiceHelper.GetService<SliderMenuPage>());
    }
}
