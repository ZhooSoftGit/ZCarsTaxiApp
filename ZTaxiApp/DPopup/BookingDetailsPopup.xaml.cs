using CommunityToolkit.Maui.Views;
using System.Threading.Tasks;

namespace ZTaxiApp.DPopup;

public partial class BookingDetailsPopup : Popup
{
    public BookingDetailsPopup()
    {
        InitializeComponent();
        BindingContext = new BookingDetailsViewModel();
        if (BindingContext is BookingDetailsViewModel vm)
        {
            vm.CurrentPopup = this;
        }
    }

    private void OnSkipClicked(object sender, EventArgs e)
    {
        CloseAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is BookingDetailsViewModel vm)
        {
            vm.RejectCommand.Execute(e);
            await CloseAsync();
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        if (BindingContext is BookingDetailsViewModel vm)
        {
            vm.AcceptCommand.Execute(e);
            await CloseAsync();
        }
    }
}