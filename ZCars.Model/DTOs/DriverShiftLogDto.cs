using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class DriverShiftLogDto
    {
        #region Properties

        public string CheckInLocation { get; set; }

        public DateTime CheckInTime { get; set; }

        public string CheckOutLocation { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int DriverId { get; set; }

        public int ShiftId { get; set; }

        public DriverStatus ShiftStatus { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        #endregion
    }

}
