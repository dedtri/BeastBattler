using TravelTime.Client.Mobile.Services;
using TravelTime.Client.Mobile.ViewModels;
using TravelTime.Client.Mobile.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace TravelTime.Client.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Adding HttpClient
            builder.Services.AddSingleton<HttpClient>();

            // Adding Interfaces
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

            // Adding services
            builder.Services.AddSingleton<SessionService>();
            builder.Services.AddSingleton<AmadeusApiService>();

            // Adding Views and ViewModels
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();

            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterPageViewModel>();

            builder.Services.AddSingleton<MyAccountPage>();
            builder.Services.AddSingleton<MyAccountPageViewModel>();

            builder.Services.AddSingleton<FlightSearchViewModel>();
            builder.Services.AddSingleton<FlightSearchPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
