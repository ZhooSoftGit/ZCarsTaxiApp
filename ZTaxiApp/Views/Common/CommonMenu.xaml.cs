using CommunityToolkit.Maui.Views;
using ZTaxiApp.ViewModel.Common;

namespace ZTaxiApp.Views.Common;

public partial class CommonMenu : Popup
{
	public CommonMenu()
	{
		InitializeComponent();
		BindingContext = new SidebarViewModel();

		if(BindingContext is SidebarViewModel vm)
		{
			vm.CurrentPopup = this;
		}
    }
}