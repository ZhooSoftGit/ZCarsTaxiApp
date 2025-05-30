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

        public int TimerValue { get; set; }

        private System.Timers.Timer _timer;
        private const int TotalTime = 15;
        public BookingPopupViewModel()
        {
            InitiateTimer();

        }

        private void InitiateTimer()
        {
            TimerValue = TotalTime;
            _timer = new System.Timers.Timer(1200);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            TimerValue--;

            if (TimerValue <= 5)
            {
                ShowLoader = false;
            }

            if (TimerValue <= 0)
            {
                _timer.Close();
                _timer.Dispose();
                CurrentPopup.Close(true);
            }
        }
    }
}
