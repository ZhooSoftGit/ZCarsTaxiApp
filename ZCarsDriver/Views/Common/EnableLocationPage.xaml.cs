using ZCarsDriver.ViewModel.Common;

namespace ZCarsDriver.Views.Common;

public partial class EnableLocationPage : ContentPage
{
    public EnableLocationPage()
    {
        InitializeComponent();
        BindingContext = new EnableLocationViewModel();
    }
}
