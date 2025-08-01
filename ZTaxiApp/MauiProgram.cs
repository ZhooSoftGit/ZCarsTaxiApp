﻿using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Maps;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using ZhooSoft.Auth;
using ZhooSoft.Auth.ViewModel;
using ZhooSoft.Auth.Views;
using ZhooSoft.Controls;
using ZhooSoft.Core;
using ZhooSoft.Core.Alerts;
using ZhooSoft.Core.NavigationBase;
using ZhooSoft.ServiceBase;
using ZTaxiApp.CoreHelper;
using ZTaxiApp.DPopup;
using ZTaxiApp.NavigationExtension;
using ZTaxiApp.Services;
using ZTaxiApp.Services.AppService;
using ZTaxiApp.Services.Contracts;
using ZTaxiApp.Services.Services;
using ZTaxiApp.Services.Session;
using ZTaxiApp.ViewModel;
using ZTaxiApp.Views;
using ZTaxiApp.Views.Common;
using ZTaxiApp.Views.Driver;
using ZTaxiApp.Views.Rides;
using ZTaxiApp.Views.Vendor;
using ZTaxi.Services.Contracts;
using ZTaxi.Services;
using ZTaxiApp.Views.UserApp;
using CommunityToolkit.Maui.Services;




#if ANDROID
using ZTaxiApp.PlatformHelper;
using ZhooSoft.Controls.Platforms.Android;
using ZTaxiApp.Platforms.Android.Helpers;
using Android.Content.Res;
#endif

namespace ZTaxiApp;

public static class MauiProgram
{
    public static string GoogleMapsKey = "AIzaSyDz-0CVEdMMiWHfuXm_zXZp2t69963hCkY";
    public static MauiApp CreateMauiApp()
    {

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("zapp.ttf", "zappfont");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddServices();
        builder.Services.AddViewModel();
        builder.Services.AddPages();

        builder.Services.AddSingleton<IMainAppNavigation, MainAppNavigation>();

        builder.UseMauiApp<App>().UseMauiMaps()
           .UseMauiCommunityToolkit()
           .UseMauiCommunityToolkitMaps(GoogleMapsKey);

#if ANDROID

            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<CustomMapView, CustomMapHandler>();
            });

#endif

        var app = builder.Build();

        ServiceHelper.Initialize(app.Services);

        AddCustomUI();

        return app;
    }

    private static void AddCustomUI()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });

        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("EditorCustomization", (handler, view) =>
        {
#if ANDROID
        handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });
        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("PickerCustomization", (handler, view) =>
        {
#if ANDROID
        handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("DatePickerCustomization", (handler, view) =>
        {
#if ANDROID
        handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });

        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping("TimePickerCustomization", (handler, view) =>
        {
#if ANDROID
        handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });
    }

    private static IServiceCollection AddPages(this IServiceCollection services)
    {
        services.AddTransient<SplashScreen>();

        services.AddTransient<LoginPage>();
        services.AddTransient<OTPVerificationPage>();
        services.AddTransient<HomeViewPage>();
        services.AddTransient<RideLaterBookingsPage>();
        services.AddTransient<DocumentUploadPage>();
        services.AddTransient<RideMapBasePage>();
        services.AddTransient<BaseProfilePage>();

        services.AddTransient<VendorOtpPage>();
        services.AddTransient<SearchLocationPage>();
        services.AddTransient<OtpVerificationPage>();

        services.AddTransient<VehicleListPage>();

        services.AddTransient<CancelTripPage>();
        services.AddTransient<TripDetailsPage>();

        services.AddTransient<CustomMapWebView>();
        services.AddTransient<BookingDetailsPopup>();

        services.AddTransient<CommonFormPage>();

        services.AddTransient<RideDetailsPage>();
        services.AddTransient<RideListPage>();
        services.AddTransient<DashboardPage>();
        services.AddTransient<VehicleDetailsPage>();

        services.AddTransient<EarningsPage>();
        services.AddTransient<PeakHoursPage>();

        services.AddTransient<MapViewPage>();
        services.AddTransient<RideLaterBookingsPage>();
        services.AddTransient<BookingInfoPage>();
        services.AddTransient<ReviewBookingPage>();
        services.AddTransient<ZhooChatPage>();
        services.AddTransient<SliderMenuPage>();
        services.AddTransient<SettingsPage>();
        services.AddTransient<AboutPage>();
        services.AddTransient<ProfilePage>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IAlertService, AlertService>();
        services.AddSingleton<IAppNavigation, AppNavigation>();

#if ANDROID
            services.AddSingleton<IProgressService, ProgressService_Android>();
            services.AddTransient<ILocationHelper, LocationHelperAndroid>();
#endif

#if IOS
        services.AddSingleton<IProgressService, ProgressService_iOS>();
#endif
        services.AddSingleton<IUserSessionManager, UserSessionManager>();
        services.AddSingleton<HttpClient>();
        services.AddSingleton<IHttpAuthHelper, HttpAuthHelper>();
        services.AddSingleton<IPopupService, PopupService>();

        services.AddSingleton<IApiService, ApiService>();

        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IDocumentService, DocumentService>();
        services.AddSingleton<IDriverService, DriverService>();
        services.AddSingleton<IDriverShiftLogService, DriverShiftLogService>();
        services.AddSingleton<IDriverVehicleLinkService, DriverVehicleLinkService>();
        services.AddSingleton<IPeakHoursService, PeakHoursService>();
        services.AddSingleton<IRideHistoryService, RideHistoryService>();
        services.AddSingleton<IRideTripService, RideTripService>();
        services.AddSingleton<IServiceProviderService, ServiceProviderService>();
        services.AddSingleton<IServiceRequestService, ServiceRequestService>();
        services.AddSingleton<ISparePartsProviderService, SparePartsProviderService>();
        services.AddSingleton<ITaxiBookingService, TaxiBookingService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IVehicleDetailsService, VehicleDetailsService>();
        services.AddSingleton<IVehicleLocationService, VehicleLocationService>();
        services.AddSingleton<IVehicleMetadataService, VehicleMetadataService>();
        services.AddSingleton<IVendorService, VendorService>();

        #region
        services.AddSingleton<IOsrmService, OsrmService>();
        services.AddSingleton<ICallService, CallService>();
        services.AddSingleton<IAddressService, AddressService>();
        services.AddSingleton<UserSignalRService>();
        #endregion

        return services;
    }

    private static IServiceCollection AddViewModel(this IServiceCollection services)
    {
        services.AddTransient<LoginViewModel>();
        services.AddTransient<OTPVerificationViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<RideLaterBookingsViewModel>();
        services.AddTransient<DocumentUploadViewModel>();
        services.AddTransient<DynamicFormViewModel>();

        services.AddTransient<BaseProfileViewModel>();

        services.AddTransient<CancelTripViewModel>();
        services.AddTransient<TripDetailsViewModel>();

        services.AddTransient<VehicleListViewModel>();

        services.AddTransient<RideMapBaseViewModel>();
        services.AddTransient<CustomMapWebViewModel>();

        services.AddTransient<SearchLocationViewModel>();
        services.AddTransient<VendorOtpViewModel>();
        services.AddTransient<OtpVerificationViewModel>();


        services.AddTransient<VehicleDetailsViewModel>();
        services.AddTransient<RideListViewModel>();
        services.AddTransient<RideDetailsViewModel>();

        services.AddTransient<EarningsViewModel>();
        services.AddTransient<PeakHoursViewModel>();

        services.AddTransient<MapViewViewModel>();
        services.AddTransient<RideLaterBookingsViewModel>();
        services.AddTransient<BookingInfoViewModel>();
        services.AddTransient<ReviewBookingViewModel>();
        services.AddTransient<ZhooChatViewModel>();

        services.AddTransient<SliderMenuViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<ProfileViewModel>();
        return services;
    }
}
