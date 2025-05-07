using ZhooSoft.Core;

namespace ZCarsDriver.Services
{
    public static class ApiConstants
    {
        #region Constants

        public const string AcceptRide = "taxi/accept-ride";

        public const string AccountRefreshToken = "Account/RefreshToken";

        public const string AccountReSendOtp = "Account/resend-otp";

        public const string AccountSendOtp = "Account/send-otp";

        public const string AccountVerifyOtp = "Account/verify-otp";

        public const string AddPeakHour = "AddPeakHour";

        public const string APIUrl = GlobalConstants.APPUrl;

        public const string AssignServiceRequest = "service-requests/{0}/assign/{1}";

        public const string BaseUrl = APIUrl;

        public const string BookRide = "taxi/book-ride";

        public const string CalculateFare = "taxi/calculate-fare";

        public const string CancelRideRequest = "taxi/cancel-request/{0}";// Format with rideRequestId

        public const string CancelTrip = "ride-trips/cancel";

        public const string CheckIn = "driver-shift/checkin";

        public const string CheckOut = "driver-shift/checkout";

        public const string CreateServiceRequest = "service-requests";

        public const string DevUrl = "";

        public const string DocumentApprove = "Document/approve";

        public const string DocumentGetDocuments = "Document/getdocuments";

        public const string DocumentGetSignedUrl = "Document/getsignedurl";

        public const string DocumentUpload = "Document/upload";

        public const string DocumentUpsert = "Document/UpsertDocument";

        public const string DriverDetailFilter = "DriverDetail/filter";

        public const string DriverDetailGet = "DriverDetail";

        public const string DriverDetailRegister = "DriverDetail/register";

        public const string DriverDetailUpdate = "DriverDetail/update";

        public const string DriverRideHistory = "ride-history/driver-history";

        public const string DriverVehicleByVendor = "DriverAndVehicleLink/vehicles-by-vendor";

        public const string DriverVehicleDeleteLink = "DriverAndVehicleLink/delete-link";

        public const string DriverVehicleGetLink = "DriverAndVehicleLink/get-link";

        public const string DriverVehicleRequestLink = "DriverAndVehicleLink/request-link";

        public const string DriverVehicleVerifyLink = "DriverAndVehicleLink/verify-link";

        public const string EndTrip = "ride-trips/end";

        public const string GetFareOptions = "taxi/get-fare-options";

        public const string GetServiceProviderById = "serviceproviders";

        public const string GetServiceProvidersByFilter = "serviceproviders/filter";

        public const string GetServiceRequestDetails = "service-requests/GetServiceRequestDetails";

        public const string GetServiceRequests = "service-requests/admin/search";

        public const string GetShiftLogs = "driver-shift/shift-logs";

        public const string GetUserDashBoardDetails = "user/GetUserDashBoardDetails";

        public const string GetUserDetails = "user/GetUserDetails";

        public const string GetVehicleById = "vehicle/{id}";

        public const string GetVehicleLocation = "vehiclelocation/get-location";

        public const string GetVehiclesByFilter = "vehicle/filter";

        public const string GetVendorById = "vendors/{id}";

        public const string GetVendorsByFilter = "vendors/filter";

        public const string NotifyNearbyProviders = "service-requests/{0}/notify-nearby";

        public const string PassengerRideHistory = "ride-history/passenger-history";

        public const string PeakHourById = "peak-hours/{0}";

        public const string PeakHours = "peak-hours";

        public const string ProdUrl = "";

        public const string ReachPickup = "ride-trips/reach-pickup";

        public const string RegisterServiceProvider = "serviceproviders/register";

        public const string RegisterVehicleDetails = "vehicle/register";

        public const string SearchCabs = "taxi/search-cabs";

        public const string SkipBid = "taxi/skip-bid";

        public const string SparePartsProvidersBase = "sparepartsproviders";

        public const string SparePartsProvidersFilter = "sparepartsproviders/filter";

        public const string SparePartsProvidersRegister = "sparepartsproviders/register";

        public const string SparePartsProvidersUpdate = "sparepartsproviders/update";

        public const string StartTrip = "ride-trips/start";

        public const string TripPayment = "ride-trips/payment";

        public const string UATUrl = "";

        public const string UpdateDistance = "ride-trips/update-distance";

        public const string UpdateInsurance = "vehicle/update-insurance/{id}";

        public const string UpdateServiceProvider = "serviceproviders/update";

        public const string UpdateServiceRequestStatus = "service-requests/{0}/update-status";

        public const string UpdateVehicleLocation = "vehiclelocation/update-location";

        public const string UpdateVehicleStatus = "vehiclelocation/update-status";

        public const string UpsertUserDetails = "user/upsertUserDetails";

        public const string UpsertVehicle = "vehicle/upsert";

        public const string UpsertVendor = "vendors/update";

        public const string VehicleModels = "metadata/vehicle-models";

        public const string VehicleModelsByType = "metadata/vehicle-models/{0}";

        public const string VehicleTypes = "metadata/vehicle-types";

        public const string VendorRegisterRequest = "vendors/register";

        private const string _baseUrl = "";

        #endregion
    }
}
