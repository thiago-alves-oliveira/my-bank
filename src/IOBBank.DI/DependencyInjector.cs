using IOBBank.Application.AutoMapper;
using IOBBank.Domain.Interfaces.Repositories;
using IOBBank.Infra.Data.Context;
using IOBBank.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IOBBank.DI;

public static class DependencyInjector
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddSingleton<Migrator>()
            .AddRepositories()
            .AddDbContext(configuration)
            .AddHttpClients();
        return services;
    }


    public static IServiceCollection AddServicesIntegrador(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddRepositories()
            .AddDbContext(configuration)
            .AddHttpClients();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<HttpClient>();            
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();             
        services.AddScoped<IBankLaunchRepository, BankLaunchRepository>();             

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IOBBankContext>(options =>
        {
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 33)), // Ajuste para a versão do MySQL que você está utilizando
                mysqlOptions =>
                {
                    // Definir o comportamento de schema como Ignore
                    mysqlOptions.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Ignore);
                }
            );
        });

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        return services;
    }
}
