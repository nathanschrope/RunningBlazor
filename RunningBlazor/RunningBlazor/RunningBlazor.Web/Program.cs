using RunningBlazor.Web.Components;
using RunningBlazor.Shared.Services;
using RunningBlazor.Web.Services;

namespace RunningBlazor;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        // Add device-specific services used by the RunningBlazor.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(
                typeof(RunningBlazor.Shared._Imports).Assembly,
                typeof(RunningBlazor.Web.Client._Imports).Assembly);

        app.Run();
    }
}
