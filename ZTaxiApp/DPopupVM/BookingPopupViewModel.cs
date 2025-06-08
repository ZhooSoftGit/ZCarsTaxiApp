using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ZhooSoft.Core;
using ZTaxiApp.Services;

namespace ZTaxiApp.DPopupVM
{
    public partial class BookingPopupViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _showLoader = true;

        public Popup CurrentPopup;
        private UserSignalRService? _userSignalRService;

        public BookingPopupViewModel()
        {
            InitiateTimer();
            _userSignalRService = ServiceHelper.GetService<UserSignalRService>();
        }

        private async void InitiateTimer()
        {
            
        }
    }
}
