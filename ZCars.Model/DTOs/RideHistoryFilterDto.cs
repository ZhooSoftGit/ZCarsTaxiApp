namespace ZhooCars.Model.DTOs
{
    public class RideHistoryFilterDto
    {
        #region Properties

        public DateTime? FromDate { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTime? ToDate { get; set; }

        #endregion
    }

}
