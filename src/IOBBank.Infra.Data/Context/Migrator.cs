using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IOBBank.Infra.Data.Context;

public class Migrator
{
    private readonly IServiceProvider _serviceProvider;

    public Migrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Migrate()
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<IOBBankContext>()
            ?? throw new NullReferenceException();

        await dbContext.Database.MigrateAsync();
    }
}
