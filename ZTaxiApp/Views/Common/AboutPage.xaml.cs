using System.Windows.Input;
using ZhooSoft.Core;
using ZhooSoft.Core.NavigationBase;

namespace ZTaxiApp.Views.Common;

public partial class AboutPage : ContentPage
{
    public ICommand VisitWebsiteCommand { get; }

    public AboutPage()
    {
        InitializeComponent();
        VisitWebsiteCommand = new Command(() => Launcher.OpenAsync(new Uri("https://www.zhoosoft.com")));
        BindingContext = this;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await ServiceHelper.GetService<INavigationService>().PopAsync();
    }
}