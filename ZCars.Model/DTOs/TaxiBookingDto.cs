using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class AcceptRideRequest
    {
        #region Properties

        public int DriverId { get; set; }

        public int RideRequestId { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }

    public class AvailableCabModel
    {
        #region Properties

        public int DriverId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }

    public class FareCalculationRequest
    {
        #region Properties

        public double DropOffLatitude { get; set; }

        public double DropOffLongitude { get; set; }

        public double PickUpLatitude { get; set; }

        public double PickUpLongitude { get; set; }

        public int RideTypeId { get; set; }

        public int VehicleTypeId { get; set; }

        #endregion
    }

    public class FareDetailsModel
    {
        #region Properties

        public double EstimatedDistance { get; set; }

        public double EstimatedFare { get; set; }

        public double EstimatedTime { get; set; }

        #endregion
    }

    public class FareRequestModel
    {
        #region Properties

        public double DropoffLatitude { get; set; }

        public double DropoffLongitude { get; set; }

        public int DurationHours { get; set; }

        public bool IsRoundTrip { get; set; }

        public double PickupLatitude { get; set; }

        public double PickupLongitude { get; set; }

        public RideTypeEnum RideType { get; set; }

        #endregion
    }

    public class FareResponseModel
    {
        #region Properties

        public double EstimatedFare { get; set; }

        public RideTypeEnum RideType { get; set; }

        public VehicleTypeEnum VehicleType { get; set; }

        #endregion
    }

    public class RideRequestModel
    {
        #region Properties

        public double DropOffLatitude { get; set; }

        public string DropOffLocation { get; set; }

        public double DropOffLongitude { get; set; }

        public double PickUpLatitude { get; set; }

        public string PickUpLocation { get; set; }

        public double PickUpLongitude { get; set; }

        public RideTypeEnum RideType { get; set; }

        public VehicleTypeEnum VehicleType { get; set; }

        #endregion
    }

    public class RideServiceResponse
    {
        #region Properties

        public string Message { get; set; }

        public bool Success { get; set; }

        #endregion
    }

}
