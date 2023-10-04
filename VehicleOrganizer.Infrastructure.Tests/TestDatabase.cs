using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace VehicleOrganizer.Infrastructure.Tests;

public static class TestDatabase
{
    public static void Reset()
    {
        CreateContext().Database.EnsureDeleted();
    }

    public static DataBaseContext CreateContext(bool allowLazyLoading = false)
    {
        var services = new ServiceCollection();
        services.AddTestDatabaseContext();
        services.AddLogging();
        var sp = services.BuildServiceProvider();
        var context = sp.GetRequiredService<DataBaseContext>();

        context.ChangeTracker.LazyLoadingEnabled = allowLazyLoading;

        return context;
    }

    public static DataBaseContext GetTestContext(this IFixture fixture) => CreateContext(true);

    public static void AddTestDatabaseContext(this IFixture fixture, bool allowLazyLoading = false)
    {
        Reset();
        fixture.Register(() => CreateContext(allowLazyLoading));
    }

    public static IServiceCollection AddTestDatabaseContext(this IServiceCollection services)
    {
        services.AddDbContext<DataBaseContext>(builder =>
        {
            builder.UseInMemoryDatabase("Dummy");
            //builder.UseLazyLoadingProxies();
        });

        return services;
    }
}
