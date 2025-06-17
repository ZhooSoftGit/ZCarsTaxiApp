using CommunityToolkit.Maui.Views;
using ZTaxiApp.DPopupVM;

namespace ZTaxiApp.DPopup;

public partial class CommonMenuPopup : Popup
{
    private double _popupWidth = 0;
    private bool _hasOpened = false;
    public CommonMenuPopup()
    {
        Padding = 0;
        Margin = 0;
        
        InitializeComponent();
        BindingContext = new CommonMenuPopupViewModel(CloseWithAnimationAsync);

        this.Opened += CommonMenuPopup_Opened1; ;
    }

    private async void CommonMenuPopup_Opened1(object? sender, EventArgs e)
    {
        // Let the layout render fully first
        // await Task.Delay(50); // short delay helps layout measure properly

        _popupWidth = Window.Width;

        // Slide in from left to center
        // PopupContent.TranslationX = -_popupWidth;
        await PopupContent.TranslateTo(0, 0, 250, Easing.CubicOut);
    }

    public async Task CloseWithAnimationAsync()
    {
        await PopupContent.TranslateTo(-this.Window.Width, 0, 250, Easing.CubicIn);
        await CloseAsync();
    }
}