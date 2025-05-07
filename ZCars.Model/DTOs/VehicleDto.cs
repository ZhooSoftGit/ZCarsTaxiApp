using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class InsuranceUpdateDto
    {
        #region Properties

        public DateTime InsuranceExpiryDate { get; set; }

        public string InsuranceNumber { get; set; } = string.Empty;

        #endregion
    }

    public class RegisterVehicleDto
    {
        #region Properties

        public List<DocumentDto> Documents { get; set; }

        public required VehicleDto VehicleDetails { get; set; }

        #endregion
    }

    public class VehicleDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public required string Color { get; set; } = "White";

        public required string FuelType { get; set; } = "Petrol";

        public required DateTime InsuranceExpiryDate { get; set; } = DateTime.Now;

        public required string InsuranceNumber { get; set; } = "1234";

        public required string RCNumber { get; set; } = "abc";

        public DateTime? RegistrationDate { get; set; }

        public required int SeatingCapacity { get; set; } = 4;

        public VehicleStatus Status { get; set; }

        public int VehicleId { get; set; }

        public required string VehicleMake { get; set; } = "TATA";

        public required string VehicleModel { get; set; }

        public required string VehicleRegistrationNumber { get; set; }

        public int VehicleTypeId { get; set; }

        public required int VehicleYear { get; set; } = 2004;


        public bool ShowRegistrationOption { get; set; }

        #endregion
    }
}
