using ZTaxiApp.ViewModel.Common;

namespace ZTaxiApp.Views.Common;

public partial class EnableLocationPage : ContentPage
{
    public EnableLocationPage()
    {
        InitializeComponent();
        BindingContext = new EnableLocationViewModel();
    }
}
