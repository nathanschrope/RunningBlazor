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

        await builder.Build().RunAsync();
    }
}
