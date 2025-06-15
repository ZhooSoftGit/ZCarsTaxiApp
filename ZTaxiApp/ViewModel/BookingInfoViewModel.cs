using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhooSoft.Core;
using ZTaxiApp.Helpers;
using ZTaxiApp.Views.Driver;

namespace ZTaxiApp.ViewModel
{
    public class BookingFeature
    {
        #region Properties

        public string Description { get; set; }

        public string Icon { get; set; }

        #endregion
    }

    public partial class BookingInfoViewModel : ViewModelBase
    {
        #region Fields

        [ObservableProperty]
        private ImageSource _infoImage;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private ObservableCollection<BookingFeature> features;

        #endregion

        #region Constructors

        public BookingInfoViewModel()
        {
            var isRental = AppHelper.SelectedAction == ActionEnum.Rental;
            if (isRental)
            {
                Title = PageTitleName = "ZTaxi Rentals";
                InfoImage = "rentalbanner.png";
                Features = new ObservableCollection<BookingFeature>
            {
                new BookingFeature { Icon = "icon_timer.png", Description = "Keep a car and driver for up to 12 hours" },
                new BookingFeature { Icon = "icon_briefcase.png", Description = "Ideal for business meetings, tourist travel and multiple stop trips" },
                new BookingFeature { Icon = "icon_hand.png", Description = "As many stops as you need" }
            };
            }
            else
            {
                Title = PageTitleName = "ZTaxi Outstation";
                InfoImage = "outstationbanner.png";
                Features = new ObservableCollection<BookingFeature>
            {
                new BookingFeature { Icon = "icon_timer.png", Description = "Travel to other cities with round-trip fare" },
                new BookingFeature { Icon = "icon_briefcase.png", Description = "Perfect for weekend getaways or return same day" },
                new BookingFeature { Icon = "icon_hand.png", Description = "Choose from wide range of packages" }
            };
            }
        }

        #endregion

        #region Properties

        public ICommand GetStartedCommand => new Command(OnGetStarted);

        #endregion

        #region Methods

        private async void OnGetStarted()
        {
            await _navigationService.PushAsync(ServiceHelper.GetService<RideLaterBookingsPage>());
        }

        #endregion
    }
}
