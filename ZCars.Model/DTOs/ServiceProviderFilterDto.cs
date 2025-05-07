namespace ZhooCars.Model.DTOs
{
    public class ServiceProviderFilterDto
    {
        #region Properties

        public string? City { get; set; }

        public string? CompanyName { get; set; }

        public string? FirstName { get; set; }

        public bool? IsActive { get; set; }

        public string? LastName { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? PhoneNumber { get; set; }

        public string? PostalCode { get; set; }

        #endregion
    }

}
