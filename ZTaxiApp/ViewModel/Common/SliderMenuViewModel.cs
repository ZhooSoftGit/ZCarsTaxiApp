using CommunityToolkit.Maui;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxiApp.Views.Common;
using ZTaxiApp.Views.Vendor;

namespace ZTaxiApp.ViewModel
{
    public partial class SliderMenuViewModel : ViewModelBase
    {
        public string Title { get; set; } = "Menu";
        public string Message { get; set; } = "Choose an option";
        public event Action OnClosed;
        public ICommand CloseCommand { get; }

        public ICommand MenuCommand { get; }


        public SliderMenuViewModel()
        {
            // Initialize the command
            MenuCommand = new Command<string>(async (obj) => await OnMenuSelected(obj));

            // Optional: trigger a method on constructor
            Initialize();
        }

        private async Task OnRideHistory()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<RideListPage>());
        }

        private void Initialize()
        {
            // This method runs when VM is constructed
            // e.g. load data, log analytics, etc.
        }

        private async Task OnMenuSelected(string action)
        {
            switch (action)
            {
                case "RideHistory":
                    await OnRideHistory();
                    break;

                case "Settings":
                    await _navigationService.PushAsync(ServiceHelper.GetService<SettingsPage>());
                    break;

                case "Help":
                    await Launcher.OpenAsync(new Uri("https://www.zhoosoft.com"));
                    break;

                    // add other menu items as needed
            }
        }
    }
}
