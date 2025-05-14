using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTaxiApp.Helpers
{
    public enum PopupEnum
    {
        CancelRide,
        TripDetails,
        CallRide,

        ShowProfile,
        ShowDashboard
    }

    public enum MenuEnum
    {
        Notification,
        RideTypes,
        Reports,
        DemoVideo,
        ContactSupport,
        UserAppQRCode,
        FAQ
    }

    public enum ActionEnum
    {
        Cab,
        Auto,
        OutStation,
        Rental,
        RequestDriver,
        RequestService,
        RequestSparParts,
        BuyVehicle,
        SellVehicle,
        Insurance,
        Loan,
        FastTag,
        Emergency
    }

    public enum DashboardActionType
    {
        Ride,
        Service
    }

}
