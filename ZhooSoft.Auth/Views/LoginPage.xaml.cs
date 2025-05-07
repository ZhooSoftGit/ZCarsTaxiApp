using ZhooSoft.Auth.ViewModel;
using ZhooSoft.Core;

namespace ZhooSoft.Auth.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<LoginViewModel>();
	}
}