using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ZhooSoft.Core;

namespace ZTaxiApp.DPopupVM
{
    public partial class BookingPopupViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _showLoader = true;

        public Popup CurrentPopup;

        public BookingPopupViewModel()
        {
            InitiateTimer();
        }

        private async void InitiateTimer()
        {
            await Task.Delay(8000); // 10 seconds delay
            ShowLoader = false;
            await Task.Delay(2000); // 10 seconds delay
            await CurrentPopup.CloseAsync(true);
        }
    }
}
