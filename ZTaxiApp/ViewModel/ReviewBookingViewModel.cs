using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using ZhooSoft.Core;
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

        public List<string> RideInclusions { get; set; }

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

        [ObservableProperty]
        private ReviewBookingModel _booking;


        public ICommand ConfirmBookingCmd { get; }

        #endregion

        public ReviewBookingViewModel()
        {
            ConfirmBookingCmd = new Command(async () => await ConfirmBooking());
        }

        private async Task ConfirmBooking()
        {
            await _alertService.ShowAlert("info", "booking confirmed", "okay");
            await _navigationService.PopToRootAsync();
        }

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

        #endregion
    }
}
