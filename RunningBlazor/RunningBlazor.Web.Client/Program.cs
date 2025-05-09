using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RunningBlazor.Services;
using RunningBlazor.Shared.Running;
using RunningBlazor.Shared.Services;
using RunningBlazor.Web.Client.Services;

namespace RunningBlazor.Web.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        // Add device-specific services used by the RunningBlazor.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddSingleton<IDataFactory<RunData>, DataFactory<RunData>>();
        builder.Services.AddSingleton<IDataFactory<Setting>, DataFactory<Setting>>();
        builder.Services.AddSingleton<IDataFactory<DataTab>, DataFactory<DataTab>>();
        builder.Services.AddSingleton<ProgramSettings, ProgramSettings>();

        await builder.Build().RunAsync();
    }
}
