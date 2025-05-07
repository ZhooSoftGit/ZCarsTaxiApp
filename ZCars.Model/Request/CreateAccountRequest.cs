namespace ZhooCars.Services
{
    public class CreateAccountRequest
    {
        #region Properties

        public string InstanceId { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public string VerificationId { get; set; }

        #endregion
    }
}
