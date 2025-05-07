using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZCarsDriver.Helpers;
using ZCarsDriver.Services.Session;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel.Common
{
    public partial class SidebarViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<SidebarOption> _options;

        [ObservableProperty]
        private SidebarHeader _header;

        public Popup CurrentPopup;

        public IAsyncRelayCommand OnSelectOption { get; }

        public SidebarViewModel()
        {
            LoadOptions("role");
            LoadHeader();
            OnSelectOption = new AsyncRelayCommand<SidebarOption>((obj)=> OnSelect(obj));
        }

        private async Task OnSelect(SidebarOption obj)
        {
            await CurrentPopup.CloseAsync(obj);
        }

        private void LoadHeader()
        {
            Header = new SidebarHeader
            {
                UserName = "Gokulkannan",
                PhoneNumber = "8110000196",
                CabNumber = "114",
                DriverId = "TIA01619"
            };
        }

        private void LoadOptions(string role)
        {
            Options =
            [
                new SidebarOption { Title = "Notifications", Icon = "notifications.png", MenuType = MenuEnum.Notification, HasNotification = true, NotificationCount = 142 },
                new SidebarOption { Title = "Trip Types", Icon = "triptypes.png", MenuType = MenuEnum.RideTypes },
                new SidebarOption { Title = "Reports", Icon = "reports.png", MenuType = MenuEnum.Reports },
                new SidebarOption { Title = "User App QR Code", Icon = "qr.png", MenuType = MenuEnum.UserAppQRCode },
                new SidebarOption { Title = "Watch Demo Videos", Icon = "video.png", MenuType = MenuEnum.DemoVideo, HasNotification = true, NotificationCount = 9 },
                new SidebarOption { Title = "FAQs", Icon = "faq.png", MenuType = MenuEnum.FAQ },
                new SidebarOption { Title = "Contact Support", Icon = "support.png", MenuType = MenuEnum.ContactSupport },
            ];

        }
    }

    public class SidebarOption
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public MenuEnum MenuType { get; set; }
        public bool HasNotification { get; set; }
        public int NotificationCount { get; set; }
    }

    public class SidebarHeader
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string CabNumber { get; set; }
        public string DriverId { get; set; }
    }
}
