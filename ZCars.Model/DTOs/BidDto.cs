using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class PlaceBidRequestModel
    {
        #region Properties

        public decimal BidAmount { get; set; }

        public int DriverId { get; set; }

        public int RideRequestId { get; set; }

        #endregion
    }

    public class UpdateBidRequestModel
    {
        #region Properties

        public int BidId { get; set; }

        public int RideRequestId { get; set; }

        public BidStatus Status { get; set; }

        #endregion
    }

}
