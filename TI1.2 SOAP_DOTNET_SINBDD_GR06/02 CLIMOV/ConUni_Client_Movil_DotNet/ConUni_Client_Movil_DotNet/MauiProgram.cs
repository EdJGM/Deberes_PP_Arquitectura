using CommunityToolkit.Maui;
using ConUni_Client_Movil_DotNet.Services;
using ConUni_Client_Movil_DotNet.Views;
using Microsoft.Extensions.Logging;

namespace ConUni_Client_Movil_DotNet
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

            // Registrar servicios
            builder.Services.AddSingleton<ConversorService>();

            // Registrar páginas
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MenuPage>();
            builder.Services.AddTransient<TemperaturaPage>();
            builder.Services.AddTransient<LongitudPage>();
            builder.Services.AddTransient<MasaPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
