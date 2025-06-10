using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;
using ZhooSoft.Core;
using ZhooSoft.Core.Alerts;
using ZTaxi.Core.Storage;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxiApp.Common;
using ZTaxiApp.Helpers;
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

        public UserSignalRService()
        {

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
                await _connection.InvokeAsync("SendBookingRequest", request);
                _trackedDriverId = request.DriverId;
            }
            catch(Exception ex)
            {

            }
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
                        StartTrackingConfirmedDriver(responseDriverId);
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

        public void StartTrackingConfirmedDriver(string driverId)
        {
            _locationTimer = new System.Threading.Timer(async _ =>
            {
                try
                {
                    if (_connection?.State == HubConnectionState.Connected)
                    {
                        await _connection.InvokeAsync("SendLiveLocationToUser", driverId);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending location: {ex.Message}");
                }
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        }

        public void StopTrackingConfirmedDriver()
        {
            _locationTimer?.Dispose();
        }

        public async Task NotifyCancelTrip()
        {
            var rideId = AppHelper.CurrentRide?.RideDetails?.RideTripId ?? 1;
            try
            {
                await _connection.InvokeAsync("CancelTripNotification", rideId);
                StopTrackingConfirmedDriver();
            }
            catch(Exception ex)
            {

            }
        }

        public void RegisterHandler()
        {
            _connection.On<string>("OnStartPickup", rideId => {
                UpdateRideStatus("Driver On the Way");
            });
            _connection.On<string>("OnPickupReached", rideId => { 
                UpdateRideStatus("Driver Reached your location"); 
            });
            _connection.On<string>("OnStartTrip", rideId => {
                
            });
            _connection.On<string>("OnCompleteTrip", rideId => {
                StopTrackingConfirmedDriver();
                UpdateRideStatus("Trip completed");
            });
            _connection.On<string>("OnTripCancelled", rideId =>
            {
                //call the API
                RideStorageService.Clear();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await ServiceHelper.GetService<IAlertService>().ShowAlert("OOPS", "Your trip has been cancelled", "ok");
                    await ServiceHelper.GetService<IAppNavigation>().LaunchUserDashBoard();
                });
                StopTrackingConfirmedDriver();
            });

            _connection.On<string>("TripStarted", bookingRequestId =>
            {
                // Update UI for trip started
            });

            _connection.On<string>("TripCompleted", bookingRequestId =>
            {
                // Update UI for trip completed
            });

            _connection.On<string>("TripCancelled", bookingRequestId =>
            {
                // Update UI for trip completed
            });
        }

        private void UpdateRideStatus(string message)
        {
            MainThread.BeginInvokeOnMainThread(async() =>
            {
                await Toast.Make(message, ToastDuration.Short).Show();
            });
        }
    }
}
