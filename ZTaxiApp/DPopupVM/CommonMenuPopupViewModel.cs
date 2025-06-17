using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxiApp.Views.Vendor;

namespace ZTaxiApp.DPopupVM
{
    public class CommonMenuPopupViewModel : ViewModelBase
    {
        public string Title { get; set; } = "Menu";
        public string Message { get; set; } = "Choose an option";

        public ICommand CloseCommand { get; }

        public ICommand OnRideHistoryCmd { get; }

        private IPopupService? _popupService;

        public CommonMenuPopupViewModel(Func<Task> closeAction)
        {
            CloseCommand = new Command(async () => await closeAction());
            OnRideHistoryCmd = new Command(async () => await OnRideHistory());
            _popupService = ServiceHelper.GetService<IPopupService>();
        }

        private async Task OnRideHistory()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<RideListPage>());
            CloseCommand?.Execute(null);
        }
    }
}
