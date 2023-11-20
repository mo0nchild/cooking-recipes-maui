using MauiLabs.View.Pages;
using MauiLabs.View.Services;
using MauiLabs.View.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Resources;

namespace MauiLabs.View
{
    public static class MauiProgram : object
    {
        public static MauiApp CreateMauiApp()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            using (var stream = assembly.GetManifestResourceStream("MauiLabs.View.appsettings.json")) 
            {
                var config = new ConfigurationBuilder().AddJsonStream(stream);
                builder.Configuration.AddConfiguration(config.Build());
            }
		    builder.Logging.AddDebug();
            builder.Services.AddViewServices(builder.Configuration).Wait();
            builder.Services
                .AddTransient<UserProfilePage>()
                .AddTransient<UserListPage>()
                .AddTransient<MainPage>();

            builder.Services.AddTransient<UserListVm>();
            builder.Services.AddTransient<UserProfileVm>();
            return builder.Build();
        }
    }
}