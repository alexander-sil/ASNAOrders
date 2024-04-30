using Microsoft.Extensions.Logging;

namespace ASNAOrders.Web.Administration.Client.Mobile
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
                    fonts.AddFont("Arial-Regular.TTF", "Arial");
                    fonts.AddFont("ArialRounded-Semibold.TTF", "ArialRounded");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
