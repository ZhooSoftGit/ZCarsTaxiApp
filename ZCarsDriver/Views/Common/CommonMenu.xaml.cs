using CommunityToolkit.Maui.Views;
using ZCarsDriver.ViewModel.Common;

namespace ZCarsDriver.Views.Common;

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