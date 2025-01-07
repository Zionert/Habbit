using Microsoft.Extensions.Logging;
using Auth0.OidcClient;
using Habbit.Resources.Pages;
using Habbit.Services;
using System.Net.Http;


namespace Habbit
{
    public static class MauiProgram
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
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesomeSolid");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FontAwesomeRegular");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FontAwesomeBrands");
                });



#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-b5nb4y3005k58q3j.us.auth0.com",
                ClientId = "HxsLTgKn5lJfYPS89kBiqB0cWB5U2Kkf",
                RedirectUri = "habbit://callback/",
                PostLogoutRedirectUri = "habbit://callback/",
                Scope = "openid profile email"
            }));
            builder.Services.AddSingleton<AddPage>();
            builder.Services.AddSingleton<GoalsPage>();
            builder.Services.AddSingleton<HabbitsPage>();
            builder.Services.AddSingleton<StaticsPage>();
            builder.Services.AddSingleton<HabitService>();
            builder.Services.AddSingleton<TaskService>();


            return builder.Build();
        }
    }
}
