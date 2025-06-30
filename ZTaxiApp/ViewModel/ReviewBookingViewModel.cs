using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxiApp.Common;
using ZTaxiApp.Services.Contracts;
using ZTaxiApp.UIModel;

namespace ZTaxiApp.ViewModel
{
    public class ReviewBookingModel
    {
        #region Properties

        public double DistanceKm { get; set; }

        public LocationInfo DropLocation { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime? EndDateTime { get; set; }

        public decimal EstimatedFare { get; set; }

        public string FareBreakup { get; set; }

        public decimal InsuranceCost { get; set; }

        public bool IsInsuranceAdded { get; set; }

        public LocationInfo PickupLocation { get; set; }

        public int? RentelHrs { get; set; }

        public List<string> RideInclusions { get; set; }

        public RideTypeEnum RideType { get; internal set; }

        public List<string> RulesAndRestrictions { get; set; }

        public DateTime StartDateTime { get; set; }

        public List<string> TermsAndConditions { get; set; }

        public string TripType { get; set; }

        public string VehicleType { get; set; }

        #endregion
    }

    public partial class ReviewBookingViewModel : ViewModelBase
    {
        #region Fields

        private readonly ITaxiBookingService _taxiBookingService;

        [ObservableProperty]
        private ReviewBookingModel _booking;

        #endregion

        #region Constructors

        public ReviewBookingViewModel()
        {
            ConfirmBookingCmd = new Command(async () => await ConfirmBooking());
            _taxiBookingService = ServiceHelper.GetService<ITaxiBookingService>();
        }

        #endregion

        #region Properties

        public ICommand ConfirmBookingCmd { get; }

        #endregion

        #region Methods

        public async override void OnAppearing()
        {
            base.OnAppearing();
            PageTitleName = "Review Bookings";
            if (NavigationParams.ContainsKey("reviewBooking"))
            {
                if (NavigationParams["reviewBooking"] is ReviewBookingModel rb)
                {
                    Booking = rb;
                }
            }
        }

        private async Task ConfirmBooking()
        {
            var result = await _taxiBookingService.BookRideAsync(new Model.DTOs.RideRequestModel
            {
                DropOffLatitude = Booking.DropLocation.Latitude,
                DropOffLocation = Booking.DropLocation.Address,
                DropOffLongitude = Booking.DropLocation.Longitude,
                PickUpLatitude = Booking.PickupLocation.Latitude,
                PickUpLongitude = Booking.PickupLocation.Longitude,
                PickUpLocation = Booking.PickupLocation.Address,
                RideType = Booking.RideType,
                VehicleType = Enum.TryParse<VehicleTypeEnum>(Booking.VehicleType, ignoreCase: true, out var ride) ? ride : VehicleTypeEnum.Sedan,
                DropDateTime = Booking.EndDateTime,
                PickupDateTime = Booking.StartDateTime,
                RideStatus = ZTaxiApp.Common.RideStatus.Scheduled,
                RentalHours = Booking.RentelHrs

            });
            await _alertService.ShowAlert("info", "booking confirmed", "okay");
            await _navigationService.PopToRootAsync();
        }

        #endregion
    }
}
