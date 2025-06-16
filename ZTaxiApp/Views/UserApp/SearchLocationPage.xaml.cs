using ZTaxiApp.ViewModel;
using ZhooSoft.Core;

namespace ZTaxiApp.Views.Driver;

public partial class SearchLocationPage : BaseContentPage<SearchLocationViewModel>
{
	public SearchLocationPage()
	{
		InitializeComponent();
	}

    private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
		if(ViewModel is SearchLocationViewModel svm)
		{
			svm.SearchCommand?.Execute(e.NewTextValue);
		}
    }
}