namespace ZhooCars.Common
{
    #region Enums

    public enum MobileModule
    {
        Driver,
        Vendor,
        ServiceProvider,
        SparParts,
        BuyAndSell,
        RequestForm,
        CustomerSupport
    }

    public enum ApprovalStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public enum UserRoles
    {
        User = 1,
        Driver = 2,
        Admin = 3,
        Vendor = 4,
        ServiceProvider = 5,
        SparPartsDistributor = 6,
        Owner = 7,
        BuyAndSell = 8
    }

    public enum RegsitrationType
    {
        BasicDetails,
        DriverApplication,
        VendorApplication,
        VechicleDetails,
        ServiceProviderApplication,
        SparPartsApplication,
        BuyForm,
        SellingForm
    }

    public enum DocumentTypes
    {
        Aadhar = 1,
        Pan = 2,
        RcBook = 3,
        Insurance = 4,
        License = 5,
        GSTIN = 6,
        BankDetails = 7
    }

    public enum DriverStatus
    {
        CheckIn = 1,
        Idle = 2,
        InRide = 3,
        Break,
        CheckOut,
        LoggedOut
    }

    public enum FuelType
    {
        Petrol,
        Diseal,
        Electronic
    }

    public enum VehicleStatus
    {
        Active = 1,
        InActive,
        Maintenance
    }

    public enum RideStatus
    {
        Requested,      // Ride request is created by the customer
        Assigned,       // Driver is assigned to the ride request
        Reached,        // Driver has reached the pickup location
        Started,        // Ride has started
        Completed,      // Ride is completed
        Cancelled,      // Ride is cancelled by the user or driver
        Failed
    }

    public enum RideTypeEnum
    {
        Local = 0,
        Rental = 1,
        Outstation = 2
    }

    public enum VehicleTypeEnum
    {
        Hatchback,      // Compact small cars
        Sedan,          // Medium-sized cars with a separate trunk
        SUV,            // Sport Utility Vehicles
        MPV,            // Multi-Purpose Vehicles
        EV,             // Electric Vehicles
        Luxury,         // High-end luxury cars
        AutoRickshaw,   // Three-wheeler city ride
        BikeTaxi
    }

    public enum BidStatus
    {
        Requested,           // The ride request is sent to the driver
        CancelledByDriver,   // The driver has canceled the request
        MissedByDriver,      // The driver didn't accept/respond in time
        AllottedToOther,     // The request was assigned to another driver
        Accepted
    }

    public enum ServiceRequestStatus
    {
        Pending,
        Open,
        Assigned,
        InProgress,
        OnHold,
        Escalated,
        Completed,
        Rejected,
        Cancelled,
        Failed,
        Closed
    }

    public enum ActionType
    {
        New,
        View,
        Edit
    }

    public enum DocumentHttpMethod
    {
        /// <summary>
        /// The GET HTTP verb.
        /// </summary>
        GET,
        /// <summary>
        /// The HEAD HTTP verb.
        /// </summary>
        HEAD,
        /// <summary>
        /// The PUT HTTP verb.
        /// </summary>
        PUT,
        /// <summary>
        /// The DELETE HTTP verb.
        /// </summary>
        DELETE
    }

    #endregion

}
