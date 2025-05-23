﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTaxiApp.Helpers;
using ZTaxiApp.Model.DTOs;
using ZhooSoft.Core;
using ZTaxi.Model.DTOs.UserApp;

namespace ZTaxiApp.ViewModel
{
    public partial class TripDetailsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private BookingRequestModel _bookingRequest;

        [ObservableProperty]
        private RideTripDto _rideTripDto;


        public IRelayCommand GoBackCommand { get; }

        public TripDetailsViewModel()
        {
            PageTitleName = "Trip Details";
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if(AppHelper.CurrentRide != null)
            {
                BookingRequest = AppHelper.CurrentRide.BookingRequest;
                RideTripDto = AppHelper.CurrentRide.RideDetails;
            }
        }
    }
}
