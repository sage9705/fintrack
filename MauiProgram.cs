using Microsoft.Extensions.Logging;
using FinTrack.Services;
using FinTrack.ViewModels;
using FinTrack.Views;

namespace FinTrack;

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
            });

        builder.Services.AddSingleton<ITransactionService, MockTransactionService>();
        builder.Services.AddTransient<TransactionsViewModel>();
        builder.Services.AddTransient<TransactionsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}