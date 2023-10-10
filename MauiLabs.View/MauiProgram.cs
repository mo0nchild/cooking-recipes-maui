using MauiLabs.Dal;
using MauiLabs.View.Pages;
using MauiLabs.View.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MauiLabs.View
{
    public static class MauiProgram : object
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Configuration.AddJsonFile("appsettings.json");
		    builder.Logging.AddDebug();

            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services
                .AddTransient<AnotherPage>()
                .AddTransient<MainPage>();

            builder.Services.AddTransient<UserProfileVm>();
            return builder.Build();
        }
    }
}