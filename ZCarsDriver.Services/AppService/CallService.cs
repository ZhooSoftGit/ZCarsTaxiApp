namespace ZCarsDriver.Services.AppService
{
    public class CallService : ICallService
    {
        public async Task MakePhoneCall(string phoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    Console.WriteLine("Invalid phone number.");
                    return;
                }

                var uri = new Uri($"tel:{phoneNumber}");

                if (await Launcher.CanOpenAsync(uri))
                {
                    await Launcher.OpenAsync(uri);
                }
                else
                {
                    Console.WriteLine("Phone call is not supported on this device.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error making call: {ex.Message}");
            }
        }
    }
}
