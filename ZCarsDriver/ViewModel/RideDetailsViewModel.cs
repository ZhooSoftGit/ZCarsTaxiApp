using CommunityToolkit.Mvvm.ComponentModel;
using ZCarsDriver.UIModel;
using ZhooSoft.Core;

namespace ZCarsDriver.ViewModel
{
    public partial class RideDetailsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string driverPhoto = "driver_photo.png";

        [ObservableProperty]
        private string driverName = "David Jones";

        [ObservableProperty]
        private string pickupLocation = "San Francisco Airport";

        [ObservableProperty]
        private string dropoffLocation = "San Francisco 44 street";

        [ObservableProperty]
        private DateTime rideDateTime = new DateTime(2024, 12, 13, 8, 50, 0);

        [ObservableProperty]
        private decimal totalFare = 72.86m;

        [ObservableProperty]
        private decimal tripFare = 50.86m;

        [ObservableProperty]
        private decimal convenienceFee = 10.00m;

        [ObservableProperty]
        private decimal taxesFees = 12.00m;

        [ObservableProperty]
        private RideReview _rideReview;

        public RideDetailsViewModel()
        {

        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            PageTitleName = "Ride details";
            LoadData();
        }

        private void LoadData()
        {
            RideReview = new RideReview
            {
                UserName = "John Doe",
                UserPhoto = "user1.png",
                ReviewDate = "April 3, 2025",
                Rating = 5,
                ReviewText = "The ride was smooth and the driver was very professional!"
            };
        }
    }
}
