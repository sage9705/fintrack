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

        builder.Services.AddSingleton<DatabaseContext>();
        builder.Services.AddSingleton<ITransactionService, SQLiteTransactionService>();
        builder.Services.AddTransient<TransactionsViewModel>();
        builder.Services.AddTransient<TransactionsPage>();
        builder.Services.AddTransient<AddTransactionViewModel>();
        builder.Services.AddTransient<AddTransactionPage>();
        builder.Services.AddTransient<EditTransactionViewModel>();
        builder.Services.AddTransient<EditTransactionPage>();
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddSingleton<CategoryService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}