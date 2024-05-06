using BeastBattler.Client.Mobile.Services;
using BeastBattler.Client.Mobile.ViewModels;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BeastBattler.Client.Mobile
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

            // Adding Interfaces
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

            // Adding services
            builder.Services.AddSingleton<SessionService>();

            // Adding Views and ViewModels
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();

            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterPageViewModel>();

            builder.Services.AddSingleton<CagesPage>();
            builder.Services.AddSingleton<CagesPageViewModel>();

            builder.Services.AddSingleton<AddCagePage>();
            builder.Services.AddSingleton<AddCagePageViewModel>();

            builder.Services.AddSingleton<CageDetailPage>();
            builder.Services.AddSingleton<CageDetailPageViewModel>();

            builder.Services.AddSingleton<CreaturesPage>();
            builder.Services.AddSingleton<CreaturesPageViewModel>();

            builder.Services.AddSingleton<AddCreaturePage>();
            builder.Services.AddSingleton<AddCreaturePageViewModel>();

            builder.Services.AddSingleton<CreatureDetailPage>();
            builder.Services.AddSingleton<CreatureDetailPageViewModel>();

            builder.Services.AddSingleton<EditCreaturePage>();
            builder.Services.AddSingleton<EditCreaturePageViewModel>();

            builder.Services.AddSingleton<MyAccountPage>();
            builder.Services.AddSingleton<MyAccountPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
