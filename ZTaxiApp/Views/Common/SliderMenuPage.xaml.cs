using ZhooSoft.Core;
using ZTaxiApp.ViewModel;

namespace ZTaxiApp.Views.Common;

public partial class SliderMenuPage : BaseContentPage<SliderMenuViewModel>
{

    public SliderMenuPage()
    {
        InitializeComponent();
        Content.TranslationX = -5000;

        if (BindingContext is SliderMenuViewModel vm)
        {
            vm.OnClosed += Vm_OnClosed;
        }

    }

    private async void Vm_OnClosed()
    {
        await Content.TranslateTo(-this.Window.Width, 0, 250, Easing.CubicIn);
        if (BindingContext is SliderMenuViewModel vm)
        {
            await vm._navigationService.PopAsync();
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Content.TranslateTo(0, 0, 250);
    }
}