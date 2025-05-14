using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTaxi.Model.DTOs.UserApp;

namespace ZTaxiApp.NavigationExtension
{
    public interface IAppNavigation
    {
        Task LaunchDriverDashBoard();

        Task OpenRidePopup(BookingRequestModel requestModel, Popup popup);
    }
}
