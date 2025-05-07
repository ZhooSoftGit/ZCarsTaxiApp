namespace ZhooCars.Model.DTOs
{
    public class VehicleModelDto
    {
        #region Properties

        public string BrandName { get; set; } = string.Empty;

        public int ModelId { get; set; }

        public string ModelName { get; set; } = string.Empty;

        public int VehicleTypeId { get; set; }

        #endregion
    }
}
