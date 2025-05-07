using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.UIModel
{
    public partial class Cab : ObservableObject
    {
        [ObservableProperty]
        private string _registrationNumber;

        [ObservableProperty]
        private string _model;

        [ObservableProperty]
        private bool _isSelected;
    }

    public class TimeSlot
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
