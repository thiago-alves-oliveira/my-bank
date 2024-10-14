using IOBBank.Application;
using IOBBank.Core;
using IOBBank.Core.Middlewares;
using IOBBank.DI;
using IOBBank.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Serilog;

namespace IOBBank.Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApiServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            opt.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
            

        services
            .AddSwaggerConfiguration()
            .AddServices(configuration)
            .AddCqrs(typeof(IAssemblyMarker).Assembly);
        
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddLogging(logging => logging.AddSerilog());

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopmentOrInternal())
        {
            app.UseDeveloperExceptionPage();

            var migrator = app.Services.GetRequiredService<Migrator>();

            migrator.Migrate().Wait();
        }

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseSwaggerConfiguration();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }

    public static bool IsDevelopmentOrInternal(this IWebHostEnvironment environment)
    {
        return environment.IsDevelopment() || environment.EnvironmentName == "Internal";
    }
}
