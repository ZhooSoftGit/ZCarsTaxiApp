using System.Collections.Specialized;
using ZhooSoft.Core;
using ZTaxiApp.ViewModel;

namespace ZTaxiApp.Views.Common;

public partial class ZhooChatPage : BaseContentPage<ZhooChatViewModel>
{
    public ZhooChatPage()
    {
        InitializeComponent();

        if (ViewModel is ZhooChatViewModel vm)
        {
            vm.Messages.CollectionChanged += Messages_CollectionChanged;
        }

    }

    private void Messages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems?.Count > 0)
        {
            var lastItem = ViewModel.Messages.Last();
            MessageList.ScrollTo(lastItem, position: ScrollToPosition.End, animate: true);
        }
    }
}