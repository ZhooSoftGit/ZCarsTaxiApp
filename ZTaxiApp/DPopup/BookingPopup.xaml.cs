using CommunityToolkit.Maui.Views;
using ZTaxiApp.DPopupVM;

namespace ZTaxiApp.DPopup;

public partial class BookingPopup : Popup
{
	public BookingPopup()
	{
		InitializeComponent();
		BindingContext = new BookingPopupViewModel();

		if(BindingContext is BookingPopupViewModel vm)
		{
			vm.CurrentPopup = this;
		}
	}
}