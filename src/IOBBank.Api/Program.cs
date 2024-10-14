using IOBBank.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

        builder.Services.AddApiServicesConfiguration(builder.Configuration);



        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromDays(2));

        var app = builder.Build();

        app.UseApiConfiguration();
        app.MapGet("/", () => "Version: %BUILD_NUMBER%");

        app.Run();
    }
}