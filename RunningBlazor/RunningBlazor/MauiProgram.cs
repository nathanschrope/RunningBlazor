using Microsoft.Extensions.Logging;
using RunningBlazor.Shared.Services;
using RunningBlazor.Services;
using RunningBlazor.Shared.Running;
using SQLitePCL;

namespace RunningBlazor;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Batteries.Init();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add device-specific services used by the RunningBlazor.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddSingleton<IDataFactory<RunData>, DataFactory<RunData>>();
        builder.Services.AddSingleton<IDataFactory<Setting>, DataFactory<Setting>>();
        builder.Services.AddSingleton<IDataFactory<DataTab>, DataFactory<DataTab>>();
        builder.Services.AddSingleton<ProgramSettings, ProgramSettings>();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
