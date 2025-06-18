using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.AspNetCore.SignalR.Client;
using ZhooSoft.Core;
using ZhooSoft.Core.Alerts;
using ZTaxi.Core.Storage;
using ZTaxi.Model.Common;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxiApp.Common;
using ZTaxiApp.Helpers;
using ZTaxiApp.Model;
using ZTaxiApp.NavigationExtension;
using ZTaxiApp.UIModel;


namespace ZTaxiApp.Services
{
    public class UserSignalRService
    {
        private HubConnection _connection;
        private string _userId;
        private System.Timers.Timer? _nearbyDriversTimer;
        private string _hubUrl = $"http://192.168.1.5:7091/hubs/location";  // 🔁 Change to your actual URL
        private string? _trackedDriverId;

        public event Action<List<DriverLocation>>? OnNearbyDriversUpdated;
        public event Action<string>? OnDriverConfirmedBooking;
        public event Action<DriverLocation>? OnDriverLocationUpdated;

        public event Action<TripOtpInfo>? OnTripOtpReceived;

        public UserSignalRService()
        {

        }

        public HubConnection GetConnection()
        {
            return _connection;
        }

        public void Initialize(string userId)
        {
            _userId = userId;
            var url = $"{_hubUrl}?userId={userId}";
            _connection = new HubConnectionBuilder()
            .WithUrl($"{url}")
            .WithAutomaticReconnect()
            .Build();

            RegisterHandler();

            // Subscribed actions from SignalR
            _connection.On<List<DriverLocation>>("ReceiveNearbyDrivers", drivers =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnNearbyDriversUpdated?.Invoke(drivers);
                });
            });

            _connection.On<string>("DriverConfirmedBooking", driverId =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnDriverConfirmedBooking?.Invoke(driverId);
                });
            });

            _connection.On<DriverLocation>("ReceiveDriverLocation", location =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (location.DriverId == _trackedDriverId)
                        OnDriverLocationUpdated?.Invoke(location);
                });
            });
        }

        public async Task ConnectAsync()
        {
            try
            {
                await _connection.StartAsync();
                Console.WriteLine("✅ Connected to SignalR hub.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to connect: {ex.Message}");
            }
        }

        public async Task DisconnectAsync()
        {
            await _connection.StopAsync();
            _nearbyDriversTimer?.Stop();
        }

        // Start polling for nearby drivers every X seconds
        public async Task GetNearbyDriversAsync(Location getCurrentLocation, int intervalSeconds = 30)
        {

            if (_connection.State == HubConnectionState.Connected)
            {
                try
                {
                    await _connection.SendAsync("GetNearbyDrivers", getCurrentLocation.Latitude, getCurrentLocation.Longitude); ;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching nearby drivers: {ex.Message}");
                }
            }
        }

        // Send booking request to a specific driver (or multiple)
        public async Task SendBookingRequestToDriver(BookingRequestModel request)
        {
            try
            {
                if (await CheckConnection())
                {
                    await _connection.InvokeAsync("SendBookingRequest", request);
                    _trackedDriverId = request.DriverId;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<bool> CheckConnection()
        {
            if (_connection.State == HubConnectionState.Connected)
            {
                return true;
            }
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Reconnection failed: " + ex.Message);
                return false;
            }
            return true;
        }

        public async Task SendBookingRequest(List<string> driverIds, BookingRequestModel request)
        {
            var confirmRide = await SendSequentialRequests(driverIds, request);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (confirmRide)
                {
                    await OnBookingConfirmed(request);
                }
                else
                {
                    await ServiceHelper.GetService<IAlertService>().ShowAlert("Info", "Driver not accepted", "Ok");
                    await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard(true);
                }
            });
        }

        public async Task OnBookingConfirmed(BookingRequestModel request)
        {
            var ride = new CurrentRide
            {
                BookingRequest = request,
                CurrentStatus = RideStatus.Assigned
            };
            RideStorageService.Save(ride);

            await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard(true);
        }

        public async Task<bool> SendSequentialRequests(List<string> driverIds, BookingRequestModel request)
        {
            bool isRideConfirmed = false;
            var location = await AppHelper.GetUserLocation();

            TaskCompletionSource<string> tcs = null;

            // Set up handler for driver response ONCE outside the loop
            void OnBookingResponse(BookingResponseModel responseObj)
            {
                var responseDriverId = responseObj.DriverId;
                var status = responseObj.Status;

                var issame = request.DriverId == responseDriverId;

                if (request.DriverId == responseDriverId)
                {
                    if (status.Equals(RideStatus.Assigned.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        isRideConfirmed = true;
                        tcs?.TrySetResult("confirmed");
                    }
                    else if (status.Equals(RideStatus.Cancelled.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        tcs?.TrySetResult("rejected");
                    }
                }
            }

            _connection.On<BookingResponseModel>("BookingResponseFromDriver", OnBookingResponse);

            foreach (var driverId in driverIds.Take(3))
            {
                request.DriverId = driverId;
                tcs = new TaskCompletionSource<string>();

                // Send booking request to driver
                await SendBookingRequestToDriver(request);

                var completed = await Task.WhenAny(tcs.Task, Task.Delay(15000));

                if (completed == tcs.Task && tcs.Task.Result == "confirmed")
                {
                    // Booking confirmed, break out
                    break;
                }

                // Otherwise, try next driver
            }

            // Clean up SignalR handler to avoid memory leaks
            _connection.Remove("BookingResponseFromDriver");

            return isRideConfirmed;
        }

        private System.Threading.Timer _locationTimer;


        public async Task NotifyCancelTrip()
        {
            var rideId = AppHelper.CurrentRide?.BookingRequest.BoookingRequestId.ToString();
            try
            {
                await _connection.InvokeAsync("CancelTripNotification", rideId);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task OnPickupReached()
        {
            var ride = AppHelper.CurrentRide;
            ride.CurrentStatus = RideStatus.Reached;
            AppHelper.SaveRideInfo(ride);
            await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard();
        }

        public async Task OnPickupCancelled()
        {
            var ride = AppHelper.CurrentRide;
            ride.CurrentStatus = RideStatus.Cancelled;
            AppHelper.SaveRideInfo(ride);
            await ServiceHelper.GetService<IAlertService>().ShowAlert("OOPS", "Your trip has been cancelled", "ok");
            await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard();
        }

        public async Task TripStarted()
        {
            var ride = AppHelper.CurrentRide;
            ride.CurrentStatus = RideStatus.Started;
            AppHelper.SaveRideInfo(ride);
            await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard();
        }

        public async Task TripCompleted()
        {
            var ride = AppHelper.CurrentRide;
            ride.CurrentStatus = RideStatus.Completed;
            AppHelper.SaveRideInfo(ride);
            await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard();
        }

        public async Task TripCancelled()
        {
            var ride = AppHelper.CurrentRide;
            ride.CurrentStatus = RideStatus.Cancelled;
            await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard();
        }

        public async Task ReceiveOTP(TripOtpInfo tripOtp)
        {
            // Store or update UI
            Console.WriteLine($"Start OTP: {tripOtp.StartOtp}");
            Console.WriteLine($"End OTP: {tripOtp.EndOtp}");

            var ride = AppHelper.CurrentRide;
            ride.CurrentStatus = RideStatus.Assigned;
            ride.TripOtpInfo = tripOtp;
            AppHelper.CurrentRide = null;
            RideStorageService.Save(ride);
            OnTripOtpReceived?.Invoke(tripOtp);
            await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard();
        }

        public void RegisterHandler()
        {
            _connection.On<string>("OnStartPickup", rideId =>
            {
                
            });
            _connection.On<string>("OnPickupReached", rideId =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await OnPickupReached();
                });
            });
            

            _connection.On<string>("OnTripCancelled", rideId =>
            {
                //call the API
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await OnPickupCancelled();
                });
            });

            _connection.On<string>("TripStarted", bookingRequestId =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await TripStarted();
                });
            });

            _connection.On<string>("TripCompleted", bookingRequestId =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await TripCompleted();
                });
            });

            _connection.On<string>("TripCancelled", bookingRequestId =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await TripCancelled();
                });
            });

            _connection.On<TripOtpInfo>("ReceiveTripOtps", (tripOtp) =>
            {
                // Handle received OTPs
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await ReceiveOTP(tripOtp);
                });
            });
        }
    }
}
