namespace ZTaxiApp.Services.AppService
{
    public interface ICallService
    {
        Task MakePhoneCall(string phoneNumber);
    }
}
